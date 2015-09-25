using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowManager.WindowLibrary.Models
{
    public class Window
    {
        public IntPtr Id;
        public string Name;
        public Rect Rectangle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner

        public override string ToString()
        {
            return string.Format("[left {0} top {1} right {2} bottom {3}]", Left, Top, Right, Bottom);
        }
    }
}
