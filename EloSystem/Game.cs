﻿namespace EloSystem
{
    public enum Race
    {
        Zerg = 0, Terran, Protoss, Random
    }

    public class Game
    {
        public int RatingChange { get; private set; }
        public Map Map { get; set; }
        public Match Match { get; private set; }
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
        public SCPlayer Player1 { get; set; }
        public SCPlayer Player2 { get; set; }
        public SCPlayer Winner { get; set; }
        public SCPlayer Loser
        {
            get
            {
                return this.Winner == this.Player1 ? this.Player2 : this.Player1;
            }
        }
        public Season Season { get; set; }
        public Tournament Tournament { get; set; }

        internal Game(GameEntry gameData, Match match) : this(match.Player1, match.Player2, gameData)
        {

        }

        internal Game(SCPlayer player1, SCPlayer player2, GameEntry gameData, Match match = null, Tournament tournament = null, Season season = null)
            : this(tournament, season, gameData.Map, match, player1, gameData.Player1Race, player2, gameData.Player2Race, gameData.WinnerWas == PlayerSlotType.Player1 ? player1 : player2, gameData.RatingChange)
        {

        }

        internal Game(Tournament tournament, Season season, Map map, Match match, SCPlayer player1, Race player1Race, SCPlayer player2, Race player2Race, SCPlayer winner, int ratingChange)
        {
            this.Tournament = tournament;
            this.Season = season;
            this.Map = map;
            this.Match = match;
            this.Player1 = player1;
            this.Player1Race = player1Race;
            this.Player2 = player2;
            this.Player2Race = player2Race;
            this.Winner = winner;
            this.RatingChange = ratingChange;
        }
    }
}
