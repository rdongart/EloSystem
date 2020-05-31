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
    public partial class GameFilter<T> : UserControl, IGameFilter
    {
        public delegate string NameGetter<T>(T item);
        public delegate Image ImageGetter<T>(T item);
        public delegate T AspectGetter<T>(Game game);

        private const int ROW_HEIGHT_DEFAULT = 22;
        private const int IMAGE_HEIGHT_DEFAULT = GameFilter<T>.ROW_HEIGHT_DEFAULT - 2;
        private const int CLM_IMAGE_WIDTH = 60;
        private const int CLM_NAME_WIDTH = 90;

        public int ImageColumnnWidth
        {
            get
            {
                return this.imageColumnWidth;
            }
            set
            {
                this.imageColumnWidth = value;

                this.UpdateColumnWidths();
            }
        }
        public int NameColumnWidth
        {
            get
            {
                return this.nameColumnWidth;
            }
            set
            {
                this.nameColumnWidth = value;

                this.UpdateColumnWidths();
            }
        }
        private int imageColumnWidth = GameFilter<T>.CLM_IMAGE_WIDTH;
        private int nameColumnWidth = GameFilter<T>.CLM_NAME_WIDTH;
        private OLVColumn olvClmName;
        private OLVColumn olvClmImage;
        private NameGetter<T> nameGetter;
        private ImageGetter<T> imageGetter;
        private List<T> selectedContentApplied;
        private ObjectListView contentOLV;
        private string columnHeader;
        public NameGetter<T> ItemNameGetter
        {
            get
            {
                return this.nameGetter;
            }
            set
            {
                this.nameGetter = value;

                this.UpdateColumnWidths();
            }
        }
        public ImageGetter<T> ItemImageGetter
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
        public AspectGetter<T> FilterAspectGetter { get; set; }
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
            this.tblLoPnlMain.SetRowSpan(this.contentOLV, 2);

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
            if (this.nameGetter == null)
            {
                this.olvClmName.MinimumWidth = 0;
                this.olvClmName.Width = 0;
                this.olvClmName.MaximumWidth = 0;
            }
            else
            {
                this.olvClmName.MaximumWidth = this.NameColumnWidth;
                this.olvClmName.Width = this.NameColumnWidth;
                this.olvClmName.MinimumWidth = this.NameColumnWidth;
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

            this.FilterChanged.Invoke(this, e);
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
                AlternateRowBackColor = Color.FromArgb(210, 210, 210),
                BackColor = Color.FromArgb(175, 175, 235),
                CheckBoxes = true,
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 10.5f, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = true,
                RowHeight = GameFilter<T>.ROW_HEIGHT_DEFAULT,
                Scrollable = true,
                ShowGroups = false,
                Size = new Size(190, 300),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = false
            };

            contentFilterOLV.CellClick += GameFilter<T>.ContentFilterOLV_CellClick;

            const int CLM_CHECKBOX_WIDTH = 21;

            var olvClmCheckBox = new OLVColumn() { MaximumWidth = CLM_CHECKBOX_WIDTH, Width = CLM_CHECKBOX_WIDTH, MinimumWidth = CLM_CHECKBOX_WIDTH, CellPadding = null, Text = "" };
            this.olvClmImage = new OLVColumn() { Width = 0, MinimumWidth = 0, MaximumWidth = 0, Text = "", IsTileViewColumn = true };
            this.olvClmName = new OLVColumn() { Width = 0, MinimumWidth = 0, MaximumWidth = 0, Text = "" };

            contentFilterOLV.AllColumns.AddRange(new OLVColumn[] { olvClmCheckBox, this.olvClmImage, this.olvClmName });

            contentFilterOLV.Columns.AddRange(new ColumnHeader[] { olvClmCheckBox, this.olvClmImage, this.olvClmName });

            var imageRenderer = new ImageRenderer()
            {
                Bounds = new Rectangle(3, 2, 3, 3),
                CellPadding = new Rectangle(3, 1, 3, 1)
            };


            this.olvClmImage.AspectGetter = obj =>
            {
                var item = (T)obj;

                if (this.ItemImageGetter != null)
                {
                    Image image = this.ItemImageGetter(item);

                    return new Image[] { image.ResizeSARWithinBounds(this.ImageColumnnWidth - imageRenderer.CellPadding.Value.Width, this.RowHeight - imageRenderer.CellPadding.Value.Height) };
                }
                else { return null; }

            };

            this.olvClmImage.Renderer = imageRenderer;

            this.olvClmName.AspectGetter = obj =>
            {
                var item = (T)obj;

                if (this.ItemNameGetter != null) { return this.ItemNameGetter(item); }
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
