using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Collector;
using System.Threading;
using System.Xml;

namespace BioChome
{
    public partial class FrmCollector : DockContent
    {
        //private static FrmCollector Instance;
        private Button[] buttles;
        private bool[] buttleSelected;

        public FrmCollector()
        {            
            InitializeComponent();
            buttleSelected = new bool[120];
            buttles = new Button[120];
            int column = 25, row = 6;
            //Graphics len = CreateGraphics();
            //SizeF size = len.MeasureString("100", new Font("宋体", 8));
            int w = Buttles_Area.Width / column, h = Buttles_Area.Height / row;
            //for (int i = 0; i < row; ++i)
            //{
            //    for (int j = 0; j < column; ++j)
            //    {
            //buttles[j + i * column] = new System.Windows.Forms.Button();
            //buttles[j + i * column].Left = j * w;
            //buttles[j + i * column].Top = i * h;
            //buttles[j + i * column].Width = w;
            //buttles[j + i * column].Height = h;
            //buttles[j + i * column].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            //buttles[j + i * column].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
            //buttles[j + i * column].ForeColor = Color.MidnightBlue;
            //buttles[j + i * column].Text = (j + i * column + 1).ToString();
            //buttles[j + i * column].UseVisualStyleBackColor = true;
            //buttles[j + i * column].Click += new System.EventHandler(Buttles_Command);
            //buttles[j + i * column].Image = Image.FromFile("Icon//Empty.png");
            //Buttles_Area.Controls.Add(buttles[j + i * column]);
            //    }
            //}
            for (int i = 49; i <= 60; ++i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 48; i >= 37; --i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 25; i <= 36; ++i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 24; i >= 13; --i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 1; i <= 12; ++i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 109; i <= 120; ++i)
            {
                buttles[i-1] = new System.Windows.Forms.Button();
                buttles[i-1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i-1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i-1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i-1].ForeColor = Color.MidnightBlue;
                buttles[i-1].Text = i.ToString();
                buttles[i-1].UseVisualStyleBackColor = true;
                buttles[i-1].Click += new System.EventHandler(Buttles_Command);
                buttles[i-1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i-1]);
            }
            for (int i = 108; i >= 97; --i)
            {
                buttles[i - 1] = new System.Windows.Forms.Button();
                buttles[i - 1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i - 1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i - 1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i - 1].ForeColor = Color.MidnightBlue;
                buttles[i - 1].Text = i.ToString();
                buttles[i - 1].UseVisualStyleBackColor = true;
                buttles[i - 1].Click += new System.EventHandler(Buttles_Command);
                buttles[i - 1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i - 1]);
            }
            for (int i = 85; i <= 96; ++i)
            {
                buttles[i - 1] = new System.Windows.Forms.Button();
                buttles[i - 1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i - 1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i - 1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i - 1].ForeColor = Color.MidnightBlue;
                buttles[i - 1].Text = i.ToString();
                buttles[i - 1].UseVisualStyleBackColor = true;
                buttles[i - 1].Click += new System.EventHandler(Buttles_Command);
                buttles[i - 1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i - 1]);
            }
            for (int i = 84; i >= 73; --i)
            {
                buttles[i - 1] = new System.Windows.Forms.Button();
                buttles[i - 1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i - 1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i - 1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i - 1].ForeColor = Color.MidnightBlue;
                buttles[i - 1].Text = i.ToString();
                buttles[i - 1].UseVisualStyleBackColor = true;
                buttles[i - 1].Click += new System.EventHandler(Buttles_Command);
                buttles[i - 1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i - 1]);
            }
            for (int i = 61; i <= 72; ++i)
            {
                buttles[i - 1] = new System.Windows.Forms.Button();
                buttles[i - 1].TextAlign = ContentAlignment.MiddleCenter;
                buttles[i - 1].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                buttles[i - 1].Font = new System.Drawing.Font("宋体", 7, FontStyle.Bold);
                buttles[i - 1].ForeColor = Color.MidnightBlue;
                buttles[i - 1].Text = i.ToString();
                buttles[i - 1].UseVisualStyleBackColor = true;
                buttles[i - 1].Click += new System.EventHandler(Buttles_Command);
                buttles[i - 1].Image = Image.FromFile("Icon//Empty.png");
                Buttles_Area.Controls.Add(buttles[i - 1]);
            }

            //Buttles_Panel.Width = w * column;
            //buttles[0].Image = Image.FromFile("Icon//Buttle_A1B0.png");
            CurrentButtleNo_Label.Text = "当前瓶号：";
            HideButtlesPanel_Click(null, null);
        }

        private void FrmCollector_Resize(object sender, EventArgs e)
        {
            int column = 25, row = 5;
            int w = Buttles_Area.Width / column, h = Buttles_Area.Height / row;
            //for (int i = 0; i < row; ++i)
            //{
            //    for (int j = 0; j < column; ++j)
            //    {
            //        if (buttles[j + i * column] != null)
            //        {
            //            buttles[j + i * column].Left = j * w;
            //            buttles[j + i * column].Top = i * h;
            //            buttles[j + i * column].Width = w;
            //            buttles[j + i * column].Height = h;
            //        }
            //    }
            //}
            if (buttles == null) return;

            for (int i = 49; i <= 60; ++i)
            {
                if (buttles[i-1] == null) continue;
                buttles[i-1].Left = (i - 49) * w;
                buttles[i-1].Top = 0 * h;
                buttles[i-1].Width = w;
                buttles[i-1].Height = h;
            }
            for (int i = 48; i >= 37; --i)
            {
                if (buttles[i-1] == null) continue;
                buttles[i-1].Left = (48 - i) * w;
                buttles[i-1].Top = 1 * h;
                buttles[i-1].Width = w;
                buttles[i-1].Height = h;
            }
            for (int i = 25; i <= 36; ++i)
            {
                if (buttles[i-1] == null) continue;
                buttles[i-1].Left = (i - 25) * w;
                buttles[i-1].Top = 2 * h;
                buttles[i-1].Width = w;
                buttles[i-1].Height = h;
            }
            for (int i = 24; i >= 13; --i)
            {
                if (buttles[i-1] == null) continue;
                buttles[i-1].Left = (24 - i) * w;
                buttles[i-1].Top = 3 * h;
                buttles[i-1].Width = w;
                buttles[i-1].Height = h;
            }
            for (int i = 1; i <= 12; ++i)
            {
                if (buttles[i-1] == null) continue;
                buttles[i-1].Left = (i - 1) * w;
                buttles[i-1].Top = 4 * h;
                buttles[i-1].Width = w;
                buttles[i-1].Height = h;
            }
            for (int i = 109; i <= 120; ++i)
            {
                if (buttles[i - 1] == null) continue;
                buttles[i - 1].Left = (i - 109 + 12) * w + w / 2;
                buttles[i - 1].Top = 0 * h;
                buttles[i - 1].Width = w;
                buttles[i - 1].Height = h;
            }
            for (int i = 108; i >= 97; --i)
            {
                if (buttles[i - 1] == null) continue;
                buttles[i - 1].Left = (108 - i + 12) * w + w / 2;
                buttles[i - 1].Top = 1 * h;
                buttles[i - 1].Width = w;
                buttles[i - 1].Height = h;
            }
            for (int i = 85; i <= 96; ++i)
            {
                if (buttles[i - 1] == null) continue;
                buttles[i - 1].Left = (i - 85 + 12) * w + w / 2;
                buttles[i - 1].Top = 2 * h;
                buttles[i - 1].Width = w;
                buttles[i - 1].Height = h;
            }
            for (int i = 84; i >= 73; --i)
            {
                if (buttles[i - 1] == null) continue;
                buttles[i - 1].Left = (84 - i + 12) * w + w / 2;
                buttles[i - 1].Top = 3 * h;
                buttles[i - 1].Width = w;
                buttles[i - 1].Height = h;
            }
            for (int i = 61; i <= 72; ++i)
            {
                if (buttles[i - 1] == null) continue;
                buttles[i - 1].Left = (i - 61 + 12) * w + w / 2;
                buttles[i - 1].Top = 4 * h;
                buttles[i - 1].Width = w;
                buttles[i - 1].Height = h;
            }
        }

        public static FrmCollector GetInstance()
        {
            //if (Instance == null)
            //{
            //    Instance = new FrmCollector();
            //    Instance.TabText = "Collector";
            //}
            return new FrmCollector();
        }

        private void FrmCollector_DockStateChanged(object sender, EventArgs e)
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

        private void FrmCollector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!StartCollectorProgram.Enabled && StopCollectorProgram.Enabled)
            {
                DialogResult ret = MessageBox.Show("馏分收集器程序正在运行，是否立即停止？", "馏分收集器", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                switch (ret)
                {
                    case DialogResult.Yes:
                        StopCollectorProgram_Click(null, null);
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                }
            }

            SaveCollectorMethodConfig(CollectorMethodXMLPath);
            if (FrmLeft.collectorInstance != null)
                FrmLeft.collectorInstance.CollectorInstanceDispose();

            //Instance = null;  // 否则下次打开时报错，提示“无法访问已释放对象”
        }

        private void FrmCollector_Load(object sender, EventArgs e)
        {
            CreateCollectorXmlFile(CollectorMethodXMLPath);
            foreach (XmlNode node in ReadCollectorXmlFile(CollectorMethodXMLPath)[1])
            {
                if (node.Name == "MethodConfig") continue;
                CollectorMethod_Browser.Items.Add(node.Name);
            }
            ReadCollectorMethod(CollectorMethodXMLPath, (CollectorMethod_Browser.SelectedIndex = ReadCollectorMethodConfig(CollectorMethodXMLPath)));
            //CollectorProgram_Grid_CellValueChanged(null, null);

            CollectSet_Panel.ColumnStyles[1].Width = 0;
        }

        private void PipeLength_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (PipeLength_TextBox.Text == "" || PipeDiameter_TextBox.Text == "") return;
            DelayVolume.Text = string.Format("{0:0.00}", (Convert.ToDouble(PipeLength_TextBox.Text) * Convert.ToDouble(PipeDiameter_TextBox.Text) * Convert.ToDouble(PipeDiameter_TextBox.Text) * 3.1415926 / 1000));
        }
        private void PipeDiameter_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (PipeLength_TextBox.Text == "" || PipeDiameter_TextBox.Text == "") return;
            DelayVolume.Text = string.Format("{0:0.00}", (Convert.ToDouble(PipeLength_TextBox.Text) * Convert.ToDouble(PipeDiameter_TextBox.Text) * Convert.ToDouble(PipeDiameter_TextBox.Text) * 3.1415926 / 1000));
        }

        private void HideButtlesPanel_Click(object sender, EventArgs e)
        {
            CollectSet_Panel.ColumnStyles[3].Width = 150;
            CollectSet_Panel.ColumnStyles[1].Width = 0;
            CollectSet_Panel.ColumnStyles[2].Width = CollectSet_Panel.Width - CollectSet_Panel.ColumnStyles[0].Width - CollectSet_Panel.ColumnStyles[3].Width;
        }

        private void HideCollectorProgramPanel_Click(object sender, EventArgs e)
        {
            CollectSet_Panel.ColumnStyles[3].Width = 150;
            CollectSet_Panel.ColumnStyles[2].Width = 0;
            CollectSet_Panel.ColumnStyles[1].Width = CollectSet_Panel.Width - CollectSet_Panel.ColumnStyles[0].Width - CollectSet_Panel.ColumnStyles[3].Width;
        }

        private void CollectorButtles_Fresher_Tick(object sender, EventArgs e)
        {
            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
            {
                if (!StartCollectorProgram.Enabled && !StopCollectorProgram.Enabled)
                {
                    if (th_collectorProgram != null)
                    {
                        StartCollectorProgram.Enabled = false;
                        StopCollectorProgram.Enabled = true;
                    }
                    else
                    {
                        StartCollectorProgram.Enabled = true;
                        StopCollectorProgram.Enabled = false;
                    }
                }
                else {
                    buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Image = Image.FromFile("Icon//Current.png");
                }
                GoToLastButtle.Enabled = true;
                GoToNextButtle.Enabled = true;
            }
            else
            {
                StartCollectorProgram.Enabled = false;
                StopCollectorProgram.Enabled = false;
                GoToLastButtle.Enabled = false;
                GoToNextButtle.Enabled = false;
                CurrentButtleNo_Label.Text = "当前瓶号：";
            }

            if (!FrmMain.ISRegisted())
                StartCollectorProgram.Enabled = false;

            //buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Image = Image.FromFile("Icon//Empty.png");
            int buttleSelectedNo = UVDetector.CurvShow.GetButtleSelected();
            if (buttleSelectedNo != 0)
            {
                if (buttleSelected[buttleSelectedNo - 1])
                {
                    buttleSelected[buttleSelectedNo - 1] = false;
                    buttles[buttleSelectedNo - 1].BackColor = Color.FromName("Control");
                    //buttles[buttleSelectedNo - 1].Image = Image.FromFile("Icon//Empty.png");
                    UVDetector.CurvShow.ClearButtleSelected();
                }
                else
                {
                    buttleSelected[buttleSelectedNo - 1] = true;
                    buttles[buttleSelectedNo - 1].BackColor = Color.LightGreen;
                    //buttles[buttleSelectedNo - 1].Image = Image.FromFile("Icon//Selected.png");
                    UVDetector.CurvShow.ClearButtleSelected();
                }
            }
        }

        public void Buttles_Command(object sender, EventArgs e)
        {
            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
            {
                buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Image = Image.FromFile("Icon//Empty.png");
                ((Button)sender).Image = Image.FromFile("Icon//Current.png");
                Collector.Collector.t_CollectorPara.currentButtleNo = Convert.ToInt32(((Button)sender).Text);
                if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                    FrmLeft.collectorInstance.GotoButtle(Collector.Collector.t_CollectorPara.currentButtleNo);
                CurrentButtleNo_Label.Text = "当前瓶号：" + Collector.Collector.t_CollectorPara.currentButtleNo.ToString();
            }
        }

        private void GoToLastButtle_Click(object sender, EventArgs e)
        {
            if (Collector.Collector.t_CollectorPara.currentButtleNo == 1) return;
            buttles[Collector.Collector.t_CollectorPara.currentButtleNo-- - 1].Image = Image.FromFile("Icon//Empty.png");
            buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Image = Image.FromFile("Icon//Current.png");
            CurrentButtleNo_Label.Text = "当前瓶号：" + Collector.Collector.t_CollectorPara.currentButtleNo.ToString();
            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                FrmLeft.collectorInstance.GotoButtle(Collector.Collector.t_CollectorPara.currentButtleNo);
        }

        private void GoToNextButtle_Click(object sender, EventArgs e)
        {
            if (Collector.Collector.t_CollectorPara.currentButtleNo == 120) return;
            buttles[Collector.Collector.t_CollectorPara.currentButtleNo++ - 1].Image = Image.FromFile("Icon//Empty.png");
            buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Image = Image.FromFile("Icon//Current.png");
            CurrentButtleNo_Label.Text = "当前瓶号：" + Collector.Collector.t_CollectorPara.currentButtleNo.ToString();
            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                FrmLeft.collectorInstance.GotoButtle(Collector.Collector.t_CollectorPara.currentButtleNo);
        }

        private void CollectorProgram_Grid_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            for (int i = 0; i < CollectorProgram_Grid.RowCount; ++i)
                CollectorProgram_Grid.Rows[i].HeaderCell.Value = (i + 1).ToString();
            CollectorProgram_Grid.Refresh();
        }

        private void CollectorProgram_Grid_MouseMove(object sender, MouseEventArgs e)
        {
            CollectorProgram_Grid.Focus();
        }

        private void CollectorProgram_Add_Click(object sender, EventArgs e)
        {
            CollectorProgram_Grid.Rows.Add();
        }

        private void CollectorProgram_Insert_Click(object sender, EventArgs e)
        {
            if (CollectorProgram_Grid.RowCount > 0 && CollectorProgram_Grid.CurrentRow.Index >= 0)
                CollectorProgram_Grid.Rows.Insert(CollectorProgram_Grid.CurrentRow.Index, new DataGridViewRow());
        }

        private void CollectorProgram_Del_Click(object sender, EventArgs e)
        {
            if (CollectorProgram_Grid.RowCount > 0 && CollectorProgram_Grid.CurrentRow.Index >= 0)
                CollectorProgram_Grid.Rows.RemoveAt(CollectorProgram_Grid.CurrentRow.Index);
        }

        private void StartButtleNo_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (StartButtleNo_TextBox.Text == "" || StopButtleNo_TextBox.Text == "") return;
            for (int i = 0; i < Convert.ToInt32(StartButtleNo_TextBox.Text); ++i)
                buttles[i].Enabled = false;
            for (int i = Convert.ToInt32(StartButtleNo_TextBox.Text) - 1; i < Convert.ToInt32(StopButtleNo_TextBox.Text); ++i)
                buttles[i].Enabled = true;
        }

        private void StopButtleNo_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (StartButtleNo_TextBox.Text == "" || StopButtleNo_TextBox.Text == "") return;
            for (int i = Convert.ToInt32(StartButtleNo_TextBox.Text) - 1; i < Convert.ToInt32(StopButtleNo_TextBox.Text); ++i)
                buttles[i].Enabled = true;
            for (int i = Convert.ToInt32(StopButtleNo_TextBox.Text); i < 120; ++i)
                buttles[i].Enabled = false;
        }

        private void CollectorProgram_Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int row = 0; row < CollectorProgram_Grid.RowCount; ++row)
            {
                if (CollectorProgram_Grid.Rows[row].Cells[0].Value != null)
                {
                    switch (CollectorProgram_Grid.Rows[row].Cells[0].Value.ToString())
                    {
                        case "时间":
                            CollectorProgram_Grid.Rows[row].Cells[1].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "阈值":
                            CollectorProgram_Grid.Rows[row].Cells[1].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[3].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "斜率":
                            CollectorProgram_Grid.Rows[row].Cells[1].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionBackColor = Color.Empty;
                            break;
                        case "时间&阈值":
                            CollectorProgram_Grid.Rows[row].Cells[1].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[3].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "时间&阈值&斜率":
                        case "时间&(阈值|斜率)":
                            CollectorProgram_Grid.Rows[row].Cells[1].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[1].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[2].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[3].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[3].Style.SelectionBackColor = Color.Empty;
                            break;
                    }
                }
                if (CollectorProgram_Grid.Rows[row].Cells[4].Value != null)
                {
                    switch (CollectorProgram_Grid.Rows[row].Cells[4].Value.ToString())
                    {
                        case "时间":
                            CollectorProgram_Grid.Rows[row].Cells[5].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "阈值":
                            CollectorProgram_Grid.Rows[row].Cells[5].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[7].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "斜率":
                            CollectorProgram_Grid.Rows[row].Cells[5].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionBackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionBackColor = Color.Empty;
                            break;
                        case "时间&阈值":
                            CollectorProgram_Grid.Rows[row].Cells[5].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[7].ReadOnly = true;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.BackColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionForeColor = this.BackColor;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionBackColor = this.BackColor;
                            break;
                        case "时间&阈值&斜率":
                        case "时间&(阈值|斜率)":
                            CollectorProgram_Grid.Rows[row].Cells[5].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[5].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[6].Style.SelectionBackColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[7].ReadOnly = false;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.BackColor = Color.FromName("Window");
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionForeColor = Color.Empty;
                            CollectorProgram_Grid.Rows[row].Cells[7].Style.SelectionBackColor = Color.Empty;
                            break;
                    }
                }
            }
        }

        private Thread th_collectorProgram;
        private bool runProgram = true;
        private void RunCollectorProgram_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            if (runProgram)
                ret = MessageBox.Show("是否启动馏分收集器程序？", "馏分收集器", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            else
                ret = DialogResult.Yes;
            switch (ret)
            {
                case DialogResult.Yes:
                    th_collectorProgram = new Thread(CollectorProgram);
                    th_collectorProgram.Start();
                    ShowCollectorLog("启动馏分收集器程序");

                    StartCollectorProgram.Enabled = false;
                    StopCollectorProgram.Enabled = true;

                    break;
                case DialogResult.No:
                    break;
            }
        }
        private void StopCollectorProgram_Click(object sender, EventArgs e)
        {
            //DialogResult ret;
            //if (runProgram)
            //    ret = MessageBox.Show("是否停止馏分收集器程序？", "馏分收集器", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //else
            //    ret = DialogResult.Yes;
            //switch (ret)
            //{
            //    case DialogResult.Yes:
                    //关电磁阀
                    if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                        FrmLeft.collectorInstance.StopCollect();

                    th_collectorProgram.Abort();
                    ShowCollectorLog("停止馏分收集器程序");

                    StartCollectorProgram.Enabled = true;
                    StopCollectorProgram.Enabled = false;
            //        break;
            //    case DialogResult.No:
            //        break;
            //}
        }


        private void CollectorProgram()
        {
            int collectorProcessCnt = 0;
            DateTime startCollectTime;
            double sTime = 0;
            bool peakStartCondition = false;
            bool peakStopCondition = false;
            bool channel0 = false, channel1 = false, channel2 = false;
            double delayVolume = 0;
            double totalFlow = 0;

            this.Invoke(new EventHandler(delegate
            {
                channel0 = CollectorProgramChannel1.Checked;
                channel1 = CollectorProgramChannel2.Checked && UVDetector.UV.t_UVPara.uvWaveLengthCnt > 1;
                channel2 = CollectorProgramChannel3.Checked && UVDetector.UV.t_UVPara.uvWaveLengthCnt > 2;

                delayVolume = Convert.ToDouble(DelayVolume.Text);
                //FrmPump.TotalFlow = 100;
                totalFlow = FrmPump.TotalFlow;
            }));

            while (collectorProcessCnt < CollectorProgram_Grid.RowCount)
            {
                this.Invoke(new EventHandler(delegate
                {
                    CollectorProgram_Grid.CurrentCell = CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[0];
                }));
                while (!peakStartCondition)
                {
                    Thread.Sleep(10);
                    switch (CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[0].Value.ToString())
                    {
                        case "时间":
                            if (UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[1].Value))
                                peakStartCondition = true;
                            break;
                        case "阈值":
                            if ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value)))
                                peakStartCondition = true;
                            break;
                        case "斜率":
                            if ((channel0 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value))))
                                peakStartCondition = true;
                            break;
                        case "时间&阈值":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[1].Value))
                                && ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))))
                                peakStartCondition = true;
                            break;
                        case "时间&阈值&斜率":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[1].Value))
                                && ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value)))
                                && ((channel0 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))))
                                peakStartCondition = true;
                            break;
                        case "时间&(阈值|斜率)":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[1].Value))
                                && (((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[2].Value)))
                                || ((channel0 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStartSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[3].Value))))))
                                peakStartCondition = true;
                            break;
                        default:
                            continue;
                    }
                }
                peakStartCondition = false;
                UVDetector.UV.CurvPeak.collectorButtleNo = Collector.Collector.t_CollectorPara.currentButtleNo;
                //延迟体积
                startCollectTime = DateTime.Now;
                while (((TimeSpan)(DateTime.Now - startCollectTime)).TotalMinutes * totalFlow < delayVolume) Thread.Sleep(10);
                sTime = UVDetector.CurvShow.GetUVTime("min");
                //开电磁阀
                if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen
                    && buttles[Collector.Collector.t_CollectorPara.currentButtleNo - 1].Enabled)
                    FrmLeft.collectorInstance.StartCollect();
                while (!peakStopCondition)
                {
                    Thread.Sleep(10);
                    this.Invoke(new EventHandler(delegate
                    {
                        double nTime = UVDetector.CurvShow.GetUVTime("min");
                        double accVolume = (nTime - sTime) * totalFlow;
                        if ((ButtleVolume_TextBox.Text != "" && accVolume > Convert.ToDouble(ButtleVolume_TextBox.Text))
                            || (CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[8].Value != null                            
                            && accVolume > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[8].Value))
                            || (CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[9].Value != null
                            && (nTime - sTime) > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[9].Value)))
                        {
                            //下一瓶
                            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                                GoToNextButtle_Click(null, null);
                            UVDetector.UV.CurvPeak.collectorButtleNo = Collector.Collector.t_CollectorPara.currentButtleNo;
                            sTime = UVDetector.CurvShow.GetUVTime("min");
                        }
                    }));

                    switch (CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[4].Value.ToString())
                    {
                        case "时间":
                            if (UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[5].Value))
                                peakStopCondition = true;
                            break;
                        case "阈值":
                            if ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value)))
                                peakStopCondition = true;
                            break;
                        case "斜率":
                            if ((channel0 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value))))
                                peakStopCondition = true;
                            break;
                        case "时间&阈值":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[5].Value))
                                && ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))))
                                peakStopCondition = true;
                            break;
                        case "时间&阈值&斜率":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[5].Value))
                                && ((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value)))
                                && ((channel0 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))))
                                peakStopCondition = true;
                            break;
                        case "时间&(阈值|斜率)":
                            if ((UVDetector.CurvShow.GetUVTime("min") > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[5].Value))
                                && (((channel0 && UVDetector.CurvShow.GetUVValue("mAu", 0) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel1 && UVDetector.CurvShow.GetUVValue("mAu", 1) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value))
                                || (channel2 && UVDetector.CurvShow.GetUVValue("mAu", 2) - UVDetector.CurvShow.GetCurv_yMin() > Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[6].Value)))
                                || ((channel0 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[0], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel1 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[1], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value)))
                                || (channel2 && UVDetector.CurvShow.GetStopSlop(UVDetector.UV.curvQueue[2], Convert.ToDouble(CollectorProgram_Grid.Rows[collectorProcessCnt].Cells[7].Value))))))
                                peakStopCondition = true;
                            break;
                        default:
                            continue;
                    }
                }
                peakStopCondition = false;
                UVDetector.UV.CurvPeak.collectorButtleNo = 0;
                startCollectTime = DateTime.Now;
                while (((TimeSpan)(DateTime.Now - startCollectTime)).TotalMinutes * totalFlow < delayVolume) Thread.Sleep(10);
                //关电磁阀
                if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                    FrmLeft.collectorInstance.StopCollect();
                collectorProcessCnt++;
                if (collectorProcessCnt >= CollectorProgram_Grid.RowCount) break;
                this.Invoke(new EventHandler(delegate
                {
                    //下一瓶
                    if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                        GoToNextButtle_Click(null, null);
                }));

            }

            //关电磁阀
            if (FrmLeft.collectorInstance != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort != null && FrmLeft.collectorInstance.t_SerialPortCommu.collectorPort.IsOpen)
                FrmLeft.collectorInstance.StopCollect();

            //UVDetector.UV.ISPeakStart(UVDetector.UV.curvQueue[CollectorProgram_Channel_ComboBox.SelectedIndex]);
            //UVDetector.UV.SetPeakQueue()
        }


        /****************************************************************************/
        

        /****************************************************************************/
        private void ShowCollectorLog(string log)
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


        public void Start_CollectorProgram()
        {
            if (StartCollectorProgram.Enabled)
            { 
                runProgram = false;
                RunCollectorProgram_Click(null, null);
                runProgram = true;
            }
        }
        public void Stop_CollectorProgram()
        {
            if (StopCollectorProgram.Enabled)
            {
                //runProgram = false;
                StopCollectorProgram_Click(null, null);
                //runProgram = true;
            }
        }

    }
}
