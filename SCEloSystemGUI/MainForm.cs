using SCEloSystemGUI.UserControls;
using System.IO;
using EloSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm : Form
    {
        private EloData eloSystem;
        private ContentAdder mapAdder;

        internal MainForm(EloData eloSystem)
        {
            InitializeComponent();

            this.eloSystem = eloSystem;

            this.Text = this.eloSystem.Name;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.mapAdder = new ContentAdder() { ContentType = ContentTypes.Map };
            this.mapAdder.OnAddButtonClick += this.AddContent;

            this.tblLOPnlMaps.Controls.Add(this.mapAdder, 0, 0);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) { return; }

            if (this.eloSystem.DataWasChanged)
            {
                switch (MessageBox.Show("If you close this Elo System, all changes not saved will be lost. Are you sure you would like to close?", "Close?", MessageBoxButtons.OKCancel))
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    default: break;
                }
            }

        }


    }
}
