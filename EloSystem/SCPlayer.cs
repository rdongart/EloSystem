﻿using CustomExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace EloSystem
{
    [Serializable]
    public class SCPlayer : EloSystemContent, ISerializable
    {
        private const int MINIMUM_GAMES_THRESHOLD = 30; // the minimum number of games for a particular rating value to be given full influence on the total rating
        private const double MINIMUM_GAMES_PERCENTAGE = 0.07;

        private DateTime birthDate;
        private List<string> aliases;
        public Country Country { get; set; }
        public ResultVariables RatingVs { get; private set; }
        public string Nationality
        {
            get
            {
                return this.Country == null ? "" : this.Country.Name;
            }
        }
        public string IRLName { get; set; }
        public string TeamName
        {
            get
            {
                return this.Team == null ? "" : this.Team.Name;
            }
        }
        public Team Team { get; set; }
        public WinRateCounter Stats { get; private set; }

        internal SCPlayer(string name, int startRating, int imageID, int id, Team team = null, Country nationality = null)
            : this(name, startRating, imageID, id, new string[] { }, team, nationality)
        {

        }
        internal SCPlayer(string name, int startRating, int imageID, int id, IEnumerable<string> aliases, Team team = null, Country nationality = null) : base(name, imageID, id)
        {
            this.aliases = aliases.ToList();
            this.Country = nationality;
            this.Stats = new WinRateCounter();
            this.RatingVs = new ResultVariables(startRating);
            this.Team = team;
            this.birthDate = new DateTime();
        }

        internal static int RatingTotal(ResultVariables ratingVs, WinRateCounter gameStats)
        {
            // if any games have been played
            return gameStats.GamesTotal() > 0 ?

                // ...calculate the rating
                ((ratingVs.Protoss * SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Protoss), gameStats.GamesTotal())
                + ratingVs.Random * SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Random), gameStats.GamesTotal())
                + ratingVs.Terran * SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Terran), gameStats.GamesTotal())
                + ratingVs.Zerg * SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Zerg), gameStats.GamesTotal()))
                / SCPlayer.GetTotalInfluenceValue(gameStats)).RoundToInt()

                // ...otherwise just return the default starting rating value
                : ratingVs.Protoss;
        }

        private static double GetRatingInfluence(int numberOfGames, int totalGames)
        {
            return Math.Min(1, Math.Min((numberOfGames / (double)totalGames) / SCPlayer.MINIMUM_GAMES_PERCENTAGE, numberOfGames / (double)SCPlayer.MINIMUM_GAMES_THRESHOLD));
        }

        private static double GetTotalInfluenceValue(WinRateCounter gameStats)
        {
            return SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Protoss), gameStats.GamesTotal()) + SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Random), gameStats.GamesTotal())
                + SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Terran), gameStats.GamesTotal()) + SCPlayer.GetRatingInfluence(gameStats.GamesVs(Race.Zerg), gameStats.GamesTotal());
        }

        #region Implementing ISerializable
        private enum Field
        {
            Aliases, BirthDate, GameStats, IRLName, Name, Country, Ratings, Team
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Aliases.ToString(), (List<string>)this.aliases);
            info.AddValue(Field.BirthDate.ToString(), (DateTime)this.birthDate);

            if (this.Country != null) { info.AddValue(Field.Country.ToString(), (Country)this.Country); }

            info.AddValue(Field.GameStats.ToString(), (WinRateCounter)this.Stats);
            info.AddValue(Field.IRLName.ToString(), (string)this.IRLName);
            info.AddValue(Field.Ratings.ToString(), (ResultVariables)this.RatingVs);

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
                        case Field.BirthDate: this.birthDate = (DateTime)info.GetValue(field.ToString(), typeof(DateTime)); break;
                        case Field.Country: this.Country = (Country)info.GetValue(field.ToString(), typeof(Country)); break;
                        case Field.GameStats: this.Stats = (WinRateCounter)info.GetValue(field.ToString(), typeof(WinRateCounter)); break;
                        case Field.IRLName: this.IRLName = (string)info.GetString(field.ToString()); break;
                        case Field.Ratings: this.RatingVs = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
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
            return SCPlayer.RatingTotal(this.RatingVs, this.Stats);
        }

        public bool RemoveAlias(string alias)
        {
            return this.aliases.Remove(alias);
        }

        public void SetAliases(IEnumerable<string> aliases)
        {
            this.aliases = aliases.ToList();
        }

        public bool TryGetBirthDate(out DateTime birthDate)
        {
            birthDate = this.birthDate;

            return this.birthDate > DateTime.MinValue;
        }

        public Race GetPrimaryRace()
        {
            if (this.Stats.GamesTotal() <= 0) { return Race.Random; }

            // make groups of all the opponent races where the player plays the same race and order so the most played race grouping is on top
            var groupingBySubPrimary = Enum.GetValues(typeof(Race)).Cast<Race>().Where(race => this.Stats.GamesVs(race) > 0).Select(race => new { SubPrimary = this.GetPrimaryRaceVs(race) }).GroupBy(item =>
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
            IOrderedEnumerable<KeyValuePair<Race, int>> racesPlayed = Enum.GetValues(typeof(Race)).Cast<Race>().ToDictionary(r => r, r => this.Stats.GamesInMathcup(r, race)).OrderByDescending(kvp => kvp.Value);

            return racesPlayed.First().Value > 0 ? racesPlayed.First().Key : this.GetPrimaryRace();
        }

        public void AddAlias(string alias)
        {
            this.aliases.Add(alias);
        }

        public void SetBirthDate(DateTime birthday)
        {
            this.birthDate = birthday;
        }

        public bool NamesMatches(Regex pattern)
        {
            return pattern.IsMatch(this.Name) || this.aliases.Any(alias => pattern.IsMatch(alias)) || pattern.IsMatch(this.IRLName);
        }
    }
}
