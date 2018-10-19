using CustomControls;
using EloSystem;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    internal static class EloSystemGUIStaticMembers
    {
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

        //internal static void SaveEloData(EloData dataFile, string fileName)
        //{
        //    //try
        //    //{
        //        dataFile.SaveData(fileName, EloSystemGUIStaticMethods.ShouldExistingFileBeReplaced);


        //        MessageBox.Show("Elo System saved!", "", MessageBoxButtons.OK);
        //    //}
        //    //catch (IOException ioExeption) { MessageBox.Show(ioExeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    //catch (Exception exeption) { MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //}

        private static bool IsInvalidFilename(string fileName)
        {
            Regex checkForInvalidChararacters = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");

            return checkForInvalidChararacters.IsMatch(fileName);
        }
        
    }
}
