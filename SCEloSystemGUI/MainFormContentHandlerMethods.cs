using CustomControls;
using BrightIdeasSoftware;
using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SCEloSystemGUI.Properties;

namespace SCEloSystemGUI
{
    public partial class MainForm
    {
        private static void DisplayContentEditFailureMessage()
        {
            MessageBox.Show(String.Format("A failure occurred while trying to add edit content."));
        }

        private static bool ShouldSimilarNamedContentBeAdded(string name, string type)
        {
            DialogResult dlgResult = MessageBox.Show(String.Format("A {0} named {1} already exists. Are you sure you would like to add new content with and identical name?", type, name), "Name is already in use", MessageBoxButtons.OKCancel);

            switch (dlgResult)
            {
                case DialogResult.OK:
                case DialogResult.Yes: return true;
                case DialogResult.None:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.No: return false;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", dlgResult.ToString(), typeof(ContentTypes).Name));
            }

        }

        private static void DisplayContentEditSuccesMessage()
        {
            MessageBox.Show(String.Format("Content was successfully edited."));
        }

        private void OnEditTournament(object sender, EventArgs e)
        {
            var editorSender = sender as DblNameContentEditor<Tournament>;

            if (editorSender != null) { this.EditContent<Tournament>(editorSender); }
        }

        private void OnEditTeam(object sender, EventArgs e)
        {
            var editorSender = sender as DblNameContentEditor<Team>;

            if (editorSender != null) { this.EditContent<Team>(editorSender); }
        }

        private void EditContent<T>(DblNameContentEditor<T> editor) where T : EloSystemContent, IHasDblName
        {
            editor.SelectedItem.Name = editor.NameShort;
            editor.SelectedItem.NameLong = editor.NameLong;

            if (editor.NewImage != null) { this.eloSystem.EidtImage(editor.SelectedItem, editor.NewImage); }
            else if (editor.RemoveCurrentImage) { this.eloSystem.EidtImage(editor.SelectedItem, null); }
        }

        private void AddContent(object sender, ContentAddingEventArgs e)
        {
            IContentAdder adder = e.ContentAdder;

            if (adder == null) { return; }

            EloSystemContent currentContent;

            switch (adder.ContentType)
            {
                case ContentTypes.Country: currentContent = this.eloSystem.GetCountry(adder.ContentName); break;
                case ContentTypes.Map: currentContent = this.eloSystem.GetMap(adder.ContentName); break;
                case ContentTypes.Player: currentContent = this.eloSystem.GetPlayers(adder.ContentName).FirstOrDefault(); break;
                case ContentTypes.Team: currentContent = this.eloSystem.GetTeam(adder.ContentName); break;
                case ContentTypes.Tournament: currentContent = this.eloSystem.GetTournament(adder.ContentName); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            if (currentContent != null && !MainForm.ShouldSimilarNamedContentBeAdded(adder.ContentName, adder.ContentType.ToString())) { return; }

            switch (adder.ContentType)
            {
                case ContentTypes.Country: this.eloSystem.AddCountry(adder.ContentName, adder.NewImage); break;
                case ContentTypes.Map:
                    var mapAdder = e.ContentAdder as MapAdder;

                    this.eloSystem.AddMap(mapAdder.ContentName, mapAdder.MapType, mapAdder.MapSize, mapAdder.NewImage);

                    break;
                case ContentTypes.Player:
                    var playerAdder = e.ContentAdder as PlayerEditor;

                    this.eloSystem.AddPlayer(playerAdder.ContentName, playerAdder.GetAliases(), playerAdder.IRLName, playerAdder.StartRating, playerAdder.SelectedTeam, playerAdder.SelectedCountry
                        , playerAdder.NewImage, playerAdder.BirthDateWasSet ? playerAdder.BirthDate : new DateTime());

                    break;
                case ContentTypes.Team:
                    var teamAdder = e.ContentAdder as DblNameContentAdder;

                    this.eloSystem.AddTeam(teamAdder.NameShort, teamAdder.NameLong, teamAdder.NewImage); break;
                case ContentTypes.Tournament:
                    var tournamentAdder = e.ContentAdder as DblNameContentAdder;

                    this.eloSystem.AddTournament(tournamentAdder.NameShort, tournamentAdder.NameLong, adder.NewImage); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            MessageBox.Show(string.Format("Your {0} was added succesfully.", adder.ContentType.ToString().ToLower()));
        }

        private void AddSeason(object sender, EventArgs e)
        {
            var adderSender = sender as SeasonAdder;

            if (seasonAdder == null || adderSender.SelectedTournament == null) { return; }

            this.eloSystem.AddSeason(adderSender.ContentName, adderSender.SelectedTournament);
        }

        private void AddTilSet(object sender, EventArgs e)
        {
            var hasNameSender = sender as HasNameContentAdder<Tileset>;

            if (hasNameSender == null)
            {
                MainForm.DisplayContentEditFailureMessage();
                return;
            }

            Tileset currentContent = this.eloSystem.GetTileSet(hasNameSender.Name);

            if (currentContent != null) { MessageBox.Show(String.Format("A {0} named {1} already exists.", currentContent.GetType().Name, currentContent.Name)); }
            else
            {
                this.eloSystem.AddTileSet(hasNameSender.ContentName);
                MainForm.DisplayContentEditSuccesMessage();
            }
        }

        private void TeamAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddTeamsToImgCmbBox();
        }

        private void CountryAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddCountriesToImgCmbBox();
        }

