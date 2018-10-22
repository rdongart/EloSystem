using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Match : ISerializable
    {
        private List<GameEntry> games;
        public SCPlayer Player1 { get; private set; }
        public SCPlayer Player2 { get; private set; }

        internal Match(SCPlayer player1, SCPlayer player2, IEnumerable<GameEntry> games)
        {
            this.Player1 = player1;
            this.Player2 = player2;

            this.games = games.ToList();

            foreach (GameEntry entry in this.games) { entry.RatingChange = EloData.RatingChange(new Game(player1, player2, entry)); }
        }

        #region Implementing ISerializable
        private enum Field
        {
            Games, Player1, Player2
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Games.ToString(), (List<GameEntry>)this.games);
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
                        case Field.Games:
                            this.games = (List<GameEntry>)info.GetValue(field.ToString(), typeof(List<GameEntry>)); break;
                        case Field.Player1:
                            this.Player1 = (SCPlayer)info.GetValue(field.ToString(), typeof(SCPlayer)); break;
                        case Field.Player2:
                            this.Player2 = (SCPlayer)info.GetValue(field.ToString(), typeof(SCPlayer)); break;
                    }
                }

            }
        }
        #endregion
        
        internal IEnumerable<GameEntry> GetEntries()
        {
            foreach (GameEntry entry in this.games.ToList()) { yield return entry; }
        }

        private Game ConvertToGame(GameEntry entry)
        {
            return new Game(this.Player1, this.Player2, entry);
        }

        public IEnumerable<Game> GetGames()
        {
            return this.games.Select(entry => this.ConvertToGame(entry));
        }
    }
}
