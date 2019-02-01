using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Xml;

namespace BioChome
{
    public partial class FrmRight : DockContent
    {
        private static FrmRight Instance;

        public FrmRight()
        {
            InitializeComponent();
        }

        public static FrmRight GetInstance()
        {
            Instance = new FrmRight();
            return Instance;
        }

        private void FrmRight_Load(object sender, EventArgs e)
        {
            this.DockPanel.DockRightPortion = 120;
            //this.EquipmentText_Information.ForeColor
            //this.EquipmentText_Information.Items[1] = "        Tauto";
            this.EquipmentText_Information.Rows.Add("公司");
            this.EquipmentText_Information.Rows.Add("");
            this.EquipmentText_Information.Rows.Add("软件名");
            this.EquipmentText_Information.Rows.Add("");
            this.EquipmentText_Information.Rows.Add("版本");
            this.EquipmentText_Information.Rows.Add("");
            this.EquipmentText_Information.Rows.Add("日期");
            this.EquipmentText_Information.Rows.Add("");
            this.EquipmentText_Information.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            this.EquipmentText_Information.Rows[2].DefaultCellStyle.BackColor = Color.LightGray;
            this.EquipmentText_Information.Rows[4].DefaultCellStyle.BackColor = Color.LightGray;
            this.EquipmentText_Information.Rows[6].DefaultCellStyle.BackColor = Color.LightGray;
            this.EquipmentText_Information.Rows[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.EquipmentText_Information.Rows[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.EquipmentText_Information.Rows[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.EquipmentText_Information.Rows[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.EquipmentText_Information.Rows[1].Cells[0].Value = "";
            this.EquipmentText_Information.Rows[3].Cells[0].Value = "BioChome";
            this.EquipmentText_Information.Rows[5].Cells[0].Value = "V2.0.0.2";
            this.EquipmentText_Information.Rows[7].Cells[0].Value = FrmMain.RegisteTime();
            this.EquipmentText_Information.Rows[0].Selected = false;
            this.EquipmentText_Information.ReadOnly = true;
            this.EquipmentText_Information.Height = 23 * 8;

            CreateMethodSnapShotXmlFile(MethodSnapShotXMLPath);
            foreach (XmlNode node in ReadMethodSnapShotXmlFile(MethodSnapShotXMLPath)[1])
            {
                if (node.Name == "MethodConfig") continue;
                Method_Browser.Items.Add(node.Name);
            }
            //ReadMethodSnapShotMethod(MethodSnapShotXMLPath, (Method_Browser.SelectedIndex = ReadMethodSnapShotMethodConfig(MethodSnapShotXMLPath)));

        }

        private void FrmRight_DockStateChanged(object sender, EventArgs e)
        {
            //关闭时（dockstate为unknown） 不把dockstate保存 
            if (Instance != null)
            {
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.ms_FrmRight = this.DockState;
            }
        }

        private void FrmRight_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AppConfig.frmRight = null;
            e.Cancel = true;
            this.Hide();
        }

        private void QuickStart_Click(object sender, EventArgs e)
        {
            QuickStart.Enabled = false;
            QuickPause.Enabled = false;
            QuickStop.Enabled = false;
            ProgramList_CheckBox.Enabled = false;

            if (!ProgramList_CheckBox.GetItemChecked(0) && !ProgramList_CheckBox.GetItemChecked(1) && !ProgramList_CheckBox.GetItemChecked(2) && !ProgramList_CheckBox.GetItemChecked(3)) return;
            if (ProgramList_CheckBox.GetItemChecked(1)) AppConfig.frmCollector.Start_CollectorProgram();
            if (ProgramList_CheckBox.GetItemChecked(2)) AppConfig.frmPump.Start_ConstantProgram();
            if (ProgramList_CheckBox.GetItemChecked(3)) AppConfig.frmPump.Start_GradProgram();
            if (ProgramList_CheckBox.GetItemChecked(0)) AppConfig.frmUV.Start_UVProgram();

            Thread.Sleep(200);
            QuickStart.Enabled = false;
            QuickPause.Enabled = true;
            QuickStop.Enabled = true;
            ProgramList_CheckBox.Enabled = false;
        }

        private void QuickPause_Click(object sender, EventArgs e)
        {
            QuickStart.Enabled = false;
            QuickPause.Enabled = false;
            QuickStop.Enabled = false;
            ProgramList_CheckBox.Enabled = false;

            if (ProgramList_CheckBox.GetItemChecked(0)) AppConfig.frmUV.Pause_UVProgram();
            if (ProgramList_CheckBox.GetItemChecked(2)) AppConfig.frmPump.Pause_ConstantProgram();
            if (ProgramList_CheckBox.GetItemChecked(3)) AppConfig.frmPump.Pause_GradProgram();

            Thread.Sleep(200);
            QuickStart.Enabled = false;
            QuickPause.Enabled = true;
            QuickStop.Enabled = true;
            ProgramList_CheckBox.Enabled = false;
        }

        private void QuickStop_Click(object sender, EventArgs e)
        {
            QuickStart.Enabled = false;
            QuickPause.Enabled = false;
            QuickStop.Enabled = false;
            ProgramList_CheckBox.Enabled = false;

            if (ProgramList_CheckBox.GetItemChecked(1)) AppConfig.frmCollector.Stop_CollectorProgram();
            if (ProgramList_CheckBox.GetItemChecked(2)) AppConfig.frmPump.Stop_ConstantProgram();
            if (ProgramList_CheckBox.GetItemChecked(3)) AppConfig.frmPump.Stop_GradProgram();
            if (ProgramList_CheckBox.GetItemChecked(0)) AppConfig.frmUV.Stop_UVProgram();

            Thread.Sleep(200);
            QuickStart.Enabled = true;
            QuickPause.Enabled = false;
            QuickStop.Enabled = false;
            ProgramList_CheckBox.Enabled = true;
        }

        private void ProgramList_CheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked) return;
            ((CheckedListBox)sender).SetItemChecked(2, false);
            ((CheckedListBox)sender).SetItemChecked(3, false);
            e.NewValue = CheckState.Checked;
        }

        private void ProgramList_CheckBox_MouseMove(object sender, MouseEventArgs e)
        {
            ProgramList_CheckBox.Focus();
        }

        private void EquipState_Fresher_Tick(object sender, EventArgs e)
        {
            if (!QuickStart.Enabled && QuickPause.Enabled && QuickStop.Enabled)
            {
                if ((ProgramList_CheckBox.GetItemChecked(0) && !(FrmLeft.uvInstance != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen))
                    || (ProgramList_CheckBox.GetItemChecked(1) && !(FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen))
                    || ((ProgramList_CheckBox.GetItemChecked(2) || ProgramList_CheckBox.GetItemChecked(3))
                        && !((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                            || (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                            || (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                            || (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)))
                    )
                {
                    QuickPause_Click(null, null);
                }

            }

            if (FrmLeft.uvInstance != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen)
                ProgramList_CheckBox.SetItemChecked(0, true);
            else {
                //if (!QuickStart.Enabled && QuickPause.Enabled && QuickStop.Enabled)
                //    QuickPause_Click(null, null);

                ProgramList_CheckBox.SetItemChecked(0, false);
            }


            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                ProgramList_CheckBox.SetItemChecked(1, true);
            else {
                //if (!QuickStart.Enabled && QuickPause.Enabled && QuickStop.Enabled)
                //    QuickPause_Click(null, null);

                ProgramList_CheckBox.SetItemChecked(1, false);
            }


            if ((FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen))
            {
                if (!ProgramList_CheckBox.GetItemChecked(2) && !ProgramList_CheckBox.GetItemChecked(3))
                {
                    ProgramList_CheckBox.SetItemChecked(2, false);
                    ProgramList_CheckBox.SetItemChecked(3, true);
                }
            } else
            {
                //if (!QuickStart.Enabled && QuickPause.Enabled && QuickStop.Enabled)
                //    QuickPause_Click(null, null);

                ProgramList_CheckBox.SetItemChecked(2, false);
                ProgramList_CheckBox.SetItemChecked(3, false);
            }


            if ((FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                || (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.uvInstance != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen))
            {
                if (!QuickStart.Enabled && !QuickPause.Enabled && !QuickStop.Enabled)
                {
                    QuickStart.Enabled = true;
                    ProgramList_CheckBox.Enabled = true;
                }
            }
            else
            {
                QuickStart.Enabled = false;
                ProgramList_CheckBox.Enabled = false;
            }



            if (this.EquipmentText_Information.RowCount > 0)
                this.EquipmentText_Information.Rows[7].Cells[0].Value = FrmMain.RegisteTime();

        }


        /****************************************************************************/

    }
}
