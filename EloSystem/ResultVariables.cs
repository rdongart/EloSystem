using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class ResultVariables : ISerializable
    {
        private int _protoss;
        private int _random;
        private int _terran;
        private int _zerg;
        public int Protoss
        {
            get
            {
                return this._protoss;
            }
            internal set
            {
                this._protoss = value < 0 ? 0 : value;
            }
        }
        public int Random
        {
            get
            {
                return this._random;
            }
            internal set
            {
                this._random = value < 0 ? 0 : value;
            }
        }
        public int Terran
        {
            get
            {
                return this._terran;
            }
            internal set
            {
                this._terran = value < 0 ? 0 : value;
            }
        }
        public int Zerg
        {
            get
            {
                return this._zerg;
            }
            internal set
            {
                this._zerg = value < 0 ? 0 : value;
            }
        }

        internal ResultVariables(int startValue)
        {
            this.Protoss = startValue;
            this.Random = startValue;
            this.Terran = startValue;
            this.Zerg = startValue;
        }

        #region Implementing ISerializable
        private enum Field
        {
            Protoss, Random, Terran, Zerg
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Protoss.ToString(), (ushort)this.Protoss);
            info.AddValue(Field.Random.ToString(), (ushort)this.Random);
            info.AddValue(Field.Terran.ToString(), (ushort)this.Terran);
            info.AddValue(Field.Zerg.ToString(), (ushort)this.Zerg);
        }
        internal ResultVariables(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Protoss: this.Protoss = (int)info.GetUInt16(field.ToString()); break;
                        case Field.Random: this.Random = (int)info.GetUInt16(field.ToString()); break;
                        case Field.Terran: this.Terran = (int)info.GetUInt16(field.ToString()); break;
                        case Field.Zerg: this.Zerg = (int)info.GetUInt16(field.ToString()); break;
                    }
                }

            }
        }
        #endregion

        public int GetValueFor(Race race)
        {
            switch (race)
            {
                case Race.Protoss: return this.Protoss;
                case Race.Random: return this.Random;
                case Race.Terran: return this.Terran;
                case Race.Zerg: return this.Zerg;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }
        public void AddValueTo(Race race, int value)
        {
            switch (race)
            {
                case Race.Protoss: this.Protoss += value; break;
                case Race.Random: this.Random += value; break;
                case Race.Terran: this.Terran += value; break;
                case Race.Zerg: this.Zerg += value; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }
    }
}
