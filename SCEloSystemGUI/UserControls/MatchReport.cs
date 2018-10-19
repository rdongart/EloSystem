using CustomControls;
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
    public partial class MatchReport : UserControl
    {
        internal ImageComboBox ImgCmbBxPlayer1 { get; private set; }
        internal ImageComboBox ImgCmbBxPlayer2 { get; private set; }

        public MatchReport()
        {
            InitializeComponent();

            this.ImgCmbBxPlayer1 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlGameReport.Controls.Add(this.ImgCmbBxPlayer1, 1, 2);
            this.tblLOPnlGameReport.SetColumnSpan(this.ImgCmbBxPlayer1, 4);

            this.ImgCmbBxPlayer2 = MatchReport.GetPlayerSelectionComboBox();
            this.tblLOPnlGameReport.Controls.Add(this.ImgCmbBxPlayer2, 6, 2);
            this.tblLOPnlGameReport.SetColumnSpan(this.ImgCmbBxPlayer2, 4);
        }

        private static ImageComboBox GetPlayerSelectionComboBox()
        {
            return new ImageComboBox()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 160,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(3, 1, 3, 1),
                ItemHeight = 20,
                Margin = new Padding(4, 4, 4, 4),
                Size = new Size(160, 28),
            };
        }
    }
}
