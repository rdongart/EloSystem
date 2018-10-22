namespace EloSystem
{
    public enum Race
    {
        Zerg, Terran, Protoss, Random
    }

    public class Game
    {
        public Map Map { get; private set; }
        public Race Player1Race { get; private set; }
        public Race Player2Race { get; private set; }
        public Race WinnersRace
        {
            get
            {
                return this.Winner == this.Player1 ? this.Player1Race : this.Player2Race;
            }
        }
        public Race LosersRace
        {
            get
            {
                return this.Winner == this.Player1 ? this.Player2Race : this.Player1Race;
            }
        }
        public SCPlayer Player1 { get; private set; }
        public SCPlayer Player2 { get; private set; }
        public SCPlayer Winner { get; private set; }
        public SCPlayer Loser
        {
            get
            {
                return this.Winner == this.Player1 ? this.Player2 : this.Player1;
            }
        }

        internal Game(SCPlayer player1, SCPlayer player2, GameEntry gameData)
            : this(gameData.Map, player1, gameData.Player1Race, player2, gameData.Player2Race, gameData.WinnerWas == PlayerSlotType.Player1 ? player1 : player2)
        {

        }
        internal Game(Map map, SCPlayer player1, Race player1Race, SCPlayer player2, Race player2Race, SCPlayer winner)
        {
            this.Map = map;
            this.Player1 = player1;
            this.Player1Race = player1Race;
            this.Player2 = player2;
            this.Player2Race = player2Race;
            this.Winner = winner;
        }

    }
}
