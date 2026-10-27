namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmMoMoQR
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblMerchantTitle = new System.Windows.Forms.Label();
            this.lblMerchantVal = new System.Windows.Forms.Label();
            this.lblSdtTitle = new System.Windows.Forms.Label();
            this.lblSdtVal = new System.Windows.Forms.Label();
            this.lblTienTitle = new System.Windows.Forms.Label();
            this.lblTienVal = new System.Windows.Forms.Label();
            this.lblMemoTitle = new System.Windows.Forms.Label();
            this.lblMemoVal = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // frmMoMoQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(490, 680);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMoMoQR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán Ví điện tử MoMo";
            this.Load += new System.EventHandler(this.frmMoMoQR_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblMerchantTitle;
        private System.Windows.Forms.Label lblMerchantVal;
        private System.Windows.Forms.Label lblSdtTitle;
        private System.Windows.Forms.Label lblSdtVal;
        private System.Windows.Forms.Label lblTienTitle;
        private System.Windows.Forms.Label lblTienVal;
        private System.Windows.Forms.Label lblMemoTitle;
        private System.Windows.Forms.Label lblMemoVal;
    }
}
