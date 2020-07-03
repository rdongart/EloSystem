using System.Linq;
using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm : Form
    {
        private const Keys PLAYER1_SHORTCUTKEY = Keys.F2;
        private const Keys PLAYER2_SHORTCUTKEY = Keys.F3;
        private const Keys HEADTOHEAD_SHORTCUTKEY = Keys.F5;

        private bool contentWasEdited;
        private ContentAdder countryAdder;
        private DblNameContentAdder teamAdder;
        private DblNameContentAdder tournamentAdder;
        private DblNameContentEditor<Team> teamEditor;
        private DblNameContentEditor<Tournament> tournamentEditor;
        private Dictionary<int, ResourceItem> resMemory = new Dictionary<int, ResourceItem>();
        private HasNameContentAdder<Tileset> tileSetAdder;
        private HasNameContentEditor<Tileset> tileSetEditor;
        private MapAdder mapAdder;
        private MatchReport matchReport;
        private PlayerEditor playerAdder;
        private SeasonAdder seasonAdder;
        private SeasonEditor seasonEditor;
        private SingleNameContentEditor<Country> countryEditor;
        private PlayerStats playerStatsDisplay;

        internal MainForm()
        {
            InitializeComponent();

            this.Icon = Resources.SCEloIcon;

            this.contentWasEdited = false;

            this.Text = GlobalState.DataBase.Name;

            this.LoadContent();

            this.matchReport.MatchChangedReported += this.OnMatchChanged;
        }

        private void OnMatchChanged(object sender, EventArgs e)
        {
            this.mapAdder.Update();

            GlobalState.MirrorMatchupEvaluation.ScheduleMirrorMatchupEvaluation();
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

            this.tileSetEditor = new HasNameContentEditor<Tileset>() { ContentGetter = GlobalState.DataBase.GetTileSets, BorderStyle = BorderStyle.FixedSingle };
            this.tileSetEditor.EditButtonClicked += this.EditTilSet;
            this.tileSetEditor.RemoveButtonClicked += this.RemoveTilSet;
            this.tblLOPnlMaps.Controls.Add(this.tileSetEditor, 1, 1);

            this.seasonAdder = new SeasonAdder();
            this.seasonAdder.AddButtonClick += this.AddSeason;
            this.tblLOPnlTournaments.Controls.Add(this.seasonAdder, 1, 0);

            this.seasonEditor = new SeasonEditor() { BorderStyle = BorderStyle.FixedSingle };
            this.seasonEditor.EditButtonClicked += this.OnSeasonEdited;
            this.seasonEditor.RemoveButtonClicked += this.OnRemoveSeason;
            this.tblLOPnlTournaments.Controls.Add(this.seasonEditor, 1, 1);

            this.mapAdder = new MapAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddMap += this.AddContent;
            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);
            this.tblLOPnlMaps.SetRowSpan(this.mapAdder, 2);

            this.countryAdder = new ContentAdder() { ContentType = ContentTypes.Country };
            this.countryAdder.OnAddMap += this.AddContent;
            this.countryAdder.OnAddMap += this.CountryAdder_OnAddButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryAdder, 0, 0);

            this.countryEditor = new SingleNameContentEditor<Country>() { ContentName = "Country", ImageGetter = EloGUIControlsStaticMembers.ImageGetterMethod, ContentGetter = GlobalState.DataBase.GetCountries };
            this.countryEditor.EditButtonClicked += this.OnEditCountry;
            this.countryEditor.EditButtonClicked += this.CountryEdited_OnEditedButtonClick;
            this.countryEditor.RemoveButtonClicked += this.CountryEdited_OnRemoveButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryEditor, 0, 1);

            this.teamAdder = new DblNameContentAdder() { ContentType = ContentTypes.Team };
            this.teamAdder.OnAddMap += this.AddContent;
            this.teamAdder.OnAddMap += this.TeamAdder_OnAddButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamAdder, 0, 0);

            this.playerAdder = new PlayerEditor() { ContentType = ContentTypes.Player };
            this.playerAdder.OnAddMap += this.AddContent;
            this.playerAdder.OnAddMap += this.PlayerAdder_OnAddButtonClick;
            this.playerAdder.OnEditButtonClick += this.PlayerAdder_OnPlayerDatabaseEdited;
            this.playerAdder.OnRemoveButtonClick += this.PlayerAdder_OnPlayerDatabaseEdited;
            this.tblLOPnlPlayers.Controls.Add(this.playerAdder, 0, 0);

            this.tournamentAdder = new DblNameContentAdder() { ContentType = ContentTypes.Tournament };
            this.tournamentAdder.OnAddMap += this.AddContent;
            this.tournamentAdder.OnAddMap += this.TournamentAdder_OnAddButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentAdder, 0, 0);

            this.tournamentEditor = new DblNameContentEditor<Tournament>() { ContentName = "Tornament", ImageGetter = EloGUIControlsStaticMembers.ImageGetterMethod, ContentGetter = GlobalState.DataBase.GetTournaments };
            this.tournamentEditor.RemoveCondition = (tournament) => !tournament.GetGames().Any();
            this.tournamentEditor.EditButtonClicked += this.OnEditTournament;
            this.tournamentEditor.EditButtonClicked += this.TournamentEdited_OnEditedButtonClick;
            this.tournamentEditor.RemoveButtonClicked += this.TournamentEdited_OnRemoveButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentEditor, 0, 1);

            this.teamEditor = new DblNameContentEditor<Team>() { ContentName = "Team", ImageGetter = EloGUIControlsStaticMembers.ImageGetterMethod, ContentGetter = GlobalState.DataBase.GetTeams };
            this.teamEditor.EditButtonClicked += this.OnEditTeam;
            this.teamEditor.EditButtonClicked += this.TeamEdited_OnEditedButtonClick;
            this.teamEditor.RemoveButtonClicked += this.TeamEdited_OnRemoveButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamEditor, 0, 1);

            this.matchReport = new MatchReport() { Margin = new Padding(6), Dock = DockStyle.Fill };
            this.tabPageReportMatch.Controls.Add(this.matchReport);

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

            if (GlobalState.DataBase.ContentHasBeenChanged || this.contentWasEdited)
            {
                switch (MessageBox.Show("If you close this Elo System, all changes not saved will be lost. Are you sure you would like to close?", "Close?", MessageBoxButtons.OKCancel))
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    default: break;
                }
            }

        }

        private void toolStripMenuItemPlayerStats_Click(object sender, EventArgs e)
        {
            Cursor previousCursor = Cursor.Current;

            Cursor.Current = Cursors.WaitCursor;

            if (this.playerStatsDisplay == null) { this.playerStatsDisplay = new PlayerStats(); };

            Cursor.Current = previousCursor;

            CustomControls.Styles.FormStyles.ShowFullFormRelativeToAnchor(this.playerStatsDisplay, this);

            this.playerStatsDisplay.ShowDialog();
        }

        private void mapStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor previousCursor = Cursor.Current;

            Cursor.Current = Cursors.WaitCursor;

            MapStatsDisplay.ShowMapList(this);

            Cursor.Current = previousCursor;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();

            aboutBox.ShowDialog();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == MainForm.PLAYER1_SHORTCUTKEY)
            {
                var selectedPlayer = this.matchReport.ImgCmbBxPlayer1.SelectedValue as SCPlayer;

                if (selectedPlayer != null) { PlayerProfile.ShowProfile(selectedPlayer, this); }
            }
            else if (e.KeyCode == MainForm.PLAYER2_SHORTCUTKEY)
            {
                var selectedPlayer = this.matchReport.ImgCmbBxPlayer2.SelectedValue as SCPlayer;

                if (selectedPlayer != null) { PlayerProfile.ShowProfile(selectedPlayer, this); }
            }
            else if (e.KeyCode == MainForm.HEADTOHEAD_SHORTCUTKEY)
            {
                var selectedPlayer1 = this.matchReport.ImgCmbBxPlayer1.SelectedValue as SCPlayer;
                var selectedPlayer2 = this.matchReport.ImgCmbBxPlayer2.SelectedValue as SCPlayer;

                if (selectedPlayer1 != null && selectedPlayer2 != null) { PlayerProfile.ShowProfile(selectedPlayer1, selectedPlayer2, this); }
            }
        }

        private void tournamentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentsOverview.ShowOverview();
        }

        private void MainForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            GlobalState.OpenHelp();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalState.OpenHelp();
        }
    }
}
