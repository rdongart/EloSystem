using CustomControls;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CustomExtensionMethods;

namespace SCEloSystemGUI.UserControls
{
    internal partial class PlayerAdder : UserControl, IContentAdder
    {
        internal const string DEFAULT_TXTBXALIAS_TEXT = "Type alias here...";
        
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
        internal Team SelectedTeam
        {
            get
            {
                return this.ImgCmbBxTeams.SelectedValue as Team;
            }
        }
        private ContentTypes contentType;
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
                this.lbHeading.Text = String.Format("Create new {0}", value.ToString().ToLower());

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
        public Image SelectedImage { get; private set; }
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

        internal PlayerAdder()
        {
            InitializeComponent();

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.txtBxAlias.Text = PlayerAdder.DEFAULT_TXTBXALIAS_TEXT;
            this.btnAdd.Enabled = false;
            this.btnAddAlias.Enabled = false;

            this.numUDStartRating.Maximum = int.MaxValue;
            this.numUDStartRating.Value = EloSystemStaticMembers.START_RATING_DEFAULT;

            this.ImgCmbBxCountries = PlayerAdder.CreateStandardImageComboBox();
            this.ImgCmbBxCountries.TabIndex = 7;
            this.tblLOPnlPlayerAdder.Controls.Add(this.ImgCmbBxCountries, 1, 6);

            this.ImgCmbBxTeams = PlayerAdder.CreateStandardImageComboBox();
            this.ImgCmbBxTeams.TabIndex = 8;
            this.tblLOPnlPlayerAdder.Controls.Add(this.ImgCmbBxTeams, 1, 7);
        }

        private static ImageComboBox CreateStandardImageComboBox()
        {
            return new ImageComboBox()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 154,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(4, 2, 4, 2),
                ItemHeight = 18,
                Margin = new Padding(6, 3, 6, 3),
                SelectedItemFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(154, 24),
            };
        }

        private void txtBxAlias_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxAlias.Text != string.Empty) { this.btnAddAlias.Enabled = true; }
            else { this.btnAddAlias.Enabled = false; }
        }

        private void btnAddAlias_Click(object sender, EventArgs e)
        {
            this.lstViewAliases.Items.Add(this.txtBxAlias.Text);

            this.txtBxAlias.Text = PlayerAdder.DEFAULT_TXTBXALIAS_TEXT;
            this.btnAddAlias.Enabled = false;
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
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAdd.Enabled = true; }
            else { this.btnAdd.Enabled = false; }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            if (EloGUIControlsStaticMembers.TryGetFilePathFromUser(out filePath))
            {
                this.lbFileName.Text = filePath;
                this.SelectedImage = Bitmap.FromFile(filePath);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddButtonClick.Invoke(sender, new ContentAddingEventArgs(this));

            if (this.SelectedImage != null) { this.SelectedImage.Dispose(); }

            this.lbFileName.Text = string.Empty;

            this.txtBxIRLName.Text = string.Empty;
            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.txtBxAlias.Text = PlayerAdder.DEFAULT_TXTBXALIAS_TEXT;
            this.lstViewAliases.Items.Clear();
            this.ImgCmbBxCountries.SelectedIndex = -1;
            this.ImgCmbBxTeams.SelectedIndex = -1;
            this.numUDStartRating.Value = EloSystemStaticMembers.START_RATING_DEFAULT;
            this.btnAdd.Enabled = false;
            this.btnAddAlias.Enabled = false;
            this.btnRemoveAlias.Enabled = false;
            this.chkBxShowDateTimeAdder.Checked = false;
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
        }
    }
}
