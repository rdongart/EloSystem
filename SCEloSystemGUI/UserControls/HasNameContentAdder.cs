using EloSystem;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class HasNameContentAdder<T> : UserControl where T : IHasName 
    {
        public event EventHandler OnAddButtonClick = delegate { };
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }

        public HasNameContentAdder()
        {
            InitializeComponent();

            this.lbHeading.Text = String.Format("Create new {0}", typeof(T).Name.ToLower());

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBxName.Text != string.Empty) { this.btnAdd.Enabled = true; }
            else { this.btnAdd.Enabled = false; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAddButtonClick.Invoke(this, new EventArgs());

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }
    }
}
