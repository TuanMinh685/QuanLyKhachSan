namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmHoaDon
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblPhong = new System.Windows.Forms.Label();
            this.cboDatPhong = new System.Windows.Forms.ComboBox();
            this.dgvChiTietDichVu = new System.Windows.Forms.DataGridView();
            this.btnThemDichVu = new System.Windows.Forms.Button();
            this.btnXoaDongDichVu = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblRightTitle = new System.Windows.Forms.Label();
            this.lblTienPhongTitle = new System.Windows.Forms.Label();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.lblTienDVTitle = new System.Windows.Forms.Label();
            this.lblTienDichVu = new System.Windows.Forms.Label();
            this.lblCocTitle = new System.Windows.Forms.Label();
            this.lblCoc = new System.Windows.Forms.Label();
            this.lblVoucherTitle = new System.Windows.Forms.Label();
            this.txtVoucher = new System.Windows.Forms.TextBox();
            this.btnApDungVoucher = new System.Windows.Forms.Button();
            this.lblGiamGiaTitle = new System.Windows.Forms.Label();
            this.lblTienGiamGia = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.lblTongTienTitle = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblPTTT = new System.Windows.Forms.Label();
            this.cboPTTT = new System.Windows.Forms.ComboBox();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDichVu)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1164, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(462, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "HÓA ĐƠN & THANH TOÁN - KHÁCH SẠN AN NHIÊN";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.lblPhong);
            this.pnlLeft.Controls.Add(this.cboDatPhong);
            this.pnlLeft.Controls.Add(this.dgvChiTietDichVu);
            this.pnlLeft.Controls.Add(this.btnThemDichVu);
            this.pnlLeft.Controls.Add(this.btnXoaDongDichVu);
            this.pnlLeft.Location = new System.Drawing.Point(25, 75);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(680, 580);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = true;
            this.lblPhong.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPhong.Location = new System.Drawing.Point(15, 18);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(126, 25);
            this.lblPhong.TabIndex = 0;
            this.lblPhong.Text = "Chọn phòng:";
            // 
            // cboDatPhong
            // 
            this.cboDatPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDatPhong.FormattingEnabled = true;
            this.cboDatPhong.Items.AddRange(new object[] {
            "Phòng 206 - Đỗ Thu Trang (Deluxe)",
            "Phòng 301 - Nguyễn Văn Hùng (Superior)",
            "Phòng 102 - Lê Minh Tuấn (Standard)"});
            this.cboDatPhong.Location = new System.Drawing.Point(145, 16);
            this.cboDatPhong.Name = "cboDatPhong";
            this.cboDatPhong.Size = new System.Drawing.Size(320, 29);
            this.cboDatPhong.TabIndex = 1;
            this.cboDatPhong.SelectedIndexChanged += new System.EventHandler(this.cboDatPhong_SelectedIndexChanged);
            // 
            // dgvChiTietDichVu
            // 
            this.dgvChiTietDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietDichVu.Location = new System.Drawing.Point(15, 60);
            this.dgvChiTietDichVu.Name = "dgvChiTietDichVu";
            this.dgvChiTietDichVu.RowHeadersWidth = 51;
            this.dgvChiTietDichVu.Size = new System.Drawing.Size(650, 460);
            this.dgvChiTietDichVu.TabIndex = 2;
            // 
            // btnThemDichVu
            // 
            this.btnThemDichVu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnThemDichVu.FlatAppearance.BorderSize = 0;
            this.btnThemDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDichVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemDichVu.ForeColor = System.Drawing.Color.White;
            this.btnThemDichVu.Location = new System.Drawing.Point(15, 530);
            this.btnThemDichVu.Name = "btnThemDichVu";
            this.btnThemDichVu.Size = new System.Drawing.Size(160, 36);
            this.btnThemDichVu.TabIndex = 3;
            this.btnThemDichVu.Text = "+ Thêm dịch vụ";
            this.btnThemDichVu.UseVisualStyleBackColor = false;
            this.btnThemDichVu.Click += new System.EventHandler(this.btnThemDichVu_Click);
            // 
            // btnXoaDongDichVu
            // 
            this.btnXoaDongDichVu.BackColor = System.Drawing.Color.White;
            this.btnXoaDongDichVu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnXoaDongDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDongDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaDongDichVu.Location = new System.Drawing.Point(190, 530);
            this.btnXoaDongDichVu.Name = "btnXoaDongDichVu";
            this.btnXoaDongDichVu.Size = new System.Drawing.Size(130, 36);
            this.btnXoaDongDichVu.TabIndex = 4;
            this.btnXoaDongDichVu.Text = "Xóa món";
            this.btnXoaDongDichVu.UseVisualStyleBackColor = false;
            this.btnXoaDongDichVu.Click += new System.EventHandler(this.btnXoaDongDichVu_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.lblRightTitle);
            this.pnlRight.Controls.Add(this.lblTienPhongTitle);
            this.pnlRight.Controls.Add(this.lblTienPhong);
            this.pnlRight.Controls.Add(this.lblTienDVTitle);
            this.pnlRight.Controls.Add(this.lblTienDichVu);
            this.pnlRight.Controls.Add(this.lblCocTitle);
            this.pnlRight.Controls.Add(this.lblCoc);
            this.pnlRight.Controls.Add(this.lblVoucherTitle);
            this.pnlRight.Controls.Add(this.txtVoucher);
            this.pnlRight.Controls.Add(this.btnApDungVoucher);
            this.pnlRight.Controls.Add(this.lblGiamGiaTitle);
            this.pnlRight.Controls.Add(this.lblTienGiamGia);
            this.pnlRight.Controls.Add(this.pnlLine);
            this.pnlRight.Controls.Add(this.lblTongTienTitle);
            this.pnlRight.Controls.Add(this.lblTongTien);
            this.pnlRight.Controls.Add(this.lblPTTT);
            this.pnlRight.Controls.Add(this.cboPTTT);
            this.pnlRight.Controls.Add(this.btnLapHoaDon);
            this.pnlRight.Location = new System.Drawing.Point(725, 75);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(410, 580);
            this.pnlRight.TabIndex = 2;
            // 
            // lblRightTitle
            // 
            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRightTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRightTitle.Location = new System.Drawing.Point(20, 15);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(155, 28);
            this.lblRightTitle.TabIndex = 0;
            this.lblRightTitle.Text = "Tóm tắt chi phí";
            // 
            // lblTienPhongTitle
            // 
            this.lblTienPhongTitle.AutoSize = true;
            this.lblTienPhongTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienPhongTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTienPhongTitle.Location = new System.Drawing.Point(20, 65);
            this.lblTienPhongTitle.Name = "lblTienPhongTitle";
            this.lblTienPhongTitle.Size = new System.Drawing.Size(149, 21);
            this.lblTienPhongTitle.TabIndex = 1;
            this.lblTienPhongTitle.Text = "Tiền phòng (2 đêm):";
            // 
            // lblTienPhong
            // 
            this.lblTienPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTienPhong.Location = new System.Drawing.Point(190, 65);
            this.lblTienPhong.Name = "lblTienPhong";
            this.lblTienPhong.Size = new System.Drawing.Size(195, 23);
            this.lblTienPhong.TabIndex = 2;
            this.lblTienPhong.Text = "2.900.000 đ";
            this.lblTienPhong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTienDVTitle
            // 
            this.lblTienDVTitle.AutoSize = true;
            this.lblTienDVTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienDVTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTienDVTitle.Location = new System.Drawing.Point(20, 105);
            this.lblTienDVTitle.Name = "lblTienDVTitle";
            this.lblTienDVTitle.Size = new System.Drawing.Size(96, 21);
            this.lblTienDVTitle.TabIndex = 3;
            this.lblTienDVTitle.Text = "Tiền dịch vụ:";
            // 
            // lblTienDichVu
            // 
            this.lblTienDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTienDichVu.Location = new System.Drawing.Point(190, 105);
            this.lblTienDichVu.Name = "lblTienDichVu";
            this.lblTienDichVu.Size = new System.Drawing.Size(195, 23);
            this.lblTienDichVu.TabIndex = 4;
            this.lblTienDichVu.Text = "0 đ";
            this.lblTienDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCocTitle
            // 
            this.lblCocTitle.AutoSize = true;
            this.lblCocTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCocTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCocTitle.Location = new System.Drawing.Point(20, 145);
            this.lblCocTitle.Name = "lblCocTitle";
            this.lblCocTitle.Size = new System.Drawing.Size(155, 21);
            this.lblCocTitle.TabIndex = 5;
            this.lblCocTitle.Text = "Đã cọc / Tạm ứng:";
            // 
            // lblCoc
            // 
            this.lblCoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCoc.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblCoc.Location = new System.Drawing.Point(190, 145);
            this.lblCoc.Name = "lblCoc";
            this.lblCoc.Size = new System.Drawing.Size(195, 23);
            this.lblCoc.TabIndex = 6;
            this.lblCoc.Text = "- 1.000.000 đ";
            this.lblCoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVoucherTitle
            // 
            this.lblVoucherTitle.AutoSize = true;
            this.lblVoucherTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVoucherTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblVoucherTitle.Location = new System.Drawing.Point(20, 190);
            this.lblVoucherTitle.Name = "lblVoucherTitle";
            this.lblVoucherTitle.Size = new System.Drawing.Size(96, 21);
            this.lblVoucherTitle.TabIndex = 7;
            this.lblVoucherTitle.Text = "Mã Voucher:";
            // 
            // txtVoucher
            // 
            this.txtVoucher.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVoucher.Location = new System.Drawing.Point(130, 185);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Size = new System.Drawing.Size(140, 29);
            this.txtVoucher.TabIndex = 8;
            // 
            // btnApDungVoucher
            // 
            this.btnApDungVoucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnApDungVoucher.FlatAppearance.BorderSize = 0;
            this.btnApDungVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApDungVoucher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApDungVoucher.ForeColor = System.Drawing.Color.White;
            this.btnApDungVoucher.Location = new System.Drawing.Point(280, 184);
            this.btnApDungVoucher.Name = "btnApDungVoucher";
            this.btnApDungVoucher.Size = new System.Drawing.Size(100, 31);
            this.btnApDungVoucher.TabIndex = 9;
            this.btnApDungVoucher.Text = "Áp dụng";
            this.btnApDungVoucher.UseVisualStyleBackColor = false;
            this.btnApDungVoucher.Click += new System.EventHandler(this.btnApDungVoucher_Click);
            // 
            // lblGiamGiaTitle
            // 
            this.lblGiamGiaTitle.AutoSize = true;
            this.lblGiamGiaTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGiamGiaTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGiamGiaTitle.Location = new System.Drawing.Point(20, 230);
            this.lblGiamGiaTitle.Name = "lblGiamGiaTitle";
            this.lblGiamGiaTitle.Size = new System.Drawing.Size(134, 21);
            this.lblGiamGiaTitle.TabIndex = 10;
            this.lblGiamGiaTitle.Text = "Giảm trừ voucher:";
            // 
            // lblTienGiamGia
            // 
            this.lblTienGiamGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienGiamGia.ForeColor = System.Drawing.Color.Crimson;
            this.lblTienGiamGia.Location = new System.Drawing.Point(190, 230);
            this.lblTienGiamGia.Name = "lblTienGiamGia";
            this.lblTienGiamGia.Size = new System.Drawing.Size(195, 23);
            this.lblTienGiamGia.TabIndex = 11;
            this.lblTienGiamGia.Text = "- 0 đ";
            this.lblTienGiamGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlLine.Location = new System.Drawing.Point(20, 270);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(365, 1);
            this.pnlLine.TabIndex = 12;
            // 
            // lblTongTienTitle
            // 
            this.lblTongTienTitle.AutoSize = true;
            this.lblTongTienTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTienTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTongTienTitle.Location = new System.Drawing.Point(20, 290);
            this.lblTongTienTitle.Name = "lblTongTienTitle";
            this.lblTongTienTitle.Size = new System.Drawing.Size(194, 25);
            this.lblTongTienTitle.TabIndex = 13;
            this.lblTongTienTitle.Text = "TỔNG THANH TOÁN:";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.lblTongTien.Location = new System.Drawing.Point(180, 285);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(205, 35);
            this.lblTongTien.TabIndex = 14;
            this.lblTongTien.Text = "1.900.000 đ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPTTT
            // 
            this.lblPTTT.AutoSize = true;
            this.lblPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPTTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPTTT.Location = new System.Drawing.Point(20, 345);
            this.lblPTTT.Name = "lblPTTT";
            this.lblPTTT.Size = new System.Drawing.Size(104, 21);
            this.lblPTTT.TabIndex = 15;
            this.lblPTTT.Text = "Phương thức:";
            // 
            // cboPTTT
            // 
            this.cboPTTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboPTTT.FormattingEnabled = true;
            this.cboPTTT.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản QR",
            "Thẻ tín dụng / POS"});
            this.cboPTTT.Location = new System.Drawing.Point(130, 340);
            this.cboPTTT.Name = "cboPTTT";
            this.cboPTTT.Size = new System.Drawing.Size(250, 29);
            this.cboPTTT.TabIndex = 16;
            // 
            // btnLapHoaDon
            // 
            this.btnLapHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.btnLapHoaDon.FlatAppearance.BorderSize = 0;
            this.btnLapHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDon.Location = new System.Drawing.Point(20, 420);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(365, 50);
            this.btnLapHoaDon.TabIndex = 17;
            this.btnLapHoaDon.Text = "XÁC NHẬN THANH TOÁN & IN";
            this.btnLapHoaDon.UseVisualStyleBackColor = false;
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1164, 675);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn & Thanh Toán";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDichVu)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboDatPhong;
        private System.Windows.Forms.DataGridView dgvChiTietDichVu;
        private System.Windows.Forms.Button btnThemDichVu;
        private System.Windows.Forms.Button btnXoaDongDichVu;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblRightTitle;
        private System.Windows.Forms.Label lblTienPhongTitle;
        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.Label lblTienDVTitle;
        private System.Windows.Forms.Label lblTienDichVu;
        private System.Windows.Forms.Label lblCocTitle;
        private System.Windows.Forms.Label lblCoc;
        private System.Windows.Forms.Label lblVoucherTitle;
        private System.Windows.Forms.TextBox txtVoucher;
        private System.Windows.Forms.Button btnApDungVoucher;
        private System.Windows.Forms.Label lblGiamGiaTitle;
        private System.Windows.Forms.Label lblTienGiamGia;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Label lblTongTienTitle;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblPTTT;
        private System.Windows.Forms.ComboBox cboPTTT;
        private System.Windows.Forms.Button btnLapHoaDon;
    }
}