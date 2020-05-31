using CustomExtensionMethods;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class HasNameContentEditor<T> : UserControl where T : HasNameContent
    {
        private HasNameContentGetterDelegate<T> contentGetter;
        public EventHandler EditButtonClicked = delegate { };
        public EventHandler RemoveButtonClicked = delegate { };
        public HasNameContentGetterDelegate<T> ContentGetter
        {
            private get
            {
                return this.contentGetter;
            }
            set
            {
                this.contentGetter = value;

                this.UpdateItems();
            }
        }
        public HasNameContenRemoveCondition<T> RemoveCondition { private get; set; }
        public string NewItemName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        public T SelectedItem
        {
            get
            {
                var selectedItem = this.cmbBxContent.SelectedItem as Tuple<string, T>;

                return selectedItem == null ? null : selectedItem.Item2;
            }
        }

        public HasNameContentEditor()
        {
            InitializeComponent();

            this.lbHeading.Text = String.Format("Edit {0}", typeof(T).Name.ToLower());
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            this.SetControlsEnabledStatus();
        }

        private void SetControlsEnabledStatus()
        {
            this.btnEdit.Enabled = this.SelectedItem != null && this.NewItemName != string.Empty && this.NewItemName != this.SelectedItem.Name;

            this.btnRemove.Enabled = this.SelectedItem != null && (this.RemoveCondition == null || this.RemoveCondition(this.SelectedItem));

            this.txtBxName.Enabled = this.SelectedItem != null;
        }

        private void ResetControls()
        {
            this.txtBxName.Text = string.Empty;
        }

        private void cmbBxContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedItem == null) { this.ResetControls(); }
            else
            {
                this.txtBxName.Text = this.SelectedItem.Name;

                this.SetControlsEnabledStatus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditButtonClicked.Invoke(this, new EventArgs());

            this.txtBxName.Text = string.Empty;

            this.UpdateItems();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.RemoveButtonClicked.Invoke(this, new EventArgs());

            this.txtBxName.Text = string.Empty;

            this.UpdateItems();
        }

        public void UpdateItems()
        {
            if (this.ContentGetter != null)
            {
                var selectedItem = this.SelectedItem;

                this.cmbBxContent.DisplayMember = "Item1";
                this.cmbBxContent.ValueMember = "Item2";

                this.cmbBxContent.Items.Clear();
                this.cmbBxContent.Items.AddRange(this.ContentGetter().OrderBy(item => item.Name).Select(item => Tuple.Create<string, T>(item.Name, item)).ToArray());

                IEnumerable<T> items = this.cmbBxContent.Items.Cast<Tuple<string, T>>().Select(tupl => tupl.Item2);

                if (selectedItem != null && items.Contains(selectedItem)) { this.cmbBxContent.SelectedIndex = items.IndexOf(selectedItem); }
                else { this.cmbBxContent.SelectedIndex = -1; }

                this.SetControlsEnabledStatus();
            }
        }
    }
}
