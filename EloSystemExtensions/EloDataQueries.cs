using System;
using CustomExtensionMethods;
using EloSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EloSystemExtensions
{
    public static class EloDataQueries
    {
        /// <summary>
        /// Conducts a case insensitive search through players' names, IRL-names, and aliases.
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public static IEnumerable<SCPlayer> PlayerLookup(this EloData ed, string searchInput)
        {
            const string CASEINSENSITIVE_PATTERN = @"(?i)";
            const string ANYCHAR_PATTERN = @".";

            if (searchInput == "") { return ed.GetPlayers(); }
            else if (searchInput.Length == 1) { return ed.SearchPlayers(new Regex(CASEINSENSITIVE_PATTERN + searchInput)); }
            else { return ed.SearchPlayers(Enumerable.Range(0, searchInput.Length - 1).Select(index => new Regex(CASEINSENSITIVE_PATTERN + searchInput.Replace(index, ANYCHAR_PATTERN)))); }
        }

        public static IEnumerable<Game> GamesByPlayer(this EloData ed, SCPlayer player)
        {
            if (player == null) { throw new ArgumentNullException("player"); }

            return ed.GetAllGames().Where(game => game.Player1.Equals(player) || game.Player2.Equals(player));
        }

        public static IEnumerable<Game> HeadToHeadGames(this EloData ed, SCPlayer player1, SCPlayer player2)
        {
            if (player1 == null) { throw new ArgumentNullException("player1"); }
            if (player2 == null) { throw new ArgumentNullException("player2"); }

            return ed.GetAllGames().Where(game => (game.Player1.Equals(player1) && game.Player2.Equals(player2)) || (game.Player1.Equals(player2) && game.Player2.Equals(player1)));
        }

    }
}
