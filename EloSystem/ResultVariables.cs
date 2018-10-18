using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class ResultVariables : ISerializable
    {
        private int _vsProtoss;
        private int _vsRandom;
        private int _vsTerran;
        private int _vsZerg;
        public int VsProtoss
        {
            get
            {
                return this._vsProtoss;
            }
            internal set
            {
                this._vsProtoss = value < 0 ? 0 : value;
            }
        }
        public int VsRandom
        {
            get
            {
                return this._vsRandom;
            }
            internal set
            {
                this._vsRandom = value < 0 ? 0 : value;
            }
        }
        public int VsTerran
        {
            get
            {
                return this._vsTerran;
            }
            internal set
            {
                this._vsTerran = value < 0 ? 0 : value;
            }
        }
        public int VsZerg
        {
            get
            {
                return this._vsZerg;
            }
            internal set
            {
                this._vsZerg = value < 0 ? 0 : value;
            }
        }

        internal ResultVariables(int startValue)
        {
            this.VsProtoss = startValue;
            this.VsRandom = startValue;
            this.VsTerran = startValue;
            this.VsZerg = startValue;
        }

        #region Implementing ISerializable
        private enum Field
        {
            VsProtoss, VsRandom, VsTerran, VsZerg
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.VsProtoss.ToString(), (ushort)this.VsProtoss);
            info.AddValue(Field.VsRandom.ToString(), (ushort)this.VsRandom);
            info.AddValue(Field.VsTerran.ToString(), (ushort)this.VsTerran);
            info.AddValue(Field.VsZerg.ToString(), (ushort)this.VsZerg);
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
                        case Field.VsProtoss: this.VsProtoss = (int)info.GetUInt16(field.ToString()); break;
                        case Field.VsRandom: this.VsRandom = (int)info.GetUInt16(field.ToString()); break;
                        case Field.VsTerran: this.VsTerran = (int)info.GetUInt16(field.ToString()); break;
                        case Field.VsZerg: this.VsZerg = (int)info.GetUInt16(field.ToString()); break;
                    }
                }

            }
        }
        #endregion

        public int GetValueVs(Race vsRace)
        {
            switch (vsRace)
            {
                case Race.Protoss: return this.VsProtoss;
                case Race.Random: return this.VsRandom;
                case Race.Terran: return this.VsTerran;
                case Race.Zerg: return this.VsZerg;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, vsRace.ToString()));
            }
        }
        public void AddValueVs(Race vsRace, int value)
        {
            switch (vsRace)
            {
                case Race.Protoss: this.VsProtoss += value; break;
                case Race.Random: this.VsRandom += value; break;
                case Race.Terran: this.VsTerran += value; break;
                case Race.Zerg: this.VsZerg += value; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, vsRace.ToString()));
            }
        }
    }
}
