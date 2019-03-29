using System.Collections.Generic;
using System.Linq;

namespace EloSystem
{
    public static class EloExtensionMethods
    {
        public static IEnumerable<Match> OrderByDescendingEntry(this IEnumerable<Match> matches)
        {
            return matches.OrderByDescending(m => m.Date).ThenByDescending(m => m.DailyIndex);
        }

        public static bool HasOverlappingPlayersAny(this Match match, Match comparison)
        {
            return match.Player1.Equals(comparison.Player1) || match.Player1.Equals(comparison.Player2) || match.Player2.Equals(comparison.Player1) || match.Player2.Equals(comparison.Player2);
        }

        public static bool HasOverlappingPlayersAll(this Match match, Match comparison)
        {
            return (match.Player1.Equals(comparison.Player1) && match.Player2.Equals(comparison.Player2)) || (match.Player1.Equals(comparison.Player2) && match.Player2.Equals(comparison.Player1));
        }
    }
}
