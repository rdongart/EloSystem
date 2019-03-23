using SCEloSystemGUI.Properties;
using EloSystem;
using EloSystem.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CustomControls;
using BrightIdeasSoftware;
using CustomExtensionMethods;

namespace SCEloSystemGUI.UserControls
{
    public delegate EloData ResourceGetter();

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
        public EventHandler MatchReported = delegate { };

        public MatchReport()
        {
            InitializeComponent();

            this.player1StatsDisplay = new PlayerMatchStatsDisplay();
            this.player2StatsDisplay = new PlayerMatchStatsDisplay();

            this.contextSelector = new MatchContextSelector(MatchReport.GetTournamentPickerComboBox(t => this.ImageGetter(t.ImageID)), this.cmbBxSeasonPicker);
            this.contextSelector.TournamentSelector.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;
            this.contextSelector.SeasonSelector.SelectedIndexChanged += this.MatchReportabilitySelector_SelectedIndexChanged;

            this.tblLOPnlMatchContext.Controls.Add(this.contextSelector.TournamentSelector, 1, 0);

            this.player1StatsDisplay.Dock = DockStyle.Fill;
            this.tblLoPnlPlayers.Controls.Add(this.player1StatsDisplay, 0, 3);

            this.player2StatsDisplay.Dock = DockStyle.Fill;
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

            this.oLstVRecentMatches = MatchReport.CreateMatchListView();
            this.oLstVRecentMatches.SelectedIndexChanged += this.OLstVRecentMatches_SelectedIndexChanged;
            this.tblLoPnlRecentMatches.Controls.Add(this.oLstVRecentMatches, 0, 2);
            this.MatchReported += this.OnMatchReported;
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

        private static ObjectListView CreateMatchListView()
        {
            var matchLV = new ObjectListView()
            {
                AlternateRowBackColor = Color.FromArgb(217, 217, 217),
                Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(6),
                MultiSelect = false,
                RowHeight = 22,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(784, 850),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = true,
            };

            matchLV.FormatCell += MatchReport.MatchLV_FormatCell;

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmDate = new OLVColumn() { Width = 90, Text = "Date" };
            var olvClmPlayer1 = new OLVColumn() { Width = 130, Text = "Player 1" };
            var olvClmRatingChangePlayer1 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmResult = new OLVColumn() { Width = 70, Text = "Result" };
            var olvClmRatingChangePlayer2 = new OLVColumn() { Width = 50, Text = "Rating Change" };
            var olvClmPlayer2 = new OLVColumn() { Width = 130, Text = "Player 2" };
            var olvClmTournament = new OLVColumn() { Width = 130, Text = "Tournament" };
            var olvClmSeason = new OLVColumn() { Width = 110, Text = "Season" };

            matchLV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            matchLV.Size = new Size(784, 850);
            matchLV.HasCollapsibleGroups = false;
            matchLV.ShowGroups = false;
            matchLV.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            matchLV.RowHeight = 22;
            matchLV.UseCellFormatEvents = true;
            matchLV.FormatCell += MatchReport.MatchLV_FormatCell;
            matchLV.Scrollable = true;
            matchLV.Margin = new Padding(3);
            matchLV.AlternateRowBackColor = Color.FromArgb(217, 217, 217);
            matchLV.UseAlternatingBackColors = true; matchLV.MultiSelect = false;

            matchLV.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

            matchLV.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmDate, olvClmPlayer1, olvClmRatingChangePlayer1, olvClmResult, olvClmRatingChangePlayer2, olvClmPlayer2, olvClmTournament, olvClmSeason });

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

                if (match != null) { return MatchReport.ConvertRatingString(match.RatingChangeBy(PlayerSlotType.Player1)); }
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

