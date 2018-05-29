using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
//using Microsoft.Office.Interop.Word;

namespace BioChome
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            AppConfig.frmMain = this;
        }

        private string m_DockPath = string.Empty;
        private string m_DockPathDefault = string.Empty;

        public Register.Dog dog;
        private static bool ISDogOK = true;
        private static string dogNoStr = string.Empty;
        private void Register_Fresher_Tick(object sender, EventArgs e)
        {
            dog.DogAddr = 0;            // The address read
            dog.DogBytes = 50;          // The number of bytes read

            dog.ReadDog();

            if (dog.Retcode == 0)
            {
                byte[] chTemp = new byte[50];
                for (int i = 0; i < 50; i++)
                {
                    chTemp[i] = (byte)dog.DogData[i];
                }

                string str = Encoding.UTF8.GetString(chTemp);
                if (str.Contains(DogSerialNo.Text))
                {
                    ISDogOK = true;
                    dogNoStr = DogSerialNo.Text;
                }
                else if(ISDogOK)
                {
                    ISDogOK = false;
                    dogNoStr = string.Empty;
                    ShowSerialPortLog("发现非法的加密狗");
                }
            }
            else if (ISDogOK)
            {
                ISDogOK = false;
                dogNoStr = string.Empty;
                ShowSerialPortLog("加密狗错误");
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            dog = new Register.Dog(100);

            this.Dock_Main.DocumentStyle = DocumentStyle.DockingMdi;
            this.m_DockPath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "DockPanel.config");
            this.m_DockPathDefault = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "DockPanel_Default.config");

            this.Left = SystemInformation.WorkingArea.Left;
            this.Top = SystemInformation.WorkingArea.Top;
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //AppConfig.frmLeft = FrmLeft.GetInstance();
            //AppConfig.frmLeft.Show(this.dockPanel1, AppConfig.ms_FrmLeft);

            //AppConfig.frmRight = FrmRight.GetInstance();
            //AppConfig.frmRight.Show(this.dockPanel1, AppConfig.ms_FrmRight);

            //AppConfig.frmBottom = FrmBottom.GetInstance();
            //AppConfig.frmBottom.Show(this.dockPanel1, AppConfig.ms_FrmBottom);

            //Report.Report.SaveAsXLS(this.m_DockPath);
            this.InitDockPanel(this.m_DockPath);

        }

        private void InitDockPanel(string path)
        {
            try
            {
                //if (this.Dock_Main.ActiveContent == null)
                //{
                //    this.Dock_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
                //    this.Dock_Main.ActiveAutoHideContent = null;
                //    this.Dock_Main.BackColor = System.Drawing.SystemColors.Control;
                //    this.Dock_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //    this.Dock_Main.Dock = System.Windows.Forms.DockStyle.Fill;
                //    this.Dock_Main.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
                //    this.Dock_Main.Location = new System.Drawing.Point(0, 25);
                //    this.Dock_Main.Name = "Dock_Main";
                //    this.Dock_Main.Size = new System.Drawing.Size(1264, 857);
                //    this.Dock_Main.TabIndex = 1;
                //    this.Dock_Main.DocumentStyle = DocumentStyle.DockingMdi;
                //    this.Controls.Add(this.Dock_Main);
                //}
                //根据配置文件动态加载浮动窗体
                this.Dock_Main.LoadFromXml(path, delegate (string persistString)
                {
                    //功能窗体
                    if (persistString == typeof(FrmUV).ToString())
                    {
                        AppConfig.frmUV = FrmUV.GetInstance();
                        //AppConfig.frmUV.TabText = "UV Detector";
                        return AppConfig.frmUV;
                    }
                    else if (persistString == typeof(FrmPump).ToString())
                    {
                        AppConfig.frmPump = FrmPump.GetInstance();
                        //AppConfig.frmPump.TabText = "Pump";
                        return AppConfig.frmPump;
                    }
                    else if (persistString == typeof(FrmCollector).ToString())
                    {
                        AppConfig.frmCollector = FrmCollector.GetInstance();
                        //AppConfig.frmCollector.TabText = "Collector";
                        return AppConfig.frmCollector;
                    }
                    else if (persistString == typeof(FrmPH).ToString())
                    {
                        AppConfig.frmPH = FrmPH.GetInstance();
                        //AppConfig.frmPH.TabText = "PH";
                        return AppConfig.frmPH;
                    }
                    else if (persistString == typeof(FrmLeft).ToString())
                    {
                        AppConfig.frmLeft = FrmLeft.GetInstance();
                        AppConfig.frmLeft.Focus();
                        return AppConfig.frmLeft;
                    }
                    else if (persistString == typeof(FrmRight).ToString())
                    {
                        AppConfig.frmRight = FrmRight.GetInstance();
                        return AppConfig.frmRight;
                    }
                    else if (persistString == typeof(FrmBottom).ToString())
                    {
                        AppConfig.frmBottom = FrmBottom.GetInstance();
                        return AppConfig.frmBottom;
                    }   
                    //主框架之外的窗体不显示
                    return null;
                });
            }
            catch (Exception Err)
            {
                // 配置文件不存在或配置文件有问题时按系统默认规则加载子窗体 
                //FrmFunction.GetInstance().Show(this.dockPanel1);
                AppConfig.frmLeft = FrmLeft.GetInstance();
                AppConfig.frmLeft.Show(this.Dock_Main, AppConfig.ms_FrmLeft);

                AppConfig.frmRight = FrmRight.GetInstance();
                AppConfig.frmRight.Show(this.Dock_Main, AppConfig.ms_FrmRight);

                AppConfig.frmBottom = FrmBottom.GetInstance();
                AppConfig.frmBottom.Show(this.Dock_Main, AppConfig.ms_FrmBottom);
                MessageBox.Show(Err.Message);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (AppConfig.frmUV != null) { AppConfig.frmUV.IsHidden = false;}
                //if (AppConfig.frmPumpA != null) AppConfig.frmPumpA.IsHidden = false;
                //if (AppConfig.frmPumpB != null) AppConfig.frmPumpB.IsHidden = false;
                //if (AppConfig.frmCollector != null) AppConfig.frmCollector.IsHidden = false;
                //if (AppConfig.frmPH != null) AppConfig.frmPH.IsHidden = false;

                //AppConfig.frmUV.Dispose();
                //AppConfig.frmUV.Dispose();
                //AppConfig.frmUV.Dispose();
                //AppConfig.frmUV.Dispose();
                //为了下次打开程序时，浮动窗体的显示位置和关闭时一致，
                Dock_Main.SaveAsXml(this.m_DockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }
        }


        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //AppConfig.frmWnd[AppConfig.frmPointer] = FrmFunction.GetInstance();
            //AppConfig.frmWnd[AppConfig.frmPointer].TabText = (AppConfig.frmPointer).ToString();
            //AppConfig.frmWnd[AppConfig.frmPointer++].Show(this.dockPanel1, (AppConfig.ms_FrmFunction = DockState.Document));
            //switch (AppConfig.ms_FrmFunction)
            //{
            //    case DockState.DockTop:
            //        frmFun.Show(this.dockPanel1, (AppConfig.ms_FrmFunction = DockState.DockBottom));
            //        break;
            //}

            //this.StatusBar.Items[0].Text = frmFun.Text;
            //this.StatusBar.Text = "创建" + AppConfig.frmWnd[AppConfig.frmPointer-1].TabText;
        }
        
        
        public void NewUVDocument()
        {
            if (AppConfig.frmUV == null || AppConfig.frmUV.IsDisposed == true)
            {
                AppConfig.frmUV = FrmUV.GetInstance();
                //AppConfig.frmUV.TabText = "UV Detector";
                AppConfig.frmUV.Show(this.Dock_Main, (AppConfig.ms_FrmUV = DockState.Document));
            } else
            {
                AppConfig.frmUV.Close();
            }

        }
        public void NewPumpDocument()
        {
            if (AppConfig.frmPump == null || AppConfig.frmPump.IsDisposed == true)
            {
                AppConfig.frmPump = FrmPump.GetInstance();
                //AppConfig.frmPump.TabText = "Pump";
                AppConfig.frmPump.Show(this.Dock_Main, (AppConfig.ms_FrmFunction = DockState.Document));
            }
            else
            {
                AppConfig.frmPump.Close();
            }
        }
        public void NewCollectorDocument()
        {
            if (AppConfig.frmCollector == null || AppConfig.frmCollector.IsDisposed == true)
            {
                AppConfig.frmCollector = FrmCollector.GetInstance();
                //AppConfig.frmCollector.TabText = "Collector";
                AppConfig.frmCollector.Show(this.Dock_Main, (AppConfig.ms_FrmFunction = DockState.Document));
            }
            else
            {
                AppConfig.frmCollector.Close();
            }
        }
        public void NewPHDocument()
        {
            if (AppConfig.frmPH == null || AppConfig.frmPH.IsDisposed == true)
            {
                AppConfig.frmPH = FrmPH.GetInstance();
                //AppConfig.frmPH.TabText = "PH";
                AppConfig.frmPH.Show(this.Dock_Main, (AppConfig.ms_FrmFunction = DockState.Document));
            }
            else
            {
                AppConfig.frmPH.Close();
            }
        }


        private void reLoad_View_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppConfig.frmUV != null && AppConfig.frmUV.IsHandleCreated == true) AppConfig.frmUV.Close();
                if (AppConfig.frmPump != null && AppConfig.frmPump.IsHandleCreated == true) AppConfig.frmPump.Close();
                if (AppConfig.frmCollector != null && AppConfig.frmCollector.IsHandleCreated == true) AppConfig.frmCollector.Close();
                if (AppConfig.frmPH != null && AppConfig.frmPH.IsHandleCreated == true) AppConfig.frmPH.Close();
                if (AppConfig.frmLeft != null && AppConfig.frmLeft.IsHandleCreated == true) AppConfig.frmLeft.Close();
                if (AppConfig.frmBottom != null && AppConfig.frmBottom.IsHandleCreated == true) AppConfig.frmBottom.Close();
                if (AppConfig.frmRight != null && AppConfig.frmRight.IsHandleCreated == true) AppConfig.frmRight.Close();
                //AppConfig.frmUV = FrmUV.GetInstance();
                //AppConfig.frmPump = FrmPump.GetInstance();
                //AppConfig.frmCollector = FrmCollector.GetInstance();
                //AppConfig.frmPH = FrmPH.GetInstance();
                //AppConfig.frmLeft = FrmLeft.GetInstance();
                //AppConfig.frmBottom = FrmBottom.GetInstance();
                //AppConfig.frmRight = FrmRight.GetInstance();

                this.Dock_Main.Dispose();
                this.Dock_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
                this.Dock_Main.ActiveAutoHideContent = null;
                this.Dock_Main.BackColor = System.Drawing.SystemColors.Control;
                this.Dock_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //this.Dock_Main.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
                //this.Dock_Main.Location = new System.Drawing.Point(0, 25);
                this.Dock_Main.Name = "Dock_Main";
                //this.Dock_Main.Size = new System.Drawing.Size(1264, 857);
                //this.Dock_Main.TabIndex = 1;
                this.Dock_Main.DocumentStyle = DocumentStyle.DockingMdi;
                this.Controls.Add(this.Dock_Main);
                this.Dock_Main.BringToFront();
                this.Dock_Main.Dock = System.Windows.Forms.DockStyle.Fill;
                //根据配置文件动态加载浮动窗体
                this.InitDockPanel(this.m_DockPath);
            } catch
            {

            }
        }

        private void reSet_View_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppConfig.frmUV != null && AppConfig.frmUV.IsHandleCreated == true) AppConfig.frmUV.Close();
                if (AppConfig.frmPump != null && AppConfig.frmPump.IsHandleCreated == true) AppConfig.frmPump.Close();
                if (AppConfig.frmCollector != null && AppConfig.frmCollector.IsHandleCreated == true) AppConfig.frmCollector.Close();
                if (AppConfig.frmPH != null && AppConfig.frmPH.IsHandleCreated == true) AppConfig.frmPH.Close();
                if (AppConfig.frmLeft != null && AppConfig.frmLeft.IsHandleCreated == true) AppConfig.frmLeft.Close();
                if (AppConfig.frmBottom != null && AppConfig.frmBottom.IsHandleCreated == true) AppConfig.frmBottom.Close();
                if (AppConfig.frmRight != null && AppConfig.frmRight.IsHandleCreated == true) AppConfig.frmRight.Close();

                this.Dock_Main.Dispose();
                this.Dock_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
                this.Dock_Main.ActiveAutoHideContent = null;
                this.Dock_Main.BackColor = System.Drawing.SystemColors.Control;
                this.Dock_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //this.Dock_Main.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
                //this.Dock_Main.Location = new System.Drawing.Point(0, 25);
                this.Dock_Main.Name = "Dock_Main";
                //this.Dock_Main.Size = new System.Drawing.Size(1264, 857);
                //this.Dock_Main.TabIndex = 1;
                this.Dock_Main.DocumentStyle = DocumentStyle.DockingMdi;
                this.Controls.Add(this.Dock_Main);
                this.Dock_Main.BringToFront();
                this.Dock_Main.Dock = System.Windows.Forms.DockStyle.Fill;

                this.InitDockPanel(this.m_DockPathDefault);
                //AppConfig.frmLeft = FrmLeft.GetInstance();
                //AppConfig.frmLeft.Show(this.Dock_Main, AppConfig.ms_FrmLeft);

                //AppConfig.frmRight = FrmRight.GetInstance();
                //AppConfig.frmRight.Show(this.Dock_Main, AppConfig.ms_FrmRight);

                //AppConfig.frmBottom = FrmBottom.GetInstance();
                //AppConfig.frmBottom.Show(this.Dock_Main, AppConfig.ms_FrmBottom);
            }
            catch (Exception Err)
            {
                // 配置文件不存在或配置文件有问题时按系统默认规则加载子窗体 
                //FrmFunction.GetInstance().Show(this.dockPanel1);
                //AppConfig.frmLeft = FrmLeft.GetInstance();
                //AppConfig.frmLeft.Show(this.dockPanel1, AppConfig.ms_FrmLeft);

                //AppConfig.frmRight = FrmRight.GetInstance();
                //AppConfig.frmRight.Show(this.dockPanel1, AppConfig.ms_FrmRight);

                //AppConfig.frmBottom = FrmBottom.GetInstance();
                //AppConfig.frmBottom.Show(this.dockPanel1, AppConfig.ms_FrmBottom);
                MessageBox.Show(Err.Message);
            }

        }

        private void Report_View_DropDownOpening(object sender, EventArgs e)
        {
            if (FrmUV.reportPath == null || FrmUV.reportPath == "") Output_Report.Enabled = false;
            else Output_Report.Enabled = true;

        }
        private void View_Menu_DropDownOpening(object sender, EventArgs e)
        {
            if (AppConfig.frmLeft != null && AppConfig.frmLeft.IsHidden) EquipmentList_View.Checked = false;
            else EquipmentList_View.Checked = true;
            if (AppConfig.frmBottom != null && AppConfig.frmBottom.IsHidden) Log_View.Checked = false;
            else Log_View.Checked = true;
            if (AppConfig.frmRight != null && AppConfig.frmRight.IsHidden) QuickStart_View.Checked = false;
            else QuickStart_View.Checked = true;

            if ((FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                || (FrmLeft.phInstance != null && FrmLeft.phInstance.t_SerialPortCommu.phPort != null && FrmLeft.phInstance.t_SerialPortCommu.phPort.IsOpen)
                || (FrmLeft.pumpInstanceA != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceA.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceB != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceB.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceC != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceC.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.pumpInstanceD != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort != null && FrmLeft.pumpInstanceD.t_SerialPortCommu.pumpPort.IsOpen)
                || (FrmLeft.uvInstance != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort != null && FrmLeft.uvInstance.t_SerialPortCommu.uvPort.IsOpen))
            {
                reSet_View.Enabled = false;
                reLoad_View.Enabled = false;
            }
            else
            {
                reSet_View.Enabled = true;
                reLoad_View.Enabled = true;
            }
        }

        private void EquipmentList_View_Click(object sender, EventArgs e)
        {
            if (!EquipmentList_View.Checked)
                AppConfig.frmLeft.Hide();
            else
                AppConfig.frmLeft.Show();
        }

        private void Log_View_Click(object sender, EventArgs e)
        {
            if (!Log_View.Checked)
                AppConfig.frmBottom.Hide();
            else
                AppConfig.frmBottom.Show();
        }

        private void QuickStart_View_Click(object sender, EventArgs e)
        {
            if (!QuickStart_View.Checked)
                AppConfig.frmRight.Hide();
            else
                AppConfig.frmRight.Show();
        }

        private void Print_Report_Click(object sender, EventArgs e)
        {
            //PrintReportDialog.ShowDialog();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "Default.xls";
            p.StartInfo.Verb = "open";
            p.Start();
        }

        private void PrintPreview_Report_Click(object sender, EventArgs e)
        {
            //PrintReportDocument.DocumentName = m_DockPath;
            PrintReportPreviewDialog.Document = PrintReportDocument;
            PrintReportPreviewDialog.ShowDialog();
        }

        private void PrintReportDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("abc", new System.Drawing.Font("宋体",12), System.Drawing.Brushes.Black, 10, 35);

        }

        private void Output_Report_Click(object sender, EventArgs e)
        {
            Report_Saver.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
            string reportPath = FrmUV.GetReportFileName();
            Report_Saver.InitialDirectory = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "Report");
            Report_Saver.FileName = reportPath;
            Report_Saver.RestoreDirectory = true;
            if (Report_Saver.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.Copy("Template.xls", Report_Saver.FileName, true);
            } else
            {
                return;
            }
            Report.Report.SaveAsXLS(Report_Saver.FileName, FrmUV.GetReportDataTable(), FrmUV.GetReportVPS());

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = Report_Saver.FileName;
            p.StartInfo.Verb = "open";
            p.Start();
        }

        private void Modify_Template_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "Template.xls";
            p.StartInfo.Verb = "open";
            p.Start();
        }


        public static bool ISRegisted()
        {
            return ISDogOK;
        }
        public static string RegisteTime()
        {
            if (dogNoStr != "")
                return dogNoStr.Split(' ')[4] + " "  + dogNoStr.Split(' ')[5];
            return "";
        }

        /*******************************************************************/
        private void ShowSerialPortLog(string log)
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

    }
}
