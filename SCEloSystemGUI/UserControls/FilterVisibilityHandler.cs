using System;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class PlayerStats : Form
    {
        private const int FILTERROW_INDEX = 0;
        private const float HEIGHT_CHANGE_SPEED = 55;
        private const float FILTERROW_HEIGHT = 384;

        private void ShowFilters()
        {
            RowStyle filterRow = this.tblLoPnlPlayerStats.RowStyles[PlayerStats.FILTERROW_INDEX];

            float heightDifference = PlayerStats.FILTERROW_HEIGHT - filterRow.Height;

            if (heightDifference > 0)
            {
                if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate () { filterRow.Height += Math.Min(heightDifference, PlayerStats.HEIGHT_CHANGE_SPEED); }); }
                else { filterRow.Height += Math.Min(heightDifference, PlayerStats.HEIGHT_CHANGE_SPEED); }
            }

            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate () { this.tblLoPnlPlayerStats.Update(); }); }
            else { this.tblLoPnlPlayerStats.Update(); }

        }

        private void HideCustomizations()
        {
            RowStyle filterRow = this.tblLoPnlPlayerStats.RowStyles[PlayerStats.FILTERROW_INDEX];

            float heightDifference = filterRow.Height - 0;

            if (heightDifference > 0)
            {
                if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate () { filterRow.Height -= Math.Min(heightDifference, PlayerStats.HEIGHT_CHANGE_SPEED); }); }
                else { filterRow.Height -= Math.Min(heightDifference, PlayerStats.HEIGHT_CHANGE_SPEED); }
            }

            if (filterRow.Height == 0)
            {
                if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate () { this.tabCtrlCustomizations.Visible = false; }); }
                else { this.tabCtrlCustomizations.Visible = false; }
            }

            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate () { this.tblLoPnlPlayerStats.Update(); }); }
            else { this.tblLoPnlPlayerStats.Update(); }

        }

        private bool StopFilterShowProcces()
        {
            return this.tblLoPnlPlayerStats.RowStyles[PlayerStats.FILTERROW_INDEX].Height == PlayerStats.FILTERROW_HEIGHT;
        }

        private bool StopFilterHideProcces()
        {
            return this.tblLoPnlPlayerStats.RowStyles[PlayerStats.FILTERROW_INDEX].Height == 0;
        }
    }
}
