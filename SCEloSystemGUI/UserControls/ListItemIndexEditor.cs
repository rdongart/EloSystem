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
        private static Color selectedItemColor = Color.LightBlue;

        private bool isClearingSelections = false;
        private int startIndex = -1;
        private int acceptedIndexChange = 0;
        private ListView lstView;
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

        protected void SetListView(ListView lstV, int activeItemIndex)
        {
            this.lstView = lstV;
            this.lstView.Dock = DockStyle.Fill;
            this.lstView.FullRowSelect = true;
            this.lstView.MouseLeave += LstView_MouseLeave;
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
            }

            this.activeItem.BackColor = ListItemIndexEditor.selectedItemColor;

            this.UpdateControlsEnabledStatus();
        }

        private void LstView_MouseLeave(object sender, EventArgs e)
        {
            this.AddListViewItems();
        }

        private void LstView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!this.isClearingSelections && (this.lstView.SelectedItems.Count > 1 || (this.lstView.SelectedItems.Count == 1 && this.lstView.SelectedItems[0] != this.activeItem)))
            {
                this.isClearingSelections = true;
                this.lstView.SelectedItems.Clear();
                this.activeItem.Selected = true;
                this.activeItem.BackColor = ListItemIndexEditor.selectedItemColor;
                this.lstView.Refresh();
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
            int currentActiveIndex = this.ItemsWithIndexValues[this.activeItem];

            var itemExchangedWithActiveItem = this.ItemsWithIndexValues.First(lstViewItem => lstViewItem.Value == currentActiveIndex - 1).Key;

            this.ItemsWithIndexValues.Remove(itemExchangedWithActiveItem);
            this.ItemsWithIndexValues.Remove(this.activeItem);

            this.ItemsWithIndexValues.Add(this.activeItem, currentActiveIndex - 1);
            this.ItemsWithIndexValues.Add(itemExchangedWithActiveItem, currentActiveIndex);

            this.AddListViewItems();

            this.UpdateControlsEnabledStatus();
        }

        private void btnIncreaseIndex_Click(object sender, EventArgs e)
        {
            int currentActiveIndex = this.ItemsWithIndexValues[this.activeItem];

            var itemExchangedWithActiveItem = this.ItemsWithIndexValues.First(lstViewItem => lstViewItem.Value == currentActiveIndex + 1).Key;

            this.ItemsWithIndexValues.Remove(itemExchangedWithActiveItem);
            this.ItemsWithIndexValues.Remove(this.activeItem);

            this.ItemsWithIndexValues.Add(this.activeItem, currentActiveIndex + 1);
            this.ItemsWithIndexValues.Add(itemExchangedWithActiveItem, currentActiveIndex);

            this.AddListViewItems();

            this.UpdateControlsEnabledStatus();
        }

        private void AddListViewItems()
        {
            this.lstView.Items.Clear();

            this.lstView.Items.AddRange(this.ItemsWithIndexValues.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key).ToArray());

            this.activeItem.Selected = true;

            this.activeItem.BackColor = ListItemIndexEditor.selectedItemColor;
        }
    }
}
