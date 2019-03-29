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

        internal static ImprovedImageComboBox<T> CreateStandardImprovedImageComboBox<T>(ImageGetter<T> imageGetter) where T : class, IHasName
        {
            return new ImprovedImageComboBox<T>()
            {
                Dock = DockStyle.Fill,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 160,
                Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FormattingEnabled = true,
                ImageMargin = new Padding(4, 2, 4, 2),
                ItemHeight = 20,
                Margin = new Padding(6, 3, 6, 3),
                SelectedItemFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Size = new Size(154, 26),
                ImageGetter = imageGetter,
                NameGetter = t => t.Name                
            };
        }

        internal static string ConvertRatingChangeString(string ratingChangeTxt)
        {
            int ratingChangeValue = 0;

            bool hasRatingValue = int.TryParse(ratingChangeTxt, out ratingChangeValue);

            return String.Format("{0}{1}", ratingChangeValue > 0 ? "+" : "", hasRatingValue ? ratingChangeValue.ToString(EloSystemGUIStaticMembers.NUMBER_FORMAT) : ratingChangeTxt);
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
    }
}
