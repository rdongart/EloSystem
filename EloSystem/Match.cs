using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Match : ISerializable
    {
        internal const int DAILYINDEX_MOSTRECENT = -1;

        private List<GameEntry> games;
        public DateTime Date { get; private set; }
        /// <summary>
        /// Zero based index value based on the order of which this match was reported on a particular date.
        /// </summary>
        public int DailyIndex { get; internal set; }
        public SCPlayer Player1 { get; private set; }
        public SCPlayer Player2 { get; private set; }

        internal Match(SCPlayer player1, SCPlayer player2, IEnumerable<GameEntry> games, DateTime date)
        {
            this.Date = date;

            this.Player1 = player1;
            this.Player2 = player2;

            this.games = games.ToList();

            foreach (GameEntry entry in this.games)
            {
                SCPlayer winner = entry.WinnerWas == PlayerSlotType.Player1 ? player1 : player2;
                SCPlayer loser = entry.WinnerWas == PlayerSlotType.Player1 ? player2 : player1;

                entry.RatingChange = EloData.RatingChange(winner.Stats, entry.WinnersRace, loser.Stats, entry.LosersRace, EloData.ExpectedWinRatio(winner.RatingVs.GetValueFor(entry.LosersRace)
                    , loser.RatingVs.GetValueFor(entry.WinnersRace)));
            }

        }

        #region Implementing ISerializable
        private enum Field
        {
            DailyIndex, Date, Games, Player1, Player2
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.DailyIndex.ToString(), (int)this.DailyIndex);
            info.AddValue(Field.Games.ToString(), (List<GameEntry>)this.games);
            info.AddValue(Field.Date.ToString(), (DateTime)this.Date);
            info.AddValue(Field.Player1.ToString(), (SCPlayer)this.Player1);
            info.AddValue(Field.Player2.ToString(), (SCPlayer)this.Player2);
        }
        internal Match(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.DailyIndex: this.DailyIndex = (int)info.GetInt32(field.ToString()); break;
                        case Field.Date: this.Date = (DateTime)info.GetValue(field.ToString(), typeof(DateTime)); break;
                        case Field.Games: this.games = (List<GameEntry>)info.GetValue(field.ToString(), typeof(List<GameEntry>)); break;
                        case Field.Player1: this.Player1 = (SCPlayer)info.GetValue(field.ToString(), typeof(SCPlayer)); break;
                        case Field.Player2: this.Player2 = (SCPlayer)info.GetValue(field.ToString(), typeof(SCPlayer)); break;
                    }
                }

            }
        }
        #endregion

        internal IEnumerable<GameEntry> GetEntries()
        {
            foreach (GameEntry entry in this.games.ToList()) { yield return entry; }
        }

        internal IEnumerable<Game> GetGames()
        {
            foreach (Game game in this.games.Select(entry => new Game(entry, this))) { yield return game; }
        }

        public int RatingChangeBy(PlayerSlotType player)
        {
            return this.games.Where(game => game.WinnerWas == player).Sum(game => game.RatingChange) - this.games.Where(game => game.WinnerWas != player).Sum(game => game.RatingChange);
        }

        public int WinsBy(PlayerSlotType player)
        {
            return this.games.Count(game => game.WinnerWas == player);
        }

        public bool HasPlayer(SCPlayer player)
        {
            return this.Player1 == player || this.Player2 == player;
        }

        internal bool IsMoreRecentThan(DateTime date, int dailyMatchIndex)
        {
            return this.Date.CompareTo(date) > 0 || (this.Date.CompareTo(date) == 0 && (this.DailyIndex <= dailyMatchIndex && dailyMatchIndex != Match.DAILYINDEX_MOSTRECENT));
        }
    }
}
