using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    public enum PlayerSlotType
    {
        Player1, Player2
    }

    [Serializable]
    public class GameEntry : ISerializable
    {
        internal Map Map { get; private set; }
        public int RatingChange { get; internal set; }
        public Matchup MatchType
        {
            get
            {
                switch (this.Player1Race)
                {
                    case Race.Zerg:
                        switch (this.Player2Race)
                        {
                            case Race.Zerg: return Matchup.ZvZ;
                            case Race.Terran: return Matchup.TvZ;
                            case Race.Protoss: return Matchup.ZvP;
                            case Race.Random: return Matchup.RvZ;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, this.Player2Race.ToString()));
                        }
                    case Race.Terran:
                        switch (this.Player2Race)
                        {
                            case Race.Zerg: return Matchup.TvZ;
                            case Race.Terran: return Matchup.TvT;
                            case Race.Protoss: return Matchup.PvT;
                            case Race.Random: return Matchup.RvT;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, this.Player2Race.ToString()));
                        }
                    case Race.Protoss:
                        switch (this.Player2Race)
                        {
                            case Race.Zerg: return Matchup.ZvP;
                            case Race.Terran: return Matchup.PvT;
                            case Race.Protoss: return Matchup.PvP;
                            case Race.Random: return Matchup.RvP;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, this.Player2Race.ToString()));
                        }
                    case Race.Random:
                        switch (this.Player2Race)
                        {
                            case Race.Zerg: return Matchup.RvZ;
                            case Race.Terran: return Matchup.RvT;
                            case Race.Protoss: return Matchup.RvP;
                            case Race.Random: return Matchup.RvR;
                            default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, this.Player2Race.ToString()));
                        }
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, this.Player1Race.ToString()));
                }
            }
        }
        public Race LosersRace
        {
            get
            {
                switch (this.WinnerWas)
                {
                    case PlayerSlotType.Player1: return this.Player2Race;
                    case PlayerSlotType.Player2: return this.Player1Race;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, this.WinnerWas.ToString()));
                }
            }
        }
        public Race Player1Race { get; private set; }
        public Race Player2Race { get; private set; }
        public Race WinnersRace
        {
            get
            {
                switch (this.WinnerWas)
                {
                    case PlayerSlotType.Player1: return this.Player1Race;
                    case PlayerSlotType.Player2: return this.Player2Race;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, this.WinnerWas.ToString()));
                }
            }
        }
        public PlayerSlotType WinnerWas { get; private set; }
        public string MapName
        {
            get
            {
                return this.Map == null ? "" : this.Map.Name;
            }
        }

        public GameEntry(PlayerSlotType winner, Race player1Race, Race player2Race, Map map = null)
        {
            this.Map = map;
            this.Player1Race = player1Race;
            this.Player2Race = player2Race;
            this.RatingChange = 0;
            this.WinnerWas = winner;
        }

        #region Implementing ISerializable
        private enum Field
        {
            Map, Player1Race, Player2Race, RatingChange, WinnerWas
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Map.ToString(), (Map)this.Map);
            info.AddValue(Field.Player1Race.ToString(), (byte)this.Player1Race);
            info.AddValue(Field.Player2Race.ToString(), (byte)this.Player2Race);
            info.AddValue(Field.RatingChange.ToString(), (byte)this.RatingChange);
            info.AddValue(Field.WinnerWas.ToString(), (byte)this.WinnerWas);
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
                        case Field.WinnerWas: this.WinnerWas = (PlayerSlotType)info.GetByte(field.ToString()); break;
                    }
                }

            }
        }
        #endregion
    }
}
