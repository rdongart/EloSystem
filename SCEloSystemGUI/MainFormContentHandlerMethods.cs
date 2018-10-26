using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm
    {
        private static void DisplayContentEditFailureMessage()
        {
            MessageBox.Show(String.Format("A failure occurred while trying to add edit content."));
        }

        private static bool ShouldContentBeReplaced(string name, string type)
        {
            DialogResult dlgResult = MessageBox.Show(String.Format("A {0} named {1} already exists. Are you sure you would like to overwrite that {0}?", type, name), "Name is already in use", MessageBoxButtons.OKCancel);

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

        private void AddContent(object sender, ContentAddingEventArgs e)
        {
            IContentAdder adder = e.ContentAdder;

            if (adder == null) { return; }

            EloSystemContent currentContent;

            switch (adder.ContentType)
            {
                case ContentTypes.Country: currentContent = this.eloSystem.GetCountry(adder.ContentName); break;
                case ContentTypes.Map: currentContent = this.eloSystem.GetMap(adder.ContentName); break;
                case ContentTypes.Player: currentContent = this.eloSystem.GetPlayer(adder.ContentName); break;
                case ContentTypes.Team: currentContent = this.eloSystem.GetTeam(adder.ContentName); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            if (currentContent != null && MainForm.ShouldContentBeReplaced(adder.ContentName, adder.ContentType.ToString()) == false) { return; }
            else if (currentContent != null)
            {
                switch (adder.ContentType)
                {
                    case ContentTypes.Country: this.eloSystem.RemoveCountry(currentContent as Country); break;
                    case ContentTypes.Map: this.eloSystem.RemoveMap(currentContent as Map); break;
                    case ContentTypes.Player: this.eloSystem.RemovePlayer(currentContent as SCPlayer); break;
                    case ContentTypes.Team: this.eloSystem.RemoveTeam(currentContent as Team); break;
                    default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
                }
            }

            switch (adder.ContentType)
            {
                case ContentTypes.Country: this.eloSystem.AddCountry(adder.ContentName, adder.SelectedImage); break;
                case ContentTypes.Map: this.eloSystem.AddMap(adder.ContentName, adder.SelectedImage); break;
                case ContentTypes.Player:
                    var playerAdder = e.ContentAdder as PlayerAdder;

                    this.eloSystem.AddPlayer(playerAdder.ContentName, playerAdder.GetAliases(), playerAdder.StartRating, playerAdder.SelectedTeam, playerAdder.SelectedCountry, playerAdder.SelectedImage);

                    break;
                case ContentTypes.Team: this.eloSystem.AddTeam(adder.ContentName, adder.SelectedImage); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            MessageBox.Show(string.Format("Your {0} was added succesfully.", adder.ContentType.ToString().ToLower()));
        }

        private void AddTilSet(object sender, HasNameAddingEventArgs e)
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
                this.eloSystem.AddTileSet(hasNameSender.Name);
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

        private void AddCountriesToImgCmbBox()
        {
            var currentSelection = this.playerAdder.ImgCmbBxCountries.SelectedValue as Country;

            Func<Country, Image> GetImage = c =>
            {
                EloImage eloImg;

                if (this.eloSystem.TryGetImage(c.ImageID, out eloImg)) { return eloImg.Image; }
                else { return null; }

            };

            this.playerAdder.ImgCmbBxCountries.DisplayMember = "Item1";
            this.playerAdder.ImgCmbBxCountries.ValueMember = "Item2";
            this.playerAdder.ImgCmbBxCountries.ImageMember = "Item3";

            var items = (new Tuple<string, Country, Image>[] { Tuple.Create<string, Country, Image>("none", null, null) }).Concat(this.eloSystem.GetCountries().OrderBy(country => country.Name).Select(country =>
                Tuple.Create<string, Country, Image>(country.Name, country, GetImage(country)))).ToList();

            this.playerAdder.ImgCmbBxCountries.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection))
            {
                this.playerAdder.ImgCmbBxCountries.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection));
            }
        }

        private void AddPlayersToImgCmbBox()
        {
            EloGUIControlsStaticMembers.AddPlayersToImgCmbBox(this.matchReport.ImgCmbBxPlayer1, this.eloSystem);
            EloGUIControlsStaticMembers.AddPlayersToImgCmbBox(this.matchReport.ImgCmbBxPlayer2, this.eloSystem);
        }


        private void AddTeamsToImgCmbBox()
        {
            var currentSelection = this.playerAdder.ImgCmbBxTeams.SelectedValue as Team;

            Func<Team, Image> GetImage = c =>
            {
                EloImage eloImg;

                if (this.eloSystem.TryGetImage(c.ImageID, out eloImg)) { return eloImg.Image; }
                else { return null; }

            };

            this.playerAdder.ImgCmbBxTeams.DisplayMember = "Item1";
            this.playerAdder.ImgCmbBxTeams.ValueMember = "Item2";
            this.playerAdder.ImgCmbBxTeams.ImageMember = "Item3";

            var items = (new Tuple<string, Team, Image>[] { Tuple.Create<string, Team, Image>("none", null, null) }).Concat(this.eloSystem.GetTeams().OrderBy(team => team.Name).Select(team =>
                Tuple.Create<string, Team, Image>(team.Name, team, GetImage(team)))).ToList();

            this.playerAdder.ImgCmbBxTeams.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection))
            {
                this.playerAdder.ImgCmbBxTeams.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection));
            }
        }

    }

}