﻿using BrightIdeasSoftware;
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
        private ContentAdder countryAdder;
        private DblNameContentAdder teamAdder;
        private DblNameContentAdder tournamentAdder;
        private DblNameContentEditor<Tournament> tournamentEditor;
        private Dictionary<int, ResourceItem> resMemory = new Dictionary<int, ResourceItem>();
        private EloData eloSystem;
        private HasNameContentAdder<Tileset> tileSetAdder;
        private MapAdder mapAdder;
        private MatchReport matchReport;
        private PlayerAdder playerAdder;
        private SeasonAdder seasonAdder;
        private ObjectListView oLstVRecentMatches;

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
            this.tileSetAdder = new HasNameContentAdder<Tileset>();
            this.tileSetAdder.OnAddButtonClick += this.AddTilSet;
            this.tileSetAdder.OnAddButtonClick += this.TileSetAdder_OnAddButtonClick;
            this.tblLOPnlMaps.Controls.Add(this.tileSetAdder, 1, 0);

            this.seasonAdder = new SeasonAdder();
            this.seasonAdder.OnAddButtonClick += this.AddSeason;
            this.tblLOPnlTournaments.Controls.Add(this.seasonAdder, 1, 0);

            this.mapAdder = new MapAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddButtonClick += this.AddContent;
            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);

            this.countryAdder = new ContentAdder() { ContentType = ContentTypes.Country };
            this.countryAdder.OnAddButtonClick += this.AddContent;
            this.countryAdder.OnAddButtonClick += this.CountryAdder_OnAddButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryAdder, 0, 0);

            this.teamAdder = new DblNameContentAdder() { ContentType = ContentTypes.Team };
            this.teamAdder.OnAddButtonClick += this.AddContent;
            this.teamAdder.OnAddButtonClick += this.TeamAdder_OnAddButtonClick;
            this.tblLOPnlTeams.Controls.Add(this.teamAdder, 0, 0);

            this.playerAdder = new PlayerAdder() { ContentType = ContentTypes.Player };
            this.playerAdder.OnAddButtonClick += this.AddContent;
            this.playerAdder.OnAddButtonClick += PlayerAdder_OnAddButtonClick;
            this.tblLOPnlPlayers.Controls.Add(this.playerAdder, 0, 0);

            this.tournamentAdder = new DblNameContentAdder() { ContentType = ContentTypes.Tournament };
            this.tournamentAdder.OnAddButtonClick += this.AddContent;
            this.tournamentAdder.OnAddButtonClick += this.TournamentAdder_OnAddButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentAdder, 0, 0);

            this.tournamentEditor = new DblNameContentEditor<Tournament>() { ContentName = "Tornament", ResourceGetter = this.ImageGetterMethod };
            this.tournamentEditor.EditButtonClicked += this.EditContent;
            this.tournamentEditor.EditButtonClicked += this.TournamentEdited_OnEditedButtonClick;
            this.tblLOPnlTournaments.Controls.Add(this.tournamentEditor, 0, 1);

            this.matchReport = new MatchReport();
            this.tblLoPnlReportMatch.Controls.Add(this.matchReport, 0, 0);
            this.matchReport.EloDataSource = () => { return this.eloSystem; };

            this.oLstVRecentMatches = MainForm.CreateMatchListView();
            this.tblLoPnlRecentMatches.Controls.Add(this.oLstVRecentMatches, 0, 1);
            this.matchReport.MatchReported += this.OnMatchReported;

            this.AddTileSetsToCmbBox();
            this.AddCountriesToImgCmbBox();
            this.AddTeamsToImgCmbBox();
            this.AddPlayersToImgCmbBox();
            this.AddTournamentsToImgCmbBox();
            this.AddRecentMatches();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) { return; }

            if (this.eloSystem.ContentHasBeenChanged)
            {
                switch (MessageBox.Show("If you close this Elo System, all changes not saved will be lost. Are you sure you would like to close?", "Close?", MessageBoxButtons.OKCancel))
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    default: break;
                }
            }

        }

        private Image ImageGetterMethod(int imageID)
        {
            EloImage eloImg;

            if (this.eloSystem.TryGetImage(imageID, out eloImg)) { return eloImg.Image; }
            else { return null; }
        }
    }
}