        private void TileSetAdder_OnAddButtonClick(object sender, EventArgs e)
        {
            this.AddTileSetsToCmbBox();
        }

        private void PlayerAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddPlayersToImgCmbBox();
        }

        private void PlayerAdder_OnPlayerDatabaseEdited(object sender, EventArgs e)
        {
            this.contentWasEdited = true;

            this.AddPlayersToImgCmbBox();
        }

        private void TournamentAdder_OnAddButtonClick(object sender, ContentAddingEventArgs e)
        {
            this.AddTournamentsToImgCmbBox();
        }

        private void TournamentEdited_OnEditedButtonClick(object sender, EventArgs e)
        {
            this.contentWasEdited = true;

            this.AddTournamentsToImgCmbBox();
        }

        private void TeamEdited_OnEditedButtonClick(object sender, EventArgs e)
        {
            this.contentWasEdited = true;

            this.AddTeamsToImgCmbBox();
        }

        private void AddTileSetsToCmbBox()
        {
            var selectedItem = this.mapAdder.cmbBxTileset.SelectedItem != null ? (this.mapAdder.cmbBxTileset.SelectedItem as Tuple<string, Tileset>).Item2 : null;

            List<Tileset> tileSetList = this.eloSystem.GetTileSets().OrderBy(tileSet => tileSet.Name).ToList();

            this.mapAdder.cmbBxTileset.DisplayMember = "Item1";
            this.mapAdder.cmbBxTileset.ValueMember = "Item2";

            this.mapAdder.cmbBxTileset.Items.Clear();
            this.mapAdder.cmbBxTileset.Items.AddRange(tileSetList.Select(tileSet => Tuple.Create<string, Tileset>(tileSet.Name, tileSet)).ToArray());

            if (selectedItem != null && tileSetList.Contains(selectedItem)) { this.mapAdder.cmbBxTileset.SelectedIndex = tileSetList.IndexOf(selectedItem); }
        }

        private void AddCountriesToImgCmbBox()
        {
            ImageGetter<Country> getter = this.ImageGetterMethod;

            this.playerAdder.ImgCmbBxCountries.AddItems(this.eloSystem.GetCountries().ToArray(), getter, true);
        }

        private void AddPlayersToImgCmbBox()
        {
            SCPlayer[] players = this.eloSystem.GetPlayers().ToArray();

            this.matchReport.ImgCmbBxPlayer1.AddItems(players, false);
            this.matchReport.ImgCmbBxPlayer2.AddItems(players, false);
        }

        private void AddTeamsToImgCmbBox()
        {
            this.teamEditor.AddContentItems(this.eloSystem.GetTeams(), this.ImageGetterMethod);

            ImageGetter<Team> getter = this.ImageGetterMethod;

            this.playerAdder.ImgCmbBxTeams.AddItems(this.eloSystem.GetTeams().ToArray(), getter, true);
        }

        private void AddTournamentsToImgCmbBox()
        {
            this.seasonAdder.AddTournamentItems(this.eloSystem.GetTournaments(), this.ImageGetterMethod);

            this.tournamentEditor.AddContentItems(this.eloSystem.GetTournaments(), this.ImageGetterMethod);
        }
    }

}