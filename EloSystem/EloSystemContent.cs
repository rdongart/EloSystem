using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    abstract public class EloSystemContent : ISerializable, IHasName, IHasImageID
    {
        public string Name { get; private set; }
        public int ImageID { get; internal set; }

        internal EloSystemContent(string name, int imageID)
        {
            this.Name = name;
            this.ImageID = imageID;
        }

        #region Implementing ISerializable
        private enum Field
        {
            ImageID, Name
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.ImageID.ToString(), (int)this.ImageID);
            info.AddValue(Field.Name.ToString(), (string)this.Name);
        }

        internal EloSystemContent(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.ImageID: this.ImageID = (int)info.GetInt32(field.ToString()); break;
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                    }
                }

            }
        }
        #endregion               
    }
}
