using System;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal static class StaticMembers
    {
        internal const string DEFAULT_TXTBX_TEXT = "Type the name here...";

        [STAThread]
        internal static bool TryGetFilePathFromUser(out string filePath)
        {
            filePath = "";

            while (true)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select image";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
                openFileDialog.Filter = "Image files|*.gif;*.bmp;*.jpg;*.jpeg;*.png*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    return true;
                }
                else { return false; }

            }
        }


    }
}
