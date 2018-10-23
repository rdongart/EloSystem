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
        public WinRateCounter Stats { get; private set; }
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
        internal SCPlayer(string name, int startRating, int imageID, IEnumerable<string> aliases, Team team = null, Country nationality = null) : base(name, imageID)
        {
            this.aliases = aliases.ToList();
            this.Country = nationality;
            this.Stats = new WinRateCounter();
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
            Aliases, GameStats, Name, Country, Ratings, Team
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Aliases.ToString(), (List<string>)this.aliases);

            if (this.Country != null) { info.AddValue(Field.Country.ToString(), (Country)this.Country); }

            info.AddValue(Field.GameStats.ToString(), (WinRateCounter)this.Stats);
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
                        case Field.GameStats: this.Stats = (WinRateCounter)info.GetValue(field.ToString(), typeof(WinRateCounter)); break;
                        case Field.Ratings: this.RatingsVs = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
                        case Field.Team: this.Team = (Team)info.GetValue(field.ToString(), typeof(Team)); break;
                    }
                }

            }
        }
        #endregion

        private bool HasPlayedAnyGames()
        {
            return this.Stats.GamesTotal() > 0;
        }

        private double GetTotalInfluenceValue()
        {
            return SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Protoss)) + SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Random))
                + SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Terran)) + SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Zerg));
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
                ((this.RatingsVs.Protoss * SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Protoss))
                + this.RatingsVs.Random * SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Random))
                + this.RatingsVs.Terran * SCPlayer.GetRatingInfluence(this.Stats.GamesVs(Race.Terran))
                + this.RatingsVs.Zerg * SCPlayer.GetRatingInfluence(Stats.GamesVs(Race.Zerg)))
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
            if (groupingBySubPrimary.Count > 1 && groupingBySubPrimary[0].Count() > groupingBySubPrimary[1].Count()) { return groupingBySubPrimary[0].Key; }
            else // calculate the primary race from most played games among the ones that have equal number of sub primary races (= being primary against a specific opponent race)
            {

                int maxSubPrimaryCount = groupingBySubPrimary[0].Count();


                return groupingBySubPrimary.Where(grouping => grouping.Count() == maxSubPrimaryCount).ToDictionary(grouping => grouping.Key, grouping =>
                    this.Stats.GamesWith(grouping.Key)).OrderByDescending(kvp => kvp.Value).First().Key;
            }
        }

        public Race GetPrimaryRaceVs(Race race)
        {
            return Enum.GetValues(typeof(Race)).Cast<Race>().ToDictionary(r => r, r => this.Stats.GamesInMathcup(r, race)).OrderByDescending(kvp => kvp.Value).First().Key;
        }

        public void AddAlias(string alias)
        {
            this.aliases.Add(alias);
        }
    }
}
