namespace UserDataBackUp
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnClose = new System.Windows.Forms.Button();
            this.openInputFile = new System.Windows.Forms.OpenFileDialog();
            this.saveOutputFile = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.lblInput = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(346, 231);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOutput);
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.MaximumSize = new System.Drawing.Size(457, 225);
            this.tabControl1.MinimumSize = new System.Drawing.Size(457, 225);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 225);
            this.tabControl1.TabIndex = 0;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.label6);
            this.tabOutput.Controls.Add(this.label4);
            this.tabOutput.Controls.Add(this.btnOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(449, 199);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "导出";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "请按「导出」按钮开始用户信息导出的操作。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(407, 108);
            this.label4.TabIndex = 13;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(342, 6);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(99, 23);
            this.btnOutput.TabIndex = 0;
            this.btnOutput.Text = "导出";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.lblInput);
            this.tabInput.Controls.Add(this.btnInput);
            this.tabInput.Location = new System.Drawing.Point(4, 22);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(449, 199);
            this.tabInput.TabIndex = 1;
            this.tabInput.Text = "导入";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(6, 17);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(245, 12);
            this.lblInput.TabIndex = 20;
            this.lblInput.Text = "请按「导入」按钮开始用户信息导入的操作。";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(342, 6);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(99, 23);
            this.btnInput.TabIndex = 0;
            this.btnInput.Text = "导入";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 262);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 300);
            this.MinimumSize = new System.Drawing.Size(465, 300);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple EA Application -- UserDataBackUp";
            this.tabControl1.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabOutput.PerformLayout();
            this.tabInput.ResumeLayout(false);
            this.tabInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog openInputFile;
        private System.Windows.Forms.SaveFileDialog saveOutputFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInput;
    }
}

