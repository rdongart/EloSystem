using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class ListItemIndexEditor : UserControl
    {
        private bool isClearingSelections = false;
        private int startIndex = -1;
        private int acceptedIndexChange = 0;
        private ObjectListView lstView;
        private ListViewItem activeItem;
        private Dictionary<ListViewItem, int> ItemsWithIndexValues;
        public event EventHandler IndexChangesAccepted = delegate { };
        public int AcceptedIndexChange
        {
            get
            {
                return this.acceptedIndexChange;
            }
            private set
            {
                this.acceptedIndexChange = value;
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

        protected ListItemIndexEditor()
        {
            InitializeComponent();
        }

        protected void SetListView(ObjectListView lstV, int activeItemIndex)
        {
            this.lstView = lstV;
            this.lstView.Dock = DockStyle.Fill;
            this.lstView.FullRowSelect = true;
            this.lstView.ItemSelectionChanged += LstView_ItemSelectionChanged;
            this.lstView.Margin = new Padding(0, 6, 6, 10);
            this.tLPMain.SetRowSpan(this.lstView, 2);
            this.tLPMain.Controls.Add(this.lstView, 0, 1);

            this.lstView.SelectedItems.Clear();

            this.ItemsWithIndexValues = this.lstView.Items.Cast<ListViewItem>().Select((item, index) => new KeyValuePair<ListViewItem, int>(item, index)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (activeItemIndex > -1)
            {
                this.activeItem = this.lstView.Items[activeItemIndex];
                this.activeItem.Selected = true;
                this.startIndex = activeItemIndex;

                if (this.lstView.Scrollable)
                {
                    int rowHeightCorrected = this.lstView.RowHeight + 1;
                    this.lstView.LowLevelScroll(0, activeItemIndex * rowHeightCorrected);
                }
            }

            this.UpdateControlsEnabledStatus();
        }

        private void LstView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!this.isClearingSelections && (this.lstView.SelectedItems.Count > 1 || (this.lstView.SelectedItems.Count == 1 && this.lstView.SelectedItems[0] != this.activeItem)))
            {
                this.isClearingSelections = true;
                this.lstView.SelectedItems.Clear();
                this.activeItem.Selected = true;
                this.isClearingSelections = false;
            }
        }

        private void UpdateControlsEnabledStatus()
        {
            int currentActiveIndex = this.ItemsWithIndexValues[this.activeItem];

            this.btnAcceptChanges.Enabled = this.startIndex != currentActiveIndex && this.AcceptedIndexChange == 0;
            this.btnDecreaseIndex.Enabled = this.lstView != null && currentActiveIndex > 0;
            this.btnIncreaseIndex.Enabled = this.lstView != null && currentActiveIndex < this.lstView.Items.Count - 1;
        }

        private void btnAcceptChanges_Click(object sender, EventArgs e)
        {
            this.AcceptedIndexChange = this.startIndex - this.ItemsWithIndexValues[this.activeItem];
            this.IndexChangesAccepted.Invoke(this, new EventArgs());
        }

        private void btnDecreaseIndex_Click(object sender, EventArgs e)
        {
            const int INDEX_DECREMENT = -1;

            this.indexChanger(INDEX_DECREMENT);
        }

        private void indexChanger(int change)
        {
            int currentActiveIndex = this.ItemsWithIndexValues[this.activeItem];

            var itemExchangedWithActiveItem = this.ItemsWithIndexValues.First(lstViewItem => lstViewItem.Value == currentActiveIndex + change).Key;

            this.ItemsWithIndexValues.Remove(itemExchangedWithActiveItem);
            this.ItemsWithIndexValues.Remove(this.activeItem);

            this.ItemsWithIndexValues.Add(this.activeItem, currentActiveIndex + change);
            this.ItemsWithIndexValues.Add(itemExchangedWithActiveItem, currentActiveIndex);

            Point currentScroll = this.lstView.LowLevelScrollPosition;

            this.AddListViewItems();

            this.UpdateControlsEnabledStatus();

            if (this.lstView.Scrollable)
            {
                int rowHeightCorrected = this.lstView.RowHeight + 1;
                this.lstView.LowLevelScroll(currentScroll.X, currentScroll.Y * rowHeightCorrected);
            }
        }

        private void btnIncreaseIndex_Click(object sender, EventArgs e)
        {
            const int INDEX_INCREMENT = 1;

            this.indexChanger(INDEX_INCREMENT);
        }

        private void AddListViewItems()
        {
            this.lstView.Items.Clear();

            this.lstView.Items.AddRange(this.ItemsWithIndexValues.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key).ToArray());

            this.activeItem.Selected = true;
        }
    }
}
