using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm : Form
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
                case ContentTypes.Player: throw new NotImplementedException();// TODO add method for adding a player here
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

    }

}