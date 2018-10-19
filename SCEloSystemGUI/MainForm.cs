using EloSystem;
using EloSystem.ResourceManagement;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private PlayerAdder playerAdder;

        internal MainForm(EloData eloSystem)
        {
            InitializeComponent();

            this.eloSystem = eloSystem;

            this.Text = this.eloSystem.Name;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.mapAdder = new ContentAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddButtonClick += this.AddContent;
            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);

            this.countryAdder = new ContentAdder() { ContentType = ContentTypes.Country };
            this.countryAdder.OnAddButtonClick += this.AddContent;
            this.countryAdder.OnAddButtonClick += CountryAdder_OnAddButtonClick;
            this.tblLOPnlCountries.Controls.Add(this.countryAdder, 0, 0);

            this.teamAdder = new ContentAdder() { ContentType = ContentTypes.Team };
            this.teamAdder.OnAddButtonClick += this.AddContent;
            this.tblLOPnlTeams.Controls.Add(this.teamAdder, 0, 0);

            this.playerAdder = new PlayerAdder() { ContentType = ContentTypes.Player };
            this.playerAdder.OnAddButtonClick += this.AddContent;
            this.tblLOPnlPlayers.Controls.Add(this.playerAdder, 0, 0);

            this.AddCountriesToImgCmbBox();
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

                    if (this.eloSystem.TryGetImage(c.ImageID, out eloImg))
                    {
                        return eloImg.Image;
                    }
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
