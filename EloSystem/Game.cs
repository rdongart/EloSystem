using System;
using CustomExtensionMethods;

namespace EloSystem
{
    public enum Race
    {
        Zerg = 0, Terran, Protoss, Random
    }

    public class Game
    {
        private GameEntry entryInMatch;
        /// <summary>
        /// The game's index in the list of games within a match.
        /// </summary>
        public int GameIndex
        {
            get
            {
                return this.Match.GetEntries().IndexOf(this.entryInMatch);
            }
        }
        public int RatingChange
        {
            get
            {
                return this.entryInMatch.RatingChange;
            }
        }
        public Map Map
        {
            get
            {
                return this.entryInMatch.Map;
            }
        }
        public Match Match { get; private set; }
        public Matchup MatchType
        {
            get
            {
                return this.entryInMatch.MatchType;
            }
        }
        public Race Player1Race
        {
            get
            {
                return this.entryInMatch.Player1Race;
            }
        }
        public Race Player2Race
        {
            get
            {
                return this.entryInMatch.Player2Race;
            }
        }
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
        public SCPlayer Player1
        {
            get
            {
                return this.Match.Player1;
            }
        }
        public SCPlayer Player2
        {
            get
            {
                return this.Match.Player2;
            }
        }
        public SCPlayer Winner
        {
            get
            {
                switch (this.entryInMatch.WinnerWas)
                {
                    case PlayerSlotType.Player1: return this.Match.Player1;
                    case PlayerSlotType.Player2: return this.Match.Player2;
                    default: throw new Exception(string.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, this.entryInMatch.WinnerWas.ToString()));
                }
            }
        }
        public SCPlayer Loser
        {
            get
            {
                switch (this.entryInMatch.WinnerWas)
                {
                    case PlayerSlotType.Player1: return this.Match.Player2;
                    case PlayerSlotType.Player2: return this.Match.Player1;
                    default: throw new Exception(string.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, this.entryInMatch.WinnerWas.ToString()));
                }
            }
        }
        public Season Season { get; internal set; }
        public Tournament Tournament { get; internal set; }

        internal Game(GameEntry gameData, Match match) : this(match.Player1, match.Player2, gameData, match)
        {

        }

        internal Game(SCPlayer player1, SCPlayer player2, GameEntry gameData, Match match, Tournament tournament = null, Season season = null)
            : this(tournament, season, gameData.Map, match)
        {
            this.entryInMatch = gameData;
        }

        private Game(Tournament tournament, Season season, Map map, Match match)
        {
            this.Tournament = tournament;
            this.Season = season;
            this.Match = match;
        }

        public bool HasPlayer(SCPlayer player)
        {
            return this.Match.HasPlayer(player);
        }
    }
}
