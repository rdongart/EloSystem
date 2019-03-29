using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    abstract public class EloSystemContent : ISerializable, IHasName, IHasImageID
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public int ImageID { get; internal set; }

        internal EloSystemContent(string name, int imageID, int id)
        {
            this.Name = name;
            this.ImageID = imageID;
            this.ID = id;
        }

        #region Implementing ISerializable
        private enum Field
        {
            ID, ImageID, Name
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.ID.ToString(), (int)this.ID);
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
                        case Field.ID: this.ID = (int)info.GetInt32(field.ToString()); break;
                        case Field.ImageID: this.ImageID = (int)info.GetInt32(field.ToString()); break;
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                    }
                }

            }
        }
        #endregion               
    }
}
