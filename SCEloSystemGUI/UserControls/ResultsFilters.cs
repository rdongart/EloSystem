﻿using CustomControls;
using CustomExtensionMethods;
using EloSystem;
using EloSystem.ResourceManagement;
using EloSystemExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public delegate bool PlayerSelectorFilter(SCPlayer player);

    public partial class ResultsFilters : UserControl
    {
        private const string NO_SELECTION_TEXT = "<no player selected>";
        private const string NO_RESULTS_TEXT = "-";

        private bool haltPublishingMapFilterChange;
        private bool haltPublishingTournamentFilterChange;
        internal EventHandler ResultFilterChanged = delegate { };
        internal Map SelectedMap
        {
            get
            {
                return this.selectedMap;
            }
            private set
            {
                bool handlerShouldBeInvoked = this.selectedMap != value;

                this.selectedMap = value;

                if (handlerShouldBeInvoked)
                {
                    this.SetMapStats();
                    this.ResultFilterChanged.Invoke(this, new EventArgs());
                }
            }
        }
        internal SCPlayer OpponentPlayer
        {
            get
            {
                return this.opponentPlayer;
            }
            private set
            {
                bool handlerShouldBeInvoked = false;

                if (this.opponentPlayer != value) { handlerShouldBeInvoked = true; }

                this.opponentPlayer = value;

                if (handlerShouldBeInvoked)
                {
                    this.SetResults();
                    this.UpdateMapSelector();
                    this.btnRemovePlayer.Enabled = this.opponentPlayer != null;
                    this.ResultFilterChanged.Invoke(this, new EventArgs());
                }
            }
        }
        internal Tournament SelectedTournament
        {
            get
            {
                return this.selectedTournament;
            }
            private set
            {
                bool handlerShouldBeInvoked = this.selectedTournament != value;

                this.selectedTournament = value;

                if (handlerShouldBeInvoked)
                {
                    this.SetTournamentStats();
                    this.ResultFilterChanged.Invoke(this, new EventArgs());
                }
            }
        }
        private Map selectedMap;
        private SCPlayer opponentPlayer;
        private SCPlayer player;
        private ImprovedImageComboBox<Tournament> tournamentSelector;
        private Tournament selectedTournament;

        internal ResultsFilters(SCPlayer player)
        {
            InitializeComponent();

            this.player = player;

            this.PopulateComboboxWithMaps();

            this.cmbBxMapSelection.SelectedIndex = 0;

            this.cmbBxMapSelection.SelectedIndexChanged += this.cmbBxMapSelection_SelectedIndexChanged;

            this.tournamentSelector = EloGUIControlsStaticMembers.CreateStandardImprovedImageComboBox<Tournament>(EloGUIControlsStaticMembers.ImageGetterMethod);

            this.tournamentSelector.Margin = new Padding(8, 9, 8, 0);
            this.tournamentSelector.NameGetter = (t) => t.Name;
            this.tournamentSelector.AddItems(GlobalState.DataBase.GetTournaments().Where(t => t.GetGames().Any(game => game.HasPlayer(this.player))).OrderBy(t => t.Name).ToArray(), true);
            this.tournamentSelector.SelectedIndexChanged += this.TournamentSelector_SelectedIndexChanged;
            this.tblLOPnlTournamentFilter.Controls.Add(this.tournamentSelector, 0, 3);
            this.tblLOPnlTournamentFilter.SetColumnSpan(this.tournamentSelector, 2);

            this.SetResults();
            this.SetMapStats();
            this.SetTournamentStats();

            this.toolTipMatchListFilter.SetToolTip(this.lbMapStatsHeader, String.Format("Displays the matchup stats from the whole database on this map. Only {0}'s preferred matchups are shown."
                , this.player.Name));
        }

        private void TournamentSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.haltPublishingTournamentFilterChange) { return; }

            var selTournament = this.tournamentSelector.SelectedValue as Tournament;

            this.SetTournamentFilter(selTournament);
        }

        private void PopulateComboboxWithMaps()
        {
            EloGUIControlsStaticMembers.PopulateComboboxWithMaps(this.cmbBxMapSelection, GlobalState.DataBase.GetMaps().Where(map => GlobalState.DataBase.GamesOnMap(map).Any(game => game.HasPlayer(this.player)))
                , true, this.player, this.OpponentPlayer);
        }

        private void UpdateMapSelector()
        {
            Map selectedMap = this.SelectedMap;

            this.PopulateComboboxWithMaps();

            if (selectedMap != null && this.cmbBxMapSelection.Items.OfType<Tuple<string, Map>>().Any(item => item.Item2 == selectedMap))
            {
                this.cmbBxMapSelection.SelectedIndex = this.cmbBxMapSelection.Items.IndexOf(this.cmbBxMapSelection.Items.OfType<Tuple<string, Map>>().First(item => item.Item2 == selectedMap));
            }
            else { this.cmbBxMapSelection.SelectedIndex = -1; }

        }

        private static string GetMapStatsFor(Race playersRace, Race vsRace, Map map)
        {
            const string NO_PERCENTAGE_TEXT = "--";

            Func<double, int, int, string> GetRaceFilteredMapStats = (winRatio, winsForPlayersRace, TotalGames) => String.Format("{0}%  -  {1} / {2}", ((winRatio) * 100).RoundToInt().ToString()
                , winsForPlayersRace.ToString("#,#"), TotalGames.ToString("#,#"));

            switch (vsRace)
            {
                case Race.Zerg:
                    switch (playersRace)
                    {
                        case Race.Zerg: return NO_PERCENTAGE_TEXT;
                        case Race.Terran: return GetRaceFilteredMapStats(map.Stats.ZvT.WinRatioRace2CorrectedForExpectedWR(), map.Stats.ZvT.Race2Wins, map.Stats.ZvT.TotalGames);
                        case Race.Protoss: return GetRaceFilteredMapStats(map.Stats.PvZ.WinRatioRace1CorrectedForExpectedWR(), map.Stats.PvZ.Race1Wins, map.Stats.PvZ.TotalGames);
                        case Race.Random: return GetRaceFilteredMapStats(map.Stats.ZvR.WinRatioRace2CorrectedForExpectedWR(), map.Stats.ZvR.Race2Wins, map.Stats.ZvR.TotalGames);
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).ToString(), playersRace.ToString()));
                    }
                case Race.Terran:
                    switch (playersRace)
                    {
                        case Race.Zerg: return GetRaceFilteredMapStats(map.Stats.ZvT.WinRatioRace1CorrectedForExpectedWR(), map.Stats.ZvT.Race1Wins, map.Stats.ZvT.TotalGames);
                        case Race.Terran: return NO_PERCENTAGE_TEXT;
                        case Race.Protoss: return GetRaceFilteredMapStats(map.Stats.PvT.WinRatioRace1CorrectedForExpectedWR(), map.Stats.PvT.Race1Wins, map.Stats.PvT.TotalGames);
                        case Race.Random: return GetRaceFilteredMapStats(map.Stats.TvR.WinRatioRace2CorrectedForExpectedWR(), map.Stats.TvR.Race2Wins, map.Stats.TvR.TotalGames);
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).ToString(), playersRace.ToString()));
                    }
                case Race.Protoss:
                    switch (playersRace)
                    {
                        case Race.Zerg:
                            return GetRaceFilteredMapStats(map.Stats.PvZ.WinRatioRace2CorrectedForExpectedWR(), map.Stats.PvZ.Race2Wins, map.Stats.PvZ.TotalGames);
                        case Race.Terran: return GetRaceFilteredMapStats(map.Stats.PvT.WinRatioRace2CorrectedForExpectedWR(), map.Stats.PvT.Race2Wins, map.Stats.PvT.TotalGames);
                        case Race.Protoss: return NO_PERCENTAGE_TEXT;
                        case Race.Random: return GetRaceFilteredMapStats(map.Stats.PvR.WinRatioRace2CorrectedForExpectedWR(), map.Stats.PvR.Race2Wins, map.Stats.PvR.TotalGames);
                        default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).ToString(), playersRace.ToString()));
                    }
                case Race.Random: return NO_PERCENTAGE_TEXT;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).ToString(), vsRace.ToString()));

            }
        }

        private void btnSelectPlayer_Click(object sender, EventArgs e)
        {
            if (HeadToHeadSelector.Show(this.player, (p) => p != this.player && GlobalState.DataBase.HeadToHeadGames(this.player, p).Any(), "Select player for head-to-head analysis") == DialogResult.OK)
            {
                this.SetHeadToHeadOpponent(HeadToHeadSelector.SelectedPlayer);
            }
        }

        public void SetHeadToHeadOpponent(SCPlayer player)
        {
            this.OpponentPlayer = player;

            this.lbOpponent.Text = this.OpponentPlayer.Name;

            EloImage playerRes;

            if (GlobalState.DataBase.TryGetImage(this.OpponentPlayer.ImageID, out playerRes))
            {
                this.picBxPlayer.Image = playerRes.Image;

                this.toolTipMatchListFilter.SetToolTip(this.picBxPlayer, this.OpponentPlayer.Name);
            }
            else
            {
                this.picBxPlayer.Image = null;
                this.toolTipMatchListFilter.SetToolTip(this.picBxPlayer, string.Empty);
            }

        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            this.OpponentPlayer = null;

            this.lbOpponent.Text = ResultsFilters.NO_SELECTION_TEXT;

            this.picBxPlayer.Image = null;

            this.toolTipMatchListFilter.SetToolTip(this.picBxPlayer, string.Empty);
        }

        public IEnumerable<MatchEditorItem> MatchFilterFromGames(IEnumerable<Game> games)
        {
            Func<IEnumerable<Game>, IEnumerable<Game>> OpponentFilter = gms =>
            {
                if (this.OpponentPlayer != null) { return gms.Where(game => game.HasPlayer(this.OpponentPlayer)); }
                else { return gms; }
            };

            Func<IEnumerable<Game>, IEnumerable<Game>> TournamentFilter = gms =>
            {
                if (this.SelectedTournament != null) { return gms.Where(game => game.Tournament == this.SelectedTournament); }
                else { return gms; }
            };

            IEnumerable<Game> gamesByPlayer = games.Where(game => game.HasPlayer(this.player));

            return OpponentFilter(TournamentFilter(gamesByPlayer)).ToMatchEditorItems();
        }

        public IEnumerable<Game> GamesFilter(IEnumerable<Game> games)
        {
            Func<IEnumerable<Game>, IEnumerable<Game>> OpponentFilter = gms =>
            {
                if (this.OpponentPlayer != null) { return gms.Where(game => game.HasPlayer(this.OpponentPlayer)); }
                else { return gms; }
            };

            Func<IEnumerable<Game>, IEnumerable<Game>> MapFilter = gms =>
            {
                if (this.SelectedMap != null) { return gms.Where(game => game.Map == this.SelectedMap); }
                else { return gms; }
            };

            Func<IEnumerable<Game>, IEnumerable<Game>> TournamentFilter = gms =>
            {
                if (this.SelectedTournament != null) { return gms.Where(game => game.Tournament == this.SelectedTournament); }
                else { return gms; }
            };

            IEnumerable<Game> gamesByPlayer = games.Where(game => game.HasPlayer(this.player));

            return OpponentFilter(MapFilter(TournamentFilter(gamesByPlayer)));
        }

        private void SetResults()
        {
            IEnumerable<Game> filteredGames = this.GamesFilter(GlobalState.DataBase.GetAllGames());

            Func<Race, int[]> WinsLossesVsRace = rc =>
            {
                var winsLosses = new int[2];

                IEnumerable<Game> gamesVsRace = filteredGames.Where(game => (game.Player1.Equals(this.player) && game.Player2Race == rc) || (game.Player2.Equals(this.player) && game.Player1Race == rc));

                winsLosses[0] = gamesVsRace.Where(game => game.Winner.Equals(this.player)).Count();
                winsLosses[1] = gamesVsRace.Where(game => game.Loser.Equals(this.player)).Count();

                return winsLosses;
            };

            Func<int[], string> GetVsRaceResultString = winsLosses =>
            {
                if (winsLosses.Sum() > 0)
                {
                    return String.Format("{0}%  -  {1} / {2}", (((double)winsLosses[0] / (winsLosses[0] + winsLosses[1])) * 100).RoundToInt().ToString(), winsLosses[0].ToString("#,#"), (winsLosses[0]
                        + winsLosses[1]).ToString("#,#"));
                }
                else { return NO_RESULTS_TEXT; }
            };

            if (this.OpponentPlayer != null || this.SelectedMap != null || this.SelectedTournament != null)
            {
                this.lbResultsTotal.Text = GetVsRaceResultString(new int[] {filteredGames.Where(game => game.Winner.Equals(this.player)).Count(), filteredGames.Where(game =>
                    game.Loser.Equals(this.player)).Count() });
                this.lbResultsZerg.Text = GetVsRaceResultString(WinsLossesVsRace(Race.Zerg));
                this.lbResultsTerran.Text = GetVsRaceResultString(WinsLossesVsRace(Race.Terran));
                this.lbResultsProtoss.Text = GetVsRaceResultString(WinsLossesVsRace(Race.Protoss));
                this.lbResultsRandom.Text = GetVsRaceResultString(WinsLossesVsRace(Race.Random));
            }
            else
            {
                this.lbResultsTotal.Text = NO_RESULTS_TEXT;
                this.lbResultsZerg.Text = NO_RESULTS_TEXT;
                this.lbResultsTerran.Text = NO_RESULTS_TEXT;
                this.lbResultsProtoss.Text = NO_RESULTS_TEXT;
                this.lbResultsRandom.Text = NO_RESULTS_TEXT;
            }
        }

        private void cmbBxMapSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.haltPublishingMapFilterChange) { return; }

            var selMap = this.cmbBxMapSelection.SelectedItem as Tuple<string, Map>;

            this.SetMapFilter(selMap.Item2);
        }

        public void SetMapFilter(Map map)
        {
            this.SelectedMap = map;

            if (this.SelectedMap != null) { this.picBxMap.Image = EloGUIControlsStaticMembers.ImageGetterMethod(map); }
            else { this.picBxMap.Image = null; }

            this.SetResults();

            // here we set the combobox with maps to the correct index
            this.haltPublishingMapFilterChange = true;

            if (this.cmbBxMapSelection.Items.Cast<Tuple<string, Map>>().Any(item => item.Item2 == map))
            {
                this.cmbBxMapSelection.SelectedIndex
                    = this.cmbBxMapSelection.Items.Cast<Tuple<string, Map>>().IndexOf(this.cmbBxMapSelection.Items.Cast<Tuple<string, Map>>().First(item => item.Item2 == map));
            }
            else { this.cmbBxMapSelection.SelectedIndex = -1; }

            this.haltPublishingMapFilterChange = false;
        }

        private void SetTournamentStats()
        {
            if (this.SelectedTournament == null) { this.lbTournamentGamesCount.Text = ResultsFilters.NO_RESULTS_TEXT; }
            else { this.lbTournamentGamesCount.Text = GlobalState.DataBase.GamesByPlayer(this.player).Where(game => game.Tournament == this.SelectedTournament).Count().ToString(Styles.NUMBER_FORMAT); }
        }

        private void SetMapStats()
        {
            if (this.SelectedMap == null)
            {
                this.lbRaceVsZerg.Text = ResultsFilters.NO_RESULTS_TEXT;
                this.lbRaceVsTerran.Text = ResultsFilters.NO_RESULTS_TEXT;
                this.lbRaceVsProtoss.Text = ResultsFilters.NO_RESULTS_TEXT;
            }
            else
            {
                this.lbRaceVsZerg.Text = String.Format("{0}vZ:  {1}", this.player.GetPrimaryRaceVs(Race.Zerg).ToString().Substring(0, 1)
                    , ResultsFilters.GetMapStatsFor(this.player.GetPrimaryRaceVs(Race.Zerg), Race.Zerg, this.SelectedMap));
                this.lbRaceVsTerran.Text = String.Format("{0}vT:  {1}", this.player.GetPrimaryRaceVs(Race.Terran).ToString().Substring(0, 1)
                    , ResultsFilters.GetMapStatsFor(this.player.GetPrimaryRaceVs(Race.Terran), Race.Terran, this.SelectedMap));
                this.lbRaceVsProtoss.Text = String.Format("{0}vP:  {1}", this.player.GetPrimaryRaceVs(Race.Protoss).ToString().Substring(0, 1)
                    , ResultsFilters.GetMapStatsFor(this.player.GetPrimaryRaceVs(Race.Protoss), Race.Protoss, this.SelectedMap));
            }
        }

        public void SetTournamentFilter(Tournament tournament)
        {
            this.SelectedTournament = tournament;

            if (this.SelectedTournament != null) { this.picBxTournament.Image = EloGUIControlsStaticMembers.ImageGetterMethod(this.SelectedTournament); }
            else { this.picBxTournament.Image = null; }

            this.SetResults();

            // here we set the improvedImagecombobox with tournaments to the correct index
            this.haltPublishingTournamentFilterChange = true;

            this.tournamentSelector.TrySetSelectedIndex(this.SelectedTournament);

            this.haltPublishingTournamentFilterChange = false;
        }


    }
}