                if (match != null) { return MatchReport.ConvertRatingString(match.RatingChangeBy(PlayerSlotType.Player2)); }
                else { return ""; }
            };

            olvClmPlayer2.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null) { return match.Player2.Name; }
                else { return ""; }
            };

            olvClmTournament.AspectGetter = obj =>
            {
                var match = obj as MatchEditorItem;

                if (match != null && match.Tournament != null) { return match.Tournament.Name; }
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

        private static string ConvertRatingString(string ratingChangeTxt)
        {
            int ratingChangeValue = 0;

            bool hasRatingValue = int.TryParse(ratingChangeTxt, out ratingChangeValue);

            return String.Format("{0}{1}", ratingChangeValue > 0 ? "+" : "", hasRatingValue ? ratingChangeValue.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT) : ratingChangeTxt);
        }

        private static void MatchLV_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {
                int cellValue;

                if (int.TryParse(e.SubItem.Text, out cellValue))
                {
                    if (cellValue < 0) { e.SubItem.ForeColor = Color.Red; }
                    else if (cellValue > 0) { e.SubItem.ForeColor = Color.ForestGreen; }
                    else
                    {
                        e.SubItem.ForeColor = SystemColors.ControlText;
                    }
                }
            }
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

        private void OnMatchReported(object sender, EventArgs e)
        {
            this.AddRecentMatches();
        }

        private void AddRecentMatches()
        {
            if (this.EloDataSource != null)
            {
                this.oLstVRecentMatches.SetObjects(this.EloDataSource().GetAllGames().OrderByDescending(game => game.Match.Date).GroupBy(game => game.Match).Select(grp =>
                    new MatchEditorItem(grp, grp.Key)).Take((int)Settings.Default.NoRecentMatches));
            }

        }

        private void ImgCmbBxPlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.player2StatsDisplay.AddPlayerStats(this.ImgCmbBxPlayer2.SelectedValue as SCPlayer);
            this.gameReports.ForEach(gr => gr.Player2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer);
        }

        private void ImgCmbBxPlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.player1StatsDisplay.AddPlayerStats(this.ImgCmbBxPlayer1.SelectedValue as SCPlayer);
            this.gameReports.ForEach(gr => gr.Player1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer);
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
                var nextGameReport = new GameReport()
                {
                    EloDataSource = this.eloDataSource,
                    Player1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer,
                    Player2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer
                };

                this.AddGameReport(nextGameReport);
            }
            else if (this.rdBtnEditNewMatchReport.Checked && this.editorMatch != null)
            {
                var gameEditor = new GameEntryEditorItem();

                if (this.editorMatch.GetGames().Any())
                {
                    GameEntryEditorItem lastGame = this.editorMatch.GetGames().Last();

                    gameEditor.SetRace(PlayerSlotType.Player1, (Race)lastGame.Player1Race);
                    gameEditor.SetRace(PlayerSlotType.Player2, (Race)lastGame.Player2Race);
                }

                this.AddGameReport(new GameReport(gameEditor, this.editorMatch.Player1, this.editorMatch.Player2, this.EloDataSource) { EloDataSource = this.EloDataSource });
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
            else if (this.rdBtnEditNewMatchReport.Checked && this.editorMatch != null)
            {
                this.btnEnterMatchReport.Enabled = this.dtpMatchDate.Value.Date.CompareTo(this.editorMatch.DateValue.Date) != 0
                    || this.contextSelector.SelectedTournament != this.editorMatch.Tournament
                    || this.contextSelector.SelectedSeason != this.editorMatch.Season
                    || this.ImgCmbBxPlayer1.SelectedValue != this.editorMatch.Player1
                    || this.ImgCmbBxPlayer2.SelectedValue != this.editorMatch.Player2
                    || this.gameReports.Count != this.editorMatch.GetGames().Count()
                    || this.editorMatch.GetGames().Where((gameEditor, index) => gameEditor.IsDifferentFrom(this.gameReports[index])).Any();
            }
        }

        private void ArrangeGameReports()
        {
            this.SetGameReportTitles();

            this.ArrangeGameReportLocations();
        }

        private DateTime GetNextMatchDateValue(DateTime dateValue)
        {
            return dateValue.Date.AddMilliseconds(this.EloDataSource().GetAllGames().Count(game => game.Match.Date.Date == dateValue.Date));
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
                this.editorMatch.Tournament = this.contextSelector.SelectedTournament;
                this.editorMatch.SeasonIndex = this.contextSelector.SeasonSelector.SelectedIndex;

                // only if the date has changed will the match be given a new date value because exact time values are used to retain order in matches played on same date
                if (this.editorMatch.DateValue.Date.CompareTo(this.dtpMatchDate.Value.Date) != 0) { this.editorMatch.DateValue = this.GetNextMatchDateValue(this.dtpMatchDate.Value); }

                this.editorMatch.Player1 = ply1;
                this.editorMatch.Player2 = ply2;

                this.editorMatch.SetGames(this.gameReports.Select(report => new GameEntryEditorItem(report)));

                this.ReenterAnyChanges();

                this.UpdateMatchReportability();
            }
        }

        private void ReenterAnyChanges()
        {
            IEnumerable<MatchEditorItem> matches = this.oLstVRecentMatches.Items.OfType<OLVListItem>().Select(item => item.RowObject as MatchEditorItem);

            if (matches.Where(match => match.HasBeenEdited()).IsEmpty()) { return; }

            var lastEditedItem = matches.Last(item => item.HasBeenEdited());

            List<MatchEditorItem> matchesToReenter = matches.TakeWhile(item => item.DateValue.CompareTo(lastEditedItem.DateValue) > -1).ToList();

            this.EloDataSource().RollBackLastMatches(matchesToReenter.Count);

            matchesToReenter.Reverse();

            IEnumerator<MatchEditorItem> eMatchesToReenter = matchesToReenter.GetEnumerator();

            while (eMatchesToReenter.MoveNext())
            {
                MatchEditorItem match = eMatchesToReenter.Current;

                this.EnterMatchReport(match.Player1, match.Player2, match.GetGames().Select(game => new GameEntry(game.WinnerWas, game.Player1Race, game.Player2Race, game.Map)), match.Season, match.Tournament
                    , match.DateWasEdited ? this.GetNextMatchDateValue(match.DateValue) : match.DateValue);
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

            this.oLstVRecentMatches.Cursor = Cursors.Hand;

            this.SaveUnfinishedReport();

            this.ResetControls();

            this.btnAddGame.Enabled = false;

            this.oLstVRecentMatches.FullRowSelect = true;
        }

        private void ChangeToAddNewReportModus()
        {
            this.btnEnterMatchReport.Text = "Enter &Report";

            this.lbMatchReportHeader.Text = "Match Report";

            this.oLstVRecentMatches.Cursor = Cursors.Default;

            if (this.unfinishedReport != null) { this.ContinueUnfinishedReport(); }

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

            this.editorMatch = item;

            this.ResetControls();

            this.contextSelector.TrySetSelections(item.Tournament, item.Season);

            this.dtpMatchDate.Value = item.DateValue;

            this.ImgCmbBxPlayer1.TrySetSelectedIndex(item.Player1);
            this.ImgCmbBxPlayer2.TrySetSelectedIndex(item.Player2);

            foreach (GameEntryEditorItem game in item.GetGames()) { this.AddGameReport(new GameReport(game, item.Player1, item.Player2, this.EloDataSource) { EloDataSource = this.EloDataSource }); }

            this.suspendMatchReportabilityCheck = false;

            this.UpdateMatchReportability();
        }

        private void btnRollBackLastMatch_Click(object sender, EventArgs e)
        {
            this.EloDataSource().RollBackLastMatches(1);

            this.AddRecentMatches();

            this.ImgCmbBxPlayer1.AddItems(this.EloDataSource().GetPlayers().ToArray(), false);
            this.ImgCmbBxPlayer2.AddItems(this.EloDataSource().GetPlayers().ToArray(), false);
        }
    }
}
