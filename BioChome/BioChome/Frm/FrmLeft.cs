using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace BioChome
{
    public partial class FrmLeft : DockContent
    {
        private static FrmLeft Instance;

        private SerialPort findingPort;
        private string[] serialPortList;
        private DataTable equip_DataTable;
        private DataTable equipShow_DataTable;

        public static UVDetector.UV uvInstance;
        public static Pump.Pump pumpInstanceA;
        public static Pump.Pump pumpInstanceB;
        public static Pump.Pump pumpInstanceC;
        public static Pump.Pump pumpInstanceD;
        public static Collector.Collector collectorInstance;
        public static PH.PH phInstance;

        public FrmLeft()
        {
            InitializeComponent();
            equipList.ExpandAll();
            //equipList.SelectedNode.BackColor = Color.Gray;
            equipList.SelectedNode = null;
        }

        private void equipList_Leave(object sender, EventArgs e)
        {
            equipList.SelectedNode = null;
        }

        public static FrmLeft GetInstance()
        {
            Instance = new FrmLeft();
            return Instance;
        }

        private Thread th_findSerialPort;
        private void FrmLeft_Load(object sender, EventArgs e)
        {
            th_findSerialPort = new Thread(FindSerialPort);
            th_findSerialPort.Start();

            serialPortList = new string[0];
            equip_DataTable = new DataTable();
            equip_DataTable.Columns.Add("ComPort", typeof(SerialPort));
            equip_DataTable.Columns.Add("PortName", typeof(string));
            equip_DataTable.Columns.Add("ID", typeof(string));
            equip_DataTable.Columns.Add("Type", typeof(string));
            equip_DataTable.Columns.Add("No", typeof(string));
            equip_DataTable.Columns.Add("Remarks", typeof(string));

            equipShow_DataTable = new DataTable();
            equipShow_DataTable.Columns.Add("Type", typeof(string));
            equipShow_DataTable.Columns.Add("No", typeof(string));
            equipShow_DataTable.Columns.Add("Remarks", typeof(string));

            this.DockPanel.DockLeftPortion = 110;

            //DataRow drsUV = equipShow_DataTable.NewRow();
            //drsUV["Type"] = "UV";
            //drsUV["No"] = "1001";
            //drsUV["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsUV);
            //DataRow drsPumpA = equipShow_DataTable.NewRow();
            //drsPumpA["Type"] = "Pump";
            //drsPumpA["No"] = "2001";
            //drsPumpA["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsPumpA);
            //DataRow drsPumpB = equipShow_DataTable.NewRow();
            //drsPumpB["Type"] = "Pump";
            //drsPumpB["No"] = "2002";
            //drsPumpB["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsPumpB);
            //DataRow drsPumpC = equipShow_DataTable.NewRow();
            //drsPumpC["Type"] = "Pump";
            //drsPumpC["No"] = "2003";
            //drsPumpC["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsPumpC);
            //DataRow drsPumpD = equipShow_DataTable.NewRow();
            //drsPumpD["Type"] = "Pump";
            //drsPumpD["No"] = "2004";
            //drsPumpD["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsPumpD);
            //DataRow drsCollector = equipShow_DataTable.NewRow();
            //drsCollector["Type"] = "Collector";
            //drsCollector["No"] = "3001";
            //drsCollector["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsCollector);
            //DataRow drsPH = equipShow_DataTable.NewRow();
            //drsPH["Type"] = "PH";
            //drsPH["No"] = "4001";
            //drsPH["Remarks"] = "";
            //equipShow_DataTable.Rows.Add(drsPH);
        }

        private void FrmLeft_DockStateChanged(object sender, EventArgs e)
        {
            if (Instance != null)
            {
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.ms_FrmLeft = this.DockState;
            }

        }

        private void FrmLeft_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AppConfig.frmLeft = null;
            DataRow[] d = equip_DataTable.Select();
            foreach (DataRow d0 in d)
            {
                if (((SerialPort)d0["ComPort"]).IsOpen)
                    ((SerialPort)d0["ComPort"]).Dispose();
                else
                    continue;
            }
            if (findingPort.IsOpen) findingPort.Dispose();

            e.Cancel = true;
            this.Hide();
        }

        private void equipList_DoubleClick(object sender, EventArgs e)
        {
            switch (equipList.SelectedNode.Name)
            {
                case "UV":
                    AppConfig.frmMain.NewUVDocument();
                    break;
                case "PumpA":
                case "PumpB":
                case "PumpC":
                case "PumpD":
                    AppConfig.frmMain.NewPumpDocument();
                    break;
                case "Collector":
                    AppConfig.frmMain.NewCollectorDocument();
                    break;
                case "PH":
                    AppConfig.frmMain.NewPHDocument();
                    break;
            }
        }

        private TreeNode rightClickNode;
        private void equipList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (e.Node.Name)
                {
                    case "UV":
                        rightClickNode = e.Node;
                        DataRow[] drsUV = equipShow_DataTable.Select("Type = 'UV'");
                        AddEquip_Menu.Items.Clear();
                        if (e.Node.ToolTipText != "")
                        {
                            AddEquip_Menu.Items.Add("断开连接");
                        } else
                        {
                            foreach (DataRow equ in drsUV)
                                AddEquip_Menu.Items.Add(equ["No"].ToString());
                        }
                        break;
                    case "PumpA":
                    case "PumpB":
                    case "PumpC":
                    case "PumpD":
                        rightClickNode = e.Node;
                        DataRow[] drsPump = equipShow_DataTable.Select("Type = 'Pump'");
                        AddEquip_Menu.Items.Clear();
                        if (e.Node.ToolTipText != "")
                        {
                            AddEquip_Menu.Items.Add("断开连接");
                        } else
                        {
                            foreach (DataRow equ in drsPump)
                                AddEquip_Menu.Items.Add(equ["No"].ToString());
                        }
                        break;
                    case "Collector":
                        rightClickNode = e.Node;
                        DataRow[] drsCollector = equipShow_DataTable.Select("Type = 'Collector'");
                        AddEquip_Menu.Items.Clear();
                        if (e.Node.ToolTipText != "")
                        {
                            AddEquip_Menu.Items.Add("断开连接");
                        } else
                        {
                            foreach (DataRow equ in drsCollector)
                                AddEquip_Menu.Items.Add(equ["No"].ToString());
                        }
                        break;
                    case "PH":
                        rightClickNode = e.Node;
                        DataRow[] drsPH = equipShow_DataTable.Select("Type = 'PH'");
                        AddEquip_Menu.Items.Clear();
                        if (e.Node.ToolTipText != "")
                        {
                            AddEquip_Menu.Items.Add("断开连接");
                        } else
                        {
                            foreach (DataRow equ in drsPH)
                                AddEquip_Menu.Items.Add(equ["No"].ToString());
                        }
                        break;
                }
            }
        }

        private void AddEquip_Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "断开连接")
            {
                 DataRow[] drsConnect = equip_DataTable.Select("No = '" + rightClickNode.ToolTipText + "'");
                ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                switch (rightClickNode.Name)
                {
                    case "UV":
                        uvInstance.ClearUVSerialPort();
                        uvInstance.UVInstanceDispose();
                        ShowSerialPortLog("紫外检测器" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                    case "PumpA":
                        pumpInstanceA.ClearPumpSerialPort();
                        pumpInstanceA.PumpInstanceDispose();
                        ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                    case "PumpB":
                        pumpInstanceB.ClearPumpSerialPort();
                        pumpInstanceB.PumpInstanceDispose();
                        ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                    case "PumpC":
                        pumpInstanceC.ClearPumpSerialPort();
                        pumpInstanceC.PumpInstanceDispose();
                        ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                   case "PumpD":
                        pumpInstanceD.ClearPumpSerialPort();
                        pumpInstanceD.PumpInstanceDispose();
                        ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                    case "Collector":
                        collectorInstance.ClearCollectorSerialPort();
                        collectorInstance.CollectorInstanceDispose();
                        ShowSerialPortLog("馏分收集器" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                    case "PH":
                        phInstance.ClearPHSerialPort();
                        phInstance.PHInstanceDispose();
                        ShowSerialPortLog("采集器" + drsConnect[0]["No"].ToString() + "断开联接");
                        break;
                }
                switch (rightClickNode.Name)
                {
                    case "UV":
                        DataRow drsUV = equipShow_DataTable.NewRow();
                        drsUV["Type"] = "UV";
                        drsUV["No"] = rightClickNode.ToolTipText;
                        drsUV["Remarks"] = "";
                        equipShow_DataTable.Rows.Add(drsUV);
                        break;
                    case "PumpA":
                    case "PumpB":
                    case "PumpC":
                    case "PumpD":
                        DataRow drsPump = equipShow_DataTable.NewRow();
                        drsPump["Type"] = "Pump";
                        drsPump["No"] = rightClickNode.ToolTipText;
                        drsPump["Remarks"] = "";
                        equipShow_DataTable.Rows.Add(drsPump);
                        break;
                    case "Collector":
                        DataRow drsCollector = equipShow_DataTable.NewRow();
                        drsCollector["Type"] = "Collector";
                        drsCollector["No"] = rightClickNode.ToolTipText;
                        drsCollector["Remarks"] = "";
                        equipShow_DataTable.Rows.Add(drsCollector);
                        break;
                    case "PH":
                        DataRow drsPH = equipShow_DataTable.NewRow();
                        drsPH["Type"] = "PH";
                        drsPH["No"] = rightClickNode.ToolTipText;
                        drsPH["Remarks"] = "";
                        equipShow_DataTable.Rows.Add(drsPH);
                        break;
                }
                rightClickNode.ForeColor = Color.DarkGray;
                rightClickNode.ToolTipText = "";
            }
            else
            {
                DataRow[] drsConnect = equip_DataTable.Select("No = '" + e.ClickedItem.Text + "'");
                try
                {
                    ((SerialPort)drsConnect[0]["ComPort"]).Open();
                }catch
                {
                    return;
                }
                switch (rightClickNode.Name)
                {
                    case "UV":
                        uvInstance = new UVDetector.UV();
                        uvInstance.SetUVSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (uvInstance.InitialUV())
                            ShowSerialPortLog("紫外检测器" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("紫外检测器" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                    case "PumpA":
                        pumpInstanceA = new Pump.Pump();
                        pumpInstanceA.SetPumpSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (pumpInstanceA.InitialPump('A', drsConnect[0]["No"].ToString()))
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        { 
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                    break;
                    case "PumpB":
                        pumpInstanceB = new Pump.Pump();
                        pumpInstanceB.SetPumpSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (pumpInstanceB.InitialPump('B', drsConnect[0]["No"].ToString()))
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                    case "PumpC":
                        pumpInstanceC = new Pump.Pump();
                        pumpInstanceC.SetPumpSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (pumpInstanceC.InitialPump('C', drsConnect[0]["No"].ToString()))
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                    case "PumpD":
                        pumpInstanceD = new Pump.Pump();
                        pumpInstanceD.SetPumpSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (pumpInstanceD.InitialPump('D', drsConnect[0]["No"].ToString()))
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("泵" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                    case "Collector":
                        collectorInstance = new Collector.Collector();
                        collectorInstance.SetCollectorSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (collectorInstance.InitialCollector())
                            ShowSerialPortLog("馏分收集器" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("馏分收集器" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                    case "PH":
                        phInstance = new PH.PH();
                        phInstance.SetPHSerialPort((SerialPort)drsConnect[0]["ComPort"], drsConnect[0]["ID"].ToString());
                        if (phInstance.InitialPH(e.ClickedItem.Text))
                            ShowSerialPortLog("采集器" + drsConnect[0]["No"].ToString() + "建立联接");
                        else
                        {
                            ShowSerialPortLog("采集器" + drsConnect[0]["No"].ToString() + "初始化失败，请重试");
                            ((SerialPort)drsConnect[0]["ComPort"]).Dispose();
                            return;
                        }
                        break;
                }

                rightClickNode.ToolTipText = e.ClickedItem.Text;
                rightClickNode.ForeColor = Color.Empty;
                DataRow[] drs = equipShow_DataTable.Select("No = '" + e.ClickedItem.Text + "'");
                if (drs.Length > 0) equipShow_DataTable.Rows.Remove(drs[0]);
            }

        }

        private void RefreshEquipList_Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (string s in serialPortList)
            {
                DataRow[] d0 = equip_DataTable.Select("PortName = '" + s + "'");
                if (d0.Length > 0)
                {
                    foreach (DataRow d00 in d0)
                    {
                        if (((SerialPort)d00["ComPort"]).IsOpen) continue;
                        System.Collections.ArrayList portList1 = new System.Collections.ArrayList(serialPortList);
                        portList1.Remove(s);
                        serialPortList = (string[])portList1.ToArray(typeof(string));
                        DataRow[] d1 = equipShow_DataTable.Select("No = '" + d0[0]["No"] + "'");
                        foreach (DataRow d11 in d1)
                            equipShow_DataTable.Rows.Remove(d11);
                        equip_DataTable.Rows.Remove(d00);
                    }
                } else
                {
                    System.Collections.ArrayList portList2 = new System.Collections.ArrayList(serialPortList);
                    portList2.Remove(s);
                    serialPortList = (string[])portList2.ToArray(typeof(string));
                }
            }
            //foreach (string s in serialPortList)
            //{
            //    DataRow[] d0 = equip_DataTable.Select("PortName = '" + s + "'");

            //    foreach (DataRow d00 in d0)
            //    {
            //        if (((SerialPort)d00["ComPort"]).IsOpen) continue;
            //    //DataRow[] d1 = equip_DataTable.Select("PortName = '" + s + "'");
            //        DataRow[] d1 = equipShow_DataTable.Select("No = '" + d0[0]["No"] + "'");
            //        foreach (DataRow d11 in d1)
            //            equipShow_DataTable.Rows.Remove(d11);
            //        equip_DataTable.Rows.Remove(d00);      
            //    }
            //}
        }



        /*******************************************************************/
        protected void FindSerialPort()
        {
            while (true)
            {
                try
                {                   
                    string[] nowPortNames = SerialPort.GetPortNames();
                     Thread.Sleep(500);
                    int nowPortCnt = nowPortNames.Length;
                    if (serialPortList.Length > nowPortCnt)
                    {
                        FindUnabledSerialPort();
                        serialPortList = new string[nowPortCnt];
                        nowPortNames.CopyTo(serialPortList, 0);
                    }
                    else if (serialPortList.Length < nowPortCnt)
                    {
                        serialPortList = new string[nowPortCnt];
                        nowPortNames.CopyTo(serialPortList, 0);
                        FindNewSerialPort(nowPortNames);
                    }
                } catch
                {
                    continue;
                }
            }
        }

        private string retStr1 = "", retStr2 = "";
        private void FindNewSerialPort(string[] s)
        {
            //string[] s = SerialPort.GetPortNames();
            this.Invoke(new EventHandler(delegate
            {
                FindSerialPort_Progress.Maximum = s.Length;
                FindSerialPort_Progress.Height = 15;
                FindSerialPort_Progress.Value = 0;
                FindSerialPort_Progress.Visible = true;
                FindSerialPort_Label.Left = 0;
                FindSerialPort_Label.Top = FindSerialPort_Progress.Top - FindSerialPort_Label.Height;
                //FindSerialPort_Label.BackColor = Color.Transparent;
                //FindSerialPort_Label.Parent = equipList;
                //FindSerialPort_Label.BringToFront();
                FindSerialPort_Label.Visible = true;
            }));

            //foreach (string s in SerialPort.GetPortNames())
            for (int i = 0; i < s.Length; ++i)
            {
                this.Invoke(new EventHandler(delegate
                {
                    FindSerialPort_Progress.Value = i + 1;
                }));
                findingPort = new SerialPort();
                try
                {
                    if (equip_DataTable.Select("PortName = '" + s[i] + "'").Length == 0)
                    {
                        findingPort.PortName = s[i];
                        findingPort.ReadTimeout = 100;
                        //port.ReceivedBytesThreshold = 1;
                        findingPort.RtsEnable = false;
                        findingPort.DtrEnable = false;
                        findingPort.BaudRate = 9600;
                        findingPort.DataBits = 8;
                        findingPort.StopBits = StopBits.One;
                        findingPort.Parity = Parity.None;
                        findingPort.DiscardNull = false;
                        findingPort.Open();
                        //握手
                        if (EquipHandShake(findingPort))
                        {
                            DataRow drs = equip_DataTable.NewRow();
                            drs["ComPort"] = findingPort;
                            drs["PortName"] = findingPort.PortName;
                            if (retStr1.Contains("A") && retStr1.Contains("Y"))
                            {
                                drs["ID"] = "Monitor";
                                drs["Type"] = "PH";
                                drs["No"] = "PH/C Monitor";
                            }
                            else if (retStr1.Contains("Q") && retStr1.Contains("Y"))
                            {
                                drs["ID"] = "Tauto";
                                drs["Type"] = "Pump";
                                drs["No"] = GetEquipNo(retStr2);
                            }
                            else
                            {
                                drs["ID"] = GetEquipID(retStr1);
                                drs["Type"] = GetEquipType(retStr1);
                                drs["No"] = GetEquipNo(retStr2);
                            }
                            //drs["Remarks"] =
                            equip_DataTable.Rows.Add(drs);
                            //MessageBox.Show("发现设备！", "端口连接", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            DataRow drsShow = equipShow_DataTable.NewRow();
                            drsShow["Type"] = drs["Type"];
                            drsShow["No"] = drs["No"];
                            drsShow["Remarks"] = drs["Remarks"];
                            equipShow_DataTable.Rows.Add(drsShow);
                            retStr1 = "";
                            retStr2 = "";
                        }
                        findingPort.Dispose();
                    }
                }
                catch (Exception e)
                {
                    findingPort.Dispose();
                    continue;
                }
            }
            this.Invoke(new EventHandler(delegate
            {
                FindSerialPort_Progress.Value = s.Length;
                FindSerialPort_Progress.Visible = false;
                FindSerialPort_Label.Visible = false;
            }));
        }
        private void FindUnabledSerialPort()
        {
            foreach (string x in serialPortList)
            {
                if (Array.IndexOf<string>(SerialPort.GetPortNames(), x) == -1)
                {
                    DataRow[] drsPort = equip_DataTable.Select("PortName = '" + x + "'");

                    if (drsPort.Length > 0)
                    {
                        foreach (DataRow ds in drsPort)
                        {
                            ((SerialPort)ds["ComPort"]).Dispose();
                            string[] nodeNameList = { "UV", "PumpA", "PumpB", "PumpC", "PumpD", "Collector", "PH" };
                            foreach (string nodeName in nodeNameList)
                            {
                                TreeNode[] nodeUV = equipList.Nodes.Find(nodeName, true);
                                foreach (TreeNode node in nodeUV)
                                {
                                    if (node.ToolTipText == ds["No"].ToString())
                                    {
                                        node.ForeColor = Color.DarkGray;
                                        node.ToolTipText = "";
                                        switch (node.Name)
                                        {
                                            case "UV":
                                                uvInstance.ClearUVSerialPort();
                                                ShowSerialPortLog("紫外检测器" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "PumpA":
                                                pumpInstanceA.ClearPumpSerialPort();
                                                ShowSerialPortLog("泵" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "PumpB":
                                                pumpInstanceB.ClearPumpSerialPort();
                                                ShowSerialPortLog("泵" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "PumpC":
                                                pumpInstanceC.ClearPumpSerialPort();
                                                ShowSerialPortLog("泵" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "PumpD":
                                                pumpInstanceD.ClearPumpSerialPort();
                                                ShowSerialPortLog("泵" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "Collector":
                                                collectorInstance.ClearCollectorSerialPort();
                                                ShowSerialPortLog("馏分收集器" + ds["No"].ToString() + "联接错误");
                                                break;
                                            case "PH":
                                                phInstance.ClearPHSerialPort();
                                                ShowSerialPortLog("采集器" + ds["No"].ToString() + "联接错误");
                                                break;
                                        }
                                    }
                                }
                            }
                            DataRow[] drs = equipShow_DataTable.Select("No = '" + ds["No"] + "'");
                            if (drs.Length > 0) equipShow_DataTable.Rows.Remove(drs[0]);
                            equip_DataTable.Rows.Remove(ds);
                        }
                    }
                }
            }
            //this.Invoke(new EventHandler(delegate
            //{
                MessageBox.Show("发现设备已断开！", "端口错误", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                //MessageBox.Show("设备断开连接！", "端口错误", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //}));
        }

        private bool EquipHandShake(SerialPort port)
        {
            int overTime = 0;
            retStr1 = WaitReceive(port, "");
            if (retStr1.Contains("A") && retStr1.Contains("C") && retStr1.Contains("P") && retStr1.Contains("T") && retStr1.Contains("Y"))
            {
                ShowSerialPortLog("发现设备：采集盒");
                return true;
            }
            else if (retStr1.Contains("Q") && retStr1.Contains("Y"))
            {
                do
                {
                    port.Write("Q00I0000Y");
                    retStr2 = WaitReceive(port, "");
                    overTime++;
                } while (overTime < 5 && !retStr2.Contains("I"));
                if (overTime >= 5)
                    return false;
                else
                    ShowSerialPortLog("发现设备：泵" + GetEquipNo(retStr2));
                return true;
            }

            string sendStr1 = "!00001      ";
            port.Write(sendStr1 + GetCRC(sendStr1) + "\n");
            retStr1 = WaitReceive(port, sendStr1);
            if (retStr1 == "") return false;
            if (GetCRC(retStr1.Substring(0,12)) != retStr1.Substring(12,3))
            {
                string sendStr11 = "!00001      ";
                port.Write(sendStr11 + GetCRC(sendStr11) + "\n");
                retStr1 = WaitReceive(port, sendStr11);
                if (retStr1 == "" || GetCRC(retStr1.Substring(0, 12)) != retStr1.Substring(12, 3))
                {
                    ShowSerialPortLog("设备连接错误，请重试！");
                    return false;
                }
            }
            string sendStr2 = "!" + GetEquipID(retStr1) + "003      ";
            port.Write(sendStr2 + GetCRC(sendStr2) + "\n");
            retStr2 = WaitReceive(port, sendStr2);
            if (retStr2 == "") return false;
            if (GetCRC(retStr2.Substring(0, 12)) != retStr2.Substring(12, 3))
            {
                string sendStr22 = "!" + GetEquipID(retStr1) + "003      ";
                port.Write(sendStr22 + GetCRC(sendStr22) + "\n");
                retStr2 = WaitReceive(port, sendStr22);
                if (retStr2 == "" || GetCRC(retStr2.Substring(0, 12)) != retStr2.Substring(12, 3))
                {
                    ShowSerialPortLog("设备连接错误，请重试！");
                    return false;
                }
            }

            switch (GetEquipType(retStr1))
            {
                case "UV":
                    ShowSerialPortLog("发现设备：紫外检测器" + GetEquipNo(retStr2));
                    break;
                case "Pump":
                    ShowSerialPortLog("发现设备：泵" + GetEquipNo(retStr2));
                    break;
                case "Collector":
                    ShowSerialPortLog("发现设备：馏分收集器" + GetEquipNo(retStr2));
                    break;
                case "PH":
                    break;
                default:
                    return false;
            }
            
            return true;
        }

        private string WaitReceive(SerialPort port, string sendStr)
        {
            string s = string.Empty;
            int timeOutCnt = 0;
            if (sendStr == "")
            {
               do
                {
                    try
                    {
                        //s = port.ReadTo("Y");
                        //if (s.Contains("Q")) return s + "Y";
                        s = s + (char)(port.ReadByte());
                        if (s.Contains("\n")) return "";
                        if (s.Contains("Y")) return s;
                    }
                    catch
                    {
                        //return "";
                        timeOutCnt++;
                    }
                    //if (s == "") return "";
                    timeOutCnt++;
                } while (timeOutCnt < 20);
                if (timeOutCnt >= 20) return "";
            }
            else
            {
                try
                {
                    do
                    {
                        s = port.ReadTo("\n");
                        if (s == "") return "";
                        timeOutCnt++;
                    } while (s.Substring(0, 1) != sendStr.Substring(0, 1) || s.Substring(4, 2) != sendStr.Substring(4, 2) && timeOutCnt < 10);
                }
                catch
                {
                    return "";
                }
                if (timeOutCnt >= 10) return "";
            }
            return s;
        }

        private string GetCRC(string s)
        {
            int sum = 0;
            byte[] b = System.Text.Encoding.Default.GetBytes(s);
            for (int i = 0; i < s.Length; ++i)
            {
                //sum = sum + Convert.ToInt32(s.Substring(i, 1));
                sum = sum + Convert.ToInt32(b[i]);
            }

            return string.Format("{0:000}", sum % 256);
        }


        private string GetEquipID(string s)
        {
            return s.Substring(1, 2);
        }
        private string GetEquipType(string s)
        {
            switch (s.Substring(10, 2))
            {
                case "50":
                case "51":
                case "52":
                    return "UV";
                case "10":
                case "11":
                case "20":
                    return "Pump";
                case "98":
                    return "Collector";
            }
            return "";
        }
        private string GetEquipNo(string s)
        {
            if (s.Contains("Y"))
                return s.Substring(4, 4);
            return s.Substring(6, 6);
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
