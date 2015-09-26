using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowManager.WindowLibrary;

namespace WindowManager.SystemTray
{
    class WindowManagerTool
    {
        private readonly NotifyIcon notifyIcon;
        
        public WindowManagerTool(NotifyIcon notifyIcon)
        {
            this.notifyIcon = notifyIcon;
        }
        /// <summary>
        /// Tools the strip menu item with handler.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        /// <param name="eventHandler">The event handler.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Builds the context menu.
        /// </summary>
        /// <param name="contextMenuStrip">The context menu strip.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void BuildContextMenu(ContextMenuStrip contextMenuStrip)
        {
            contextMenuStrip.Items.Clear();

            var switchWindow = new ToolStripMenuItem("Move Window");
            switchWindow.DropDownItems.AddRange(GetActiveWindowsList(switchWindowCommandItem_Click).ToArray());

            contextMenuStrip.Items.Add(switchWindow);
        }

        /// <summary>
        /// Gets the active windows list.
        /// </summary>
        /// <returns>IEnumerable<ToolStripMenuItem></returns>
        private IEnumerable<ToolStripMenuItem> GetActiveWindowsList(EventHandler eventHandler)
        {
            return WindowUtilities.GetWindows().OrderBy(win => win.Name).Select(win => ToolStripMenuItemWithHandler(win.Name, eventHandler));
        }

        private void switchWindowCommandItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem itemClicked = ((ToolStripMenuItem)sender);
            var windowName = itemClicked.Text;
            MessageBox.Show($"Moving window {windowName}");
        }
    }
}
