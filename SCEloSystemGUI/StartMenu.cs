using EloSystem;
using EloSystem.IO;
using MLCommon.GUI.SplashScreenWorker;
using System;
using System.IO;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class StartMenu : Form
    {
        internal EloData EloSystem { get; private set; }

        public StartMenu()
        {
            InitializeComponent();
        }

        [STAThread]
        private static bool TryGetFilePathFromUser(string dialogTitle, string initialDirectory, string filter, out string filePath)
        {
            filePath = "";

            while (true)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = dialogTitle;
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = filter;
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


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Hide();
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            if (EloSystemGUIStaticMembers.GetEloSystemName(ref fileName) == DialogResult.OK)
            {
                this.EloSystem = new EloData(fileName);
                this.DialogResult = DialogResult.OK;
            }
            else { this.DialogResult = DialogResult.Cancel; }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filePath;

            if (StartMenu.TryGetFilePathFromUser("Select Elo System to load", StaticMembers.SaveDirectory, "(*" + StaticMembers.FILE_EXTENSION_NAME + "*)|*" + StaticMembers.FILE_EXTENSION_NAME + "*"
                , out filePath))
            {
                this.LoadSCEloSystem(filePath);

                if (this.EloSystem != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }

            }

        }

        private void LoadSCEloSystem(string path)
        {
            var loaderScreen = new LoaderScreen(path);

            SplashScreen.Show(loaderScreen);

            EloData eloSystem;

            if (loaderScreen.TryGetProcessResult(out eloSystem))
            {
                this.EloSystem = eloSystem;
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            else { MessageBox.Show("The file could not be recognized as a an SC Elo System.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
