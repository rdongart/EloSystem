using CustomExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Season : HasNameContent, ISerializable
    {
        private List<Match> matches;

        internal Season(string name, int id) : base(name,id)
        {
            this.matches = new List<Match>();
        }

        #region Implementing ISerializable
        private enum Field
        {
            Matches
        }

        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Matches.ToString(), (List<Match>)this.matches);
        }

        internal Season(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Matches: this.matches = (List<Match>)info.GetValue(field.ToString(), typeof(List<Match>)); break;
                    }
                }

            }
        }
        #endregion

        public IEnumerable<Match> GetMatches()
        {
            foreach (Match match in this.matches.ToList(this.matches.Count)) { yield return match; }
        }

        internal void AddMatch(Match match)
        {
            this.matches.Add(match);
        }

        internal void RemoveMatch(Match match)
        {
            this.matches.Remove(match);
        }
    }
}
