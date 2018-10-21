using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class WinRateCounter : WinRateStats, ISerializable
    {
        internal WinRateCounter()
        {

        }

        #region Implementing ISerializable
        new public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        internal WinRateCounter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion

        internal void ReportLoss(Race ownRace, Race vsRace)
        {
            this.IncrementTotalGames(ownRace, vsRace);
        }

        internal void ReportWin(Race ownRace, Race vsRace)
        {
            this.wins.GamesAs(ownRace).AddValueTo(vsRace, 1);
            this.IncrementTotalGames(ownRace, vsRace);
        }

        private void IncrementTotalGames(Race ownRace, Race vsRace)
        {
            this.totalGames.GamesAs(ownRace).AddValueTo(vsRace, 1);
        }
    }
}
