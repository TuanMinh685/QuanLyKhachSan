namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmApplePayModal
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblAppleIcon = new System.Windows.Forms.Label();
            this.lblTienTitle = new System.Windows.Forms.Label();
            this.lblTienVal = new System.Windows.Forms.Label();
            this.pnlPosInfo = new System.Windows.Forms.Panel();
            this.lblPosTitle = new System.Windows.Forms.Label();
            this.lblPosDevice = new System.Windows.Forms.Label();
            this.lblPosRefTitle = new System.Windows.Forms.Label();
            this.txtPosRef = new System.Windows.Forms.TextBox();
            this.lblGuide = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlPosInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // frmApplePayModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 20);
            this.ClientSize = new System.Drawing.Size(500, 620);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmApplePayModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán Apple Pay / Chạm Contactless POS";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlPosInfo.ResumeLayout(false);
            this.pnlPosInfo.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblAppleIcon;
        private System.Windows.Forms.Label lblTienTitle;
        private System.Windows.Forms.Label lblTienVal;
        private System.Windows.Forms.Panel pnlPosInfo;
        private System.Windows.Forms.Label lblPosTitle;
        private System.Windows.Forms.Label lblPosDevice;
        private System.Windows.Forms.Label lblPosRefTitle;
        private System.Windows.Forms.TextBox txtPosRef;
        private System.Windows.Forms.Label lblGuide;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}
