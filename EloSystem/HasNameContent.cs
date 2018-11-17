﻿using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public abstract class HasNameContent : IHasName, ISerializable
    {
        public string Name { get; set; }

        internal HasNameContent(string name)
        {
            this.Name = name;
        }

        #region Implementing ISerializable
        private enum Field
        {
            ImageID, Name
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
                        case Field.Name: this.Name = (string)info.GetString(field.ToString()); break;
                    }
                }

            }
        }
        #endregion               
    }
}
