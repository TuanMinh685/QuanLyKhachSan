namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmKhoaManHinhModal
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
            this.components = new System.ComponentModel.Container();
            this.tmrKhoa = new System.Windows.Forms.Timer(this.components);
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblPhuDe = new System.Windows.Forms.Label();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblTenNguoiDung = new System.Windows.Forms.Label();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.lblThoiGianKhoa = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.pnlInputPass = new System.Windows.Forms.Panel();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnHienPass = new System.Windows.Forms.Button();
            this.lblLoi = new System.Windows.Forms.Label();
            this.btnMoKhoa = new System.Windows.Forms.Button();
            this.btnDoiTaiKhoan = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            this.pnlInputPass.SuspendLayout();
            this.SuspendLayout();
            // 
            // frmKhoaManHinhModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9.5F, 21.5F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(10, 25, 41);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmKhoaManHinhModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Khóa Bàn Làm Việc Lễ Tân";
            this.TopMost = true;
            this.pnlCard.ResumeLayout(false);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlInputPass.ResumeLayout(false);
            this.pnlInputPass.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer tmrKhoa;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblPhuDe;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Label lblTenNguoiDung;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.Label lblThoiGianKhoa;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Panel pnlInputPass;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnHienPass;
        private System.Windows.Forms.Label lblLoi;
        private System.Windows.Forms.Button btnMoKhoa;
        private System.Windows.Forms.Button btnDoiTaiKhoan;
    }
}
