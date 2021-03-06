﻿using BrightIdeasSoftware;
using CustomControls;
using CustomControls.Styles;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace SCEloSystemGUI
{
    public partial class TournamentProfile : Form
    {
        private GameFilter<Matchup> matchupFiltering;
        private GameByPlayerFilter participantFiltering;
        private List<IGameFilter> gameFilters;
        private ObjectListView gameListView;
        private ObjectListView matchListView;
        private PageSelecterLinker selectLinkerGames;
        private PageSelecterLinker selectLinkerMatches;
        private Season seasonFilter
        {
            get
            {
                var selItem = this.cmbBxSeasonSelecter.SelectedItem as Tuple<string, Season>;

                return selItem != null ? selItem.Item2 : null;
            }
        }
        private Season seasonFilterApplied;
        private Tournament tournament;

        private TournamentProfile(Tournament tournament)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;
            this.tournament = tournament;

            this.lbTitle.Text = this.tournament.NameLong;
            this.lbNameLong.Text = this.tournament.NameLong;
            this.lbNameShort.Text = this.tournament.Name;
            this.lbTotalGamesCount.Text = this.tournament.GetGames().Count().ToString(Styles.NUMBER_FORMAT);

            this.matchupFiltering = EloGUIControlsStaticMembers.CreateMatchupDrivenGameFilter();
            this.matchupFiltering.FilterChanged += this.MatchupFiltering_FilterChanged;
            this.tblLOPnlGameFilters.Controls.Add(this.matchupFiltering, 0, 1);

            this.participantFiltering = new GameByPlayerFilter() { Dock = DockStyle.Fill, Margin = new Padding(6, 4, 4, 4) };
            this.participantFiltering.FilterChanged += this.MatchupFiltering_FilterChanged;
            this.tblLOPnlGameFilters.Controls.Add(this.participantFiltering, 1, 1);
            this.tblLOPnlGameFilters.SetColumnSpan(this.participantFiltering, 2);

            this.gameFilters = new List<IGameFilter>() { this.matchupFiltering, this.participantFiltering };

            this.gameListView = TournamentProfile.CreateGameListView();
            this.tblLOPnlGames.Controls.Add(this.gameListView, 0, 1);
            this.selectLinkerGames = new PageSelecterLinker(this.gameListView) { ItemsPerPage = (int)Settings.Default.MatchesPerPage };
            this.selectLinkerGames.ItemGetter = () =>
            {
                return this.tournament.GetGames().Where(game => (this.seasonFilterApplied == null || game.Season == this.seasonFilterApplied)
                    && this.gameFilters.All(filter => filter.FilterGame(game))).OrderNewestFirst();
            };
            this.tblLOPnlGames.Controls.Add(this.selectLinkerGames.Selecter, 0, 0);
            Styles.PageSelecterStyles.SetSpaceStyle(this.selectLinkerGames.Selecter);

            this.matchListView = TournamentProfile.CreateMatchListView();
            this.tblLOPnlMatches.Controls.Add(this.matchListView, 0, 1);
            this.selectLinkerMatches = new PageSelecterLinker(this.matchListView) { ItemsPerPage = (int)Settings.Default.MatchesPerPage };
            this.selectLinkerMatches.ItemGetter = () =>
            {
                return this.tournament.GetGames().Where(game => (this.seasonFilterApplied == null || game.Season == this.seasonFilterApplied)
                  && this.gameFilters.All(filter => filter.FilterGame(game))).ToMatchEditorItems().OrderNewestFirst();
            };
            this.tblLOPnlMatches.Controls.Add(this.selectLinkerMatches.Selecter, 0, 0);
            Styles.PageSelecterStyles.SetSpaceStyle(this.selectLinkerMatches.Selecter);

            EloImage eloLogo;

            if (GlobalState.DataBase.TryGetImage(tournament.ImageID, out eloLogo)) { this.picBxLogo.Image = eloLogo.Image.ResizeSARWithinBounds(this.picBxLogo.Width, this.picBxLogo.Height); }

            this.FillSeasonSelecter();
            this.ApplyFilter();

            if (Settings.Default.PlayerResultDisplayTypes == ResultsDisplay.Games) { this.tabControlResults.SelectedTab = this.tabPageGames; }
            else { this.tabControlResults.SelectedTab = this.tabPageMatches; }

        }

        private void MatchupFiltering_FilterChanged(object sender, EventArgs e)
        {
            this.SetBtnFilterApplyEnabledStatus();
        }

        public static void ShowProfile(Tournament tournament, Form anchorForm = null)
        {
            System.Windows.Forms.Cursor previousCursor = System.Windows.Forms.Cursor.Current;

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            var profileDisplay = new TournamentProfile(tournament);

            System.Windows.Forms.Cursor.Current = previousCursor;

            if (anchorForm != null) { FormStyles.ShowFullFormRelativeToAnchor(profileDisplay, anchorForm); }

            profileDisplay.ShowDialog();

            profileDisplay.Dispose();
        }

        private void FillSeasonSelecter()
        {
            List<Season> seasonList = this.tournament.GetSeasons().OrderBy(season => season.Name).ToList();

            var selectedItem = this.cmbBxSeasonSelecter.SelectedItem != null ? (this.cmbBxSeasonSelecter.SelectedItem as Tuple<string, Season>).Item2 : null;

            this.cmbBxSeasonSelecter.DisplayMember = "Item1";
            this.cmbBxSeasonSelecter.ValueMember = "Item2";

            this.cmbBxSeasonSelecter.Items.Clear();
            this.cmbBxSeasonSelecter.Items.AddRange((new Tuple<string, Season>[] { Tuple.Create<string, Season>("<any>", null) }).Concat(seasonList.Select(season =>
                Tuple.Create<string, Season>(season.Name, season))).ToArray());

            this.cmbBxSeasonSelecter.SelectedIndex = 0;
        }

        private void TournamentProfile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void SetBtnFilterApplyEnabledStatus()
        {
            this.btnApplyFilters.Enabled = this.seasonFilterApplied != this.seasonFilter || this.gameFilters.Any(filter => filter.HasChangesNotApplied());
        }

        private void ApplyFilter()
        {
            this.seasonFilterApplied = this.seasonFilter;
            this.gameFilters.ForEach(filter => filter.ApplyChanges());

            this.selectLinkerGames.UpdateListItems();
            this.selectLinkerMatches.UpdateListItems();

            this.SetBtnFilterApplyEnabledStatus();
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            this.ApplyFilter();
        }

        private void cmbBxSeasonSelecter_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<SCPlayer> participants = this.seasonFilter == null ? this.tournament.GetGames().SelectMany(game => new SCPlayer[] { game.Player1, game.Player2 })
                : this.seasonFilter.GetMatches().SelectMany(match => new SCPlayer[] { match.Player1, match.Player2 });

            this.participantFiltering.SetItems(participants.Distinct().OrderBy(p => p.Name).ThenBy(p => p.IRLName));

            this.SetBtnFilterApplyEnabledStatus();
        }

        private static ObjectListView CreateGameListView()
        {
            const int ROW_HEIGHT = 20;

            var gamesLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                Dock = DockStyle.Fill,
                EmptyListMsg = "No games were found.",
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = ROW_HEIGHT,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(935, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            Styles.ObjectListViewStyles.SetHotItemStyle(gamesLV);
            Styles.ObjectListViewStyles.DeselectItemsOnMousUp(gamesLV);

            const int RACE_COLUMN_WIDTH = 50;
            const int RATING_CHANGE_COLUMN_WIDTH = 55;
            const int NAME_COLUMN_WIDTH = 100;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 80, Text = "Date" };
            var olvClmPlayer1Race = new OLVColumn() { Width = RACE_COLUMN_WIDTH, Text = "Race", ToolTipText = "Player 1's race" };
            var olvClmPlayer1Name = new OLVColumn() { Width = NAME_COLUMN_WIDTH, Text = "Name", ToolTipText = "Player 1's name" };
            var olvClmPlayer1RatingChange = new OLVColumn() { Width = RATING_CHANGE_COLUMN_WIDTH, Text = "Rating", ToolTipText = "Change in player 1's rating score" };
            var olvClmResult = new OLVColumn() { Width = 40, Text = "Result" };
            var olvClmPlayer2RatingChange = new OLVColumn() { Width = RATING_CHANGE_COLUMN_WIDTH, Text = "Rating", ToolTipText = "Change in player 2's rating score" };
            var olvClmPlayer2Name = new OLVColumn() { Width = NAME_COLUMN_WIDTH, Text = "Name", ToolTipText = "Player 2s name" };
            var olvClmPlayer2Race = new OLVColumn() { Width = RACE_COLUMN_WIDTH, Text = "Race", ToolTipText = "Player 2's race" };
            var olvClmMap = new OLVColumn() { Width = 150, Text = "Map" };
            var olvClmSeason = new OLVColumn() { Width = 130, Text = "Season" };

            gamesLV.FormatCell += TournamentProfile.GamesLV_FormatCell;
            gamesLV.MouseClick += TournamentProfile.GamesLV_MouseClick;

            gamesLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmPlayer1Race, olvClmPlayer1Name, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange
                , olvClmPlayer2Name,olvClmPlayer2Race ,olvClmMap, olvClmSeason });

            gamesLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmPlayer1Race, olvClmPlayer1Name, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange, olvClmPlayer2Name
                , olvClmPlayer2Race, olvClmMap, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmPlayer1Race, olvClmPlayer1RatingChange, olvClmResult, olvClmPlayer2RatingChange, olvClmPlayer2Race })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmDate.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Match.DateTime.ToShortDateString(); }
                else { return string.Empty; }
            };

            const int RACE_IMAGE_HEIGHT_MAX = ROW_HEIGHT - 2;
            const int RACE_IMAGE_WIDTH_MAX = RACE_COLUMN_WIDTH;

            olvClmPlayer1Race.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return new Image[] { RaceIconProvider.GetRaceBitmap(game.Player1Race).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX) }; }
                else { return null; }
            };

            olvClmPlayer1Name.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Player1.Name; }
                else { return "N/A"; }
            };

            olvClmPlayer1RatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return Styles.StringStyles.ConvertRatingChangeString((game.RatingChange * (game.Player1.Equals(game.Winner) ? 1 : -1)).ToString()); }
                else { return ""; }
            };

            olvClmResult.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return String.Format("{0}", game.Winner.Equals(game.Player1) ? "1 - 0" : "0 - 1"); }
                else { return ""; }
            };

            var raceRenderer = new ImageRenderer() { Bounds = new Rectangle(6, 1, 6, 6) };
            olvClmPlayer1Race.Renderer = raceRenderer;
            olvClmPlayer2Race.Renderer = raceRenderer;

            olvClmPlayer2RatingChange.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return Styles.StringStyles.ConvertRatingChangeString((game.RatingChange * (game.Player2.Equals(game.Winner) ? 1 : -1)).ToString()); }
                else { return ""; }
            };

            olvClmPlayer2Name.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return game.Player2.Name; }
                else { return "N/A"; }
            };

            olvClmPlayer2Race.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null) { return new Image[] { RaceIconProvider.GetRaceBitmap(game.Player2Race).ResizeSARWithinBounds(RACE_IMAGE_WIDTH_MAX, RACE_IMAGE_HEIGHT_MAX) }; }
                else { return null; }
            };

            olvClmMap.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null && game.Map != null) { return game.Map.Name; }
                else { return "N/A"; }
            };

            olvClmSeason.AspectGetter = obj =>
            {
                var game = obj as Game;

                if (game != null && game.Season != null) { return game.Season.Name; }
                else { return ""; }
            };

            return gamesLV;
        }

        internal static ObjectListView CreateMatchListView()
        {
            var matchLV = new ObjectListView()
            {
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                EmptyListMsg = "No matches were found.",
                Font = new Font("Calibri", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FullRowSelect = false,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = false,
                RowHeight = 20,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(654, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 90, Text = "Date" };
            var olvClmPlayer1 = new OLVColumn() { Width = 130, Text = "Player 1" };
            var olvClmRatingChangePlayer1 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmResult = new OLVColumn() { Width = 70, Text = "Result" };
            var olvClmRatingChangePlayer2 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmPlayer2 = new OLVColumn() { Width = 130, Text = "Player 2" };
            var olvClmSeason = new OLVColumn() { Width = 110, Text = "Season" };

            matchLV.FormatCell += TournamentProfile.MatchLV_FormatCell;
            matchLV.MouseClick += TournamentProfile.MatchLV_MouseClick;

            Styles.ObjectListViewStyles.SetHotItemStyle(matchLV);
            Styles.ObjectListViewStyles.DeselectItemsOnMousUp(matchLV);

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmSeason });

            foreach (OLVColumn clm in new OLVColumn[] { olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2 })
            {
                clm.HeaderTextAlign = HorizontalAlignment.Center;
                clm.TextAlign = HorizontalAlignment.Center;
            }

            olvClmPlayer1.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player1.Name; }
                else { return ""; }
            };

            olvClmDate.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.DateValue.ToShortDateString(); }
                else { return string.Empty; }
            };

            olvClmRatingChangePlayer1.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return Styles.StringStyles.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player1)); }
                else { return ""; }
            };

            olvClmResult.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return String.Format("{0} - {1}", match.WinsBy(PlayerSlotType.Player1), match.WinsBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmRatingChangePlayer2.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return Styles.StringStyles.ConvertRatingChangeString(match.RatingChangeBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmPlayer2.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player2.Name; }
                else { return ""; }
            };

            olvClmSeason.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null && match.Season != null) { return match.Season.Name; }
                else { return ""; }
            };

            return matchLV;
        }

        private static void MatchLV_MouseClick(object sender, MouseEventArgs e)
        {
            var olv = sender as ObjectListView;

            if (olv == null || olv.SelectedItem == null) { return; }

            var selectedMatchItem = olv.SelectedItem.RowObject as MatchEditorItem;

            if (selectedMatchItem != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: PlayerProfile.ShowProfile(selectedMatchItem.Player1, olv.FindForm()); break;
                    case MouseButtons.Right: PlayerProfile.ShowProfile(selectedMatchItem.Player2, olv.FindForm()); break;
                    case MouseButtons.Middle: PlayerProfile.ShowProfile(selectedMatchItem.Player1, selectedMatchItem.Player2, olv.FindForm()); break;
                    case MouseButtons.None:
                    case MouseButtons.XButton1:
                    case MouseButtons.XButton2: break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MouseButtons).Name, e.Button.ToString()));
                }
            }

            olv.SelectedItems.Clear();

        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5) { EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem); }
        }

        private static void GamesLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);

                EloSystemGUIStaticMembers.FormatRatingChangeOLVCell(e.SubItem);
            }
        }

        private static void GamesLV_MouseClick(object sender, MouseEventArgs e)
        {
            var olv = sender as ObjectListView;

            if (olv == null || olv.SelectedItem == null) { return; }

            var selectedGame = olv.SelectedItem.RowObject as Game;

            if (selectedGame != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: PlayerProfile.ShowProfile(selectedGame.Player1, olv.FindForm()); break;
                    case MouseButtons.Right: PlayerProfile.ShowProfile(selectedGame.Player2, olv.FindForm()); break;
                    case MouseButtons.Middle: PlayerProfile.ShowProfile(selectedGame.Player1, selectedGame.Player2, olv.FindForm()); break;
                    case MouseButtons.None:
                    case MouseButtons.XButton1:
                    case MouseButtons.XButton2: break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MouseButtons).Name, e.Button.ToString()));
                }
            }

            olv.SelectedItems.Clear();
        }

        private void tabControlGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlResults.SelectedTab == this.tabPageMatches) { Settings.Default.PlayerResultDisplayTypes = ResultsDisplay.Matches; }
            else if (this.tabControlResults.SelectedTab == this.tabPageGames) { Settings.Default.PlayerResultDisplayTypes = ResultsDisplay.Games; }
        }
    }
}
