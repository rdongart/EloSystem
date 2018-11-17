using EloSystem.ResourceManagement;
using EloSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomControls;

namespace SCEloSystemGUI.UserControls
{
    public partial class SeasonAdder : UserControl
    {
        internal const string DEFAULT_TXTBXSEASONNAME_TEXT = "Type season name here...";

        private ImageComboBox imgCmbBxTournaments;
        public event EventHandler OnAddButtonClick = delegate { };
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        public Tournament SelectedTournament
        {
            get
            {
                if (this.imgCmbBxTournaments.SelectedIndex < 0) { return null; }
                else
                {
                    var selectedItem = this.imgCmbBxTournaments.SelectedItem as Tuple<string, Tournament, Image>;

                    return selectedItem == null ? null : selectedItem.Item2;
                }
            }
        }

        public SeasonAdder()
        {
            InitializeComponent();

            this.imgCmbBxTournaments = EloGUIControlsStaticMembers.CreateStandardContentAdderImageComboBox();
            this.imgCmbBxTournaments.TabIndex = 0;
            this.tblLOPnlSeasonAdder.Controls.Add(this.imgCmbBxTournaments, 1, 1);

            this.imgCmbBxTournaments.SelectedIndexChanged += this.ImgCmbBxTournaments_SelectedIndexChanged;
        }

        private void ImgCmbBxTournaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBxName.Enabled = this.SelectedTournament != null;

            if (this.SelectedTournament == null) { this.txtBxName.Text = string.Empty; }
            else if (this.txtBxName.Text == string.Empty) { this.txtBxName.Text = SeasonAdder.DEFAULT_TXTBXSEASONNAME_TEXT; }

        }

        internal void AddTournamentItems(IEnumerable<Tournament> tournaments, ImageGetter resourceGetter)
        {
            var currentSelection = this.imgCmbBxTournaments.SelectedValue as Tournament;

            this.imgCmbBxTournaments.DisplayMember = "Item1";
            this.imgCmbBxTournaments.ValueMember = "Item2";
            this.imgCmbBxTournaments.ImageMember = "Item3";

            var items = (new Tuple<string, Tournament, Image>[] { Tuple.Create<string, Tournament, Image>("none", null, null) }).Concat(tournaments.OrderBy(tournament => tournament.Name).Select(tournament =>
                Tuple.Create<string, Tournament, Image>(String.Format("{0}{1}",tournament.Name, tournament.NameLong != string.Empty ? " (" + tournament.NameLong + ")" : string.Empty), tournament, resourceGetter(tournament.ImageID)))).ToList();

            this.imgCmbBxTournaments.DataSource = items;

            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection)) { this.imgCmbBxTournaments.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection)); }
            else { this.txtBxName.Enabled = false; }
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAdd.Enabled = true; }
            else { this.btnAdd.Enabled = false; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddButtonClick.Invoke(this, new EventArgs());

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }
    }
}
