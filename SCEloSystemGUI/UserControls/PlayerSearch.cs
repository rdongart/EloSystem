using BrightIdeasSoftware;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class PlayerSearch : UserControl
    {
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
        public EventHandler<PlayerSearchEventArgs> PlayerSearchInitiated = delegate { };

        public PlayerSearch(ObjectListView lstV)
        {
            InitializeComponent();

            lstV.Margin = new Padding(6);
            lstV.Dock = DockStyle.Fill;
            this.tblLOPnlPlayerSearch.Controls.Add(lstV, 0, 2);
            this.tblLOPnlPlayerSearch.SetColumnSpan(lstV, 2);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.PlayerSearchInitiated(this, new PlayerSearchEventArgs(this.txtBxFilter.Text));
        }

        private void txtBxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && this.txtBxFilter.Text != "") { this.PlayerSearchInitiated.Invoke(this, new PlayerSearchEventArgs(this.txtBxFilter.Text)); }
        }

    }
}
