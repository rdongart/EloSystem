﻿using BrightIdeasSoftware;
using CustomControls;
using CustomExtensionMethods;
using EloSystem;
using EloSystemExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EloSystem.ResourceManagement;

namespace SCEloSystemGUI.UserControls
{
    internal partial class PlayerEditor : UserControl, IContentAdder
    {
        private const int DEFAULT_PLAYERAGE = 30;

        internal Country SelectedCountry
        {
            get
            {
                return this.ImgCmbBxCountries.SelectedValue as Country;
            }
        }
        internal int StartRating
        {
            get
            {
                return this.numUDStartRating.Value.RoundToInt();
            }
        }
        internal ResourceGetter EloDataSource
        {
            private get { return this.eloDataSource; }
            set { this.eloDataSource = value; }
        }
        internal Team SelectedTeam
        {
            get
            {
                return this.ImgCmbBxTeams.SelectedValue as Team;
            }
        }
        private ContentTypes contentType;
        private ObjectListView lstVSearchResults;
        private ResourceGetter eloDataSource;
        public bool BirthDateWasSet
        {
            get
            {
                return this.chkBxShowDateTimeAdder.Checked;
            }
        }
        public ContentTypes ContentType
        {
            get
            {
                return this.contentType;
            }
            internal set
            {
                this.lbHeading.Text = String.Format("Create or edit {0}", value.ToString().ToLower());

                this.contentType = value;
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return this.dateTimePickerBirthDate.Value;
            }
        }
        public event EventHandler<ContentAddingEventArgs> OnAddButtonClick = delegate { };
        public event EventHandler OnEditButtonClick = delegate { };
        public Image NewImage { get; private set; }
        public ImageComboBox ImgCmbBxCountries { get; private set; }
        public ImageComboBox ImgCmbBxTeams { get; private set; }
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        public string IRLName
        {
            get
            {
                return this.txtBxIRLName.Text;
            }
        }

        internal PlayerEditor()
        {
            InitializeComponent();

            this.txtBxName.Text = string.Empty;
            this.txtBxAlias.Text = string.Empty;
            this.btnAdd.Enabled = false;
            this.btnAddAlias.Enabled = false;

            this.numUDStartRating.Maximum = int.MaxValue;
            this.numUDStartRating.Value = EloSystemStaticMembers.START_RATING_DEFAULT;

            this.ImgCmbBxCountries = EloGUIControlsStaticMembers.CreateStandardContentAdderImageComboBox();
            this.ImgCmbBxCountries.TabIndex = 11;
            this.ImgCmbBxCountries.SelectedIndexChanged += this.ImgCmbBx_SelectedIndexChanged;
            this.tblLOPnlPlayerEditor.Controls.Add(this.ImgCmbBxCountries, 4, 7);

            this.ImgCmbBxTeams = EloGUIControlsStaticMembers.CreateStandardContentAdderImageComboBox();
            this.ImgCmbBxTeams.SelectedIndexChanged += this.ImgCmbBx_SelectedIndexChanged;
            this.ImgCmbBxTeams.TabIndex = 12;
            this.tblLOPnlPlayerEditor.Controls.Add(this.ImgCmbBxTeams, 4, 8);

            this.lstVSearchResults = PlayerEditor.CreatePlayerSearchResultListView();
            this.lstVSearchResults.SelectionChanged += this.LstVSearchResults_SelectionChanged;
            this.tblLOPnlPlayerEditor.Controls.Add(this.lstVSearchResults, 1, 3);
            this.tblLOPnlPlayerEditor.SetRowSpan(this.lstVSearchResults, 6);

            this.rdBtnAddNew.Checked = true;
        }

        private void ImgCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetbtnAddEnabledStatus();
        }

        private void LstVSearchResults_SelectionChanged(object sender, EventArgs e)
        {
            if (this.lstVSearchResults.SelectedItem != null)
            {
                SCPlayer selectedPlayer = this.lstVSearchResults.SelectedItem.RowObject as SCPlayer;

                if (selectedPlayer != null) { this.MakeEditable(selectedPlayer); }
            }

            this.SetbtnAddEnabledStatus();
        }

