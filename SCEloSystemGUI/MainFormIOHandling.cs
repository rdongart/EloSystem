using SCEloSystemGUI.UserControls;
using System.IO;
using EloSystem.IO;
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
        private static bool ShouldExistingFileBeReplaced(object sender, FileOverwriteEventArgs e)
        {
            if (MessageBox.Show(String.Format("An Elo System named {0} already exists. Are you sure you would like to overwrite that file?", e.FileName), "Name is already in use"
                , MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                return true;
            }
            else { return false; }
        }

        private static string GetNewDefaultSaveName(string currentName)
        {
            const string VERSION_DELIMITER_SYMBOL = "_";

            int version;

            int indexOfCopyVersionDelimiter = currentName.LastIndexOf(VERSION_DELIMITER_SYMBOL) + 1;

            if (currentName.Length <= indexOfCopyVersionDelimiter && int.TryParse(currentName.Substring(indexOfCopyVersionDelimiter), out version))
            {
                return String.Format("{0}{1}", currentName.Substring(0, indexOfCopyVersionDelimiter), (version + 1).ToString());
            }
            else { return String.Format("{0}{1}{2}", currentName, VERSION_DELIMITER_SYMBOL, 1.ToString()); }
        }

        private void Save()
        {
            this.Cursor = Cursors.WaitCursor;

            this.eloSystem.SaveData(this.eloSystem.Name);

            MessageBox.Show("Elo System saved!", "", MessageBoxButtons.OK);

            this.Cursor = Cursors.Default;
        }

        private void SaveAs()
        {
            string newFileName = MainForm.GetNewDefaultSaveName(this.eloSystem.Name);

            if (EloSystemGUIStaticMethods.GetEloSystemName(ref newFileName) != DialogResult.OK) { return; }

            this.Cursor = Cursors.WaitCursor;

            this.eloSystem.SaveData(this.eloSystem.Name, MainForm.ShouldExistingFileBeReplaced);

            this.Cursor = Cursors.Default;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }



    }

}