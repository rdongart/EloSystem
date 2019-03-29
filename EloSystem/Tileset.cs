using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Tileset : HasNameContent, ISerializable
    {
        public Tileset(string name, int id) : base(name, id)
        {

        }

        #region Implementing ISerializable
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        internal Tileset(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion               
    }
}
