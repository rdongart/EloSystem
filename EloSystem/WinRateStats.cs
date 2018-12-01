using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class WinRateStats : ISerializable
    {
        protected SCResultsMatrix totalGames;
        protected SCResultsMatrix wins;

        internal WinRateStats()
        {
            this.totalGames = new SCResultsMatrix();
            this.wins = new SCResultsMatrix();
        }

        #region Implementing ISerializable
        private enum Field
        {
            TotalGames, Wins
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.TotalGames.ToString(), (SCResultsMatrix)this.totalGames);
            info.AddValue(Field.Wins.ToString(), (SCResultsMatrix)this.wins);
        }
        internal WinRateStats(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.TotalGames: this.totalGames = (SCResultsMatrix)info.GetValue(field.ToString(), typeof(SCResultsMatrix)); break;
                        case Field.Wins: this.wins = (SCResultsMatrix)info.GetValue(field.ToString(), typeof(SCResultsMatrix)); break;
                    }
                }

            }
        }
        #endregion

        public int GamesInMathcup(Race ownRace, Race vsRace)
        {
            return this.totalGames.GamesAs(ownRace).GetValueFor(vsRace);
        }

        public int GamesWith(Race ownRace)
        {
            return this.totalGames.GamesAs(ownRace).Total();
        }

        public int GamesVs(Race vsRace)
        {
            return this.totalGames.GamesVs(vsRace);
        }

        public int GamesTotal()
        {
            return this.totalGames.GamesTotal();
        }

        public int WinsInMathcup(Race ownRace, Race vsRace)
        {
            return this.wins.GamesAs(ownRace).GetValueFor(vsRace);
        }

        public int WinsWith(Race ownRace)
        {
            return this.wins.GamesAs(ownRace).Total();
        }

        public int WinsVs(Race vsRace)
        {
            return this.wins.GamesVs(vsRace);
        }


        public int WinsTotal()
        {
            return this.wins.GamesTotal();
        }

        public double WinRatioInMathcup(Race ownRace, Race vsRace)
        {
            int gamesInMatchup = this.GamesInMathcup(ownRace, vsRace);

            return gamesInMatchup > 0 ? this.WinsInMathcup(ownRace, vsRace) / (double)gamesInMatchup : 0.0;
        }

        public double WinRatioWith(Race ownRace)
        {
            int gamesWithRace = this.GamesWith(ownRace);

            return gamesWithRace > 0 ? this.WinsWith(ownRace) / (double)gamesWithRace : 0.0;
        }

        public double WinRatioVs(Race vsRace)
        {
            int gamesVsRace = this.GamesVs(vsRace);

            return gamesVsRace > 0 ? this.WinsVs(vsRace) / (double)gamesVsRace : 0.0;
        }

        public double WinRatioTotal()
        {
            int gamesTotal = this.GamesTotal();

            return gamesTotal > 0 ? this.WinsTotal() / (double)gamesTotal : 0.0;
        }
    }
}

    
