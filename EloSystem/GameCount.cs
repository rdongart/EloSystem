using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace EloSystem
{
    [Serializable]
    public class GameCount : ResultVariables, ISerializable
    {

        internal GameCount() : base(0)
        {

        }

        #region Implementing ISerializable
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        internal GameCount(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion

        public int Total()
        {
            return this.Protoss + this.Random + this.Terran + this.Zerg;
        }
        public int Vs(Race race)
        {
            switch (race)
            {
                case Race.Zerg: return this.Zerg;
                case Race.Terran: return this.Terran;
                case Race.Protoss: return this.Protoss;
                case Race.Random: return this.Random;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }
    }
}
