using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Map : EloSystemContent, ISerializable
    {
        public MapStats Stats { get; private set; }

        internal Map(string name, int imageID) : base(name, imageID)
        {
            this.Stats = new MapStats();
        }

        #region Implementing ISerializable
        private enum Field
        {
            Stats
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Stats.ToString(), (MapStats)this.Stats);
        }

        internal Map(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Stats: this.Stats = (MapStats)info.GetValue(field.ToString(), typeof(MapStats)); break;
                    }

                }
            }
        }
        #endregion
    }
}
