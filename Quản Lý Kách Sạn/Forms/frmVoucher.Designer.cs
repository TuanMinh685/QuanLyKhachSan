namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmVoucher
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.cardLeft = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvVoucher = new System.Windows.Forms.DataGridView();
            this.cardRight = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblLoaiGiam = new System.Windows.Forms.Label();
            this.rdoPhanTram = new System.Windows.Forms.RadioButton();
            this.rdoSoTien = new System.Windows.Forms.RadioButton();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.lblDonToiThieu = new System.Windows.Forms.Label();
            this.txtDonToiThieu = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblNgayBD = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKT = new System.Windows.Forms.Label();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.cardLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            this.cardRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.pnlTop.Controls.Add(this.lblSub);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KHÁCH SẠN AN NHIÊN";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblSub.Location = new System.Drawing.Point(260, 20);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(275, 23);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "|   Quản lý mã ưu đãi & Khuyến mãi";
            // 
            // cardLeft
            // 
            this.cardLeft.BackColor = System.Drawing.Color.White;
            this.cardLeft.Controls.Add(this.txtTimKiem);
            this.cardLeft.Controls.Add(this.dgvVoucher);
            this.cardLeft.Location = new System.Drawing.Point(25, 80);
            this.cardLeft.Name = "cardLeft";
            this.cardLeft.Padding = new System.Windows.Forms.Padding(16);
            this.cardLeft.Size = new System.Drawing.Size(740, 595);
            this.cardLeft.TabIndex = 1;
            this.cardLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(16, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "🔍 Tìm kiếm theo mã voucher...";
            this.txtTimKiem.Size = new System.Drawing.Size(320, 30);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dgvVoucher
            // 
            this.dgvVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Location = new System.Drawing.Point(16, 60);
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.RowHeadersWidth = 51;
            this.dgvVoucher.Size = new System.Drawing.Size(708, 515);
            this.dgvVoucher.TabIndex = 1;
            this.dgvVoucher.SelectionChanged += new System.EventHandler(this.dgvVoucher_SelectionChanged);
            // 
            // cardRight
            // 
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.lblCardTitle);
            this.cardRight.Controls.Add(this.lblCode);
            this.cardRight.Controls.Add(this.txtCode);
            this.cardRight.Controls.Add(this.lblLoaiGiam);
            this.cardRight.Controls.Add(this.rdoPhanTram);
            this.cardRight.Controls.Add(this.rdoSoTien);
            this.cardRight.Controls.Add(this.lblGiaTri);
            this.cardRight.Controls.Add(this.txtGiaTri);
            this.cardRight.Controls.Add(this.lblDonToiThieu);
            this.cardRight.Controls.Add(this.txtDonToiThieu);
            this.cardRight.Controls.Add(this.lblSoLuong);
            this.cardRight.Controls.Add(this.txtSoLuong);
            this.cardRight.Controls.Add(this.lblNgayBD);
            this.cardRight.Controls.Add(this.dtpNgayBatDau);
            this.cardRight.Controls.Add(this.lblNgayKT);
            this.cardRight.Controls.Add(this.dtpNgayHetHan);
            this.cardRight.Controls.Add(this.chkTrangThai);
            this.cardRight.Controls.Add(this.btnThem);
            this.cardRight.Controls.Add(this.btnSua);
            this.cardRight.Controls.Add(this.btnXoa);
            this.cardRight.Controls.Add(this.btnLamMoi);
            this.cardRight.Location = new System.Drawing.Point(785, 80);
            this.cardRight.Name = "cardRight";
            this.cardRight.Padding = new System.Windows.Forms.Padding(20);
            this.cardRight.Size = new System.Drawing.Size(390, 595);
            this.cardRight.TabIndex = 2;
            this.cardRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblCardTitle.Location = new System.Drawing.Point(20, 16);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(183, 28);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Thông tin Voucher";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCode.Location = new System.Drawing.Point(20, 55);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(73, 20);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "Mã Code:";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.Location = new System.Drawing.Point(20, 77);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(350, 30);
            this.txtCode.TabIndex = 2;
            // 
            // lblLoaiGiam
            // 
            this.lblLoaiGiam.AutoSize = true;
            this.lblLoaiGiam.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiGiam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoaiGiam.Location = new System.Drawing.Point(20, 115);
            this.lblLoaiGiam.Name = "lblLoaiGiam";
            this.lblLoaiGiam.Size = new System.Drawing.Size(78, 20);
            this.lblLoaiGiam.TabIndex = 3;
            this.lblLoaiGiam.Text = "Loại giảm:";
            // 
            // rdoPhanTram
            // 
            this.rdoPhanTram.AutoSize = true;
            this.rdoPhanTram.Checked = true;
            this.rdoPhanTram.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoPhanTram.Location = new System.Drawing.Point(120, 113);
            this.rdoPhanTram.Name = "rdoPhanTram";
            this.rdoPhanTram.Size = new System.Drawing.Size(126, 25);
            this.rdoPhanTram.TabIndex = 4;
            this.rdoPhanTram.TabStop = true;
            this.rdoPhanTram.Text = "Phần trăm (%)";
            this.rdoPhanTram.UseVisualStyleBackColor = true;
            // 
            // rdoSoTien
            // 
            this.rdoSoTien.AutoSize = true;
            this.rdoSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoSoTien.Location = new System.Drawing.Point(255, 113);
            this.rdoSoTien.Name = "rdoSoTien";
            this.rdoSoTien.Size = new System.Drawing.Size(117, 25);
            this.rdoSoTien.TabIndex = 5;
            this.rdoSoTien.Text = "Tiền mặt (đ)";
            this.rdoSoTien.UseVisualStyleBackColor = true;
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.AutoSize = true;
            this.lblGiaTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGiaTri.Location = new System.Drawing.Point(20, 150);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(78, 20);
            this.lblGiaTri.TabIndex = 6;
            this.lblGiaTri.Text = "Mức giảm:";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGiaTri.Location = new System.Drawing.Point(20, 172);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(350, 30);
            this.txtGiaTri.TabIndex = 7;
            // 
            // lblDonToiThieu
            // 
            this.lblDonToiThieu.AutoSize = true;
            this.lblDonToiThieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDonToiThieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDonToiThieu.Location = new System.Drawing.Point(20, 212);
            this.lblDonToiThieu.Name = "lblDonToiThieu";
            this.lblDonToiThieu.Size = new System.Drawing.Size(99, 20);
            this.lblDonToiThieu.TabIndex = 8;
            this.lblDonToiThieu.Text = "Đơn tối thiểu:";
            // 
            // txtDonToiThieu
            // 
            this.txtDonToiThieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDonToiThieu.Location = new System.Drawing.Point(20, 234);
            this.txtDonToiThieu.Name = "txtDonToiThieu";
            this.txtDonToiThieu.Size = new System.Drawing.Size(165, 30);
            this.txtDonToiThieu.TabIndex = 9;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoLuong.Location = new System.Drawing.Point(205, 212);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(72, 20);
            this.lblSoLuong.TabIndex = 10;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoLuong.Location = new System.Drawing.Point(205, 234);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(165, 30);
            this.txtSoLuong.TabIndex = 11;
            // 
            // lblNgayBD
            // 
            this.lblNgayBD.AutoSize = true;
            this.lblNgayBD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayBD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgayBD.Location = new System.Drawing.Point(20, 275);
            this.lblNgayBD.Name = "lblNgayBD";
            this.lblNgayBD.Size = new System.Drawing.Size(65, 20);
            this.lblNgayBD.TabIndex = 12;
            this.lblNgayBD.Text = "Bắt đầu:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(20, 297);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(165, 29);
            this.dtpNgayBatDau.TabIndex = 13;
            // 
            // lblNgayKT
            // 
            this.lblNgayKT.AutoSize = true;
            this.lblNgayKT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayKT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgayKT.Location = new System.Drawing.Point(205, 275);
            this.lblNgayKT.Name = "lblNgayKT";
            this.lblNgayKT.Size = new System.Drawing.Size(66, 20);
            this.lblNgayKT.TabIndex = 14;
            this.lblNgayKT.Text = "Hết hạn:";
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(205, 297);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(165, 29);
            this.dtpNgayHetHan.TabIndex = 15;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.chkTrangThai.Location = new System.Drawing.Point(20, 340);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(164, 25);
            this.chkTrangThai.TabIndex = 16;
            this.chkTrangThai.Text = "Kích hoạt sử dụng";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(20, 385);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(350, 42);
            this.btnThem.TabIndex = 17;
            this.btnThem.Text = "THÊM VOUCHER";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(20, 437);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 38);
            this.btnSua.TabIndex = 18;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Location = new System.Drawing.Point(200, 437);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 38);
            this.btnXoa.TabIndex = 19;
            this.btnXoa.Text = "Xóa mã";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnLamMoi.Location = new System.Drawing.Point(20, 485);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(350, 38);
            this.btnLamMoi.TabIndex = 20;
            this.btnLamMoi.Text = "Làm mới Form";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.cardRight);
            this.Controls.Add(this.cardLeft);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmVoucher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Voucher - An Nhiên Hotel";
            this.Load += new System.EventHandler(this.frmVoucher_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.cardLeft.ResumeLayout(false);
            this.cardLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel cardLeft;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvVoucher;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblLoaiGiam;
        private System.Windows.Forms.RadioButton rdoPhanTram;
        private System.Windows.Forms.RadioButton rdoSoTien;
        private System.Windows.Forms.Label lblGiaTri;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.Label lblDonToiThieu;
        private System.Windows.Forms.TextBox txtDonToiThieu;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblNgayBD;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKT;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}