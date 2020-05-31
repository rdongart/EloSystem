using System;

namespace EloSystemExtensions
{
    public enum MirrorMatchup
    {
        ZvZ, TvT, PvP, RvR
    }

    internal class MirrorMathcupPlayerData
    {
        public int GamesPvP { get; internal set; }
        public int GamesTvT { get; internal set; }
        public int GamesZvZ { get; internal set; }
        public int RatingPvP { get; internal set; }
        public int RatingTvT { get; internal set; }
        public int RatingZvZ { get; internal set; }

        internal MirrorMathcupPlayerData(int startRating)
        {
            this.RatingPvP = startRating;
            this.RatingTvT = startRating;
            this.RatingZvZ = startRating;
        }

        public int RatingIn(MirrorMatchup mm)
        {
            switch (mm)
            {
                case MirrorMatchup.PvP: return this.RatingPvP;
                case MirrorMatchup.TvT: return this.RatingTvT;
                case MirrorMatchup.ZvZ: return this.RatingZvZ;
                case MirrorMatchup.RvR: return 0;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MirrorMatchup).Name, mm.ToString()));
            }
        }

        public int GamesIn(MirrorMatchup mm)
        {
            switch (mm)
            {
                case MirrorMatchup.PvP: return this.GamesPvP;
                case MirrorMatchup.TvT: return this.GamesTvT;
                case MirrorMatchup.ZvZ: return this.GamesZvZ;
                case MirrorMatchup.RvR: return 0;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MirrorMatchup).Name, mm.ToString()));
            }
        }

        public void RegisterResult(MirrorMatchup mm, int ratingChange)
        {
            switch (mm)
            {
                case MirrorMatchup.ZvZ:
                    this.GamesZvZ++;
                    this.RatingZvZ += ratingChange;
                    break;
                case MirrorMatchup.TvT:
                    this.GamesTvT++;
                    this.RatingTvT += ratingChange;
                    break;
                case MirrorMatchup.PvP:
                    this.GamesPvP++;
                    this.RatingPvP += ratingChange;
                    break;
                case MirrorMatchup.RvR: break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MirrorMatchup).Name, mm.ToString()));
            }
        }
    }
}
