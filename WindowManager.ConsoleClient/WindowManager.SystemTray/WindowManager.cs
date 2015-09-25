using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowManager.SystemTray
{
    class WindowManager
    {
        public bool FirstRun { get; internal set; }

        public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null)
            {
                item.Click += eventHandler;
            }
            item.ToolTipText = "Tool Tip Text";
            return item;
        }
    }
}
