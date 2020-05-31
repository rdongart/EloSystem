using CustomExtensionMethods;
using CustomControls;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class SeasonEditor : UserControl
    {
        private ImprovedImageComboBox<Tournament> imgCmbBxTournaments;
        public EventHandler EditButtonClicked = delegate { };
        public EventHandler RemoveButtonClicked = delegate { };
        public string NewSeasonName
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
                return this.imgCmbBxTournaments.SelectedValue as Tournament;
            }
        }
        public Season SelectedSeason
        {
            get
            {
                return this.cmbBxSeasons.SelectedItem == null ? null : (this.cmbBxSeasons.SelectedItem as Tuple<string, Season>).Item2;
            }
        }

        public SeasonEditor()
        {
            InitializeComponent();

            this.imgCmbBxTournaments = EloGUIControlsStaticMembers.CreateStandardImprovedImageComboBox<Tournament>(null);
            this.imgCmbBxTournaments.TabIndex = 0;
            this.imgCmbBxTournaments.SelectedIndexChanged += this.ImgCmbBxTournaments_SelectedIndexChanged;
            this.tblLoPnlMain.Controls.Add(this.imgCmbBxTournaments, 1, 1);

            this.UpdateControlContents();
        }

        private void UpdateSeasons()
        {
            if (this.SelectedTournament == null || !this.SelectedTournament.GetSeasons().Any())
            {
                this.cmbBxSeasons.SelectedIndex = -1;

                this.cmbBxSeasons.Items.Clear();

                this.cmbBxSeasons.Enabled = false;
            }
            else
            {
                this.cmbBxSeasons.Enabled = true;

                List<Season> seasonList = this.SelectedTournament.GetSeasons().OrderBy(season => season.Name).ToList();

                var selectedItem = this.SelectedSeason;

                this.cmbBxSeasons.DisplayMember = "Item1";
                this.cmbBxSeasons.ValueMember = "Item2";

                this.cmbBxSeasons.Items.Clear();
                this.cmbBxSeasons.Items.AddRange(seasonList.Select(season => Tuple.Create<string, Season>(season.Name, season)).ToArray());

                if (selectedItem != null && seasonList.Contains(selectedItem)) { this.cmbBxSeasons.SelectedIndex = seasonList.IndexOf(selectedItem); }
                else { this.cmbBxSeasons.SelectedIndex = -1; }
            }
        }

        private void SetSelectedSeason(Season season)
        {
            if (this.cmbBxSeasons.Items.Cast<Tuple<string, Season>>().Any(obj => obj.Item2.Equals(season)))
            {
                this.cmbBxSeasons.SelectedIndex = this.cmbBxSeasons.Items.Cast<Tuple<string, Season>>().Select(obj => obj.Item2).IndexOf(season);
            }
        }

        private void SetControlsEnabledStatus()
        {
            this.btnEdit.Enabled = this.SelectedSeason != null && this.NewSeasonName != string.Empty && this.NewSeasonName != this.SelectedSeason.Name;

            this.btnRemove.Enabled = this.SelectedSeason != null && !this.SelectedSeason.GetMatches().Any();

            this.txtBxName.Enabled = this.SelectedSeason != null;
        }

        public void UpdateControlContents()
        {
            var selectedSeason = this.SelectedSeason;

            this.imgCmbBxTournaments.AddItems(GlobalState.DataBase.GetTournaments().ToArray(), (t) => { return EloGUIControlsStaticMembers.ImageGetterMethod(t); }, false);

            this.UpdateSeasons();

            this.SetSelectedSeason(selectedSeason);

            this.SetControlsEnabledStatus();
        }

        private void ImgCmbBxTournaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateSeasons();

            this.SetControlsEnabledStatus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Are you sure you would like to remove the season \"{0}\" from the tournament \"{1}\"?", this.SelectedSeason.Name, this.SelectedTournament.Name), "Remove season"
                , MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (GlobalState.DataBase.RemoveSeason(this.SelectedSeason))
                {
                    this.cmbBxSeasons.SelectedIndex = -1;

                    this.txtBxName.Text = string.Empty;

                    this.UpdateControlContents();

                    this.RemoveButtonClicked.Invoke(this, new EventArgs());
                }
                else { MessageBox.Show("Failed to remove content from the database. Some removal conditions were not met.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.SelectedSeason.Name = this.txtBxName.Text;

            this.EditButtonClicked.Invoke(this, new EventArgs());

            this.UpdateControlContents();
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            this.SetControlsEnabledStatus();
        }

        private void cmbBxSeasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedSeason != null) { this.txtBxName.Text = this.SelectedSeason.Name; }

            this.SetControlsEnabledStatus();
        }
    }
}
