using System;

namespace EloSystem.IO
{
    public delegate bool FileOverwriteEventHandler(object sender, FileOverwriteEventArgs e);

    public class FileOverwriteEventArgs : EventArgs
    {
        public String FileName { get; private set; }

        public FileOverwriteEventArgs(string fileName)
        {
            this.FileName = fileName;
        }
    }
}