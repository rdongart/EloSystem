using CustomExtensionMethods;
using EloSystem;
using System;

namespace EloSystemExtensions
{
    internal class ExtendedGameData
    {
        public bool UnderDogWasWinner
        {
            get
            {
                return this.ExpectedWinRatioWinner < this.ExpectedWinRatioLoser;
            }
        }
        public double ExpectedWinRatioLoser
        {
            get
            {
                return 1 - this.ExpectedWinRatioWinner;
            }
        }
        public double ExpectedWinRatioUnderdog
        {
            get
            {
                return Math.Min(this.ExpectedWinRatioWinner, this.ExpectedWinRatioLoser).Round(2);
            }
        }
        public double ExpectedWinRatioWinner { get; private set; }
        public Game Game { get; private set; }
        public MirrorMatchup Matchup { get; private set; }

        internal ExtendedGameData(Game game, double expectedWinRatioWinner, MirrorMatchup mmType)
        {
            this.Game = game;
            this.ExpectedWinRatioWinner = expectedWinRatioWinner;
            this.Matchup = mmType;
        }

    }
}