        private static ObjectListView CreatePlayerSearchResultListView()
        {
            var playerSearchResult = new ObjectListView();

            var olvClmEmpty = new OLVColumn() { MinimumWidth = 0, MaximumWidth = 0, Width = 0, CellPadding = null };
            var olvClmName = new OLVColumn() { Width = 160, Text = "Name" };
            var olvClmAliases = new OLVColumn() { Width = 240, Text = "Aliases" };
            var olvClmIRLName = new OLVColumn() { Width = 80, Text = "IRL-Name" };

            playerSearchResult.Dock = DockStyle.Fill;
            playerSearchResult.Enabled = false;
            playerSearchResult.Size = new Size(294, 204);
            playerSearchResult.HasCollapsibleGroups = false;
            playerSearchResult.ShowGroups = false;
            playerSearchResult.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            playerSearchResult.RowHeight = 22;
            playerSearchResult.Scrollable = true;

            playerSearchResult.AllColumns.AddRange(new OLVColumn[] { olvClmEmpty, olvClmName, olvClmAliases, olvClmIRLName });

            playerSearchResult.Columns.AddRange(new ColumnHeader[] { olvClmEmpty, olvClmName, olvClmAliases, olvClmIRLName });

            playerSearchResult.AlternateRowBackColor = Color.FromArgb(217, 217, 217);
            playerSearchResult.UseAlternatingBackColors = true;

            playerSearchResult.FullRowSelect = true;

            olvClmName.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null) { return player.Name; }
                else { return ""; }
            };

            olvClmAliases.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null) { return string.Join(", ", player.GetAliases()); }
                else { return ""; }
            };

            olvClmIRLName.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null) { return player.IRLName; }
                else { return ""; }
            };

            return playerSearchResult;
        }

        private void txtBxAlias_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxAlias.Text != string.Empty) { this.btnAddAlias.Enabled = true; }
            else { this.btnAddAlias.Enabled = false; }

            this.SetbtnAddEnabledStatus();
        }

        private void btnAddAlias_Click(object sender, EventArgs e)
        {
            this.lstViewAliases.Items.Add(this.txtBxAlias.Text);

            this.txtBxAlias.Text = string.Empty;
            this.btnAddAlias.Enabled = false;

            this.SetbtnAddEnabledStatus();
        }

        private void lstViewAliases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstViewAliases.SelectedItems.Count > 0) { this.btnRemoveAlias.Enabled = true; }
            else { this.btnRemoveAlias.Enabled = false; }
        }

        private void btnRemoveAlias_Click(object sender, EventArgs e)
        {
            List<ListViewItem> itemsToRemove = this.lstViewAliases.SelectedItems.Cast<ListViewItem>().ToList();

            this.lstViewAliases.SelectedItems.Clear();

            foreach (ListViewItem item in itemsToRemove) { this.lstViewAliases.Items.Remove(item); }

            this.btnRemoveAlias.Enabled = false;

            this.SetbtnAddEnabledStatus();
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            this.SetbtnAddEnabledStatus();
        }

        private void SetbtnAddEnabledStatus()
        {
            if (this.rdBtnAddNew.Checked) { this.btnAdd.Enabled = this.txtBxName.Text != string.Empty; }
            else if (this.rdBtnEdit.Checked && this.lstVSearchResults.SelectedItem != null)
            {
                SCPlayer playerToEdit = this.lstVSearchResults.SelectedItem.RowObject as SCPlayer;

                if (playerToEdit == null) { this.btnAdd.Enabled = false; }
                else
                {
                    DateTime birthDateCurrent;

                    string[] newAliases = this.lstViewAliases.Items.Cast<ListViewItem>().Select(item => item.Text).ToArray();

                    this.btnAdd.Enabled = playerToEdit.Name != this.txtBxName.Text
                        || playerToEdit.IRLName != this.txtBxIRLName.Text
                        || (this.chkBxShowDateTimeAdder.Checked && (!playerToEdit.TryGetBirthDate(out birthDateCurrent)
                        || this.dateTimePickerBirthDate.Value != birthDateCurrent))
                        || newAliases.Any(alias => !playerToEdit.GetAliases().Contains(alias))
                        || playerToEdit.GetAliases().Any(alias => !newAliases.Contains(alias))
                        || this.chckBxRemoveCurrentImage.Checked
                        || this.lbFileName.Text != string.Empty
                        || (this.ImgCmbBxCountries.SelectedValue as Country) != playerToEdit.Country
                        || (this.ImgCmbBxTeams.SelectedValue as Team) != playerToEdit.Team;
                }
            }
            else { this.btnAdd.Enabled = false; }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            if (EloGUIControlsStaticMembers.TryGetFilePathFromUser(out filePath))
            {
                this.lbFileName.Text = filePath;
                this.NewImage = Bitmap.FromFile(filePath);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.rdBtnAddNew.Checked)
            {
                this.OnAddButtonClick.Invoke(sender, new ContentAddingEventArgs(this));

                this.ClearFields();
            }
            else if (this.rdBtnEdit.Checked && this.lstVSearchResults.SelectedItem != null)
            {
                SCPlayer playerToEdit = this.lstVSearchResults.SelectedItem.RowObject as SCPlayer;

                if (playerToEdit != null)
                {
                    playerToEdit.Name = this.txtBxName.Text;
                    playerToEdit.IRLName = this.txtBxIRLName.Text;

                    if (this.chkBxShowDateTimeAdder.Checked) { playerToEdit.SetBirthDate(this.dateTimePickerBirthDate.Value); }

                    playerToEdit.SetAliases(this.lstViewAliases.Items.Cast<ListViewItem>().Select(item => item.Text));

                    if (this.NewImage != null) { this.EloDataSource().EidtImage(playerToEdit, this.NewImage); }
                    else if (this.chckBxRemoveCurrentImage.Checked) { this.EloDataSource().EidtImage(playerToEdit, null); }

                    playerToEdit.Country = this.ImgCmbBxCountries.SelectedValue as Country;

                    playerToEdit.Team = this.ImgCmbBxTeams.SelectedValue as Team;

                    this.OnEditButtonClick.Invoke(sender, new EventArgs());
                }

                this.SetbtnAddEnabledStatus();
            }
        }

        private void ClearFields()
        {
            if (this.NewImage != null) { this.NewImage.Dispose(); }

            this.lbFileName.Text = string.Empty;

            this.txtBxIRLName.Text = string.Empty;
            this.txtBxName.Text = string.Empty;
            this.txtBxAlias.Text = string.Empty;
            this.lstViewAliases.Items.Clear();
            this.lbCurrentImage.Enabled = false;
            this.SetCurrentImageToNull();
            this.tblLoPnlCurrentImage.Enabled = false;
            this.ImgCmbBxCountries.SelectedIndex = -1;
            this.ImgCmbBxTeams.SelectedIndex = -1;
            this.numUDStartRating.Value = EloSystemStaticMembers.START_RATING_DEFAULT;
            this.btnAdd.Enabled = false;
            this.btnAddAlias.Enabled = false;
            this.btnRemoveAlias.Enabled = false;
            this.chkBxShowDateTimeAdder.Checked = false;
            this.dateTimePickerBirthDate.Value = DateTime.Today.AddYears(-PlayerEditor.DEFAULT_PLAYERAGE);
        }

        public IEnumerable<string> GetAliases()
        {
            foreach (string alias in this.lstViewAliases.Items.Cast<ListViewItem>().Select(item => item.Text).ToList()) { yield return alias; }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            this.lbFileName.Text = string.Empty;

            this.btnRemoveImage.Enabled = false;
        }

        private void lbFileName_TextChanged(object sender, EventArgs e)
        {
            this.btnRemoveImage.Enabled = this.lbFileName.Text != string.Empty;
        }

        private void chkBxShowDateTimeAdder_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerBirthDate.Visible = this.chkBxShowDateTimeAdder.Checked;

            this.SetbtnAddEnabledStatus();
        }

        private void rdBtnAddNew_CheckedChanged(object sender, EventArgs e)
        {
            this.SetFunctionType();
        }

        private void MakeEditable(SCPlayer player)
        {
            EloImage portrait;

            if (this.picBxCurrentImage.Image != null) { this.SetCurrentImageToNull(); }


            if (this.EloDataSource().TryGetImage(player.ImageID, out portrait))
            {
                this.tblLoPnlCurrentImage.Enabled = true;
                this.picBxCurrentImage.Image = portrait.Image;
            }
            else { this.tblLoPnlCurrentImage.Enabled = false; }

            this.txtBxName.Text = player.Name;
            this.txtBxIRLName.Text = player.IRLName;

            DateTime birthDate;

            if (player.TryGetBirthDate(out birthDate))
            {
                this.dateTimePickerBirthDate.Value = birthDate;
                this.chkBxShowDateTimeAdder.Checked = true;
            }
            else
            {
                this.dateTimePickerBirthDate.Value = DateTime.Today.AddYears(-PlayerEditor.DEFAULT_PLAYERAGE);
                this.chkBxShowDateTimeAdder.Checked = false;
            }

            this.lstViewAliases.Items.Clear();

            if (player.GetAliases().Any())
            {
                foreach (string alias in player.GetAliases()) { this.lstViewAliases.Items.Add(alias); }

                this.btnRemoveAlias.Enabled = true;
            }
            else { this.btnRemoveAlias.Enabled = false; }

            this.SetCurrentCountry(player.Country);
            this.SetCurrentTeam(player.Team);
            this.numUDStartRating.Value = player.RatingTotal();

            this.txtBxAlias.Text = string.Empty;
            this.lbFileName.Text = string.Empty;

            this.lbCurrentImage.Enabled = true;
            this.chckBxRemoveCurrentImage.Checked = false;

            this.txtBxName.Enabled = true;
            this.txtBxIRLName.Enabled = true;
            this.tblLoPnlBirthDate.Enabled = true;
            this.tblLoPnlAliases.Enabled = true;
            this.tblLoPnlImage.Enabled = true;
            this.btnBrowse.Enabled = true;
            this.ImgCmbBxCountries.Enabled = true;
            this.ImgCmbBxTeams.Enabled = true;
            this.numUDStartRating.Enabled = false;
            this.btnAdd.Enabled = true;
        }

        private void SetCurrentCountry(Country currentCountry)
        {
            if (currentCountry == null) { this.ImgCmbBxCountries.SelectedIndex = 0; }
            else
            {
                int index = this.ImgCmbBxCountries.Items.Cast<Tuple<string, Country, Image>>().TakeWhile(item => item.Item2 != currentCountry).Count();

                this.ImgCmbBxCountries.SelectedIndex = index >= this.ImgCmbBxCountries.Items.Count ? 0 : index;
            }

        }

        private void SetCurrentTeam(Team currentTeam)
        {
            if (currentTeam == null) { this.ImgCmbBxTeams.SelectedIndex = 0; }
            else
            {
                int index = this.ImgCmbBxTeams.Items.Cast<Tuple<string, Team, Image>>().TakeWhile(item => item.Item2 != currentTeam).Count();

                this.ImgCmbBxTeams.SelectedIndex = index >= this.ImgCmbBxTeams.Items.Count ? 0 : index;
            }

        }

        private void SetFunctionType()
        {
            this.lbCurrentImage.Enabled = false;
            this.tblLoPnlCurrentImage.Enabled = false;
            this.SetCurrentImageToNull();
            this.chckBxRemoveCurrentImage.Checked = false;

            if (this.rdBtnAddNew.Checked)
            {
                this.ClearFields();

                this.txtBxFilter.Enabled = false;
                this.btnSearch.Enabled = false;
                this.lstVSearchResults.Enabled = false;

                this.txtBxName.Enabled = true;
                this.txtBxIRLName.Enabled = true;
                this.tblLoPnlBirthDate.Enabled = true;
                this.tblLoPnlAliases.Enabled = true;
                this.tblLoPnlImage.Enabled = true;
                this.btnBrowse.Enabled = true;
                this.ImgCmbBxCountries.Enabled = true;
                this.ImgCmbBxTeams.Enabled = true;
                this.numUDStartRating.Enabled = true;

                this.btnAdd.Text = "Add to system";
            }
            else if (this.rdBtnEdit.Checked)
            {
                this.txtBxFilter.Enabled = true;
                this.btnSearch.Enabled = true;
                this.lstVSearchResults.Enabled = true;

                this.txtBxName.Enabled = false;
                this.txtBxIRLName.Enabled = false;
                this.tblLoPnlBirthDate.Enabled = false;
                this.tblLoPnlAliases.Enabled = false;
                this.tblLoPnlImage.Enabled = false;
                this.btnBrowse.Enabled = false;
                this.ImgCmbBxCountries.Enabled = false;
                this.ImgCmbBxTeams.Enabled = false;
                this.numUDStartRating.Enabled = false;
                this.btnAdd.Enabled = false;

                this.btnAdd.Text = "Save changes";
            }
        }

        private void SetCurrentImageToNull()
        {
            if (this.picBxCurrentImage.Image != null)
            {
                this.picBxCurrentImage.Image.Dispose();
                this.picBxCurrentImage.Image = null;
            }
        }

        private void rdBtnEdit_CheckedChanged(object sender, EventArgs e)
        {
            this.lstVSearchResults.SelectedItems.Clear();

            this.SetFunctionType();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchPlayersWithPattern();
        }

        private void SearchPlayersWithPattern()
        {
            this.AddPlayersToListView(this.EloDataSource().FindPlayerMatches(this.txtBxFilter.Text).OrderBy(player => player.Name));
        }

        private void AddPlayersToListView(IEnumerable<SCPlayer> players)
        {
            this.lstVSearchResults.SetObjects(players);
        }

        private void txtBxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) { this.SearchPlayersWithPattern(); }
        }

        private void txtBxIRLName_TextChanged(object sender, EventArgs e)
        {
            this.SetbtnAddEnabledStatus();
        }

        private void dateTimePickerBirthDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerBirthDate.Visible) { this.SetbtnAddEnabledStatus(); }
        }

        private void SetImageVisibility()
        {
            this.picBxCurrentImage.Visible = this.chckBxRemoveCurrentImage.Checked == false;
        }

        private void chckBxRemoveCurrentImage_CheckedChanged(object sender, EventArgs e)
        {
            this.SetImageVisibility();

            this.SetbtnAddEnabledStatus();
        }
    }
}