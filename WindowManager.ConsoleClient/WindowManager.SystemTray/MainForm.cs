using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowManager.WindowLibrary;
using WindowManager.WindowLibrary.Models;

namespace WindowManager.SystemTray
{
    public partial class MainForm : Form
    {
        private const string IconFileName = "favicon.ico";

        public MainForm()
        {
            this.Icon = new Icon(IconFileName);
            this.Text = "Window Manager";
            InitializeComponent();
            UpdateWindowList();
        }

        private void UpdateWindowList()
        {
            var windowList = WindowUtilities.GetWindows();

            windowModelBindingSource.Clear();

            windowList.ToList().ForEach(win => windowModelBindingSource.Add(new WindowModel
            {
                Name = win.Name,
                Rect = new Rectangle(win.Rectangle.Left, win.Rectangle.Top, win.Rectangle.Right - win.Rectangle.Left, win.Rectangle.Bottom - win.Rectangle.Top)
            }));
        }

        private void windowListButton_Click(object sender, EventArgs e)
        {
            UpdateWindowList();
        }

        private void WindowListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (windowModelBindingSource != null && windowModelBindingSource.Current != null)
            {
                //WindowModel window = (WindowModel)windowModelBindingSource.Current;
                //windowHandleTextBox.Text = window.Name;
                //leftTextBox.Text = window.Rect.Left.ToString();
                //topTextBox.Text = window.Rect.Top.ToString();
                //rightTextBox.Text = window.Rect.Right.ToString();
                //bottomTextBox.Text = window.Rect.Bottom.ToString();
            }
        }
    }
}
