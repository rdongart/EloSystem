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

            if (currentContent != null && MessageBox.Show(String.Format("A {0} named {1} already exists. Are you sure you would like to overwrite that {0}?", adder.ContentType.ToString().ToLower()
                , adder.ContentName), "Name is already in use", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
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

                    this.eloSystem.AddPlayer(playerAdder.ContentName, playerAdder.StartRating, playerAdder.SelectedTeam, playerAdder.SelectedCountry, playerAdder.SelectedImage);

                    break;
                case ContentTypes.Team: this.eloSystem.AddTeam(adder.ContentName, adder.SelectedImage); break;
                default: throw new Exception(String.Format("{0} is an unkonwn {1} in the current context.", adder.ContentType.ToString(), typeof(ContentTypes).Name));
            }

            MessageBox.Show(string.Format("Your {0} was added succesfully.", adder.ContentType.ToString().ToLower()));

            this.ContentChanged();
        }

        private void ContentChanged()
        {
            //throw new NotImplementedException();// TODO: implement this method
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

            var items = this.eloSystem.GetCountries().OrderBy(country => country.Name).Select(country => Tuple.Create<string, Country, Image>(country.Name, country, GetImage(country))).ToList();

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

            this.playerAdder.ImgCmbBxCountries.DisplayMember = "Item1";
            this.playerAdder.ImgCmbBxCountries.ValueMember = "Item2";
            this.playerAdder.ImgCmbBxCountries.ImageMember = "Item3";

            var items = this.eloSystem.GetTeams().OrderBy(team => team.Name).Select(team => Tuple.Create<string, Team, Image>(team.Name, team, GetImage(team))).ToList();

            this.playerAdder.ImgCmbBxTeams.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection))
            {
                this.playerAdder.ImgCmbBxTeams.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection));
            }
        }

    }

}