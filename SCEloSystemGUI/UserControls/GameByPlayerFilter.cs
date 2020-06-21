using BrightIdeasSoftware;
using CustomControls.Utilities;
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

    public partial class GameByPlayerFilter : UserControl, IGameFilter
    {
        private const int ROW_HEIGHT_DEFAULT = 22;
        private const int IMAGE_HEIGHT_DEFAULT = GameByPlayerFilter.ROW_HEIGHT_DEFAULT - 2;
        private const int CLM_IMAGE_WIDTH = 60;
        private const int CLM_NAME_WIDTH = 90;

        private ResourceCacheSystem<Country, Image> flagsCache;
        private HashSet<SCPlayer> selectedContentApplied = new HashSet<SCPlayer>();
        private ObjectListView playersOLV;
        private OLVColumn lastPrimarySortColumn; // because the PrimarySortColumn for ObjectListView changes, we store it here

        public GameByPlayerFilter()
        {
            InitializeComponent();

            this.playersOLV = this.CreateContentFilterListView();
            this.playersOLV.ItemChecked += this.ContentOLV_ItemChecked;

            this.tblLoPnlMain.Controls.Add(this.playersOLV, 0, 1);
            this.tblLoPnlMain.SetColumnSpan(this.playersOLV, 2);

            this.SetBtnEnabledStatus();
        }

        #region IGameFilter implemention
        public event EventHandler FilterChanged = delegate { };

        public void ApplyChanges()
        {
            this.selectedContentApplied = new HashSet<SCPlayer>(this.GetAllowedContent());
        }

        public bool FilterGame(Game game)
        {
            return this.selectedContentApplied.Contains(game.Player1) || this.selectedContentApplied.Contains(game.Player2);
        }

        public bool HasChangesNotApplied()
        {
            return !this.selectedContentApplied.SequenceEqual(new HashSet<SCPlayer>(this.GetAllowedContent()));
        }
        #endregion

        private IEnumerable<SCPlayer> GetAllowedContent()
        {
            return this.playersOLV.CheckedItems.Cast<OLVListItem>().Select(lvItem => lvItem.RowObject as SCPlayer);
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

        private void ContentOLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, new EventArgs());
        }

        private void PlayersOLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            const int ALTERNATIVE_SORT_COLUMN = 1;
            const int GAMER_HANDLE_COLUMN = 3;
            const int IRL_NAME_COLUMN = 4;

            OLVColumn oldSortColumn = this.lastPrimarySortColumn;
            OLVColumn newSortColumn = e.Column == GAMER_HANDLE_COLUMN || e.Column == IRL_NAME_COLUMN ? this.playersOLV.AllColumns[e.Column] : this.playersOLV.AllColumns[ALTERNATIVE_SORT_COLUMN];

            if (newSortColumn != oldSortColumn)
            {
                this.playersOLV.SecondarySortColumn = oldSortColumn;
                this.playersOLV.Sorting = SortOrder.Ascending;
            }
            else { this.playersOLV.Sorting = this.playersOLV.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending; }

            this.lastPrimarySortColumn = newSortColumn;

            this.playersOLV.Sort(newSortColumn, this.playersOLV.Sorting);
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
                HeaderStyle = ColumnHeaderStyle.Clickable,
                HasCollapsibleGroups = false,
                Margin = new Padding(3),
                MultiSelect = true,
                PrimarySortOrder = SortOrder.Ascending,
                RowHeight = GameByPlayerFilter.ROW_HEIGHT_DEFAULT,
                Scrollable = true,
                ShowFilterMenuOnRightClick = false,
                ShowGroups = false,
                Size = new Size(190, 300),
                UseAlternatingBackColors = true,
                UseCellFormatEvents = false
            };

            contentFilterOLV.ColumnClick += this.PlayersOLV_ColumnClick;
            contentFilterOLV.CellClick += GameByPlayerFilter.ContentFilterOLV_CellClick;
            contentFilterOLV.ItemSelectionChanged += GameByPlayerFilter.ContentFilterOLV_SelectionChanged;

            const int CLM_CHECKBOX_WIDTH = 21;
            const int FLAG_CLM_WIDTH = 35;

            var olvClmCheckBox = new OLVColumn() { MaximumWidth = CLM_CHECKBOX_WIDTH, Width = CLM_CHECKBOX_WIDTH, MinimumWidth = CLM_CHECKBOX_WIDTH, CellPadding = null, Text = "" };
            var olvClmAlternativeSorting = new OLVColumn() { MaximumWidth = 0, Width = 0, MinimumWidth = 0, CellPadding = null, Text = "", Sortable = true };
            var olvClmFlag = new OLVColumn()
            {
                MaximumWidth = FLAG_CLM_WIDTH,
                Width = FLAG_CLM_WIDTH,
                MinimumWidth = FLAG_CLM_WIDTH,
                Sortable = true,
                Text = "Nationality",
                IsTileViewColumn = true,
                TextAlign = HorizontalAlignment.Center
            };
            var olvClmGamerHandle = new OLVColumn() { Sortable = true, Width = 100, Text = "Name" };
            var olvClmIRLName = new OLVColumn() { Sortable = true, Width = 100, Text = "IRL Name" };

            contentFilterOLV.AllColumns.AddRange(new OLVColumn[] { olvClmCheckBox, olvClmAlternativeSorting, olvClmFlag, olvClmGamerHandle, olvClmIRLName });

            contentFilterOLV.Columns.AddRange(new ColumnHeader[] { olvClmCheckBox, olvClmAlternativeSorting, olvClmFlag, olvClmGamerHandle, olvClmIRLName });

            contentFilterOLV.PrimarySortColumn = olvClmGamerHandle;

            var imageRenderer = new ImageRenderer()
            {
                Bounds = new Rectangle(3, 2, 3, 3),
                CellPadding = new Rectangle(3, 1, 3, 1)
            };

            this.flagsCache = new ResourceCacheSystem<Country, Image>()
            {
                ResourceGetter = (key) => EloGUIControlsStaticMembers.ImageGetterMethod(key).ResizeSameAspectRatio(contentFilterOLV.RowHeight - 4)
            };

            olvClmAlternativeSorting.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null && player.Country != null) { return player.Country.Name; }
                else { return string.Empty; }
            };

            olvClmFlag.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null && player.Country != null) { return new Image[] { this.flagsCache.GetResource(player.Country) }; }
                else { return null; }

            };

            olvClmFlag.Renderer = imageRenderer;

            olvClmGamerHandle.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null) { return player.Name; }
                else { return string.Empty; }
            };

            olvClmIRLName.AspectGetter = obj =>
            {
                var player = obj as SCPlayer;

                if (player != null) { return player.IRLName; }
                else { return string.Empty; }
            };

            return contentFilterOLV;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(true);
        }

        private void SetAllSelectionsTo(bool selectionState)
        {
            foreach (OLVListItem item in this.playersOLV.Items) { item.Checked = selectionState; }

            this.SetBtnEnabledStatus();

            this.FilterChanged.Invoke(this, new EventArgs());
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            this.SetAllSelectionsTo(false);
        }

        private void SetBtnEnabledStatus()
        {
            this.btnSelectAll.Enabled = this.playersOLV.Items.Cast<OLVListItem>().Any(item => !item.Checked);

            this.btnDeselectAll.Enabled = this.playersOLV.Items.Cast<OLVListItem>().Any(item => item.Checked);
        }

        public void SetItems(IEnumerable<SCPlayer> items)
        {
            bool listWasEmpty = this.playersOLV.Items.Count == 0;

            this.playersOLV.SetObjects(items.ToArray());

            if (listWasEmpty || !items.Any(item => this.selectedContentApplied.Contains(item))) { this.SetAllSelectionsTo(true); }
            else { foreach (OLVListItem item in this.playersOLV.Items) { if (this.selectedContentApplied.Contains(item.RowObject as SCPlayer)) { item.Checked = true; } } }

            this.ApplyChanges();
            bool working = this.HasChangesNotApplied();
        }
    }
}
