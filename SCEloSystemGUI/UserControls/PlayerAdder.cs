using EloSystem;
using CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal partial class PlayerAdder : UserControl, IContentAdder
    {
        internal const string DEFAULT_TXTBXALIAS_TEXT = "Type alias here...";

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
        internal Country SelectedCountry { get; private set; }
        public event EventHandler<ContentAddingEventArgs> OnAddButtonClick = delegate { };
        public Image SelectedImage { get; private set; }
        public ImageComboBox ImgCmbBxCountries { get; private set; }
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        private ContentTypes contentType;


        internal PlayerAdder()
        {
            InitializeComponent();

            this.txtBxName.Text = StaticMembers.DEFAULT_TXTBX_TEXT;
            this.txtBxAlias.Text = PlayerAdder.DEFAULT_TXTBXALIAS_TEXT;
            this.btnAdd.Enabled = false;
            this.btnAddAlias.Enabled = false;

            this.ImgCmbBxCountries = new ImageComboBox()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 154,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ItemHeight = 18,
                Size = new Size(154, 24),
                Margin = new Padding(6, 3, 6, 3),
                ImageMargin = new Padding(4, 2, 4, 2)

            };
            this.ImgCmbBxCountries.SelectionChangeCommitted += this.ImCmbBxCountries_SelectionChangeCommitted;
            this.tblLOPnlPlayerAdder.Controls.Add(this.ImgCmbBxCountries, 1, 4);
        }

        private void ImCmbBxCountries_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SelectedCountry = this.ImgCmbBxCountries.SelectedValue as Country;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            if (StaticMembers.TryGetFilePathFromUser(out filePath))
            {
                this.lbFileName.Text = filePath;
                this.SelectedImage = Bitmap.FromFile(filePath);
            }

        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAdd.Enabled = true; }
            else { this.btnAdd.Enabled = false; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddButtonClick.Invoke(sender, new ContentAddingEventArgs(this));

            this.lbFileName.Text = string.Empty;
            this.txtBxName.Text = StaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
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
    }
}
