using EloSystem;
using SCEloSystemGUI.UserControls;
using System;

namespace SCEloSystemGUI
{
    public class GameEntryEditorItem
    {
        private bool player1IsSetToWinner = true;
        private bool player2IsSetToWinner = false;
        public bool Player1IsSetToWinner
        {
            get
            {
                return this.player1IsSetToWinner;
            }
            set
            {
                if (value == true) { this.Player2IsSetToWinner = false; }

                this.player1IsSetToWinner = value;
            }
        }
        public bool Player2IsSetToWinner
        {
            get
            {
                return this.player2IsSetToWinner;
            }
            set
            {
                if (value == true) { this.Player1IsSetToWinner = false; }

                this.player2IsSetToWinner = value;
            }
        }
        public Race Player1Race { get; set; }
        public Race Player2Race { get; set; }
        public PlayerSlotType WinnerWas
        {
            get
            {
                return this.Player1IsSetToWinner ? PlayerSlotType.Player1 : PlayerSlotType.Player2;
            }
        }
        public Map Map { get; set; }

        public GameEntryEditorItem()
        {

        }

        public GameEntryEditorItem(Game game)
        {
            this.Player2IsSetToWinner = game.Winner.Equals(game.Player2);

            this.Player1Race = game.Player1Race;
            this.Player2Race = game.Player2Race;

            this.Map = game.Map;
        }

        public GameEntryEditorItem(GameReport gameReport)
        {
            this.Player2IsSetToWinner = gameReport.WinnerPlayer.Equals(gameReport.Player2);

            this.Player1Race = (Race)gameReport.RaceIndexPlayer1;
            this.Player2Race = (Race)gameReport.RaceIndexPlayer2;

            this.Map = gameReport.GetMapOrDefault();
        }

        public bool IsDifferentFrom(GameReport report)
        {
            return (int)this.Player1Race != report.RaceIndexPlayer1 || (int)this.Player2Race != report.RaceIndexPlayer2 || (report.WinnerSlot == PlayerSlotType.Player1 && this.Player2IsSetToWinner)
                || (report.WinnerSlot == PlayerSlotType.Player2 && this.Player1IsSetToWinner) || report.GetMapOrDefault() != this.Map;
        }

        public bool IsDifferentFrom(Game game)
        {
            return this.Player1Race != game.Player1Race || this.Player2Race != game.Player2Race || (this.WinnerWas == PlayerSlotType.Player1 && game.Winner.Equals(game.Player2))
                || (this.WinnerWas == PlayerSlotType.Player2 && game.Winner.Equals(game.Player1)) || this.Map != game.Map;
        }

        public void SetRace(PlayerSlotType player, Race race)
        {
            switch (player)
            {
                case PlayerSlotType.Player1: this.Player1Race = race; break;
                case PlayerSlotType.Player2: this.Player2Race = race; break;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, player.ToString()));
            }
        }
    }
}
