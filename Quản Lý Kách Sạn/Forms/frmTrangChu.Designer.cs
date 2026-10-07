namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmTrangChu
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblHotelSub = new System.Windows.Forms.Label();
            this.lblHotelName = new System.Windows.Forms.Label();
            this.btnPhong = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnVoucher = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitleSection = new System.Windows.Forms.Label();
            this.lblUserGreeting = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.pnlSidebar.Controls.Add(this.btnDangXuat);
            this.pnlSidebar.Controls.Add(this.btnBaoCao);
            this.pnlSidebar.Controls.Add(this.btnVoucher);
            this.pnlSidebar.Controls.Add(this.btnHoaDon);
            this.pnlSidebar.Controls.Add(this.btnDichVu);
            this.pnlSidebar.Controls.Add(this.btnKhachHang);
            this.pnlSidebar.Controls.Add(this.btnDatPhong);
            this.pnlSidebar.Controls.Add(this.btnPhong);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(240, 780);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(26)))), ((int)(((byte)(39)))));
            this.pnlLogo.Controls.Add(this.lblHotelSub);
            this.pnlLogo.Controls.Add(this.lblHotelName);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(240, 75);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblHotelSub
            // 
            this.lblHotelSub.AutoSize = true;
            this.lblHotelSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHotelSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHotelSub.Location = new System.Drawing.Point(20, 42);
            this.lblHotelSub.Name = "lblHotelSub";
            this.lblHotelSub.Size = new System.Drawing.Size(183, 19);
            this.lblHotelSub.TabIndex = 1;
            this.lblHotelSub.Text = "Hệ thống quản lý khách sạn";
            // 
            // lblHotelName
            // 
            this.lblHotelName.AutoSize = true;
            this.lblHotelName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHotelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.lblHotelName.Location = new System.Drawing.Point(20, 14);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new System.Drawing.Size(193, 28);
            this.lblHotelName.TabIndex = 0;
            this.lblHotelName.Text = "AN NHIÊN HOTEL";
            // 
            // btnPhong
            // 
            this.btnPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPhong.FlatAppearance.BorderSize = 0;
            this.btnPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPhong.Location = new System.Drawing.Point(0, 75);
            this.btnPhong.Name = "btnPhong";
            this.btnPhong.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnPhong.Size = new System.Drawing.Size(240, 52);
            this.btnPhong.TabIndex = 1;
            this.btnPhong.Text = "🚪 Quản lý phòng";
            this.btnPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhong.UseVisualStyleBackColor = true;
            this.btnPhong.Click += new System.EventHandler(this.btnPhong_Click);
            // 
            // btnDatPhong
            // 
            this.btnDatPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDatPhong.FlatAppearance.BorderSize = 0;
            this.btnDatPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnDatPhong.Location = new System.Drawing.Point(0, 127);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDatPhong.Size = new System.Drawing.Size(240, 52);
            this.btnDatPhong.TabIndex = 2;
            this.btnDatPhong.Text = "📅 Đặt & Nhận phòng";
            this.btnDatPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDatPhong.UseVisualStyleBackColor = true;
            this.btnDatPhong.Click += new System.EventHandler(this.btnDatPhong_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnKhachHang.Location = new System.Drawing.Point(0, 179);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnKhachHang.Size = new System.Drawing.Size(240, 52);
            this.btnKhachHang.TabIndex = 3;
            this.btnKhachHang.Text = "👤 Khách hàng";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.UseVisualStyleBackColor = true;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnDichVu
            // 
            this.btnDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDichVu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDichVu.FlatAppearance.BorderSize = 0;
            this.btnDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnDichVu.Location = new System.Drawing.Point(0, 231);
            this.btnDichVu.Name = "btnDichVu";
            this.btnDichVu.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDichVu.Size = new System.Drawing.Size(240, 52);
            this.btnDichVu.TabIndex = 4;
            this.btnDichVu.Text = "🛎️ Dịch vụ phòng";
            this.btnDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDichVu.UseVisualStyleBackColor = true;
            this.btnDichVu.Click += new System.EventHandler(this.btnDichVu_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHoaDon.FlatAppearance.BorderSize = 0;
            this.btnHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnHoaDon.Location = new System.Drawing.Point(0, 283);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnHoaDon.Size = new System.Drawing.Size(240, 52);
            this.btnHoaDon.TabIndex = 5;
            this.btnHoaDon.Text = "💳 Hóa đơn thanh toán";
            this.btnHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnVoucher
            // 
            this.btnVoucher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVoucher.FlatAppearance.BorderSize = 0;
            this.btnVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoucher.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVoucher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnVoucher.Location = new System.Drawing.Point(0, 335);
            this.btnVoucher.Name = "btnVoucher";
            this.btnVoucher.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnVoucher.Size = new System.Drawing.Size(240, 52);
            this.btnVoucher.TabIndex = 6;
            this.btnVoucher.Text = "🏷️ Mã ưu đãi (Voucher)";
            this.btnVoucher.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoucher.UseVisualStyleBackColor = true;
            this.btnVoucher.Click += new System.EventHandler(this.btnVoucher_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBaoCao.Location = new System.Drawing.Point(0, 387);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnBaoCao.Size = new System.Drawing.Size(240, 52);
            this.btnBaoCao.TabIndex = 7;
            this.btnBaoCao.Text = "📊 Báo cáo & Thống kê";
            this.btnBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnDangXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnDangXuat.Location = new System.Drawing.Point(0, 730);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDangXuat.Size = new System.Drawing.Size(240, 50);
            this.btnDangXuat.TabIndex = 8;
            this.btnDangXuat.Text = "🚪 Đăng xuất";
            this.btnDangXuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblUserGreeting);
            this.pnlHeader.Controls.Add(this.lblTitleSection);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(240, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblTitleSection
            // 
            this.lblTitleSection.AutoSize = true;
            this.lblTitleSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitleSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.lblTitleSection.Location = new System.Drawing.Point(20, 16);
            this.lblTitleSection.Name = "lblTitleSection";
            this.lblTitleSection.Size = new System.Drawing.Size(225, 28);
            this.lblTitleSection.TabIndex = 0;
            this.lblTitleSection.Text = "HỆ THỐNG QUẢN LÝ";
            // 
            // lblUserGreeting
            // 
            this.lblUserGreeting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserGreeting.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUserGreeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblUserGreeting.Location = new System.Drawing.Point(850, 18);
            this.lblUserGreeting.Name = "lblUserGreeting";
            this.lblUserGreeting.Size = new System.Drawing.Size(330, 24);
            this.lblUserGreeting.TabIndex = 1;
            this.lblUserGreeting.Text = "Chào mừng, Quản trị viên (Admin)";
            this.lblUserGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(240, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 720);
            this.pnlContent.TabIndex = 2;
            // 
            // frmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1440, 780);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "frmTrangChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Khách Sạn An Nhiên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTrangChu_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblHotelSub;
        private System.Windows.Forms.Label lblHotelName;
        private System.Windows.Forms.Button btnPhong;
        private System.Windows.Forms.Button btnDatPhong;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnDichVu;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button btnVoucher;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitleSection;
        private System.Windows.Forms.Label lblUserGreeting;
        private System.Windows.Forms.Panel pnlContent;
    }
}