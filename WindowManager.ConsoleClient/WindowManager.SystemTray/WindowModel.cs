using System.Drawing;

namespace WindowManager.SystemTray
{
    internal class WindowModel
    {
        public string Name { get; set; }
        public Rectangle Rect { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}