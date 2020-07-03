using CustomExtensionMethods;
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

        internal static int IdentifierDistance(this SCPlayer player, string searchInput)
        {
            return (new string[] { player.Name, player.IRLName }).Concat(player.GetAliases()).Select(playerIdentifier => SCPlayerExtensions.LevenshteinDistance(playerIdentifier, searchInput)).Min();
        }

        /// <summary>
        /// Return the difference between two strings.
        /// </summary>
        /// <param name="inputA"></param>
        /// <param name="inputB"></param>
        /// <returns></returns>
        private static int LevenshteinDistance(string inputA, string inputB)
        {
            // the following algorithm calculates the minimum number of single-character edits required to change one word into the other

            var previousDistances = new int[inputB.Length + 1];

            for (int i = 0; i <= inputB.Length; i++) { previousDistances[i] = i; }

            for (int indexA = 0; indexA < inputA.Length; indexA++)
            {
                int currentMinDistance = indexA + 1;

                for (int indexB = 0; indexB < inputB.Length; indexB++)
                {
                    int substitutionCost = inputA[indexA] == inputB[indexB] ? previousDistances[indexB] : previousDistances[indexB] + 1;

                    previousDistances[indexB] = currentMinDistance; // update previous distances so we have the value for the next iteration with indexA

                    currentMinDistance = Math.Min(Math.Min(
                        previousDistances[indexB + 1] + 1, // deletion cost
                        currentMinDistance + 1), // insertion costs    
                        substitutionCost);
                }

                previousDistances[inputB.Length] = currentMinDistance; // update previous distance with the last value
            }

            return previousDistances[inputB.Length];
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