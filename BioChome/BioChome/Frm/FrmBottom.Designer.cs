namespace BioChome
{
    partial class FrmBottom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBottom));
            this.EquipmentToolBar_LOG = new System.Windows.Forms.ToolStrip();
            this.Log_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.Log_Copy = new System.Windows.Forms.ToolStripButton();
            this.Log_Clear = new System.Windows.Forms.ToolStripButton();
            this.EquipmentLOG_Text = new System.Windows.Forms.RichTextBox();
            this.EquipmentToolBar_LOG.SuspendLayout();
            this.SuspendLayout();
            // 
            // EquipmentToolBar_LOG
            // 
            this.EquipmentToolBar_LOG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Log_SelectAll,
            this.Log_Copy,
            this.Log_Clear});
            this.EquipmentToolBar_LOG.Location = new System.Drawing.Point(0, 0);
            this.EquipmentToolBar_LOG.Name = "EquipmentToolBar_LOG";
            this.EquipmentToolBar_LOG.Size = new System.Drawing.Size(284, 25);
            this.EquipmentToolBar_LOG.TabIndex = 0;
            this.EquipmentToolBar_LOG.Text = "toolStrip1";
            // 
            // Log_SelectAll
            // 
            this.Log_SelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Log_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("Log_SelectAll.Image")));
            this.Log_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Log_SelectAll.Name = "Log_SelectAll";
            this.Log_SelectAll.Size = new System.Drawing.Size(23, 22);
            this.Log_SelectAll.Text = "全选";
            this.Log_SelectAll.Click += new System.EventHandler(this.Log_SelectAll_Click);
            // 
            // Log_Copy
            // 
            this.Log_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Log_Copy.Image = ((System.Drawing.Image)(resources.GetObject("Log_Copy.Image")));
            this.Log_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Log_Copy.Name = "Log_Copy";
            this.Log_Copy.Size = new System.Drawing.Size(23, 22);
            this.Log_Copy.Text = "复制";
            this.Log_Copy.Click += new System.EventHandler(this.Log_Copy_Click);
            // 
            // Log_Clear
            // 
            this.Log_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Log_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Log_Clear.Image")));
            this.Log_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Log_Clear.Name = "Log_Clear";
            this.Log_Clear.Size = new System.Drawing.Size(23, 22);
            this.Log_Clear.Text = "清空";
            this.Log_Clear.Click += new System.EventHandler(this.Log_Clear_Click);
            // 
            // EquipmentLOG_Text
            // 
            this.EquipmentLOG_Text.AutoWordSelection = true;
            this.EquipmentLOG_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EquipmentLOG_Text.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EquipmentLOG_Text.Location = new System.Drawing.Point(0, 25);
            this.EquipmentLOG_Text.Name = "EquipmentLOG_Text";
            this.EquipmentLOG_Text.ReadOnly = true;
            this.EquipmentLOG_Text.Size = new System.Drawing.Size(284, 237);
            this.EquipmentLOG_Text.TabIndex = 1;
            this.EquipmentLOG_Text.Text = "";
            // 
            // FrmBottom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.EquipmentLOG_Text);
            this.Controls.Add(this.EquipmentToolBar_LOG);
            this.Name = "FrmBottom";
            this.TabText = "日志";
            this.Text = "FrmBottom";
            this.DockStateChanged += new System.EventHandler(this.FrmBottom_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBottom_FormClosing);
            this.Load += new System.EventHandler(this.FrmBottom_Load);
            this.EquipmentToolBar_LOG.ResumeLayout(false);
            this.EquipmentToolBar_LOG.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip EquipmentToolBar_LOG;
        private System.Windows.Forms.ToolStripButton Log_SelectAll;
        private System.Windows.Forms.ToolStripButton Log_Copy;
        private System.Windows.Forms.ToolStripButton Log_Clear;
        public System.Windows.Forms.RichTextBox EquipmentLOG_Text;
    }
}