using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm : Form
    {
        private ContentAdder countryAdder;
        private ContentAdder mapAdder;
        private ContentAdder teamAdder;
        private Dictionary<int, ResourceItem> resMemory = new Dictionary<int, ResourceItem>();
        private EloData eloSystem;
        private MatchReport matchReport;
        private PlayerAdder playerAdder;

        internal MainForm(EloData eloSystem)
        {
            InitializeComponent();

            this.eloSystem = eloSystem;

            this.Text = this.eloSystem.Name;

            this.LoadContent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadContent()
        {
            this.mapAdder = new ContentAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddButtonClick += this.AddContent;
            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);

            this.countryAdder = new ContentAdder() { ContentType = ContentTypes.Country };
            this.countryAdder.OnAddButtonClick += this.AddContent;
            this.countryAdder.OnAddButtonClick += this.CountryAdder_OnAddButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryAdder, 0, 0);

            this.teamAdder = new ContentAdder() { ContentType = ContentTypes.Team };
            this.teamAdder.OnAddButtonClick += this.AddContent;
            this.teamAdder.OnAddButtonClick += this.TeamAdder_OnAddButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamAdder, 0, 0);

            this.playerAdder = new PlayerAdder() { ContentType = ContentTypes.Player };
            this.playerAdder.OnAddButtonClick += this.AddContent;
            this.playerAdder.OnAddButtonClick += PlayerAdder_OnAddButtonClick;
            this.tblLOPnlPlayers.Controls.Add(this.playerAdder, 0, 0);

            this.matchReport = new MatchReport();
            this.tabPageReportMatch.Controls.Add(this.matchReport);

            this.AddCountriesToImgCmbBox();         
            this.AddTeamsToImgCmbBox();
            this.AddPlayersToImgCmbBox();
        }

        private void PlayerAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddPlayersToImgCmbBox();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) { return; }

            if (this.eloSystem.DataWasChanged)
            {
                switch (MessageBox.Show("If you close this Elo System, all changes not saved will be lost. Are you sure you would like to close?", "Close?", MessageBoxButtons.OKCancel))
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    default: break;
                }
            }

        }


    }
}
