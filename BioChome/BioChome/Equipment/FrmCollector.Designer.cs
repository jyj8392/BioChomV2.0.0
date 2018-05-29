namespace BioChome
{
    partial class FrmCollector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCollector));
            this.Buttles_Panel = new System.Windows.Forms.Panel();
            this.Buttles_Area = new System.Windows.Forms.Panel();
            this.Buttles_ToolBar = new System.Windows.Forms.ToolStrip();
            this.GoToLastButtle = new System.Windows.Forms.ToolStripButton();
            this.GoToNextButtle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.StartButtleNo_Label = new System.Windows.Forms.ToolStripLabel();
            this.StartButtleNo_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.StopButtleNo_Label = new System.Windows.Forms.ToolStripLabel();
            this.StopButtleNo_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.CurrentButtleNo_Label = new System.Windows.Forms.ToolStripLabel();
            this.HideCollectorProgramPanel = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DelayVolumeUnit_Label = new System.Windows.Forms.Label();
            this.DelayVolume = new System.Windows.Forms.Label();
            this.PipeDiameterUnit_Label = new System.Windows.Forms.Label();
            this.PipeLengthUnit_Label = new System.Windows.Forms.Label();
            this.ButtleVolumeUnit_Label = new System.Windows.Forms.Label();
            this.PipeDiameter_TextBox = new System.Windows.Forms.TextBox();
            this.PipeLength_TextBox = new System.Windows.Forms.TextBox();
            this.PipeDiameter_Label = new System.Windows.Forms.Label();
            this.PipeLength_Label = new System.Windows.Forms.Label();
            this.ButtleVolume_TextBox = new System.Windows.Forms.TextBox();
            this.ButtleVolume_Label = new System.Windows.Forms.Label();
            this.DelayVolume_Label = new System.Windows.Forms.Label();
            this.CollectorProgram_Panel = new System.Windows.Forms.Panel();
            this.CollectorProgram_ToolBar = new System.Windows.Forms.ToolStrip();
            this.StartCollectorProgram = new System.Windows.Forms.ToolStripButton();
            this.StopCollectorProgram = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CollectorProgram_Add = new System.Windows.Forms.ToolStripButton();
            this.CollectorProgram_Insert = new System.Windows.Forms.ToolStripButton();
            this.CollectorProgram_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CollectorProgram_MinPeakWidth_Label = new System.Windows.Forms.ToolStripLabel();
            this.CollectorProgram_MinPeakWidth_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.CollectorProgram_MinPeakWidthUnit_Label = new System.Windows.Forms.ToolStripLabel();
            this.HideButtlesPanel = new System.Windows.Forms.ToolStripButton();
            this.CollectorProgramChannel_Menu = new System.Windows.Forms.ToolStripDropDownButton();
            this.CollectorProgramChannel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CollectorProgramChannel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.CollectorProgramChannel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.CollectorProgram_Grid = new System.Windows.Forms.DataGridView();
            this.StartFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartSlope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StopTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopSlope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectorMethod_Panel = new System.Windows.Forms.Panel();
            this.CollectorMethod_Browser = new System.Windows.Forms.ListBox();
            this.CollectorMethod_List = new System.Windows.Forms.ToolStrip();
            this.CollectorMethod_Del = new System.Windows.Forms.ToolStripButton();
            this.CollectorMethod_Save = new System.Windows.Forms.ToolStripButton();
            this.CollectorMethod_New = new System.Windows.Forms.ToolStripButton();
            this.Method_Title = new System.Windows.Forms.ToolStripLabel();
            this.CollectSet_Panel = new System.Windows.Forms.TableLayoutPanel();
            this.CollectorButtles_Fresher = new System.Windows.Forms.Timer(this.components);
            this.Buttles_Panel.SuspendLayout();
            this.Buttles_ToolBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.CollectorProgram_Panel.SuspendLayout();
            this.CollectorProgram_ToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CollectorProgram_Grid)).BeginInit();
            this.CollectorMethod_Panel.SuspendLayout();
            this.CollectorMethod_List.SuspendLayout();
            this.CollectSet_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Buttles_Panel
            // 
            this.Buttles_Panel.Controls.Add(this.Buttles_Area);
            this.Buttles_Panel.Controls.Add(this.Buttles_ToolBar);
            this.Buttles_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Buttles_Panel.Location = new System.Drawing.Point(689, 3);
            this.Buttles_Panel.Name = "Buttles_Panel";
            this.Buttles_Panel.Size = new System.Drawing.Size(421, 192);
            this.Buttles_Panel.TabIndex = 0;
            // 
            // Buttles_Area
            // 
            this.Buttles_Area.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Buttles_Area.Location = new System.Drawing.Point(3, 28);
            this.Buttles_Area.Name = "Buttles_Area";
            this.Buttles_Area.Size = new System.Drawing.Size(415, 161);
            this.Buttles_Area.TabIndex = 1;
            // 
            // Buttles_ToolBar
            // 
            this.Buttles_ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GoToLastButtle,
            this.GoToNextButtle,
            this.toolStripSeparator2,
            this.StartButtleNo_Label,
            this.StartButtleNo_TextBox,
            this.StopButtleNo_Label,
            this.StopButtleNo_TextBox,
            this.CurrentButtleNo_Label,
            this.HideCollectorProgramPanel});
            this.Buttles_ToolBar.Location = new System.Drawing.Point(0, 0);
            this.Buttles_ToolBar.Name = "Buttles_ToolBar";
            this.Buttles_ToolBar.Size = new System.Drawing.Size(421, 25);
            this.Buttles_ToolBar.TabIndex = 0;
            this.Buttles_ToolBar.Text = "toolStrip1";
            // 
            // GoToLastButtle
            // 
            this.GoToLastButtle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GoToLastButtle.Image = ((System.Drawing.Image)(resources.GetObject("GoToLastButtle.Image")));
            this.GoToLastButtle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GoToLastButtle.Name = "GoToLastButtle";
            this.GoToLastButtle.Size = new System.Drawing.Size(23, 22);
            this.GoToLastButtle.Text = "上一瓶";
            this.GoToLastButtle.Click += new System.EventHandler(this.GoToLastButtle_Click);
            // 
            // GoToNextButtle
            // 
            this.GoToNextButtle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GoToNextButtle.Image = ((System.Drawing.Image)(resources.GetObject("GoToNextButtle.Image")));
            this.GoToNextButtle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GoToNextButtle.Name = "GoToNextButtle";
            this.GoToNextButtle.Size = new System.Drawing.Size(23, 22);
            this.GoToNextButtle.Text = "下一瓶";
            this.GoToNextButtle.Click += new System.EventHandler(this.GoToNextButtle_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // StartButtleNo_Label
            // 
            this.StartButtleNo_Label.Name = "StartButtleNo_Label";
            this.StartButtleNo_Label.Size = new System.Drawing.Size(68, 22);
            this.StartButtleNo_Label.Text = "开始瓶号：";
            // 
            // StartButtleNo_TextBox
            // 
            this.StartButtleNo_TextBox.Name = "StartButtleNo_TextBox";
            this.StartButtleNo_TextBox.Size = new System.Drawing.Size(44, 25);
            this.StartButtleNo_TextBox.Text = "10";
            this.StartButtleNo_TextBox.TextChanged += new System.EventHandler(this.StartButtleNo_TextBox_TextChanged);
            // 
            // StopButtleNo_Label
            // 
            this.StopButtleNo_Label.Name = "StopButtleNo_Label";
            this.StopButtleNo_Label.Size = new System.Drawing.Size(68, 22);
            this.StopButtleNo_Label.Text = "结束瓶号：";
            // 
            // StopButtleNo_TextBox
            // 
            this.StopButtleNo_TextBox.Name = "StopButtleNo_TextBox";
            this.StopButtleNo_TextBox.Size = new System.Drawing.Size(44, 25);
            this.StopButtleNo_TextBox.Text = "120";
            this.StopButtleNo_TextBox.TextChanged += new System.EventHandler(this.StopButtleNo_TextBox_TextChanged);
            // 
            // CurrentButtleNo_Label
            // 
            this.CurrentButtleNo_Label.Name = "CurrentButtleNo_Label";
            this.CurrentButtleNo_Label.Size = new System.Drawing.Size(68, 22);
            this.CurrentButtleNo_Label.Text = "当前瓶号：";
            // 
            // HideCollectorProgramPanel
            // 
            this.HideCollectorProgramPanel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HideCollectorProgramPanel.AutoSize = false;
            this.HideCollectorProgramPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HideCollectorProgramPanel.Image = ((System.Drawing.Image)(resources.GetObject("HideCollectorProgramPanel.Image")));
            this.HideCollectorProgramPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HideCollectorProgramPanel.Name = "HideCollectorProgramPanel";
            this.HideCollectorProgramPanel.Size = new System.Drawing.Size(15, 22);
            this.HideCollectorProgramPanel.Text = "toolStripButton1";
            this.HideCollectorProgramPanel.Click += new System.EventHandler(this.HideCollectorProgramPanel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DelayVolumeUnit_Label);
            this.groupBox1.Controls.Add(this.DelayVolume);
            this.groupBox1.Controls.Add(this.PipeDiameterUnit_Label);
            this.groupBox1.Controls.Add(this.PipeLengthUnit_Label);
            this.groupBox1.Controls.Add(this.ButtleVolumeUnit_Label);
            this.groupBox1.Controls.Add(this.PipeDiameter_TextBox);
            this.groupBox1.Controls.Add(this.PipeLength_TextBox);
            this.groupBox1.Controls.Add(this.PipeDiameter_Label);
            this.groupBox1.Controls.Add(this.PipeLength_Label);
            this.groupBox1.Controls.Add(this.ButtleVolume_TextBox);
            this.groupBox1.Controls.Add(this.ButtleVolume_Label);
            this.groupBox1.Controls.Add(this.DelayVolume_Label);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 192);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // DelayVolumeUnit_Label
            // 
            this.DelayVolumeUnit_Label.AutoSize = true;
            this.DelayVolumeUnit_Label.Location = new System.Drawing.Point(135, 42);
            this.DelayVolumeUnit_Label.Name = "DelayVolumeUnit_Label";
            this.DelayVolumeUnit_Label.Size = new System.Drawing.Size(17, 12);
            this.DelayVolumeUnit_Label.TabIndex = 15;
            this.DelayVolumeUnit_Label.Text = "ml";
            // 
            // DelayVolume
            // 
            this.DelayVolume.AutoSize = true;
            this.DelayVolume.Location = new System.Drawing.Point(87, 42);
            this.DelayVolume.Name = "DelayVolume";
            this.DelayVolume.Size = new System.Drawing.Size(11, 12);
            this.DelayVolume.TabIndex = 14;
            this.DelayVolume.Text = "1";
            // 
            // PipeDiameterUnit_Label
            // 
            this.PipeDiameterUnit_Label.AutoSize = true;
            this.PipeDiameterUnit_Label.Location = new System.Drawing.Point(135, 92);
            this.PipeDiameterUnit_Label.Name = "PipeDiameterUnit_Label";
            this.PipeDiameterUnit_Label.Size = new System.Drawing.Size(17, 12);
            this.PipeDiameterUnit_Label.TabIndex = 13;
            this.PipeDiameterUnit_Label.Text = "mm";
            // 
            // PipeLengthUnit_Label
            // 
            this.PipeLengthUnit_Label.AutoSize = true;
            this.PipeLengthUnit_Label.Location = new System.Drawing.Point(135, 65);
            this.PipeLengthUnit_Label.Name = "PipeLengthUnit_Label";
            this.PipeLengthUnit_Label.Size = new System.Drawing.Size(17, 12);
            this.PipeLengthUnit_Label.TabIndex = 12;
            this.PipeLengthUnit_Label.Text = "mm";
            // 
            // ButtleVolumeUnit_Label
            // 
            this.ButtleVolumeUnit_Label.AutoSize = true;
            this.ButtleVolumeUnit_Label.Location = new System.Drawing.Point(135, 17);
            this.ButtleVolumeUnit_Label.Name = "ButtleVolumeUnit_Label";
            this.ButtleVolumeUnit_Label.Size = new System.Drawing.Size(17, 12);
            this.ButtleVolumeUnit_Label.TabIndex = 11;
            this.ButtleVolumeUnit_Label.Text = "ml";
            // 
            // PipeDiameter_TextBox
            // 
            this.PipeDiameter_TextBox.Location = new System.Drawing.Point(85, 89);
            this.PipeDiameter_TextBox.Name = "PipeDiameter_TextBox";
            this.PipeDiameter_TextBox.Size = new System.Drawing.Size(44, 21);
            this.PipeDiameter_TextBox.TabIndex = 10;
            this.PipeDiameter_TextBox.TextChanged += new System.EventHandler(this.PipeDiameter_TextBox_TextChanged);
            // 
            // PipeLength_TextBox
            // 
            this.PipeLength_TextBox.Location = new System.Drawing.Point(85, 62);
            this.PipeLength_TextBox.Name = "PipeLength_TextBox";
            this.PipeLength_TextBox.Size = new System.Drawing.Size(44, 21);
            this.PipeLength_TextBox.TabIndex = 9;
            this.PipeLength_TextBox.TextChanged += new System.EventHandler(this.PipeLength_TextBox_TextChanged);
            // 
            // PipeDiameter_Label
            // 
            this.PipeDiameter_Label.AutoSize = true;
            this.PipeDiameter_Label.Location = new System.Drawing.Point(12, 92);
            this.PipeDiameter_Label.Name = "PipeDiameter_Label";
            this.PipeDiameter_Label.Size = new System.Drawing.Size(41, 12);
            this.PipeDiameter_Label.TabIndex = 8;
            this.PipeDiameter_Label.Text = "管径：";
            // 
            // PipeLength_Label
            // 
            this.PipeLength_Label.AutoSize = true;
            this.PipeLength_Label.Location = new System.Drawing.Point(12, 65);
            this.PipeLength_Label.Name = "PipeLength_Label";
            this.PipeLength_Label.Size = new System.Drawing.Size(41, 12);
            this.PipeLength_Label.TabIndex = 7;
            this.PipeLength_Label.Text = "管长：";
            // 
            // ButtleVolume_TextBox
            // 
            this.ButtleVolume_TextBox.Location = new System.Drawing.Point(85, 12);
            this.ButtleVolume_TextBox.Name = "ButtleVolume_TextBox";
            this.ButtleVolume_TextBox.Size = new System.Drawing.Size(44, 21);
            this.ButtleVolume_TextBox.TabIndex = 6;
            this.ButtleVolume_TextBox.Text = "10.00";
            // 
            // ButtleVolume_Label
            // 
            this.ButtleVolume_Label.AutoSize = true;
            this.ButtleVolume_Label.Location = new System.Drawing.Point(12, 17);
            this.ButtleVolume_Label.Name = "ButtleVolume_Label";
            this.ButtleVolume_Label.Size = new System.Drawing.Size(77, 12);
            this.ButtleVolume_Label.TabIndex = 5;
            this.ButtleVolume_Label.Text = "收集瓶容量：";
            // 
            // DelayVolume_Label
            // 
            this.DelayVolume_Label.AutoSize = true;
            this.DelayVolume_Label.Location = new System.Drawing.Point(12, 42);
            this.DelayVolume_Label.Name = "DelayVolume_Label";
            this.DelayVolume_Label.Size = new System.Drawing.Size(65, 12);
            this.DelayVolume_Label.TabIndex = 0;
            this.DelayVolume_Label.Text = "延迟体积：";
            // 
            // CollectorProgram_Panel
            // 
            this.CollectorProgram_Panel.Controls.Add(this.CollectorProgram_ToolBar);
            this.CollectorProgram_Panel.Controls.Add(this.CollectorProgram_Grid);
            this.CollectorProgram_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CollectorProgram_Panel.Location = new System.Drawing.Point(159, 3);
            this.CollectorProgram_Panel.MinimumSize = new System.Drawing.Size(213, 0);
            this.CollectorProgram_Panel.Name = "CollectorProgram_Panel";
            this.CollectorProgram_Panel.Size = new System.Drawing.Size(524, 192);
            this.CollectorProgram_Panel.TabIndex = 7;
            // 
            // CollectorProgram_ToolBar
            // 
            this.CollectorProgram_ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartCollectorProgram,
            this.StopCollectorProgram,
            this.toolStripSeparator3,
            this.CollectorProgram_Add,
            this.CollectorProgram_Insert,
            this.CollectorProgram_Del,
            this.toolStripSeparator1,
            this.CollectorProgram_MinPeakWidth_Label,
            this.CollectorProgram_MinPeakWidth_TextBox,
            this.CollectorProgram_MinPeakWidthUnit_Label,
            this.HideButtlesPanel,
            this.CollectorProgramChannel_Menu});
            this.CollectorProgram_ToolBar.Location = new System.Drawing.Point(0, 0);
            this.CollectorProgram_ToolBar.Name = "CollectorProgram_ToolBar";
            this.CollectorProgram_ToolBar.Size = new System.Drawing.Size(524, 25);
            this.CollectorProgram_ToolBar.TabIndex = 5;
            this.CollectorProgram_ToolBar.Text = "toolStrip1";
            // 
            // StartCollectorProgram
            // 
            this.StartCollectorProgram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartCollectorProgram.Image = ((System.Drawing.Image)(resources.GetObject("StartCollectorProgram.Image")));
            this.StartCollectorProgram.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartCollectorProgram.Name = "StartCollectorProgram";
            this.StartCollectorProgram.Size = new System.Drawing.Size(23, 22);
            this.StartCollectorProgram.Text = "运行";
            this.StartCollectorProgram.Click += new System.EventHandler(this.RunCollectorProgram_Click);
            // 
            // StopCollectorProgram
            // 
            this.StopCollectorProgram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopCollectorProgram.Image = ((System.Drawing.Image)(resources.GetObject("StopCollectorProgram.Image")));
            this.StopCollectorProgram.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopCollectorProgram.Name = "StopCollectorProgram";
            this.StopCollectorProgram.Size = new System.Drawing.Size(23, 22);
            this.StopCollectorProgram.Text = "停止";
            this.StopCollectorProgram.Click += new System.EventHandler(this.StopCollectorProgram_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // CollectorProgram_Add
            // 
            this.CollectorProgram_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorProgram_Add.Image = ((System.Drawing.Image)(resources.GetObject("CollectorProgram_Add.Image")));
            this.CollectorProgram_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorProgram_Add.Name = "CollectorProgram_Add";
            this.CollectorProgram_Add.Size = new System.Drawing.Size(23, 22);
            this.CollectorProgram_Add.Text = "添加";
            this.CollectorProgram_Add.ToolTipText = "添加";
            this.CollectorProgram_Add.Click += new System.EventHandler(this.CollectorProgram_Add_Click);
            // 
            // CollectorProgram_Insert
            // 
            this.CollectorProgram_Insert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorProgram_Insert.Image = ((System.Drawing.Image)(resources.GetObject("CollectorProgram_Insert.Image")));
            this.CollectorProgram_Insert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorProgram_Insert.Name = "CollectorProgram_Insert";
            this.CollectorProgram_Insert.Size = new System.Drawing.Size(23, 22);
            this.CollectorProgram_Insert.Text = "插入";
            this.CollectorProgram_Insert.Click += new System.EventHandler(this.CollectorProgram_Insert_Click);
            // 
            // CollectorProgram_Del
            // 
            this.CollectorProgram_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorProgram_Del.Image = ((System.Drawing.Image)(resources.GetObject("CollectorProgram_Del.Image")));
            this.CollectorProgram_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorProgram_Del.Name = "CollectorProgram_Del";
            this.CollectorProgram_Del.Size = new System.Drawing.Size(23, 22);
            this.CollectorProgram_Del.Text = "删除";
            this.CollectorProgram_Del.Click += new System.EventHandler(this.CollectorProgram_Del_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CollectorProgram_MinPeakWidth_Label
            // 
            this.CollectorProgram_MinPeakWidth_Label.Name = "CollectorProgram_MinPeakWidth_Label";
            this.CollectorProgram_MinPeakWidth_Label.Size = new System.Drawing.Size(68, 22);
            this.CollectorProgram_MinPeakWidth_Label.Text = "最小峰宽：";
            // 
            // CollectorProgram_MinPeakWidth_TextBox
            // 
            this.CollectorProgram_MinPeakWidth_TextBox.Name = "CollectorProgram_MinPeakWidth_TextBox";
            this.CollectorProgram_MinPeakWidth_TextBox.Size = new System.Drawing.Size(44, 25);
            // 
            // CollectorProgram_MinPeakWidthUnit_Label
            // 
            this.CollectorProgram_MinPeakWidthUnit_Label.Name = "CollectorProgram_MinPeakWidthUnit_Label";
            this.CollectorProgram_MinPeakWidthUnit_Label.Size = new System.Drawing.Size(14, 22);
            this.CollectorProgram_MinPeakWidthUnit_Label.Text = "s";
            // 
            // HideButtlesPanel
            // 
            this.HideButtlesPanel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HideButtlesPanel.AutoSize = false;
            this.HideButtlesPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HideButtlesPanel.Image = ((System.Drawing.Image)(resources.GetObject("HideButtlesPanel.Image")));
            this.HideButtlesPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HideButtlesPanel.Name = "HideButtlesPanel";
            this.HideButtlesPanel.Size = new System.Drawing.Size(15, 22);
            this.HideButtlesPanel.Text = "toolStripButton1";
            this.HideButtlesPanel.Click += new System.EventHandler(this.HideButtlesPanel_Click);
            // 
            // CollectorProgramChannel_Menu
            // 
            this.CollectorProgramChannel_Menu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CollectorProgramChannel_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CollectorProgramChannel1,
            this.CollectorProgramChannel2,
            this.CollectorProgramChannel3});
            this.CollectorProgramChannel_Menu.Image = ((System.Drawing.Image)(resources.GetObject("CollectorProgramChannel_Menu.Image")));
            this.CollectorProgramChannel_Menu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorProgramChannel_Menu.Name = "CollectorProgramChannel_Menu";
            this.CollectorProgramChannel_Menu.Size = new System.Drawing.Size(81, 22);
            this.CollectorProgramChannel_Menu.Text = "检测器信号";
            // 
            // CollectorProgramChannel1
            // 
            this.CollectorProgramChannel1.CheckOnClick = true;
            this.CollectorProgramChannel1.Name = "CollectorProgramChannel1";
            this.CollectorProgramChannel1.Size = new System.Drawing.Size(107, 22);
            this.CollectorProgramChannel1.Text = "波长1";
            // 
            // CollectorProgramChannel2
            // 
            this.CollectorProgramChannel2.CheckOnClick = true;
            this.CollectorProgramChannel2.Name = "CollectorProgramChannel2";
            this.CollectorProgramChannel2.Size = new System.Drawing.Size(107, 22);
            this.CollectorProgramChannel2.Text = "波长2";
            // 
            // CollectorProgramChannel3
            // 
            this.CollectorProgramChannel3.CheckOnClick = true;
            this.CollectorProgramChannel3.Name = "CollectorProgramChannel3";
            this.CollectorProgramChannel3.Size = new System.Drawing.Size(107, 22);
            this.CollectorProgramChannel3.Text = "波长3";
            // 
            // CollectorProgram_Grid
            // 
            this.CollectorProgram_Grid.AllowUserToAddRows = false;
            this.CollectorProgram_Grid.AllowUserToDeleteRows = false;
            this.CollectorProgram_Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CollectorProgram_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CollectorProgram_Grid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CollectorProgram_Grid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CollectorProgram_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CollectorProgram_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartFilter,
            this.StartTime,
            this.StartThreshold,
            this.StartSlope,
            this.StopFilter,
            this.StopTime,
            this.StopThreshold,
            this.StopSlope,
            this.Volume,
            this.MaxTime});
            this.CollectorProgram_Grid.Location = new System.Drawing.Point(0, 25);
            this.CollectorProgram_Grid.MultiSelect = false;
            this.CollectorProgram_Grid.Name = "CollectorProgram_Grid";
            this.CollectorProgram_Grid.RowHeadersWidth = 48;
            this.CollectorProgram_Grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CollectorProgram_Grid.RowTemplate.Height = 23;
            this.CollectorProgram_Grid.Size = new System.Drawing.Size(521, 167);
            this.CollectorProgram_Grid.TabIndex = 4;
            this.CollectorProgram_Grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CollectorProgram_Grid_CellValueChanged);
            this.CollectorProgram_Grid.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.CollectorProgram_Grid_RowStateChanged);
            this.CollectorProgram_Grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CollectorProgram_Grid_MouseMove);
            // 
            // StartFilter
            // 
            this.StartFilter.FillWeight = 64F;
            this.StartFilter.HeaderText = "开始条件";
            this.StartFilter.Items.AddRange(new object[] {
            "时间",
            "阈值",
            "斜率",
            "时间&阈值",
            "时间&阈值&斜率",
            "时间&(阈值|斜率)"});
            this.StartFilter.Name = "StartFilter";
            this.StartFilter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // StartTime
            // 
            this.StartTime.FillWeight = 64F;
            this.StartTime.HeaderText = "开始时间(min)";
            this.StartTime.Name = "StartTime";
            this.StartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartThreshold
            // 
            this.StartThreshold.FillWeight = 64F;
            this.StartThreshold.HeaderText = "开始阈值(mAu)";
            this.StartThreshold.Name = "StartThreshold";
            this.StartThreshold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartSlope
            // 
            this.StartSlope.FillWeight = 64F;
            this.StartSlope.HeaderText = "开始斜率(mAu)";
            this.StartSlope.Name = "StartSlope";
            this.StartSlope.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StopFilter
            // 
            this.StopFilter.FillWeight = 64F;
            this.StopFilter.HeaderText = "结束条件";
            this.StopFilter.Items.AddRange(new object[] {
            "时间",
            "阈值",
            "斜率",
            "时间&阈值",
            "时间&阈值&斜率",
            "时间&(阈值|斜率)"});
            this.StopFilter.Name = "StopFilter";
            this.StopFilter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // StopTime
            // 
            this.StopTime.FillWeight = 65F;
            this.StopTime.HeaderText = "结束时间(min)";
            this.StopTime.Name = "StopTime";
            this.StopTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StopThreshold
            // 
            this.StopThreshold.FillWeight = 65F;
            this.StopThreshold.HeaderText = "结束阈值(mAu)";
            this.StopThreshold.Name = "StopThreshold";
            this.StopThreshold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StopSlope
            // 
            this.StopSlope.FillWeight = 65F;
            this.StopSlope.HeaderText = "结束斜率(mAu)";
            this.StopSlope.Name = "StopSlope";
            this.StopSlope.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Volume
            // 
            this.Volume.HeaderText = "体积(ml)";
            this.Volume.Name = "Volume";
            this.Volume.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MaxTime
            // 
            this.MaxTime.FillWeight = 64F;
            this.MaxTime.HeaderText = "时间间隔(min)";
            this.MaxTime.Name = "MaxTime";
            this.MaxTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CollectorMethod_Panel
            // 
            this.CollectorMethod_Panel.Controls.Add(this.CollectorMethod_Browser);
            this.CollectorMethod_Panel.Controls.Add(this.CollectorMethod_List);
            this.CollectorMethod_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CollectorMethod_Panel.Location = new System.Drawing.Point(1116, 3);
            this.CollectorMethod_Panel.Name = "CollectorMethod_Panel";
            this.CollectorMethod_Panel.Size = new System.Drawing.Size(145, 192);
            this.CollectorMethod_Panel.TabIndex = 3;
            // 
            // CollectorMethod_Browser
            // 
            this.CollectorMethod_Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CollectorMethod_Browser.FormattingEnabled = true;
            this.CollectorMethod_Browser.ItemHeight = 12;
            this.CollectorMethod_Browser.Location = new System.Drawing.Point(0, 25);
            this.CollectorMethod_Browser.Name = "CollectorMethod_Browser";
            this.CollectorMethod_Browser.Size = new System.Drawing.Size(145, 167);
            this.CollectorMethod_Browser.TabIndex = 0;
            this.CollectorMethod_Browser.DoubleClick += new System.EventHandler(this.CollectorMethod_Browser_DoubleClick);
            this.CollectorMethod_Browser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CollectorMethod_Browser_KeyDown);
            // 
            // CollectorMethod_List
            // 
            this.CollectorMethod_List.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CollectorMethod_Del,
            this.CollectorMethod_Save,
            this.CollectorMethod_New,
            this.Method_Title});
            this.CollectorMethod_List.Location = new System.Drawing.Point(0, 0);
            this.CollectorMethod_List.Name = "CollectorMethod_List";
            this.CollectorMethod_List.Size = new System.Drawing.Size(145, 25);
            this.CollectorMethod_List.TabIndex = 0;
            this.CollectorMethod_List.Text = "UVToolBar_Set";
            // 
            // CollectorMethod_Del
            // 
            this.CollectorMethod_Del.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CollectorMethod_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorMethod_Del.Image = ((System.Drawing.Image)(resources.GetObject("CollectorMethod_Del.Image")));
            this.CollectorMethod_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorMethod_Del.Name = "CollectorMethod_Del";
            this.CollectorMethod_Del.Size = new System.Drawing.Size(23, 22);
            this.CollectorMethod_Del.Text = "删除方法";
            this.CollectorMethod_Del.Click += new System.EventHandler(this.CollectorMethod_Del_Click);
            // 
            // CollectorMethod_Save
            // 
            this.CollectorMethod_Save.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CollectorMethod_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorMethod_Save.Image = ((System.Drawing.Image)(resources.GetObject("CollectorMethod_Save.Image")));
            this.CollectorMethod_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorMethod_Save.Name = "CollectorMethod_Save";
            this.CollectorMethod_Save.Size = new System.Drawing.Size(23, 22);
            this.CollectorMethod_Save.Text = "保存方法";
            this.CollectorMethod_Save.Click += new System.EventHandler(this.CollectorMethod_Save_Click);
            // 
            // CollectorMethod_New
            // 
            this.CollectorMethod_New.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CollectorMethod_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollectorMethod_New.Image = ((System.Drawing.Image)(resources.GetObject("CollectorMethod_New.Image")));
            this.CollectorMethod_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollectorMethod_New.Name = "CollectorMethod_New";
            this.CollectorMethod_New.Size = new System.Drawing.Size(23, 22);
            this.CollectorMethod_New.Text = "新建方法";
            this.CollectorMethod_New.Click += new System.EventHandler(this.CollectorMethod_New_Click);
            // 
            // Method_Title
            // 
            this.Method_Title.Name = "Method_Title";
            this.Method_Title.Size = new System.Drawing.Size(56, 22);
            this.Method_Title.Text = "方法列表";
            // 
            // CollectSet_Panel
            // 
            this.CollectSet_Panel.ColumnCount = 4;
            this.CollectSet_Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.CollectSet_Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.3814F));
            this.CollectSet_Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.6186F));
            this.CollectSet_Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.CollectSet_Panel.Controls.Add(this.Buttles_Panel, 2, 0);
            this.CollectSet_Panel.Controls.Add(this.CollectorProgram_Panel, 1, 0);
            this.CollectSet_Panel.Controls.Add(this.groupBox1, 0, 0);
            this.CollectSet_Panel.Controls.Add(this.CollectorMethod_Panel, 3, 0);
            this.CollectSet_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CollectSet_Panel.Location = new System.Drawing.Point(0, 0);
            this.CollectSet_Panel.Name = "CollectSet_Panel";
            this.CollectSet_Panel.RowCount = 1;
            this.CollectSet_Panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CollectSet_Panel.Size = new System.Drawing.Size(1264, 198);
            this.CollectSet_Panel.TabIndex = 4;
            // 
            // CollectorButtles_Fresher
            // 
            this.CollectorButtles_Fresher.Enabled = true;
            this.CollectorButtles_Fresher.Tick += new System.EventHandler(this.CollectorButtles_Fresher_Tick);
            // 
            // FrmCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 198);
            this.Controls.Add(this.CollectSet_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimumSize = new System.Drawing.Size(20, 240);
            this.Name = "FrmCollector";
            this.TabText = "馏分收集器";
            this.Text = "FrmCollector";
            this.DockStateChanged += new System.EventHandler(this.FrmCollector_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCollector_FormClosing);
            this.Load += new System.EventHandler(this.FrmCollector_Load);
            this.Resize += new System.EventHandler(this.FrmCollector_Resize);
            this.Buttles_Panel.ResumeLayout(false);
            this.Buttles_Panel.PerformLayout();
            this.Buttles_ToolBar.ResumeLayout(false);
            this.Buttles_ToolBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.CollectorProgram_Panel.ResumeLayout(false);
            this.CollectorProgram_Panel.PerformLayout();
            this.CollectorProgram_ToolBar.ResumeLayout(false);
            this.CollectorProgram_ToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CollectorProgram_Grid)).EndInit();
            this.CollectorMethod_Panel.ResumeLayout(false);
            this.CollectorMethod_Panel.PerformLayout();
            this.CollectorMethod_List.ResumeLayout(false);
            this.CollectorMethod_List.PerformLayout();
            this.CollectSet_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView CollectorProgram_Grid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel CollectorMethod_Panel;
        private System.Windows.Forms.ListBox CollectorMethod_Browser;
        private System.Windows.Forms.ToolStrip CollectorMethod_List;
        private System.Windows.Forms.ToolStripButton CollectorMethod_Del;
        private System.Windows.Forms.ToolStripButton CollectorMethod_Save;
        private System.Windows.Forms.ToolStripButton CollectorMethod_New;
        private System.Windows.Forms.ToolStripLabel Method_Title;
        private System.Windows.Forms.Panel CollectorProgram_Panel;
        private System.Windows.Forms.ToolStrip CollectorProgram_ToolBar;
        private System.Windows.Forms.ToolStripButton CollectorProgram_Add;
        private System.Windows.Forms.ToolStripButton CollectorProgram_Insert;
        private System.Windows.Forms.ToolStripButton CollectorProgram_Del;
        private System.Windows.Forms.Label DelayVolume_Label;
        private System.Windows.Forms.Label PipeDiameterUnit_Label;
        private System.Windows.Forms.Label PipeLengthUnit_Label;
        private System.Windows.Forms.Label ButtleVolumeUnit_Label;
        private System.Windows.Forms.TextBox PipeDiameter_TextBox;
        private System.Windows.Forms.TextBox PipeLength_TextBox;
        private System.Windows.Forms.Label PipeDiameter_Label;
        private System.Windows.Forms.Label PipeLength_Label;
        private System.Windows.Forms.TextBox ButtleVolume_TextBox;
        private System.Windows.Forms.Label ButtleVolume_Label;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel CollectorProgram_MinPeakWidth_Label;
        private System.Windows.Forms.ToolStripTextBox CollectorProgram_MinPeakWidth_TextBox;
        private System.Windows.Forms.ToolStripLabel CollectorProgram_MinPeakWidthUnit_Label;
        private System.Windows.Forms.ToolStripButton HideButtlesPanel;
        private System.Windows.Forms.Panel Buttles_Panel;
        private System.Windows.Forms.Panel Buttles_Area;
        private System.Windows.Forms.ToolStrip Buttles_ToolBar;
        private System.Windows.Forms.ToolStripButton GoToLastButtle;
        private System.Windows.Forms.ToolStripButton GoToNextButtle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel StartButtleNo_Label;
        private System.Windows.Forms.ToolStripTextBox StartButtleNo_TextBox;
        private System.Windows.Forms.ToolStripLabel StopButtleNo_Label;
        private System.Windows.Forms.ToolStripTextBox StopButtleNo_TextBox;
        private System.Windows.Forms.ToolStripLabel CurrentButtleNo_Label;
        private System.Windows.Forms.TableLayoutPanel CollectSet_Panel;
        private System.Windows.Forms.ToolStripButton HideCollectorProgramPanel;
        private System.Windows.Forms.ToolStripButton StartCollectorProgram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label DelayVolumeUnit_Label;
        private System.Windows.Forms.Label DelayVolume;
        private System.Windows.Forms.ToolStripButton StopCollectorProgram;
        private System.Windows.Forms.Timer CollectorButtles_Fresher;
        private System.Windows.Forms.ToolStripDropDownButton CollectorProgramChannel_Menu;
        private System.Windows.Forms.ToolStripMenuItem CollectorProgramChannel1;
        private System.Windows.Forms.ToolStripMenuItem CollectorProgramChannel2;
        private System.Windows.Forms.ToolStripMenuItem CollectorProgramChannel3;
        private System.Windows.Forms.DataGridViewComboBoxColumn StartFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartSlope;
        private System.Windows.Forms.DataGridViewComboBoxColumn StopFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopSlope;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxTime;
    }
}