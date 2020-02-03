using System;

namespace EloSystem
{
    public class PlayerStatsCloneDev
    {
        public readonly DateTime Date;
        public readonly ResultVariables RatingVs;
        public readonly WinRateCounter Stats;

        internal PlayerStatsCloneDev(PlayerStatsClone playerStats, DateTime date)
        {
            this.Date = date;
            this.RatingVs = playerStats.RatingVs.Clone();
            this.Stats = playerStats.Stats.Clone();
        }

        public int RatingTotal()
        {
            return SCPlayer.RatingTotal(this.RatingVs, this.Stats);
        }
    }
}
