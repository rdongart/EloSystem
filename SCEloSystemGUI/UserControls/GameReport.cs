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
    public partial class GameReport : UserControl
    {
        internal string Title
        {
            get
            {
                return this.lbGameHeader.Text;
            }
            set
            {
                this.lbGameHeader.Text = value;
            }
        }
        public event EventHandler<EventArgs> OnRemoveButtonClick = delegate { };

        public GameReport()
        {
            InitializeComponent();
        }

        private void btnRemoveGame_Click(object sender, EventArgs e)
        {
            this.OnRemoveButtonClick.Invoke(sender, e);
        }
    }
}
