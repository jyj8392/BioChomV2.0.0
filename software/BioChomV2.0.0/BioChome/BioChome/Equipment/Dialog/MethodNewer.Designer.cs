namespace BioChome.Equipment.Dialog
{
    partial class MethodNewer
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
            this.MethodFileName = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.MethodNew = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MethodFileName
            // 
            this.MethodFileName.Location = new System.Drawing.Point(107, 6);
            this.MethodFileName.Name = "MethodFileName";
            this.MethodFileName.Size = new System.Drawing.Size(100, 21);
            this.MethodFileName.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(26, 41);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "确定";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoButton.Location = new System.Drawing.Point(132, 41);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(75, 23);
            this.NoButton.TabIndex = 2;
            this.NoButton.Text = "取消";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // MethodNew
            // 
            this.MethodNew.AutoSize = true;
            this.MethodNew.Location = new System.Drawing.Point(12, 9);
            this.MethodNew.Name = "MethodNew";
            this.MethodNew.Size = new System.Drawing.Size(89, 12);
            this.MethodNew.TabIndex = 3;
            this.MethodNew.Text = "新建配置名称：";
            // 
            // MethodNewer
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.NoButton;
            this.ClientSize = new System.Drawing.Size(230, 89);
            this.Controls.Add(this.MethodNew);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.MethodFileName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MethodNewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MethodNewer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MethodFileName;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.Label MethodNew;
    }
}