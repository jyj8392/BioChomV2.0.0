namespace BioChome
{
    partial class FrmRight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRight));
            this.EquipmentText_Information = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuickStart = new System.Windows.Forms.Button();
            this.ProgramList_CheckBox = new System.Windows.Forms.CheckedListBox();
            this.ProgramList_Label = new System.Windows.Forms.Label();
            this.QuickStop = new System.Windows.Forms.Button();
            this.EquipState_Fresher = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Method_List = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Method_Browser = new System.Windows.Forms.ListBox();
            this.UVMethod_Panel = new System.Windows.Forms.Panel();
            this.MethodSnapShot_List = new System.Windows.Forms.ToolStrip();
            this.Method_Del = new System.Windows.Forms.ToolStripButton();
            this.Method_Save = new System.Windows.Forms.ToolStripButton();
            this.Method_New = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.QuickPause = new System.Windows.Forms.Button();
            this.SaveTIme_Method = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EquipmentText_Information)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.UVMethod_Panel.SuspendLayout();
            this.MethodSnapShot_List.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // EquipmentText_Information
            // 
            this.EquipmentText_Information.AllowUserToAddRows = false;
            this.EquipmentText_Information.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.EquipmentText_Information.BackgroundColor = System.Drawing.SystemColors.Control;
            this.EquipmentText_Information.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EquipmentText_Information.ColumnHeadersVisible = false;
            this.EquipmentText_Information.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.EquipmentText_Information.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EquipmentText_Information.Location = new System.Drawing.Point(3, 417);
            this.EquipmentText_Information.Name = "EquipmentText_Information";
            this.EquipmentText_Information.RowHeadersVisible = false;
            this.EquipmentText_Information.RowTemplate.Height = 23;
            this.EquipmentText_Information.Size = new System.Drawing.Size(116, 144);
            this.EquipmentText_Information.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // QuickStart
            // 
            this.QuickStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuickStart.Location = new System.Drawing.Point(3, 3);
            this.QuickStart.Name = "QuickStart";
            this.QuickStart.Size = new System.Drawing.Size(110, 26);
            this.QuickStart.TabIndex = 2;
            this.QuickStart.Text = "一键启动";
            this.QuickStart.UseVisualStyleBackColor = true;
            this.QuickStart.Click += new System.EventHandler(this.QuickStart_Click);
            // 
            // ProgramList_CheckBox
            // 
            this.ProgramList_CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramList_CheckBox.FormattingEnabled = true;
            this.ProgramList_CheckBox.Items.AddRange(new object[] {
            "紫外检测器",
            "馏分收集器",
            "泵（恒流）",
            "泵（梯度）"});
            this.ProgramList_CheckBox.Location = new System.Drawing.Point(0, 15);
            this.ProgramList_CheckBox.Name = "ProgramList_CheckBox";
            this.ProgramList_CheckBox.Size = new System.Drawing.Size(116, 84);
            this.ProgramList_CheckBox.TabIndex = 6;
            this.ProgramList_CheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ProgramList_CheckBox_ItemCheck);
            this.ProgramList_CheckBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProgramList_CheckBox_MouseMove);
            // 
            // ProgramList_Label
            // 
            this.ProgramList_Label.AutoSize = true;
            this.ProgramList_Label.Location = new System.Drawing.Point(3, 0);
            this.ProgramList_Label.Name = "ProgramList_Label";
            this.ProgramList_Label.Size = new System.Drawing.Size(53, 12);
            this.ProgramList_Label.TabIndex = 7;
            this.ProgramList_Label.Text = "程序列表";
            // 
            // QuickStop
            // 
            this.QuickStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuickStop.Enabled = false;
            this.QuickStop.Location = new System.Drawing.Point(3, 67);
            this.QuickStop.Name = "QuickStop";
            this.QuickStop.Size = new System.Drawing.Size(110, 26);
            this.QuickStop.TabIndex = 9;
            this.QuickStop.Text = "一键停止";
            this.QuickStop.UseVisualStyleBackColor = true;
            this.QuickStop.Click += new System.EventHandler(this.QuickStop_Click);
            // 
            // EquipState_Fresher
            // 
            this.EquipState_Fresher.Enabled = true;
            this.EquipState_Fresher.Interval = 300;
            this.EquipState_Fresher.Tick += new System.EventHandler(this.EquipState_Fresher_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.EquipmentText_Information, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(122, 564);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Method_List);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 208);
            this.panel1.TabIndex = 13;
            // 
            // Method_List
            // 
            this.Method_List.AutoSize = true;
            this.Method_List.Location = new System.Drawing.Point(3, 0);
            this.Method_List.Name = "Method_List";
            this.Method_List.Size = new System.Drawing.Size(53, 12);
            this.Method_List.TabIndex = 13;
            this.Method_List.Text = "方法列表";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.Method_Browser);
            this.panel4.Controls.Add(this.SaveTIme_Method);
            this.panel4.Controls.Add(this.UVMethod_Panel);
            this.panel4.Location = new System.Drawing.Point(0, 15);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(116, 193);
            this.panel4.TabIndex = 14;
            // 
            // Method_Browser
            // 
            this.Method_Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Method_Browser.FormattingEnabled = true;
            this.Method_Browser.ItemHeight = 12;
            this.Method_Browser.Location = new System.Drawing.Point(0, 25);
            this.Method_Browser.Name = "Method_Browser";
            this.Method_Browser.Size = new System.Drawing.Size(116, 151);
            this.Method_Browser.TabIndex = 0;
            this.Method_Browser.Click += new System.EventHandler(this.Method_Browser_Click);
            this.Method_Browser.DoubleClick += new System.EventHandler(this.Method_Browser_DoubleClick);
            this.Method_Browser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Method_Browser_KeyDown);
            // 
            // UVMethod_Panel
            // 
            this.UVMethod_Panel.Controls.Add(this.MethodSnapShot_List);
            this.UVMethod_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UVMethod_Panel.Location = new System.Drawing.Point(0, 0);
            this.UVMethod_Panel.Name = "UVMethod_Panel";
            this.UVMethod_Panel.Size = new System.Drawing.Size(116, 25);
            this.UVMethod_Panel.TabIndex = 3;
            // 
            // MethodSnapShot_List
            // 
            this.MethodSnapShot_List.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Method_Del,
            this.Method_Save,
            this.Method_New});
            this.MethodSnapShot_List.Location = new System.Drawing.Point(0, 0);
            this.MethodSnapShot_List.Name = "MethodSnapShot_List";
            this.MethodSnapShot_List.Size = new System.Drawing.Size(116, 25);
            this.MethodSnapShot_List.TabIndex = 0;
            this.MethodSnapShot_List.Text = "UVToolBar_Set";
            // 
            // Method_Del
            // 
            this.Method_Del.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Method_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Method_Del.Image = ((System.Drawing.Image)(resources.GetObject("Method_Del.Image")));
            this.Method_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Method_Del.Name = "Method_Del";
            this.Method_Del.Size = new System.Drawing.Size(23, 22);
            this.Method_Del.Text = "删除方法";
            this.Method_Del.Click += new System.EventHandler(this.Method_Del_Click);
            // 
            // Method_Save
            // 
            this.Method_Save.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Method_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Method_Save.Image = ((System.Drawing.Image)(resources.GetObject("Method_Save.Image")));
            this.Method_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Method_Save.Name = "Method_Save";
            this.Method_Save.Size = new System.Drawing.Size(23, 22);
            this.Method_Save.Text = "保存方法";
            this.Method_Save.Click += new System.EventHandler(this.Method_Save_Click);
            // 
            // Method_New
            // 
            this.Method_New.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Method_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Method_New.Image = ((System.Drawing.Image)(resources.GetObject("Method_New.Image")));
            this.Method_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Method_New.Name = "Method_New";
            this.Method_New.Size = new System.Drawing.Size(23, 22);
            this.Method_New.Text = "新建方法";
            this.Method_New.Click += new System.EventHandler(this.Method_New_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ProgramList_CheckBox);
            this.panel2.Controls.Add(this.ProgramList_Label);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(116, 94);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.QuickStop);
            this.panel3.Controls.Add(this.QuickPause);
            this.panel3.Controls.Add(this.QuickStart);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(116, 94);
            this.panel3.TabIndex = 12;
            // 
            // QuickPause
            // 
            this.QuickPause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QuickPause.Enabled = false;
            this.QuickPause.Location = new System.Drawing.Point(3, 35);
            this.QuickPause.Name = "QuickPause";
            this.QuickPause.Size = new System.Drawing.Size(110, 26);
            this.QuickPause.TabIndex = 8;
            this.QuickPause.Text = "一键暂停";
            this.QuickPause.UseVisualStyleBackColor = true;
            this.QuickPause.Click += new System.EventHandler(this.QuickPause_Click);
            // 
            // SaveTIme_Method
            // 
            this.SaveTIme_Method.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SaveTIme_Method.Location = new System.Drawing.Point(0, 176);
            this.SaveTIme_Method.Name = "SaveTIme_Method";
            this.SaveTIme_Method.Size = new System.Drawing.Size(116, 17);
            this.SaveTIme_Method.TabIndex = 4;
            // 
            // FrmRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(122, 564);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmRight";
            this.TabText = "快速启动";
            this.Text = "FrmRight";
            this.DockStateChanged += new System.EventHandler(this.FrmRight_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRight_FormClosing);
            this.Load += new System.EventHandler(this.FrmRight_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EquipmentText_Information)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.UVMethod_Panel.ResumeLayout(false);
            this.UVMethod_Panel.PerformLayout();
            this.MethodSnapShot_List.ResumeLayout(false);
            this.MethodSnapShot_List.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView EquipmentText_Information;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button QuickStart;
        private System.Windows.Forms.CheckedListBox ProgramList_CheckBox;
        private System.Windows.Forms.Label ProgramList_Label;
        private System.Windows.Forms.Button QuickStop;
        private System.Windows.Forms.Timer EquipState_Fresher;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button QuickPause;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Method_List;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel UVMethod_Panel;
        private System.Windows.Forms.ToolStrip MethodSnapShot_List;
        private System.Windows.Forms.ToolStripButton Method_Del;
        private System.Windows.Forms.ToolStripButton Method_Save;
        private System.Windows.Forms.ToolStripButton Method_New;
        private System.Windows.Forms.ListBox Method_Browser;
        private System.Windows.Forms.Label SaveTIme_Method;
    }
}