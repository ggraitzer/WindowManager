using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowManager.WindowLibrary.Models;

namespace WindowManager.WindowLibrary
{
    public class WindowUtilities
    {
        /// <summary>
        /// filter function
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        /// <summary>
        /// check if windows visible
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// return windows text
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpWindowText"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        /// <summary>
        /// enumarator on all desktop windows
        /// </summary>
        /// <param name="hDesktop"></param>
        /// <param name="lpEnumCallbackFunction"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint = true);

        public static Window GetWindow(IntPtr hWnd)
        {
            Window window = new Window();

            RECT rect;
            WindowUtilities.GetWindowRect(hWnd, out rect);
            window.Rectangle = rect;

            StringBuilder strbTitle = new StringBuilder(255);
            int nLength = WindowUtilities.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
            window.Name = strbTitle.ToString();

            return window;
        }

        public static IEnumerable<Window> GetWindows()
        {
            List<Window> windows = new List<Window>();

            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = WindowUtilities.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (WindowUtilities.IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false)
                {
                    RECT rectangle;
                    WindowUtilities.GetWindowRect(hWnd, out rectangle);

                    Window win = new Window();
                    win.Id = hWnd;
                    win.Name = strTitle;
                    win.Rectangle = rectangle;

                    windows.Add(win);
                }

                return true;
            };

            if (!WindowUtilities.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
            {
                throw new Exception("EnumDesktopWindows Failed!");
            }

            return windows;
        }
    }
}
