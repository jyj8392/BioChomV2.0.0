namespace BioChome
{
    partial class FrmPH
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
            this.Conductance_Label = new System.Windows.Forms.Label();
            this.PH_Label = new System.Windows.Forms.Label();
            this.Temperature_Label = new System.Windows.Forms.Label();
            this.ConductanceValue_Label = new System.Windows.Forms.Label();
            this.ConductanceUnit_Label = new System.Windows.Forms.Label();
            this.PHValue_Label = new System.Windows.Forms.Label();
            this.TemperatureValue_Label = new System.Windows.Forms.Label();
            this.TemperatureUnit_Label = new System.Windows.Forms.Label();
            this.PHInfo_Fresher = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Conductance_Label
            // 
            this.Conductance_Label.AutoSize = true;
            this.Conductance_Label.Location = new System.Drawing.Point(12, 9);
            this.Conductance_Label.Name = "Conductance_Label";
            this.Conductance_Label.Size = new System.Drawing.Size(53, 12);
            this.Conductance_Label.TabIndex = 0;
            this.Conductance_Label.Text = "电导率：";
            // 
            // PH_Label
            // 
            this.PH_Label.AutoSize = true;
            this.PH_Label.Location = new System.Drawing.Point(12, 31);
            this.PH_Label.Name = "PH_Label";
            this.PH_Label.Size = new System.Drawing.Size(41, 12);
            this.PH_Label.TabIndex = 1;
            this.PH_Label.Text = "PH值：";
            // 
            // Temperature_Label
            // 
            this.Temperature_Label.AutoSize = true;
            this.Temperature_Label.Location = new System.Drawing.Point(12, 55);
            this.Temperature_Label.Name = "Temperature_Label";
            this.Temperature_Label.Size = new System.Drawing.Size(41, 12);
            this.Temperature_Label.TabIndex = 2;
            this.Temperature_Label.Text = "温度：";
            // 
            // ConductanceValue_Label
            // 
            this.ConductanceValue_Label.AutoSize = true;
            this.ConductanceValue_Label.Location = new System.Drawing.Point(74, 8);
            this.ConductanceValue_Label.Name = "ConductanceValue_Label";
            this.ConductanceValue_Label.Size = new System.Drawing.Size(41, 12);
            this.ConductanceValue_Label.TabIndex = 3;
            this.ConductanceValue_Label.Text = "------";
            // 
            // ConductanceUnit_Label
            // 
            this.ConductanceUnit_Label.AutoSize = true;
            this.ConductanceUnit_Label.Location = new System.Drawing.Point(120, 8);
            this.ConductanceUnit_Label.Name = "ConductanceUnit_Label";
            this.ConductanceUnit_Label.Size = new System.Drawing.Size(41, 12);
            this.ConductanceUnit_Label.TabIndex = 4;
            this.ConductanceUnit_Label.Text = "us/cm2";
            // 
            // PHValue_Label
            // 
            this.PHValue_Label.AutoSize = true;
            this.PHValue_Label.Location = new System.Drawing.Point(74, 31);
            this.PHValue_Label.Name = "PHValue_Label";
            this.PHValue_Label.Size = new System.Drawing.Size(41, 12);
            this.PHValue_Label.TabIndex = 5;
            this.PHValue_Label.Text = "------";
            // 
            // TemperatureValue_Label
            // 
            this.TemperatureValue_Label.AutoSize = true;
            this.TemperatureValue_Label.Location = new System.Drawing.Point(74, 54);
            this.TemperatureValue_Label.Name = "TemperatureValue_Label";
            this.TemperatureValue_Label.Size = new System.Drawing.Size(41, 12);
            this.TemperatureValue_Label.TabIndex = 6;
            this.TemperatureValue_Label.Text = "------";
            // 
            // TemperatureUnit_Label
            // 
            this.TemperatureUnit_Label.AutoSize = true;
            this.TemperatureUnit_Label.Location = new System.Drawing.Point(122, 54);
            this.TemperatureUnit_Label.Name = "TemperatureUnit_Label";
            this.TemperatureUnit_Label.Size = new System.Drawing.Size(17, 12);
            this.TemperatureUnit_Label.TabIndex = 7;
            this.TemperatureUnit_Label.Text = "℃";
            // 
            // PHInfo_Fresher
            // 
            this.PHInfo_Fresher.Enabled = true;
            this.PHInfo_Fresher.Interval = 500;
            this.PHInfo_Fresher.Tick += new System.EventHandler(this.PHInfo_Fresher_Tick);
            // 
            // FrmPH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 344);
            this.Controls.Add(this.TemperatureUnit_Label);
            this.Controls.Add(this.TemperatureValue_Label);
            this.Controls.Add(this.PHValue_Label);
            this.Controls.Add(this.ConductanceUnit_Label);
            this.Controls.Add(this.ConductanceValue_Label);
            this.Controls.Add(this.Temperature_Label);
            this.Controls.Add(this.PH_Label);
            this.Controls.Add(this.Conductance_Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmPH";
            this.TabText = "采集盒";
            this.Text = "FrmPH";
            this.DockStateChanged += new System.EventHandler(this.FrmPH_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPH_FormClosing);
            this.Load += new System.EventHandler(this.FrmPH_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Conductance_Label;
        private System.Windows.Forms.Label PH_Label;
        private System.Windows.Forms.Label Temperature_Label;
        private System.Windows.Forms.Label ConductanceValue_Label;
        private System.Windows.Forms.Label ConductanceUnit_Label;
        private System.Windows.Forms.Label PHValue_Label;
        private System.Windows.Forms.Label TemperatureValue_Label;
        private System.Windows.Forms.Label TemperatureUnit_Label;
        private System.Windows.Forms.Timer PHInfo_Fresher;
    }
}