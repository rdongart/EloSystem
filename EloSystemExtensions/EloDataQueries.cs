using System.Windows.Forms;
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

            string[] inputArray = Enumerable.Range(0, Math.Max(0, searchInput.Length)).Select(index => Regex.Escape(searchInput.Substring(index, 1))).ToArray();

            try
            {
                Func<IEnumerable<SCPlayer>> performLookup = () =>
                {
                    if (searchInput == "") { return ed.GetPlayers(); }
                    else if (searchInput.Length == 1) { return ed.SearchPlayers(new Regex(CASEINSENSITIVE_PATTERN + "^" + Regex.Escape(searchInput))); }
                    else if (searchInput.Length == 2)
                    {
                        return ed.SearchPlayers(new Regex(CASEINSENSITIVE_PATTERN
                            // match either the first letter and any other letter for same sized strings
                            + "(" + "^" + inputArray[0] + ANYCHAR_PATTERN + "{0,1}" + "$"
                            // or
                            + "|"
                            // first part of string matches completely
                            + String.Join("", inputArray) + ")"));
                    }
                    else
                    {
                        return ed.SearchPlayers(Enumerable.Range(0, searchInput.Length).Select(counter =>
                        {


                            return new Regex(CASEINSENSITIVE_PATTERN + String.Join("", Enumerable.Range(0, searchInput.Length).Select(index =>
                            {
                                if (index == counter) { return ANYCHAR_PATTERN; }
                                else { return inputArray[index]; }
                            })));
                        }));
                    }
                };

                return performLookup().OrderBy(player => player.IdentifierDistance(searchInput));
            }
            catch (Exception exc)
            {
                MessageBox.Show(String.Format("{0}\n\n{1}\n\n{2}", exc.Message, exc.InnerException.Message, exc.StackTrace), "Unhandled error!");

                return new SCPlayer[] { };
            }
        }

        public static IEnumerable<Game> GamesByPlayer(this EloData ed, SCPlayer player)
        {
            if (player == null) { throw new ArgumentNullException("player"); }

            return ed.GetAllGames().Where(game => game.HasPlayer(player));
        }

        public static IEnumerable<Game> GamesOnMap(this EloData ed, Map map)
        {
            if (map == null) { throw new ArgumentNullException("map"); }

            return ed.GetAllGames().Where(game => game.Map != null && game.Map.Equals(map));
        }

        public static IEnumerable<Game> HeadToHeadGames(this EloData ed, SCPlayer player1, SCPlayer player2)
        {
            if (player1 == null) { throw new ArgumentNullException("player1"); }
            if (player2 == null) { throw new ArgumentNullException("player2"); }

            return ed.GetAllGames().Where(game => (game.Player1.Equals(player1) && game.Player2.Equals(player2)) || (game.Player1.Equals(player2) && game.Player2.Equals(player1)));
        }

    }
}
