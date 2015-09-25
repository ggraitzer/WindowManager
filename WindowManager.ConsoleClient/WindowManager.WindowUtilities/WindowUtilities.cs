using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using WindowManager.WindowLibrary.Models;
using System.Threading;
using System.Diagnostics;

namespace WindowManager.WindowLibrary
{
    public class WindowUtilities
    {
        private const string BoxName = "WindowManager Box Guide";

        #region Windows DLL Imports
        /// <summary>
        /// filter function
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        /// <summary>
        /// check if windows visible
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// return windows text
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpWindowText">The lp window text.</param>
        /// <param name="nMaxCount">The n maximum count.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        /// <summary>
        /// enumarator on all desktop windows
        /// </summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <param name="lpEnumCallbackFunction">The lp enum callback function.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        /// <summary>
        /// Gets the window rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out Models.Rect lpRect);

        /// <summary>
        /// Moves the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        /// <param name="Repaint">if set to <c>true</c> [repaint].</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint = true);

        // For Windows Mobile, replace user32.dll with coredll.dll
        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// <summary>
        /// Finds the window by caption.
        /// </summary>
        /// <param name="ZeroOnly">The zero only.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        /// <summary>
        /// Gets the cursor position.
        /// </summary>
        /// <param name="lpPoint">The lp point.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// Point coordinates
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// The x
            /// </summary>
            public int X;
            /// <summary>
            /// The y
            /// </summary>
            public int Y;

            public override string ToString()
            {
                return $"{base.ToString()} {"{"}{X},{Y}{"}"}";
            }
        }

        #endregion

        public static Window GetWindow(IntPtr hWnd)
        {
            Window window = new Window();

            Rect rect;
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
                    Rect rectangle;
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

        public static void MoveWindow(IntPtr hWnd, Rect rect)
        {
            WindowUtilities.MoveWindow(hWnd, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top + 50);
        }

        public static POINT MousePosition
        {
            get
            {
                POINT p;
                GetCursorPos(out p);
                return p;
            }
        }

        public static void CreateBox(Rect rectangle)
        {
            Form f = new Form();
            f.Name = BoxName;
            f.BackColor = Color.AliceBlue;
            f.FormBorderStyle = FormBorderStyle.None;
            f.TopMost = true;
            f.Opacity = 0.3;

            Application.EnableVisualStyles();
            Task.Run(() =>
            {
                Application.Run(f);
            });

            MoveBox(rectangle);
        }

        public static void RemoveBox()
        {
            IntPtr hWnd = FindBox(new TimeSpan(0, 0, 1));
            var proc = Process.GetProcesses().Where(p => p.Handle == hWnd).Single();
            if (proc == null)
            {
                return;
            }

            proc.Kill();
        }

        public static void MoveBox(Rect rect)
        {
            IntPtr hWnd = FindBox(new TimeSpan(0, 0, 1));
            MoveWindow(hWnd, rect);
        }

        private static IntPtr FindBox(TimeSpan timeout)
        {
            DateTime time = DateTime.Now;
            IntPtr hWnd = IntPtr.Zero;

            while (DateTime.Now < time.Add(timeout) || hWnd != IntPtr.Zero)
            {
                hWnd = FindWindowByCaption(IntPtr.Zero, BoxName);
            }

            return hWnd;
        }
    }
}
