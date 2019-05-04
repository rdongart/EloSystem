using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public abstract class HasNameContent : IHasName, ISerializable
    {
        public int ID { get; internal set; }
        public string Name { get; set; }

        internal HasNameContent(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }

        #region Implementing ISerializable
        private enum Field
        {
            ID, Name
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.ID.ToString(), (int)this.ID);
            info.AddValue(Field.Name.ToString(), (string)this.Name);
        }

        internal HasNameContent(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.ID: this.ID = (int)info.GetInt32(field.ToString()); break;
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                    }
                }

            }
        }
        #endregion               
    }
}
