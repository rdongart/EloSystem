namespace EloSystem
{
    public class PlayerStatsClone
    {
        public SCPlayer Player { get; private set; }
        public ResultVariables RatingVs { get; set; }
        public WinRateCounter Stats { get; set; }

        internal PlayerStatsClone(SCPlayer player)
        {
            this.Player = player;
            this.RatingVs = player.RatingVs.Clone();
            this.Stats = player.Stats.Clone();
        }

        internal void RollBackResult(Game game)
        {
            this.RollBackResult(game.Winner, game.Player1.Equals(this.Player) ? game.Player1Race : game.Player2Race, game.Player1.Equals(this.Player) ? game.Player2Race : game.Player1Race, game.RatingChange);
        }

        internal void RollBackResult(Match match)
        {
            foreach (GameEntry entry in match.GetEntries())
            {
                this.RollBackResult(
                    entry.WinnerWas == PlayerSlotType.Player1 ? match.Player1 : match.Player2
                    , match.Player1.Equals(this.Player) ? entry.Player1Race : entry.Player2Race
                    , match.Player1.Equals(this.Player) ? entry.Player2Race : entry.Player1Race
                    , entry.RatingChange);
            }
        }

        private void RollBackResult(SCPlayer winner, Race ownRace, Race opponentRace, int ratingChange)
        {
            bool thisPlayerWasWinner = winner.Equals(this.Player);

            this.RatingVs.AddValueTo(opponentRace, (thisPlayerWasWinner ? -ratingChange : ratingChange));

            if (thisPlayerWasWinner) { this.Stats.RollBackWin(ownRace, opponentRace); }
            else { this.Stats.RollBackLoss(ownRace, opponentRace); }
        }

        public int RatingTotal()
        {
            return SCPlayer.RatingTotal(this.RatingVs, this.Stats);
        }
    }
}
