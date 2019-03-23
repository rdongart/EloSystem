using CustomExtensionMethods;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace SCEloSystemGUI.UserControls
{
    public partial class MapAdder : UserControl, IContentAdder
    {
        internal const string DEFAULT_TXTBXALIAS_TEXT = "Type description here...";

        internal Tileset SelectedTileset
        {
            get
            {
                return this.cmbBxTileset.SelectedItem as Tileset;
            }
        }
        private ContentTypes contentType;
        public ContentTypes ContentType
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
        public event EventHandler<ContentAddingEventArgs> OnAddPlayer = delegate { };
        public Image NewImage { get; private set; }
        public MapPlayerType MapType
        {
            get
            {
                return (this.cmbBxMapType.SelectedItem as Tuple<string, MapPlayerType>).Item2;
            }
        }
        public Size MapSize
        {
            get
            {
                return new Size((int)this.numUDWidth.Value, (int)this.numUDHeight.Value);
            }
        }
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }

        public MapAdder()
        {
            InitializeComponent();

            this.cmbBxMapType.DisplayMember = "Item1";
            this.cmbBxMapType.ValueMember = "Item2";

            this.cmbBxMapType.Items.AddRange(Enum.GetValues(typeof(MapPlayerType)).Cast<MapPlayerType>().Select(mapType => Tuple.Create<string, MapPlayerType>(mapType.ToString().Replace("_", " ").Substring(1), mapType)).ToArray());

            this.cmbBxMapType.SelectedIndex = 0;
        }

        private void txtBxDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxDescription.Text != string.Empty) { this.btnAddDescription.Enabled = true; }
            else { this.btnAddDescription.Enabled = false; }
        }

        private void btnAddDescription_Click(object sender, EventArgs e)
        {
            this.lstViewDescriptions.Items.Add(this.txtBxDescription.Text);

            this.txtBxDescription.Text = string.Empty;
            this.btnAddDescription.Enabled = false;
        }

        private void lstViewDescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstViewDescriptions.SelectedItems.Count > 0) { this.btnRemoveDescription.Enabled = true; }
            else { this.btnRemoveDescription.Enabled = false; }
        }

        private void btnRemoveDescription_Click(object sender, EventArgs e)
        {
            List<ListViewItem> itemsToRemove = this.lstViewDescriptions.SelectedItems.Cast<ListViewItem>().ToList();

            this.lstViewDescriptions.SelectedItems.Clear();

            foreach (ListViewItem item in itemsToRemove) { this.lstViewDescriptions.Items.Remove(item); }

            this.btnRemoveDescription.Enabled = false;
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
                this.NewImage = Bitmap.FromFile(filePath);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddPlayer.Invoke(sender, new ContentAddingEventArgs(this));

            if (this.NewImage != null) { this.NewImage.Dispose(); }

            this.NewImage = null;

            this.lbFileName.Text = string.Empty;
            this.txtBxName.Text = string.Empty;
            this.txtBxDescription.Text = string.Empty;
            this.lstViewDescriptions.Items.Clear();
            this.cmbBxTileset.SelectedIndex = -1;
            this.btnAdd.Enabled = false;
            this.btnAddDescription.Enabled = false;
            this.btnRemoveDescription.Enabled = false;
        }

        public IEnumerable<string> GetAliases()
        {
            foreach (string alias in this.lstViewDescriptions.Items.Cast<ListViewItem>().Select(item => item.Text).ToList()) { yield return alias; }
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
    }
}
