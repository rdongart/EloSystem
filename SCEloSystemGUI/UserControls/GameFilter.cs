using CustomControls.Utilities;
using BrightIdeasSoftware;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class GameFilter<T> : UserControl, IGameFilter
    {
        public delegate string NameGetter(T item);
        public delegate Image ImageGetter(T item);
        public delegate T AspectGetter(Game game);

        private const int ROW_HEIGHT_DEFAULT = 22;
        private const int IMAGE_HEIGHT_DEFAULT = GameFilter<T>.ROW_HEIGHT_DEFAULT - 2;
        private const int CLM_IMAGE_WIDTH = 60;
        private const int CLM_NAME_WIDTH = 90;

        private ResourceCacheSystem<T, Image> imageCache;
        public int ImageColumnnWidth
        {
            get
            {
                return this.imageColumnWidth;
            }
            set
            {
                this.imageColumnWidth = value;

                this.imageCache.ClearCache();

                this.UpdateColumnWidths();
            }
        }
        public int PrimaryNameColumnWidth
        {
            get
            {
                return this.primaryNameColumnWidth;
            }
            set
            {
                this.primaryNameColumnWidth = value;

                this.UpdateColumnWidths();
            }
        }
        public int SecondaryNameColumnWidth
        {
            get
            {
                return this.secondaryNameColumWidth;
            }
            set
            {
                this.secondaryNameColumWidth = value;

                this.UpdateColumnWidths();
            }
        }
        private int secondaryNameColumWidth = GameFilter<T>.CLM_NAME_WIDTH;
        private int imageColumnWidth = GameFilter<T>.CLM_IMAGE_WIDTH;
        private int primaryNameColumnWidth = GameFilter<T>.CLM_NAME_WIDTH;
        private OLVColumn olvClmPrimaryName;
        private OLVColumn olvClmSecondaryName;
        private OLVColumn olvClmImage;
        private NameGetter primaryNameGetter;
        private NameGetter secondaryNameGetter;
        private ImageGetter imageGetter;
        private List<T> selectedContentApplied;
        private ObjectListView contentOLV;
        private string columnHeader;
        public NameGetter ItemSecondaryNameGetter
        {
            get
            {
                return this.secondaryNameGetter;
            }
            set
            {
                this.secondaryNameGetter = value;

                this.UpdateColumnWidths();
            }
        }
        public NameGetter ItemPrimaryNameGetter
        {
            get
            {
                return this.primaryNameGetter;
            }
            set
            {
                this.primaryNameGetter = value;

                this.UpdateColumnWidths();
            }
        }
        public ImageGetter ItemImageGetter
        {
            get
            {
                return this.imageGetter;
            }
            set
            {
                this.imageGetter = value;

                this.UpdateColumnWidths();
            }
        }
        public AspectGetter FilterAspectGetter { get; set; }
        public int RowHeight
        {
            get
            {
                return this.contentOLV.RowHeight;
            }
            set
            {
                this.contentOLV.RowHeight = value;
            }
        }
        public string Header
        {
            get
            {
                return this.lbHeader.Text;
            }
            set
            {
                this.lbHeader.Text = value;
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
                this.contentOLV.AllColumns[2].Text = value;

                this.columnHeader = value;
            }
        }
        public event EventHandler FilterChanged = delegate { };
        public View View
        {
            get
            {
                return this.contentOLV.View;
            }
            set
            {
                this.contentOLV.View = value;
            }
        }

        public GameFilter()
        {
            InitializeComponent();

            this.selectedContentApplied = new List<T>();

            this.contentOLV = this.CreateContentFilterListView();
            this.contentOLV.ItemChecked += this.ContentOLV_ItemChecked;

            this.RowHeight = GameFilter<T>.ROW_HEIGHT_DEFAULT;

            this.tblLoPnlMain.Controls.Add(this.contentOLV, 0, 1);
            this.tblLoPnlMain.SetColumnSpan(this.contentOLV, 2);

            this.SetBtnEnabledStatus();
        }

        private static void ContentFilterOLV_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Item != null) { e.Item.Checked = !e.Item.Checked; }
        }

        private static void ContentFilterOLV_SelectionChanged(object sender, EventArgs e)
        {
            var objList = sender as ObjectListView;

            if (objList == null) { return; }

            foreach (ListViewItem item in objList.Items) { item.Checked = item.Selected; }

        }

        private void UpdateColumnWidths()
        {
            if (this.primaryNameGetter == null)
            {
                this.olvClmPrimaryName.MinimumWidth = 0;
                this.olvClmPrimaryName.Width = 0;
                this.olvClmPrimaryName.MaximumWidth = 0;
            }
            else
            {
                this.olvClmPrimaryName.MaximumWidth = this.PrimaryNameColumnWidth;
                this.olvClmPrimaryName.Width = this.PrimaryNameColumnWidth;
                this.olvClmPrimaryName.MinimumWidth = this.PrimaryNameColumnWidth;
            }

            if (this.secondaryNameGetter == null)
            {
                this.olvClmPrimaryName.MinimumWidth = 0;
                this.olvClmPrimaryName.Width = 0;
                this.olvClmPrimaryName.MaximumWidth = 0;
            }
            else
            {
                this.olvClmPrimaryName.MaximumWidth = this.PrimaryNameColumnWidth;
                this.olvClmPrimaryName.Width = this.PrimaryNameColumnWidth;
                this.olvClmPrimaryName.MinimumWidth = this.PrimaryNameColumnWidth;
            }

            if (this.imageGetter == null)
            {
                this.olvClmImage.MinimumWidth = 0;
                this.olvClmImage.Width = 0;
                this.olvClmImage.MaximumWidth = 0;
            }
            else
            {
                this.olvClmImage.MaximumWidth = this.ImageColumnnWidth;
                this.olvClmImage.Width = this.ImageColumnnWidth;
                this.olvClmImage.MinimumWidth = this.ImageColumnnWidth;
            }

        }

        private void ContentOLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, new EventArgs());
        }

        public bool FilterGame(Game game)
        {
            if (this.FilterAspectGetter == null) { return true; }
            else
            {
                T aspectOfGame = this.FilterAspectGetter(game);

                return this.selectedContentApplied.Contains(aspectOfGame);
            }

        }

        private ObjectListView CreateContentFilterListView()
        {
            var contentFilterOLV = new ObjectListView()
            {
                AllowColumnReorder = false,
                AlternateRowBackColor = EloSystemGUIStaticMembers.OlvRowAlternativeBackColor,
                BackColor = EloSystemGUIStaticMembers.OlvRowBackColor,
                CheckBoxes = true,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 10.5f, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = true,
                RowHeight = GameFilter<T>.ROW_HEIGHT_DEFAULT,
                Scrollable = true,
                ShowFilterMenuOnRightClick = false,
                ShowGroups = false,
                Size = new Size(190, 300),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = false,
            };

            contentFilterOLV.CellClick += GameFilter<T>.ContentFilterOLV_CellClick;
            contentFilterOLV.ItemSelectionChanged += GameFilter<T>.ContentFilterOLV_SelectionChanged;

            const int CLM_CHECKBOX_WIDTH = 21;

            var olvClmCheckBox = new OLVColumn() { MaximumWidth = CLM_CHECKBOX_WIDTH, Width = CLM_CHECKBOX_WIDTH, MinimumWidth = CLM_CHECKBOX_WIDTH, CellPadding = null, Text = "" };
            this.olvClmImage = new OLVColumn() { Width = 0, MinimumWidth = 0, MaximumWidth = 0, Text = "", IsTileViewColumn = true };
            this.olvClmPrimaryName = new OLVColumn() { Width = 0, MinimumWidth = 0, MaximumWidth = 0, Text = "" };
            this.olvClmSecondaryName = new OLVColumn() { Width = 0, MinimumWidth = 0, MaximumWidth = 0, Text = "" };

            contentFilterOLV.AllColumns.AddRange(new OLVColumn[] { olvClmCheckBox, this.olvClmImage, this.olvClmPrimaryName, this.olvClmSecondaryName });

            contentFilterOLV.Columns.AddRange(new ColumnHeader[] { olvClmCheckBox, this.olvClmImage, this.olvClmPrimaryName, this.olvClmSecondaryName });

            var imageRenderer = new ImageRenderer()
            {
                Bounds = new Rectangle(3, 2, 3, 3),
                CellPadding = new Rectangle(3, 1, 3, 1)
            };

            this.imageCache = new ResourceCacheSystem<T, Image>()
            {
                ResourceGetter = (key) => this.ItemImageGetter(key).ResizeSARWithinBounds(this.ImageColumnnWidth - imageRenderer.CellPadding.Value.Width, this.RowHeight - imageRenderer.CellPadding.Value.Height)
            };

            this.olvClmImage.AspectGetter = obj =>
            {
                var item = (T)obj;

                if (this.ItemImageGetter != null) { return new Image[] { this.imageCache.GetResource(item) }; }
                else { return null; }

            };

            this.olvClmImage.Renderer = imageRenderer;

            this.olvClmPrimaryName.AspectGetter = obj =>
            {
                var item = (T)obj;

                if (this.ItemPrimaryNameGetter != null) { return this.ItemPrimaryNameGetter(item); }
                else { return string.Empty; }
            };

            this.olvClmSecondaryName.AspectGetter = obj =>
            {
                var item = (T)obj;

                if (this.ItemSecondaryNameGetter != null) { return this.ItemSecondaryNameGetter(item); }
                else { return string.Empty; }
            };

            return contentFilterOLV;
        }

        private IEnumerable<T> GetAllowedContent()
        {
            return this.contentOLV.CheckedItems.Cast<OLVListItem>().Select(lvItem => (T)lvItem.RowObject);
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
            else { foreach (OLVListItem item in this.contentOLV.Items) { if (this.selectedContentApplied.Contains((T)item.RowObject)) { item.Checked = true; } } }
        }

        public bool HasChangesNotApplied()
        {
            return this.GetAllowedContent().Count() != this.selectedContentApplied.Count || this.GetAllowedContent().Any(item => !this.selectedContentApplied.Contains(item));
        }

        public void ApplyChanges()
        {
            this.selectedContentApplied = this.GetAllowedContent().ToList();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(true);
        }

        private void SetAllSelectionsTo(bool selectionState)
        {
            foreach (OLVListItem item in this.contentOLV.Items) { item.Checked = selectionState; }

            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, new EventArgs());
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(false);
        }

        private void SetBtnEnabledStatus()
        {
            this.btnSelectAll.Enabled = this.contentOLV.Items.Cast<OLVListItem>().Any(item => !item.Checked);

            this.btnDeselectAll.Enabled = this.contentOLV.Items.Cast<OLVListItem>().Any(item => item.Checked);
        }
    }
}
