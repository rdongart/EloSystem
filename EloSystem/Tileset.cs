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

        public bool Equals(Tileset obj)
        {
            return this.Equals(obj);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            else if (this.GetType() != obj.GetType()) { return false; }
            else
            {
                var tileObj = obj as Tileset;

                return this.Name.Equals(tileObj.Name) && this.ID.Equals(tileObj.ID);
            }
        }
        public override int GetHashCode()
        {
            const int HASH_SEED = 53;

            unchecked { return this.ID.GetHashCode() * this.Name.GetHashCode() * HASH_SEED; }
        }
    }
}
