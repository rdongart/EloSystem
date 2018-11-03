using CustomControls;
using EloSystem;
using EloSystem.ResourceManagement;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal static class EloGUIControlsStaticMembers
    {
        internal const string DEFAULT_TXTBX_TEXT = "Type the name here...";

        private static string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

        [STAThread]
        internal static bool TryGetFilePathFromUser(out string filePath)
        {
            filePath = "";

            while (true)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select image";
                openFileDialog.InitialDirectory = EloGUIControlsStaticMembers.initialDirectory;
                openFileDialog.Filter = "Image files|*.gif;*.bmp;*.jpg;*.jpeg;*.png*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.Multiselect = false;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    EloGUIControlsStaticMembers.initialDirectory = Path.GetDirectoryName(filePath);

                    return true;
                }
                else { return false; }

            }
        }

        public static void AddPlayersToImgCmbBox(ImageComboBox imgCmbBx, EloData eloSystem)
        {
            var currentSelection = imgCmbBx.SelectedValue as SCPlayer;

            Func<SCPlayer, Image> GetImage = c =>
            {
                EloImage eloImg;

                if (eloSystem.TryGetImage(c.ImageID, out eloImg)) { return eloImg.Image; }
                else { return null; }

            };

            Func<SCPlayer, bool> HasExtraIdentifiers = player => { return player.IRLName != String.Empty || player.Team != null; };

            imgCmbBx.DisplayMember = "Item1";
            imgCmbBx.ValueMember = "Item2";
            imgCmbBx.ImageMember = "Item3";

            var items = eloSystem.GetPlayers().OrderBy(scPlayer => scPlayer.Name).Select(scPlayer => Tuple.Create<string, SCPlayer, Image>(String.Format("{0} {1}", scPlayer.Name, HasExtraIdentifiers(scPlayer) ? "(" + scPlayer.TeamName + " | " + scPlayer.IRLName + ")" : String.Empty), scPlayer, GetImage(scPlayer))).ToList();

            imgCmbBx.DataSource = items;


            if (currentSelection != null && items.Any(item => item.Item2 == currentSelection)) { imgCmbBx.SelectedIndex = items.IndexOf(items.First(item => item.Item2 == currentSelection)); }
        }


    }
}
