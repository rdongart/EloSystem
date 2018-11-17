using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Tournament : EloSystemContent, ISerializable
    {
        private List<Season> seasons;
        internal Season DefaultSeason
        {
            get
            {
                return this.seasons[0];
            }
        }

        internal Tournament(string name, int imageID) : base(name, imageID)
        {
            this.seasons = new List<Season>() { new Season("N/A") };
        }

        #region Implementing ISerializable
        private enum Field
        {
            Seasons
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Seasons.ToString(), (List<Season>)this.seasons);
        }

        internal Tournament(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Seasons: this.seasons = (List<Season>)info.GetValue(field.ToString(), typeof(List<Season>)); break;
                    }
                }

            }
        }
        #endregion

        internal bool RemoveSeason(Season season)
        {
            if (season != this.DefaultSeason) { return this.seasons.Remove(season); }
            else { return false; }
        }

        internal void AddSeason(Season season)
        {
            this.seasons.Add(season);
        }

        public IEnumerable<Game> GetGames()
        {
            foreach (Game game in this.seasons.SelectMany(season => season.GetMatches().SelectMany(match => match.GetEntries().Select(entry => new Game(match.Player1, match.Player2, entry, match, this, season.Equals(this.DefaultSeason) ? null : season)))))
            {
                yield return game;
            }
        }

        public IEnumerable<Season> GetSeasons()
        {
            foreach (Season season in this.seasons.Skip(1)) { yield return season; }
        }
    }
}