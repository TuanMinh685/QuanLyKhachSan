namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmThanhToanModal
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
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblDatPhong = new System.Windows.Forms.Label();
            this.cboDatPhong = new System.Windows.Forms.ComboBox();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.txtTienPhong = new System.Windows.Forms.TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.lblDichVuSection = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.nudSoLuongDV = new System.Windows.Forms.NumericUpDown();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.dgvChiTietDV = new System.Windows.Forms.DataGridView();
            this.lblTienDV = new System.Windows.Forms.Label();
            this.txtTienDV = new System.Windows.Forms.TextBox();
            this.lblVoucherSection = new System.Windows.Forms.Label();
            this.txtVoucherCode = new System.Windows.Forms.TextBox();
            this.btnGoiYVoucher = new System.Windows.Forms.Button();
            this.btnApDungVoucher = new System.Windows.Forms.Button();
            this.txtTienGiamGia = new System.Windows.Forms.TextBox();
            this.lblPTTT = new System.Windows.Forms.Label();
            this.cboPTTT = new System.Windows.Forms.ComboBox();
            this.pnlTongTienBox = new System.Windows.Forms.Panel();
            this.lblTongTienTieuDe = new System.Windows.Forms.Label();
            this.lblTongTienHienThi = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnVietQR = new System.Windows.Forms.Button();
            this.btnMoMo = new System.Windows.Forms.Button();
            this.btnApplePay = new System.Windows.Forms.Button();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDV)).BeginInit();
            this.SuspendLayout();
            // 
            // frmThanhToanModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThanhToanModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hóa đơn & Thanh toán trả phòng";
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDV)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblDatPhong;
        private System.Windows.Forms.ComboBox cboDatPhong;
        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.TextBox txtTienPhong;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.Label lblDichVuSection;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.NumericUpDown nudSoLuongDV;
        private System.Windows.Forms.Button btnThemDV;
        private System.Windows.Forms.DataGridView dgvChiTietDV;
        private System.Windows.Forms.Label lblTienDV;
        private System.Windows.Forms.TextBox txtTienDV;
        private System.Windows.Forms.Label lblVoucherSection;
        private System.Windows.Forms.TextBox txtVoucherCode;
        private System.Windows.Forms.Button btnGoiYVoucher;
        private System.Windows.Forms.Button btnApDungVoucher;
        private System.Windows.Forms.TextBox txtTienGiamGia;
        private System.Windows.Forms.Label lblPTTT;
        private System.Windows.Forms.ComboBox cboPTTT;
        private System.Windows.Forms.Panel pnlTongTienBox;
        private System.Windows.Forms.Label lblTongTienTieuDe;
        private System.Windows.Forms.Label lblTongTienHienThi;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnVietQR;
        private System.Windows.Forms.Button btnMoMo;
        private System.Windows.Forms.Button btnApplePay;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.ContextMenuStrip cmsVoucher;
    }
}
