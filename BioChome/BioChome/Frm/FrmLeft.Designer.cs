//using System.Data;
//using System.IO.Ports;

namespace BioChome
{
    partial class FrmLeft
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                //DataRow[] d = equip_DataTable.Select();
                //foreach (DataRow d0 in d)
                //{
                //    if (((SerialPort)d0["ComPort"]).IsOpen)
                //        ((SerialPort)d0["ComPort"]).Dispose();
                //    else
                //        continue;
                //}
                //if (findingPort.IsOpen) findingPort.Dispose();

                //if (pumpInstanceA != null)
                //    pumpInstanceA.PumpInstanceDispose();
                //if (pumpInstanceB != null)
                //    pumpInstanceB.PumpInstanceDispose();
                //if (pumpInstanceC != null)
                //    pumpInstanceC.PumpInstanceDispose();
                //if (pumpInstanceD != null)
                //    pumpInstanceD.PumpInstanceDispose();

                //if (th_findSerialPort != null && th_findSerialPort.IsAlive)
                //{
                //    th_findSerialPort.Abort();
                //}
                //if (th_overTimeSerialPort != null && th_overTimeSerialPort.IsAlive)
                //{
                //    th_overTimeSerialPort.Abort();
                //}
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("紫外检测器");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("A组分");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("B组分");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("C组分");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("D组分");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("泵", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("馏分收集器");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("采集盒");
            this.AddEquip_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.equipList = new System.Windows.Forms.TreeView();
            this.RefreshEquipList_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindSerialPort_Progress = new System.Windows.Forms.ProgressBar();
            this.FindSerialPort_Label = new System.Windows.Forms.Label();
            this.AddEquip_Menu.SuspendLayout();
            this.RefreshEquipList_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddEquip_Menu
            // 
            this.AddEquip_Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AddEquip_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.AddEquip_Menu.Name = "contextMenuStrip1";
            this.AddEquip_Menu.Size = new System.Drawing.Size(88, 52);
            this.AddEquip_Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.AddEquip_Menu_ItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(87, 24);
            this.toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(87, 24);
            this.toolStripMenuItem3.Text = "2";
            // 
            // equipList
            // 
            this.equipList.BackColor = System.Drawing.SystemColors.Window;
            this.equipList.ContextMenuStrip = this.RefreshEquipList_Menu;
            this.equipList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipList.HotTracking = true;
            this.equipList.Location = new System.Drawing.Point(0, 0);
            this.equipList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.equipList.Name = "equipList";
            treeNode1.ContextMenuStrip = this.AddEquip_Menu;
            treeNode1.ForeColor = System.Drawing.Color.DarkGray;
            treeNode1.Name = "UV";
            treeNode1.Text = "紫外检测器";
            treeNode2.ContextMenuStrip = this.AddEquip_Menu;
            treeNode2.ForeColor = System.Drawing.Color.DarkGray;
            treeNode2.Name = "PumpA";
            treeNode2.Text = "A组分";
            treeNode3.ContextMenuStrip = this.AddEquip_Menu;
            treeNode3.ForeColor = System.Drawing.Color.DarkGray;
            treeNode3.Name = "PumpB";
            treeNode3.Text = "B组分";
            treeNode4.ContextMenuStrip = this.AddEquip_Menu;
            treeNode4.ForeColor = System.Drawing.Color.DarkGray;
            treeNode4.Name = "PumpC";
            treeNode4.Text = "C组分";
            treeNode5.ContextMenuStrip = this.AddEquip_Menu;
            treeNode5.ForeColor = System.Drawing.Color.DarkGray;
            treeNode5.Name = "PumpD";
            treeNode5.Text = "D组分";
            treeNode6.Name = "Pump";
            treeNode6.Text = "泵";
            treeNode7.ContextMenuStrip = this.AddEquip_Menu;
            treeNode7.ForeColor = System.Drawing.Color.DarkGray;
            treeNode7.Name = "Collector";
            treeNode7.Text = "馏分收集器";
            treeNode8.ContextMenuStrip = this.AddEquip_Menu;
            treeNode8.ForeColor = System.Drawing.Color.DarkGray;
            treeNode8.Name = "PH";
            treeNode8.Text = "采集盒";
            this.equipList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode7,
            treeNode8});
            this.equipList.ShowNodeToolTips = true;
            this.equipList.Size = new System.Drawing.Size(192, 461);
            this.equipList.TabIndex = 7;
            this.equipList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.equipList_NodeMouseClick);
            this.equipList.DoubleClick += new System.EventHandler(this.equipList_DoubleClick);
            this.equipList.Leave += new System.EventHandler(this.equipList_Leave);
            // 
            // RefreshEquipList_Menu
            // 
            this.RefreshEquipList_Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RefreshEquipList_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem});
            this.RefreshEquipList_Menu.Name = "RefreshEquipList_Menu";
            this.RefreshEquipList_Menu.Size = new System.Drawing.Size(109, 28);
            this.RefreshEquipList_Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.RefreshEquipList_Menu_ItemClicked);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.刷新ToolStripMenuItem.Text = "刷新";
            // 
            // FindSerialPort_Progress
            // 
            this.FindSerialPort_Progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FindSerialPort_Progress.Location = new System.Drawing.Point(0, 435);
            this.FindSerialPort_Progress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FindSerialPort_Progress.Name = "FindSerialPort_Progress";
            this.FindSerialPort_Progress.Size = new System.Drawing.Size(192, 26);
            this.FindSerialPort_Progress.TabIndex = 8;
            this.FindSerialPort_Progress.Visible = false;
            // 
            // FindSerialPort_Label
            // 
            this.FindSerialPort_Label.AutoSize = true;
            this.FindSerialPort_Label.BackColor = System.Drawing.SystemColors.Window;
            this.FindSerialPort_Label.Location = new System.Drawing.Point(-3, 416);
            this.FindSerialPort_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FindSerialPort_Label.Name = "FindSerialPort_Label";
            this.FindSerialPort_Label.Size = new System.Drawing.Size(121, 15);
            this.FindSerialPort_Label.TabIndex = 9;
            this.FindSerialPort_Label.Text = "正在扫描设备...";
            this.FindSerialPort_Label.Visible = false;
            // 
            // FrmLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 461);
            this.Controls.Add(this.FindSerialPort_Label);
            this.Controls.Add(this.FindSerialPort_Progress);
            this.Controls.Add(this.equipList);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmLeft";
            this.TabText = "设备列表";
            this.Text = "FrmLeft";
            this.DockStateChanged += new System.EventHandler(this.FrmLeft_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLeft_FormClosing);
            this.Load += new System.EventHandler(this.FrmLeft_Load);
            this.AddEquip_Menu.ResumeLayout(false);
            this.RefreshEquipList_Menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView equipList;
        private System.Windows.Forms.ContextMenuStrip AddEquip_Menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip RefreshEquipList_Menu;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ProgressBar FindSerialPort_Progress;
        private System.Windows.Forms.Label FindSerialPort_Label;
    }
}