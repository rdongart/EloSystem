using EloSystem;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class HasNameContentAdder<T> : UserControl where T : IHasName 
    {

        public Type ContentType
        {
            get
            {
                return typeof(T);
            }
        }
        public event EventHandler<HasNameAddingEventArgs> OnAddButtonClick = delegate { };
        public string ContentName
        {
            get
            {
                return this.txtBxName.Text;
            }
        }
        private IHasName contentType;

        public HasNameContentAdder()
        {
            InitializeComponent();

            this.lbHeading.Text = String.Format("Create new {0}", this.ContentName.ToLower());

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
            this.OnAddButtonClick.Invoke(sender, new HasNameAddingEventArgs(this.contentType));

            this.txtBxName.Text = EloGUIControlsStaticMembers.DEFAULT_TXTBX_TEXT;
            this.btnAdd.Enabled = false;
        }
    }
}
