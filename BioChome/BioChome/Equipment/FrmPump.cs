using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Pump;
using System.Xml;

namespace BioChome
{
    public partial class FrmPump : DockContent
    {
        public FrmPump()
        {            
            InitializeComponent();
        }

        //private FrmPump Instance;

        public static FrmPump GetInstance()
        {
            //Instance = new FrmPump();
            return new FrmPump();
        }

        private void FrmPump_Load(object sender, EventArgs e)
        {
            CreatePumpXmlFile(PumpMethodXMLPath);
            foreach (XmlNode node in ReadPumpXmlFile(PumpMethodXMLPath)[1])
            {
                if (node.Name == "MethodConfig") continue;
                PumpMethod_Browser.Items.Add(node.Name);
            }
            ReadPumpMethod(PumpMethodXMLPath, (PumpMethod_Browser.SelectedIndex = ReadPumpMethodConfig(PumpMethodXMLPath)));
            GradProgram_Grid_CellValueChanged(null, null);

        }

        private void FrmPump_DockStateChanged(object sender, EventArgs e)
        {
            //关闭时（dockstate为unknown） 不把dockstate保存 
            
            //if (this != null)
            //{
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.ms_FrmFunction = this.DockState;
            //}

        }

        private void FrmPump_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isPumpRunGrad || isPumpRunConstant)
            {
                DialogResult ret = MessageBox.Show("泵程序正在运行，是否立即停止？", "泵", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        if (!isPumpPauseGrad)
                            PauseGradProgram_Click(null, null);
                        if (!isPumpPauseConstant)
                            PauseConstantProgram_Click(null, null);
                        StopGradProgram_Click(null, null);
                        StopConstantProgram_Click(null, null);
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                }
            }


            SavePumpMethodConfig(PumpMethodXMLPath);

            PumpPressureShow.PressureThreadDispose();

            if (FrmLeft.pumpInstanceA != null)
                FrmLeft.pumpInstanceA.PumpInstanceDispose();
            if (FrmLeft.pumpInstanceB != null)
                FrmLeft.pumpInstanceB.PumpInstanceDispose();
            if (FrmLeft.pumpInstanceC != null)
                FrmLeft.pumpInstanceC.PumpInstanceDispose();
            if (FrmLeft.pumpInstanceD != null)
                FrmLeft.pumpInstanceD.PumpInstanceDispose();

