using EloSystem;
using EloSystem.ResourceManagement;
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
    public partial class DblNameContentEditor<T> : UserControl where T : EloSystemContent, IHasDblName
    {
        public bool RemoveCurrentImage
        {
            get
            {
                return this.chckBxRemoveCurrentImage.Checked;
            }
        }
        public EventHandler EditButtonClicked = delegate { };
        public Image NewImage { get; private set; }
        public ImageGetter ResourceGetter { private get; set; }
        public string ContentName
        {
            set
            {
                this.lbHeading.Text = String.Format("Edit {0}", value);
                this.lbSelectHeading.Text = String.Format("Select {0}", value);
            }
        }
        public string NameLong
        {
            get
            {
                return this.txtBxNameLong.Text;
            }
        }
        public string NameShort
        {
            get
            {
                return this.txtBxNameShort.Text;
            }
        }
        public T SelectedItem
        {
            get
            {
                return this.cmbBxContent.SelectedValue as T;
            }
        }
        private ImageComboBoxImprovedItemHandling cmbBxContent;

        public DblNameContentEditor()
        {
            InitializeComponent();

            this.cmbBxContent = EloGUIControlsStaticMembers.CreateStandardImprovedImageComboBox();
            this.tblLoPnlMain.Controls.Add(this.cmbBxContent, 1, 1);
            this.cmbBxContent.SelectedIndexChanged += this.CmbBxContent_SelectedIndexChanged;
        }

        private void CmbBxContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedItem == null) { this.ResetControls(); }
            else
            {
                this.txtBxNameShort.Text = this.SelectedItem.Name;
                this.txtBxNameLong.Text = this.SelectedItem.NameLong;
                this.picBxCurrentImage.Image = this.ResourceGetter(this.SelectedItem.ImageID);

                this.SetImageVisibility();
                this.SetControlsEnabledStatus();
            }
        }

        private void ResetControls()
        {
            this.txtBxNameShort.Text = string.Empty;
            this.txtBxNameLong.Text = string.Empty;
            this.lbFileName.Text = string.Empty;
            this.picBxCurrentImage.Image = null;
            this.chckBxRemoveCurrentImage.Checked = false;

            this.SetImageVisibility();

            this.SetControlsEnabledStatus();
        }

        private void SetControlsEnabledStatus()
        {
            this.btnRemoveImage.Enabled = this.lbFileName.Text != string.Empty;

            this.chckBxRemoveCurrentImage.Enabled = this.picBxCurrentImage.Image != null;

            this.btnEdit.Enabled = this.SelectedItem != null && (this.txtBxNameShort.Text != this.SelectedItem.Name || this.txtBxNameLong.Text != this.SelectedItem.NameLong || this.lbFileName.Text != string.Empty || this.chckBxRemoveCurrentImage.Checked);
        }

        internal void AddContentItems(IEnumerable<T> contentItems, ImageGetter resourceGetter)
        {
            this.cmbBxContent.AddItems<T>(contentItems.ToArray(), resourceGetter, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditButtonClicked.Invoke(this, e);

            this.lbFileName.Text = string.Empty;

            this.SetControlsEnabledStatus();
        }

        private void SetImageVisibility()
        {
            this.picBxCurrentImage.Visible = this.chckBxRemoveCurrentImage.Checked == false;
        }

        private void chckBxRemoveCurrentImage_CheckedChanged(object sender, EventArgs e)
        {
            this.SetImageVisibility();

            this.SetControlsEnabledStatus();
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

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            this.SetControlsEnabledStatus();
        }
    }
}
