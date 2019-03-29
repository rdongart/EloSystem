using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Team : EloSystemContent, IHasDblName, ISerializable
    {
        public string NameLong { get; set; }

        internal Team(string name, int imageID, int id) : base(name, imageID, id)
        {
            this.NameLong = string.Empty;
        }

        #region Implementing ISerializable
        private enum Field
        {
            NameLong
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.NameLong.ToString(), (string)this.NameLong);
        }

        internal Team(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field)) { switch (field) { case Field.NameLong: this.NameLong = (string)info.GetString(field.ToString()); break; } }
            }
        }
        #endregion               
    }
}
