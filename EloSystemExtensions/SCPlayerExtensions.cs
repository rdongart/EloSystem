using CustomExtensionMethods;
using System.Collections.Generic;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloSystemExtensions
{
    public static class SCPlayerExtensions
    {
        public static bool PlaysMultipleRaces(this SCPlayer player)
        {
            return player.RaceUsageFrequency().Where(kvp => kvp.Value > 0).Count() > 1;
        }
        
        /// <summary>
        /// Returns a KeyValuePair sequence with the mathucp race usage frequency of a player against a particular opponent race.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="vsRace"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Race, int>> RaceUsageFrequency(this SCPlayer player, Race vsRace)
        {
            IEnumerable<Race> playableRaces = Enum.GetValues(typeof(Race)).Cast<Race>();

            int[] racesPlayedVsOpponentRace = new int[Enum.GetValues(typeof(Race)).Length];

            foreach (Race ownRace in playableRaces) { racesPlayedVsOpponentRace[playableRaces.IndexOf(ownRace)] = player.Stats.GamesInMathcup(ownRace, vsRace); }

            return racesPlayedVsOpponentRace.Select((frequency, raceIndex) => new KeyValuePair<Race, int>(playableRaces.ElementAt(raceIndex), frequency));
        }


        /// <summary>
        /// Returns a KeyValuePair sequence with the mathucp primary race usage frequency of a player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Race, int>> RaceUsageFrequency(this SCPlayer player)
        {
            IEnumerable<Race> playableRaces = Enum.GetValues(typeof(Race)).Cast<Race>();

            int[] racesPlayed = new int[Enum.GetValues(typeof(Race)).Length];

            foreach (Race thisRace in playableRaces) { racesPlayed[playableRaces.IndexOf(player.GetPrimaryRaceVs(thisRace))]++; }

            return racesPlayed.Select((frequency, raceIndex) => new KeyValuePair<Race, int>(playableRaces.ElementAt(raceIndex), frequency));
        }

    }
}