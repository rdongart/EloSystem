using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Country : EloSystemContent, ISerializable
    {
        internal Country(string name, int imageID, int id) : base(name, imageID,id)
        {

        }

        #region Implementing ISerializable
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        internal Country(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        #endregion               
    }
}
