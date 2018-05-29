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
    public partial class FrmPH : DockContent
    {
        //private static FrmPH Instance;

        public FrmPH()
        {            
            InitializeComponent();
        }

        public static FrmPH GetInstance()
        {
            //if (Instance == null)
            //{
            //    Instance = new FrmPH();
            //    Instance.TabText = "PH";
            //}
            return new FrmPH();
        }

        private void FrmPH_DockStateChanged(object sender, EventArgs e)
        {
            //关闭时（dockstate为unknown） 不把dockstate保存 
            //if (Instance != null)
            //{
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.ms_FrmFunction = this.DockState;
            //}

        }

        private void FrmPH_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Instance = null;  // 否则下次打开时报错，提示“无法访问已释放对象”        
            if (FrmLeft.phInstance != null)
                FrmLeft.phInstance.PHInstanceDispose();

        }

        private void FrmPH_Load(object sender, EventArgs e)
        {

        }

        private void PHInfo_Fresher_Tick(object sender, EventArgs e)
        {
            if (FrmLeft.phInstance != null && FrmLeft.phInstance.t_SerialPortCommu.phPort != null && FrmLeft.phInstance.t_SerialPortCommu.phPort.IsOpen)
            {
                ConductanceValue_Label.Text = string.Format("{0:000000}", FrmLeft.phInstance.t_PHInfo.conductance);
                PHValue_Label.Text = string.Format("{0:0.0}", FrmLeft.phInstance.t_PHInfo.ph);
                TemperatureValue_Label.Text = string.Format("{0:00.0}", FrmLeft.phInstance.t_PHInfo.temperature);
            } else
            {
                ConductanceValue_Label.Text = "-------";
                PHValue_Label.Text = "-------";
                TemperatureValue_Label.Text = "-------";
            }
        }

    }
}
