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
        private const int STARTRATING_DEFAULT = 1500;

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
        private List<Match> matches;
        private List<SCPlayer> players;
        private List<Team> teams;
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
        public string Name { get; private set; }

        public EloData(string name)
        {
            this.Name = name;

            this.countries = new List<Country>();
            this.maps = new List<Map>();
            this.matches = new List<Match>();
            this.players = new List<SCPlayer>();
            this.teams = new List<Team>();
            this.DataWasChanged = true;
            this.resHandler = new ResourceHandler(StaticMembers.SaveDirectory + name);
        }

        /// <summary>
        /// Returns a factor that increases the rating value changes based on the rating calibration phase that at least one player in a game is in.
        /// </summary>
        /// <param name="game"></param>
        private static int GetRatingBonusFactor(Game game)
        {
            const int CALIBRATION_PHASE1_BONUSFACTOR = 3;
            const int CALIBRATION_PHASE1_NO_MATCHES = 10;
            const int CALIBRATION_PHASE2_BONUSFACTOR = 2;
            const int CALIBRATION_PHASE2_NO_MATCHES = 50;
            const int BONUSFACTOR_STANDARD = 1;


            if (game.Player1.games.GetValueVs(game.Player2Race) < CALIBRATION_PHASE1_NO_MATCHES
                || game.Player2.games.GetValueVs(game.Player1Race) < CALIBRATION_PHASE1_NO_MATCHES)
            {
                return CALIBRATION_PHASE1_BONUSFACTOR;
            }
            else if (game.Player1.games.GetValueVs(game.Player2Race) < CALIBRATION_PHASE2_NO_MATCHES
             || game.Player2.games.GetValueVs(game.Player1Race) < CALIBRATION_PHASE2_NO_MATCHES)
            {
                return CALIBRATION_PHASE2_BONUSFACTOR;
            }
            else { return BONUSFACTOR_STANDARD; }

        }
        private static void UpdateRating(Match match)
        {
            const double MAX_RATING_DISTANCE_DETERMINER = 0.0005;
            const double EXP_WIN_RATIO_EVEN_RATING = 0.5;
            const double EXP_WIN_RATIO_MAX = 1.0;
            const double EXP_WIN_RATIO_MIN = 0.0;
            const int RATING_CHANGE_STANDARD_MAX = 28;

            var player1RatingGain = new ResultVariables(0);
            var player2RatingGain = new ResultVariables(0);

            foreach (GameEntry game in match.GetEntries())
            {
                double player1ExpectedWinRatio =
                    ((match.Player1.Ratings.GetValueVs(game.Player2Race) - match.Player2.Ratings.GetValueVs(game.Player1Race))
                    * MAX_RATING_DISTANCE_DETERMINER + EXP_WIN_RATIO_EVEN_RATING).TruncateToRange(EXP_WIN_RATIO_MIN, EXP_WIN_RATIO_MAX);

                double player2ExpectedWinRatio = EXP_WIN_RATIO_MAX - player1ExpectedWinRatio;

                int ratingPoints = ((RATING_CHANGE_STANDARD_MAX * EXP_WIN_RATIO_EVEN_RATING)
                    * (EXP_WIN_RATIO_MAX - player1ExpectedWinRatio)
                    * EloData.GetRatingBonusFactor(new Game(match.Player1, match.Player2, game))).RoundToInt();

                player1RatingGain.AddValueVs(game.Player2Race, ratingPoints * (game.Winner.Equals(match.Player1) ? 1 : -1));


                player2RatingGain.AddValueVs(game.Player1Race, ratingPoints * (game.Winner.Equals(match.Player2) ? 1 : -1));
            }

            foreach (Race race in Enum.GetValues(typeof(Race)).Cast<Race>())
            {
                match.Player1.Ratings.AddValueVs(race, player1RatingGain.GetValueVs(race));
                match.Player1.games.AddValueVs(race, match.GetEntries().Count(entry => entry.Player2Race == race));

                match.Player2.Ratings.AddValueVs(race, player2RatingGain.GetValueVs(race));
                match.Player2.games.AddValueVs(race, match.GetEntries().Count(entry => entry.Player1Race == race));
            }

        }

        #region Implementing ISerializable
        private enum Field
        {
            Countries, Maps, Matches, Name, ImagesSaved, Players, Teams
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Countries.ToString(), (List<Country>)this.countries);
            info.AddValue(Field.Maps.ToString(), (List<Map>)this.maps);
            info.AddValue(Field.Matches.ToString(), (List<Match>)this.matches);
            info.AddValue(Field.Name.ToString(), (string)this.Name);
            info.AddValue(Field.ImagesSaved.ToString(), (int)this.imagesSaved);
            info.AddValue(Field.Players.ToString(), (List<SCPlayer>)this.players);
            info.AddValue(Field.Teams.ToString(), (List<Team>)this.teams);
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
                        case Field.Matches: this.matches = (List<Match>)info.GetValue(field.ToString(), typeof(List<Match>)); break;
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                        case Field.ImagesSaved: this.imagesSaved = (int)info.GetInt32(field.ToString()); break;
                        case Field.Players: this.players = (List<SCPlayer>)info.GetValue(field.ToString(), typeof(List<SCPlayer>)); break;
                        case Field.Teams: this.teams = (List<Team>)info.GetValue(field.ToString(), typeof(List<Team>)); break;
                    }
                }

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
                return true;
            }
            else { return false; }
        }

        public bool AddMap(string name, Image image = null)
        {
            if (name != string.Empty && !this.maps.Any(map => map.Name == name))
            {
                this.maps.Add(new Map(name, this.AddNewImage(image)));

                this.DataWasChanged = true;
                return true;
            }
            else { return false; }
        }

        public bool AddPlayer(string name, int startRating = EloData.STARTRATING_DEFAULT, Team team = null, Country country = null, Image image = null)
        {
            return this.AddPlayer(name, new string[] { }, team, country);
        }

        public bool AddPlayer(string name, IEnumerable<string> aliases, Team team = null, Country country = null, Image image = null)
        {
            if (name != string.Empty && !this.players.Any(player => player.Name == name))
            {
                this.players.Add(new SCPlayer(name, EloData.STARTRATING_DEFAULT, this.AddNewImage(image), aliases, team, country));
                this.DataWasChanged = true;
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

            this.countries.Remove(country);
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

            this.maps.Remove(map);
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

            this.players.Remove(player);
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

            this.teams.Remove(team);
        }

        public bool ReportMatch(SCPlayer player1, SCPlayer player2, GameEntry[] entries)
        {
            if (entries.All(game => game.Winner.Equals(player1) || game.Winner.Equals(player2)))
            {
                var match = new Match(player1, player2, entries.Select(entry => new GameEntry(entry.Winner
                    , entry.Player1Race, entry.Player2Race, entry.Map)));

                EloData.UpdateRating(match);

                this.matches.Add(match);

                this.DataWasChanged = true;

                return true;
            }
            else { return false; }

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
            
            this.resHandler.SaveResourceChanges();

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

        public IEnumerable<Game> GetGames()
        {
            foreach (Game game in this.matches.SelectMany(match => match.GetGames()).ToList()) { yield return game; }
        }

        public IEnumerable<Map> GetMaps()
        {
            foreach (Map map in this.maps.ToList()) { yield return map; }
        }

        public IEnumerable<Match> GetMatches()
        {
            foreach (Match match in this.matches.ToList()) { yield return match; }
        }

        public IEnumerable<SCPlayer> GetPlayers()
        {
            foreach (SCPlayer player in this.players.ToList()) { yield return player; }
        }

        public IEnumerable<Team> GetTeams()
        {
            foreach (Team team in this.teams.ToList()) { yield return team; }
        }

        public bool TryGetImage(int imageID, out EloImage eloImg)
        {
            Image img = this.resHandler.GetImage(imageID);

            if (img != null)
            {
                eloImg = new EloImage(img);

                return true;
            }
            else
            {
                eloImg = new EloImage();
                return false;
            }

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

    }
}
