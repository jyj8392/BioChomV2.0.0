namespace UVDetector
{
    partial class CurvShow
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CurvRulerPic = new System.Windows.Forms.PictureBox();
            this.CurvArea = new System.Windows.Forms.PictureBox();
            this.Curv0Visible = new System.Windows.Forms.CheckBox();
            this.Curv1Visible = new System.Windows.Forms.CheckBox();
            this.Curv2Visible = new System.Windows.Forms.CheckBox();
            this.Curv0Color_Set = new System.Windows.Forms.Label();
            this.Curv1Color_Set = new System.Windows.Forms.Label();
            this.Curv2Color_Set = new System.Windows.Forms.Label();
            this.CollectState = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurvRulerPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurvArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CollectState)).BeginInit();
            this.SuspendLayout();
            // 
            // CurvRulerPic
            // 
            this.CurvRulerPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurvRulerPic.Location = new System.Drawing.Point(0, 0);
            this.CurvRulerPic.Name = "CurvRulerPic";
            this.CurvRulerPic.Size = new System.Drawing.Size(396, 295);
            this.CurvRulerPic.TabIndex = 0;
            this.CurvRulerPic.TabStop = false;
            // 
            // CurvArea
            // 
            this.CurvArea.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.CurvArea.Location = new System.Drawing.Point(54, 39);
            this.CurvArea.Name = "CurvArea";
            this.CurvArea.Size = new System.Drawing.Size(326, 223);
            this.CurvArea.TabIndex = 1;
            this.CurvArea.TabStop = false;
            this.CurvArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CurvArea_MouseDown);
            this.CurvArea.MouseEnter += new System.EventHandler(this.CurvArea_MouseEnter);
            this.CurvArea.MouseLeave += new System.EventHandler(this.CurvArea_MouseLeave);
            this.CurvArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CurvArea_MouseMove);
            this.CurvArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CurvArea_MouseUp);
            this.CurvArea.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.CurvArea_MouseWheel);
            // 
            // Curv0Visible
            // 
            this.Curv0Visible.AutoSize = true;
            this.Curv0Visible.Location = new System.Drawing.Point(255, 61);
            this.Curv0Visible.Name = "Curv0Visible";
            this.Curv0Visible.Size = new System.Drawing.Size(78, 16);
            this.Curv0Visible.TabIndex = 2;
            this.Curv0Visible.Text = "checkBox1";
            this.Curv0Visible.UseVisualStyleBackColor = true;
            this.Curv0Visible.Visible = false;
            // 
            // Curv1Visible
            // 
            this.Curv1Visible.AutoSize = true;
            this.Curv1Visible.Location = new System.Drawing.Point(255, 84);
            this.Curv1Visible.Name = "Curv1Visible";
            this.Curv1Visible.Size = new System.Drawing.Size(78, 16);
            this.Curv1Visible.TabIndex = 3;
            this.Curv1Visible.Text = "checkBox2";
            this.Curv1Visible.UseVisualStyleBackColor = true;
            this.Curv1Visible.Visible = false;
            // 
            // Curv2Visible
            // 
            this.Curv2Visible.AutoSize = true;
            this.Curv2Visible.Location = new System.Drawing.Point(255, 107);
            this.Curv2Visible.Name = "Curv2Visible";
            this.Curv2Visible.Size = new System.Drawing.Size(78, 16);
            this.Curv2Visible.TabIndex = 4;
            this.Curv2Visible.Text = "checkBox3";
            this.Curv2Visible.UseVisualStyleBackColor = true;
            this.Curv2Visible.Visible = false;
            // 
            // Curv0Color_Set
            // 
            this.Curv0Color_Set.BackColor = System.Drawing.Color.Gray;
            this.Curv0Color_Set.Cursor = System.Windows.Forms.Cursors.Default;
            this.Curv0Color_Set.Location = new System.Drawing.Point(216, 67);
            this.Curv0Color_Set.Name = "Curv0Color_Set";
            this.Curv0Color_Set.Size = new System.Drawing.Size(30, 3);
            this.Curv0Color_Set.TabIndex = 5;
            this.Curv0Color_Set.Visible = false;
            this.Curv0Color_Set.Click += new System.EventHandler(this.Curv0Color_Set_Click);
            // 
            // Curv1Color_Set
            // 
            this.Curv1Color_Set.BackColor = System.Drawing.Color.Gray;
            this.Curv1Color_Set.Cursor = System.Windows.Forms.Cursors.Default;
            this.Curv1Color_Set.Location = new System.Drawing.Point(216, 91);
            this.Curv1Color_Set.Name = "Curv1Color_Set";
            this.Curv1Color_Set.Size = new System.Drawing.Size(30, 3);
            this.Curv1Color_Set.TabIndex = 6;
            this.Curv1Color_Set.Visible = false;
            this.Curv1Color_Set.Click += new System.EventHandler(this.Curv1Color_Set_Click);
            // 
            // Curv2Color_Set
            // 
            this.Curv2Color_Set.BackColor = System.Drawing.Color.Gray;
            this.Curv2Color_Set.Cursor = System.Windows.Forms.Cursors.Default;
            this.Curv2Color_Set.Location = new System.Drawing.Point(216, 113);
            this.Curv2Color_Set.Name = "Curv2Color_Set";
            this.Curv2Color_Set.Size = new System.Drawing.Size(30, 3);
            this.Curv2Color_Set.TabIndex = 7;
            this.Curv2Color_Set.Visible = false;
            this.Curv2Color_Set.Click += new System.EventHandler(this.Curv2Color_Set_Click);
            // 
            // CollectState
            // 
            this.CollectState.BackColor = System.Drawing.Color.Transparent;
            this.CollectState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CollectState.Location = new System.Drawing.Point(54, 210);
            this.CollectState.Name = "CollectState";
            this.CollectState.Size = new System.Drawing.Size(192, 52);
            this.CollectState.TabIndex = 8;
            this.CollectState.TabStop = false;
            this.CollectState.Click += new System.EventHandler(this.CollectState_Click);
            // 
            // CurvShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CollectState);
            this.Controls.Add(this.Curv2Color_Set);
            this.Controls.Add(this.Curv1Color_Set);
            this.Controls.Add(this.Curv0Color_Set);
            this.Controls.Add(this.Curv2Visible);
            this.Controls.Add(this.Curv1Visible);
            this.Controls.Add(this.Curv0Visible);
            this.Controls.Add(this.CurvArea);
            this.Controls.Add(this.CurvRulerPic);
            this.Name = "CurvShow";
            this.Size = new System.Drawing.Size(396, 295);
            this.Load += new System.EventHandler(this.CurvShow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurvShow_Paint);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.CurvShow_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.CurvRulerPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurvArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CollectState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CurvRulerPic;
        private System.Windows.Forms.PictureBox CurvArea;
        private System.Windows.Forms.CheckBox Curv0Visible;
        private System.Windows.Forms.CheckBox Curv1Visible;
        private System.Windows.Forms.CheckBox Curv2Visible;
        private System.Windows.Forms.Label Curv0Color_Set;
        private System.Windows.Forms.Label Curv1Color_Set;
        private System.Windows.Forms.Label Curv2Color_Set;
        private System.Windows.Forms.PictureBox CollectState;
    }
}
