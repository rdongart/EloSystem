using CustomExtensionMethods;
using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class Rating : ISerializable
    {
        private const int MINIMUM_GAMES_THRESHOLD = 10; // the minimum number of games for a particular rating value to be given full influence on the total rating

        internal ResultVariables games { get; set; }
        public ResultVariables Ratings { get; private set; }


        internal Rating(int startRating)
        {
            this.games = new ResultVariables(0);
            this.Ratings = new ResultVariables(startRating);
        }

        private static double GetRatingInfluence(int numberOfGames)
        {
            return Math.Min(1, numberOfGames / (double)Rating.MINIMUM_GAMES_THRESHOLD);
        }

        #region Implementing ISerializable
        private enum Field
        {
            Games, Ratings
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Games.ToString(), (ResultVariables)this.games);
            info.AddValue(Field.Ratings.ToString(), (ResultVariables)this.Ratings);
        }
        internal Rating(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Games:
                            this.games = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
                        case Field.Ratings:
                            this.Ratings = (ResultVariables)info.GetValue(field.ToString(), typeof(ResultVariables)); break;
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
            return Rating.GetRatingInfluence(this.games.VsProtoss) + Rating.GetRatingInfluence(this.games.VsRandom)
                + Rating.GetRatingInfluence(this.games.VsTerran) + Rating.GetRatingInfluence(this.games.VsZerg);
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
                ((this.Ratings.VsProtoss * Rating.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * Rating.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * Rating.GetRatingInfluence(this.games.VsProtoss)
                + this.Ratings.VsProtoss * Rating.GetRatingInfluence(this.games.VsProtoss))
                / this.GetTotalInfluenceValue()).RoundToInt()

                // ...otherwise just return the default starting rating value
                : this.Ratings.VsProtoss;

        }

    }
}
