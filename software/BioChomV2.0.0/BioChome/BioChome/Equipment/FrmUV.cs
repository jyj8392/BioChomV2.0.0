using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using UVDetector;
using System.Xml;
using BioChome.Equipment.Dialog;

namespace BioChome
{
    public partial class FrmUV : DockContent
    {
        //private static FrmUV Instance;

        public FrmUV()
        {            
            InitializeComponent();
        }

        public static FrmUV GetInstance()
        {
            //if (Instance == null)
            //{
            //    Instance = new FrmUV();
            //    Instance.TabText = "UV Detector";
            //}
            return new FrmUV();
        }

        private void FrmUV_Load(object sender, EventArgs e)
        {
            UVVPS_ComboBox.Items.Add("0.1");
            UVVPS_ComboBox.Items.Add("0.2");
            UVVPS_ComboBox.Items.Add("0.5");
            UVVPS_ComboBox.Items.Add("1");
            UVVPS_ComboBox.Items.Add("2");
            UVVPS_ComboBox.Items.Add("3");
            UVVPS_ComboBox.Items.Add("4");
            UVVPS_ComboBox.Items.Add("5");
            UVVPS_ComboBox.Items.Add("10");
            UVVPS_ComboBox.Text = UVVPS_ComboBox.Items[0].ToString();

            UVAUUnit_ComboBox.Items.Add("mAu");
            UVAUUnit_ComboBox.Items.Add("Au");
            UVAUUnit_ComboBox.Text = UVAUUnit_ComboBox.Items[0].ToString();
            UVTimeUnit_ComboBox.Items.Add("min");
            UVTimeUnit_ComboBox.Items.Add("h");
            UVTimeUnit_ComboBox.Text = UVTimeUnit_ComboBox.Items[0].ToString();

            CurvAUUnit_ComboBox.Items.Add("mAu");
            CurvAUUnit_ComboBox.Items.Add("Au");
            CurvAUUnit_ComboBox.Text = UVAUUnit_ComboBox.Items[0].ToString();
            CurvTimeUnit_ComboBox.Items.Add("min");
            CurvTimeUnit_ComboBox.Items.Add("h");
            CurvTimeUnit_ComboBox.Text = UVTimeUnit_ComboBox.Items[0].ToString();

            CreateUVXmlFile(UVMethodXMLPath);
            foreach (XmlNode node in ReadUVXmlFile(UVMethodXMLPath)[1])
            {
                if (node.Name == "MethodConfig") continue;
                UVMethod_Browser.Items.Add(node.Name);
            }
            ReadUVMethod(UVMethodXMLPath,(UVMethod_Browser.SelectedIndex = ReadUVMethodConfig(UVMethodXMLPath)));
            //SaveUVXmlFile("UVMethod.Config", 1);
            //equ_UV.COMString = "2,45";
            //equ_UV.Protocol_Analysis(equ_UV.COMString);
            //MessageBox.Show(equ_UV.t_UVPara.id.ToString());
            //MessageBox.Show(equ_UV.t_UVPara.atx.ToString());

        }

        private void FrmUV_DockStateChanged(object sender, EventArgs e)
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


        private void FrmUV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UVSpectrum_Stop.Enabled)
            {
                DialogResult ret = MessageBox.Show("紫外检测器正在采集，是否立即停止？", "紫外检测器", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        UVSpectrum_Stop_Click(null, null);
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                }
            }

            //if (UVSpectrum_Stop.Enabled)
            //    UVSpectrum_Stop_Click(null,null);

            UVAnalysisInfo_Fresher.Enabled = false;
            SaveUVMethodConfig(UVMethodXMLPath);

