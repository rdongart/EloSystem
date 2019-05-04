using BrightIdeasSoftware;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using EloSystem.ResourceManagement;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System;

namespace SCEloSystemGUI.UserControls
{
    public delegate T ContentGetter<T>(SCPlayer player) where T : EloSystemContent;

    public partial class ContentFilter<T> : UserControl, IPlayerFilter where T : EloSystemContent
    {
        private const int IMAGE_SIZE_DEFAULT = 24;
        private const int ROW_HEIGHT_DEFAULT = 22;

        private bool acceptNullApplied;
        private List<T> selectedContentApplied;
        private ObjectListView contentOLV;
        private ResourceGetter eloDataBase;
        private string columnHeader;
        private string header;
        public ContentGetter<T> ContentGetter { get; set; }
        public int ImageDimensionMax { get; set; }
        public int RowHeight { get; set; }
        public string Header
        {
            get
            {
                return this.header;
            }
            set
            {
                this.lbHeader.Text = value;

                this.header = value;
            }
        }
        public string ColumnHeader
        {
            get
            {
                return this.columnHeader;
            }
            set
            {
                this.contentOLV.Columns[2].Text = value;

                this.columnHeader = value;
            }
        }
        public event EventHandler FilterChanged = delegate { };

        public ContentFilter(ResourceGetter databaseGetter)
        {
            InitializeComponent();

            this.ImageDimensionMax = ContentFilter<T>.IMAGE_SIZE_DEFAULT;
            this.RowHeight = ContentFilter<T>.ROW_HEIGHT_DEFAULT;

            this.selectedContentApplied = new List<T>();

            this.eloDataBase = databaseGetter;

            this.contentOLV = this.CreateContentFilterListView();
            this.contentOLV.ItemChecked += this.ContentOLV_ItemChecked;

            this.tblLoPnlMain.Controls.Add(this.contentOLV, 0, 1);
            this.tblLoPnlMain.SetRowSpan(this.contentOLV, 3);

            this.SetBtnEnabledStatus();
        }

        private static void ContentFilterOLV_CellClick(object sender, CellClickEventArgs e)
        {
            e.Item.Checked = !e.Item.Checked;
        }

        private static void ContentFilterOLV_SelectionChanged(object sender, EventArgs e)
        {
            var objList = sender as ObjectListView;

            if (objList == null) { return; }

            foreach (ListViewItem item in objList.Items) { item.Checked = item.Selected; }

        }
        
        private void ContentOLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, e);
        }

        public bool PlayerFilter(SCPlayer player)
        {
            if (this.ContentGetter == null) { return true; }
            else
            {
                T contentForPlayer = this.ContentGetter(player);

                return (contentForPlayer == null && this.acceptNullApplied) || this.selectedContentApplied.Contains(contentForPlayer);
            }

        }

        private ObjectListView CreateContentFilterListView()
        {
            var contentFilterOLV = new ObjectListView()
            {
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                CheckBoxes = true,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 10.5f, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = true,
                RowHeight = this.RowHeight,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(186, 300),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = false,
            };

            contentFilterOLV.CellClick += ContentFilter<T>.ContentFilterOLV_CellClick;
  
            const int CLM_CHECKBOX_WIDTH = 21;
            const int CLM_IMAGE_WIDTH = 40;
            const int CLM_NAME_WIDTH = 110;

            var olvClmCheckBox = new OLVColumn() { MaximumWidth = CLM_CHECKBOX_WIDTH, Width = CLM_CHECKBOX_WIDTH, MinimumWidth = CLM_CHECKBOX_WIDTH, CellPadding = null, Text = "" };
            var olvClmImage = new OLVColumn() { Width = CLM_IMAGE_WIDTH, MinimumWidth = CLM_IMAGE_WIDTH, MaximumWidth = CLM_IMAGE_WIDTH, Text = "" };
            var olvClmName = new OLVColumn() { Width = CLM_NAME_WIDTH, MinimumWidth = CLM_NAME_WIDTH, MaximumWidth = CLM_NAME_WIDTH, Text = "" };

            contentFilterOLV.AllColumns.AddRange(new OLVColumn[] { olvClmCheckBox, olvClmImage, olvClmName });

            contentFilterOLV.Columns.AddRange(new ColumnHeader[] { olvClmCheckBox, olvClmImage, olvClmName });
            
            olvClmImage.AspectGetter = obj =>
            {
                var content = obj as T;

                EloImage image;

                if (this.eloDataBase().TryGetImage(content.ImageID, out image)) { return new Image[] { image.Image.ResizeSameAspectRatio(this.ImageDimensionMax) }; }
                else { return null; }

            };

            var imageRenderer = new ImageRenderer() { Bounds = new Rectangle(3, 2, 3, 3) };
            olvClmImage.Renderer = imageRenderer;

            olvClmName.AspectGetter = obj =>
            {
                var content = obj as T;

                return content.Name;
            };

            return contentFilterOLV;
        }

        private IEnumerable<T> GetAllowedContent()
        {
            return this.contentOLV.CheckedItems.Cast<OLVListItem>().Select(lvItem => lvItem.RowObject as T);
        }

        public void SetItems(IEnumerable<T> items)
        {
            bool listWasEmpty = this.contentOLV.Items.Count == 0;

            this.contentOLV.SetObjects(items.ToArray());

            if (listWasEmpty)
            {
                this.SetAllSelectionsTo(true);
                this.ApplyChanges();
            }
            else { foreach (OLVListItem item in this.contentOLV.Items) { if (this.selectedContentApplied.Contains(item.RowObject)) { item.Checked = true; } } }
        }

        public bool HasChangesNotApplied()
        {
            return this.cbAllowEmpty.Checked != this.acceptNullApplied || this.GetAllowedContent().Count() != this.selectedContentApplied.Count
                || this.GetAllowedContent().Any(item => !this.selectedContentApplied.Contains(item));
        }

        public void ApplyChanges()
        {
            this.acceptNullApplied = this.cbAllowEmpty.Checked;

            this.selectedContentApplied = this.GetAllowedContent().ToList();
        }

        private void cbAllowEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterChanged.Invoke(this, e);

            this.SetBtnEnabledStatus();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(true);
        }

        private void SetAllSelectionsTo(bool selectionState)
        {
            foreach (OLVListItem item in this.contentOLV.Items) { item.Checked = selectionState; }

            this.cbAllowEmpty.Checked = selectionState;

            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, new EventArgs());
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(false);
        }

        private void SetBtnEnabledStatus()
        {
            this.btnSelectAll.Enabled = !this.cbAllowEmpty.Checked || this.contentOLV.Items.Cast<OLVListItem>().Any(item => !item.Checked);

            this.btnDeselectAll.Enabled = this.cbAllowEmpty.Checked || this.contentOLV.Items.Cast<OLVListItem>().Any(item => item.Checked);
        }
    }
}
