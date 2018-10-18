using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

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
                            //switch (menu.MenuResult)
                            //{
                            //    case StartMenuResult.Abort:
                            //        throw new Exception(String.Format("Unable to handle {0} with a value of {1} in the current environment."
                            //            , typeof(StartMenuResult).Name, menu.MenuResult.ToString()));
                            //    case StartMenuResult.RunEditor:
                            //        var editor = new DataEditor(true);
                            //        Application.Run(editor);
                            //        break;
                            //    case StartMenuResult.RunGame:
                            //        var mainGameForm = new ManagerLifeMainForm(menu.Game);

                            //        ManagerLifeGUI.UI.RegisterForm(mainGameForm);

                            var mainEloSystemForm = new MainForm(menu.EloSystem);

                            Application.Run(mainEloSystemForm);
                            //        break;
                            //    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(StartMenuResult).Name, menu.MenuResult.ToString()));
                            //}
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


            Application.Run(new StartMenu());
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
