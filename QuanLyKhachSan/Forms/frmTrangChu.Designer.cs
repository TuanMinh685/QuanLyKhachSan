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
            pnlSidebar = new Panel();
            btnDangXuat = new Button();
            btnBaoCao = new Button();
            btnVoucher = new Button();
            btnHoaDon = new Button();
            btnDichVu = new Button();
            btnKhachHang = new Button();
            btnDatPhong = new Button();
            btnPhong = new Button();
            pnlLogo = new Panel();
            lblHotelSub = new Label();
            lblHotelName = new Label();
            pnlHeader = new Panel();
            lblUserGreeting = new Label();
            lblTitleSection = new Label();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(15, 37, 55);
            pnlSidebar.Controls.Add(btnDangXuat);
            pnlSidebar.Controls.Add(btnBaoCao);
            pnlSidebar.Controls.Add(btnVoucher);
            pnlSidebar.Controls.Add(btnHoaDon);
            pnlSidebar.Controls.Add(btnDichVu);
            pnlSidebar.Controls.Add(btnKhachHang);
            pnlSidebar.Controls.Add(btnDatPhong);
            pnlSidebar.Controls.Add(btnPhong);
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(4, 4, 4, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(300, 1050);
            pnlSidebar.TabIndex = 0;
            pnlSidebar.Paint += pnlSidebar_Paint;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Cursor = Cursors.Hand;
            btnDangXuat.Dock = DockStyle.Bottom;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.Font = new Font("Segoe UI", 9.5F);
            btnDangXuat.ForeColor = Color.FromArgb(248, 113, 113);
            btnDangXuat.Location = new Point(0, 988);
            btnDangXuat.Margin = new Padding(4, 4, 4, 4);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Padding = new Padding(25, 0, 0, 0);
            btnDangXuat.Size = new Size(300, 62);
            btnDangXuat.TabIndex = 8;
            btnDangXuat.Text = "🚪 Đăng xuất";
            btnDangXuat.TextAlign = ContentAlignment.MiddleLeft;
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // btnBaoCao
            // 
            btnBaoCao.Cursor = Cursors.Hand;
            btnBaoCao.Dock = DockStyle.Top;
            btnBaoCao.FlatAppearance.BorderSize = 0;
            btnBaoCao.FlatStyle = FlatStyle.Flat;
            btnBaoCao.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBaoCao.ForeColor = Color.FromArgb(226, 232, 240);
            btnBaoCao.Location = new Point(0, 484);
            btnBaoCao.Margin = new Padding(4, 4, 4, 4);
            btnBaoCao.Name = "btnBaoCao";
            btnBaoCao.Padding = new Padding(25, 0, 0, 0);
            btnBaoCao.Size = new Size(300, 65);
            btnBaoCao.TabIndex = 7;
            btnBaoCao.Text = "📊 Báo cáo & Thống kê";
            btnBaoCao.TextAlign = ContentAlignment.MiddleLeft;
            btnBaoCao.UseVisualStyleBackColor = true;
            btnBaoCao.Click += btnBaoCao_Click;
            // 
            // btnVoucher
            // 
            btnVoucher.Cursor = Cursors.Hand;
            btnVoucher.Dock = DockStyle.Top;
            btnVoucher.FlatAppearance.BorderSize = 0;
            btnVoucher.FlatStyle = FlatStyle.Flat;
            btnVoucher.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVoucher.ForeColor = Color.FromArgb(226, 232, 240);
            btnVoucher.Location = new Point(0, 419);
            btnVoucher.Margin = new Padding(4, 4, 4, 4);
            btnVoucher.Name = "btnVoucher";
            btnVoucher.Padding = new Padding(25, 0, 0, 0);
            btnVoucher.Size = new Size(300, 65);
            btnVoucher.TabIndex = 6;
            btnVoucher.Text = "🏷️ Mã ưu đãi (Voucher)";
            btnVoucher.TextAlign = ContentAlignment.MiddleLeft;
            btnVoucher.UseVisualStyleBackColor = true;
            btnVoucher.Click += btnVoucher_Click;
            // 
            // btnHoaDon
            // 
            btnHoaDon.Cursor = Cursors.Hand;
            btnHoaDon.Dock = DockStyle.Top;
            btnHoaDon.FlatAppearance.BorderSize = 0;
            btnHoaDon.FlatStyle = FlatStyle.Flat;
            btnHoaDon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHoaDon.ForeColor = Color.FromArgb(226, 232, 240);
            btnHoaDon.Location = new Point(0, 354);
            btnHoaDon.Margin = new Padding(4, 4, 4, 4);
            btnHoaDon.Name = "btnHoaDon";
            btnHoaDon.Padding = new Padding(25, 0, 0, 0);
            btnHoaDon.Size = new Size(300, 65);
            btnHoaDon.TabIndex = 5;
            btnHoaDon.Text = "💳 Hóa đơn thanh toán";
            btnHoaDon.TextAlign = ContentAlignment.MiddleLeft;
            btnHoaDon.UseVisualStyleBackColor = true;
            btnHoaDon.Click += btnHoaDon_Click;
            // 
            // btnDichVu
            // 
            btnDichVu.Cursor = Cursors.Hand;
            btnDichVu.Dock = DockStyle.Top;
            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatStyle = FlatStyle.Flat;
            btnDichVu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDichVu.ForeColor = Color.FromArgb(226, 232, 240);
            btnDichVu.Location = new Point(0, 289);
            btnDichVu.Margin = new Padding(4, 4, 4, 4);
            btnDichVu.Name = "btnDichVu";
            btnDichVu.Padding = new Padding(25, 0, 0, 0);
            btnDichVu.Size = new Size(300, 65);
            btnDichVu.TabIndex = 4;
            btnDichVu.Text = "🛎️ Dịch vụ phòng";
            btnDichVu.TextAlign = ContentAlignment.MiddleLeft;
            btnDichVu.UseVisualStyleBackColor = true;
            btnDichVu.Click += btnDichVu_Click;
            // 
            // btnKhachHang
            // 
            btnKhachHang.Cursor = Cursors.Hand;
            btnKhachHang.Dock = DockStyle.Top;
            btnKhachHang.FlatAppearance.BorderSize = 0;
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKhachHang.ForeColor = Color.FromArgb(226, 232, 240);
            btnKhachHang.Location = new Point(0, 224);
            btnKhachHang.Margin = new Padding(4, 4, 4, 4);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Padding = new Padding(25, 0, 0, 0);
            btnKhachHang.Size = new Size(300, 65);
            btnKhachHang.TabIndex = 3;
            btnKhachHang.Text = "👤 Khách hàng";
            btnKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            btnKhachHang.UseVisualStyleBackColor = true;
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // btnDatPhong
            // 
            btnDatPhong.Cursor = Cursors.Hand;
            btnDatPhong.Dock = DockStyle.Top;
            btnDatPhong.FlatAppearance.BorderSize = 0;
            btnDatPhong.FlatStyle = FlatStyle.Flat;
            btnDatPhong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDatPhong.ForeColor = Color.FromArgb(226, 232, 240);
            btnDatPhong.Location = new Point(0, 159);
            btnDatPhong.Margin = new Padding(4, 4, 4, 4);
            btnDatPhong.Name = "btnDatPhong";
            btnDatPhong.Padding = new Padding(25, 0, 0, 0);
            btnDatPhong.Size = new Size(300, 65);
            btnDatPhong.TabIndex = 2;
            btnDatPhong.Text = "📅 Đặt & Nhận phòng";
            btnDatPhong.TextAlign = ContentAlignment.MiddleLeft;
            btnDatPhong.UseVisualStyleBackColor = true;
            btnDatPhong.Click += btnDatPhong_Click;
            // 
            // btnPhong
            // 
            btnPhong.Cursor = Cursors.Hand;
            btnPhong.Dock = DockStyle.Top;
            btnPhong.FlatAppearance.BorderSize = 0;
            btnPhong.FlatStyle = FlatStyle.Flat;
            btnPhong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPhong.ForeColor = Color.FromArgb(226, 232, 240);
            btnPhong.Location = new Point(0, 94);
            btnPhong.Margin = new Padding(4, 4, 4, 4);
            btnPhong.Name = "btnPhong";
            btnPhong.Padding = new Padding(25, 0, 0, 0);
            btnPhong.Size = new Size(300, 65);
            btnPhong.TabIndex = 1;
            btnPhong.Text = "🚪 Quản lý phòng";
            btnPhong.TextAlign = ContentAlignment.MiddleLeft;
            btnPhong.UseVisualStyleBackColor = true;
            btnPhong.Click += btnPhong_Click;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(10, 26, 39);
            pnlLogo.Controls.Add(lblHotelSub);
            pnlLogo.Controls.Add(lblHotelName);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Margin = new Padding(4, 4, 4, 4);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(300, 94);
            pnlLogo.TabIndex = 0;
            // 
            // lblHotelSub
            // 
            lblHotelSub.AutoSize = true;
            lblHotelSub.Font = new Font("Segoe UI", 8.5F);
            lblHotelSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblHotelSub.Location = new Point(25, 52);
            lblHotelSub.Margin = new Padding(4, 0, 4, 0);
            lblHotelSub.Name = "lblHotelSub";
            lblHotelSub.Size = new Size(224, 23);
            lblHotelSub.TabIndex = 1;
            lblHotelSub.Text = "Hệ thống quản lý khách sạn";
            // 
            // lblHotelName
            // 
            lblHotelName.AutoSize = true;
            lblHotelName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHotelName.ForeColor = Color.FromArgb(232, 119, 26);
            lblHotelName.Location = new Point(25, 18);
            lblHotelName.Margin = new Padding(4, 0, 4, 0);
            lblHotelName.Name = "lblHotelName";
            lblHotelName.Size = new Size(215, 32);
            lblHotelName.TabIndex = 0;
            lblHotelName.Text = "AN NHIÊN HOTEL";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblUserGreeting);
            pnlHeader.Controls.Add(lblTitleSection);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(300, 0);
            pnlHeader.Margin = new Padding(4, 4, 4, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1500, 75);
            pnlHeader.TabIndex = 1;
            pnlHeader.Paint += pnlHeader_Paint;
            // 
            // lblUserGreeting
            // 
            lblUserGreeting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserGreeting.Font = new Font("Segoe UI", 9.5F);
            lblUserGreeting.ForeColor = Color.FromArgb(100, 116, 139);
            lblUserGreeting.Location = new Point(1062, 22);
            lblUserGreeting.Margin = new Padding(4, 0, 4, 0);
            lblUserGreeting.Name = "lblUserGreeting";
            lblUserGreeting.Size = new Size(412, 30);
            lblUserGreeting.TabIndex = 1;
            lblUserGreeting.Text = "Chào mừng, Quản trị viên (Admin)";
            lblUserGreeting.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitleSection
            // 
            lblTitleSection.AutoSize = true;
            lblTitleSection.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitleSection.ForeColor = Color.FromArgb(15, 37, 55);
            lblTitleSection.Location = new Point(25, 20);
            lblTitleSection.Margin = new Padding(4, 0, 4, 0);
            lblTitleSection.Name = "lblTitleSection";
            lblTitleSection.Size = new Size(249, 32);
            lblTitleSection.TabIndex = 0;
            lblTitleSection.Text = "HỆ THỐNG QUẢN LÝ";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(244, 246, 249);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(300, 75);
            pnlContent.Margin = new Padding(4, 4, 4, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1500, 975);
            pnlContent.TabIndex = 2;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // frmTrangChu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(1800, 1050);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(4, 4, 4, 4);
            MinimumSize = new Size(1594, 886);
            Name = "frmTrangChu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Khách Sạn An Nhiên";
            WindowState = FormWindowState.Maximized;
            Load += frmTrangChu_Load;
            pnlSidebar.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlLogo.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);

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