            //this.Hide();
            //e.Cancel = true;
            //Instance = null;  // 否则下次打开时报错，提示“无法访问已释放对象”
        }

        private void GradProgram_Add_Click(object sender, EventArgs e)
        {
            GradProgram_Grid.Rows.Add();
        }

        private void GradProgram_Del_Click(object sender, EventArgs e)
        {
            if (GradProgram_Grid.RowCount > 0 && GradProgram_Grid.CurrentRow.Index >= 0)
                GradProgram_Grid.Rows.RemoveAt(GradProgram_Grid.CurrentRow.Index);
        }

        private void GradProgram_Insert_Click(object sender, EventArgs e)
        {
            if (GradProgram_Grid.RowCount > 0 && GradProgram_Grid.CurrentRow.Index >= 0)
                GradProgram_Grid.Rows.Insert(GradProgram_Grid.CurrentRow.Index, new DataGridViewRow());
        }

        private void GradProgram_Grid_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            for (int i = 0; i < GradProgram_Grid.RowCount; ++i)
                GradProgram_Grid.Rows[i].HeaderCell.Value = (i+1).ToString();
            GradProgram_Grid.Refresh();
        }

        private bool gradReadyStop;
        public static double TotalFlow;
        private TimeSpan pumpRunTime;
        private void PumpInfo_Fresher_Tick(object sender, EventArgs e)
        {

            if ((isPumpRunGrad || isPumpRunConstant) && (!isPumpPauseConstant && !isPumpPauseGrad))
                pumpRunTime = pumpRunTime.Add(new TimeSpan(0,0,0,0,PumpInfo_Fresher.Interval));


            try
            {
                if ((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                    || (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                    || (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                    || (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen))
                {
                    if (!StartConstantProgram.Enabled && !PauseConstantProgram.Enabled && !StopConstantProgram.Enabled
                        && !StartGradProgram.Enabled && !PauseGradProgram.Enabled && !StopGradProgram.Enabled)
                    {
                        if (PauseConstantProgram.Checked)
                        {
                            StartConstantProgram.Enabled = false;
                            PauseConstantProgram.Enabled = true;
                            StopConstantProgram.Enabled = true;
                        }
                        else if (PauseGradProgram.Checked)
                        {
                            StartGradProgram.Enabled = false;
                            PauseGradProgram.Enabled = true;
                            StopGradProgram.Enabled = true;
                        }
                        else
                        {
                            StartGradProgram.Enabled = true;
                            PumpSet_Download.Enabled = true;
                            StartConstantProgram.Enabled = true;
                        }
                    }

                    if ((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                        && !PumpNo_ComBox.Items.Contains("PumpA"))
                        PumpNo_ComBox.Items.Add("PumpA");
                    if ((FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                        && !PumpNo_ComBox.Items.Contains("PumpB"))
                        PumpNo_ComBox.Items.Add("PumpB");
                    if ((FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                        && !PumpNo_ComBox.Items.Contains("PumpC"))
                        PumpNo_ComBox.Items.Add("PumpC");
                    if ((FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                        && !PumpNo_ComBox.Items.Contains("PumpD"))
                        PumpNo_ComBox.Items.Add("PumpD");
                    if (PumpNo_ComBox.SelectedIndex == -1) PumpNo_ComBox.SelectedIndex = 0;

                    PumpPressureShow.SetCurv_yMax(Convert.ToDouble(PumpMaxPressure_TextBox.Text));
                    switch (PumpNo_ComBox.Text)
                    {
                        case "PumpA": PumpPressureShow.SetPressureVal(FrmLeft.pumpInstanceA.t_PumpInfo.pressure); break;
                        case "PumpB": PumpPressureShow.SetPressureVal(FrmLeft.pumpInstanceB.t_PumpInfo.pressure); break;
                        case "PumpC": PumpPressureShow.SetPressureVal(FrmLeft.pumpInstanceC.t_PumpInfo.pressure); break;
                        case "PumpD": PumpPressureShow.SetPressureVal(FrmLeft.pumpInstanceD.t_PumpInfo.pressure); break;
                    }

                }
                else
                {
                    if (!StartConstantProgram.Enabled && PauseConstantProgram.Enabled && StopConstantProgram.Enabled && !PauseConstantProgram.Checked)
                        Pause_ConstantProgram();
                    if (!StartGradProgram.Enabled && PauseGradProgram.Enabled && StopGradProgram.Enabled && !PauseGradProgram.Checked)
                        Pause_GradProgram();

                    StartGradProgram.Enabled = false;
                    PumpSet_Download.Enabled = false;
                    PauseGradProgram.Enabled = false;
                    StopGradProgram.Enabled = false;
                    StartConstantProgram.Enabled = false;
                    PauseConstantProgram.Enabled = false;
                    StopConstantProgram.Enabled = false;
                }
                if (FrmLeft.pumpInstanceA != null && (FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort == null || !FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                    && PumpNo_ComBox.Items.Contains("PumpA"))
                    PumpNo_ComBox.Items.Remove("PumpA");
                if (FrmLeft.pumpInstanceB != null && (FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort == null || !FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                    && PumpNo_ComBox.Items.Contains("PumpB"))
                    PumpNo_ComBox.Items.Remove("PumpB");
                if (FrmLeft.pumpInstanceC != null && (FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort == null || !FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                    && PumpNo_ComBox.Items.Contains("PumpC"))
                    PumpNo_ComBox.Items.Remove("PumpC");
                if (FrmLeft.pumpInstanceD != null && (FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort == null || !FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                    && PumpNo_ComBox.Items.Contains("PumpD"))
                    PumpNo_ComBox.Items.Remove("PumpD");
                if (PumpNo_ComBox.Items.Count == 0) { PumpNo_ComBox.Text = ""; PumpNo_ComBox.SelectedIndex = -1; PumpPressureShow.ClearPressureVal(); }



                if (!FrmMain.ISRegisted())
                {
                    StartGradProgram.Enabled = false;
                    StartConstantProgram.Enabled = false;
                }

                if (!isPumpPauseGrad)
                {
                    if (isPumpRunGrad)
                    {
                        if (GradProgram_Label.BackColor == Color.LightGreen) GradProgram_Label.BackColor = Color.FromName("Control");
                        else GradProgram_Label.BackColor = Color.LightGreen;
                        PumpStatus_StatusLabel.Text = "梯度程序运行中...";
                        //if (((TimeSpan)(DateTime.Now - gradStartTime)).TotalSeconds <= Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60)
                        //    PumpProgram_Progress.Value = Convert.ToInt32(((TimeSpan)(DateTime.Now - gradStartTime)).TotalSeconds / (Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60) * 100);
                        if (pumpRunTime.TotalSeconds <= Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60)
                            PumpProgram_Progress.Value = Convert.ToInt32(pumpRunTime.TotalSeconds / (Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60) * 100);

                        if (gradReadyStop)
                        {
                            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceA.StopPump();
                            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceB.StopPump();
                            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceC.StopPump();
                            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceD.StopPump();
                            isPumpRunGrad = false;
                            ShowPumpLog("停止梯度程序");
                            gradReadyStop = false;

                            StartGradProgram.Enabled = true;
                            PauseGradProgram.Enabled = false;
                            StopGradProgram.Enabled = false;
                            StartConstantProgram.Enabled = true;
                            PauseConstantProgram.Enabled = false;
                            StopConstantProgram.Enabled = false;
                            return;
                        }

                        double gradFlowA = 0, gradFlowB = 0, gradFlowC = 0, gradFlowD = 0;
                        //if (GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value != null
                        //    && ((TimeSpan)(DateTime.Now - gradStartTime)).TotalSeconds
                        //    > Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60
                        //    && !gradReadyStop)
                        if (GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value != null
                            && pumpRunTime.TotalSeconds > Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[0].Value) * 60
                            && !gradReadyStop)
                        {
                            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value != null && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[2].Value != null)
                                FrmLeft.pumpInstanceA.SetPumpGradFlow(gradFlowA = Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[2].Value) / 100);
                            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value != null && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[3].Value != null)
                                FrmLeft.pumpInstanceB.SetPumpGradFlow(gradFlowB = Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[3].Value) / 100);
                            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value != null && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[4].Value != null)
                                FrmLeft.pumpInstanceC.SetPumpGradFlow(gradFlowC = Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[4].Value) / 100);
                            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value != null && GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[5].Value != null)
                                FrmLeft.pumpInstanceD.SetPumpGradFlow(gradFlowD = Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[GradProgram_Grid.RowCount - 1].Cells[5].Value) / 100);
                            gradReadyStop = true;
                            PumpProgram_Progress.Value = 100;
                        }
                        else
                        {
                            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceA.SetPumpGradFlow(gradFlowA = GetCurrentGradFlow(0));
                            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceB.SetPumpGradFlow(gradFlowB = GetCurrentGradFlow(1));
                            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceC.SetPumpGradFlow(gradFlowC = GetCurrentGradFlow(2));
                            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceD.SetPumpGradFlow(gradFlowD = GetCurrentGradFlow(3));
                        }

                        PumpFlow_StatusLabel.Text = "泵流量   ";
                        if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen && gradFlowA >= 0)
                            PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   A %:" + string.Format("{0:0.000}", gradFlowA) + "ml/min";
                        if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen && gradFlowB >= 0)
                            PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   B %:" + string.Format("{0:0.000}", gradFlowB) + "ml/min";
                        if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen && gradFlowC >= 0)
                            PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   C %:" + string.Format("{0:0.000}", gradFlowC) + "ml/min";
                        if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen && gradFlowD >= 0)
                            PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   D %:" + string.Format("{0:0.000}", gradFlowD) + "ml/min";
                        TotalFlow = gradFlowA + gradFlowB + gradFlowC + gradFlowD;
                    }
                    else
                    {
                        GradProgram_Label.BackColor = Color.FromName("Control");
                        TotalFlow = 0;
                    }
                }

                if (!isPumpPauseConstant)
                {
                    if (isPumpRunConstant)
                    {
                        if (ConstantProgram_Label.BackColor == Color.LightGreen) ConstantProgram_Label.BackColor = Color.FromName("Control");
                        else ConstantProgram_Label.BackColor = Color.LightGreen;
                        PumpStatus_StatusLabel.Text = "恒流程序运行中...";
                        if (pumpRunTime.TotalMinutes <= Convert.ToDouble(ConstantTime_TextBox.Text))
                            PumpProgram_Progress.Value = Convert.ToInt32(pumpRunTime.TotalMinutes / Convert.ToDouble(ConstantTime_TextBox.Text) * 100);

                        if (ConstantTime_TextBox.Text != "" && ConstantPumpA_TextBox.Text != "" && ConstantPumpB_TextBox.Text != ""
                            && ConstantPumpC_TextBox.Text != "" && ConstantPumpD_TextBox.Text != ""
                            && ConstantFlow_TextBox.Text != "" && ConstantFlow_TextBox.Text != "0")
                        {
                            PumpFlow_StatusLabel.Text = "泵流量   ";
                            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                                PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   A %:" + string.Format("{0:0.000}", Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpA_TextBox.Text) / 100) + "ml/min";
                            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                                PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   B %:" + string.Format("{0:0.000}", Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpB_TextBox.Text) / 100) + "ml/min";
                            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                                PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   C %:" + string.Format("{0:0.000}", Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpC_TextBox.Text) / 100) + "ml/min";
                            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                                PumpFlow_StatusLabel.Text = PumpFlow_StatusLabel.Text + "   D %:" + string.Format("{0:0.000}", Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpD_TextBox.Text) / 100) + "ml/min";
                            TotalFlow = Convert.ToDouble(ConstantFlow_TextBox.Text);
                        }

                        if (ConstantTime_TextBox.Text != "" && pumpRunTime.TotalMinutes > Convert.ToDouble(ConstantTime_TextBox.Text))
                        {
                            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceA.StopPump();
                            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceB.StopPump();
                            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceC.StopPump();
                            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                                FrmLeft.pumpInstanceD.StopPump();
                            isPumpRunConstant = false;
                            ShowPumpLog("停止恒流程序");
                            PumpProgram_Progress.Value = 100;

                            StartGradProgram.Enabled = true;
                            PauseGradProgram.Enabled = false;
                            StopGradProgram.Enabled = false;
                            StartConstantProgram.Enabled = true;
                            PauseConstantProgram.Enabled = false;
                            StopConstantProgram.Enabled = false;
                        }
                    }
                    else
                    {
                        ConstantProgram_Label.BackColor = Color.FromName("Control");
                        TotalFlow = 0;
                    }
                }

                if (!isPumpPauseConstant && !isPumpPauseGrad)
                {
                    if (!isPumpRunConstant && !isPumpRunGrad)
                    {
                        PumpProgram_Progress.Visible = false;
                        PumpStatus_StatusLabel.Visible = false;
                        PumpFlow_StatusLabel.Visible = false;
                    }
                    else
                    {
                        PumpProgram_Progress.Visible = true;
                        PumpStatus_StatusLabel.Visible = true;
                        PumpFlow_StatusLabel.Visible = true;
                    }
                }

                //if (Convert.ToDouble(GradPrograme_Grid.Rows[GradPrograme_Grid.Rows.Count-1].Cells[0].Value) - Convert.ToDouble(GradPrograme_Grid.Rows[0].Cells[0].Value) >= 0.005)
                //    UVDetector.CurvShow.SetCurv_xUnit(UVTime_ComboBox.Text);
                //if (Convert.ToDouble(Text_UVSpectrum_Ymax.Text) - Convert.ToDouble(Text_UVSpectrum_Ymin.Text) >= 0.005)
                //    UVDetector.CurvShow.SetCurv_yUnit(UVAU_ComboBox.Text);
                //UVDetector.CurvShow.SetCurv_Color(UVCurvColor_Set.BackColor);
            } catch
            {
                return;
            }
        }

        private void GradProgram_Grid_MouseMove(object sender, MouseEventArgs e)
        {
            GradProgram_Grid.Focus();
        }

        private void GradProgram_Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GradProgram_Grid.RowCount == 0)
                {
                    Pump.GradCurvShow.SetGradCurv_DataTable(null);
                    return;
                }
                double xmax = Convert.ToDouble(GradProgram_Grid.Rows[0].Cells[0].Value);
                for (int i = 0; i < GradProgram_Grid.RowCount; ++i)
                {
                    if (GradProgram_Grid.Rows[i].Cells[0].Value != null && xmax < Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[0].Value))
                        xmax = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[0].Value);
                }
                double ymax = Convert.ToDouble(GradProgram_Grid.Rows[0].Cells[1].Value);
                for (int i = 0; i < GradProgram_Grid.RowCount; ++i)
                {
                    if (GradProgram_Grid.Rows[i].Cells[1].Value != null && ymax < Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value))
                        ymax = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value);
                }
                Pump.GradCurvShow.SetGradCurv_xMax(xmax);
                Pump.GradCurvShow.SetGradCurv_xMin(0);
                Pump.GradCurvShow.SetGradCurv_yMax(ymax);
                Pump.GradCurvShow.SetGradCurv_yMin(0);

                DataTable PumpFlowDataTable = new DataTable();
                PumpFlowDataTable.Columns.Add("Time", typeof(double));
                PumpFlowDataTable.Columns.Add("FlowA", typeof(double));
                PumpFlowDataTable.Columns.Add("FlowB", typeof(double));
                PumpFlowDataTable.Columns.Add("FlowC", typeof(double));
                PumpFlowDataTable.Columns.Add("FlowD", typeof(double));
                for (int i = 0; i < GradProgram_Grid.RowCount; ++i)
                {
                    DataRow drs = PumpFlowDataTable.NewRow();
                    drs["Time"] = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[0].Value);
                    if (GradProgram_Grid.Rows[i].Cells[1].Value != null && GradProgram_Grid.Rows[i].Cells[2].Value != null) drs["FlowA"] = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[2].Value) / 100;
                    if (GradProgram_Grid.Rows[i].Cells[1].Value != null && GradProgram_Grid.Rows[i].Cells[3].Value != null) drs["FlowB"] = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[3].Value) / 100;
                    if (GradProgram_Grid.Rows[i].Cells[1].Value != null && GradProgram_Grid.Rows[i].Cells[4].Value != null) drs["FlowC"] = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[4].Value) / 100;
                    if (GradProgram_Grid.Rows[i].Cells[1].Value != null && GradProgram_Grid.Rows[i].Cells[5].Value != null) drs["FlowD"] = Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[5].Value) / 100;
                    PumpFlowDataTable.Rows.Add(drs);
                }
                Pump.GradCurvShow.SetGradCurv_DataTable(PumpFlowDataTable);
                Pump.GradCurvShow.SetGradCurv_Color("A", PumpA_Color.BackColor);
                Pump.GradCurvShow.SetGradCurv_Color("B", PumpB_Color.BackColor);
                Pump.GradCurvShow.SetGradCurv_Color("C", PumpC_Color.BackColor);
                Pump.GradCurvShow.SetGradCurv_Color("D", PumpD_Color.BackColor);
                Pump.GradCurvShow.SetGradCurv_Visible("A", !PumpA_Color.Checked);
                Pump.GradCurvShow.SetGradCurv_Visible("B", !PumpB_Color.Checked);
                Pump.GradCurvShow.SetGradCurv_Visible("C", !PumpC_Color.Checked);
                Pump.GradCurvShow.SetGradCurv_Visible("D", !PumpD_Color.Checked);
            }
            catch
            {
                return;
            }
        }

        private void PumpNo_ComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpA")
            {
                PumpMaxPressure_TextBox.Text = FrmLeft.pumpInstanceA.t_PumpInfo.setMaxPressure.ToString();
                PumpMinPressure_TextBox.Text = FrmLeft.pumpInstanceA.t_PumpInfo.setMinPressure.ToString();
                //PumpPressureShow.ClearPressureVal();
            }
            if ((FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpB")
            {
                PumpMaxPressure_TextBox.Text = FrmLeft.pumpInstanceB.t_PumpInfo.setMaxPressure.ToString();
                PumpMinPressure_TextBox.Text = FrmLeft.pumpInstanceB.t_PumpInfo.setMinPressure.ToString();
                //PumpPressureShow.ClearPressureVal();
            }
            if ((FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpC")
            {
                PumpMaxPressure_TextBox.Text = FrmLeft.pumpInstanceC.t_PumpInfo.setMaxPressure.ToString();
                PumpMinPressure_TextBox.Text = FrmLeft.pumpInstanceC.t_PumpInfo.setMinPressure.ToString();
                //PumpPressureShow.ClearPressureVal();
            }
            if ((FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpD")
            {
                PumpMaxPressure_TextBox.Text = FrmLeft.pumpInstanceD.t_PumpInfo.setMaxPressure.ToString();
                PumpMinPressure_TextBox.Text = FrmLeft.pumpInstanceD.t_PumpInfo.setMinPressure.ToString();
                //PumpPressureShow.ClearPressureVal();
            }
        }

        private void PumpSet_Download_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(PumpMaxPressure_TextBox.Text) < Convert.ToDouble(PumpMinPressure_TextBox.Text))
            {
                MessageBox.Show("压力最小值必须小于最大值", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if ((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpA")
            {
                FrmLeft.pumpInstanceA.SetPumpMaxPressure(Convert.ToDouble(PumpMaxPressure_TextBox.Text));
                FrmLeft.pumpInstanceA.SetPumpMinPressure(Convert.ToDouble(PumpMinPressure_TextBox.Text));
            }
            if ((FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpB")
            {
                FrmLeft.pumpInstanceB.SetPumpMaxPressure(Convert.ToDouble(PumpMaxPressure_TextBox.Text));
                FrmLeft.pumpInstanceB.SetPumpMinPressure(Convert.ToDouble(PumpMinPressure_TextBox.Text));
            }
            if ((FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpC")
            {
                FrmLeft.pumpInstanceC.SetPumpMaxPressure(Convert.ToDouble(PumpMaxPressure_TextBox.Text));
                FrmLeft.pumpInstanceC.SetPumpMinPressure(Convert.ToDouble(PumpMinPressure_TextBox.Text));
            }
            if ((FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                && PumpNo_ComBox.Text == "PumpD")
            {
                FrmLeft.pumpInstanceD.SetPumpMaxPressure(Convert.ToDouble(PumpMaxPressure_TextBox.Text));
                FrmLeft.pumpInstanceD.SetPumpMinPressure(Convert.ToDouble(PumpMinPressure_TextBox.Text));
            }
        }

        private void PumpA_Color_Click(object sender, EventArgs e)
        {
            Pump.GradCurvShow.SetGradCurv_Visible("A", !PumpA_Color.Checked);
        }

        private void PumpA_Color_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            PumpA_Color.BackColor = curvColor.Color;
            Pump.GradCurvShow.SetGradCurv_Color("A", PumpA_Color.BackColor);
            Pump.GradCurvShow.SetGradCurv_Visible("A", !(PumpA_Color.Checked = false));
        }

        private void PumpB_Color_Click(object sender, EventArgs e)
        {
            Pump.GradCurvShow.SetGradCurv_Visible("B", !PumpB_Color.Checked);
        }

        private void PumpB_Color_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            PumpB_Color.BackColor = curvColor.Color;
            Pump.GradCurvShow.SetGradCurv_Color("B", PumpB_Color.BackColor);
            Pump.GradCurvShow.SetGradCurv_Visible("B", !(PumpB_Color.Checked = false));
        }

        private void PumpC_Color_Click(object sender, EventArgs e)
        {
            Pump.GradCurvShow.SetGradCurv_Visible("C", !PumpC_Color.Checked);
        }

        private void PumpC_Color_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            PumpC_Color.BackColor = curvColor.Color;
            Pump.GradCurvShow.SetGradCurv_Color("C", PumpC_Color.BackColor);
            Pump.GradCurvShow.SetGradCurv_Visible("C", !(PumpC_Color.Checked = false));
        }

        private void PumpD_Color_Click(object sender, EventArgs e)
        {
            Pump.GradCurvShow.SetGradCurv_Visible("D", !PumpD_Color.Checked);
        }

        private void PumpD_Color_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog curvColor = new ColorDialog();
            curvColor.ShowDialog();
            PumpD_Color.BackColor = curvColor.Color;
            Pump.GradCurvShow.SetGradCurv_Color("D", PumpD_Color.BackColor);
            Pump.GradCurvShow.SetGradCurv_Visible("D", !(PumpD_Color.Checked = false));
        }


        private bool isPumpRunGrad, isPumpPauseGrad;
        private bool runProgram = true;
        private void StartGradProgram_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            bool IsCommuOK = true;
            if (runProgram)
                ret = MessageBox.Show("是否运行梯度程序?", "泵", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            else
                ret = DialogResult.Yes;
            switch (ret)
            {
                case DialogResult.Yes:
                    /*****数据有效性检查*****/
                    foreach (DataGridViewRow drs in GradProgram_Grid.Rows)
                    {
                        if (drs.Cells[0].Value == null)
                        {
                            MessageBox.Show("梯度程序错误，请重新输入参数！\n时间不得为空", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        if (drs.Cells[1].Value == null)
                        {
                            drs.Cells[1].Value = 0;
                            MessageBox.Show("梯度程序错误，请重新输入参数！\n总流量不得为0", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        double a = 0, b = 0, c = 0, d = 0;
                        if (drs.Cells[2].Value != null) a = Convert.ToDouble(drs.Cells[2].Value);
                        if (drs.Cells[3].Value != null) b = Convert.ToDouble(drs.Cells[3].Value);
                        if (drs.Cells[4].Value != null) c = Convert.ToDouble(drs.Cells[4].Value);
                        if (drs.Cells[5].Value != null) d = Convert.ToDouble(drs.Cells[5].Value);
                        if (a + b + c + d > 100)
                        {
                            MessageBox.Show("梯度程序错误，请重新输入参数！\n各相流量比例总和不得大于100%", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        if (a + b + c + d == 0)
                        {
                            MessageBox.Show("梯度程序错误，请重新输入参数！\n各相流量比例总和不得为0", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    /*****数据有效性检查*****/
                    StartGradProgram.Enabled = false;
                    PauseGradProgram.Enabled = false;
                    StopGradProgram.Enabled = false;
                    StartConstantProgram.Enabled = false;
                    PauseConstantProgram.Enabled = false;
                    StopConstantProgram.Enabled = false;
                    pumpRunTime = new TimeSpan();

                    if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceA.SetPumpFlow(GetCurrentGradFlow(0));
                    if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceB.SetPumpFlow(GetCurrentGradFlow(1));
                    if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceC.SetPumpFlow(GetCurrentGradFlow(2));
                    if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceD.SetPumpFlow(GetCurrentGradFlow(3));

                    if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceA.StartPump();
                    if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceB.StartPump();
                    if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceC.StartPump();
                    if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceD.StartPump();

                    if (IsCommuOK)
                    {
                        isPumpRunGrad = true;
                        isPumpPauseGrad = false;
                        gradReadyStop = false;
                        pumpRunTime = new TimeSpan();
                        ShowPumpLog("启动梯度程序");
                        StartGradProgram.Enabled = false;
                        PauseGradProgram.Enabled = true;
                        StopGradProgram.Enabled = true;
                        StartConstantProgram.Enabled = false;
                        PauseConstantProgram.Enabled = false;
                        StopConstantProgram.Enabled = false;
                    }
                    else
                    {
                        ShowPumpLog("启动梯度程序失败");
                        StopGradProgram_Click(sender, e);
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }
        private DateTime pauseTime;
        private void PauseGradProgram_Click(object sender, EventArgs e)
        {
            StartGradProgram.Enabled = false;
            PauseGradProgram.Enabled = false;
            StopGradProgram.Enabled = false;
            StartConstantProgram.Enabled = false;
            PauseConstantProgram.Enabled = false;
            StopConstantProgram.Enabled = false;

            if (PauseGradProgram.Checked)
            {
                if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceA.StopPump();
                if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceB.StopPump();
                if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceC.StopPump();
                if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceD.StopPump();

                GradProgram_Label.BackColor = Color.LightGreen;
                isPumpPauseGrad = true;
                PumpStatus_StatusLabel.Text = "梯度程序已暂停...";
                pauseTime = DateTime.Now;
                ShowPumpLog("暂停梯度程序");
            } else
            {
                if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceA.StartPump();
                if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceB.StartPump();
                if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceC.StartPump();
                if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                    FrmLeft.pumpInstanceD.StartPump();

                isPumpPauseGrad = false;
                ShowPumpLog("启动梯度程序");
            }

            StartGradProgram.Enabled = false;
            PauseGradProgram.Enabled = true;
            StopGradProgram.Enabled = true;
            StartConstantProgram.Enabled = false;
            PauseConstantProgram.Enabled = false;
            StopConstantProgram.Enabled = false;
        }
        private void StopGradProgram_Click(object sender, EventArgs e)
        {
            //DialogResult ret;
            //if (runProgram)
            //    ret = MessageBox.Show("是否停止梯度程序?", "泵", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //else
            //    ret = DialogResult.Yes;
            //if (ret == DialogResult.Yes)
            //{
            StartGradProgram.Enabled = false;
            PauseGradProgram.Enabled = false;
            StopGradProgram.Enabled = false;
            PauseGradProgram.Checked = false;
            StartConstantProgram.Enabled = false;
            PauseConstantProgram.Enabled = false;
            StopConstantProgram.Enabled = false;
            PauseConstantProgram.Checked = false;
            if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                FrmLeft.pumpInstanceA.StopPump();
            if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                FrmLeft.pumpInstanceB.StopPump();
            if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                FrmLeft.pumpInstanceC.StopPump();
            if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                FrmLeft.pumpInstanceD.StopPump();
            isPumpRunGrad = false;
            ShowPumpLog("停止梯度程序");
            isPumpPauseGrad = false;
            gradReadyStop = false;

            StartGradProgram.Enabled = true;
            PauseGradProgram.Enabled = false;
            StopGradProgram.Enabled = false;
            PauseGradProgram.Checked = false;
            StartConstantProgram.Enabled = true;
            PauseConstantProgram.Enabled = false;
            StopConstantProgram.Enabled = false;
            PauseConstantProgram.Checked = false;
            //}
        }




        private bool isPumpRunConstant, isPumpPauseConstant;
        private void StartConstantProgram_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            bool IsCommuOK = true;

            try
            {
                if (runProgram)
                    ret = MessageBox.Show("是否运行恒流程序?", "泵", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                else
                    ret = DialogResult.Yes;
                switch (ret)
                {
                    case DialogResult.Yes:
                        /*****数据有效性检查*****/
                        if (ConstantTime_TextBox.Text == "") ConstantTime_TextBox.Text = "1";
                        if (ConstantPumpA_TextBox.Text == "") ConstantPumpA_TextBox.Text = "0";
                        if (ConstantPumpB_TextBox.Text == "") ConstantPumpB_TextBox.Text = "0";
                        if (ConstantPumpC_TextBox.Text == "") ConstantPumpC_TextBox.Text = "0";
                        if (ConstantPumpD_TextBox.Text == "") ConstantPumpD_TextBox.Text = "0";
                        if (ConstantFlow_TextBox.Text == "" || ConstantFlow_TextBox.Text == "0")
                        {
                            ConstantFlow_TextBox.Text = "0";
                            MessageBox.Show("恒流程序错误，请重新输入参数！\n总流量不得为0", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        if ((Convert.ToDouble(ConstantPumpA_TextBox.Text)
                            + Convert.ToDouble(ConstantPumpB_TextBox.Text)
                            + Convert.ToDouble(ConstantPumpC_TextBox.Text)
                            + Convert.ToDouble(ConstantPumpD_TextBox.Text)) > 100)
                        {
                            MessageBox.Show("恒流程序错误，请重新输入参数！\n各相流量比例总和不得大于100%", "泵", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        /*****数据有效性检查*****/
                        StartGradProgram.Enabled = false;
                        PauseGradProgram.Enabled = false;
                        StopGradProgram.Enabled = false;
                        StartConstantProgram.Enabled = false;
                        PauseConstantProgram.Enabled = false;
                        StopConstantProgram.Enabled = false;
                        pumpRunTime = new TimeSpan();

                        if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpA_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceA.SetPumpFlow(Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpA_TextBox.Text) / 100);
                        if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpB_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceB.SetPumpFlow(Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpB_TextBox.Text) / 100);
                        if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpC_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceC.SetPumpFlow(Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpC_TextBox.Text) / 100);
                        if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpD_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceD.SetPumpFlow(Convert.ToDouble(ConstantFlow_TextBox.Text) * Convert.ToDouble(ConstantPumpD_TextBox.Text) / 100);
                        if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpA_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceA.StartPump();
                        if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpB_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceB.StartPump();
                        if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpC_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceC.StartPump();
                        if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen && Convert.ToDouble(ConstantPumpD_TextBox.Text) != 0)
                            IsCommuOK = FrmLeft.pumpInstanceD.StartPump();

                        if (IsCommuOK)
                        {
                            isPumpRunConstant = true;
                            isPumpPauseConstant = false;
                            pumpRunTime = new TimeSpan();
                            ShowPumpLog("启动恒流程序");
                            StartGradProgram.Enabled = false;
                            PauseGradProgram.Enabled = false;
                            StopGradProgram.Enabled = false;
                            StartConstantProgram.Enabled = false;
                            PauseConstantProgram.Enabled = true;
                            StopConstantProgram.Enabled = true;
                        }
                        else
                        {
                            ShowPumpLog("启动恒流程序失败!");
                            StopConstantProgram_Click(sender, e);
                        }
                        break;
                    case DialogResult.No:
                        break;
                }
            } catch
            {
                return;
            }
        }
        private void PauseConstantProgram_Click(object sender, EventArgs e)
        {
            bool IsCommuOK = true;
            try
            {
                StartGradProgram.Enabled = false;
                PauseGradProgram.Enabled = false;
                StopGradProgram.Enabled = false;
                StartConstantProgram.Enabled = false;
                PauseConstantProgram.Enabled = false;
                StopConstantProgram.Enabled = false;

                if (PauseConstantProgram.Checked)
                {
                    if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceA.StopPump();
                    if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceB.StopPump();
                    if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceC.StopPump();
                    if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceD.StopPump();

                    if (IsCommuOK)
                    {
                        ConstantProgram_Label.BackColor = Color.LightGreen;
                        isPumpPauseConstant = true;
                        PumpStatus_StatusLabel.Text = "恒流程序已暂停...";
                        ShowPumpLog("暂停恒流程序");
                    } else
                    {

                    }
                }
                else
                {
                    if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceA.StartPump();
                    if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceB.StartPump();
                    if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceC.StartPump();
                    if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                        IsCommuOK = FrmLeft.pumpInstanceD.StartPump();

                    isPumpPauseConstant = false;
                    ShowPumpLog("启动恒流程序");

                    StartGradProgram.Enabled = false;
                    PauseGradProgram.Enabled = false;
                    StopGradProgram.Enabled = false;
                    StartConstantProgram.Enabled = false;
                    PauseConstantProgram.Enabled = true;
                    StopConstantProgram.Enabled = true;
                }
            }catch
            {
                return;
            }
        }
        private void StopConstantProgram_Click(object sender, EventArgs e)
        {
            bool IsCommuOK = true;
            //DialogResult ret;
            //if (runProgram)
            //    ret = MessageBox.Show("是否停止恒流程序?", "泵", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //else
            //    ret = DialogResult.Yes;
            //if (ret == DialogResult.Yes)
            //{
            try
            {
                StartGradProgram.Enabled = false;
                PauseGradProgram.Enabled = false;
                StopGradProgram.Enabled = false;
                PauseGradProgram.Checked = false;
                StartConstantProgram.Enabled = false;
                PauseConstantProgram.Enabled = false;
                StopConstantProgram.Enabled = false;
                PauseConstantProgram.Checked = false;

                if (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                    IsCommuOK = FrmLeft.pumpInstanceA.StopPump();
                if (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                    IsCommuOK = FrmLeft.pumpInstanceB.StopPump();
                if (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                    IsCommuOK = FrmLeft.pumpInstanceC.StopPump();
                if (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                    IsCommuOK = FrmLeft.pumpInstanceD.StopPump();
                isPumpRunConstant = false;
                isPumpPauseConstant = false;
                ShowPumpLog("停止恒流程序");

                StartGradProgram.Enabled = true;
                PauseGradProgram.Enabled = false;
                StopGradProgram.Enabled = false;
                PauseGradProgram.Checked = false;
                StartConstantProgram.Enabled = true;
                PauseConstantProgram.Enabled = false;
                StopConstantProgram.Enabled = false;
                PauseConstantProgram.Checked = false;
                //}
            }catch
            {
                return;
            }
        }


        /****************************************************************************/
        private double GetCurrentGradFlow(int channel)
        {
            for (int i = 1; i < GradProgram_Grid.Rows.Count; ++i)
            {
                //endPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[i]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                //endPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[i]["FlowA"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                //curv_pen.DrawLine(new Pen(GradCurvRuler.curvA_Color, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                //startPoint.X = endPoint.X;
                //startPoint.Y = endPoint.Y;
                if (GradProgram_Grid.Rows[i - 1].Cells[0].Value != null && GradProgram_Grid.Rows[i].Cells[0].Value != null
                    && GradProgram_Grid.Rows[i - 1].Cells[1].Value != null && GradProgram_Grid.Rows[i - 1].Cells[2 + channel].Value != null
                    && GradProgram_Grid.Rows[i].Cells[1].Value != null && GradProgram_Grid.Rows[i].Cells[2 + channel].Value != null
                    && pumpRunTime.TotalSeconds >= Convert.ToDouble(GradProgram_Grid.Rows[i-1].Cells[0].Value)*60 
                    && pumpRunTime.TotalSeconds < Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[0].Value)*60)
                {
                    return (pumpRunTime.TotalSeconds - Convert.ToDouble(GradProgram_Grid.Rows[i - 1].Cells[0].Value)*60)
                        / (Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[0].Value)*60 - Convert.ToDouble(GradProgram_Grid.Rows[i - 1].Cells[0].Value)*60)
                        * (Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i].Cells[2 + channel].Value)/100 - Convert.ToDouble(GradProgram_Grid.Rows[i - 1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i-1].Cells[2 + channel].Value)/100)
                        + (Convert.ToDouble(GradProgram_Grid.Rows[i-1].Cells[1].Value) * Convert.ToDouble(GradProgram_Grid.Rows[i-1].Cells[2 + channel].Value) / 100);
                }
            }
            return -1;
        }

        private void ShowPumpLog(string log)
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
        /****************************************************************************/
        public void Start_GradProgram()
        {
            if (StartGradProgram.Enabled)
            {
                runProgram = false;
                StartGradProgram_Click(null, null);
                runProgram = true;
            }
        }
        public void Start_ConstantProgram()
        {
            if (StartConstantProgram.Enabled)
            {
                runProgram = false;
                StartConstantProgram_Click(null, null);
                runProgram = true;
            }
        }
        public void Pause_GradProgram()
        {
            if (PauseGradProgram.Enabled)
            {
                if (PauseGradProgram.Checked) PauseGradProgram.Checked = false;
                else PauseGradProgram.Checked = true;
                PauseGradProgram_Click(null, null);
            }
        }
        public void Pause_ConstantProgram()
        {
            //if (FrmLeft.uvInstance != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen)

            if (PauseConstantProgram.Enabled)
            {
                if (PauseConstantProgram.Checked) PauseConstantProgram.Checked = false;
                else PauseConstantProgram.Checked = true;
                PauseConstantProgram_Click(null, null);
            }
        }

        public void Stop_GradProgram()
        {
            if (StopGradProgram.Enabled)
            {
                //runProgram = false;
                StopGradProgram_Click(null, null);
                //runProgram = true;
            }
        }
        public void Stop_ConstantProgram()
        {
            if (StopConstantProgram.Enabled)
            {
                //runProgram = false;
                StopConstantProgram_Click(null, null);
                //runProgram = true;
            }
        }

    }

}
