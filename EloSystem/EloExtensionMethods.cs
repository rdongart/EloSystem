using System;
using System.Collections.Generic;
using System.Linq;

namespace EloSystem
{
    public static class EloExtensionMethods
    {
        public static IEnumerable<Match> OrderByNewestFirst(this IEnumerable<Match> matches)
        {
            return matches.OrderByDescending(m => m.DateTime.Date).ThenByDescending(m => m.DailyIndex);
        }

        public static bool HasOverlappingPlayersAny(this Match match, Match comparison)
        {
            return match.Player1.Equals(comparison.Player1) || match.Player1.Equals(comparison.Player2) || match.Player2.Equals(comparison.Player1) || match.Player2.Equals(comparison.Player2);
        }

        public static bool HasOverlappingPlayersAll(this Match match, Match comparison)
        {
            return (match.Player1.Equals(comparison.Player1) && match.Player2.Equals(comparison.Player2)) || (match.Player1.Equals(comparison.Player2) && match.Player2.Equals(comparison.Player1));
        }

        public static IEnumerable<Game> OrderNewestFirst(this IEnumerable<Game> games)
        {
            return games.OrderByDescending(game => game.Match.DateTime.Date).ThenByDescending(game => game.Match.DailyIndex).ThenByDescending(game => game.GameIndex);
        }

        public static IEnumerable<Game> OrderOldestFirst(this IEnumerable<Game> games)
        {
            return games.OrderBy(game => game.Match.DateTime.Date).ThenBy(game => game.Match.DailyIndex).ThenBy(game => game.GameIndex);
        }

        /// <summary>
        /// Returns the leftmost race from the two races in the matchup text.
        /// </summary>
        /// <param name="matchup"></param>
        /// <returns></returns>
        public static Race Race1(this Matchup matchup)
        {
            switch (matchup)
            {
                case Matchup.ZvP:
                case Matchup.ZvZ: return Race.Zerg;
                case Matchup.PvT:
                case Matchup.PvP: return Race.Protoss;
                case Matchup.TvZ:
                case Matchup.TvT: return Race.Terran;
                case Matchup.RvZ:
                case Matchup.RvP:
                case Matchup.RvT:
                case Matchup.RvR: return Race.Random;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, matchup.ToString()));
            }
        }

        /// <summary>
        /// Returns the rightmost race from the two races in the matchup text.
        /// </summary>
        /// <param name="matchup"></param>
        /// <returns></returns>
        public static Race Race2(this Matchup matchup)
        {
            switch (matchup)
            {
                case Matchup.ZvP:
                case Matchup.PvP:
                case Matchup.RvP: return Race.Protoss;
                case Matchup.ZvZ:
                case Matchup.TvZ:
                case Matchup.RvZ: return Race.Zerg;
                case Matchup.PvT:
                case Matchup.TvT:
                case Matchup.RvT: return Race.Terran;
                case Matchup.RvR: return Race.Random;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, matchup.ToString()));
            }
        }
    }
}
