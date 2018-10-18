using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Team : EloSystemContent, ISerializable
    {
        internal Team(string name, int imageID) : base(name, imageID)
        {

        }

        #region Implementing ISerializable
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        internal Team(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        #endregion               
    }
}
