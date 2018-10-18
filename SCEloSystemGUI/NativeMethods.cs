using System;
using System.Runtime.InteropServices;

namespace SCEloSystemGUI
{
    internal static class NativeMethods
    {
        [DllImport("User32.dll")]
        internal static extern int ShowWindowAsync(IntPtr hWnd, int swCommand);
    }
}
