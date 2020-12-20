using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
    public partial class EloData : ISerializable
    {
        private const int IMAGEID_NO_IMAGE = 0;
        internal const double EXP_WIN_RATIO_EVEN_RATING = 0.5;
        internal const double EXP_WIN_RATIO_MAX = 1.0;
        private const double EXP_WIN_RATIO_MIN = 0.0;

        private IDHandler idHandler;
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
        public bool ContentHasBeenChanged { get; private set; }
        [field: NonSerialized]
        public EventHandler CountryPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler MapPoolChanged = delegate { };
        [field: NonSerialized]
        public EventHandler MatchPoolChanged = delegate { };
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

            this.idHandler = new IDHandler();

            this.countries = new List<Country>();
            this.maps = new List<Map>();
            this.players = new List<SCPlayer>();
            this.teams = new List<Team>();
            this.tileSets = new List<Tileset>();
            this.tournaments = new List<Tournament>() { new Tournament("", EloData.IMAGEID_NO_IMAGE, 0) };
            this.ContentHasBeenChanged = true;
            this.resHandler = new ResourceHandler(StaticMembers.SaveDirectory + name);
        }

        public static double ExpectedWinRatio(int ownRating, int opponentRating)
        {
            const int RATING_POINTS_PER_EXPECTED_WINRATIO_PERCENTAGE = 15;
            const double MAX_RATING_DISTANCE_DETERMINER = 0.01 / RATING_POINTS_PER_EXPECTED_WINRATIO_PERCENTAGE;


            return ((ownRating - opponentRating) * MAX_RATING_DISTANCE_DETERMINER + EloData.EXP_WIN_RATIO_EVEN_RATING).TruncateToRange(EloData.EXP_WIN_RATIO_MIN, EloData.EXP_WIN_RATIO_MAX);
        }

        /// <summary>
        /// Calculates the rating change for a game.
        /// </summary>
        /// <param name="winnersStats"></param>
        /// <param name="winnersRace"></param>
        /// <param name="losersStats"></param>
        /// <param name="losersRace"></param>
        /// <param name="ewrWinner">Expected win rate of the winner. Should be a value between 0 and 1.</param>
        /// <returns></returns>
        internal static int RatingChange(WinRateCounter winnersStats, Race winnersRace, WinRateCounter losersStats, Race losersRace, double ewrWinner)
        {
            return EloData.RatingChange(winnersStats.GamesVs(losersRace), losersStats.GamesVs(winnersRace), ewrWinner);
        }

        public static int RatingChange(PlayerStatsClone winnersStats, Race winnersRace, PlayerStatsClone losersStats, Race losersRace)
        {
            return EloData.RatingChange(winnersStats.Stats, winnersRace, losersStats.Stats, losersRace, EloData.ExpectedWinRatio(winnersStats.RatingVs.GetValueFor(losersRace)
                , losersStats.RatingVs.GetValueFor(winnersRace)));
        }

        /// <summary>
        /// Calculates the rating change for a game.
        /// </summary>
        /// <param name="winnersGamesPlayedVsRace">The number of games played by the winner against losers race.</param>
        /// <param name="losersGamesPlayedVsRace">The number of games played by the loser against winners race.</param>
        /// <param name="ewrWinner">Expected win rate of the winner. Should be a value between 0 and 1.</param>
        /// <returns></returns>
        public static int RatingChange(int winnersGamesPlayedVsRace, int losersGamesPlayedVsRace, double ewrWinner)
        {
            const int K_FACTOR = 36;

            return (K_FACTOR * (EloData.EXP_WIN_RATIO_MAX - ewrWinner.TruncateToRange(0, 1)) * EloData.RatingBonusFactorTotal(winnersGamesPlayedVsRace, losersGamesPlayedVsRace)).RoundToInt();
        }

        /// <summary>
        /// Returns a factor that increases the rating value changes based on the rating calibration phase that at least one player in a game is in.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="player2Stats"></param>
        /// <returns></returns>
        private static double RatingBonusFactorTotal(WinRateCounter player1Stats, Race player1Race, WinRateCounter player2Stats, Race player2Race)
        {
            return EloData.RatingBonusFactorByPlayer(player1Stats.GamesVs(player2Race)) + EloData.RatingBonusFactorByPlayer(player2Stats.GamesVs(player1Race));
        }

        private static double RatingBonusFactorTotal(int winnersGamesPlayedVsRace, int losersGamesPlayedVsRace)
        {
            const int BONUSFACTOR_STANDARD = 1;

            return BONUSFACTOR_STANDARD + EloData.RatingBonusFactorByPlayer(winnersGamesPlayedVsRace) + EloData.RatingBonusFactorByPlayer(losersGamesPlayedVsRace);
        }

        private static double RatingBonusFactorByPlayer(WinRateCounter playerStats, Race opponentRace)
        {
            return EloData.RatingBonusFactorByPlayer(playerStats.GamesVs(opponentRace));
        }

        private static double RatingBonusFactorByPlayer(int gamesPlayedVsRace)
        {
            const int BONUSFACTOR_STANDARD = 0;
            const double CALIBRATION_PHASE1_BONUSFACTOR = 1.5;
            const int CALIBRATION_PHASE1_NO_MATCHES = 10;
            const double CALIBRATION_PHASE2_BONUSFACTOR = 1;
            const int CALIBRATION_PHASE2_NO_MATCHES = 15 + CALIBRATION_PHASE1_NO_MATCHES;
            const double CALIBRATION_PHASE3_BONUSFACTOR = 0.5;
            const int CALIBRATION_PHASE3_NO_MATCHES = 30 + CALIBRATION_PHASE2_NO_MATCHES;

            if (gamesPlayedVsRace < CALIBRATION_PHASE1_NO_MATCHES) { return CALIBRATION_PHASE1_BONUSFACTOR; }
            else if (gamesPlayedVsRace < CALIBRATION_PHASE2_NO_MATCHES) { return CALIBRATION_PHASE2_BONUSFACTOR; }
            else if (gamesPlayedVsRace < CALIBRATION_PHASE3_NO_MATCHES) { return CALIBRATION_PHASE3_BONUSFACTOR; }
            else { return BONUSFACTOR_STANDARD; }
        }

        private static void UpdateMapStats(Match match)
        {
            foreach (Game game in match.GetGames().Where(gm => gm.Map != null))
            {
                game.Map.Stats.ReportMatch(game.Player1Race, game.Player2Race, game.WinnersRace, match.Player1.RatingVs.GetValueFor(game.Player2Race), match.Player2.RatingVs.GetValueFor(game.Player1Race));
            }
        }

        private static void UpdatePlayerStats(Match match)
        {
            foreach (GameEntry game in match.GetEntries())
            {
                match.Player1.RatingVs.AddValueTo(game.Player2Race, game.RatingChange * (game.WinnerWas == PlayerSlotType.Player1 ? 1 : -1));
                match.Player2.RatingVs.AddValueTo(game.Player1Race, game.RatingChange * (game.WinnerWas == PlayerSlotType.Player2 ? 1 : -1));

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

        public static double ExpectedWinRatio(PlayerStatsClone playerStats, Race racePlayer, PlayerStatsClone opponentStats, Race raceOpponent)
        {
            return EloData.ExpectedWinRatio(playerStats.RatingVs.GetValueFor(raceOpponent), opponentStats.RatingVs.GetValueFor(racePlayer));
        }

        #region Implementing ISerializable
        private enum Field
        {
            idHandler, Countries, Maps, Name, ImagesSaved, Players, Teams, TileSets, Tournaments
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.idHandler.ToString(), (IDHandler)this.idHandler);
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
                        case Field.idHandler: this.idHandler = (IDHandler)info.GetValue(field.ToString(), typeof(IDHandler)); break;
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
            this.ContentHasBeenChanged = false;

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

        private IEnumerable<Match> GetMatches()
        {
            return this.tournaments.SelectMany(t => t.GetMatches());
        }

        public bool AddCountry(string name, Image image = null)
        {
            if (name != string.Empty && !this.countries.Any(country => country.Name == name))
            {
                this.countries.Add(new Country(name, this.AddNewImage(image), this.idHandler.GetCountryIDNext()));

                this.ContentHasBeenChanged = true;

                this.CountryPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddMap(string name, MapPlayerType maptype, Size size, Tileset tiles, Image image = null)
        {
            if (name != string.Empty && !this.maps.Any(map => map.Name == name))
            {
                this.maps.Add(new Map(name, this.AddNewImage(image), maptype, tiles, this.idHandler.GetMapIDNext()) { Size = size });

                this.ContentHasBeenChanged = true;

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
            if (name != string.Empty)
            {
                var player = new SCPlayer(name, startRating, this.AddNewImage(image), this.idHandler.GetPlayerIDNext(), aliases, team, country) { IRLName = irlName };
                player.SetBirthDate(birthDate);

                this.players.Add(player);
                this.ContentHasBeenChanged = true;

                this.PlayerPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddSeason(string name, Tournament tournament)
        {
            if (name != string.Empty && !tournament.GetSeasons().Any(season => season.Name == name))
            {
                tournament.AddSeason(new Season(name, this.idHandler.GetSeasonIDNext()));

                this.ContentHasBeenChanged = true;

                this.SeasonPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddTeam(string nameShort, string nameLong, Image image = null)
        {
            this.teams.Add(new Team(nameShort, this.AddNewImage(image), this.idHandler.GetSeasonIDNext()) { NameLong = nameLong });

            this.ContentHasBeenChanged = true;

            this.TeamPoolChanged.Invoke(this, new EventArgs());

            return true;
        }

        public bool AddTileSet(string name)
        {
            if (name != string.Empty && !this.tileSets.Any(tileSet => tileSet.Name == name))
            {
                this.tileSets.Add(new Tileset(name, this.idHandler.GetTileSetIDNext()));

                this.ContentHasBeenChanged = true;

                this.TilesetPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
        }

        public bool AddTournament(string nameShort, string nameLong, Image image = null)
        {
            this.tournaments.Add(new Tournament(nameShort, this.AddNewImage(image), this.idHandler.GetTournamentIDNext()) { NameLong = nameLong });

            this.ContentHasBeenChanged = true;

            this.TournamentPoolChanged.Invoke(this, new EventArgs());

            return true;
        }

        /// <summary>
        /// Returns a clone of a player's stats just prior to the date and or daily index provided.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="date">A given date in time.</param>
        /// <param name="dailyMatchIndex">The match index of the selected date.</param>
        /// <returns></returns>
        public PlayerStatsClone PlayerStatsAtPointInTime(SCPlayer player, DateTime date, int dailyMatchIndex = Match.DAILYINDEX_MOSTRECENT)
        {
            var playerStatsClone = new PlayerStatsClone(player);

            foreach (Match match in this.GetMatches().Where(match => match.HasPlayer(player) && match.IsMoreRecentOrSameAs(date, dailyMatchIndex))) { playerStatsClone.RollBackResult(match); }

            return playerStatsClone;
        }

        public IEnumerable<PlayerStatsCloneDev> PlayerStatsDevelopment(SCPlayer player)
        {
            IEnumerable<DateTime> playersGameDates = this.GetMatches().Where(match => match.HasPlayer(player)).Select(match => match.DateTime);

            return playersGameDates.Any() ? this.PlayerStatsDevelopment(player, playersGameDates.Min()) : new PlayerStatsCloneDev[] { };
        }

        public IEnumerable<PlayerStatsCloneDev> PlayerStatsDevelopment(SCPlayer player, DateTime dateFrom)
        {
            var playerStatsClone = new PlayerStatsClone(player);

            int daysBackCounter = 0;

            DateTime todayDate = DateTime.Today;

            yield return new PlayerStatsCloneDev(playerStatsClone, todayDate);

            while (dateFrom.CompareTo(todayDate.Subtract(new TimeSpan(daysBackCounter, 0, 0, 0))) <= 0)
            {
                DateTime currentDate = todayDate.Subtract(new TimeSpan(daysBackCounter, 0, 0, 0));

                foreach (Match match in this.GetMatches().Where(m => m.HasPlayer(player) && m.DateTime.Date.Equals(currentDate))) { playerStatsClone.RollBackResult(match); }

                DateTime dayBeforeCurrentDate = DateTime.Today.Subtract(new TimeSpan(daysBackCounter + 1, 0, 0, 0, 0));

                yield return new PlayerStatsCloneDev(playerStatsClone, dayBeforeCurrentDate);

                daysBackCounter++;
            }

        }

        public int BacktraceInitialRating(SCPlayer player)
        {
            var playerStatsClone = new PlayerStatsClone(player);

            foreach (Match match in this.GetMatches().Where(m => m.HasPlayer(player))) { playerStatsClone.RollBackResult(match); }

            return playerStatsClone.RatingTotal();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="image">Set value to null in order to remove current image from the item.</param>
        public void EditImage(EloSystemContent content, Image image)
        {
            int oldImageID = content.ImageID;

            content.ImageID = this.AddNewImage(image);

            if (!this.ImageIDIsInUse(oldImageID)) { this.resHandler.RemoveImage(oldImageID); }

            this.ContentHasBeenChanged = true;
        }

        private bool ImageIDIsInUse(int imageID)
        {
            if (imageID == EloData.IMAGEID_NO_IMAGE) { return false; }

            IEnumerable<EloSystemContent> contentWithImages = this.countries.Cast<EloSystemContent>().Concat(this.maps).Concat(this.players).Concat(this.teams).Concat(this.tournaments);

            return contentWithImages.Any(item => item.ImageID == imageID);
        }

        public void RemoveCountry(Country country)
        {
            if (this.countries.Remove(country))
            {
                if (country.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(country.ImageID); }

                this.ContentHasBeenChanged = true;

                foreach (SCPlayer player in this.players.Where(p => p.Country == country)) { player.Country = null; }

                this.CountryPoolChanged.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Attempts to remove a map from the database. Only maps with no game entries can be removed.
        /// </summary>
        /// <param name="map"></param>
        public bool RemoveMap(Map map)
        {
            if (!this.GetAllGameEntries().Any(entry => entry.Map == map) && this.maps.Remove(map))
            {
                // check if the content had an image id we also need to remove and make sure that no other contents are using an identical imageID
                if (map.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(map.ImageID); }

                this.ContentHasBeenChanged = true;

                this.MapPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }

        }

        /// <summary>
        /// Attempts to remove a player from the database. Only players with no game entries can be removed.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool RemovePlayer(SCPlayer player)
        {
            if (!this.GetMatches().Any(match => match.HasPlayer(player)) && this.players.Remove(player))
            {
                if (player.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(player.ImageID); }

                this.ContentHasBeenChanged = true;

                this.PlayerPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }

        }

        /// <summary>
        /// Attempts to remove a season from the database. Only seasons with no game entries can be removed.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        public bool RemoveSeason(Season season)
        {
            Tournament tournamentWithSeason = this.tournaments.FirstOrDefault(t => t.GetSeasons().Contains(season));

            if (season.GetMatches().IsEmpty() && tournamentWithSeason != null && tournamentWithSeason != this.defaultTournament && tournamentWithSeason.RemoveSeason(season))
            {
                this.ContentHasBeenChanged = true;

                this.SeasonPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }

        }

        /// <summary>
        /// The number of games in the database.
        /// </summary>
        /// <returns></returns>
        public int GameCount()
        {
            return this.GetMatches().SelectMany(match => match.GetEntries()).Count();
        }

        /// <summary>
        /// The number of matches in the database.
        /// </summary>
        /// <returns></returns>
        public int MatchCount()
        {
            return this.GetMatches().Count();
        }

        /// <summary>
        /// Removes a team from the database.
        /// </summary>
        /// <param name="team"></param>
        public void RemoveTeam(Team team)
        {
            if (team == null) { return; }

            if (this.teams.Remove(team))
            {
                if (team.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(team.ImageID); }

                this.ContentHasBeenChanged = true;

                foreach (SCPlayer player in this.players.Where(p => p.Team == team)) { player.Team = null; }

                this.TeamPoolChanged.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Removes a tileset from the database.
        /// </summary>
        /// <param name="tileSet"></param>
        public void RemoveTileSet(Tileset tileSet)
        {
            if (tileSet == null) { return; }

            if (this.tileSets.Remove(tileSet))
            {
                this.ContentHasBeenChanged = true;

                foreach (Map map in this.maps.Where(m => m.Tileset == tileSet)) { map.Tileset = null; }

                this.TilesetPoolChanged.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Attempts to remove a tournament from the database. Only tournaments with no game entries can be removed.
        /// </summary>
        /// <param name="tournament"></param>
        /// <returns></returns>
        public bool RemoveTournament(Tournament tournament)
        {
            if (tournament != this.defaultTournament && tournament.GetGames().IsEmpty() && this.tournaments.Remove(tournament))
            {
                if (tournament.ImageID != EloData.IMAGEID_NO_IMAGE) { this.resHandler.RemoveImage(tournament.ImageID); }

                this.ContentHasBeenChanged = true;

                this.TournamentPoolChanged.Invoke(this, new EventArgs());

                return true;
            }
            else { return false; }
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
            this.ReportMatch(player1, player2, entries, season, DateTime.Now);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Tournament tournament)
        {
            this.ReportMatch(player1, player2, entries, tournament == null ? this.defaultTournament.DefaultSeason : tournament.DefaultSeason, DateTime.Now);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Tournament tournament, DateTime date)
        {
            this.ReportMatch(player1, player2, entries, tournament == null ? this.defaultTournament.DefaultSeason : tournament.DefaultSeason, date);
        }

        public void ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries, Season season, DateTime date)
        {
            // handle case where this match is older than matches already reported
            if (this.GetMatches().OrderByNewestFirst().Any(m => m.DateTime.CompareTo(date) > 0))
            {
                int newerMatchesAlreadyInDataBase = this.GetMatches().OrderByNewestFirst().Count(m => m.DateTime.CompareTo(date) > 0);

                var matchesToReenter = this.RollBackLastMatches(this.GetMatches().OrderByNewestFirst().Count(m => m.DateTime.CompareTo(date) > 0)).DistinctBy(m => m.Match).Select(game => new
                {
                    Match = game.Match,
                    Season = game.Season,
                    Tournament = game.Tournament
                }).ToList();

                this.ReportMatch(new Match(player1, player2, entries, date), season, matchesToReenter.IsEmpty());

                this.ReenterMatches(matchesToReenter.Select(matchItem => new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));
            }
            else { this.ReportMatch(new Match(player1, player2, entries, date), season, true); }

        }

        public void ChangeDailyIndex(Match matchToChange, int dailyIndexChange)
        {
            const int MATCH_TO_CHANGE = 1;

            int noMatchesToReenter = Math.Max(this.GetMatches().OrderByNewestFirst().TakeWhile(match => match.DateTime.Date.CompareTo(matchToChange.DateTime.Date) > 0
                || (match.DateTime.Date.CompareTo(matchToChange.DateTime.Date) == 0 && match.DailyIndex + dailyIndexChange <= matchToChange.DailyIndex)).Count()
                , this.GetMatches().OrderByNewestFirst().IndexOf(matchToChange) + MATCH_TO_CHANGE);

            var matchesToReenter = this.RollBackLastMatches(noMatchesToReenter).DistinctBy(m => m.Match).Select(game => new
            {
                Match = game.Match,
                Season = game.Season,
                Tournament = game.Tournament
            }).ToList();// we need to call ToList() here becauase we have been modifying the data base by remove the returned values

            var sameDateMatches = matchesToReenter.Where(m => m.Match.DateTime.Date.Equals(matchToChange.DateTime.Date));

            int newDailyIndex = matchToChange.DailyIndex + dailyIndexChange;
            int currentDailyIndex = matchToChange.DailyIndex;

            if (dailyIndexChange > 0)
            {
                foreach (var matchItem in sameDateMatches.Where(m => !m.Match.Equals(matchToChange) && m.Match.DailyIndex > currentDailyIndex && m.Match.DailyIndex <= newDailyIndex))
                {
                    matchItem.Match.DailyIndex--;
                }
            }
            else if (dailyIndexChange < 0)
            {
                foreach (var matchItem in sameDateMatches.Where(m => !m.Match.Equals(matchToChange) && m.Match.DailyIndex < currentDailyIndex && m.Match.DailyIndex >= newDailyIndex))
                {
                    matchItem.Match.DailyIndex++;
                }
            }

            matchToChange.DailyIndex += dailyIndexChange;

            this.ReenterMatches(matchesToReenter.Select(matchItem => new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));
        }

        public void DecreaseDailyIndex(Match match)
        {
            this.ChangeDailyIndex(match, -1);
        }

        public void IncreaseDailyIndex(Match match)
        {
            this.ChangeDailyIndex(match, 1);
        }

        public void ReplaceMatch(Match matchToReplace, DateTime newDate, SCPlayer player1, SCPlayer player2, GameEntry[] entries, Season season)
        {
            if (!this.GetMatches().Contains(matchToReplace)) { return; }

            // locate index of last match to roll back
            int indexOfMatchesToRollBack;

            const int MATCH_INSTANCE_TO_BE_REMOVED = 1;

            if (matchToReplace.DateTime.Date.CompareTo(newDate.Date) > 0)
            {
                indexOfMatchesToRollBack = this.GetMatches().OrderByNewestFirst().TakeWhile(item => item.DateTime.CompareTo(newDate.Date) > 0).Count() - MATCH_INSTANCE_TO_BE_REMOVED;
            }
            else { indexOfMatchesToRollBack = this.GetMatches().OrderByNewestFirst().IndexOf(matchToReplace); }


            var matchesToReenter = this.RollBackLastMatches(indexOfMatchesToRollBack + MATCH_INSTANCE_TO_BE_REMOVED).DistinctBy(m => m.Match).Select(game => new
            {
                Match = game.Match,
                Season = game.Season,
                Tournament = game.Tournament
            }).Where(matchItem => !matchItem.Match.Equals(matchToReplace)).ToList(); // we need to call ToList() here becauase the returned items have been removed from the database


            if (matchToReplace.DateTime.Date.CompareTo(newDate.Date) >= 0)
            {
                this.ReportMatch(new Match(player1, player2, entries, newDate), season, matchesToReenter.IsEmpty());

                this.ReenterMatches(matchesToReenter.Select(matchItem => new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));
            }
            else
            {
                this.ReenterMatches(matchesToReenter.Where(matchItem => matchItem.Match.DateTime.Date.CompareTo(newDate.Date) <= 0).Select(matchItem =>
                    new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));

                this.ReportMatch(new Match(player1, player2, entries, newDate), season, matchesToReenter.IsEmpty());

                this.ReenterMatches(matchesToReenter.Where(matchItem => matchItem.Match.DateTime.Date.CompareTo(newDate.Date) > 0).Select(matchItem =>
                    new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));
            }

        }

        public void ReplaceMatch(Match matchToReplace, DateTime date, SCPlayer player1, SCPlayer player2, GameEntry[] entries, Tournament tournament)
        {
            this.ReplaceMatch(matchToReplace, date, player1, player2, entries, tournament == null ? this.defaultTournament.DefaultSeason : tournament.DefaultSeason);
        }

        public void RollBackMatch(Match matchToRemove)
        {
            if (!this.GetMatches().Contains(matchToRemove)) { throw new ArgumentException("matchToRemove was not found"); }

            int index = this.GetMatches().OrderByNewestFirst().IndexOf(matchToRemove);

            var matchesToReenter = this.RollBackLastMatches(index + 1).DistinctBy(m => m.Match).Select(game => new
            {
                Match = game.Match,
                Season = game.Season,
                Tournament = game.Tournament
            }).Take(index).ToList();

            this.ReenterMatches(matchesToReenter.Select(matchItem => new Tuple<Match, Season, Tournament>(matchItem.Match, matchItem.Season, matchItem.Tournament)));
        }

        private void ReenterMatches(IEnumerable<Tuple<Match, Season, Tournament>> matchesToReenter)
        {
            bool invokeEvent = false;

            foreach (Tuple<Match, Season, Tournament> matchData in matchesToReenter.OrderBy(t => t.Item1.DateTime.Date).ThenBy(t => t.Item1.DailyIndex))
            {
                invokeEvent = true;

                if (matchData.Item2 != null) { this.ReportMatch(new Match(matchData.Item1.Player1, matchData.Item1.Player2, matchData.Item1.GetEntries(), matchData.Item1.DateTime), matchData.Item2, false); }
                else { this.ReportMatch(new Match(matchData.Item1.Player1, matchData.Item1.Player2, matchData.Item1.GetEntries(), matchData.Item1.DateTime), matchData.Item3, false); }
            }

            if (invokeEvent) { this.MatchPoolChanged.Invoke(this, new EventArgs()); }
        }

        private void ReportMatch(Match match, Season season, bool evokeMatchPoolChangedHandler)
        {
            match.DailyIndex = this.GetMatches().Count(m => m.DateTime.Date.Equals(match.DateTime.Date));

            EloData.UpdateMapStats(match);
            EloData.UpdatePlayerStats(match);

            if (season != null) { season.AddMatch(match); }
            else { this.defaultTournament.DefaultSeason.AddMatch(match); }

            this.ContentHasBeenChanged = true;

            if (evokeMatchPoolChangedHandler) { this.MatchPoolChanged.Invoke(this, new EventArgs()); }

        }

        private void ReportMatch(Match match, Tournament tournament, bool evokeMatchPoolChangedHandler)
        {
            this.ReportMatch(match, tournament.DefaultSeason, evokeMatchPoolChangedHandler);
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

            this.ContentHasBeenChanged = false;

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

        internal IEnumerable<GameEntry> GetAllGameEntries()
        {
            foreach (GameEntry entry in this.tournaments.SelectMany(tournament => tournament.GetGameEntries().ToList())) { yield return entry; }
        }

        public IEnumerable<Game> GetAllGames()
        {
            const int DEFAULT_TOURNAMENT = 1;

            foreach (Game game in this.defaultTournament.GetGames())
            {
                game.Tournament = null;

                yield return game;
            }

            foreach (Game game in this.tournaments.Skip(DEFAULT_TOURNAMENT).SelectMany(tournament => tournament.GetGames())) { yield return game; }
        }

        public IEnumerable<Map> GetMaps()
        {
            foreach (Map map in this.maps.ToList()) { yield return map; }
        }

        public IEnumerable<SCPlayer> GetPlayers()
        {
            foreach (SCPlayer player in this.players.ToList(this.players.Count)) { yield return player; }
        }

        public IEnumerable<SCPlayer> SearchPlayers(Regex pattern)
        {
            return this.SearchPlayers(new Regex[] { pattern });
        }

        public IEnumerable<SCPlayer> SearchPlayers(IEnumerable<Regex> patterns)
        {
            int playerCount = this.players.Count;

            var stringMatches = new KeyValuePair<SCPlayer, bool>[playerCount];

            Parallel.For(0, playerCount, index =>
            {
                SCPlayer player = this.players[index];

                stringMatches[index] = new KeyValuePair<SCPlayer, bool>(player, patterns.Any(pattern => player.NamesMatches(pattern)));
            });

            return stringMatches.Where(match => match.Value == true).Select(match => match.Key);
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

        public IEnumerable<SCPlayer> GetPlayers(string name)
        {
            return this.players.Where(player => player.Name == name);
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

        /// <summary>
        /// Rolls back Elo changes from most recent matches.
        /// </summary>
        /// <param name="count"></param>
        /// <returns>A sequence containing the games of the removed matches.</returns>
        public IEnumerable<Game> RollBackLastMatches(int count)
        {
            var matches = this.GetAllGames().DistinctBy(m => m.Match).OrderNewestFirst().Select(game => new
            {
                Match = game.Match,
                Season = game.Season,
                Tournament = game.Tournament,
                Games = game.Match.GetGames()
            }).Take(count).ToList();

            foreach (var matchObj in matches)
            {
                foreach (Game game in matchObj.Games)
                {
                    // roll back win rates
                    game.Player1.Stats.RollBackResult(game);
                    game.Player2.Stats.RollBackResult(game);

                    // roll back elo ratings
                    game.Player1.RatingVs.AddValueTo(game.Player2Race, game.Winner == game.Player1 ? -game.RatingChange : game.RatingChange);

                    game.Player2.RatingVs.AddValueTo(game.Player1Race, game.Winner == game.Player2 ? -game.RatingChange : game.RatingChange);
                }

                // remove from season
                if (matchObj.Season != null) { matchObj.Season.RemoveMatch(matchObj.Match); }
                else if (matchObj.Tournament != null) { matchObj.Tournament.DefaultSeason.RemoveMatch(matchObj.Match); }
                else { this.defaultTournament.DefaultSeason.RemoveMatch(matchObj.Match); }


                // roll back map stats
                foreach (Game game in matchObj.Match.GetGames().Where(gm => gm.Map != null))
                {
                    game.Map.Stats.RollBackGameResult(game.Player1Race, game.Player2Race, game.WinnersRace, game.Player1.RatingVs.GetValueFor(game.Player2Race)
                        , game.Player2.RatingVs.GetValueFor(game.Player1Race));
                }
            }

            this.ContentHasBeenChanged = true;

            this.MatchPoolChanged.Invoke(this, new EventArgs());

            return matches.SelectMany(matchItem => matchItem.Games.Select(game =>
            {
                game.Tournament = matchItem.Tournament;
                game.Season = matchItem.Season;
                return game;
            }));
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // this code ensures backward compatibility
            IEnumerable<IGrouping<DateTime, Match>> matchByDates = this.GetMatches().GroupBy(match => match.DateTime.Date);

            if (matchByDates.Select(grp => grp.Count(item => item.DailyIndex == 0)).Where(dailyIndexesEqualsZero => dailyIndexesEqualsZero > 0).Any())
            {
                IEnumerator<IGrouping<DateTime, Match>> eMatchesByDates = matchByDates.GetEnumerator();

                while (eMatchesByDates.MoveNext())
                {
                    int indexCounter = 0;

                    foreach (Match match in eMatchesByDates.Current.OrderBy(m => m.DailyIndex).ThenBy(m => m.DateTime)) { match.DailyIndex = indexCounter++; }
                }
            }
        }

        public WinRateCounter MapStatsForPlayer(SCPlayer player, Map map, SCPlayer opponent = null, Tournament tournament = null)
        {
            var playersMapStats = new WinRateCounter();

            IEnumerable<Match> tournamentFiltereMatches = tournament == null ? this.GetMatches() : tournament.GetMatches();

            IEnumerable<Match> opponentFiltereMatchs = opponent == null ? tournamentFiltereMatches : tournamentFiltereMatches.Where(m => m.HasPlayer(opponent));

            foreach (Match match in opponentFiltereMatchs.Where(m => m.HasPlayer(player)))
            {
                foreach (GameEntry entry in match.GetEntries().Where(e => e.Map == map))
                {
                    if (match.Player1.Equals(player))
                    {
                        switch (entry.WinnerWas)
                        {
                            case PlayerSlotType.Player1: playersMapStats.ReportWin(entry.WinnersRace, entry.LosersRace); break;
                            case PlayerSlotType.Player2: playersMapStats.ReportLoss(entry.LosersRace, entry.WinnersRace); break;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, entry.WinnerWas.ToString()));
                        }
                    }
                    else
                    {
                        switch (entry.WinnerWas)
                        {
                            case PlayerSlotType.Player1: playersMapStats.ReportLoss(entry.LosersRace, entry.WinnersRace); break;
                            case PlayerSlotType.Player2: playersMapStats.ReportWin(entry.WinnersRace, entry.LosersRace); break;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, entry.WinnerWas.ToString()));
                        }
                    }

                }
            }

            return playersMapStats;
        }
    }
}
