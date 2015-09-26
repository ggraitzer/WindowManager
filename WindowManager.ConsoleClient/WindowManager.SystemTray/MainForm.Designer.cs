namespace WindowManager.SystemTray
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.windowListButton = new System.Windows.Forms.Button();
            this.WindowListBox = new System.Windows.Forms.ListBox();
            this.windowModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.windowHandleTextBox = new System.Windows.Forms.TextBox();
            this.leftTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.topTextBox = new System.Windows.Forms.TextBox();
            this.bottomTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rightTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.windowModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // windowListButton
            // 
            this.windowListButton.Location = new System.Drawing.Point(12, 12);
            this.windowListButton.Name = "windowListButton";
            this.windowListButton.Size = new System.Drawing.Size(253, 23);
            this.windowListButton.TabIndex = 0;
            this.windowListButton.Text = "Update Window List";
            this.windowListButton.UseVisualStyleBackColor = true;
            this.windowListButton.Click += new System.EventHandler(this.windowListButton_Click);
            // 
            // WindowListBox
            // 
            this.WindowListBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.windowModelBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WindowListBox.DataSource = this.windowModelBindingSource;
            this.WindowListBox.FormattingEnabled = true;
            this.WindowListBox.Location = new System.Drawing.Point(12, 41);
            this.WindowListBox.Name = "WindowListBox";
            this.WindowListBox.Size = new System.Drawing.Size(253, 368);
            this.WindowListBox.TabIndex = 1;
            this.WindowListBox.SelectedIndexChanged += new System.EventHandler(this.WindowListBox_SelectedIndexChanged);
            // 
            // windowModelBindingSource
            // 
            this.windowModelBindingSource.DataSource = typeof(WindowManager.SystemTray.WindowModel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "hWnd:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Position:";
            // 
            // windowHandleTextBox
            // 
            this.windowHandleTextBox.Location = new System.Drawing.Point(359, 38);
            this.windowHandleTextBox.Name = "windowHandleTextBox";
            this.windowHandleTextBox.Size = new System.Drawing.Size(164, 20);
            this.windowHandleTextBox.TabIndex = 4;
            // 
            // leftTextBox
            // 
            this.leftTextBox.Location = new System.Drawing.Point(393, 65);
            this.leftTextBox.Name = "leftTextBox";
            this.leftTextBox.Size = new System.Drawing.Size(130, 20);
            this.leftTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Left:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Top:";
            // 
            // topTextBox
            // 
            this.topTextBox.Location = new System.Drawing.Point(393, 94);
            this.topTextBox.Name = "topTextBox";
            this.topTextBox.Size = new System.Drawing.Size(130, 20);
            this.topTextBox.TabIndex = 8;
            // 
            // bottomTextBox
            // 
            this.bottomTextBox.Location = new System.Drawing.Point(393, 153);
            this.bottomTextBox.Name = "bottomTextBox";
            this.bottomTextBox.Size = new System.Drawing.Size(130, 20);
            this.bottomTextBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(344, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Bottom:";
            // 
            // rightTextBox
            // 
            this.rightTextBox.Location = new System.Drawing.Point(393, 124);
            this.rightTextBox.Name = "rightTextBox";
            this.rightTextBox.Size = new System.Drawing.Size(130, 20);
            this.rightTextBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(352, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Right:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 426);
            this.Controls.Add(this.bottomTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rightTextBox);
            this.Controls.Add(this.topTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.leftTextBox);
            this.Controls.Add(this.windowHandleTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WindowListBox);
            this.Controls.Add(this.windowListButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.windowModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button windowListButton;
        private System.Windows.Forms.ListBox WindowListBox;
        private System.Windows.Forms.BindingSource windowModelBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox windowHandleTextBox;
        private System.Windows.Forms.TextBox leftTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox topTextBox;
        private System.Windows.Forms.TextBox bottomTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rightTextBox;
        private System.Windows.Forms.Label label7;
    }
}

