using System.Linq;
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

        internal WinRateCounter Clone()
        {
            var clone = new WinRateCounter();

            foreach (Race ownRace in Enum.GetValues(typeof(Race)).Cast<Race>())
            {
                foreach (Race opponentRace in Enum.GetValues(typeof(Race)).Cast<Race>())
                {
                    clone.totalGames.GamesAs(ownRace).AddValueTo(opponentRace, this.totalGames.GamesAs(ownRace).GetValueFor(opponentRace));
                    clone.wins.GamesAs(ownRace).AddValueTo(opponentRace, this.totalGames.GamesAs(ownRace).GetValueFor(opponentRace));
                }

            }

            return clone;
        }

        internal void RollBackResult(Game game)
        {
            if (game.Player1.Stats != this && game.Player2.Stats != this) { return; }

            Race ownRace = game.Player1.Stats == this ? game.Player1Race : game.Player2Race;
            Race vsRace = game.Player1.Stats == this ? game.Player2Race : game.Player1Race;

            if (game.Player1.Stats == this)
            {
                if (game.Winner == game.Player1) { this.RollBackWin(ownRace, vsRace); }
                else { this.RollBackLoss(ownRace, vsRace); }
            }
            else if (game.Player2.Stats == this)
            {
                if (game.Winner == game.Player2) { this.RollBackWin(ownRace, vsRace); }
                else { this.RollBackLoss(ownRace, vsRace); }
            }
        }

        internal void RollBackLoss(Race ownRace, Race vsRace)
        {
            this.DecrementTotalGames(ownRace, vsRace);
        }

        internal void RollBackWin(Race ownRace, Race vsRace)
        {
            this.wins.GamesAs(ownRace).AddValueTo(vsRace, -1);
            this.DecrementTotalGames(ownRace, vsRace);
        }

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

        private void DecrementTotalGames(Race ownRace, Race vsRace)
        {
            this.totalGames.GamesAs(ownRace).AddValueTo(vsRace, -1);
        }
    }
}
