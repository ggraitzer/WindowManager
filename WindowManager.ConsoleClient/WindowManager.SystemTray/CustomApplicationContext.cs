using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WindowManager.Logging;

namespace WindowManager.SystemTray
{
    internal class CustomApplicationContext : ApplicationContext
    {
        private const string IconFileName = "favicon.ico";

        public string DefaultTooltip { get; private set; }
        
        private WindowManager windowManager;
        private Container components;
        private NotifyIcon notifyIcon;
        private Logger logger;

        private MainForm mainForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomApplicationContext"/> class.
        /// </summary>
        public CustomApplicationContext()
        {
            logger = new Logger($"{AppContext.BaseDirectory}\\Logs");
            logger.LogInformation("**********APPLICATION START**********");
            logger.LogInformation("+ CustomApplicationContext()");

            InitializeContext();
            windowManager = new WindowManager(notifyIcon);

            logger.LogInformation("- CustomApplicationContext()");
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        private void ShowForm()
        {
            logger.LogInformation("+ ShowForm()");

            if (mainForm == null)
            {
                mainForm = new MainForm();
                mainForm.FormClosed += MainForm_FormClosed;
                mainForm.Show();
            }
            else
            {
                mainForm.Activate();
                mainForm.Show();
            }

            logger.LogInformation("- ShowForm()");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            logger.LogInformation("+ MainForm_FormClosed()");

            mainForm = null;

            logger.LogInformation("- MainForm_FormClosed()");
        }

        /// <summary>
        /// Initializes the context.
        /// </summary>
        private void InitializeContext()
        {
            logger.LogInformation("+ InitializeContext()");

            components = new Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(IconFileName),
                Text = DefaultTooltip,
                Visible = true
            };

            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseUp += notifyIcon_MouseUp;

            logger.LogInformation("- InitializeContext()");
        }

        #region NotifyIcon Events

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            logger.LogInformation("+ notifyIcon_MouseUp()");

            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }

            logger.LogInformation("- notifyIcon_MouseUp()");
        }

        /// <summary>
        /// Handles the DoubleClick event of the notifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            logger.LogInformation("+ notifyIcon_DoubleClick()");

            ShowForm();

            logger.LogInformation("- notifyIcon_DoubleClick()");
        }

        #endregion NotifyIcon Events

        #region Context Menu Strip

        /// <summary>
        /// Handles the Opening event of the ContextMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            logger.LogInformation("+ ContextMenuStrip_Opening()");

            e.Cancel = false;

            LoadContextMenuStripItemList();

            logger.LogInformation("- ContextMenuStrip_Opening()");
        }

        private void LoadContextMenuStripItemList()
        {
            logger.LogInformation("+ LoadContextMenuStripItemList()");

            windowManager.BuildContextMenu(notifyIcon.ContextMenuStrip);

            notifyIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                windowManager.ToolStripMenuItemWithHandler("&Show", showItem_Click),
                new ToolStripSeparator(),
                windowManager.ToolStripMenuItemWithHandler("&Exit", exitItem_Click)
            });

            logger.LogInformation("- LoadContextMenuStripItemList()");
        }

        private void showItem_Click(object sender, EventArgs e)
        {
            logger.LogInformation("+ showItem_Click()");

            ShowForm();

            logger.LogInformation("- showItem_Click()");
        }

        /// <summary>
        /// Handles the Click event of the exitItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            logger.LogInformation("+ exitItem_Click()");

            ExitThread();

            logger.LogInformation("- exitItem_Click()");
        }

        #endregion Context Menu Strip

        #region ApplicationContext and Dispose Implementation

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            logger.LogInformation("+ Dispose()");

            if (disposing && components != null)
            {
                components.Dispose();
            }

            logger.LogInformation("- Dispose()");
        }

        /// <summary>
        /// Exits the thread core.
        /// </summary>
        protected override void ExitThreadCore()
        {
            logger.LogInformation("+ ExitThreadCore()");

            if (mainForm != null)
            {
                mainForm.Close();
            }
            notifyIcon.Visible = false; // should remove lingering tray icon!
            base.ExitThreadCore();

            logger.LogInformation("- ExitThreadCore()");
        }

        #endregion ApplicationContext and Dispose Implementation
    }
}