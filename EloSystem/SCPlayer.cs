using CustomExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class SCPlayer : EloSystemContent, ISerializable
    {
        private const int MINIMUM_GAMES_THRESHOLD = 10; // the minimum number of games for a particular rating value to be given full influence on the total rating

        private List<string> aliases;
        public Country Country { get; set; }
        public GameCount GamesAsProtossVs { get; private set; }
        public GameCount GamesAsRandomVs { get; private set; }
        public GameCount GamesAsTerranVs { get; private set; }
        public GameCount GamesAsZergVs { get; private set; }
        public int GamesVsProtoss
        {
            get
            {
                return this.GamesAsProtossVs.Protoss + this.GamesAsRandomVs.Protoss + this.GamesAsTerranVs.Protoss + this.GamesAsZergVs.Protoss;
            }
        }
        public int GamesVsRandom
        {
            get
            {
                return this.GamesAsProtossVs.Random + this.GamesAsRandomVs.Random + this.GamesAsTerranVs.Random + this.GamesAsZergVs.Random;
            }
        }
        public int GamesVsTerran
        {
            get
            {
                return this.GamesAsProtossVs.Terran + this.GamesAsRandomVs.Terran + this.GamesAsTerranVs.Terran + this.GamesAsZergVs.Terran;
            }
        }
        public int GamesVsZerg
        {
            get
            {
                return this.GamesAsProtossVs.Zerg + this.GamesAsRandomVs.Zerg + this.GamesAsTerranVs.Zerg + this.GamesAsZergVs.Zerg;
            }
        }
        public ResultVariables RatingsVs { get; private set; }
        public string TeamName
        {
            get
            {
                return this.Team == null ? "" : this.Team.Name;
            }
        }
        public string Nationality
        {
            get
            {
                return this.Country == null ? "" : this.Country.Name;
            }
        }
        public Team Team { get; set; }

        internal SCPlayer(string name, int startRating, int imageID, Team team = null, Country nationality = null)
            : this(name, startRating, imageID, new string[] { }, team, nationality)
        {

        }
        internal SCPlayer(string name, int startRating, int imageID, IEnumerable<string> aliases = null, Team team = null, Country nationality = null) : base(name, imageID)
        {
            this.aliases = aliases == null ? new List<string>() : aliases.ToList();
            this.Country = nationality;
            this.GamesAsProtossVs = new GameCount();
            this.GamesAsRandomVs = new GameCount();
            this.GamesAsTerranVs = new GameCount();
            this.GamesAsZergVs = new GameCount();
            this.RatingsVs = new ResultVariables(startRating);
            this.Team = team;
        }

        private static double GetRatingInfluence(int numberOfGames)
        {
            return Math.Min(1, numberOfGames / (double)SCPlayer.MINIMUM_GAMES_THRESHOLD);
        }

        #region Implementing ISerializable
        private enum Field
        {
            Aliases, GamesAsProtossVs, GamesAsRandomVs, GamesAsTerranVs, GamesAsZergVs, Name, Country, Ratings, Team
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Aliases.ToString(), (List<string>)this.aliases);

            if (this.Country != null) { info.AddValue(Field.Team.ToString(), (Country)this.Country); }

            info.AddValue(Field.GamesAsProtossVs.ToString(), (GameCount)this.GamesAsProtossVs);
            info.AddValue(Field.GamesAsRandomVs.ToString(), (GameCount)this.GamesAsRandomVs);
            info.AddValue(Field.GamesAsTerranVs.ToString(), (GameCount)this.GamesAsTerranVs);
            info.AddValue(Field.GamesAsZergVs.ToString(), (GameCount)this.GamesAsZergVs);
            info.AddValue(Field.Ratings.ToString(), (ResultVariables)this.RatingsVs);

            if (this.Team != null) { info.AddValue(Field.Team.ToString(), (Team)this.Team); }
        }
        internal SCPlayer(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Aliases: this.aliases = (List<string>)info.GetValue(field.ToString(), typeof(List<string>)); break;
                        case Field.Country: this.Country = (Country)info.GetValue(field.ToString(), typeof(Country)); break;
                        case Field.GamesAsProtossVs: this.GamesAsProtossVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.GamesAsRandomVs: this.GamesAsRandomVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.GamesAsTerranVs: this.GamesAsTerranVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.GamesAsZergVs: this.GamesAsZergVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.Ratings: this.RatingsVs = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
                        case Field.Team: this.Team = (Team)info.GetValue(field.ToString(), typeof(Team)); break;
                    }
                }

            }
        }
        #endregion

        private bool HasPlayedAnyGames()
        {
            return this.GamesAsProtossVs.Total() > 0 || this.GamesAsRandomVs.Total() > 0 || this.GamesAsTerranVs.Total() > 0 || this.GamesAsZergVs.Total() > 0;
        }

        private double GetTotalInfluenceValue()
        {
            return SCPlayer.GetRatingInfluence(this.GamesVsProtoss) + SCPlayer.GetRatingInfluence(this.GamesVsRandom) + SCPlayer.GetRatingInfluence(this.GamesVsTerran)
                + SCPlayer.GetRatingInfluence(this.GamesVsZerg);
        }

        public GameCount GameCountAs(Race race)
        {
            switch (race)
            {
                case Race.Zerg: return this.GamesAsZergVs;
                case Race.Terran: return this.GamesAsTerranVs;
                case Race.Protoss: return this.GamesAsProtossVs;
                case Race.Random: return this.GamesAsRandomVs;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }

        public int GameCountVs(Race race)
        {
            switch (race)
            {
                case Race.Zerg: return this.GamesVsZerg;
                case Race.Terran: return this.GamesVsTerran;
                case Race.Protoss: return this.GamesVsProtoss;
                case Race.Random: return this.GamesVsRandom;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }

        public IEnumerable<string> GetAliases()
        {
            foreach (string name in this.aliases.ToList()) { yield return name; }
        }

        /// <summary>
        /// Returns an integer representing the total rating
        /// </summary>
        /// <returns></returns>
        public int RatingTotal()
        {
            // if any games have been played
            return this.HasPlayedAnyGames() ?

                // ...calculate the rating
                ((this.RatingsVs.Protoss * SCPlayer.GetRatingInfluence(this.GamesVsProtoss)
                + this.RatingsVs.Random * SCPlayer.GetRatingInfluence(this.GamesVsRandom)
                + this.RatingsVs.Terran * SCPlayer.GetRatingInfluence(this.GamesVsTerran)
                + this.RatingsVs.Zerg * SCPlayer.GetRatingInfluence(this.GamesVsZerg))
                / this.GetTotalInfluenceValue()).RoundToInt()

                // ...otherwise just return the default starting rating value
                : this.RatingsVs.Protoss;

        }

        public bool RemoveAlias(string alias)
        {
            return this.aliases.Remove(alias);
        }

        public Race GetPrimaryRace()
        {
            // make groups of all the opponent races where the player plays the same race and order so the most played race grouping is on top
            var groupingBySubPrimary = Enum.GetValues(typeof(Race)).Cast<Race>().Select(race => new { SubPrimary = this.GetPrimaryRaceVs(race) }).GroupBy(item =>
                item.SubPrimary).OrderByDescending(grouping => grouping.Count()).ToList();

            // check if the first place is different from the second place
            if (groupingBySubPrimary[0].Count() > groupingBySubPrimary[1].Count()) { return groupingBySubPrimary[0].Key; }
            else // calculate the primary race from most played games among the ones that have equal number of sub primary races (= being primary against a specific opponent race)
            {

                int maxSubPrimaryCount = groupingBySubPrimary[0].Count();


                return groupingBySubPrimary.Where(grouping => grouping.Count() == maxSubPrimaryCount).ToDictionary(grouping => grouping.Key, grouping =>
                    this.GameCountAs(grouping.Key).Total()).OrderByDescending(kvp => kvp.Value).First().Key;
            }
        }

        public Race GetPrimaryRaceVs(Race race)
        {
            return (new Dictionary<Race, int>() {
                { Race.Protoss, this.GamesAsProtossVs.Vs(race) }
                , { Race.Random, this.GamesAsRandomVs.Vs(race) }
                , { Race.Terran, this.GamesAsTerranVs.Vs(race) }
                , { Race.Zerg, this.GamesAsZergVs.Vs(race) } }).OrderByDescending(kvp => kvp.Value).First().Key;
        }

        public void AddAlias(string alias)
        {
            this.aliases.Add(alias);
        }
    }
}
