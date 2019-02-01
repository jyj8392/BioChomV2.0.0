using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BioChome.Equipment.Dialog
{
    public partial class MethodNewer : Form
    {
        public MethodNewer()
        {
            InitializeComponent();
        }

        public string methodName;
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (MethodFileName.Text == "") return;
            methodName = "方法-" + MethodFileName.Text;
            this.Close();
        }

        private void MethodNewer_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
