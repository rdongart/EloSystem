using EloSystem;
using MLCommon.GUI.SplashScreenWorker;
using SerializerProgress;
using System.ComponentModel;
using System.IO;

namespace SCEloSystemGUI
{
    internal class LoaderScreen: ProgressCounter
    {
        private string path;
        private EloData processResult;

        internal LoaderScreen(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <remarks>Will give the result GameData object after the process has been run.</remarks>
        /// <returns></returns>
        internal bool TryGetProcessResult(out EloData result)
        {
            result = this.processResult;

            return this.processResult != null;
        }
        private void LoadingSaveGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.IncrementProgressValueCounter();
        }
        public override bool ProcessShouldRunNow()
        {
            return File.Exists(path);
        }
        public override void RunProcess()
        {
            this.SetProgressValueMax(100);

            using (Stream fileStream = File.Open(this.path, FileMode.Open))
            {
                this.ChangeProcessDescription("Loading...");

                this.processResult = Utilities.Deserialize<object>(fileStream, this.LoadingSaveGame_ProgressChanged) as EloData;

                fileStream.Close();

                this.ProcessEnd();
            }
        }
    }
}
