﻿using CustomExtensionMethods;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCEloSystemGUI
{
    public class MatchEditorItem
    {
        private int seasonIndex = 0;
        private List<GameEntryEditorItem> editedGames;
        private readonly List<Game> sourceGames;
        internal readonly Match SourceMatch;
        public bool DateWasEdited
        {
            get
            {
                return this.DateValue.CompareTo(this.SourceMatch.DateTime) != 0;
            }
        }
        public DateTime DateValue { get; set; }
        /// <summary>
        /// The date from which this match's results could influence the ratings given from other matches.
        /// </summary>
        public DateTime LatestDateOfInfluence
        {
            get
            {
                return this.DateValue.CompareTo(this.SourceMatch.DateTime) < 0 ? this.DateValue : this.SourceMatch.DateTime;
            }
        }
        public int SeasonIndex
        {
            get
            {
                return this.seasonIndex;
            }
            set
            {
                if (this.Tournament != null && this.Tournament.GetSeasons().Count() <= value) { this.seasonIndex = this.Tournament.GetSeasons().Count() - 1; }
                else { this.seasonIndex = value; }
            }
        }
        public SCPlayer Player1 { get; set; }
        public SCPlayer Player2 { get; set; }
        public Season Season
        {
            get
            {
                return (this.Tournament == null || this.SeasonIndex == -1) ? null : this.Tournament.GetSeasons().ElementAt(this.SeasonIndex);
            }
        }
        public Tournament Tournament { get; set; }

        internal MatchEditorItem(IEnumerable<Game> games, Match match)
        {
            this.SourceMatch = match;
            this.sourceGames = games.ToList();

            this.SetInitialProperties();
        }

        private void SetInitialProperties()
        {
            this.DateValue = this.SourceMatch.DateTime;
            this.Tournament = this.sourceGames.First().Tournament;
            this.SeasonIndex = this.Tournament == null ? -1 : this.Tournament.GetSeasons().IndexOf(this.sourceGames.First().Season);
            this.Player1 = this.SourceMatch.Player1;
            this.Player2 = this.SourceMatch.Player2;

            this.editedGames = this.sourceGames.Select(game => new GameEntryEditorItem(game)).ToList();
        }

        public void CancelChanges()
        {
            this.SetInitialProperties();
        }

        public void AddGame(GameEntryEditorItem game)
        {
            this.editedGames.Add(game);
        }

        public IEnumerable<GameEntryEditorItem> GetGames()
        {
            foreach (GameEntryEditorItem game in this.editedGames) { yield return game; }
        }

        public string RatingChangeBy(PlayerSlotType player)
        {
            if (this.HasBeenEdited()) { return "?"; }

            switch (player)
            {
                case PlayerSlotType.Player1:
                    return (this.sourceGames.Where(game => game.Winner == this.Player1).Sum(game => game.RatingChange) - this.sourceGames.Where(game => game.Winner != this.Player1).Sum(game
                            => game.RatingChange)).ToString();
                case PlayerSlotType.Player2:
                    return (this.sourceGames.Where(game => game.Winner == this.Player2).Sum(game => game.RatingChange) - this.sourceGames.Where(game => game.Winner != this.Player2).Sum(game
                            => game.RatingChange)).ToString();
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, player.ToString()));
            }

        }

        public int WinsBy(PlayerSlotType player)
        {
            return this.editedGames.Count(game => game.WinnerWas == player);
        }

        public bool HasBeenEdited()
        {
            return this.Tournament != this.sourceGames.First().Tournament || this.Season != this.sourceGames.First().Season || this.DateValue.Date.CompareTo(this.SourceMatch.DateTime.Date) != 0
                || this.Player1 != this.SourceMatch.Player1 || this.Player2 != this.SourceMatch.Player2 || this.sourceGames.Where((game, index) => this.editedGames[index].IsDifferentFrom(game)).Any()
                || this.sourceGames.Count != this.editedGames.Count;
        }

        public void SetGames(IEnumerable<GameEntryEditorItem> gameReports)
        {
            if (gameReports.Any()) { this.editedGames = gameReports.ToList(); }
        }

        public bool HasPlayer(SCPlayer player)
        {
            return this.SourceMatch.HasPlayer(player);
        }
    }
}
