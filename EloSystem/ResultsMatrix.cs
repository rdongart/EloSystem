using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class SCResultsMatrix : ISerializable
    {
        private GameCount asProtossVs;
        private GameCount asRandomVs;
        private GameCount asTerranVs;
        private GameCount asZergVs;

        internal SCResultsMatrix()
        {
            this.asProtossVs = new GameCount();
            this.asRandomVs = new GameCount();
            this.asTerranVs = new GameCount();
            this.asZergVs = new GameCount();
        }

        #region Implementing ISerializable
        private enum Field
        {
            asProtossVs, asRandomVs, asTerranVs, asZergVs
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.asProtossVs.ToString(), (GameCount)this.asProtossVs);
            info.AddValue(Field.asRandomVs.ToString(), (GameCount)this.asRandomVs);
            info.AddValue(Field.asTerranVs.ToString(), (GameCount)this.asTerranVs);
            info.AddValue(Field.asZergVs.ToString(), (GameCount)this.asZergVs);
        }
        internal SCResultsMatrix(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.asProtossVs: this.asProtossVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.asRandomVs: this.asRandomVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.asTerranVs: this.asTerranVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                        case Field.asZergVs: this.asZergVs = (GameCount)info.GetValue(field.ToString(), typeof(GameCount)); break;
                    }
                }

            }
        }
        #endregion

        internal GameCount GamesAs(Race race)
        {
            switch (race)
            {
                case Race.Zerg: return this.asZergVs;
                case Race.Terran: return this.asTerranVs;
                case Race.Protoss: return this.asProtossVs;
                case Race.Random: return this.asRandomVs;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }

        internal int GamesVs(Race opponentRace)
        {
            return this.asProtossVs.Vs(opponentRace) + this.asRandomVs.Vs(opponentRace) + this.asTerranVs.Vs(opponentRace) + this.asZergVs.Vs(opponentRace);
        }

        internal int GamesTotal()
        {
            return this.asProtossVs.Total() + this.asRandomVs.Total() + this.asTerranVs.Total() + this.asZergVs.Total();
        }
    }
}
