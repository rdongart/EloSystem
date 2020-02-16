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
            if (game.Winner.Equals(this.Player)) { this.RatingVs.AddValueTo(game.LosersRace, -game.RatingChange); }
            else { this.RatingVs.AddValueTo(game.WinnersRace, game.RatingChange); }

            Race ownRace = game.Player1.Equals(this.Player) ? game.Player1Race : game.Player2Race;
            Race opponentRace = game.Player1.Equals(this.Player) ? game.Player2Race : game.Player1Race;

            if (game.Winner.Equals(this.Player)) { this.Stats.RollBackWin(ownRace, opponentRace); }
            else if (game.Loser.Equals(this.Player)) { this.Stats.RollBackLoss(ownRace, opponentRace); }

        }

        public int RatingTotal()
        {
            return SCPlayer.RatingTotal(this.RatingVs, this.Stats);
        }
    }
}
