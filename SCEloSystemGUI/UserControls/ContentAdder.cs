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
        public event EventHandler<ContentAddingEventArgs> OnAddButtonClick = delegate { };
        public Image SelectedImage { get; private set; }
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

            this.txtBxName.Text = StaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
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
    }
}
