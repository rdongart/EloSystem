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

        private ImprovedImageComboBox<Tournament> imgCmbBxTournaments;
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

            this.imgCmbBxTournaments = EloGUIControlsStaticMembers.CreateStandardImprovedImageComboBox<Tournament>(null);
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

        internal void AddTournamentItems(IEnumerable<Tournament> tournaments, ImageGetter<Tournament> resourceGetter)
        {
            this.imgCmbBxTournaments.ImageGetter = resourceGetter;
            this.imgCmbBxTournaments.AddItems(tournaments.ToArray(), true);
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
