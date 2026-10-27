namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmChiTietHoaDonModal
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblBadgeTrangThai = new System.Windows.Forms.Label();
            this.lblMaHDTag = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.pnlCustomerCard = new System.Windows.Forms.Panel();
            this.lblTenKhach = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.pnlGridWrapper = new System.Windows.Forms.Panel();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlFinanceSummary = new System.Windows.Forms.Panel();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.lblTienDV = new System.Windows.Forms.Label();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.lblTienGiam = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblPTTT = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // frmChiTietHoaDonModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(860, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChiTietHoaDonModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết hóa đơn thanh toán";
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblBadgeTrangThai;
        private System.Windows.Forms.Label lblMaHDTag;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Panel pnlCustomerCard;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Panel pnlGridWrapper;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlFinanceSummary;
        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.Label lblTienDV;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.Label lblTienGiam;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblPTTT;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnDong;
    }
}
