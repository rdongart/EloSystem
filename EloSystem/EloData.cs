using CustomExtensionMethods;
using EloSystem.IO;
using EloSystem.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EloSystem
{
    [Serializable]
    public class EloData : ISerializable
    {
        private const int IMAGEID_NO_IMAGE = 0;
        internal const double EXP_WIN_RATIO_EVEN_RATING = 0.5;
        internal const double EXP_WIN_RATIO_MAX = 1.0;
        private const double EXP_WIN_RATIO_MIN = 0.0;

        private int imagesSaved = 0;
        private int nextImageID
        {
            get
            {
                return this.imagesSaved + this.resHandler.UnsavedImages + 1;
            }
        }
        private List<Country> countries;
        private List<Map> maps;
        private List<SCPlayer> players;
        private List<Team> teams;
        private List<Tileset> tileSets;
        private List<Tournament> tournaments;
        private Tournament defaultTournament
        {
            get
            {
                return this.tournaments[0];
            }
        }
        [field: NonSerialized]
        private ResourceHandler resHandler;
        private string path
        {
            get
            {
                return StaticMembers.SaveDirectory + this.Name + StaticMembers.FILE_EXTENSION_NAME;
            }
        }
        [field: NonSerialized]
        public bool DataWasChanged { get; private set; }
        [field: NonSerialized]
        public EventHandler CountryPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler MapPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler PlayerPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler SeasonPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler TeamPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler TilesetPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler TournamentPoolChanged = delegate { };
        public string Name { get; private set; }

        public EloData(string name)
        {
            this.Name = name;

            this.countries = new List<Country>();
            this.maps = new List<Map>();
            this.players = new List<SCPlayer>();
            this.teams = new List<Team>();
            this.tileSets = new List<Tileset>();
            this.tournaments = new List<Tournament>() { new Tournament("", EloData.IMAGEID_NO_IMAGE) };
            this.DataWasChanged = true;
            this.resHandler = new ResourceHandler(StaticMembers.SaveDirectory + name);
        }

        public static double ExpectedWinRatio(int ownRating, int opponentRating)
        {
            const int RATING_POINTS_PER_EXPECTED_WINRATIO_PERCENTAGE = 15;
            const double MAX_RATING_DISTANCE_DETERMINER = 0.01 / RATING_POINTS_PER_EXPECTED_WINRATIO_PERCENTAGE;


            return ((ownRating - opponentRating) * MAX_RATING_DISTANCE_DETERMINER + EloData.EXP_WIN_RATIO_EVEN_RATING).TruncateToRange(EloData.EXP_WIN_RATIO_MIN, EloData.EXP_WIN_RATIO_MAX);
        }

        public static int RatingChange(Game game)
        {
            return EloData.RatingChange(game.Winner, game.WinnersRace, game.Loser, game.LosersRace);
        }

        public static int RatingChange(SCPlayer winner, Race winnersRace, SCPlayer loser, Race losersRace)
        {
            const int RATING_CHANGE_STANDARD_MAX = 30;

            double winnerExpectedWinRatio = EloData.ExpectedWinRatio(winner.RatingsVs.GetValueFor(losersRace), loser.RatingsVs.GetValueFor(winnersRace));

            return (RATING_CHANGE_STANDARD_MAX * (EloData.EXP_WIN_RATIO_MAX - winnerExpectedWinRatio) * EloData.RatingBonusFactorTotal(winner, winnersRace, loser, losersRace)).RoundToInt();
        }
        /// <summary>
        /// Returns a factor that increases the rating value changes based on the rating calibration phase that at least one player in a game is in.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private static double RatingBonusFactorTotal(SCPlayer player1, Race player1Race, SCPlayer player2, Race player2Race)
        {
            const int BONUSFACTOR_STANDARD = 1;

            return BONUSFACTOR_STANDARD + EloData.RatingBonusFactorByPlayer(player1, player2Race) + EloData.RatingBonusFactorByPlayer(player2, player1Race);
        }

        private static double RatingBonusFactorByPlayer(SCPlayer player, Race opponentRace)
        {
            const int BONUSFACTOR_STANDARD = 0;
            const double CALIBRATION_PHASE1_BONUSFACTOR = 1.5;
            const int CALIBRATION_PHASE1_NO_MATCHES = 10;
            const double CALIBRATION_PHASE2_BONUSFACTOR = 1;
            const int CALIBRATION_PHASE2_NO_MATCHES = 15 + CALIBRATION_PHASE1_NO_MATCHES;
            const double CALIBRATION_PHASE3_BONUSFACTOR = 0.5;
            const int CALIBRATION_PHASE3_NO_MATCHES = 30 + CALIBRATION_PHASE2_NO_MATCHES;

            if (player.Stats.GamesVs(opponentRace) < CALIBRATION_PHASE1_NO_MATCHES) { return CALIBRATION_PHASE1_BONUSFACTOR; }
            else if (player.Stats.GamesVs(opponentRace) < CALIBRATION_PHASE2_NO_MATCHES) { return CALIBRATION_PHASE2_BONUSFACTOR; }
            else if (player.Stats.GamesVs(opponentRace) < CALIBRATION_PHASE3_NO_MATCHES) { return CALIBRATION_PHASE3_BONUSFACTOR; }
            else { return BONUSFACTOR_STANDARD; }
        }

        private static void UpdateMapStats(Match match)
        {
            foreach (Game game in match.GetGames().Where(gm => gm.Map != null))
            {
                game.Map.Stats.ReportMatch(game.Player1Race, game.Player2Race, game.WinnersRace, match.Player1.RatingsVs.GetValueFor(game.Player2Race), match.Player2.RatingsVs.GetValueFor(game.Player1Race));
            }
        }

        private static void UpdatePlayerStats(Match match)
        {
            foreach (GameEntry game in match.GetEntries())
            {
                match.Player1.RatingsVs.AddValueTo(game.Player2Race, game.RatingChange * (game.WinnerWas == PlayerSlotType.Player1 ? 1 : -1));
                match.Player2.RatingsVs.AddValueTo(game.Player1Race, game.RatingChange * (game.WinnerWas == PlayerSlotType.Player2 ? 1 : -1));

                if (game.WinnerWas == PlayerSlotType.Player1)
                {
                    match.Player1.Stats.ReportWin(game.Player1Race, game.Player2Race);
                    match.Player2.Stats.ReportLoss(game.Player2Race, game.Player1Race);
                }
                else if (game.WinnerWas == PlayerSlotType.Player2)
                {
                    match.Player1.Stats.ReportLoss(game.Player1Race, game.Player2Race);
                    match.Player2.Stats.ReportWin(game.Player2Race, game.Player1Race);
                }
            }
        }

        #region Implementing ISerializable
        private enum Field
        {
            Countries, Maps, Name, ImagesSaved, Players, Teams, TileSets, Tournaments
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Countries.ToString(), (List<Country>)this.countries);
            info.AddValue(Field.Maps.ToString(), (List<Map>)this.maps);
            info.AddValue(Field.Tournaments.ToString(), (List<Tournament>)this.tournaments);
            info.AddValue(Field.Name.ToString(), (string)this.Name);
            info.AddValue(Field.ImagesSaved.ToString(), (int)this.imagesSaved);
            info.AddValue(Field.Players.ToString(), (List<SCPlayer>)this.players);
            info.AddValue(Field.Teams.ToString(), (List<Team>)this.teams);
            info.AddValue(Field.TileSets.ToString(), (List<Tileset>)this.tileSets);
        }
        internal EloData(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Countries: this.countries = (List<Country>)info.GetValue(field.ToString(), typeof(List<Country>)); break;
                        case Field.Maps: this.maps = (List<Map>)info.GetValue(field.ToString(), typeof(List<Map>)); break;
                        case Field.Tournaments: this.tournaments = (List<Tournament>)info.GetValue(field.ToString(), typeof(List<Tournament>)); break;
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                        case Field.ImagesSaved: this.imagesSaved = (int)info.GetInt32(field.ToString()); break;
                        case Field.Players: this.players = (List<SCPlayer>)info.GetValue(field.ToString(), typeof(List<SCPlayer>)); break;
                        case Field.Teams: this.teams = (List<Team>)info.GetValue(field.ToString(), typeof(List<Team>)); break;
                        case Field.TileSets: this.tileSets = (List<Tileset>)info.GetValue(field.ToString(), typeof(List<Tileset>)); break;
                    }
                }

                this.MapPoolChanged = delegate { };
            }

            this.resHandler = new ResourceHandler(StaticMembers.SaveDirectory + this.Name);
            this.DataWasChanged = false;
        }
        #endregion

        /// <summary>
        /// Adds a new image to the resources.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Returns an integer representing the image id by which the new image has been added.</returns>
        private int AddNewImage(Image image)
        {
            if (image == null) { return EloData.IMAGEID_NO_IMAGE; }
            else
            {
                int imageID = this.nextImageID;

                this.resHandler.AddImage(image, imageID);

                return imageID;
            }
        }

        public bool AddCountry(string name, Image image = null)
        {
            if (name != string.Empty && !this.countries.Any(country => country.Name == name))
            {
                this.countries.Add(new Country(name, this.AddNewImage(image)));

                this.DataWasChanged = true;

                this.CountryPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddMap(string name, MapPlayerType maptype, Image image = null)
        {
            if (name != string.Empty && !this.maps.Any(map => map.Name == name))
            {
                this.maps.Add(new Map(name, this.AddNewImage(image), maptype));

                this.DataWasChanged = true;

                this.MapPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddPlayer(string name, Team team = null, Country country = null, Image image = null)
        {
            return this.AddPlayer(name, new string[] { }, String.Empty, EloSystemStaticMembers.START_RATING_DEFAULT, team, country, image);
        }

        public bool AddPlayer(string name, IEnumerable<string> aliases, string irlName = "", int startRating = EloSystemStaticMembers.START_RATING_DEFAULT, Team team = null, Country country = null
            , Image image = null, DateTime birthDate = new DateTime())
        {
            if (name != string.Empty && !this.players.Any(player => player.Name == name))
            {
                var player = new SCPlayer(name, startRating, this.AddNewImage(image), aliases, team, country) { IRLName = irlName };
                player.SetBirthDate(birthDate);

                this.players.Add(player);
                this.DataWasChanged = true;

                this.PlayerPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddSeason(string name, Tournament tournament)
        {
            if (name != string.Empty && !tournament.GetSeasons().Any(season => season.Name == name))
            {
                tournament.AddSeason(new Season(name));

                this.DataWasChanged = true;

                this.SeasonPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddTeam(string name, Image image = null)
        {
            if (name != string.Empty && !this.teams.Any(team => team.Name == name))
            {
                this.teams.Add(new Team(name, this.AddNewImage(image)));

                this.DataWasChanged = true;

                this.TeamPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddTileSet(string name)
        {
            if (name != string.Empty && !this.tileSets.Any(tileSet => tileSet.Name == name))
            {
                this.tileSets.Add(new Tileset(name));

                this.DataWasChanged = true;

                this.TilesetPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddTournament(string name, Image image = null)
        {
            if (name != string.Empty && !this.tournaments.Any(tournament => tournament.Name == name))
            {
                this.tournaments.Add(new Tournament(name, this.AddNewImage(image)));

                this.DataWasChanged = true;

                this.TournamentPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public void RemoveCountry(string name)
        {
            Country countryToRemove = this.GetCountry(name);

            this.RemoveCountry(countryToRemove);
        }

        public void RemoveCountry(Country country)
        {
            if (country == null) { return; }

            if (country.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(country.ImageID); }

            if (this.countries.Remove(country))
            {
                this.DataWasChanged = true;

                this.CountryPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemoveMap(string name)
        {
            Map mapToRemove = this.GetMap(name);

            this.RemoveMap(mapToRemove);
        }

        public void RemoveMap(Map map)
        {
            if (map == null) { return; }

            if (map.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(map.ImageID); }

            if (this.maps.Remove(map))
            {
                this.DataWasChanged = true;

                this.MapPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemovePlayer(string name)
        {
            SCPlayer playerToRemove = this.GetPlayer(name);

            this.RemovePlayer(playerToRemove);
        }

        public void RemovePlayer(SCPlayer player)
        {
            if (player == null) { return; }

            if (player.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(player.ImageID); }

            if (this.players.Remove(player))
            {
                this.DataWasChanged = true;

                this.PlayerPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemoveSeason(string name, Tournament tournament)
        {
            Season seasonToRemove = tournament.GetSeasons().FirstOrDefault(season => season.Name == name);

            this.RemoveSeason(seasonToRemove, tournament);
        }

        public void RemoveSeason(Season season, Tournament tournament)
        {
            if (season == null || tournament == this.defaultTournament) { return; }

            if (tournament.RemoveSeason(season))
            {
                this.DataWasChanged = true;

                this.SeasonPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemoveTeam(string name)
        {
            Team teamToRemove = this.GetTeam(name);

            this.RemoveTeam(teamToRemove);
        }

        public void RemoveTeam(Team team)
        {
            if (team == null) { return; }

            if (team.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(team.ImageID); }

            if (this.teams.Remove(team))
            {
                this.DataWasChanged = true;

                this.TeamPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemoveTileSet(string name)
        {
            Tileset tilesetToRemove = this.GetTileSet(name);

            this.RemoveTileSet(tilesetToRemove);
        }

        public void RemoveTileSet(Tileset tileSet)
        {
            if (tileSet == null) { return; }

            if (this.tileSets.Remove(tileSet))
            {
                this.DataWasChanged = true;

                this.TilesetPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void RemoveTournament(string name)
        {
            Tournament tournamentToRemove = this.GetTournament(name);

            this.RemoveTournament(tournamentToRemove);
        }

        public void RemoveTournament(Tournament tournament)
        {
            if (tournament == null || tournament == this.defaultTournament) { return; }

            if (this.tournaments.Remove(tournament))
            {
                this.DataWasChanged = true;

                this.TournamentPoolChanged.Invoke(this, new EventArgs());
            }
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries)
        {
            this.ReportMatch(player1, player2, entries, this.defaultTournament.DefaultSeason);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, DateTime date)
        {
            this.ReportMatch(player1, player2, entries, this.defaultTournament.DefaultSeason, date);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Season season)
        {
            this.ReportMatch(player1, player2, entries, season, DateTime.Today);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Tournament tournament)
        {
            this.ReportMatch(player1, player2, entries, tournament == null ? this.defaultTournament.DefaultSeason : tournament.DefaultSeason, DateTime.Today);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Tournament tournament, DateTime date)
        {
            this.ReportMatch(player1, player2, entries, tournament == null ? this.defaultTournament.DefaultSeason : tournament.DefaultSeason, date);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Season season, DateTime date)
        {
            var match = new Match(player1, player2, entries.Select(entry => new GameEntry(entry.WinnerWas, entry.Player1Race, entry.Player2Race, entry.Map)), date);

            EloData.UpdateMapStats(match);
            EloData.UpdatePlayerStats(match);

            if (season != null) { season.AddMatch(match); }
            else { this.defaultTournament.DefaultSeason.AddMatch(match); }

            this.DataWasChanged = true;
        }

        /// <summary>
        /// Saves the EloData and related resources.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns>A boolean value representing whether the process was stored.</returns>
        public bool SaveData(string fileName, FileOverwriteEventHandler handler = null)
        {
            if (!Directory.Exists(StaticMembers.SaveDirectory)) { Directory.CreateDirectory(StaticMembers.SaveDirectory); }

            if (handler != null && File.Exists(StaticMembers.SaveDirectory + fileName + StaticMembers.FILE_EXTENSION_NAME) && !handler.Invoke(this, new FileOverwriteEventArgs(fileName))) { return false; }

            this.Name = fileName;
            this.imagesSaved += this.resHandler.UnsavedImages;

            using (Stream saveStream = File.Open(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(saveStream, this);
                saveStream.Close();
            }

            this.resHandler.SaveResourceChanges(StaticMembers.SaveDirectory + this.Name);

            this.DataWasChanged = false;

            return true;
        }

        public Country GetCountry(string name)
        {
            return this.countries.FirstOrDefault(country => country.Name == name);
        }

        public IEnumerable<Country> GetCountries()
        {
            foreach (Country country in this.countries.ToList()) { yield return country; }
        }

        public IEnumerable<Game> GetAllGames()
        {
            foreach (Game game in this.defaultTournament.GetGames().ToList())
            {
                game.Tournament = null;

                yield return game;
            }

            foreach (Game game in this.tournaments.Skip(1).SelectMany(tournament => tournament.GetGames()).ToList()) { yield return game; }
        }

        public IEnumerable<Map> GetMaps()
        {
            foreach (Map map in this.maps.ToList()) { yield return map; }
        }

        public IEnumerable<SCPlayer> GetPlayers()
        {
            foreach (SCPlayer player in this.players.ToList(this.players.Count)) { yield return player; }
        }

        public IEnumerable<Team> GetTeams()
        {
            foreach (Team team in this.teams.ToList()) { yield return team; }
        }

        public IEnumerable<Tileset> GetTileSets()
        {
            foreach (Tileset tileSet in this.tileSets.ToList()) { yield return tileSet; }
        }

        public IEnumerable<Tournament> GetTournaments()
        {
            foreach (Tournament tournamet in this.tournaments.Skip(1).ToList()) { yield return tournamet; }
        }

        public bool TryGetImage(int imageID, out EloImage eloImg)
        {
            eloImg = this.resHandler.GetImage(imageID);

            return eloImg.Image != null;
        }

        public Map GetMap(string name)
        {
            return this.maps.FirstOrDefault(map => map.Name == name);
        }

        public SCPlayer GetPlayer(string name)
        {
            return this.players.FirstOrDefault(player => player.Name == name);
        }

        public Team GetTeam(string name)
        {
            return this.teams.FirstOrDefault(team => team.Name == name);
        }

        public Tileset GetTileSet(string name)
        {
            return this.tileSets.FirstOrDefault(tileSet => tileSet.Name == name);
        }

        public Tournament GetTournament(string name)
        {
            return this.tournaments.FirstOrDefault(tournament => tournament.Name == name);
        }
    }
}
