namespace Pump
{
    partial class GradCurvShow
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
            this.components = new System.ComponentModel.Container();
            this.GradCurvRulerPic = new System.Windows.Forms.PictureBox();
            this.GradCurvArea = new System.Windows.Forms.PictureBox();
            this.refresher = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GradCurvRulerPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradCurvArea)).BeginInit();
            this.SuspendLayout();
            // 
            // GradCurvRulerPic
            // 
            this.GradCurvRulerPic.BackColor = System.Drawing.SystemColors.Window;
            this.GradCurvRulerPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradCurvRulerPic.Location = new System.Drawing.Point(0, 0);
            this.GradCurvRulerPic.Name = "GradCurvRulerPic";
            this.GradCurvRulerPic.Size = new System.Drawing.Size(290, 263);
            this.GradCurvRulerPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GradCurvRulerPic.TabIndex = 0;
            this.GradCurvRulerPic.TabStop = false;
            this.GradCurvRulerPic.Paint += new System.Windows.Forms.PaintEventHandler(this.GradCurvRulerPic_Paint);
            // 
            // GradCurvArea
            // 
            this.GradCurvArea.BackColor = System.Drawing.SystemColors.Window;
            this.GradCurvArea.Location = new System.Drawing.Point(98, 22);
            this.GradCurvArea.Name = "GradCurvArea";
            this.GradCurvArea.Size = new System.Drawing.Size(175, 154);
            this.GradCurvArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GradCurvArea.TabIndex = 1;
            this.GradCurvArea.TabStop = false;
            this.GradCurvArea.Paint += new System.Windows.Forms.PaintEventHandler(this.GradCurvArea_Paint);
            // 
            // refresher
            // 
            this.refresher.Enabled = true;
            this.refresher.Interval = 500;
            this.refresher.Tick += new System.EventHandler(this.refresher_Tick);
            // 
            // GradCurvShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.GradCurvArea);
            this.Controls.Add(this.GradCurvRulerPic);
            this.Name = "GradCurvShow";
            this.Size = new System.Drawing.Size(290, 263);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GradCurvShow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.GradCurvRulerPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GradCurvArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GradCurvRulerPic;
        private System.Windows.Forms.PictureBox GradCurvArea;
        private System.Windows.Forms.Timer refresher;
    }
}
