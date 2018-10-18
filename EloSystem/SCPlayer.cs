using CustomExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class SCPlayer : EloSystemContent, ISerializable
    {
        private const int MINIMUM_GAMES_THRESHOLD = 10; // the minimum number of games for a particular rating value to be given full influence on the total rating

        internal Country Country { get; private set; }
        internal ResultVariables games { get; private set; }
        internal Team Team { get; set; }

        private List<string> aliases;

        public ResultVariables Ratings { get; private set; }
        public string TeamName
        {
            get
            {
                return this.Team == null ? "" : this.Team.Name;
            }
        }
        public string Nationality
        {
            get
            {
                return this.Country == null ? "" : this.Country.Name;
            }
        }


        internal SCPlayer(string name, int startRating, int imageID, Team team = null, Country nationality = null)
            : this(name, startRating, imageID, new string[] { }, team, nationality)
        {

        }
        internal SCPlayer(string name, int startRating, int imageID, IEnumerable<string> aliases = null, Team team = null, Country nationality = null) : base(name, imageID)
        {
            this.aliases = aliases == null ? new List<string>() : aliases.ToList();
            this.Country = nationality;
            this.games = new ResultVariables(0);
            this.Ratings = new ResultVariables(startRating);
            this.Team = team;
        }

        private static double GetRatingInfluence(int numberOfGames)
        {
            return Math.Min(1, numberOfGames / (double)SCPlayer.MINIMUM_GAMES_THRESHOLD);
        }

        #region Implementing ISerializable
        private enum Field
        {
            Aliases, Games, Name, Country, Ratings, Team
        }
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(Field.Aliases.ToString(), (List<string>)this.aliases);
            info.AddValue(Field.Team.ToString(), (Country)this.Country);
            info.AddValue(Field.Games.ToString(), (ResultVariables)this.games);
            info.AddValue(Field.Ratings.ToString(), (ResultVariables)this.Ratings);
            info.AddValue(Field.Team.ToString(), (Team)this.Team);
        }
        internal SCPlayer(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Aliases: this.aliases = (List<string>)info.GetValue(field.ToString(), typeof(List<string>)); break;
                        case Field.Country: this.Country = (Country)info.GetValue(field.ToString(), typeof(Country)); break;
                        case Field.Games: this.games = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
                        case Field.Ratings: this.Ratings = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
                        case Field.Team: this.Team = (Team)info.GetValue(field.ToString(), typeof(Team)); break;
                    }
                }

            }
        }
        #endregion

        private bool HasPlayedAnyGames()
        {
            return this.games.VsProtoss > 0 || this.games.VsRandom > 0 || this.games.VsTerran > 0 || this.games.VsZerg > 0;
        }

        private double GetTotalInfluenceValue()
        {
            return SCPlayer.GetRatingInfluence(this.games.VsProtoss) + SCPlayer.GetRatingInfluence(this.games.VsRandom)
                + SCPlayer.GetRatingInfluence(this.games.VsTerran) + SCPlayer.GetRatingInfluence(this.games.VsZerg);
        }

        public IEnumerable<string> GetAliases()
        {
            foreach (string name in this.aliases.ToList()) { yield return name; }
        }

        /// <summary>
        /// Returns an integer representing the total rating
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            // if any games have been played
            return this.HasPlayedAnyGames() ?

                // ...calculate the rating
                ((this.Ratings.VsProtoss * SCPlayer.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * SCPlayer.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * SCPlayer.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * SCPlayer.GetRatingInfluence(this.games.VsProtoss))
                / this.GetTotalInfluenceValue()).RoundToInt()

                // ...otherwise just return the default starting rating value
                : this.Ratings.VsProtoss;

        }

        public void AddAlias(string alias)
        {
            this.aliases.Add(alias);
        }
        public bool RemoveAlias(string alias)
        {
            return this.aliases.Remove(alias);
        }

    }
}
