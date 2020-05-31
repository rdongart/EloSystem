using CustomControls;
using EloSystem;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class SingleNameContentEditor<T> : UserControl where T : EloSystemContent
    {
        private ImageGetter<T> imageGetter;
        private ContentGetterDelegate<T> contentGetter;
        public bool RemoveCurrentImage
        {
            get
            {
                return this.chckBxRemoveCurrentImage.Checked;
            }
        }
        public ContentGetterDelegate<T> ContentGetter
        {
            private get
            {
                return this.contentGetter;
            }
            set
            {
                this.contentGetter = value;

                this.UpdateItems();
            }
        }
        public ContentRemoveCondition<T> RemoveCondition { private get; set; }
        public EventHandler EditButtonClicked = delegate { };
        public EventHandler RemoveButtonClicked = delegate { };
        public Image NewImage { get; private set; }
        public ImageGetter<T> ImageGetter
        {
            private get
            {
                return this.imageGetter;
            }
            set
            {
                this.imageGetter = value;

                this.cmbBxContent.ImageGetter = this.imageGetter;

                this.UpdateItems();
            }
        }
        public string ContentName
        {
            set
            {
                this.lbHeading.Text = String.Format("Edit {0}", value);
                this.lbSelectHeading.Text = String.Format("Select {0}", value);
            }
        }
        public string ItemName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        public T SelectedItem
        {
            get
            {
                return this.cmbBxContent.SelectedValue as T;
            }
        }
        private ImprovedImageComboBox<T> cmbBxContent;

        public SingleNameContentEditor()
        {
            InitializeComponent();

            this.cmbBxContent = EloGUIControlsStaticMembers.CreateStandardImprovedImageComboBox<T>(null);
            this.tblLoPnlMain.Controls.Add(this.cmbBxContent, 1, 1);
            this.cmbBxContent.SelectedIndexChanged += this.CmbBxContent_SelectedIndexChanged;
        }

        private void CmbBxContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedItem == null) { this.ResetControls(); }
            else
            {
                this.txtBxName.Text = this.SelectedItem.Name;
                this.picBxCurrentImage.Image = this.ImageGetter(this.SelectedItem);

                this.SetImageVisibility();
                this.SetControlsEnabledStatus();
            }
        }

        private void ResetControls()
        {
            this.txtBxName.Text = string.Empty;
            this.lbFileName.Text = string.Empty;
            this.picBxCurrentImage.Image = null;
            this.chckBxRemoveCurrentImage.Checked = false;

            this.SetImageVisibility();

            this.SetControlsEnabledStatus();
        }

        private void SetControlsEnabledStatus()
        {
            this.btnRemoveImage.Enabled = this.lbFileName.Text != string.Empty;

            this.txtBxName.Enabled = this.SelectedItem != null;
            this.btnBrowse.Enabled = this.SelectedItem != null;

            this.chckBxRemoveCurrentImage.Enabled = this.picBxCurrentImage.Image != null;

            this.btnEdit.Enabled = this.SelectedItem != null && (this.txtBxName.Text != this.SelectedItem.Name || this.lbFileName.Text != string.Empty || this.chckBxRemoveCurrentImage.Checked);

            this.btnRemove.Enabled = this.SelectedItem != null && (this.RemoveCondition == null || this.RemoveCondition(this.SelectedItem));
        }

        public void UpdateItems()
        {
            if (this.ContentGetter != null) { this.cmbBxContent.AddItems(this.ContentGetter().ToArray(), this.ImageGetter, false); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditButtonClicked.Invoke(this, e);

            this.lbFileName.Text = string.Empty;

            this.chckBxRemoveCurrentImage.Checked = false;

            this.UpdateItems();

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

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            this.lbFileName.Text = string.Empty;

            this.btnRemoveImage.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.RemoveButtonClicked.Invoke(this, new EventArgs());

            this.lbFileName.Text = string.Empty;

            this.chckBxRemoveCurrentImage.Checked = false;

            this.UpdateItems();

            this.SetControlsEnabledStatus();
        }

        private void lbFileName_TextChanged(object sender, EventArgs e)
        {
            this.SetControlsEnabledStatus();
        }
    }
}
