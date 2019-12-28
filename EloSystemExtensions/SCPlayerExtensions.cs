using System;
using CustomExtensionMethods;
using EloSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EloSystemExtensions
{
    public static class SCPlayerExtensions
    {
        public static bool PlaysMultipleRaces(this SCPlayer player)
        {
            return player.RaceUsageFrequency().Where(kvp => kvp.Value > 0).Count() > 1;
        }

        /// <summary>
        /// Returns a KeyValuePair sequence with the mathucp race usage frequency of a player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Race, int>> RaceUsageFrequency(this SCPlayer player)
        {
            int[] racesPlayed = new int[Enum.GetValues(typeof(Race)).Length];

            foreach (Race thisRace in Enum.GetValues(typeof(Race)).Cast<Race>()) { racesPlayed[(int)player.GetPrimaryRaceVs(thisRace)]++; }

            return racesPlayed.Select((frequency, race) => new KeyValuePair<Race, int>((Race)race, frequency));
        }

    }
}