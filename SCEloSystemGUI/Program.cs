using CustomControls.Styles;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using SCEloSystemGUI.UserControls;

namespace SCEloSystemGUI
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Indicates whether this is the first application instance
            bool firstApplicationInstance;

            // Obtain the mutex name from the full assembly name
            string mutexNameTester = Assembly.GetEntryAssembly().FullName;

            using (Mutex mutex = new Mutex(false, mutexNameTester, out firstApplicationInstance))
            {
                if (!firstApplicationInstance)
                {
                    string processName = Assembly.GetEntryAssembly().GetName().Name;

                    Program.BringOldInstanceToFront(processName);
                }
                else
                {
                    GC.Collect();

                    try
                    {
                        var menu = new StartMenu();

                        while (menu.ShowDialog() == DialogResult.OK)
                        {
                            GlobalState.Initialize(menu.EloSystem);

                            var mainEloSystemForm = new MainForm();

                            FormStyles.CorrectSizeToScreenSize(mainEloSystemForm, Screen.FromControl(menu));

                            Application.Run(mainEloSystemForm);
                        }

                        menu.Close();
                    }
                    finally
                    {
                        // do any resource cleanup here 
                    }
                }

            }

            Environment.Exit(0);
        }

        private static void BringOldInstanceToFront(string processName)
        {
            Process[] RunningProcesses = Process.GetProcessesByName(processName);

            if (RunningProcesses.Length > 0)
            {
                Process runningProcess = RunningProcesses[0];
                if (runningProcess != null)
                {
                    IntPtr mainWindowHandle = runningProcess.MainWindowHandle;
                    NativeMethods.ShowWindowAsync(mainWindowHandle, (int)WindowConstants.ShowWindowConstants.SW_SHOWMINIMIZED);
                    NativeMethods.ShowWindowAsync(mainWindowHandle, (int)WindowConstants.ShowWindowConstants.SW_RESTORE);
                }

            }

        }
    }
}
