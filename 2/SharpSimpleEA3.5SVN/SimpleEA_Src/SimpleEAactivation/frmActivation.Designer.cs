namespace SimpleEAactivation
{
    partial class frmActivation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivation));
            this.lblRegisterCode = new System.Windows.Forms.Label();
            this.richTxt_RegisterCode = new System.Windows.Forms.RichTextBox();
            this.richTxt_ActivationCode = new System.Windows.Forms.RichTextBox();
            this.btnActive = new System.Windows.Forms.Button();
            this.lblActivationCode = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblDealer = new System.Windows.Forms.Label();
            this.txtDealer = new System.Windows.Forms.TextBox();
            this.lblRegisterKEY = new System.Windows.Forms.Label();
            this.richTxtRegisterKEY = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblRegisterCode
            // 
            this.lblRegisterCode.AutoSize = true;
            this.lblRegisterCode.Location = new System.Drawing.Point(53, 235);
            this.lblRegisterCode.Name = "lblRegisterCode";
            this.lblRegisterCode.Size = new System.Drawing.Size(41, 12);
            this.lblRegisterCode.TabIndex = 8;
            this.lblRegisterCode.Text = "生成码";
            // 
            // richTxt_RegisterCode
            // 
            this.richTxt_RegisterCode.Location = new System.Drawing.Point(133, 217);
            this.richTxt_RegisterCode.Name = "richTxt_RegisterCode";
            this.richTxt_RegisterCode.Size = new System.Drawing.Size(437, 75);
            this.richTxt_RegisterCode.TabIndex = 9;
            this.richTxt_RegisterCode.Text = "";
            // 
            // richTxt_ActivationCode
            // 
            this.richTxt_ActivationCode.Location = new System.Drawing.Point(133, 302);
            this.richTxt_ActivationCode.Name = "richTxt_ActivationCode";
            this.richTxt_ActivationCode.Size = new System.Drawing.Size(437, 75);
            this.richTxt_ActivationCode.TabIndex = 11;
            this.richTxt_ActivationCode.Text = "";
            // 
            // btnActive
            // 
            this.btnActive.Location = new System.Drawing.Point(216, 445);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(75, 37);
            this.btnActive.TabIndex = 31;
            this.btnActive.Text = "激活";
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // lblActivationCode
            // 
            this.lblActivationCode.AutoSize = true;
            this.lblActivationCode.Location = new System.Drawing.Point(53, 348);
            this.lblActivationCode.Name = "lblActivationCode";
            this.lblActivationCode.Size = new System.Drawing.Size(41, 12);
            this.lblActivationCode.TabIndex = 10;
            this.lblActivationCode.Text = "激活码";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(332, 445);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 37);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(166, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(302, 27);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Simple EA 激活码工具";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(133, 112);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(437, 21);
            this.txtCustomer.TabIndex = 3;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(133, 143);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(437, 21);
            this.txtAddress.TabIndex = 5;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(53, 115);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 12);
            this.lblCustomer.TabIndex = 2;
            this.lblCustomer.Text = "客户名称";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(53, 146);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(29, 12);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "地址";
            // 
            // lblDealer
            // 
            this.lblDealer.AutoSize = true;
            this.lblDealer.Location = new System.Drawing.Point(53, 177);
            this.lblDealer.Name = "lblDealer";
            this.lblDealer.Size = new System.Drawing.Size(65, 12);
            this.lblDealer.TabIndex = 6;
            this.lblDealer.Text = "所属经销商";
            // 
            // txtDealer
            // 
            this.txtDealer.Location = new System.Drawing.Point(133, 174);
            this.txtDealer.Name = "txtDealer";
            this.txtDealer.Size = new System.Drawing.Size(437, 21);
            this.txtDealer.TabIndex = 7;
            // 
            // lblRegisterKEY
            // 
            this.lblRegisterKEY.AutoSize = true;
            this.lblRegisterKEY.Location = new System.Drawing.Point(53, 402);
            this.lblRegisterKEY.Name = "lblRegisterKEY";
            this.lblRegisterKEY.Size = new System.Drawing.Size(41, 12);
            this.lblRegisterKEY.TabIndex = 12;
            this.lblRegisterKEY.Text = "注册码";
            // 
            // richTxtRegisterKEY
            // 
            this.richTxtRegisterKEY.Location = new System.Drawing.Point(133, 386);
            this.richTxtRegisterKEY.Name = "richTxtRegisterKEY";
            this.richTxtRegisterKEY.Size = new System.Drawing.Size(437, 45);
            this.richTxtRegisterKEY.TabIndex = 13;
            this.richTxtRegisterKEY.Text = "";
            // 
            // frmActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 494);
            this.Controls.Add(this.richTxtRegisterKEY);
            this.Controls.Add(this.lblRegisterKEY);
            this.Controls.Add(this.txtDealer);
            this.Controls.Add(this.lblDealer);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblActivationCode);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.richTxt_ActivationCode);
            this.Controls.Add(this.richTxt_RegisterCode);
            this.Controls.Add(this.lblRegisterCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmActivation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple EA 激活工具";
            this.Load += new System.EventHandler(this.frmActivation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegisterCode;
        private System.Windows.Forms.RichTextBox richTxt_RegisterCode;
        private System.Windows.Forms.RichTextBox richTxt_ActivationCode;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.Label lblActivationCode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblDealer;
        private System.Windows.Forms.TextBox txtDealer;
        private System.Windows.Forms.Label lblRegisterKEY;
        private System.Windows.Forms.RichTextBox richTxtRegisterKEY;
    }
}

