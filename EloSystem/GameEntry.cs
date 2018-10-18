using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class GameEntry : ISerializable
    {
        internal Map Map { get; private set; }
        public int RatingChange { get; internal set; }
        public Race Player1Race { get; private set; }
        public Race Player2Race { get; private set; }
        public SCPlayer Winner { get; private set; }
        public string MapName
        {
            get
            {
                return this.Map == null ? "" : this.Map.Name;
            }
        }

        public GameEntry(SCPlayer winner, Race player1Race, Race player2Race, Map map = null)
        {
            this.Map = map;
            this.Player1Race = player1Race;
            this.Player2Race = player2Race;
            this.RatingChange = 0;
            this.Winner = winner;
        }

        #region Implementing ISerializable
        private enum Field
        {
            Map, Player1Race, Player2Race, RatingChange, Winner
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Map.ToString(), (Map)this.Map);
            info.AddValue(Field.Player1Race.ToString(), (byte)this.Player1Race);
            info.AddValue(Field.Player2Race.ToString(), (byte)this.Player2Race);
            info.AddValue(Field.RatingChange.ToString(), (byte)this.RatingChange);
            info.AddValue(Field.Winner.ToString(), (SCPlayer)this.Winner);
        }
        internal GameEntry(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Map: this.Map = (Map)info.GetValue(field.ToString(), typeof(Map)); break;
                        case Field.Player1Race: this.Player1Race = (Race)info.GetByte(field.ToString()); break;
                        case Field.Player2Race: this.Player2Race = (Race)info.GetByte(field.ToString()); break;
                        case Field.RatingChange: this.RatingChange = (int)info.GetByte(field.ToString()); break;
                        case Field.Winner: this.Winner = (SCPlayer)info.GetValue(field.ToString(), typeof(SCPlayer)); break;
                    }
                }

            }
        }
        #endregion
    }
}
