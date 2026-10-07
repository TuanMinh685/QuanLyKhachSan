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
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cardMainLeft = new System.Windows.Forms.Panel();
            this.lblRoomTitle = new System.Windows.Forms.Label();
            this.cboDatPhong = new System.Windows.Forms.ComboBox();
            this.dgvChiTietDichVu = new System.Windows.Forms.DataGridView();
            this.btnThemDichVu = new System.Windows.Forms.Button();
            this.btnXoaDongDichVu = new System.Windows.Forms.Button();
            this.cardBillingRight = new System.Windows.Forms.Panel();
            this.lblBillingHeader = new System.Windows.Forms.Label();
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
            this.divLine = new System.Windows.Forms.Panel();
            this.lblTongTienTitle = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblPTTT = new System.Windows.Forms.Label();
            this.cboPTTT = new System.Windows.Forms.ComboBox();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            this.cardMainLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDichVu)).BeginInit();
            this.cardBillingRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 2;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblMainLayout.Controls.Add(this.cardBillingRight, 1, 0);
            this.tblMainLayout.Controls.Add(this.cardMainLeft, 0, 0);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(20, 20);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 1;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Size = new System.Drawing.Size(1160, 680);
            this.tblMainLayout.TabIndex = 0;
            // 
            // cardMainLeft
            // 
            this.cardMainLeft.BackColor = System.Drawing.Color.White;
            this.cardMainLeft.Controls.Add(this.lblRoomTitle);
            this.cardMainLeft.Controls.Add(this.cboDatPhong);
            this.cardMainLeft.Controls.Add(this.dgvChiTietDichVu);
            this.cardMainLeft.Controls.Add(this.btnThemDichVu);
            this.cardMainLeft.Controls.Add(this.btnXoaDongDichVu);
            this.cardMainLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMainLeft.Location = new System.Drawing.Point(0, 0);
            this.cardMainLeft.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.cardMainLeft.Name = "cardMainLeft";
            this.cardMainLeft.Padding = new System.Windows.Forms.Padding(16);
            this.cardMainLeft.Size = new System.Drawing.Size(739, 680);
            this.cardMainLeft.TabIndex = 0;
            this.cardMainLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblRoomTitle
            // 
            this.lblRoomTitle.AutoSize = true;
            this.lblRoomTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblRoomTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRoomTitle.Location = new System.Drawing.Point(16, 20);
            this.lblRoomTitle.Name = "lblRoomTitle";
            this.lblRoomTitle.Size = new System.Drawing.Size(126, 25);
            this.lblRoomTitle.TabIndex = 0;
            this.lblRoomTitle.Text = "Chọn phòng:";
            // 
            // cboDatPhong
            // 
            this.cboDatPhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDatPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDatPhong.FormattingEnabled = true;
            this.cboDatPhong.Items.AddRange(new object[] {
            "Phòng 206 - Đỗ Thu Trang (Deluxe)",
            "Phòng 301 - Nguyễn Văn Hùng (Superior)",
            "Phòng 102 - Lê Minh Tuấn (Standard)"});
            this.cboDatPhong.Location = new System.Drawing.Point(150, 18);
            this.cboDatPhong.Name = "cboDatPhong";
            this.cboDatPhong.Size = new System.Drawing.Size(570, 31);
            this.cboDatPhong.TabIndex = 1;
            this.cboDatPhong.SelectedIndexChanged += new System.EventHandler(this.cboDatPhong_SelectedIndexChanged);
            // 
            // dgvChiTietDichVu
            // 
            this.dgvChiTietDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietDichVu.Location = new System.Drawing.Point(16, 68);
            this.dgvChiTietDichVu.Name = "dgvChiTietDichVu";
            this.dgvChiTietDichVu.RowHeadersWidth = 51;
            this.dgvChiTietDichVu.Size = new System.Drawing.Size(704, 535);
            this.dgvChiTietDichVu.TabIndex = 2;
            // 
            // btnThemDichVu
            // 
            this.btnThemDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThemDichVu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnThemDichVu.FlatAppearance.BorderSize = 0;
            this.btnThemDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDichVu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemDichVu.ForeColor = System.Drawing.Color.White;
            this.btnThemDichVu.Location = new System.Drawing.Point(16, 620);
            this.btnThemDichVu.Name = "btnThemDichVu";
            this.btnThemDichVu.Size = new System.Drawing.Size(165, 42);
            this.btnThemDichVu.TabIndex = 3;
            this.btnThemDichVu.Text = "+ Thêm dịch vụ";
            this.btnThemDichVu.UseVisualStyleBackColor = false;
            this.btnThemDichVu.Click += new System.EventHandler(this.btnThemDichVu_Click);
            // 
            // btnXoaDongDichVu
            // 
            this.btnXoaDongDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaDongDichVu.BackColor = System.Drawing.Color.White;
            this.btnXoaDongDichVu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnXoaDongDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDongDichVu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnXoaDongDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnXoaDongDichVu.Location = new System.Drawing.Point(195, 620);
            this.btnXoaDongDichVu.Name = "btnXoaDongDichVu";
            this.btnXoaDongDichVu.Size = new System.Drawing.Size(130, 42);
            this.btnXoaDongDichVu.TabIndex = 4;
            this.btnXoaDongDichVu.Text = "Xóa món";
            this.btnXoaDongDichVu.UseVisualStyleBackColor = false;
            this.btnXoaDongDichVu.Click += new System.EventHandler(this.btnXoaDongDichVu_Click);
            // 
            // cardBillingRight
            // 
            this.cardBillingRight.BackColor = System.Drawing.Color.White;
            this.cardBillingRight.Controls.Add(this.lblBillingHeader);
            this.cardBillingRight.Controls.Add(this.lblTienPhongTitle);
            this.cardBillingRight.Controls.Add(this.lblTienPhong);
            this.cardBillingRight.Controls.Add(this.lblTienDVTitle);
            this.cardBillingRight.Controls.Add(this.lblTienDichVu);
            this.cardBillingRight.Controls.Add(this.lblCocTitle);
            this.cardBillingRight.Controls.Add(this.lblCoc);
            this.cardBillingRight.Controls.Add(this.lblVoucherTitle);
            this.cardBillingRight.Controls.Add(this.txtVoucher);
            this.cardBillingRight.Controls.Add(this.btnApDungVoucher);
            this.cardBillingRight.Controls.Add(this.lblGiamGiaTitle);
            this.cardBillingRight.Controls.Add(this.lblTienGiamGia);
            this.cardBillingRight.Controls.Add(this.divLine);
            this.cardBillingRight.Controls.Add(this.lblTongTienTitle);
            this.cardBillingRight.Controls.Add(this.lblTongTien);
            this.cardBillingRight.Controls.Add(this.lblPTTT);
            this.cardBillingRight.Controls.Add(this.cboPTTT);
            this.cardBillingRight.Controls.Add(this.btnLapHoaDon);
            this.cardBillingRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardBillingRight.Location = new System.Drawing.Point(754, 0);
            this.cardBillingRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardBillingRight.Name = "cardBillingRight";
            this.cardBillingRight.Padding = new System.Windows.Forms.Padding(20);
            this.cardBillingRight.Size = new System.Drawing.Size(406, 680);
            this.cardBillingRight.TabIndex = 1;
            this.cardBillingRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblBillingHeader
            // 
            this.lblBillingHeader.AutoSize = true;
            this.lblBillingHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBillingHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblBillingHeader.Location = new System.Drawing.Point(20, 18);
            this.lblBillingHeader.Name = "lblBillingHeader";
            this.lblBillingHeader.Size = new System.Drawing.Size(155, 28);
            this.lblBillingHeader.TabIndex = 0;
            this.lblBillingHeader.Text = "Tóm tắt chi phí";
            // 
            // lblTienPhongTitle
            // 
            this.lblTienPhongTitle.AutoSize = true;
            this.lblTienPhongTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienPhongTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTienPhongTitle.Location = new System.Drawing.Point(20, 68);
            this.lblTienPhongTitle.Name = "lblTienPhongTitle";
            this.lblTienPhongTitle.Size = new System.Drawing.Size(149, 21);
            this.lblTienPhongTitle.TabIndex = 1;
            this.lblTienPhongTitle.Text = "Tiền phòng (2 đêm):";
            // 
            // lblTienPhong
            // 
            this.lblTienPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienPhong.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTienPhong.Location = new System.Drawing.Point(186, 68);
            this.lblTienPhong.Name = "lblTienPhong";
            this.lblTienPhong.Size = new System.Drawing.Size(195, 25);
            this.lblTienPhong.TabIndex = 2;
            this.lblTienPhong.Text = "2.900.000 đ";
            this.lblTienPhong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTienDVTitle
            // 
            this.lblTienDVTitle.AutoSize = true;
            this.lblTienDVTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienDVTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTienDVTitle.Location = new System.Drawing.Point(20, 110);
            this.lblTienDVTitle.Name = "lblTienDVTitle";
            this.lblTienDVTitle.Size = new System.Drawing.Size(96, 21);
            this.lblTienDVTitle.TabIndex = 3;
            this.lblTienDVTitle.Text = "Tiền dịch vụ:";
            // 
            // lblTienDichVu
            // 
            this.lblTienDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienDichVu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTienDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTienDichVu.Location = new System.Drawing.Point(186, 110);
            this.lblTienDichVu.Name = "lblTienDichVu";
            this.lblTienDichVu.Size = new System.Drawing.Size(195, 25);
            this.lblTienDichVu.TabIndex = 4;
            this.lblTienDichVu.Text = "0 đ";
            this.lblTienDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCocTitle
            // 
            this.lblCocTitle.AutoSize = true;
            this.lblCocTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCocTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCocTitle.Location = new System.Drawing.Point(20, 152);
            this.lblCocTitle.Name = "lblCocTitle";
            this.lblCocTitle.Size = new System.Drawing.Size(155, 21);
            this.lblCocTitle.TabIndex = 5;
            this.lblCocTitle.Text = "Đã cọc / Tạm ứng:";
            // 
            // lblCoc
            // 
            this.lblCoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoc.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCoc.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblCoc.Location = new System.Drawing.Point(186, 152);
            this.lblCoc.Name = "lblCoc";
            this.lblCoc.Size = new System.Drawing.Size(195, 25);
            this.lblCoc.TabIndex = 6;
            this.lblCoc.Text = "- 1.000.000 đ";
            this.lblCoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVoucherTitle
            // 
            this.lblVoucherTitle.AutoSize = true;
            this.lblVoucherTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVoucherTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblVoucherTitle.Location = new System.Drawing.Point(20, 202);
            this.lblVoucherTitle.Name = "lblVoucherTitle";
            this.lblVoucherTitle.Size = new System.Drawing.Size(96, 21);
            this.lblVoucherTitle.TabIndex = 7;
            this.lblVoucherTitle.Text = "Mã Voucher:";
            // 
            // txtVoucher
            // 
            this.txtVoucher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVoucher.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVoucher.Location = new System.Drawing.Point(125, 198);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Size = new System.Drawing.Size(145, 30);
            this.txtVoucher.TabIndex = 8;
            // 
            // btnApDungVoucher
            // 
            this.btnApDungVoucher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApDungVoucher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnApDungVoucher.FlatAppearance.BorderSize = 0;
            this.btnApDungVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApDungVoucher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApDungVoucher.ForeColor = System.Drawing.Color.White;
            this.btnApDungVoucher.Location = new System.Drawing.Point(281, 196);
            this.btnApDungVoucher.Name = "btnApDungVoucher";
            this.btnApDungVoucher.Size = new System.Drawing.Size(100, 34);
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
            this.lblGiamGiaTitle.Location = new System.Drawing.Point(20, 246);
            this.lblGiamGiaTitle.Name = "lblGiamGiaTitle";
            this.lblGiamGiaTitle.Size = new System.Drawing.Size(134, 21);
            this.lblGiamGiaTitle.TabIndex = 10;
            this.lblGiamGiaTitle.Text = "Giảm trừ voucher:";
            // 
            // lblTienGiamGia
            // 
            this.lblTienGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienGiamGia.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTienGiamGia.ForeColor = System.Drawing.Color.Crimson;
            this.lblTienGiamGia.Location = new System.Drawing.Point(186, 246);
            this.lblTienGiamGia.Name = "lblTienGiamGia";
            this.lblTienGiamGia.Size = new System.Drawing.Size(195, 25);
            this.lblTienGiamGia.TabIndex = 11;
            this.lblTienGiamGia.Text = "- 0 đ";
            this.lblTienGiamGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // divLine
            // 
            this.divLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.divLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.divLine.Location = new System.Drawing.Point(20, 288);
            this.divLine.Name = "divLine";
            this.divLine.Size = new System.Drawing.Size(361, 1);
            this.divLine.TabIndex = 12;
            // 
            // lblTongTienTitle
            // 
            this.lblTongTienTitle.AutoSize = true;
            this.lblTongTienTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTienTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTongTienTitle.Location = new System.Drawing.Point(20, 308);
            this.lblTongTienTitle.Name = "lblTongTienTitle";
            this.lblTongTienTitle.Size = new System.Drawing.Size(194, 25);
            this.lblTongTienTitle.TabIndex = 13;
            this.lblTongTienTitle.Text = "TỔNG THANH TOÁN:";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.lblTongTien.Location = new System.Drawing.Point(176, 300);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(205, 40);
            this.lblTongTien.TabIndex = 14;
            this.lblTongTien.Text = "1.900.000 đ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPTTT
            // 
            this.lblPTTT.AutoSize = true;
            this.lblPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPTTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPTTT.Location = new System.Drawing.Point(20, 365);
            this.lblPTTT.Name = "lblPTTT";
            this.lblPTTT.Size = new System.Drawing.Size(104, 21);
            this.lblPTTT.TabIndex = 15;
            this.lblPTTT.Text = "Phương thức:";
            // 
            // cboPTTT
            // 
            this.cboPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPTTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPTTT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPTTT.FormattingEnabled = true;
            this.cboPTTT.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản QR",
            "Thẻ tín dụng / POS"});
            this.cboPTTT.Location = new System.Drawing.Point(130, 360);
            this.cboPTTT.Name = "cboPTTT";
            this.cboPTTT.Size = new System.Drawing.Size(251, 31);
            this.cboPTTT.TabIndex = 16;
            // 
            // btnLapHoaDon
            // 
            this.btnLapHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.btnLapHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapHoaDon.FlatAppearance.BorderSize = 0;
            this.btnLapHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDon.Location = new System.Drawing.Point(20, 610);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(361, 52);
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
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHoaDon";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn & Thanh Toán";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.tblMainLayout.ResumeLayout(false);
            this.cardMainLeft.ResumeLayout(false);
            this.cardMainLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDichVu)).EndInit();
            this.cardBillingRight.ResumeLayout(false);
            this.cardBillingRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.Panel cardMainLeft;
        private System.Windows.Forms.Label lblRoomTitle;
        private System.Windows.Forms.ComboBox cboDatPhong;
        private System.Windows.Forms.DataGridView dgvChiTietDichVu;
        private System.Windows.Forms.Button btnThemDichVu;
        private System.Windows.Forms.Button btnXoaDongDichVu;
        private System.Windows.Forms.Panel cardBillingRight;
        private System.Windows.Forms.Label lblBillingHeader;
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
        private System.Windows.Forms.Panel divLine;
        private System.Windows.Forms.Label lblTongTienTitle;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblPTTT;
        private System.Windows.Forms.ComboBox cboPTTT;
        private System.Windows.Forms.Button btnLapHoaDon;
    }
}