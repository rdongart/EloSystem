using BrightIdeasSoftware;
using CustomControls;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    internal static class EloSystemGUIStaticMembers
    {
        internal const string NUMBER_FORMAT = "#,#";

        internal static DialogResult GetEloSystemName(ref string fileName)
        {
            while (true)
            {
                if (Interaction.InputBox("New Elo System", "Name your Elo System", ref fileName) == DialogResult.OK)
                {
                    if (fileName == string.Empty) { MessageBox.Show("Failed to create new Elo System because name can not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else if (EloSystemGUIStaticMembers.IsInvalidFilename(fileName))
                    {
                        MessageBox.Show("Failed to create new Elo System name contained some invalid characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else { return DialogResult.OK; }

                }
                else { return DialogResult.Cancel; }

            }
        }

        private static bool IsInvalidFilename(string fileName)
        {
            Regex checkForInvalidChararacters = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");

            return checkForInvalidChararacters.IsMatch(fileName);
        }

        internal static void FormatRatingChangeOLVCell(OLVListSubItem subItem)
        {
            int cellValue;

            if (int.TryParse(subItem.Text, out cellValue))
            {
                if (cellValue < 0) { subItem.ForeColor = Color.Red; }
                else if (cellValue > 0) { subItem.ForeColor = Color.ForestGreen; }
                else { subItem.ForeColor = SystemColors.ControlText; }
            }
        }
        
    }
}