            if (FrmLeft.uvInstance != null)
                FrmLeft.uvInstance.UVInstanceDispose();
            CurvAnalysis.UV.UVDispose();
            UVDetector.UV.UVDispose();
            //Hide();
            //e.Cancel = tr ue;
            /*
            DialogResult dia = MessageBox.Show("是否真的退出系统！", " ", MessageBoxButtons.OKCancel);
            if (dia == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

            if (dia == DialogResult.OK)
            {
                //Instance = null;
                e.Cancel = true;
                //Application.Exit();
            }*/
            //Instance = null;  // 否则下次打开时报错，提示“无法访问已释放对象”
        }

        private void UVAnalysisInfo_Fresher_Tick(object sender, EventArgs e)
        {
            UVTime_Analysis.Text = string.Format("{0:0.00}", CurvAnalysis.CurvShow.GetMouseTime()) + CurvAnalysis.CurvShow.GetCurv_xUnit();
            UVValue_Analysis.Text = string.Format("{0:0.00}", CurvAnalysis.CurvShow.GetMouseValue()) + CurvAnalysis.CurvShow.GetCurv_yUnit();

        }

        private void UVInfo_Fresher_Tick(object sender, EventArgs e)
        {
            if (FrmLeft.uvInstance != null
                && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null
                && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen)
            {
                if (!UVSpectrum_Start.Enabled && !UVSpectrum_Pause.Enabled && !UVSpectrum_Stop.Enabled)
                {
                    if (UVSpectrum_Pause.Checked)
                    {
                        UVSpectrum_Start.Enabled = false;
                        UVSpectrum_Pause.Enabled = true;
                        UVSpectrum_Stop.Enabled = true;
                    }
                    else
                        UVSpectrum_Start.Enabled = true;
                }
                UVProgram_Download.Enabled = true;
                OpenUVLamp.Enabled = true;
                SetUVCurvZero.Enabled = true;
            }
            else {
                if (!UVSpectrum_Start.Enabled && UVSpectrum_Pause.Enabled && UVSpectrum_Stop.Enabled && !UVSpectrum_Pause.Checked)
                {
                    Pause_UVProgram();
                }
                UVSpectrum_Start.Enabled = false;
                UVSpectrum_Pause.Enabled = false;
                UVSpectrum_Stop.Enabled = false;

                UVProgram_Download.Enabled = false;
                OpenUVLamp.Enabled = false;
                SetUVCurvZero.Enabled = false;
            }
            if (!FrmMain.ISRegisted())
            {
                UVSpectrum_Start.Enabled = false;
            }


            //string log = UVDetector.CurvShow.GetLog();
            //if (log != null && log != "")
            //{
            //    //AppConfig.frmBottom.EquipmentText_LOG.Lines[AppConfig.frmBottom.LogCnt++] = UVSpectrum_Show.Log.Text;
            //    //UVSpectrum_Show.Log.Text = "";
            //    FrmBottom.LogCnt++;
            //    AppConfig.frmBottom.EquipmentLOG_Text.Text = AppConfig.frmBottom.EquipmentLOG_Text.Text.Insert(0, "Line " + FrmBottom.LogCnt + log.ToString() + Environment.NewLine);
            //    UVDetector.CurvShow.ClearLog();
            //}
            if (UVVPS_ComboBox.Text != "")
                UVDetector.CurvShow.SetCurv_vps(Convert.ToDouble(UVVPS_ComboBox.Text));
            try {
                if (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - Convert.ToDouble(Text_UVSpectrum_Xmin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_xUnit(UVTimeUnit_ComboBox.Text);
                if (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_yUnit(UVAUUnit_ComboBox.Text);
                UVDetector.CurvShow.SetCurv_Color(UVWaveLength1Color_Set.BackColor, UVWaveLength2Color_Set.BackColor, UVWaveLength3Color_Set.BackColor);
            } catch
            {

            }
            //if (Convert.ToDouble(Text_Analysis_Xmax.Text) - Convert.ToDouble(Text_Analysis_Xmin.Text) >= 0.005)
            //    CurvAnalysis.CurvShow.SetCurv_xUnit(CurvTimeUnit_ComboBox.Text);
            //if (Convert.ToDouble(Text_Analysis_Ymax.Text) - Convert.ToDouble(Text_Analysis_Ymin.Text) >= 0.005)
            //    CurvAnalysis.CurvShow.SetCurv_yUnit(CurvAUUnit_ComboBox.Text);
            //CurvAnalysis.CurvShow.SetCurv_Color(CurvWaveLength1Color_Set.BackColor, CurvWaveLength2Color_Set.BackColor, CurvWaveLength3Color_Set.BackColor);

            CurvTimeUnit_ComboBox.Text = CurvAnalysis.CurvShow.GetCurv_xUnit();
            CurvAUUnit_ComboBox.Text = CurvAnalysis.CurvShow.GetCurv_yUnit();
            CurvWaveLength1_TextBox.Text = CurvAnalysis.CurvShow.GetWaveLength1().ToString();
            CurvWaveLength2_TextBox.Text = CurvAnalysis.CurvShow.GetWaveLength2().ToString();
            CurvWaveLength3_TextBox.Text = CurvAnalysis.CurvShow.GetWaveLength3().ToString();
            CurvWaveLength1Color_Set.BackColor = CurvAnalysis.CurvShow.GetCurv0_Color();
            CurvWaveLength2Color_Set.BackColor = CurvAnalysis.CurvShow.GetCurv1_Color();
            CurvWaveLength3Color_Set.BackColor = CurvAnalysis.CurvShow.GetCurv2_Color();

            if (!UVSpectrum_Start.Enabled && UVSpectrum_Pause.Enabled && UVSpectrum_Stop.Enabled)
            {
                UVValue_Main.Text = "波长:" + UVDetector.CurvShow.GetWaveLength1().ToString() + " " + string.Format("{0:0.000}", UVDetector.CurvShow.GetUVValue(UVDetector.CurvShow.GetCurv_yUnit(), 0)) + UVDetector.CurvShow.GetCurv_yUnit();
                if (UVDetector.CurvShow.GetWaveLengthCnt() > 1)
                    UVValue_Main.Text = UVValue_Main.Text + "    波长:" + UVDetector.CurvShow.GetWaveLength2().ToString() + " " + string.Format("{0:0.000}", UVDetector.CurvShow.GetUVValue(UVDetector.CurvShow.GetCurv_yUnit(), 1)) + UVDetector.CurvShow.GetCurv_yUnit();
                if (UVDetector.CurvShow.GetWaveLengthCnt() > 2)
                    UVValue_Main.Text = UVValue_Main.Text + "    波长:" + UVDetector.CurvShow.GetWaveLength3().ToString() + " " + string.Format("{0:0.000}", UVDetector.CurvShow.GetUVValue(UVDetector.CurvShow.GetCurv_yUnit(), 2)) + UVDetector.CurvShow.GetCurv_yUnit();
                UVTime_Main.Text = string.Format("{0:0.00}", UVDetector.CurvShow.GetUVTime(UVDetector.CurvShow.GetCurv_xUnit())) + UVDetector.CurvShow.GetCurv_xUnit();
                if (UVDetector.CurvShow.GetUVTime(UVDetector.CurvShow.GetCurv_xUnit()) >= Convert.ToDouble(UVMaxTime_TextBox.Text))
                    UVSpectrum_Stop_Click(null, null);
            }
            else {
                UVValue_Main.Text = "";
                UVTime_Main.Text = "";
            }

            if (!xMaxEnter_UVSpectrum)
            {
                Text_UVSpectrum_Xmax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMax(), 3)).ToString();
                Text_UVSpectrum_Xmax.Invalidate();
            }
            if (!xMinEnter_UVSpectrum)
            {
                Text_UVSpectrum_Xmin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMin(), 3)).ToString();
                Text_UVSpectrum_Xmin.Invalidate();
            }
            if (!yMaxEnter_UVSpectrum)
            {
                Text_UVSpectrum_Ymax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMax(), 3)).ToString();
                Text_UVSpectrum_Ymax.Invalidate();
            }
            if (!yMinEnter_UVSpectrum)
            {
                Text_UVSpectrum_Ymin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMin(), 3)).ToString();
                Text_UVSpectrum_Ymin.Invalidate();
            }

            if (!xMaxEnter_Analysis)
            {
                Text_Analysis_Xmax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMax(), 3)).ToString();
                Text_Analysis_Xmax.Invalidate();
            }
            if (!xMinEnter_Analysis)
            {
                Text_Analysis_Xmin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMin(), 3)).ToString();
                Text_Analysis_Xmin.Invalidate();
            }
            if (!yMaxEnter_Analysis)
            {
                Text_Analysis_Ymax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMax(), 3)).ToString();
                Text_Analysis_Ymax.Invalidate();
            }
            if (!yMinEnter_Analysis)
            {
                Text_Analysis_Ymin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMin(), 3)).ToString();
                Text_Analysis_Ymin.Invalidate();
            }
        }

        private void UVProgram_Download_Click(object sender, EventArgs e)
        {
            if (!UVSpectrum_Start.Enabled)
                MessageBox.Show("正在采集图谱，不可下载紫外检测器程序！", "紫外检测器控制", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            else
            {
                DialogResult ret = MessageBox.Show("是否下载紫外检测器程序?", "紫外检测器控制", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        if (FrmLeft.uvInstance == null || FrmLeft.uvInstance.t_SerialPortCommu.uvPort == null || !FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen) return;

                        FrmLeft.uvInstance.SetUVWaveLength(UVWaveLength1_TextBox.Text, UVWaveLength2_TextBox.Text, UVWaveLength3_TextBox.Text);
                        FrmLeft.uvInstance.SetUVVPS(UVVPS_ComboBox.SelectedIndex);

                        FrmLeft.uvInstance.InitialUV();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }
        private void OpenUVLamp_Click(object sender, EventArgs e)
        {
            if (OpenUVLamp.Checked)
            {
                OpenUVLamp.Image = Image.FromFile("Icon//LampON.png");
                if (UVDetector.UV.t_UVPara.ISLampDeuteriumOpen) return;
                UVLampOpening uvLamp = new UVLampOpening();
                uvLamp.ShowDialog();
                OpenUVLamp.Text = "关灯";
                OpenUVLamp.ToolTipText = "关灯";
            } else
            {
                OpenUVLamp.Image = Image.FromFile("Icon//LampOFF.png");
                if (FrmLeft.uvInstance.UVLampClose())
                    UVDetector.UV.t_UVPara.ISLampDeuteriumOpen = false;
                OpenUVLamp.Text = "开灯";
                OpenUVLamp.ToolTipText = "开灯";
            }
        }
        private void SetUVCurvZero_Click(object sender, EventArgs e)
        {
            FrmLeft.uvInstance.ZeroUV();
        }

        private void UVTimeUnit_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UVTimeUnit_ComboBox.Text == "min" && UVMaxTimeUnit_Label.Text == "h")
            {
                UVMaxTime_TextBox.Text = (Convert.ToDouble(UVMaxTime_TextBox.Text) * 60).ToString();
            }
            else if (UVTimeUnit_ComboBox.Text == "h" && UVMaxTimeUnit_Label.Text == "min")
            {
                UVMaxTime_TextBox.Text = (Convert.ToDouble(UVMaxTime_TextBox.Text) / 60).ToString();
            }
            UVMaxTimeUnit_Label.Text = UVTimeUnit_ComboBox.Text;
        }

        private void UVCurv0Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            UVWaveLength1Color_Set.BackColor = curvColor.Color;
        }
        private void UVCurv1Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            UVWaveLength2Color_Set.BackColor = curvColor.Color;
        }
        private void UVCurv2Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            UVWaveLength3Color_Set.BackColor = curvColor.Color;
        }

        private void CurvWaveLength1Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            CurvWaveLength1Color_Set.BackColor = curvColor.Color;
            CurvAnalysis.CurvShow.SetCurv_Color(CurvWaveLength1Color_Set.BackColor, CurvWaveLength2Color_Set.BackColor, CurvWaveLength3Color_Set.BackColor);
        }
        private void CurvWaveLength2Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            CurvWaveLength2Color_Set.BackColor = curvColor.Color;
            CurvAnalysis.CurvShow.SetCurv_Color(CurvWaveLength1Color_Set.BackColor, CurvWaveLength2Color_Set.BackColor, CurvWaveLength3Color_Set.BackColor);
        }
        private void CurvWaveLength3Color_Set_Click(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            CurvWaveLength3Color_Set.BackColor = curvColor.Color;
            CurvAnalysis.CurvShow.SetCurv_Color(CurvWaveLength1Color_Set.BackColor, CurvWaveLength2Color_Set.BackColor, CurvWaveLength3Color_Set.BackColor);
        }

        private void CurvAUUnit_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurvAnalysis.CurvShow.SetCurv_yUnit(CurvAUUnit_ComboBox.Text);
        }
        private void CurvTimeUnit_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurvAnalysis.CurvShow.SetCurv_xUnit(CurvTimeUnit_ComboBox.Text);
        }
        /*********************************UVSpectrum***********************************/
        private void Set_UVSpectrum_Xmax_Click(object sender, EventArgs e)
        {
            UVDetector.CurvShow.SetCurvXtoMax();
            Text_UVSpectrum_Xmax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_UVSpectrum_Xmin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_UVSpectrum_Xmax.Invalidate();
            Text_UVSpectrum_Xmin.Invalidate();
        }

        private void Set_UVSpectrum_Ymax_Click(object sender, EventArgs e)
        {
            UVDetector.CurvShow.SetCurvYtoMax();
            Text_UVSpectrum_Ymax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_UVSpectrum_Ymin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_UVSpectrum_Ymax.Invalidate();
            Text_UVSpectrum_Ymin.Invalidate();
        }
        private void Increase_UVSpectrum_X_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - Convert.ToDouble(Text_UVSpectrum_Xmin.Text)) * 0.1;
            UVDetector.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_UVSpectrum_Xmax.Text) + delta);
            UVDetector.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_UVSpectrum_Xmin.Text) - delta);
            Text_UVSpectrum_Xmax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_UVSpectrum_Xmin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_UVSpectrum_Xmax.Invalidate();
            Text_UVSpectrum_Xmin.Invalidate();
        }
        private void Decrease_UVSpectrum_X_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - Convert.ToDouble(Text_UVSpectrum_Xmin.Text)) * 0.1;
            if (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - delta - (Convert.ToDouble(Text_UVSpectrum_Xmin.Text) + delta) >= 0.005)
            {
                UVDetector.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - delta);
                UVDetector.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_UVSpectrum_Xmin.Text) + delta);
            }
            Text_UVSpectrum_Xmax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_UVSpectrum_Xmin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_UVSpectrum_Xmax.Invalidate();
            Text_UVSpectrum_Xmin.Invalidate();
        }
        private void Increase_UVSpectrum_Y_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text)) * 0.1;
            UVDetector.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_UVSpectrum_Ymax.Text) + delta);
            UVDetector.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_UVSpectrum_Ymin.Text) - delta);
            Text_UVSpectrum_Ymax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_UVSpectrum_Ymin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_UVSpectrum_Ymax.Invalidate();
            Text_UVSpectrum_Ymin.Invalidate();
        }
        private void Decrease_UVSpectrum_Y_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text)) * 0.1;
            if (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - delta - (Convert.ToDouble(Text_UVSpectrum_Ymin.Text) + delta) >= 0.005)
            {
                UVDetector.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - delta);
                UVDetector.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_UVSpectrum_Ymin.Text) + delta);
            }
            Text_UVSpectrum_Ymax.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_UVSpectrum_Ymin.Text = (Math.Round(UVDetector.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_UVSpectrum_Ymax.Invalidate();
            Text_UVSpectrum_Ymin.Invalidate();
        }
        private void UVSpectrum_Start_Click(object sender, EventArgs e)
        {
            //FrmLeft.uvInstance.StartUV();
            UVDetector.UV.UVStartCollect();
            UVSpectrum_Start.Enabled = false;
            UVSpectrum_Pause.Enabled = true;
            UVSpectrum_Pause.Checked = false;
            UVSpectrum_Stop.Enabled = true;

            UVProgram_Download.Enabled = false;
            UVVPS_ComboBox.Enabled = false;
            UVWaveLength1_TextBox.Enabled = false;
            UVWaveLength2_TextBox.Enabled = false;
            UVWaveLength3_TextBox.Enabled = false;

            ShowUVLog("启动图谱采集");
        }
        private void UVSpectrum_Pause_Click(object sender, EventArgs e)
        {
            if (UVSpectrum_Pause.Checked)
            {
                UVDetector.UV.UVPauseCollect(false);
                ShowUVLog("暂停图谱采集");
            }
            else {
                UVDetector.UV.UVPauseCollect(true);
                ShowUVLog("启动图谱采集");
            }
            UVSpectrum_Start.Enabled = false;
            //UVSpectrum_Pause.Checked = true;
            UVSpectrum_Stop.Enabled = true;

            UVProgram_Download.Enabled = false;
            UVVPS_ComboBox.Enabled = false;
            UVWaveLength1_TextBox.Enabled = false;
            UVWaveLength2_TextBox.Enabled = false;
            UVWaveLength3_TextBox.Enabled = false;
        }
        private void UVSpectrum_Stop_Click(object sender, EventArgs e)
        {
            //FrmLeft.uvInstance.StopUV();
            UVDetector.UV.UVStopCollect();
            UVSpectrum_Start.Enabled = true;
            UVSpectrum_Pause.Enabled = false;
            UVSpectrum_Pause.Checked = false;
            UVSpectrum_Stop.Enabled = false;
            ShowUVLog("停止图谱采集");
            UVFile_Saver.Filter = "Microsoft Access 数据库(*.mdb)|*.mdb";
            UVFile_Saver.FileName = CurvShow.GetUVFileName() + ".mdb";
            UVFile_Saver.RestoreDirectory = true;
            if (UVFile_Saver.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.Copy("Curv.mdb", UVFile_Saver.FileName,true);
                ShowUVLog("保存图谱：" + UVFile_Saver.FileName);
            }

            UVProgram_Download.Enabled = true;
            UVVPS_ComboBox.Enabled = true;
            UVWaveLength1_TextBox.Enabled = true;
            UVWaveLength2_TextBox.Enabled = true;
            UVWaveLength3_TextBox.Enabled = true;
        }

        private bool yMaxEnter_UVSpectrum = false;
        private void Text_UVSpectrum_Ymax_Enter(object sender, EventArgs e)
        {
            yMaxEnter_UVSpectrum = true;
        }
        private void Text_UVSpectrum_Ymax_Leave(object sender, EventArgs e)
        {
            yMaxEnter_UVSpectrum = false;
        }
        private void Text_UVSpectrum_Ymax_TextChanged(object sender, EventArgs e)
        {
            if (yMaxEnter_UVSpectrum)
            {
                if (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_UVSpectrum_Ymax.Text));
            }
        }

        private bool yMinEnter_UVSpectrum = false;
        private void Text_UVSpectrum_Ymin_Enter(object sender, EventArgs e)
        {
            yMinEnter_UVSpectrum = true;
        }
        private void Text_UVSpectrum_Ymin_Leave(object sender, EventArgs e)
        {
            yMinEnter_UVSpectrum = false;
        }
        private void Text_UVSpectrum_Ymin_TextChanged(object sender, EventArgs e)
        {
            if (yMinEnter_UVSpectrum)
            {
                if (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_UVSpectrum_Ymin.Text));
            }
        }

        private bool xMaxEnter_UVSpectrum = false;
        private void Text_UVSpectrum_Xmax_Enter(object sender, EventArgs e)
        {
            xMaxEnter_UVSpectrum = true;
        }
        private void Text_UVSpectrum_Xmax_Leave(object sender, EventArgs e)
        {
            xMaxEnter_UVSpectrum = false;
        }
        private void Text_UVSpectrum_Xmax_TextChanged(object sender, EventArgs e)
        {
            if (xMaxEnter_UVSpectrum)
            {
                if (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - Convert.ToDouble(Text_UVSpectrum_Xmin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_UVSpectrum_Xmax.Text));
            }
        }

        private bool xMinEnter_UVSpectrum = false;
        private void Text_UVSpectrum_Xmin_Enter(object sender, EventArgs e)
        {
            xMinEnter_UVSpectrum = true;
        }
        private void Text_UVSpectrum_Xmin_Leave(object sender, EventArgs e)
        {
            xMinEnter_UVSpectrum = false;
        }
        private void Text_UVSpectrum_Xmin_TextChanged(object sender, EventArgs e)
        {
            if (xMinEnter_UVSpectrum)
            {
                if (Convert.ToDouble(Text_UVSpectrum_Xmax.Text) - Convert.ToDouble(Text_UVSpectrum_Xmin.Text) >= 0.005)
                    UVDetector.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_UVSpectrum_Xmin.Text));
            }
        }

        /****************************************************************************/

        /*****************************Analysis***************************************/
        private void Set_Analysis_Xmax_Click(object sender, EventArgs e)
        {
            CurvAnalysis.CurvShow.SetCurvXtoMax();
            Text_Analysis_Xmax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_Analysis_Xmin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_Analysis_Xmax.Invalidate();
            Text_Analysis_Xmin.Invalidate();
        }

        private void Set_Analysis_Ymax_Click(object sender, EventArgs e)
        {
            CurvAnalysis.CurvShow.SetCurvYtoMax();
            Text_Analysis_Ymax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_Analysis_Ymin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_Analysis_Ymax.Invalidate();
            Text_Analysis_Ymin.Invalidate();
        }
        private void Increase_Analysis_X_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_Analysis_Xmax.Text) - Convert.ToDouble(Text_Analysis_Xmin.Text)) * 0.1;
            CurvAnalysis.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_Analysis_Xmax.Text) + delta);
            CurvAnalysis.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_Analysis_Xmin.Text) - delta);
            Text_Analysis_Xmax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_Analysis_Xmin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_Analysis_Xmax.Invalidate();
            Text_Analysis_Xmin.Invalidate();
        }
        private void Decrease_Analysis_X_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_Analysis_Xmax.Text) - Convert.ToDouble(Text_Analysis_Xmin.Text)) * 0.1;
            if (Convert.ToDouble(Text_Analysis_Xmax.Text) - delta - (Convert.ToDouble(Text_Analysis_Xmin.Text) + delta) >= 0.005)
            {
                CurvAnalysis.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_Analysis_Xmax.Text) - delta);
                CurvAnalysis.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_Analysis_Xmin.Text) + delta);
            }
            Text_Analysis_Xmax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMax(), 3)).ToString();
            Text_Analysis_Xmin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_xMin(), 3)).ToString();
            Text_Analysis_Xmax.Invalidate();
            Text_Analysis_Xmin.Invalidate();
        }
        private void Increase_Analysis_Y_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_Analysis_Ymax.Text) - Convert.ToDouble(Text_Analysis_Ymin.Text)) * 0.1;
            CurvAnalysis.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_Analysis_Ymax.Text) + delta);
            CurvAnalysis.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_Analysis_Ymin.Text) - delta);
            Text_Analysis_Ymax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_Analysis_Ymin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_Analysis_Ymax.Invalidate();
            Text_Analysis_Ymin.Invalidate();
        }
        private void Decrease_Analysis_Y_Click(object sender, EventArgs e)
        {
            double delta = (Convert.ToDouble(Text_Analysis_Ymax.Text) - Convert.ToDouble(Text_Analysis_Ymin.Text)) * 0.1;
            if (Convert.ToDouble(Text_Analysis_Ymax.Text) - delta - (Convert.ToDouble(Text_Analysis_Ymin.Text) + delta) >= 0.005)
            {
                CurvAnalysis.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_Analysis_Ymax.Text) - delta);
                CurvAnalysis.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_Analysis_Ymin.Text) + delta);
            }
            Text_Analysis_Ymax.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMax(), 3)).ToString();
            Text_Analysis_Ymin.Text = (Math.Round(CurvAnalysis.CurvShow.GetCurv_yMin(), 3)).ToString();
            Text_Analysis_Ymax.Invalidate();
            Text_Analysis_Ymin.Invalidate();
        }

        private bool yMaxEnter_Analysis = false;
        private void Text_Analysis_Ymax_Enter(object sender, EventArgs e)
        {
            yMaxEnter_Analysis = true;
        }
        private void Text_Analysis_Ymax_Leave(object sender, EventArgs e)
        {
            yMaxEnter_Analysis = false;
        }
        private void Text_Analysis_Ymax_TextChanged(object sender, EventArgs e)
        {
            if (yMaxEnter_Analysis)
            {
                if (Convert.ToDouble(Text_Analysis_Ymax.Text) - Convert.ToDouble(Text_Analysis_Ymin.Text) >= 0.005)
                    CurvAnalysis.CurvShow.SetCurv_yMax(Convert.ToDouble(Text_Analysis_Ymax.Text));
            }
        }

        private bool yMinEnter_Analysis = false;
        private void Text_Analysis_Ymin_Enter(object sender, EventArgs e)
        {
            yMinEnter_Analysis = true;
        }
        private void Text_Analysis_Ymin_Leave(object sender, EventArgs e)
        {
            yMinEnter_Analysis = false;
        }
        private void Text_Analysis_Ymin_TextChanged(object sender, EventArgs e)
        {
            if (yMinEnter_Analysis)
            {
                if (Convert.ToDouble(Text_Analysis_Ymax.Text) - Convert.ToDouble(Text_Analysis_Ymin.Text) >= 0.005)
                    CurvAnalysis.CurvShow.SetCurv_yMin(Convert.ToDouble(Text_Analysis_Ymin.Text));
            }
        }

        private bool xMaxEnter_Analysis = false;
        private void Text_Analysis_Xmax_Enter(object sender, EventArgs e)
        {
            xMaxEnter_Analysis = true;
        }
        private void Text_Analysis_Xmax_Leave(object sender, EventArgs e)
        {
            xMaxEnter_Analysis = false;
        }
        private void Text_Analysis_Xmax_TextChanged(object sender, EventArgs e)
        {
            if (xMaxEnter_Analysis)
            {
                if (Convert.ToDouble(Text_Analysis_Xmax.Text) - Convert.ToDouble(Text_Analysis_Xmin.Text) >= 0.005)
                    CurvAnalysis.CurvShow.SetCurv_xMax(Convert.ToDouble(Text_Analysis_Xmax.Text));
            }
        }

        private bool xMinEnter_Analysis = false;
        private void Text_Analysis_Xmin_Enter(object sender, EventArgs e)
        {
            xMinEnter_Analysis = true;
        }
        private void Text_Analysis_Xmin_Leave(object sender, EventArgs e)
        {
            xMinEnter_Analysis = false;
        }
        private void Text_Analysis_Xmin_TextChanged(object sender, EventArgs e)
        {
            if (xMinEnter_Analysis)
            {
                if (Convert.ToDouble(Text_Analysis_Xmax.Text) - Convert.ToDouble(Text_Analysis_Xmin.Text) >= 0.005)
                    CurvAnalysis.CurvShow.SetCurv_xMin(Convert.ToDouble(Text_Analysis_Xmin.Text));
            }
        }

        public static string reportPath;
        private void OpenFile_Click(object sender, EventArgs e)
        {
            UVFile_Open.Filter = "Microsoft Access 数据库(*.mdb)|*.mdb";
            UVFile_Open.FileName = "";
            reportPath = "";
            UVFile_Open.RestoreDirectory = true;
            if (UVFile_Open.ShowDialog() == DialogResult.OK)
            {
                CurvAnalysis.UV.OpenCurvFile(UVFile_Open.FileName);
                UVAnalysisInfo_Fresher.Enabled = true;
                reportPath = UVFile_Open.FileName;
                OpenFileName.Text = System.IO.Path.GetFileNameWithoutExtension(reportPath);
                OpenFileName.Visible = true;
            }
        }


        /****************************************************************************/

        /****************************************************************************/
        public static string GetReportFileName()
        {            
            return System.IO.Path.GetFileNameWithoutExtension(reportPath);
        }
        public static DataTable GetReportDataTable()
        {
            return CurvAnalysis.UV.uvValue_DataTable;
        }
        public static double GetReportVPS()
        {
            return CurvAnalysis.UV.t_UVPara.vps;
        }        
        /****************************************************************************/
        private void ShowUVLog(string log)
        {
            if (log != null && log != "")
            {
                AppConfig.frmBottom.EquipmentLOG_Text.Invoke(new EventHandler(delegate
                {
                    AppConfig.frmBottom.EquipmentLOG_Text.Text = AppConfig.frmBottom.EquipmentLOG_Text.Text.Insert(0, Environment.NewLine);
                    AppConfig.frmBottom.EquipmentLOG_Text.Text = AppConfig.frmBottom.EquipmentLOG_Text.Text.Insert(0, log.ToString() + Environment.NewLine);
                    AppConfig.frmBottom.EquipmentLOG_Text.Text = AppConfig.frmBottom.EquipmentLOG_Text.Text.Insert(0, DateTime.Now.ToLongTimeString() + Environment.NewLine);
                }));
            }
        }

        public void Start_UVProgram()
        {
            if (UVSpectrum_Start.Enabled)
                UVSpectrum_Start_Click(null, null);
        }
        public void Pause_UVProgram()
        {
            if (UVSpectrum_Pause.Enabled)
            {
                if (UVSpectrum_Pause.Checked) UVSpectrum_Pause.Checked = false;
                else UVSpectrum_Pause.Checked = true;
                UVSpectrum_Pause_Click(null, null);
            }
        }
        public void Stop_UVProgram()
        {
            if (UVSpectrum_Stop.Enabled)
                UVSpectrum_Stop_Click(null, null);
        }

    }
}
