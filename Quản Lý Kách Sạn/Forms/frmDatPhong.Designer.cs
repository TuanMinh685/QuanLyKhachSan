namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmDatPhong
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
            lblTieuDe = new Label();
            lblPhuDe = new Label();
            lblNgayHienTai = new Label();
            pnlTimPhong = new Panel();
            btnTimPhong = new Button();
            cboLoaiPhong = new ComboBox();
            cboTreEm = new ComboBox();
            cboNguoiLon = new ComboBox();
            dtpNgayTra = new DateTimePicker();
            lblNgayTra = new Label();
            dtpNgayNhan = new DateTimePicker();
            label4 = new Label();
            pnlKetQua = new Panel();
            pnlNutLenh = new Panel();
            lblPhimTat = new Label();
            btnHuyPhong = new Button();
            btnTraPhong = new Button();
            btnNhanPhong = new Button();
            btnDatPhong = new Button();
            dgvPhong = new DataGridView();
            lblGhiChuGia = new Label();
            lblSoPhongTrong = new Label();
            lblPhongPhuHop = new Label();
            pnlTomTat = new Panel();
            rdoThe = new RadioButton();
            rdoChuyenKhoan = new RadioButton();
            rdoTienMat = new RadioButton();
            lblVND = new Label();
            txtTienCoc = new TextBox();
            lblCocToiThieu = new Label();
            lblCocTen = new Label();
            lblTongCongTien = new Label();
            lblTongCongTen = new Label();
            lblKe = new Label();
            lblThueTien = new Label();
            lblThueTen = new Label();
            lblPhuThuTien = new Label();
            lblPhuThuTen = new Label();
            lblTienPhong = new Label();
            lblDongPhong = new Label();
            label1 = new Label();
            pnlKhach = new Panel();
            btnDoiKhach = new Button();
            lblSdtMa = new Label();
            lblTenKhach = new Label();
            lblAvatar = new Label();
            lblKhachDatPhong = new Label();
            lblTrangThaiTao = new Label();
            lblTomTat = new Label();
            pnlTimPhong.SuspendLayout();
            pnlKetQua.SuspendLayout();
            pnlNutLenh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            pnlTomTat.SuspendLayout();
            pnlKhach.SuspendLayout();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 16F);
            lblTieuDe.ForeColor = Color.FromArgb(15, 42, 71);
            lblTieuDe.Location = new Point(20, 14);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(208, 30);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "Đặt phòng & lưu trú";
            lblTieuDe.UseMnemonic = false;
            // 
            // lblPhuDe
            // 
            lblPhuDe.AutoSize = true;
            lblPhuDe.Location = new Point(22, 50);
            lblPhuDe.Name = "lblPhuDe";
            lblPhuDe.Size = new Size(299, 15);
            lblPhuDe.TabIndex = 1;
            lblPhuDe.Text = "Tìm phòng trống, tạo đặt chỗ và xử lý nhận / trả phòng";
            // 
            // lblNgayHienTai
            // 
            lblNgayHienTai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNgayHienTai.AutoSize = true;
            lblNgayHienTai.BorderStyle = BorderStyle.FixedSingle;
            lblNgayHienTai.Location = new Point(904, 18);
            lblNgayHienTai.Name = "lblNgayHienTai";
            lblNgayHienTai.Size = new Size(110, 17);
            lblNgayHienTai.TabIndex = 2;
            lblNgayHienTai.Text = "Thứ Ba, 29/09/2026";
            lblNgayHienTai.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTimPhong
            // 
            pnlTimPhong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTimPhong.BorderStyle = BorderStyle.FixedSingle;
            pnlTimPhong.Controls.Add(btnTimPhong);
            pnlTimPhong.Controls.Add(cboLoaiPhong);
            pnlTimPhong.Controls.Add(cboTreEm);
            pnlTimPhong.Controls.Add(cboNguoiLon);
            pnlTimPhong.Controls.Add(dtpNgayTra);
            pnlTimPhong.Controls.Add(lblNgayTra);
            pnlTimPhong.Controls.Add(dtpNgayNhan);
            pnlTimPhong.Controls.Add(label4);
            pnlTimPhong.Location = new Point(20, 74);
            pnlTimPhong.Name = "pnlTimPhong";
            pnlTimPhong.Size = new Size(1050, 66);
            pnlTimPhong.TabIndex = 3;
            // 
            // btnTimPhong
            // 
            btnTimPhong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTimPhong.BackColor = Color.FromArgb(245, 158, 11);
            btnTimPhong.Cursor = Cursors.Hand;
            btnTimPhong.FlatStyle = FlatStyle.Flat;
            btnTimPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTimPhong.ForeColor = Color.FromArgb(12, 40, 72);
            btnTimPhong.Location = new Point(906, 16);
            btnTimPhong.Name = "btnTimPhong";
            btnTimPhong.Size = new Size(134, 34);
            btnTimPhong.TabIndex = 7;
            btnTimPhong.Text = "Tìm phòng trống";
            btnTimPhong.UseVisualStyleBackColor = false;
            btnTimPhong.Click += btnTimPhong_Click;
            // 
            // cboLoaiPhong
            // 
            cboLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiPhong.FormattingEnabled = true;
            cboLoaiPhong.Location = new Point(596, 30);
            cboLoaiPhong.Name = "cboLoaiPhong";
            cboLoaiPhong.Size = new Size(150, 23);
            cboLoaiPhong.TabIndex = 6;
            // 
            // cboTreEm
            // 
            cboTreEm.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTreEm.FormattingEnabled = true;
            cboTreEm.Items.AddRange(new object[] { "0 trẻ em, 1 trẻ em, 2 trẻ em, 3 trẻ em" });
            cboTreEm.Location = new Point(467, 30);
            cboTreEm.Name = "cboTreEm";
            cboTreEm.Size = new Size(120, 23);
            cboTreEm.TabIndex = 5;
            // 
            // cboNguoiLon
            // 
            cboNguoiLon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNguoiLon.FormattingEnabled = true;
            cboNguoiLon.Items.AddRange(new object[] { "1 người lớn, 2 người lớn, 3 người lớn, 4 người lớn" });
            cboNguoiLon.Location = new Point(328, 30);
            cboNguoiLon.Name = "cboNguoiLon";
            cboNguoiLon.Size = new Size(130, 23);
            cboNguoiLon.TabIndex = 4;
            // 
            // dtpNgayTra
            // 
            dtpNgayTra.Format = DateTimePickerFormat.Custom;
            dtpNgayTra.Location = new Point(169, 30);
            dtpNgayTra.Name = "dtpNgayTra";
            dtpNgayTra.Size = new Size(150, 23);
            dtpNgayTra.TabIndex = 3;
            // 
            // lblNgayTra
            // 
            lblNgayTra.AutoSize = true;
            lblNgayTra.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNgayTra.Location = new Point(169, 8);
            lblNgayTra.Name = "lblNgayTra";
            lblNgayTra.Size = new Size(100, 15);
            lblNgayTra.TabIndex = 2;
            lblNgayTra.Text = "Ngày trả phòng *";
            // 
            // dtpNgayNhan
            // 
            dtpNgayNhan.Format = DateTimePickerFormat.Custom;
            dtpNgayNhan.Location = new Point(10, 30);
            dtpNgayNhan.Name = "dtpNgayNhan";
            dtpNgayNhan.Size = new Size(150, 23);
            dtpNgayNhan.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 8);
            label4.Name = "label4";
            label4.Size = new Size(116, 13);
            label4.TabIndex = 0;
            label4.Text = "Ngày nhận phòng *";
            label4.Click += label4_Click;
            // 
            // pnlKetQua
            // 
            pnlKetQua.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlKetQua.BorderStyle = BorderStyle.FixedSingle;
            pnlKetQua.Controls.Add(pnlNutLenh);
            pnlKetQua.Controls.Add(dgvPhong);
            pnlKetQua.Controls.Add(lblGhiChuGia);
            pnlKetQua.Controls.Add(lblSoPhongTrong);
            pnlKetQua.Controls.Add(lblPhongPhuHop);
            pnlKetQua.Location = new Point(20, 154);
            pnlKetQua.Name = "pnlKetQua";
            pnlKetQua.Size = new Size(688, 456);
            pnlKetQua.TabIndex = 4;
            // 
            // pnlNutLenh
            // 
            pnlNutLenh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlNutLenh.Controls.Add(lblPhimTat);
            pnlNutLenh.Controls.Add(btnHuyPhong);
            pnlNutLenh.Controls.Add(btnTraPhong);
            pnlNutLenh.Controls.Add(btnNhanPhong);
            pnlNutLenh.Controls.Add(btnDatPhong);
            pnlNutLenh.Location = new Point(1, 406);
            pnlNutLenh.Name = "pnlNutLenh";
            pnlNutLenh.Size = new Size(684, 48);
            pnlNutLenh.TabIndex = 4;
            // 
            // lblPhimTat
            // 
            lblPhimTat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPhimTat.Font = new Font("Segoe UI", 8F);
            lblPhimTat.Location = new Point(480, 16);
            lblPhimTat.Name = "lblPhimTat";
            lblPhimTat.Size = new Size(196, 16);
            lblPhimTat.TabIndex = 4;
            lblPhimTat.Text = "F4 Đặt • F5 Nhận • F6 Trả";
            lblPhimTat.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHuyPhong
            // 
            btnHuyPhong.FlatAppearance.BorderColor = Color.FromArgb(229, 72, 77);
            btnHuyPhong.FlatStyle = FlatStyle.Flat;
            btnHuyPhong.ForeColor = Color.FromArgb(198, 40, 40);
            btnHuyPhong.Location = new Point(330, 8);
            btnHuyPhong.Name = "btnHuyPhong";
            btnHuyPhong.Size = new Size(100, 32);
            btnHuyPhong.TabIndex = 3;
            btnHuyPhong.Text = "Hủy phòng";
            btnHuyPhong.UseVisualStyleBackColor = true;
            btnHuyPhong.Click += btnHuyPhong_Click;
            // 
            // btnTraPhong
            // 
            btnTraPhong.FlatAppearance.BorderColor = Color.FromArgb(184, 197, 210);
            btnTraPhong.FlatStyle = FlatStyle.Flat;
            btnTraPhong.Location = new Point(228, 8);
            btnTraPhong.Name = "btnTraPhong";
            btnTraPhong.Size = new Size(96, 32);
            btnTraPhong.TabIndex = 2;
            btnTraPhong.Text = "Trả phòng";
            btnTraPhong.UseVisualStyleBackColor = true;
            btnTraPhong.Click += btnTraPhong_Click;
            // 
            // btnNhanPhong
            // 
            btnNhanPhong.FlatAppearance.BorderColor = Color.FromArgb(184, 197, 210);
            btnNhanPhong.FlatStyle = FlatStyle.Flat;
            btnNhanPhong.Location = new Point(118, 8);
            btnNhanPhong.Name = "btnNhanPhong";
            btnNhanPhong.Size = new Size(104, 32);
            btnNhanPhong.TabIndex = 1;
            btnNhanPhong.Text = "Nhận phòng";
            btnNhanPhong.UseVisualStyleBackColor = true;
            btnNhanPhong.Click += btnNhanPhong_Click;
            // 
            // btnDatPhong
            // 
            btnDatPhong.BackColor = Color.FromArgb(245, 158, 11);
            btnDatPhong.FlatAppearance.BorderSize = 0;
            btnDatPhong.FlatStyle = FlatStyle.Flat;
            btnDatPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDatPhong.ForeColor = Color.FromArgb(12, 40, 72);
            btnDatPhong.Location = new Point(12, 8);
            btnDatPhong.Name = "btnDatPhong";
            btnDatPhong.Size = new Size(100, 32);
            btnDatPhong.TabIndex = 0;
            btnDatPhong.Text = "Đặt phòng";
            btnDatPhong.UseVisualStyleBackColor = false;
            btnDatPhong.Click += btnDatPhong_Click;
            // 
            // dgvPhong
            // 
            dgvPhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhong.Location = new Point(1, 44);
            dgvPhong.Name = "dgvPhong";
            dgvPhong.Size = new Size(648, 360);
            dgvPhong.TabIndex = 3;
            dgvPhong.CellContentClick += dgvPhong_CellContentClick;
            // 
            // lblGhiChuGia
            // 
            lblGhiChuGia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGhiChuGia.Font = new Font("Segoe UI", 8F);
            lblGhiChuGia.Location = new Point(400, 16);
            lblGhiChuGia.Name = "lblGhiChuGia";
            lblGhiChuGia.Size = new Size(276, 16);
            lblGhiChuGia.TabIndex = 2;
            lblGhiChuGia.Text = "label1";
            lblGhiChuGia.TextAlign = ContentAlignment.MiddleRight;
            lblGhiChuGia.UseMnemonic = false;
            // 
            // lblSoPhongTrong
            // 
            lblSoPhongTrong.AutoSize = true;
            lblSoPhongTrong.BackColor = Color.FromArgb(217, 243, 230);
            lblSoPhongTrong.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSoPhongTrong.ForeColor = Color.FromArgb(15, 122, 74);
            lblSoPhongTrong.Location = new Point(150, 14);
            lblSoPhongTrong.Name = "lblSoPhongTrong";
            lblSoPhongTrong.Size = new Size(83, 13);
            lblSoPhongTrong.TabIndex = 1;
            lblSoPhongTrong.Text = "0 phòng trống";
            // 
            // lblPhongPhuHop
            // 
            lblPhongPhuHop.AutoSize = true;
            lblPhongPhuHop.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPhongPhuHop.Location = new Point(12, 10);
            lblPhongPhuHop.Name = "lblPhongPhuHop";
            lblPhongPhuHop.Size = new Size(130, 23);
            lblPhongPhuHop.TabIndex = 0;
            lblPhongPhuHop.Text = "Phòng phù hợp";
            // 
            // pnlTomTat
            // 
            pnlTomTat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlTomTat.BorderStyle = BorderStyle.FixedSingle;
            pnlTomTat.Controls.Add(rdoThe);
            pnlTomTat.Controls.Add(rdoChuyenKhoan);
            pnlTomTat.Controls.Add(rdoTienMat);
            pnlTomTat.Controls.Add(lblVND);
            pnlTomTat.Controls.Add(txtTienCoc);
            pnlTomTat.Controls.Add(lblCocToiThieu);
            pnlTomTat.Controls.Add(lblCocTen);
            pnlTomTat.Controls.Add(lblTongCongTien);
            pnlTomTat.Controls.Add(lblTongCongTen);
            pnlTomTat.Controls.Add(lblKe);
            pnlTomTat.Controls.Add(lblThueTien);
            pnlTomTat.Controls.Add(lblThueTen);
            pnlTomTat.Controls.Add(lblPhuThuTien);
            pnlTomTat.Controls.Add(lblPhuThuTen);
            pnlTomTat.Controls.Add(lblTienPhong);
            pnlTomTat.Controls.Add(lblDongPhong);
            pnlTomTat.Controls.Add(label1);
            pnlTomTat.Controls.Add(pnlKhach);
            pnlTomTat.Controls.Add(lblKhachDatPhong);
            pnlTomTat.Controls.Add(lblTrangThaiTao);
            pnlTomTat.Controls.Add(lblTomTat);
            pnlTomTat.Location = new Point(720, 154);
            pnlTomTat.Name = "pnlTomTat";
            pnlTomTat.Size = new Size(350, 456);
            pnlTomTat.TabIndex = 5;
            // 
            // rdoThe
            // 
            rdoThe.Appearance = Appearance.Button;
            rdoThe.AutoSize = true;
            rdoThe.Checked = true;
            rdoThe.FlatAppearance.CheckedBackColor = Color.FromArgb(214, 228, 240);
            rdoThe.FlatStyle = FlatStyle.Flat;
            rdoThe.Location = new Point(238, 312);
            rdoThe.Name = "rdoThe";
            rdoThe.Size = new Size(39, 27);
            rdoThe.TabIndex = 20;
            rdoThe.TabStop = true;
            rdoThe.Text = "Thẻ";
            rdoThe.TextAlign = ContentAlignment.MiddleCenter;
            rdoThe.UseVisualStyleBackColor = true;
            // 
            // rdoChuyenKhoan
            // 
            rdoChuyenKhoan.Appearance = Appearance.Button;
            rdoChuyenKhoan.AutoSize = true;
            rdoChuyenKhoan.FlatAppearance.CheckedBackColor = Color.FromArgb(214, 228, 240);
            rdoChuyenKhoan.FlatStyle = FlatStyle.Flat;
            rdoChuyenKhoan.Location = new Point(116, 312);
            rdoChuyenKhoan.Name = "rdoChuyenKhoan";
            rdoChuyenKhoan.Size = new Size(96, 27);
            rdoChuyenKhoan.TabIndex = 19;
            rdoChuyenKhoan.Text = "Chuyển khoản";
            rdoChuyenKhoan.TextAlign = ContentAlignment.MiddleCenter;
            rdoChuyenKhoan.UseVisualStyleBackColor = true;
            // 
            // rdoTienMat
            // 
            rdoTienMat.Appearance = Appearance.Button;
            rdoTienMat.AutoSize = true;
            rdoTienMat.FlatAppearance.CheckedBackColor = Color.FromArgb(214, 228, 240);
            rdoTienMat.FlatStyle = FlatStyle.Flat;
            rdoTienMat.Location = new Point(14, 312);
            rdoTienMat.Name = "rdoTienMat";
            rdoTienMat.Size = new Size(66, 27);
            rdoTienMat.TabIndex = 18;
            rdoTienMat.Text = "Tiền mặt";
            rdoTienMat.TextAlign = ContentAlignment.MiddleCenter;
            rdoTienMat.UseVisualStyleBackColor = true;
            // 
            // lblVND
            // 
            lblVND.AutoSize = true;
            lblVND.Location = new Point(292, 278);
            lblVND.Name = "lblVND";
            lblVND.Size = new Size(31, 15);
            lblVND.TabIndex = 17;
            lblVND.Text = "VNĐ";
            // 
            // txtTienCoc
            // 
            txtTienCoc.BorderStyle = BorderStyle.FixedSingle;
            txtTienCoc.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTienCoc.Location = new Point(14, 272);
            txtTienCoc.Name = "txtTienCoc";
            txtTienCoc.Size = new Size(270, 27);
            txtTienCoc.TabIndex = 16;
            txtTienCoc.TextAlign = HorizontalAlignment.Right;
            // 
            // lblCocToiThieu
            // 
            lblCocToiThieu.Font = new Font("Segoe UI", 8F);
            lblCocToiThieu.ForeColor = Color.FromArgb(217, 119, 6);
            lblCocToiThieu.Location = new Point(236, 252);
            lblCocToiThieu.Name = "lblCocToiThieu";
            lblCocToiThieu.Size = new Size(100, 15);
            lblCocToiThieu.TabIndex = 15;
            lblCocToiThieu.Text = "Tối thiểu 30%";
            lblCocToiThieu.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCocTen
            // 
            lblCocTen.AutoSize = true;
            lblCocTen.Location = new Point(14, 252);
            lblCocTen.Name = "lblCocTen";
            lblCocTen.Size = new Size(103, 15);
            lblCocTen.TabIndex = 14;
            lblCocTen.Text = "Đặt cọc / tạm ứng";
            // 
            // lblTongCongTien
            // 
            lblTongCongTien.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongCongTien.ForeColor = Color.FromArgb(12, 40, 72);
            lblTongCongTien.Location = new Point(236, 208);
            lblTongCongTien.Name = "lblTongCongTien";
            lblTongCongTien.Size = new Size(100, 23);
            lblTongCongTien.TabIndex = 13;
            lblTongCongTien.Text = "0 đ";
            lblTongCongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTongCongTen
            // 
            lblTongCongTen.AutoSize = true;
            lblTongCongTen.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongCongTen.Location = new Point(14, 214);
            lblTongCongTen.Name = "lblTongCongTen";
            lblTongCongTen.Size = new Size(74, 17);
            lblTongCongTen.TabIndex = 12;
            lblTongCongTen.Text = "Tổng cộng";
            // 
            // lblKe
            // 
            lblKe.BackColor = Color.FromArgb(216, 224, 232);
            lblKe.Location = new Point(14, 200);
            lblKe.Name = "lblKe";
            lblKe.Size = new Size(332, 1);
            lblKe.TabIndex = 11;
            lblKe.Text = "label2";
            // 
            // lblThueTien
            // 
            lblThueTien.Location = new Point(186, 174);
            lblThueTien.Name = "lblThueTien";
            lblThueTien.Size = new Size(150, 15);
            lblThueTien.TabIndex = 10;
            lblThueTien.Text = "0 đ";
            lblThueTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblThueTen
            // 
            lblThueTen.AutoSize = true;
            lblThueTen.Location = new Point(14, 174);
            lblThueTen.Name = "lblThueTen";
            lblThueTen.Size = new Size(109, 15);
            lblThueTen.TabIndex = 9;
            lblThueTen.Text = "Thuế & phí dịch vụ";
            lblThueTen.UseMnemonic = false;
            // 
            // lblPhuThuTien
            // 
            lblPhuThuTien.Location = new Point(186, 152);
            lblPhuThuTien.Name = "lblPhuThuTien";
            lblPhuThuTien.Size = new Size(150, 16);
            lblPhuThuTien.TabIndex = 8;
            lblPhuThuTien.Text = "0 đ";
            lblPhuThuTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPhuThuTen
            // 
            lblPhuThuTen.AutoSize = true;
            lblPhuThuTen.Location = new Point(14, 152);
            lblPhuThuTen.Name = "lblPhuThuTen";
            lblPhuThuTen.Size = new Size(49, 15);
            lblPhuThuTen.TabIndex = 7;
            lblPhuThuTen.Text = "Phụ thu";
            // 
            // lblTienPhong
            // 
            lblTienPhong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTienPhong.Location = new Point(186, 130);
            lblTienPhong.Name = "lblTienPhong";
            lblTienPhong.Size = new Size(150, 16);
            lblTienPhong.TabIndex = 6;
            lblTienPhong.Text = "label2";
            lblTienPhong.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDongPhong
            // 
            lblDongPhong.AutoSize = true;
            lblDongPhong.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDongPhong.Location = new Point(14, 130);
            lblDongPhong.Name = "lblDongPhong";
            lblDongPhong.Size = new Size(101, 13);
            lblDongPhong.TabIndex = 5;
            lblDongPhong.Text = "Chưa chọn phòng";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // pnlKhach
            // 
            pnlKhach.Controls.Add(btnDoiKhach);
            pnlKhach.Controls.Add(lblSdtMa);
            pnlKhach.Controls.Add(lblTenKhach);
            pnlKhach.Controls.Add(lblAvatar);
            pnlKhach.Location = new Point(12, 68);
            pnlKhach.Name = "pnlKhach";
            pnlKhach.Size = new Size(326, 50);
            pnlKhach.TabIndex = 3;
            // 
            // btnDoiKhach
            // 
            btnDoiKhach.BackColor = Color.FromArgb(243, 246, 249);
            btnDoiKhach.Cursor = Cursors.Hand;
            btnDoiKhach.FlatAppearance.BorderSize = 0;
            btnDoiKhach.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDoiKhach.Location = new Point(288, 12);
            btnDoiKhach.Name = "btnDoiKhach";
            btnDoiKhach.Size = new Size(28, 26);
            btnDoiKhach.TabIndex = 3;
            btnDoiKhach.Text = "✎";
            btnDoiKhach.UseVisualStyleBackColor = false;
            btnDoiKhach.Click += btnDoiKhach_Click;
            // 
            // lblSdtMa
            // 
            lblSdtMa.AutoSize = true;
            lblSdtMa.Location = new Point(48, 27);
            lblSdtMa.Name = "lblSdtMa";
            lblSdtMa.Size = new Size(12, 15);
            lblSdtMa.TabIndex = 2;
            lblSdtMa.Text = "-";
            // 
            // lblTenKhach
            // 
            lblTenKhach.AutoSize = true;
            lblTenKhach.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenKhach.Location = new Point(48, 8);
            lblTenKhach.Name = "lblTenKhach";
            lblTenKhach.Size = new Size(101, 15);
            lblTenKhach.TabIndex = 1;
            lblTenKhach.Text = "Chưa chọn khách";
            // 
            // lblAvatar
            // 
            lblAvatar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAvatar.ForeColor = Color.FromArgb(25, 81, 128);
            lblAvatar.Location = new Point(10, 10);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(30, 30);
            lblAvatar.TabIndex = 0;
            lblAvatar.Text = "?";
            lblAvatar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblKhachDatPhong
            // 
            lblKhachDatPhong.AutoSize = true;
            lblKhachDatPhong.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKhachDatPhong.Location = new Point(14, 48);
            lblKhachDatPhong.Name = "lblKhachDatPhong";
            lblKhachDatPhong.Size = new Size(113, 13);
            lblKhachDatPhong.TabIndex = 2;
            lblKhachDatPhong.Text = "KHÁCH ĐẶT PHÒNG";
            // 
            // lblTrangThaiTao
            // 
            lblTrangThaiTao.AutoSize = true;
            lblTrangThaiTao.BackColor = Color.FromArgb(254, 243, 199);
            lblTrangThaiTao.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTrangThaiTao.ForeColor = Color.FromArgb(180, 83, 9);
            lblTrangThaiTao.Location = new Point(270, 14);
            lblTrangThaiTao.Name = "lblTrangThaiTao";
            lblTrangThaiTao.Size = new Size(55, 13);
            lblTrangThaiTao.TabIndex = 1;
            lblTrangThaiTao.Text = "Đang tạo";
            // 
            // lblTomTat
            // 
            lblTomTat.AutoSize = true;
            lblTomTat.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTomTat.Location = new Point(14, 10);
            lblTomTat.Name = "lblTomTat";
            lblTomTat.Size = new Size(111, 21);
            lblTomTat.TabIndex = 0;
            lblTomTat.Text = "Tóm tắt chi phí";
            // 
            // frmDatPhong
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1074, 599);
            Controls.Add(pnlTomTat);
            Controls.Add(pnlKetQua);
            Controls.Add(pnlTimPhong);
            Controls.Add(lblNgayHienTai);
            Controls.Add(lblPhuDe);
            Controls.Add(lblTieuDe);
            ForeColor = Color.FromArgb(100, 116, 135);
            Name = "frmDatPhong";
            Text = "frmDatPhong";
            Load += frmDatPhong_Load;
            pnlTimPhong.ResumeLayout(false);
            pnlTimPhong.PerformLayout();
            pnlKetQua.ResumeLayout(false);
            pnlKetQua.PerformLayout();
            pnlNutLenh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            pnlTomTat.ResumeLayout(false);
            pnlTomTat.PerformLayout();
            pnlKhach.ResumeLayout(false);
            pnlKhach.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblPhuDe;
        private Label lblNgayHienTai;
        private Panel pnlTimPhong;
        private Label label4;
        private Label lblNgayTra;
        private DateTimePicker dtpNgayNhan;
        private ComboBox cboTreEm;
        private ComboBox cboNguoiLon;
        private DateTimePicker dtpNgayTra;
        private Button btnTimPhong;
        private ComboBox cboLoaiPhong;
        private Panel pnlKetQua;
        private Label lblPhongPhuHop;
        private Label lblSoPhongTrong;
        private DataGridView dgvPhong;
        private Label lblGhiChuGia;
        private Panel pnlNutLenh;
        private Button btnNhanPhong;
        private Button btnDatPhong;
        private Button btnTraPhong;
        private Button btnHuyPhong;
        private Label lblPhimTat;
        private Panel pnlTomTat;
        private Label lblKhachDatPhong;
        private Label lblTrangThaiTao;
        private Label lblTomTat;
        private Panel pnlKhach;
        private Label lblSdtMa;
        private Label lblTenKhach;
        private Label lblAvatar;
        private Label lblTienPhong;
        private Label lblDongPhong;
        private Label label1;
        private Button btnDoiKhach;
        private Label lblPhuThuTen;
        private Label lblThueTien;
        private Label lblThueTen;
        private Label lblPhuThuTien;
        private Label lblTongCongTen;
        private Label lblKe;
        private Label lblTongCongTien;
        private Label lblCocToiThieu;
        private Label lblCocTen;
        private RadioButton rdoChuyenKhoan;
        private RadioButton rdoTienMat;
        private Label lblVND;
        private TextBox txtTienCoc;
        private RadioButton rdoThe;
    }
}