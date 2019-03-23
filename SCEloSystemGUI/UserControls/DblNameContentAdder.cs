using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class DblNameContentAdder : UserControl, IContentAdder
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
                return this.NameShort;
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
        private ContentTypes contentType;

        public DblNameContentAdder()
        {
            InitializeComponent();
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

        private void txtBxNameShort_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxNameShort.Text != string.Empty) { this.btnAdd.Enabled = true; }
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
            this.txtBxNameShort.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.txtBxNameLong.Text = string.Empty;
            this.btnAdd.Enabled = false;
        }
    }
}
