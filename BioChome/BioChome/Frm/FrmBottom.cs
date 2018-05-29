using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BioChome
{
    public partial class FrmBottom : DockContent
    {
        private static FrmBottom Instance;
        public static int LogCnt = 0;

        public FrmBottom()
        {
            InitializeComponent();
        }

        public static FrmBottom GetInstance()
        {
            Instance = new FrmBottom();
            return Instance;
        }

        private void FrmBottom_Load(object sender, EventArgs e)
        {
            this.DockPanel.DockBottomPortion = 200;
        }

        private void FrmBottom_DockStateChanged(object sender, EventArgs e)
        {
            if (Instance != null)
            {
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.ms_FrmBottom = this.DockState;
            }

        }

        private void FrmBottom_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AppConfig.frmBottom = null;
            e.Cancel = true;
            this.Hide();
        }

        private void Log_SelectAll_Click(object sender, EventArgs e)
        {
            EquipmentLOG_Text.SelectAll();
        }

        private void Log_Copy_Click(object sender, EventArgs e)
        {
            EquipmentLOG_Text.Copy();
        }

        private void Log_Clear_Click(object sender, EventArgs e)
        {
            EquipmentLOG_Text.Clear();
        }
    }
}
