using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal partial class ContentAdder : UserControl, IContentAdder
    {
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
        public event EventHandler<ContentAddingEventArgs> OnAddPlayer = delegate { };
        public Image NewImage { get; private set; }
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        private ContentTypes contentType;

        public ContentAdder()
        {
            InitializeComponent();

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
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

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAdd.Enabled = true; }
            else { this.btnAdd.Enabled = false; }
        }

        private void lbFileName_TextChanged(object sender, EventArgs e)
        {
            this.btnRemoveImage.Enabled = this.lbFileName.Text != string.Empty;
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            this.lbFileName.Text = string.Empty;

            this.btnRemoveImage.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddPlayer.Invoke(sender, new ContentAddingEventArgs(this));

            if (this.NewImage != null) { this.NewImage.Dispose(); }

            this.NewImage = null;

            this.lbFileName.Text = string.Empty;
            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }
    }
}
