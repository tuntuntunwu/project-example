using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleEAregistration
{
    public partial class frmFlash : Form
    {
        public frmFlash()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.DialogResult = DialogResult.OK;
        }

        private void frmflash_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }
    }
}