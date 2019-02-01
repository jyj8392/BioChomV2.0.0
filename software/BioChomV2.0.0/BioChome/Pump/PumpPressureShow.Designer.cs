namespace Pump
{
    partial class PumpPressureShow
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
            this.PressureCurvPic = new System.Windows.Forms.PictureBox();
            this.CurvArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PressureCurvPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurvArea)).BeginInit();
            this.SuspendLayout();
            // 
            // PressureCurvPic
            // 
            this.PressureCurvPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PressureCurvPic.Location = new System.Drawing.Point(0, 0);
            this.PressureCurvPic.Name = "PressureCurvPic";
            this.PressureCurvPic.Size = new System.Drawing.Size(243, 230);
            this.PressureCurvPic.TabIndex = 0;
            this.PressureCurvPic.TabStop = false;
            // 
            // CurvArea
            // 
            this.CurvArea.Location = new System.Drawing.Point(50, 22);
            this.CurvArea.Name = "CurvArea";
            this.CurvArea.Size = new System.Drawing.Size(167, 138);
            this.CurvArea.TabIndex = 1;
            this.CurvArea.TabStop = false;
            // 
            // PumpPressureShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CurvArea);
            this.Controls.Add(this.PressureCurvPic);
            this.Name = "PumpPressureShow";
            this.Size = new System.Drawing.Size(243, 230);
            this.Load += new System.EventHandler(this.PumpPressureShow_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.PumpPressureShow_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.PressureCurvPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurvArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PressureCurvPic;
        private System.Windows.Forms.PictureBox CurvArea;
    }
}
