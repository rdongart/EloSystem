using CustomExtensionMethods;
using EloSystem;
using EloSystem.ResourceManagement;
using EloSystemExtensions;
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
        private const string ADD_MAP_BUTTON_TEST = "A&dd to system";
        private const string EDIT_MAP_BUTTON_TEST = "&Keep edit";

        internal Tileset SelectedTileset
        {
            get
            {
                return this.cmbBxTileset.SelectedItem != null ? (this.cmbBxTileset.SelectedItem as Tuple<string, Tileset>).Item2 : null;
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
                this.lbHeading.Text = String.Format("Edit or add new {0}", value.ToString().ToLower());

                this.contentType = value;
            }
        }
        public event EventHandler<ContentAddingEventArgs> OnAddMap = delegate { };
        public event EventHandler OnRemoveButtonClick = delegate { };
        public event EventHandler OnEditButtonClick = delegate { };
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

            EloGUIControlsStaticMembers.PopulateComboboxWithMaps(this.cmbBxMapsToEdit, GlobalState.DataBase.GetMaps(), false);

            this.rdBtnAddNew.Checked = true;
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

            this.SetbtnAddRemoveEnabledStatus();
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

            this.SetbtnAddRemoveEnabledStatus();
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAddEditMap.Enabled = true; }
            else { this.btnAddEditMap.Enabled = false; }

            this.SetbtnAddRemoveEnabledStatus();
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

        private void btnAddEdit_Click(object sender, EventArgs e)
        {
            if (this.rdBtnAddNew.Checked)
            {
                this.OnAddMap.Invoke(sender, new ContentAddingEventArgs(this));

                this.ClearControls();
            }
            else if (this.rdBtnEdit.Checked && this.cmbBxMapsToEdit.SelectedIndex > -1)
            {
                Map mapToEdit = (this.cmbBxMapsToEdit.SelectedItem as Tuple<string, Map>).Item2;

                mapToEdit.Name = this.txtBxName.Text;
                mapToEdit.SetDescriptions(this.lstViewDescriptions.Items.Cast<ListViewItem>().Select(item => item.Text));

                if (this.NewImage != null) { GlobalState.DataBase.EditImage(mapToEdit, this.NewImage); }
                else if (this.chckBxRemoveCurrentImage.Checked) { GlobalState.DataBase.EditImage(mapToEdit, null); }

                mapToEdit.MapType = this.MapType;
                mapToEdit.Tileset = this.SelectedTileset;
                mapToEdit.Size = this.MapSize;

                this.OnEditButtonClick.Invoke(this, new EventArgs());

                this.ClearNewImage();

                this.MakeEditable(mapToEdit);
            }

            this.SetbtnAddRemoveEnabledStatus();
        }

        private void ClearNewImage()
        {
            if (this.NewImage != null)
            {
                this.NewImage.Dispose();
                this.NewImage = null;
            }
        }

        private void chckBxRemoveCurrentImage_CheckedChanged(object sender, EventArgs e)
        {
            this.SetImageVisibility();

            this.SetbtnAddRemoveEnabledStatus();
        }

        /// <summary>
        /// Set the enabled status of buttons for adding and removing maps to/from the elo system
        /// </summary>
        private void SetbtnAddRemoveEnabledStatus()
        {
            this.btnRemoveMap.Enabled = false;

            if (this.rdBtnAddNew.Checked) { this.btnAddEditMap.Enabled = this.txtBxName.Text != string.Empty; }
            else if (this.rdBtnEdit.Checked && this.cmbBxMapsToEdit.SelectedIndex > -1)
            {
                var mapToEdit = (this.cmbBxMapsToEdit.SelectedItem as Tuple<string, Map>).Item2;

                if (mapToEdit == null) { this.btnAddEditMap.Enabled = false; }
                else
                {
                    string[] newDescriptions = this.lstViewDescriptions.Items.Cast<ListViewItem>().Select(item => item.Text).ToArray();

                    this.btnAddEditMap.Enabled = mapToEdit.Name != this.txtBxName.Text
                        || newDescriptions.Any(description => !mapToEdit.GetDescriptions().Contains(description))
                        || mapToEdit.GetDescriptions().Any(description => !newDescriptions.Contains(description))
                        || this.lbFileName.Text != string.Empty
                        || this.chckBxRemoveCurrentImage.Checked
                        || (this.cmbBxMapType.SelectedIndex > -1 && (this.cmbBxMapType.SelectedItem as Tuple<string, MapPlayerType>).Item2 != mapToEdit.MapType)
                        || (this.cmbBxTileset.SelectedIndex > -1 && (this.cmbBxTileset.SelectedItem as Tuple<string, Tileset>).Item2 != mapToEdit.Tileset)
                        || !mapToEdit.Size.Equals(new Size(this.numUDWidth.Value.RoundToInt(), this.numUDHeight.Value.RoundToInt()));

                    // we can't remove a map from the data base if it is still associated with any games

                    this.btnRemoveMap.Enabled = GlobalState.DataBase.GamesOnMap(mapToEdit).IsEmpty();
                }
            }
            else { this.btnAddEditMap.Enabled = false; }

        }

        private void SetImageVisibility()
        {
            this.picBxCurrentImage.Visible = this.chckBxRemoveCurrentImage.Checked == false;
        }

        private void ClearControls()
        {
            if (this.NewImage != null) { this.NewImage.Dispose(); }

            this.NewImage = null;

            this.lbFileName.Text = string.Empty;
            this.txtBxName.Text = string.Empty;
            this.txtBxDescription.Text = string.Empty;
            this.lstViewDescriptions.Items.Clear();
            this.cmbBxTileset.SelectedIndex = -1;
            this.btnAddEditMap.Enabled = false;
            this.btnAddDescription.Enabled = false;
            this.btnRemoveDescription.Enabled = false;

            this.SetCurrentImageToNull();
        }

        private void SetCurrentImageToNull()
        {
            if (this.picBxCurrentImage.Image != null)
            {
                this.picBxCurrentImage.Image.Dispose();
                this.picBxCurrentImage.Image = null;
            }
        }

        public IEnumerable<string> GetDescriptions()
        {
            foreach (string description in this.lstViewDescriptions.Items.Cast<ListViewItem>().Select(item => item.Text).ToList()) { yield return description; }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            this.lbFileName.Text = string.Empty;

            this.btnRemoveImage.Enabled = false;
        }

        private void lbFileName_TextChanged(object sender, EventArgs e)
        {
            this.btnRemoveImage.Enabled = this.lbFileName.Text != string.Empty;

            if (this.rdBtnEdit.Checked && this.cmbBxMapsToEdit.SelectedIndex > -1) { this.SetbtnAddRemoveEnabledStatus(); }
        }

        private void rdBtnSelectMode_CheckedChanged(object sender, EventArgs e)
        {
            this.SetFunctionType();
        }

        private void SetFunctionType()
        {
            this.lbCurrentImage.Enabled = false;
            this.tblLoPnlCurrentImage.Enabled = false;
            this.SetCurrentImageToNull();
            this.chckBxRemoveCurrentImage.Checked = false;

            if (this.rdBtnAddNew.Checked)
            {
                this.btnAddEditMap.Text = MapAdder.ADD_MAP_BUTTON_TEST;

                this.ClearControls();
            }
            else if (this.rdBtnEdit.Checked)
            {
                this.btnAddEditMap.Text = MapAdder.EDIT_MAP_BUTTON_TEST;

                EloGUIControlsStaticMembers.PopulateComboboxWithMaps(this.cmbBxMapsToEdit, GlobalState.DataBase.GetMaps(), false);
            }

            this.cmbBxMapsToEdit.SelectedIndex = -1;
            this.cmbBxMapsToEdit.Enabled = this.rdBtnEdit.Checked;
        }

        private void MakeEditable(Map map)
        {
            EloImage mapImg;

            if (this.picBxCurrentImage.Image != null) { this.SetCurrentImageToNull(); }


            if (GlobalState.DataBase.TryGetImage(map.ImageID, out mapImg))
            {
                this.tblLoPnlCurrentImage.Enabled = true;
                this.picBxCurrentImage.Image = mapImg.Image;
                this.chckBxRemoveCurrentImage.Enabled = true;
            }
            else
            {
                this.tblLoPnlCurrentImage.Enabled = false;
                this.chckBxRemoveCurrentImage.Enabled = false;
            }

            this.txtBxName.Text = map.Name;
            this.lstViewDescriptions.Items.Clear();

            if (map.GetDescriptions().Any())
            {
                foreach (string description in map.GetDescriptions()) { this.lstViewDescriptions.Items.Add(description); }

                this.btnRemoveDescription.Enabled = true;
            }
            else { this.btnRemoveDescription.Enabled = false; }

            this.txtBxDescription.Text = string.Empty;
            this.lbFileName.Text = string.Empty;

            this.lbCurrentImage.Enabled = true;
            this.chckBxRemoveCurrentImage.Checked = false;

            this.cmbBxMapType.SelectedIndex = this.cmbBxMapType.Items.IndexOf(this.cmbBxMapType.Items.Cast<Tuple<string, MapPlayerType>>().First(item => item.Item2 == map.MapType));

            if (map.Tileset != null) { this.cmbBxTileset.SelectedIndex = this.cmbBxTileset.Items.IndexOf(this.cmbBxTileset.Items.Cast<Tuple<string, Tileset>>().First(item => item.Item2 == map.Tileset)); }
            else { this.cmbBxTileset.SelectedIndex = -1; }

            this.numUDWidth.Value = map.Size.Width;
            this.numUDHeight.Value = map.Size.Height;

            this.SetbtnAddRemoveEnabledStatus();
        }

        private void cmbBxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbBxMapsToEdit.SelectedIndex > -1) { this.MakeEditable((this.cmbBxMapsToEdit.SelectedItem as Tuple<string, Map>).Item2); }
        }

        new public void Update()
        {
            base.Update();

            this.SetbtnAddRemoveEnabledStatus();
        }

        private void cmbBxTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetbtnAddRemoveEnabledStatus();
        }

        private void cmbBxMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetbtnAddRemoveEnabledStatus();
        }

        private void numUDWidth_ValueChanged(object sender, EventArgs e)
        {
            this.SetbtnAddRemoveEnabledStatus();
        }

        private void numUDHeight_ValueChanged(object sender, EventArgs e)
        {
            this.SetbtnAddRemoveEnabledStatus();
        }
    }
}
