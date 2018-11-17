using System.Linq;
using CustomControls;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EloSystem.ResourceManagement;

namespace SCEloSystemGUI.UserControls
{
    public delegate EloData ResourceGetter();

    public partial class MatchReport : UserControl
    {
        internal ImageComboBox ImgCmbBxPlayer1 { get; private set; }
        internal ImageComboBox ImgCmbBxPlayer2 { get; private set; }
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
                }
            }
        }
        private bool racesAreBeingAutoSet;
        private List<GameReport> gameReports;
        private MatchContextSelector contextSelector;
        private PlayerMatchStatsDisplay player1StatsDisplay;
        private PlayerMatchStatsDisplay player2StatsDisplay;
        private ResourceGetter eloDataSource;
        public EventHandler MatchReported = delegate { };

        public MatchReport()
        {
            InitializeComponent();

            this.player1StatsDisplay = new PlayerMatchStatsDisplay() { Margin = new Padding(3) };
            this.player2StatsDisplay = new PlayerMatchStatsDisplay() { Margin = new Padding(58, 3, 3, 3) };

            this.contextSelector = new MatchContextSelector(MatchReport.GetTournamentPickerComboBox(), this.cmbBxSeasonPicker);

            this.tblLOPnlMatchContext.Controls.Add(this.contextSelector.TournamentSelector, 1, 0);

            this.tblLOPnlMatchReport.SetRowSpan(this.player1StatsDisplay, 7);
            this.tblLOPnlMatchReport.SetColumnSpan(this.player1StatsDisplay, 5);
            this.player1StatsDisplay.Dock = DockStyle.Fill;
            this.tblLOPnlMatchReport.Controls.Add(this.player1StatsDisplay, 0, 6);

            this.tblLOPnlMatchReport.SetRowSpan(this.player2StatsDisplay, 7);
            this.tblLOPnlMatchReport.SetColumnSpan(this.player2StatsDisplay, 5);
            this.player2StatsDisplay.Dock = DockStyle.Fill;
            this.tblLOPnlMatchReport.Controls.Add(this.player2StatsDisplay, 5, 6);

            this.ImgCmbBxPlayer1 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlMatchReport.Controls.Add(this.ImgCmbBxPlayer1, 1, 4);
            this.tblLOPnlMatchReport.SetColumnSpan(this.ImgCmbBxPlayer1, 4);
            this.ImgCmbBxPlayer1.SelectedIndexChanged += this.ImgCmbBxPlayer1_SelectedIndexChanged;

            this.ImgCmbBxPlayer2 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlMatchReport.Controls.Add(this.ImgCmbBxPlayer2, 6, 4);
            this.tblLOPnlMatchReport.SetColumnSpan(this.ImgCmbBxPlayer2, 4);
            this.ImgCmbBxPlayer2.SelectedIndexChanged += this.ImgCmbBxPlayer2_SelectedIndexChanged;

            this.gameReports = new List<GameReport>();

            this.dtpMatchDate.Value = DateTime.Today;
        }

        private static GameReport RetrieveGameReportFromChild(Control child)
        {
            if (child == null) { return null; }

            while (child.Parent != null && !(child.Parent is GameReport)) { child = child.Parent; }

            return child.Parent as GameReport;
        }

        private static ImageComboBox GetPlayerSelectionComboBox()
        {
            return new ImageComboBox()
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
            };
        }

        private static ImageComboBoxImprovedItemHandling GetTournamentPickerComboBox()
        {
            return new ImageComboBoxImprovedItemHandling()
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
            };
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
            var nextGameReport = new GameReport()
            {
                EloDataSource = this.eloDataSource,
                Player1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer,
                Player2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer
            };

            nextGameReport.RemoveButtonClicked += this.NextGameReport_OnRemoveButtonClick;
            nextGameReport.GameDataReported += this.NextGameReport_GameDataReported;
            nextGameReport.RaceSelectionChanged += this.PlayerRaceWasSelected;

            this.gameReports.Add(nextGameReport);

            this.ArrangeGameReports();

            this.pnlGameReports.Controls.Add(nextGameReport);

            this.UpdateMatchReportability();
        }

        private void PlayerRaceWasSelected(object sender, RaceSelectionEventArgs e)
        {
            if (this.racesAreBeingAutoSet) { return; }

            var gameRepSender = sender as GameReport;

            if (gameRepSender == null) { return; }

            int repIndex = this.gameReports.IndexOf(gameRepSender);

            this.racesAreBeingAutoSet = true;

            foreach (GameReport rep in this.gameReports.Where(rp => !rp.RaceIsSelectedFor(e.Playerslot))) { rep.SetRaceFor(e.Playerslot, e.SelectedRace); }

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
            this.btnEnterMatchReport.Enabled = this.gameReports.Any() && this.gameReports.All(gameReport =>
            {
                GameEntry report;

                GameReportStatus status = gameReport.GetGameReportStatus(out report);

                return status != GameReportStatus.Failure;
            });
        }

        private void ArrangeGameReports()
        {
            this.SetGameReportTitles();

            this.ArrangeGameReportLocations();
        }

        private void btnEnterMatchReport_Click(object sender, EventArgs e)
        {
            SCPlayer ply1 = this.ImgCmbBxPlayer1.SelectedValue as SCPlayer;
            SCPlayer ply2 = this.ImgCmbBxPlayer2.SelectedValue as SCPlayer;

            var reports = this.gameReports.Select(rep =>
            {
                GameEntry report;

                return new { Status = rep.GetGameReportStatus(out report), GameReport = report };
            }).ToList();

            if (ply1 != null && ply2 != null && !reports.Any(rep => rep.Status == GameReportStatus.Failure))
            {
                if (this.contextSelector.SelectedSeason != null)
                {
                    this.EloDataSource().ReportMatch(ply1, ply2, reports.Select(item => item.GameReport).ToArray(), this.contextSelector.SelectedSeason, this.dtpMatchDate.Value);
                }
                else if (this.contextSelector.SelectedTournament != null)
                {
                    this.EloDataSource().ReportMatch(ply1, ply2, reports.Select(item => item.GameReport).ToArray(), this.contextSelector.SelectedTournament, this.dtpMatchDate.Value);
                }
                else { this.EloDataSource().ReportMatch(ply1, ply2, reports.Select(item => item.GameReport).ToArray(), this.dtpMatchDate.Value); }

                this.MatchReported.Invoke(this, new EventArgs());

                this.ResetControls();
            }
        }

        private void ResetControls()
        {
            this.RemoveGameReports(this.gameReports.ToArray());

            this.ImgCmbBxPlayer1.SelectedIndex = -1;
            this.ImgCmbBxPlayer2.SelectedIndex = -1;
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

            this.contextSelector.AddItems(source.GetTournaments(), this.ImageGetter);
        }

        private Image ImageGetter(int imageID)
        {
            EloImage eloImg;

            if (this.EloDataSource().TryGetImage(imageID, out eloImg)) { return eloImg.Image; }
            else { return null; }
        }
    }
}
