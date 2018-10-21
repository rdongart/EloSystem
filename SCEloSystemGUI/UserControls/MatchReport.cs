using CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class MatchReport : UserControl
    {
        internal ImageComboBox ImgCmbBxPlayer1 { get; private set; }
        internal ImageComboBox ImgCmbBxPlayer2 { get; private set; }
        private List<GameReport> gameReports;

        public MatchReport()
        {
            InitializeComponent();

            this.ImgCmbBxPlayer1 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlMatchReport.Controls.Add(this.ImgCmbBxPlayer1, 1, 2);
            this.tblLOPnlMatchReport.SetColumnSpan(this.ImgCmbBxPlayer1, 4);

            this.ImgCmbBxPlayer2 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlMatchReport.Controls.Add(this.ImgCmbBxPlayer2, 6, 2);
            this.tblLOPnlMatchReport.SetColumnSpan(this.ImgCmbBxPlayer2, 4);

            this.gameReports = new List<GameReport>();
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
                DropDownWidth = 160,
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
            var nextGameReport = new GameReport();

            nextGameReport.OnRemoveButtonClick += this.NextGameReport_OnRemoveButtonClick;

            this.gameReports.Add(nextGameReport);

            this.ArrangeGameReports();

            this.pnlGameReports.Controls.Add(nextGameReport);
        }

        private void NextGameReport_OnRemoveButtonClick(object sender, EventArgs e)
        {
            GameReport senderGameReport = MatchReport.RetrieveGameReportFromChild(sender as Control);

            if (senderGameReport == null) { return; }

            if (MessageBox.Show("Would you like to delete this game report?", "Delete game report?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) { return; }

            this.gameReports.Remove(senderGameReport);

            this.ArrangeGameReports();

            senderGameReport.Dispose();
        }

        private void ArrangeGameReports()
        {
            this.SetGameReportTitles();

            this.ArrangeGameReportLocations();
        }
    }
}
