using EloSystem.IO;
using System;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class MainForm 
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

            this.PostSaveProcedure();
        }

        private void SaveAs()
        {
            string newFileName = this.eloSystem.Name;

            if (EloSystemGUIStaticMembers.GetEloSystemName(ref newFileName) != DialogResult.OK) { return; }

            this.Cursor = Cursors.WaitCursor;

            this.eloSystem.SaveData(newFileName, MainForm.ShouldExistingFileBeReplaced);

            this.PostSaveProcedure();
        }

        private void PostSaveProcedure()
        {
            this.Text = this.eloSystem.Name;

            MessageBox.Show("Elo System saved!", "", MessageBoxButtons.OK);

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