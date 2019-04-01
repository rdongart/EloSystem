using SCEloSystemGUI.Properties;
using BrightIdeasSoftware;
using EloSystem.ResourceManagement;
using System.Drawing;
using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm : Form
    {
        private bool contentWasEdited;
        private ContentAdder countryAdder;
        private DblNameContentAdder teamAdder;
        private DblNameContentAdder tournamentAdder;
        private DblNameContentEditor<Team> teamEditor;
        private DblNameContentEditor<Tournament> tournamentEditor;
        private Dictionary<int, ResourceItem> resMemory = new Dictionary<int, ResourceItem>();
        private EloData eloSystem;
        private HasNameContentAdder<Tileset> tileSetAdder;
        private MapAdder mapAdder;
        private MatchReport matchReport;
        private PlayerEditor playerAdder;
        private SeasonAdder seasonAdder;
        private PlayerStats playerStatsDisplay;

        internal MainForm(EloData eloSystem)
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.contentWasEdited = false;

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
            this.tileSetAdder = new HasNameContentAdder<Tileset>();
            this.tileSetAdder.OnAddButtonClick += this.AddTilSet;
            this.tileSetAdder.OnAddButtonClick += this.TileSetAdder_OnAddButtonClick;
            this.tblLOPnlMaps.Controls.Add(this.tileSetAdder, 1, 0);

            this.seasonAdder = new SeasonAdder();
            this.seasonAdder.OnAddButtonClick += this.AddSeason;
            this.tblLOPnlTournaments.Controls.Add(this.seasonAdder, 1, 0);

            this.mapAdder = new MapAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddPlayer += this.AddContent;
            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);

            this.countryAdder = new ContentAdder() { ContentType = ContentTypes.Country };
            this.countryAdder.OnAddPlayer += this.AddContent;
            this.countryAdder.OnAddPlayer += this.CountryAdder_OnAddButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryAdder, 0, 0);

            this.teamAdder = new DblNameContentAdder() { ContentType = ContentTypes.Team };
            this.teamAdder.OnAddPlayer += this.AddContent;
            this.teamAdder.OnAddPlayer += this.TeamAdder_OnAddButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamAdder, 0, 0);

            this.playerAdder = new PlayerEditor() { ContentType = ContentTypes.Player };
            this.playerAdder.EloDataSource = () => { return this.eloSystem; };
            this.playerAdder.OnAddPlayer += this.AddContent;
            this.playerAdder.OnAddPlayer += this.PlayerAdder_OnAddButtonClick;
            this.playerAdder.OnEditButtonClick += this.PlayerAdder_OnPlayerDatabaseEdited;
            this.playerAdder.OnRemoveButtonClick += this.PlayerAdder_OnPlayerDatabaseEdited;
            this.tblLOPnlPlayers.Controls.Add(this.playerAdder, 0, 0);

            this.tournamentAdder = new DblNameContentAdder() { ContentType = ContentTypes.Tournament };
            this.tournamentAdder.OnAddPlayer += this.AddContent;
            this.tournamentAdder.OnAddPlayer += this.TournamentAdder_OnAddButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentAdder, 0, 0);

            this.tournamentEditor = new DblNameContentEditor<Tournament>() { ContentName = "Tornament", ResourceGetter = this.ImageGetterMethod };
            this.tournamentEditor.EditButtonClicked += this.OnEditTournament;
            this.tournamentEditor.EditButtonClicked += this.TournamentEdited_OnEditedButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentEditor, 0, 1);

            this.teamEditor = new DblNameContentEditor<Team>() { ContentName = "Team", ResourceGetter = this.ImageGetterMethod };
            this.teamEditor.EditButtonClicked += this.OnEditTeam;
            this.teamEditor.EditButtonClicked += this.TeamEdited_OnEditedButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamEditor, 0, 1);

            this.matchReport = new MatchReport() { Margin = new Padding(6) };
            this.tabPageReportMatch.Controls.Add(this.matchReport);
            this.matchReport.EloDataSource = () => { return this.eloSystem; };

            this.AddTileSetsToCmbBox();
            this.AddCountriesToImgCmbBox();
            this.AddTeamsToImgCmbBox();
            this.AddPlayersToImgCmbBox();
            this.AddTournamentsToImgCmbBox();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) { return; }

            if (this.eloSystem.ContentHasBeenChanged || this.contentWasEdited)
            {
                switch (MessageBox.Show("If you close this Elo System, all changes not saved will be lost. Are you sure you would like to close?", "Close?", MessageBoxButtons.OKCancel))
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    default: break;
                }
            }

        }

        private Image ImageGetterMethod(IHasImageID item)
        {
            EloImage eloImg;

            if (this.eloSystem.TryGetImage(item.ImageID, out eloImg)) { return eloImg.Image; }
            else { return null; }
        }

        private void toolStripMenuItemPlayerStats_Click(object sender, EventArgs e)
        {
            if (this.playerStatsDisplay == null) { this.playerStatsDisplay = new PlayerStats(() => { return this.eloSystem; }); };

            this.playerStatsDisplay.ShowDialog();
        }

        private void mapStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mapStatsForm = new MapStatsDisplay(() => { return this.eloSystem; });

            mapStatsForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();

            aboutBox.ShowDialog();
        }
    }
}
