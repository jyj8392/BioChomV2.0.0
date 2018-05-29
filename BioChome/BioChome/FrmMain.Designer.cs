namespace BioChome
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.Dock_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.Menu_Main = new System.Windows.Forms.MenuStrip();
            this.Report_View = new System.Windows.Forms.ToolStripMenuItem();
            this.Print_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreview_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.Output_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.Modify_Template = new System.Windows.Forms.ToolStripMenuItem();
            this.View_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.reSet_View = new System.Windows.Forms.ToolStripMenuItem();
            this.reLoad_View = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EquipmentList_View = new System.Windows.Forms.ToolStripMenuItem();
            this.Log_View = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickStart_View = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Status_Main = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.PrintReportDialog = new System.Windows.Forms.PrintDialog();
            this.PrintReportPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintReportDocument = new System.Drawing.Printing.PrintDocument();
            this.Register_Fresher = new System.Windows.Forms.Timer(this.components);
            this.Report_Saver = new System.Windows.Forms.SaveFileDialog();
            this.DogSerialNo = new System.Windows.Forms.Label();
            this.Menu_Main.SuspendLayout();
            this.Status_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dock_Main
            // 
            this.Dock_Main.ActiveAutoHideContent = null;
            this.Dock_Main.BackColor = System.Drawing.SystemColors.Control;
            this.Dock_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dock_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dock_Main.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.Dock_Main.Location = new System.Drawing.Point(0, 25);
            this.Dock_Main.Name = "Dock_Main";
            this.Dock_Main.Size = new System.Drawing.Size(1264, 857);
            this.Dock_Main.TabIndex = 1;
            // 
            // Menu_Main
            // 
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Report_View,
            this.View_Menu});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.Size = new System.Drawing.Size(1264, 25);
            this.Menu_Main.TabIndex = 4;
            this.Menu_Main.Text = "menuStrip1";
            // 
            // Report_View
            // 
            this.Report_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Print_Report,
            this.PrintPreview_Report,
            this.Output_Report,
            this.Modify_Template});
            this.Report_View.Name = "Report_View";
            this.Report_View.Size = new System.Drawing.Size(44, 21);
            this.Report_View.Text = "报告";
            this.Report_View.DropDownOpening += new System.EventHandler(this.Report_View_DropDownOpening);
            // 
            // Print_Report
            // 
            this.Print_Report.Name = "Print_Report";
            this.Print_Report.Size = new System.Drawing.Size(124, 22);
            this.Print_Report.Text = "打印";
            this.Print_Report.Visible = false;
            this.Print_Report.Click += new System.EventHandler(this.Print_Report_Click);
            // 
            // PrintPreview_Report
            // 
            this.PrintPreview_Report.Name = "PrintPreview_Report";
            this.PrintPreview_Report.Size = new System.Drawing.Size(124, 22);
            this.PrintPreview_Report.Text = "打印预览";
            this.PrintPreview_Report.Visible = false;
            this.PrintPreview_Report.Click += new System.EventHandler(this.PrintPreview_Report_Click);
            // 
            // Output_Report
            // 
            this.Output_Report.Name = "Output_Report";
            this.Output_Report.Size = new System.Drawing.Size(124, 22);
            this.Output_Report.Text = "生成报告";
            this.Output_Report.Click += new System.EventHandler(this.Output_Report_Click);
            // 
            // Modify_Template
            // 
            this.Modify_Template.Name = "Modify_Template";
            this.Modify_Template.Size = new System.Drawing.Size(124, 22);
            this.Modify_Template.Text = "修改模板";
            this.Modify_Template.Click += new System.EventHandler(this.Modify_Template_Click);
            // 
            // View_Menu
            // 
            this.View_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reSet_View,
            this.reLoad_View,
            this.toolStripSeparator1,
            this.EquipmentList_View,
            this.Log_View,
            this.QuickStart_View});
            this.View_Menu.Name = "View_Menu";
            this.View_Menu.Size = new System.Drawing.Size(44, 21);
            this.View_Menu.Text = "窗口";
            this.View_Menu.DropDownOpening += new System.EventHandler(this.View_Menu_DropDownOpening);
            // 
            // reSet_View
            // 
            this.reSet_View.Name = "reSet_View";
            this.reSet_View.Size = new System.Drawing.Size(124, 22);
            this.reSet_View.Text = "重置窗口";
            this.reSet_View.Click += new System.EventHandler(this.reSet_View_Click);
            // 
            // reLoad_View
            // 
            this.reLoad_View.Name = "reLoad_View";
            this.reLoad_View.Size = new System.Drawing.Size(124, 22);
            this.reLoad_View.Text = "重载窗口";
            this.reLoad_View.Click += new System.EventHandler(this.reLoad_View_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // EquipmentList_View
            // 
            this.EquipmentList_View.Checked = true;
            this.EquipmentList_View.CheckOnClick = true;
            this.EquipmentList_View.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EquipmentList_View.Name = "EquipmentList_View";
            this.EquipmentList_View.Size = new System.Drawing.Size(124, 22);
            this.EquipmentList_View.Text = "设备列表";
            this.EquipmentList_View.Click += new System.EventHandler(this.EquipmentList_View_Click);
            // 
            // Log_View
            // 
            this.Log_View.Checked = true;
            this.Log_View.CheckOnClick = true;
            this.Log_View.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Log_View.Name = "Log_View";
            this.Log_View.Size = new System.Drawing.Size(124, 22);
            this.Log_View.Text = "日志";
            this.Log_View.Click += new System.EventHandler(this.Log_View_Click);
            // 
            // QuickStart_View
            // 
            this.QuickStart_View.Checked = true;
            this.QuickStart_View.CheckOnClick = true;
            this.QuickStart_View.CheckState = System.Windows.Forms.CheckState.Checked;
            this.QuickStart_View.Name = "QuickStart_View";
            this.QuickStart_View.Size = new System.Drawing.Size(124, 22);
            this.QuickStart_View.Text = "快速启动";
            this.QuickStart_View.Click += new System.EventHandler(this.QuickStart_View_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // Status_Main
            // 
            this.Status_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
            this.Status_Main.Location = new System.Drawing.Point(0, 860);
            this.Status_Main.Name = "Status_Main";
            this.Status_Main.Size = new System.Drawing.Size(1264, 22);
            this.Status_Main.TabIndex = 6;
            this.Status_Main.Text = "statusStrip1";
            this.Status_Main.Visible = false;
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // PrintReportDialog
            // 
            this.PrintReportDialog.UseEXDialog = true;
            // 
            // PrintReportPreviewDialog
            // 
            this.PrintReportPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintReportPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintReportPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintReportPreviewDialog.Enabled = true;
            this.PrintReportPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintReportPreviewDialog.Icon")));
            this.PrintReportPreviewDialog.Name = "PrintReportPreviewDialog";
            this.PrintReportPreviewDialog.Visible = false;
            // 
            // PrintReportDocument
            // 
            this.PrintReportDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintReportDocument_PrintPage);
            // 
            // Register_Fresher
            // 
            this.Register_Fresher.Interval = 1000;
            this.Register_Fresher.Tick += new System.EventHandler(this.Register_Fresher_Tick);
            // 
            // DogSerialNo
            // 
            this.DogSerialNo.AutoSize = true;
            this.DogSerialNo.Location = new System.Drawing.Point(366, 212);
            this.DogSerialNo.Name = "DogSerialNo";
            this.DogSerialNo.Size = new System.Drawing.Size(239, 12);
            this.DogSerialNo.TabIndex = 8;
            this.DogSerialNo.Text = "WorkStation was Compiled In Jun.30,2017";
            this.DogSerialNo.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 882);
            this.Controls.Add(this.DogSerialNo);
            this.Controls.Add(this.Dock_Main);
            this.Controls.Add(this.Status_Main);
            this.Controls.Add(this.Menu_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.Menu_Main;
            this.MinimumSize = new System.Drawing.Size(1278, 858);
            this.Name = "FrmMain";
            this.Text = "色谱工作站 V2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Menu_Main.ResumeLayout(false);
            this.Menu_Main.PerformLayout();
            this.Status_Main.ResumeLayout(false);
            this.Status_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel Dock_Main;
        private System.Windows.Forms.MenuStrip Menu_Main;
        private System.Windows.Forms.ToolStripMenuItem Report_View;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Print_Report;
        private System.Windows.Forms.StatusStrip Status_Main;
        private System.Windows.Forms.ToolStripStatusLabel StatusBar;
        private System.Windows.Forms.ToolStripMenuItem View_Menu;
        private System.Windows.Forms.ToolStripMenuItem reLoad_View;
        private System.Windows.Forms.ToolStripMenuItem reSet_View;
        private System.Windows.Forms.PrintDialog PrintReportDialog;
        private System.Windows.Forms.ToolStripMenuItem PrintPreview_Report;
        private System.Windows.Forms.PrintPreviewDialog PrintReportPreviewDialog;
        private System.Drawing.Printing.PrintDocument PrintReportDocument;
        private System.Windows.Forms.ToolStripMenuItem Output_Report;
        private System.Windows.Forms.ToolStripMenuItem Modify_Template;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem EquipmentList_View;
        private System.Windows.Forms.ToolStripMenuItem Log_View;
        private System.Windows.Forms.ToolStripMenuItem QuickStart_View;
        private System.Windows.Forms.Timer Register_Fresher;
        private System.Windows.Forms.SaveFileDialog Report_Saver;
        private System.Windows.Forms.Label DogSerialNo;
    }
}

