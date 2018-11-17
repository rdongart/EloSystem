using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Drawing;


namespace EloSystem
{
    public enum MapPlayerType
    {
        _2_Player = 2, _3_Player, _4_Player, _5_Player, _6_Player, _7_Player, _8_Player
    }

    [Serializable]
    public class Map : EloSystemContent, ISerializable
    {
        private List<string> descriptions;
        public MapPlayerType MapType { get; set; }
        public MapStats Stats { get; private set; }
        public Size Size { get; set; }
        public Tileset Tileset { get; set; }

        internal Map(string name, int imageID, MapPlayerType mapType) : base(name, imageID)
        {
            this.descriptions = new List<string>();
            this.Stats = new MapStats();
            this.MapType = mapType;
        }

        #region Implementing ISerializable
        private enum Field
        {
            Descriptions, MapType, Size, Stats, Tileset
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Descriptions.ToString(), (List<string>)this.descriptions);
            info.AddValue(Field.Size.ToString(), (Size)this.Size);
            info.AddValue(Field.Stats.ToString(), (MapStats)this.Stats);
            info.AddValue(Field.MapType.ToString(), (byte)this.MapType);
            info.AddValue(Field.Tileset.ToString(), (Tileset)this.Tileset);
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
                        case Field.Descriptions: this.descriptions = (List<string>)info.GetValue(field.ToString(), typeof(List<string>)); break;
                        case Field.MapType: this.MapType = (MapPlayerType)info.GetByte(field.ToString()); break;
                        case Field.Size: this.Size = (Size)info.GetValue(field.ToString(), typeof(Size)); break;
                        case Field.Stats: this.Stats = (MapStats)info.GetValue(field.ToString(), typeof(MapStats)); break;
                        case Field.Tileset: this.Tileset = (Tileset)info.GetValue(field.ToString(), typeof(Tileset)); break;
                    }

                }
            }
        }
        #endregion

        public bool EditDescription(string oldDescription, string newDescription)
        {
            int index = this.descriptions.IndexOf(oldDescription);

            if (index > -1)
            {
                this.descriptions.Remove(oldDescription);
                this.descriptions.Insert(index, newDescription);
                return true;
            }
            else { return false; }
        }

        public bool RemoveDescription(string descriptionToRemove)
        {
            return this.descriptions.Remove(descriptionToRemove);
        }

        public IEnumerable<string> GetDescriptions()
        {
            foreach (string description in this.descriptions) { yield return description; }
        }

        public void AddDescription(string description)
        {
            this.descriptions.Add(description);
        }
    }
}
