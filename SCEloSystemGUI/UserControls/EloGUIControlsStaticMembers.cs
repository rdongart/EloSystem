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
    public delegate Image ImageGetter(int imdageID);

    internal static class EloGUIControlsStaticMembers
    {
        internal const string DEFAULT_TXTBX_TEXT = "Type the name here...";

        private static string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

        internal static ImageComboBox CreateStandardContentAdderImageComboBox()
        {
            return new ImageComboBox()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 154,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(4, 2, 4, 2),
                ItemHeight = 18,
                Margin = new Padding(6, 3, 6, 3),
                SelectedItemFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(154, 24),
            };
        }

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
