namespace YuyukoRecord
{
    partial class QrCodeImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QrCodeImage));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxQrCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试阶段功能,扫描二维码获取对局信息";
            // 
            // pictureBoxQrCode
            // 
            this.pictureBoxQrCode.Location = new System.Drawing.Point(42, 38);
            this.pictureBoxQrCode.Name = "pictureBoxQrCode";
            this.pictureBoxQrCode.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxQrCode.TabIndex = 1;
            this.pictureBoxQrCode.TabStop = false;
            // 
            // QrCodeImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.pictureBoxQrCode);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QrCodeImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QrCodeImage";
            this.Load += new System.EventHandler(this.ArCodeImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQrCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxQrCode;
    }
}