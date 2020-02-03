using BrightIdeasSoftware;
using CustomControls;
using CustomExtensionMethods;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public delegate EloData ResourceGetter();
    
    public delegate double Player1EWRGetter(Race player1Race, Race player2Race);

    public delegate int RatingChangeGetter(PlayerSlotType winner, Race player1Race, Race player2Race);

    public partial class MatchReport : UserControl
    {
        internal ImprovedImageComboBox<SCPlayer> ImgCmbBxPlayer1 { get; private set; }
        internal ImprovedImageComboBox<SCPlayer> ImgCmbBxPlayer2 { get; private set; }
        internal ResourceGetter EloDataSource
        {
            private get
            {
                return this.eloDataSource;
            }
            set
            {
                if (this.eloDataSource != null)
                {
                    this.eloDataSource().SeasonPoolChanged -= this.OnSeasonPoolChanged;
                    this.eloDataSource().TournamentPoolChanged -= this.OnTournamentPoolChanged;
                }

                this.eloDataSource = value;

                if (this.player1StatsDisplay != null) { this.player1StatsDisplay.EloDataSource = value; }

                if (this.player2StatsDisplay != null) { this.player2StatsDisplay.EloDataSource = value; }

                if (this.eloDataSource != null)
                {
                    this.eloDataSource().SeasonPoolChanged += this.OnSeasonPoolChanged;
                    this.eloDataSource().TournamentPoolChanged += this.OnTournamentPoolChanged;

                    this.AddTournaments();

                    this.AddRecentMatches();
                }
            }
        }
        private bool racesAreBeingAutoSet;
        private bool suspendMatchReportabilityCheck;
        private List<GameReport> gameReports;
        private MatchContextSelector contextSelector;
        private MatchReportScaffold unfinishedReport;
        private PlayerMatchStatsDisplay player1StatsDisplay;
        private PlayerMatchStatsDisplay player2StatsDisplay;
        private ResourceGetter eloDataSource;
        private ObjectListView oLstVRecentMatches;
        private MatchEditorItem editorMatch;
        private MatchEditorItem EditorMatch
        {
            get
            {
                return this.editorMatch;
            }
            set
            {
                if (value == null) { this.SetEditorObjectsEnabledStatus(false); }
                else if (value != null && this.editorMatch == null) { this.SetEditorObjectsEnabledStatus(true); }

                this.editorMatch = value;
            }
        }
        private PlayerStatsClone player1Stats;
        private PlayerStatsClone player2Stats;
        public EventHandler MatchReported = delegate { };

        public MatchReport()
        {
            InitializeComponent();

            this.player1StatsDisplay = new PlayerMatchStatsDisplay() { Dock = DockStyle.Fill };
            this.player2StatsDisplay = new PlayerMatchStatsDisplay() { Dock = DockStyle.Fill };

            this.contextSelector = new MatchContextSelector(MatchReport.GetTournamentPickerComboBox(t => this.ImageGetter(t.ImageID)), this.cmbBxSeasonPicker);
            this.contextSelector.TournamentSelector.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;
            this.contextSelector.SeasonSelector.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;

            this.tblLOPnlMatchContext.Controls.Add(this.contextSelector.TournamentSelector, 1, 0);

            this.tblLoPnlPlayers.Controls.Add(this.player1StatsDisplay, 0, 3);

            this.tblLoPnlPlayers.Controls.Add(this.player2StatsDisplay, 2, 3);

            this.ImgCmbBxPlayer1 = MatchReport.GetPlayerSelectionComboBox(player => this.ImageGetter(player.ImageID));
            this.ImgCmbBxPlayer1.Margin = new Padding(6, 4, 30, 4);
            this.tblLoPnlPlayers.Controls.Add(this.ImgCmbBxPlayer1, 0, 1);
            this.ImgCmbBxPlayer1.SelectedIndexChanged += this.ImgCmbBxPlayer1_SelectedIndexChanged;
            this.ImgCmbBxPlayer1.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;

            this.ImgCmbBxPlayer2 = MatchReport.GetPlayerSelectionComboBox(player => this.ImageGetter(player.ImageID));
            this.ImgCmbBxPlayer2.Margin = new Padding(30, 4, 6, 4);
            this.tblLoPnlPlayers.Controls.Add(this.ImgCmbBxPlayer2, 2, 1);
            this.ImgCmbBxPlayer2.SelectedIndexChanged += this.ImgCmbBxPlayer2_SelectedIndexChanged;
            this.ImgCmbBxPlayer2.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;

            this.gameReports = new List<GameReport>();

            this.dtpMatchDate.Value = DateTime.Today;
            this.dtpMatchDate.ValueChanged += this.MatchReportabilitySelector_SelectedIndexChanged;

            this.oLstVRecentMatches = EloSystemGUIStaticMembers.CreateMatchListView();
            this.oLstVRecentMatches.SelectedIndexChanged += this.OLstVRecentMatches_SelectedIndexChanged;
            this.tblLoPnlRecentMatches.Controls.Add(this.oLstVRecentMatches, 0, 2);
            this.MatchReported += this.OnMatchReported;
        }

        private static bool ConfirmUsingFutureDate()
        {
            return MessageBox.Show("The date you have entered is in the future. Are you sure you would like to enter this date?", "Date is in the future", MessageBoxButtons.OKCancel) == DialogResult.OK;
        }

        private static bool IsDateInTheFuture(DateTime date)
        {
            return date.Date.CompareTo(DateTime.Now.Date) > 0;
        }

        private static GameReport RetrieveGameReportFromChild(Control child)
        {
            if (child == null) { return null; }

            while (child.Parent != null && !(child.Parent is GameReport)) { child = child.Parent; }

            return child.Parent as GameReport;
        }

        private static ImprovedImageComboBox<SCPlayer> GetPlayerSelectionComboBox(ImageGetter<SCPlayer> imageGetter)
        {
            Func<SCPlayer, bool> HasExtraIdentifiers = player => { return player.IRLName != String.Empty || player.Team != null; };

            return new ImprovedImageComboBox<SCPlayer>()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 260,
                MaxDropDownItems = 18,
                Font = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(3, 2, 3, 2),
                ItemHeight = 22,
                Margin = new Padding(4, 4, 4, 4),
                SelectedItemFont = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(180, 30),
                ImageGetter = imageGetter,
                NameGetter = scPlayer => String.Format("{0} {1}", scPlayer.Name, HasExtraIdentifiers(scPlayer) ? "(" + scPlayer.TeamName + " | " + scPlayer.IRLName + ")" : String.Empty)
            };
        }

        private static ImprovedImageComboBox<Tournament> GetTournamentPickerComboBox(ImageGetter<Tournament> imageGetter)
        {
            return new ImprovedImageComboBox<Tournament>()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 240,
                MaxDropDownItems = 18,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(3, 1, 3, 1),
                ItemHeight = 20,
                Margin = new Padding(4, 4, 4, 4),
                SelectedItemFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(160, 28),
                ImageGetter = imageGetter,
                NameGetter = t => t.Name
            };
        }

        private double CalculatePlayer1EWR(Race player1Race, Race player2Race)
        {
            if (this.player1Stats == null || this.player2Stats == null) { return 0; }

            return EloData.ExpectedWinRatio(this.player1Stats, player1Race, this.player2Stats, player2Race);
        }

        private int CalculateRatingChange(PlayerSlotType winner, Race player1Race, Race player2Race)
        {
            if (this.player1Stats == null || this.player2Stats == null) { return 0; }

            switch (winner)
            {
                case PlayerSlotType.Player1: return EloData.RatingChange(this.player1Stats, player1Race, this.player2Stats, player2Race);
                case PlayerSlotType.Player2: return EloData.RatingChange(this.player2Stats, player2Race, this.player1Stats, player1Race);
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, winner.ToString()));
            }

        }

        private void MatchReportabilitySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdBtnEditNewMatchReport.Checked) { this.UpdateMatchReportability(); }
        }

        private void OLstVRecentMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.rdBtnEditNewMatchReport.Checked) { return; }

            if (this.oLstVRecentMatches.SelectedItem != null && this.oLstVRecentMatches.SelectedItem.RowObject != null)
            {
                this.LoadMatchIntoEditor(this.oLstVRecentMatches.SelectedItem.RowObject as MatchEditorItem);
            }
        }

        private void OnMatchReported(object sender, EventArgs e)
        {
            this.AddRecentMatches();
        }

        private void AddRecentMatches()
        {
            if (this.EloDataSource != null)
            {
                this.oLstVRecentMatches.SetObjects(this.EloDataSource().GetAllGames().OrderByDescending(game => game.Match.DateTime.Date).ThenByDescending(game =>
                    game.Match.DailyIndex).ToMatchEditorItems().Take((int)Settings.Default.NoRecentMatches));
            }

        }

        private void ImgCmbBxPlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlayer = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer;

            if (selectedPlayer == null) { return; }

            this.SetPlayer2Stats(selectedPlayer);

            this.player2StatsDisplay.AddPlayerStats(selectedPlayer);
            this.gameReports.ForEach(gr => gr.Player2 = selectedPlayer);

        }

        private void SetPlayer1Stats(SCPlayer player1)
        {
            if (player1 == null) { return; }

            if (this.rdBtnAddNewMatchReport.Checked) { this.player1Stats = this.EloDataSource().PlayerStatsAtPointInTime(player1, this.dtpMatchDate.Value); }
            else if (this.rdBtnEditNewMatchReport.Checked && !this.EditorMatch.DateWasEdited)
            {
                this.player1Stats = this.EloDataSource().PlayerStatsAtPointInTime(player1, this.dtpMatchDate.Value, this.EditorMatch.SourceMatch.DailyIndex);
            }

        }

        private void SetPlayer2Stats(SCPlayer player2)
        {
            if (player2 == null) { return; }

            if (this.rdBtnAddNewMatchReport.Checked) { this.player2Stats = this.EloDataSource().PlayerStatsAtPointInTime(player2, this.dtpMatchDate.Value); }
            else if (this.rdBtnEditNewMatchReport.Checked && !this.EditorMatch.DateWasEdited)
            {
                this.player2Stats = this.EloDataSource().PlayerStatsAtPointInTime(player2, this.dtpMatchDate.Value, this.EditorMatch.SourceMatch.DailyIndex);
            }

        }

        private void ImgCmbBxPlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlayer = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer;

            if (selectedPlayer == null) { return; }

            this.SetPlayer1Stats(selectedPlayer);

            this.player1StatsDisplay.AddPlayerStats(selectedPlayer);
            this.gameReports.ForEach(gr => gr.Player1 = selectedPlayer);

        }

        private void ArrangeGameReportLocations()
        {
            var nextLocation = new Point(0, this.pnlGameReports.AutoScrollPosition.Y);

            foreach (GameReport game in this.gameReports)
            {
                game.Location = new Point(nextLocation.X, nextLocation.Y);

                nextLocation.Offset(0, game.Size.Height);
            }
        }

        private void SetGameReportTitles()
        {
            const string GAMEREPORT_HEADER_STEM = "Game";

            int gameCounter = 1;

            this.gameReports.ForEach(gameReport => gameReport.Title = String.Format("{0} {1}", GAMEREPORT_HEADER_STEM, gameCounter++));
        }

        private void btnAddGame_Click(object sender, EventArgs e)
        {
            if (this.rdBtnAddNewMatchReport.Checked)
            {
                var nextGameReport = new GameReport(this.CalculatePlayer1EWR, this.CalculateRatingChange)
                {
                    EloDataSource = this.eloDataSource,
                    Player1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer,
                    Player2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer
                };

                this.AddGameReport(nextGameReport);
            }
            else if (this.rdBtnEditNewMatchReport.Checked && this.EditorMatch != null)
            {
                var gameEditor = new GameEntryEditorItem();

                if (this.EditorMatch.GetGames().Any())
                {
                    GameEntryEditorItem lastGame = this.EditorMatch.GetGames().Last();

                    gameEditor.SetRace(PlayerSlotType.Player1, (Race)lastGame.Player1Race);
                    gameEditor.SetRace(PlayerSlotType.Player2, (Race)lastGame.Player2Race);
                }

                this.AddGameReport(new GameReport(gameEditor, this.EditorMatch.Player1, this.EditorMatch.Player2, this.EloDataSource, this.CalculatePlayer1EWR, this.CalculateRatingChange)
                {
                    EloDataSource = this.EloDataSource
                });
            }
        }

        private void AddGameReport(GameReport gameReport)
        {
            gameReport.RemoveButtonClicked += this.NextGameReport_OnRemoveButtonClick;
            gameReport.GameDataReported += this.NextGameReport_GameDataReported;
            gameReport.RaceSelectionChanged += this.PlayerRaceWasSelected;

            this.gameReports.Add(gameReport);

            this.ArrangeGameReports();

            this.pnlGameReports.Controls.Add(gameReport);

            this.UpdateMatchReportability();
        }

        private void PlayerRaceWasSelected(object sender, RaceSelectionEventArgs e)
        {
            if (this.racesAreBeingAutoSet) { return; }

            var gameRepSender = sender as GameReport;

            if (gameRepSender == null) { return; }

            int repIndex = this.gameReports.IndexOf(gameRepSender);

            this.racesAreBeingAutoSet = true;

            foreach (GameReport rep in this.gameReports.Where(rp => !rp.RaceIsSelectedFor(e.Playerslot) || rp.WinnerPlayer == null)) { rep.SetRaceFor(e.Playerslot, e.SelectedRace); }

            this.racesAreBeingAutoSet = false;
        }

        private void NextGameReport_GameDataReported(object sender, EventArgs e)
        {
            this.UpdateMatchReportability();
        }

        private void NextGameReport_OnRemoveButtonClick(object sender, EventArgs e)
        {
            GameReport senderGameReport = MatchReport.RetrieveGameReportFromChild(sender as Control);

            if (senderGameReport == null) { return; }

            if (MessageBox.Show("Would you like to delete this game report?", "Delete game report?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) { return; }

            this.RemoveGameReports(new GameReport[] { senderGameReport });
        }

        private void RemoveGameReports(GameReport[] reportsToRemove)
        {
            this.gameReports = this.gameReports.Where(report => !reportsToRemove.Contains(report)).ToList();

            this.ArrangeGameReports();

            foreach (GameReport report in reportsToRemove) { report.Dispose(); }

            this.UpdateMatchReportability();
        }

        private void UpdateMatchReportability()
        {
            if (this.suspendMatchReportabilityCheck) { return; }

            if (this.rdBtnAddNewMatchReport.Checked)
            {
                this.btnEnterMatchReport.Enabled = this.gameReports.Any() && this.gameReports.All(gameReport =>
                {
                    GameEntry report;

                    GameReportStatus status = gameReport.GetGameReportStatus(out report);

                    return status != GameReportStatus.Failure;
                });
            }
            else if (this.rdBtnEditNewMatchReport.Checked)
            {
                this.btnEnterMatchReport.Enabled = this.EditorMatch != null
                    && (this.dtpMatchDate.Value.Date.CompareTo(this.EditorMatch.DateValue.Date) != 0
                    || this.contextSelector.SelectedTournament != this.EditorMatch.Tournament
                    || this.contextSelector.SelectedSeason != this.EditorMatch.Season
                    || this.ImgCmbBxPlayer1.SelectedValue != this.EditorMatch.Player1
                    || this.ImgCmbBxPlayer2.SelectedValue != this.EditorMatch.Player2
                    || this.gameReports.Count != this.EditorMatch.GetGames().Count()
                    || this.EditorMatch.GetGames().Where((gameEditor, index) => gameEditor.IsDifferentFrom(this.gameReports[index])).Any());
            }
        }

        private void ArrangeGameReports()
        {
            this.SetGameReportTitles();

            this.ArrangeGameReportLocations();
        }

        private DateTime GetNextMatchDateValue(DateTime dateValue)
        {
            return dateValue.Date.AddMilliseconds(this.EloDataSource().GetAllGames().Count(game => game.Match.DateTime.Date == dateValue.Date));
        }

        private void EnterMatchReport(SCPlayer player1, SCPlayer player2, IEnumerable<GameEntry> games, Season season, Tournament tournament, DateTime date)
        {
            if (player1 != null && player2 != null)
            {
                if (season != null) { this.EloDataSource().ReportMatch(player1, player2, games.ToArray(), season, date); }
                else if (tournament != null) { this.EloDataSource().ReportMatch(player1, player2, games.ToArray(), tournament, date); }
                else { this.EloDataSource().ReportMatch(player1, player2, games.ToArray(), date); }

            }
        }

        private void btnEnterMatchReport_Click(object sender, EventArgs e)
        {
            var ply1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer;
            var ply2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer;

            if (MatchReport.IsDateInTheFuture(this.dtpMatchDate.Value) && !MatchReport.ConfirmUsingFutureDate()) { return; }

            if (this.rdBtnAddNewMatchReport.Checked)
            {
                var reports = this.gameReports.Select(rep =>
                {
                    GameEntry report;

                    return new { Status = rep.GetGameReportStatus(out report), GameReport = report };
                }).ToList();


                if (!reports.Any(rep => rep.Status == GameReportStatus.Failure))
                {
                    this.EnterMatchReport(ply1, ply2, reports.Select(item => item.GameReport).ToArray(), this.contextSelector.SelectedSeason, this.contextSelector.SelectedTournament
                        , this.GetNextMatchDateValue(this.dtpMatchDate.Value));

                    this.MatchReported.Invoke(this, new EventArgs());

                    this.PrepareControlsForNextReport();
                }

            }
            else if (this.rdBtnEditNewMatchReport.Checked)
            {
                this.EditorMatch.Tournament = this.contextSelector.SelectedTournament;

                if (this.EditorMatch.Tournament != null && this.contextSelector.SelectedSeason != null)
                {
                    this.EditorMatch.SeasonIndex = this.EditorMatch.Tournament.GetSeasons().IndexOf(this.contextSelector.SelectedSeason);
                }
                else { this.EditorMatch.SeasonIndex = -1; }

                // only if the date has changed will the match be given a new date value because exact time values are used to retain order in matches played on same date
                if (this.EditorMatch.DateValue.Date.CompareTo(this.dtpMatchDate.Value.Date) != 0) { this.EditorMatch.DateValue = this.GetNextMatchDateValue(this.dtpMatchDate.Value); }

                this.EditorMatch.Player1 = ply1;
                this.EditorMatch.Player2 = ply2;

                this.EditorMatch.SetGames(this.gameReports.Select(report => new GameEntryEditorItem(report)));

                this.ReenterAnyChanges();

                this.EditorMatch = null;

                this.oLstVRecentMatches.SelectedItems.Clear();

                this.PrepareControlsForNextReport();

                this.UpdateMatchReportability();
            }
        }

        private void ReenterAnyChanges()
        {
            MatchEditorItem editedMatch = this.oLstVRecentMatches.Items.OfType<OLVListItem>().Select(item => item.RowObject as MatchEditorItem).FirstOrDefault(mEdit => mEdit.HasBeenEdited());

            if (editedMatch == null) { return; }

            if (editedMatch.Season != null)
            {
                this.EloDataSource().ReplaceMatch(editedMatch.SourceMatch, editedMatch.DateValue, editedMatch.Player1, editedMatch.Player2, editedMatch.GetGames().Select(game => new GameEntry(game.WinnerWas, game.Player1Race, game.Player2Race, game.Map)).ToArray(), editedMatch.Season);
            }
            else
            {
                this.EloDataSource().ReplaceMatch(editedMatch.SourceMatch, editedMatch.DateValue, editedMatch.Player1, editedMatch.Player2, editedMatch.GetGames().Select(game => new GameEntry(game.WinnerWas, game.Player1Race, game.Player2Race, game.Map)).ToArray(), editedMatch.Tournament);
            }

            this.MatchReported.Invoke(this, new EventArgs());
        }

        private void PrepareControlsForNextReport()
        {
            this.RemoveGameReports(this.gameReports.ToArray());

            this.ImgCmbBxPlayer1.SelectedIndex = -1;
            this.ImgCmbBxPlayer2.SelectedIndex = -1;
        }

        private void ResetControls()
        {
            this.PrepareControlsForNextReport();

            this.dtpMatchDate.Value = DateTime.Today;

            this.contextSelector.TrySetSelections(null, null);
        }

        private void OnSeasonPoolChanged(object sender, EventArgs e)
        {
            this.UpdateSeasons();
        }

        private void OnTournamentPoolChanged(object sender, EventArgs e)
        {
            this.AddTournaments();
        }

        private void UpdateSeasons()
        {
            if (this.EloDataSource() == null) { return; }

            EloData source = this.EloDataSource();

            if (source == null) { return; }

            this.contextSelector.UpdateSeason();
        }

        private void AddTournaments()
        {
            if (this.EloDataSource() == null) { return; }

            EloData source = this.EloDataSource();

            if (source == null) { return; }

            this.contextSelector.AddItems(source.GetTournaments());
        }

        private Image ImageGetter(int imageID)
        {
            EloImage eloImg;

            if (this.EloDataSource().TryGetImage(imageID, out eloImg)) { return eloImg.Image; }
            else { return null; }
        }

        private void rdBtnMatchReportMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdBtnAddNewMatchReport.Checked) { this.ChangeToAddNewReportModus(); }
            else if (this.rdBtnEditNewMatchReport.Checked) { this.ChangeToEditorModus(); }
        }

        private void ChangeToEditorModus()
        {
            this.btnEnterMatchReport.Text = "Keep &Changes";

            this.lbMatchReportHeader.Text = "Edit Match Report";

            this.EditorMatch = null;

            this.oLstVRecentMatches.SelectedItems.Clear();

            this.oLstVRecentMatches.Cursor = Cursors.Hand;

            this.SaveUnfinishedReport();

            this.ResetControls();

            this.oLstVRecentMatches.FullRowSelect = true;

            this.btnDeleteReport.Visible = true;
        }

        private void ChangeToAddNewReportModus()
        {
            this.btnEnterMatchReport.Text = "Enter &Report";

            this.lbMatchReportHeader.Text = "Match Report";

            this.oLstVRecentMatches.Cursor = Cursors.Default;

            if (this.unfinishedReport != null) { this.ContinueUnfinishedReport(); }

            this.SetEditorObjectsEnabledStatus(true);

            this.btnAddGame.Enabled = true;
            this.btnDeleteReport.Visible = false;
            this.btnEditMatchIndex.Enabled = false;

            this.oLstVRecentMatches.FullRowSelect = false;
        }

        private void SaveUnfinishedReport()
        {
            this.unfinishedReport = new MatchReportScaffold(this.gameReports);

            this.gameReports.Clear();
            this.pnlGameReports.Controls.Clear();

            this.unfinishedReport.Tournament = this.contextSelector.SelectedTournament;
            this.unfinishedReport.Season = this.contextSelector.SelectedSeason;
            this.unfinishedReport.Date = this.dtpMatchDate.Value;
            this.unfinishedReport.Player1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer;
            this.unfinishedReport.Player2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer;
        }

        private void ContinueUnfinishedReport()
        {
            this.ResetControls();

            if (this.unfinishedReport == null) { return; }

            this.contextSelector.TrySetSelections(this.unfinishedReport.Tournament, this.unfinishedReport.Season);

            this.dtpMatchDate.Value = this.unfinishedReport.Date;

            this.ImgCmbBxPlayer1.TrySetSelectedIndex(this.unfinishedReport.Player1);
            this.ImgCmbBxPlayer2.TrySetSelectedIndex(this.unfinishedReport.Player2);

            foreach (GameReport gameReport in this.unfinishedReport.GetGameReports()) { this.AddGameReport(gameReport); }

            this.unfinishedReport = null;
        }

        private void LoadMatchIntoEditor(MatchEditorItem item)
        {
            this.suspendMatchReportabilityCheck = true;

            this.btnAddGame.Enabled = true;

            this.EditorMatch = item;

            this.ResetControls();

            this.contextSelector.TrySetSelections(item.Tournament, item.Season);

            this.dtpMatchDate.Value = item.DateValue;

            this.ImgCmbBxPlayer1.TrySetSelectedIndex(item.Player1);
            this.ImgCmbBxPlayer2.TrySetSelectedIndex(item.Player2);

            foreach (GameEntryEditorItem game in item.GetGames())
            {
                this.AddGameReport(new GameReport(game, item.Player1, item.Player2, this.EloDataSource, this.CalculatePlayer1EWR, this.CalculateRatingChange) { EloDataSource = this.EloDataSource });
            }

            this.suspendMatchReportabilityCheck = false;

            this.UpdateMatchReportability();
        }

        private void dtpMatchDate_ValueChanged(object sender, EventArgs e)
        {
            this.SetPlayer1Stats(this.ImgCmbBxPlayer1.SelectedValue as SCPlayer);

            this.SetPlayer2Stats(this.ImgCmbBxPlayer2.SelectedValue as SCPlayer);

            this.gameReports.ForEach(gameRep => gameRep.UpdateControlValues());
        }

        private void SetEditorObjectsEnabledStatus(bool enabledStatus)
        {
            this.tblLOPnlMatchContext.Enabled = enabledStatus;
            this.tblLoPnlPlayers.Enabled = enabledStatus;
            this.pnlGameReports.Enabled = enabledStatus;
            this.btnDeleteReport.Enabled = enabledStatus;
            this.btnEditMatchIndex.Enabled = enabledStatus;
        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to delete the match from the database. Are you sure you would like to continue?", "Delete match?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                this.EloDataSource().RollBackMatch(this.EditorMatch.SourceMatch);

                this.AddRecentMatches();

                this.EditorMatch = null;

                this.oLstVRecentMatches.SelectedItems.Clear();
            }
        }

        private void btnEditMatchIndex_Click(object sender, EventArgs e)
        {
            MatchEditorItem[] matchesForThisDate = this.EloDataSource().GetAllGames().Where(game => game.Match.DateTime.Date.Equals(this.EditorMatch.SourceMatch.DateTime.Date)).GroupBy(game => game.Match).Select(grp => new MatchEditorItem(grp, grp.Key)).OrderByDescending(match => match.SourceMatch.DailyIndex).ToArray();

            var editorForm = new DailyIndexEditorForm(matchesForThisDate, matchesForThisDate.TakeWhile(match => !match.SourceMatch.Equals(this.EditorMatch.SourceMatch)).Count());

            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                this.EloDataSource().ChangeDailyIndex(this.editorMatch.SourceMatch, editorForm.IndexChanges);

                this.AddRecentMatches();

                this.EditorMatch = null;

                this.oLstVRecentMatches.SelectedItems.Clear();
            }
        }
    }
}
