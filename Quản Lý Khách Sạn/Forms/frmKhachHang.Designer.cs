namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmKhachHang
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dgvKhHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvKhCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvLsHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvLsCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle colTongTienStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblNgayHienTai = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.cboNhomKhach = new System.Windows.Forms.ComboBox();
            this.btnThemKhachHang = new System.Windows.Forms.Button();
            this.pnlDanhSach = new System.Windows.Forms.Panel();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.lblTongHoSo = new System.Windows.Forms.Label();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblTieuDeThongTin = new System.Windows.Forms.Label();
            this.lblNhomBadge = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblQuocTich = new System.Windows.Forms.Label();
            this.txtQuocTich = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblSoGiayTo = new System.Windows.Forms.Label();
            this.txtSoGiayTo = new System.Windows.Forms.TextBox();
            this.lblLoiSoGiayTo = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.chkNhanUuDai = new System.Windows.Forms.CheckBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.tabChiTiet = new System.Windows.Forms.TabControl();
            this.tpThongTinChung = new System.Windows.Forms.TabPage();
            this.lblThongTinChung = new System.Windows.Forms.Label();
            this.tpLichSu = new System.Windows.Forms.TabPage();
            this.tpGhiChu = new System.Windows.Forms.TabPage();
            this.lblGhiChuXem = new System.Windows.Forms.Label();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.colMaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoGiayTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLanLuuTru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNhomKhach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.colLsPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLsThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLsTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLsTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrTimKiem = new System.Windows.Forms.Timer(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlDanhSach.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            this.tabChiTiet.SuspendLayout();
            this.tpThongTinChung.SuspendLayout();
            this.tpLichSu.SuspendLayout();
            this.tpGhiChu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.lblTieuDe.Location = new System.Drawing.Point(18, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Text = "Quản lý khách hàng";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblMoTa.Location = new System.Drawing.Point(20, 50);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Text = "Hồ sơ khách hàng và lịch sử lưu trú tập trung";
            // 
            // lblNgayHienTai
            // 
            this.lblNgayHienTai.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblNgayHienTai.AutoSize = false;
            this.lblNgayHienTai.BackColor = System.Drawing.Color.White;
            this.lblNgayHienTai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNgayHienTai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayHienTai.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.lblNgayHienTai.Location = new System.Drawing.Point(924, 18);
            this.lblNgayHienTai.Name = "lblNgayHienTai";
            this.lblNgayHienTai.Size = new System.Drawing.Size(146, 26);
            this.lblNgayHienTai.Text = "Thứ Ba, 29/09/2026";
            this.lblNgayHienTai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtTimKiem.Location = new System.Drawing.Point(20, 77);
            this.txtTimKiem.MaxLength = 100;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(250, 27);
            this.txtTimKiem.PlaceholderText = "Tìm họ tên, SĐT, số giấy tờ... (Ctrl+F)";
            // 
            // cboNhomKhach
            // 
            this.cboNhomKhach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomKhach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhomKhach.FormattingEnabled = true;
            this.cboNhomKhach.Location = new System.Drawing.Point(278, 77);
            this.cboNhomKhach.Name = "cboNhomKhach";
            this.cboNhomKhach.Size = new System.Drawing.Size(170, 25);
            this.cboNhomKhach.Items.AddRange(new object[] {
            "Tất cả nhóm khách",
            "VIP",
            "Thân thiết",
            "Mới"});
            // 
            // btnThemKhachHang
            // 
            this.btnThemKhachHang.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnThemKhachHang.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.btnThemKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemKhachHang.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemKhachHang.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.btnThemKhachHang.Location = new System.Drawing.Point(929, 74);
            this.btnThemKhachHang.Name = "btnThemKhachHang";
            this.btnThemKhachHang.Size = new System.Drawing.Size(141, 34);
            this.btnThemKhachHang.Text = "+  Thêm khách hàng";
            this.btnThemKhachHang.UseVisualStyleBackColor = false;
            this.btnThemKhachHang.FlatAppearance.BorderSize = 0;
            // 
            // pnlDanhSach
            // 
            this.pnlDanhSach.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.pnlDanhSach.BackColor = System.Drawing.Color.White;
            this.pnlDanhSach.Location = new System.Drawing.Point(20, 120);
            this.pnlDanhSach.Name = "pnlDanhSach";
            this.pnlDanhSach.Size = new System.Drawing.Size(696, 504);
            this.pnlDanhSach.Controls.Add(this.dgvKhachHang);
            this.pnlDanhSach.Controls.Add(this.tabChiTiet);
            this.pnlDanhSach.Controls.Add(this.lblTongHoSo);
            this.pnlDanhSach.Controls.Add(this.lblSoKetQua);
            // 
            // lblSoKetQua
            // 
            this.lblSoKetQua.AutoSize = true;
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.lblSoKetQua.Location = new System.Drawing.Point(14, 13);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Text = "0 khách hàng gần đây";
            // 
            // lblTongHoSo
            // 
            this.lblTongHoSo.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblTongHoSo.AutoSize = false;
            this.lblTongHoSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongHoSo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTongHoSo.Location = new System.Drawing.Point(522, 15);
            this.lblTongHoSo.Name = "lblTongHoSo";
            this.lblTongHoSo.Size = new System.Drawing.Size(160, 18);
            this.lblTongHoSo.Text = "0 hồ sơ";
            this.lblTongHoSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right);
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Location = new System.Drawing.Point(730, 120);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(340, 462);
            this.pnlThongTin.Controls.Add(this.chkNhanUuDai);
            this.pnlThongTin.Controls.Add(this.txtGhiChu);
            this.pnlThongTin.Controls.Add(this.lblGhiChu);
            this.pnlThongTin.Controls.Add(this.lblLoiSoGiayTo);
            this.pnlThongTin.Controls.Add(this.txtSoGiayTo);
            this.pnlThongTin.Controls.Add(this.lblSoGiayTo);
            this.pnlThongTin.Controls.Add(this.txtDiaChi);
            this.pnlThongTin.Controls.Add(this.lblDiaChi);
            this.pnlThongTin.Controls.Add(this.txtEmail);
            this.pnlThongTin.Controls.Add(this.lblEmail);
            this.pnlThongTin.Controls.Add(this.txtQuocTich);
            this.pnlThongTin.Controls.Add(this.lblQuocTich);
            this.pnlThongTin.Controls.Add(this.dtpNgaySinh);
            this.pnlThongTin.Controls.Add(this.lblNgaySinh);
            this.pnlThongTin.Controls.Add(this.cboGioiTinh);
            this.pnlThongTin.Controls.Add(this.lblGioiTinh);
            this.pnlThongTin.Controls.Add(this.txtDienThoai);
            this.pnlThongTin.Controls.Add(this.lblDienThoai);
            this.pnlThongTin.Controls.Add(this.txtHoTen);
            this.pnlThongTin.Controls.Add(this.lblHoTen);
            this.pnlThongTin.Controls.Add(this.lblNhomBadge);
            this.pnlThongTin.Controls.Add(this.lblTieuDeThongTin);
            // 
            // lblTieuDeThongTin
            // 
            this.lblTieuDeThongTin.AutoSize = true;
            this.lblTieuDeThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeThongTin.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.lblTieuDeThongTin.Location = new System.Drawing.Point(14, 10);
            this.lblTieuDeThongTin.Name = "lblTieuDeThongTin";
            this.lblTieuDeThongTin.Text = "Thông tin khách hàng";
            // 
            // lblNhomBadge
            // 
            this.lblNhomBadge.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblNhomBadge.AutoSize = false;
            this.lblNhomBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNhomBadge.Location = new System.Drawing.Point(254, 12);
            this.lblNhomBadge.Name = "lblNhomBadge";
            this.lblNhomBadge.Size = new System.Drawing.Size(72, 22);
            this.lblNhomBadge.Text = "Mới";
            this.lblNhomBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNhomBadge.Visible = false;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblHoTen.Location = new System.Drawing.Point(14, 44);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Text = "Họ và tên *";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtHoTen.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtHoTen.Location = new System.Drawing.Point(14, 61);
            this.txtHoTen.MaxLength = 100;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(312, 27);
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDienThoai.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblDienThoai.Location = new System.Drawing.Point(14, 96);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Text = "Điện thoại *";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDienThoai.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtDienThoai.Location = new System.Drawing.Point(14, 113);
            this.txtDienThoai.MaxLength = 20;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(150, 27);
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblGioiTinh.Location = new System.Drawing.Point(176, 96);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Text = "Giới tính";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Location = new System.Drawing.Point(176, 113);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(150, 25);
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblNgaySinh.Location = new System.Drawing.Point(14, 148);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Text = "Ngày sinh";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(14, 165);
            this.dtpNgaySinh.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.ShowCheckBox = true;
            this.dtpNgaySinh.Size = new System.Drawing.Size(150, 25);
            // 
            // lblQuocTich
            // 
            this.lblQuocTich.AutoSize = true;
            this.lblQuocTich.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblQuocTich.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblQuocTich.Location = new System.Drawing.Point(176, 148);
            this.lblQuocTich.Name = "lblQuocTich";
            this.lblQuocTich.Text = "Quốc tịch";
            // 
            // txtQuocTich
            // 
            this.txtQuocTich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuocTich.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtQuocTich.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtQuocTich.Location = new System.Drawing.Point(176, 165);
            this.txtQuocTich.MaxLength = 50;
            this.txtQuocTich.Name = "txtQuocTich";
            this.txtQuocTich.Size = new System.Drawing.Size(150, 27);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblEmail.Location = new System.Drawing.Point(14, 200);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtEmail.Location = new System.Drawing.Point(14, 217);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(312, 27);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblDiaChi.Location = new System.Drawing.Point(14, 252);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDiaChi.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtDiaChi.Location = new System.Drawing.Point(14, 269);
            this.txtDiaChi.MaxLength = 255;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(312, 27);
            // 
            // lblSoGiayTo
            // 
            this.lblSoGiayTo.AutoSize = true;
            this.lblSoGiayTo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSoGiayTo.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblSoGiayTo.Location = new System.Drawing.Point(14, 304);
            this.lblSoGiayTo.Name = "lblSoGiayTo";
            this.lblSoGiayTo.Text = "Số CCCD / Hộ chiếu";
            // 
            // txtSoGiayTo
            // 
            this.txtSoGiayTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoGiayTo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSoGiayTo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtSoGiayTo.Location = new System.Drawing.Point(14, 321);
            this.txtSoGiayTo.MaxLength = 30;
            this.txtSoGiayTo.Name = "txtSoGiayTo";
            this.txtSoGiayTo.Size = new System.Drawing.Size(312, 27);
            // 
            // lblLoiSoGiayTo
            // 
            this.lblLoiSoGiayTo.AutoSize = true;
            this.lblLoiSoGiayTo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLoiSoGiayTo.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblLoiSoGiayTo.Location = new System.Drawing.Point(14, 350);
            this.lblLoiSoGiayTo.Name = "lblLoiSoGiayTo";
            this.lblLoiSoGiayTo.Size = new System.Drawing.Size(10, 13);
            this.lblLoiSoGiayTo.Text = "";
            this.lblLoiSoGiayTo.Visible = false;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblGhiChu.Location = new System.Drawing.Point(14, 368);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtGhiChu.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtGhiChu.Location = new System.Drawing.Point(14, 385);
            this.txtGhiChu.MaxLength = 500;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(312, 40);
            // 
            // chkNhanUuDai
            // 
            this.chkNhanUuDai.AutoSize = true;
            this.chkNhanUuDai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkNhanUuDai.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.chkNhanUuDai.Location = new System.Drawing.Point(14, 432);
            this.chkNhanUuDai.Name = "chkNhanUuDai";
            this.chkNhanUuDai.Size = new System.Drawing.Size(194, 19);
            this.chkNhanUuDai.Text = "Đồng ý nhận thông tin ưu đãi";
            this.chkNhanUuDai.UseVisualStyleBackColor = true;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.btnHuy.Location = new System.Drawing.Point(730, 592);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(84, 34);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.btnCapNhat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            this.btnCapNhat.Location = new System.Drawing.Point(918, 592);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(152, 34);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            // 
            // tabChiTiet
            // 
            this.tabChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.tabChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabChiTiet.Location = new System.Drawing.Point(0, 316);
            this.tabChiTiet.Name = "tabChiTiet";
            this.tabChiTiet.SelectedIndex = 0;
            this.tabChiTiet.Size = new System.Drawing.Size(696, 188);
            this.tabChiTiet.Controls.Add(this.tpThongTinChung);
            this.tabChiTiet.Controls.Add(this.tpLichSu);
            this.tabChiTiet.Controls.Add(this.tpGhiChu);
            // 
            // tpThongTinChung
            // 
            this.tpThongTinChung.BackColor = System.Drawing.Color.White;
            this.tpThongTinChung.Location = new System.Drawing.Point(4, 26);
            this.tpThongTinChung.Name = "tpThongTinChung";
            this.tpThongTinChung.Padding = new System.Windows.Forms.Padding(3);
            this.tpThongTinChung.Size = new System.Drawing.Size(688, 158);
            this.tpThongTinChung.Text = "Thông tin chung";
            this.tpThongTinChung.Controls.Add(this.lblThongTinChung);
            // 
            // lblThongTinChung
            // 
            this.lblThongTinChung.AutoSize = true;
            this.lblThongTinChung.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThongTinChung.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblThongTinChung.Location = new System.Drawing.Point(12, 10);
            this.lblThongTinChung.Name = "lblThongTinChung";
            this.lblThongTinChung.Text = "";
            // 
            // tpLichSu
            // 
            this.tpLichSu.BackColor = System.Drawing.Color.White;
            this.tpLichSu.Location = new System.Drawing.Point(4, 26);
            this.tpLichSu.Name = "tpLichSu";
            this.tpLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tpLichSu.Size = new System.Drawing.Size(688, 158);
            this.tpLichSu.Text = "Lịch sử lưu trú (0)";
            this.tpLichSu.Controls.Add(this.dgvLichSu);
            // 
            // tpGhiChu
            // 
            this.tpGhiChu.BackColor = System.Drawing.Color.White;
            this.tpGhiChu.Location = new System.Drawing.Point(4, 26);
            this.tpGhiChu.Name = "tpGhiChu";
            this.tpGhiChu.Padding = new System.Windows.Forms.Padding(3);
            this.tpGhiChu.Size = new System.Drawing.Size(688, 158);
            this.tpGhiChu.Text = "Ghi chú";
            this.tpGhiChu.Controls.Add(this.lblGhiChuXem);
            // 
            // lblGhiChuXem
            // 
            this.lblGhiChuXem.AutoSize = true;
            this.lblGhiChuXem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChuXem.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblGhiChuXem.Location = new System.Drawing.Point(12, 10);
            this.lblGhiChuXem.MaximumSize = new System.Drawing.Size(660, 0);
            this.lblGhiChuXem.Name = "lblGhiChuXem";
            this.lblGhiChuXem.Size = new System.Drawing.Size(0, 17);
            this.lblGhiChuXem.Text = "";
            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.dgvKhachHang.ColumnHeadersHeight = 34;
            this.dgvKhachHang.Location = new System.Drawing.Point(0, 42);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.Size = new System.Drawing.Size(696, 270);
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AllowUserToResizeRows = false;
            this.dgvKhachHang.AutoGenerateColumns = false;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKhachHang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvKhachHang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKhachHang.EnableHeadersVisualStyles = false;
            this.dgvKhachHang.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dgvKhachHang styles
            // 
            dgvKhHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvKhHeaderStyle.BackColor = System.Drawing.Color.FromArgb(234, 241, 250);
            dgvKhHeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            dgvKhHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvKhHeaderStyle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dgvKhHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(234, 241, 250);
            dgvKhHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvKhHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle = dgvKhHeaderStyle;
            dgvKhCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvKhCellStyle.BackColor = System.Drawing.Color.White;
            dgvKhCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvKhCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvKhCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dgvKhCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 241, 252);
            dgvKhCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            dgvKhCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKhachHang.DefaultCellStyle = dgvKhCellStyle;
            this.dgvKhachHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKH,
            this.colHoTen,
            this.colDienThoai,
            this.colSoGiayTo,
            this.colSoLanLuuTru,
            this.colNhomKhach});
            this.dgvKhachHang.RowTemplate.Height = 40;
            // 
            // colMaKH
            // 
            this.colMaKH.DataPropertyName = "MaKH";
            this.colMaKH.HeaderText = "Mã KH";
            this.colMaKH.Name = "colMaKH";
            this.colMaKH.ReadOnly = true;
            this.colMaKH.Width = 96;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ và tên";
            this.colHoTen.FillWeight = 100F;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            this.colHoTen.MinimumWidth = 120;
            this.colHoTen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // colDienThoai
            // 
            this.colDienThoai.DataPropertyName = "DienThoai";
            this.colDienThoai.HeaderText = "Điện thoại";
            this.colDienThoai.Name = "colDienThoai";
            this.colDienThoai.ReadOnly = true;
            this.colDienThoai.Width = 120;
            // 
            // colSoGiayTo
            // 
            this.colSoGiayTo.DataPropertyName = "SoGiayTo";
            this.colSoGiayTo.HeaderText = "Giấy tờ";
            this.colSoGiayTo.Name = "colSoGiayTo";
            this.colSoGiayTo.ReadOnly = true;
            this.colSoGiayTo.Width = 130;
            // 
            // colSoLanLuuTru
            // 
            this.colSoLanLuuTru.DataPropertyName = "SoLanLuuTru";
            this.colSoLanLuuTru.HeaderText = "Lưu trú";
            this.colSoLanLuuTru.Name = "colSoLanLuuTru";
            this.colSoLanLuuTru.ReadOnly = true;
            this.colSoLanLuuTru.Width = 80;
            // 
            // colNhomKhach
            // 
            this.colNhomKhach.DataPropertyName = "NhomKhach";
            this.colNhomKhach.HeaderText = "Nhóm";
            this.colNhomKhach.Name = "colNhomKhach";
            this.colNhomKhach.ReadOnly = true;
            this.colNhomKhach.Width = 120;
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.ColumnHeadersVisible = false;
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSu.Location = new System.Drawing.Point(3, 3);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.Size = new System.Drawing.Size(682, 152);
            this.dgvLichSu.ColumnHeadersHeight = 34;
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.AllowUserToResizeRows = false;
            this.dgvLichSu.AutoGenerateColumns = false;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLichSu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLichSu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLichSu.EnableHeadersVisualStyles = false;
            this.dgvLichSu.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvLichSu.MultiSelect = false;
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dgvLichSu styles
            // 
            dgvLsHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvLsHeaderStyle.BackColor = System.Drawing.Color.FromArgb(234, 241, 250);
            dgvLsHeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            dgvLsHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvLsHeaderStyle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dgvLsHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(234, 241, 250);
            dgvLsHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvLsHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLichSu.ColumnHeadersDefaultCellStyle = dgvLsHeaderStyle;
            dgvLsCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvLsCellStyle.BackColor = System.Drawing.Color.White;
            dgvLsCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvLsCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvLsCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            dgvLsCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 241, 252);
            dgvLsCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 42, 74);
            dgvLsCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLichSu.DefaultCellStyle = dgvLsCellStyle;
            this.dgvLichSu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLsPhong,
            this.colLsThoiGian,
            this.colLsTrangThai,
            this.colLsTongTien});
            this.dgvLichSu.RowTemplate.Height = 40;
            // 
            // colLsPhong
            // 
            this.colLsPhong.DataPropertyName = "PhongHienThi";
            this.colLsPhong.HeaderText = "Phòng";
            this.colLsPhong.FillWeight = 34F;
            this.colLsPhong.Name = "colLsPhong";
            this.colLsPhong.ReadOnly = true;
            this.colLsPhong.MinimumWidth = 120;
            this.colLsPhong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // colLsThoiGian
            // 
            this.colLsThoiGian.DataPropertyName = "ThoiGianHienThi";
            this.colLsThoiGian.HeaderText = "Thời gian";
            this.colLsThoiGian.FillWeight = 40F;
            this.colLsThoiGian.Name = "colLsThoiGian";
            this.colLsThoiGian.ReadOnly = true;
            this.colLsThoiGian.MinimumWidth = 150;
            this.colLsThoiGian.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // colLsTrangThai
            // 
            this.colLsTrangThai.DataPropertyName = "TrangThaiHienThi";
            this.colLsTrangThai.HeaderText = "Trạng thái";
            this.colLsTrangThai.Name = "colLsTrangThai";
            this.colLsTrangThai.ReadOnly = true;
            this.colLsTrangThai.Width = 140;
            // 
            // colLsTongTien
            // 
            this.colLsTongTien.DataPropertyName = "TongTienHienThi";
            this.colLsTongTien.HeaderText = "Tổng tiền";
            this.colLsTongTien.Name = "colLsTongTien";
            this.colLsTongTien.ReadOnly = true;
            this.colLsTongTien.Width = 130;
            colTongTienStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colTongTienStyle.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.colLsTongTien.DefaultCellStyle = colTongTienStyle;
            // 
            // tmrTimKiem
            // 
            this.tmrTimKiem.Interval = 300;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // frmKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1090, 638);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlDanhSach);
            this.Controls.Add(this.btnThemKhachHang);
            this.Controls.Add(this.cboNhomKhach);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblNgayHienTai);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1106, 677);
            this.Name = "frmKhachHang";
            this.Text = "Quản lý khách hàng";
            this.Load += new System.EventHandler(this.frmKhachHang_Load);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.cboNhomKhach.SelectedIndexChanged += new System.EventHandler(this.cboNhomKhach_SelectedIndexChanged);
            this.btnThemKhachHang.Click += new System.EventHandler(this.btnThemKhachHang_Click);
            this.txtDienThoai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDienThoai_KeyPress);
            this.txtSoGiayTo.TextChanged += new System.EventHandler(this.txtSoGiayTo_TextChanged);
            this.txtSoGiayTo.Leave += new System.EventHandler(this.txtSoGiayTo_Leave);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.tmrTimKiem.Tick += new System.EventHandler(this.tmrTimKiem_Tick);
            this.dgvKhachHang.SelectionChanged += new System.EventHandler(this.dgvKhachHang_SelectionChanged);
            this.dgvKhachHang.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvKhachHang_CellPainting);
            this.dgvLichSu.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvLichSu_CellPainting);
            this.pnlDanhSach.ResumeLayout(false);
            this.pnlThongTin.ResumeLayout(false);
            this.tabChiTiet.ResumeLayout(false);
            this.tpThongTinChung.ResumeLayout(false);
            this.tpLichSu.ResumeLayout(false);
            this.tpGhiChu.ResumeLayout(false);
            this.pnlDanhSach.PerformLayout();
            this.pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblNgayHienTai;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox cboNhomKhach;
        private System.Windows.Forms.Button btnThemKhachHang;
        private System.Windows.Forms.Panel pnlDanhSach;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.Label lblTongHoSo;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblTieuDeThongTin;
        private System.Windows.Forms.Label lblNhomBadge;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblQuocTich;
        private System.Windows.Forms.TextBox txtQuocTich;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblSoGiayTo;
        private System.Windows.Forms.TextBox txtSoGiayTo;
        private System.Windows.Forms.Label lblLoiSoGiayTo;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.CheckBox chkNhanUuDai;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.TabControl tabChiTiet;
        private System.Windows.Forms.TabPage tpThongTinChung;
        private System.Windows.Forms.Label lblThongTinChung;
        private System.Windows.Forms.TabPage tpLichSu;
        private System.Windows.Forms.TabPage tpGhiChu;
        private System.Windows.Forms.Label lblGhiChuXem;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoGiayTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLanLuuTru;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNhomKhach;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLsPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLsThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLsTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLsTongTien;
        private System.Windows.Forms.Timer tmrTimKiem;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
