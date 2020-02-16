﻿using BrightIdeasSoftware;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal partial class PlayerSearch : UserControl
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

        internal PlayerSearch(ObjectListView lstV)
        {
            InitializeComponent();

            lstV.Margin = new Padding(6);
            lstV.Dock = DockStyle.Fill;
            this.tblLOPnlPlayerSearch.Controls.Add(lstV, 0, 2);
            this.tblLOPnlPlayerSearch.SetColumnSpan(lstV, 2);
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.UpdateSearch();
        }

        private void txtBxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && this.txtBxFilter.Text != "") { this.UpdateSearch(); }
        }

        public void UpdateSearch()
        {
            this.PlayerSearchInitiated.Invoke(this, new PlayerSearchEventArgs(this.txtBxFilter.Text));
        }
    }
}
