using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class ContentAdder : UserControl
    {
        private const string DEFAULT_TXTBX_TEXT = "Type the name here...";

        internal ContentTypes ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.lbHeading.Text = String.Format("Create new {0}", value.ToString().ToLower());

                this.contentType = value;
            }
        }
        internal event EventHandler<ContentAddingEventArgs> OnAddButtonClick = delegate { };
        internal Image SelectedImage { get; private set; }
        internal string ContentName
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

            this.txtBxName.Text = ContentAdder.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }

        [STAThread]
        private static bool TryGetFilePathFromUser(out string filePath)
        {
            filePath = "";

            while (true)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select image";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
                openFileDialog.Filter = "Image files|*.gif;*.bmp;*.jpg;*.jpeg;*.png*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    return true;
                }
                else { return false; }

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            if (ContentAdder.TryGetFilePathFromUser(out filePath))
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
            this.txtBxName.Text = ContentAdder.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;            
        }
    }
}
