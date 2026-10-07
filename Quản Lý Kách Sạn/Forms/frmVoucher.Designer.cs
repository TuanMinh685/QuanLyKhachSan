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
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cardLeft = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblIconSearch = new System.Windows.Forms.Label();
            this.pnlGridWrapper = new System.Windows.Forms.Panel();
            this.dgvVoucher = new System.Windows.Forms.DataGridView();
            this.cardRight = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblCardSubtitle = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblCode = new System.Windows.Forms.Label();
            this.pnlCodeWrap = new System.Windows.Forms.Panel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblLoaiGiam = new System.Windows.Forms.Label();
            this.pnlLoaiGiam = new System.Windows.Forms.Panel();
            this.rdoPhanTram = new System.Windows.Forms.RadioButton();
            this.rdoSoTien = new System.Windows.Forms.RadioButton();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.pnlGiaTriWrap = new System.Windows.Forms.Panel();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.tbl2ColInputs = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColDonToiThieu = new System.Windows.Forms.Panel();
            this.lblDonToiThieu = new System.Windows.Forms.Label();
            this.pnlDonToiThieuWrap = new System.Windows.Forms.Panel();
            this.txtDonToiThieu = new System.Windows.Forms.TextBox();
            this.pnlColSoLuong = new System.Windows.Forms.Panel();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.pnlSoLuongWrap = new System.Windows.Forms.Panel();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.tbl2ColDates = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColNgayBD = new System.Windows.Forms.Panel();
            this.lblNgayBD = new System.Windows.Forms.Label();
            this.pnlNgayBDWrap = new System.Windows.Forms.Panel();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.pnlColNgayKT = new System.Windows.Forms.Panel();
            this.lblNgayKT = new System.Windows.Forms.Label();
            this.pnlNgayKTWrap = new System.Windows.Forms.Panel();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            this.cardLeft.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            this.cardRight.SuspendLayout();
            this.pnlCodeWrap.SuspendLayout();
            this.pnlLoaiGiam.SuspendLayout();
            this.pnlGiaTriWrap.SuspendLayout();
            this.tbl2ColInputs.SuspendLayout();
            this.pnlColDonToiThieu.SuspendLayout();
            this.pnlDonToiThieuWrap.SuspendLayout();
            this.pnlColSoLuong.SuspendLayout();
            this.pnlSoLuongWrap.SuspendLayout();
            this.tbl2ColDates.SuspendLayout();
            this.pnlColNgayBD.SuspendLayout();
            this.pnlNgayBDWrap.SuspendLayout();
            this.pnlColNgayKT.SuspendLayout();
            this.pnlNgayKTWrap.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 2;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblMainLayout.Controls.Add(this.cardLeft, 0, 0);
            this.tblMainLayout.Controls.Add(this.cardRight, 1, 0);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(20, 20);
            this.tblMainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 1;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Size = new System.Drawing.Size(1240, 720);
            this.tblMainLayout.TabIndex = 0;
            // 
            // cardLeft
            // 
            this.cardLeft.BackColor = System.Drawing.Color.White;
            this.cardLeft.Controls.Add(this.pnlGridWrapper);
            this.cardLeft.Controls.Add(this.pnlSearch);
            this.cardLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLeft.Location = new System.Drawing.Point(0, 0);
            this.cardLeft.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardLeft.Name = "cardLeft";
            this.cardLeft.Padding = new System.Windows.Forms.Padding(20);
            this.cardLeft.Size = new System.Drawing.Size(777, 720);
            this.cardLeft.TabIndex = 0;
            this.cardLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.txtTimKiem);
            this.pnlSearch.Controls.Add(this.lblIconSearch);
            this.pnlSearch.Location = new System.Drawing.Point(20, 20);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlSearch.Size = new System.Drawing.Size(737, 38);
            this.pnlSearch.TabIndex = 0;
            this.pnlSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // lblIconSearch
            // 
            this.lblIconSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIconSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIconSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblIconSearch.Location = new System.Drawing.Point(12, 0);
            this.lblIconSearch.Name = "lblIconSearch";
            this.lblIconSearch.Size = new System.Drawing.Size(26, 38);
            this.lblIconSearch.TabIndex = 0;
            this.lblIconSearch.Text = "🔍";
            this.lblIconSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.BackColor = System.Drawing.Color.White;
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTimKiem.Location = new System.Drawing.Point(44, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm theo mã code voucher, tên chương trình...";
            this.txtTimKiem.Size = new System.Drawing.Size(681, 22);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlGridWrapper
            // 
            this.pnlGridWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridWrapper.Controls.Add(this.dgvVoucher);
            this.pnlGridWrapper.Location = new System.Drawing.Point(20, 72);
            this.pnlGridWrapper.Name = "pnlGridWrapper";
            this.pnlGridWrapper.Size = new System.Drawing.Size(737, 628);
            this.pnlGridWrapper.TabIndex = 1;
            // 
            // dgvVoucher
            // 
            this.dgvVoucher.BackgroundColor = System.Drawing.Color.White;
            this.dgvVoucher.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVoucher.Location = new System.Drawing.Point(0, 0);
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.RowHeadersWidth = 51;
            this.dgvVoucher.Size = new System.Drawing.Size(737, 628);
            this.dgvVoucher.TabIndex = 0;
            this.dgvVoucher.SelectionChanged += new System.EventHandler(this.dgvVoucher_SelectionChanged);
            // 
            // cardRight
            // 
            this.cardRight.AutoScroll = true;
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.lblCardTitle);
            this.cardRight.Controls.Add(this.lblCardSubtitle);
            this.cardRight.Controls.Add(this.pnlDivider);
            this.cardRight.Controls.Add(this.lblCode);
            this.cardRight.Controls.Add(this.pnlCodeWrap);
            this.cardRight.Controls.Add(this.lblLoaiGiam);
            this.cardRight.Controls.Add(this.pnlLoaiGiam);
            this.cardRight.Controls.Add(this.lblGiaTri);
            this.cardRight.Controls.Add(this.pnlGiaTriWrap);
            this.cardRight.Controls.Add(this.tbl2ColInputs);
            this.cardRight.Controls.Add(this.tbl2ColDates);
            this.cardRight.Controls.Add(this.chkTrangThai);
            this.cardRight.Controls.Add(this.pnlBottomButtons);
            this.cardRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRight.Location = new System.Drawing.Point(793, 0);
            this.cardRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardRight.Name = "cardRight";
            this.cardRight.Padding = new System.Windows.Forms.Padding(24);
            this.cardRight.Size = new System.Drawing.Size(447, 720);
            this.cardRight.TabIndex = 1;
            this.cardRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCardTitle.Location = new System.Drawing.Point(22, 20);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(193, 30);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Thông tin voucher";
            // 
            // lblCardSubtitle
            // 
            this.lblCardSubtitle.AutoSize = true;
            this.lblCardSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardSubtitle.Location = new System.Drawing.Point(24, 50);
            this.lblCardSubtitle.Name = "lblCardSubtitle";
            this.lblCardSubtitle.Size = new System.Drawing.Size(252, 20);
            this.lblCardSubtitle.TabIndex = 1;
            this.lblCardSubtitle.Text = "Cấu hình chiết khấu cho khách lưu trú";
            // 
            // pnlDivider
            // 
            this.pnlDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDivider.Location = new System.Drawing.Point(24, 80);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(399, 1);
            this.pnlDivider.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCode.Location = new System.Drawing.Point(22, 95);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(117, 20);
            this.lblCode.TabIndex = 3;
            this.lblCode.Text = "Mã voucher (Code) *";
            // 
            // pnlCodeWrap
            // 
            this.pnlCodeWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCodeWrap.BackColor = System.Drawing.Color.White;
            this.pnlCodeWrap.Controls.Add(this.txtCode);
            this.pnlCodeWrap.Location = new System.Drawing.Point(24, 120);
            this.pnlCodeWrap.Name = "pnlCodeWrap";
            this.pnlCodeWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlCodeWrap.Size = new System.Drawing.Size(399, 38);
            this.pnlCodeWrap.TabIndex = 4;
            this.pnlCodeWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtCode.Location = new System.Drawing.Point(12, 8);
            this.txtCode.Name = "txtCode";
            this.txtCode.PlaceholderText = "VÍ DỤ: CHAOHET2026";
            this.txtCode.Size = new System.Drawing.Size(375, 22);
            this.txtCode.TabIndex = 0;
            // 
            // lblLoaiGiam
            // 
            this.lblLoaiGiam.AutoSize = true;
            this.lblLoaiGiam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblLoaiGiam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblLoaiGiam.Location = new System.Drawing.Point(22, 172);
            this.lblLoaiGiam.Name = "lblLoaiGiam";
            this.lblLoaiGiam.Size = new System.Drawing.Size(126, 20);
            this.lblLoaiGiam.TabIndex = 5;
            this.lblLoaiGiam.Text = "Hình thức áp dụng *";
            // 
            // pnlLoaiGiam
            // 
            this.pnlLoaiGiam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLoaiGiam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlLoaiGiam.Controls.Add(this.rdoPhanTram);
            this.pnlLoaiGiam.Controls.Add(this.rdoSoTien);
            this.pnlLoaiGiam.Location = new System.Drawing.Point(24, 196);
            this.pnlLoaiGiam.Name = "pnlLoaiGiam";
            this.pnlLoaiGiam.Size = new System.Drawing.Size(399, 40);
            this.pnlLoaiGiam.TabIndex = 6;
            this.pnlLoaiGiam.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // rdoPhanTram
            // 
            this.rdoPhanTram.AutoSize = true;
            this.rdoPhanTram.Checked = true;
            this.rdoPhanTram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoPhanTram.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular);
            this.rdoPhanTram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rdoPhanTram.Location = new System.Drawing.Point(16, 9);
            this.rdoPhanTram.Name = "rdoPhanTram";
            this.rdoPhanTram.Size = new System.Drawing.Size(124, 25);
            this.rdoPhanTram.TabIndex = 0;
            this.rdoPhanTram.TabStop = true;
            this.rdoPhanTram.Text = "Phần trăm (%)";
            this.rdoPhanTram.UseVisualStyleBackColor = true;
            // 
            // rdoSoTien
            // 
            this.rdoSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoSoTien.AutoSize = true;
            this.rdoSoTien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoSoTien.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular);
            this.rdoSoTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rdoSoTien.Location = new System.Drawing.Point(232, 9);
            this.rdoSoTien.Name = "rdoSoTien";
            this.rdoSoTien.Size = new System.Drawing.Size(155, 25);
            this.rdoSoTien.TabIndex = 1;
            this.rdoSoTien.Text = "Số tiền cố định (đ)";
            this.rdoSoTien.UseVisualStyleBackColor = true;
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.AutoSize = true;
            this.lblGiaTri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblGiaTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblGiaTri.Location = new System.Drawing.Point(22, 248);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(85, 20);
            this.lblGiaTri.TabIndex = 7;
            this.lblGiaTri.Text = "Mức giảm *";
            // 
            // pnlGiaTriWrap
            // 
            this.pnlGiaTriWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGiaTriWrap.BackColor = System.Drawing.Color.White;
            this.pnlGiaTriWrap.Controls.Add(this.txtGiaTri);
            this.pnlGiaTriWrap.Location = new System.Drawing.Point(24, 272);
            this.pnlGiaTriWrap.Name = "pnlGiaTriWrap";
            this.pnlGiaTriWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlGiaTriWrap.Size = new System.Drawing.Size(399, 38);
            this.pnlGiaTriWrap.TabIndex = 8;
            this.pnlGiaTriWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaTri.BackColor = System.Drawing.Color.White;
            this.txtGiaTri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGiaTri.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtGiaTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.txtGiaTri.Location = new System.Drawing.Point(12, 8);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.PlaceholderText = "Nhập mức giảm...";
            this.txtGiaTri.Size = new System.Drawing.Size(375, 22);
            this.txtGiaTri.TabIndex = 0;
            // 
            // tbl2ColInputs
            // 
            this.tbl2ColInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbl2ColInputs.ColumnCount = 2;
            this.tbl2ColInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColInputs.Controls.Add(this.pnlColDonToiThieu, 0, 0);
            this.tbl2ColInputs.Controls.Add(this.pnlColSoLuong, 1, 0);
            this.tbl2ColInputs.Location = new System.Drawing.Point(21, 320);
            this.tbl2ColInputs.Name = "tbl2ColInputs";
            this.tbl2ColInputs.RowCount = 1;
            this.tbl2ColInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl2ColInputs.Size = new System.Drawing.Size(405, 68);
            this.tbl2ColInputs.TabIndex = 9;
            // 
            // pnlColDonToiThieu
            // 
            this.pnlColDonToiThieu.Controls.Add(this.lblDonToiThieu);
            this.pnlColDonToiThieu.Controls.Add(this.pnlDonToiThieuWrap);
            this.pnlColDonToiThieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColDonToiThieu.Location = new System.Drawing.Point(0, 0);
            this.pnlColDonToiThieu.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlColDonToiThieu.Name = "pnlColDonToiThieu";
            this.pnlColDonToiThieu.Size = new System.Drawing.Size(194, 68);
            this.pnlColDonToiThieu.TabIndex = 0;
            // 
            // lblDonToiThieu
            // 
            this.lblDonToiThieu.AutoSize = true;
            this.lblDonToiThieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblDonToiThieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDonToiThieu.Location = new System.Drawing.Point(0, 0);
            this.lblDonToiThieu.Name = "lblDonToiThieu";
            this.lblDonToiThieu.Size = new System.Drawing.Size(97, 20);
            this.lblDonToiThieu.TabIndex = 0;
            this.lblDonToiThieu.Text = "Đơn tối thiểu";
            // 
            // pnlDonToiThieuWrap
            // 
            this.pnlDonToiThieuWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDonToiThieuWrap.BackColor = System.Drawing.Color.White;
            this.pnlDonToiThieuWrap.Controls.Add(this.txtDonToiThieu);
            this.pnlDonToiThieuWrap.Location = new System.Drawing.Point(0, 24);
            this.pnlDonToiThieuWrap.Name = "pnlDonToiThieuWrap";
            this.pnlDonToiThieuWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlDonToiThieuWrap.Size = new System.Drawing.Size(194, 38);
            this.pnlDonToiThieuWrap.TabIndex = 1;
            this.pnlDonToiThieuWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtDonToiThieu
            // 
            this.txtDonToiThieu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonToiThieu.BackColor = System.Drawing.Color.White;
            this.txtDonToiThieu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDonToiThieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDonToiThieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDonToiThieu.Location = new System.Drawing.Point(10, 8);
            this.txtDonToiThieu.Name = "txtDonToiThieu";
            this.txtDonToiThieu.PlaceholderText = "0 đ";
            this.txtDonToiThieu.Size = new System.Drawing.Size(174, 22);
            this.txtDonToiThieu.TabIndex = 0;
            // 
            // pnlColSoLuong
            // 
            this.pnlColSoLuong.Controls.Add(this.lblSoLuong);
            this.pnlColSoLuong.Controls.Add(this.pnlSoLuongWrap);
            this.pnlColSoLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColSoLuong.Location = new System.Drawing.Point(202, 0);
            this.pnlColSoLuong.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColSoLuong.Name = "pnlColSoLuong";
            this.pnlColSoLuong.Size = new System.Drawing.Size(203, 68);
            this.pnlColSoLuong.TabIndex = 1;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSoLuong.Location = new System.Drawing.Point(8, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(69, 20);
            this.lblSoLuong.TabIndex = 0;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // pnlSoLuongWrap
            // 
            this.pnlSoLuongWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSoLuongWrap.BackColor = System.Drawing.Color.White;
            this.pnlSoLuongWrap.Controls.Add(this.txtSoLuong);
            this.pnlSoLuongWrap.Location = new System.Drawing.Point(8, 24);
            this.pnlSoLuongWrap.Name = "pnlSoLuongWrap";
            this.pnlSoLuongWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlSoLuongWrap.Size = new System.Drawing.Size(195, 38);
            this.pnlSoLuongWrap.TabIndex = 1;
            this.pnlSoLuongWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoLuong.BackColor = System.Drawing.Color.White;
            this.txtSoLuong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSoLuong.Location = new System.Drawing.Point(10, 8);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.PlaceholderText = "100";
            this.txtSoLuong.Size = new System.Drawing.Size(175, 22);
            this.txtSoLuong.TabIndex = 0;
            // 
            // tbl2ColDates
            // 
            this.tbl2ColDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbl2ColDates.ColumnCount = 2;
            this.tbl2ColDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColDates.Controls.Add(this.pnlColNgayBD, 0, 0);
            this.tbl2ColDates.Controls.Add(this.pnlColNgayKT, 1, 0);
            this.tbl2ColDates.Location = new System.Drawing.Point(21, 396);
            this.tbl2ColDates.Name = "tbl2ColDates";
            this.tbl2ColDates.RowCount = 1;
            this.tbl2ColDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl2ColDates.Size = new System.Drawing.Size(405, 68);
            this.tbl2ColDates.TabIndex = 10;
            // 
            // pnlColNgayBD
            // 
            this.pnlColNgayBD.Controls.Add(this.lblNgayBD);
            this.pnlColNgayBD.Controls.Add(this.pnlNgayBDWrap);
            this.pnlColNgayBD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColNgayBD.Location = new System.Drawing.Point(0, 0);
            this.pnlColNgayBD.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlColNgayBD.Name = "pnlColNgayBD";
            this.pnlColNgayBD.Size = new System.Drawing.Size(194, 68);
            this.pnlColNgayBD.TabIndex = 0;
            // 
            // lblNgayBD
            // 
            this.lblNgayBD.AutoSize = true;
            this.lblNgayBD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNgayBD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNgayBD.Location = new System.Drawing.Point(0, 0);
            this.lblNgayBD.Name = "lblNgayBD";
            this.lblNgayBD.Size = new System.Drawing.Size(99, 20);
            this.lblNgayBD.TabIndex = 0;
            this.lblNgayBD.Text = "Ngày bắt đầu";
            // 
            // pnlNgayBDWrap
            // 
            this.pnlNgayBDWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNgayBDWrap.BackColor = System.Drawing.Color.White;
            this.pnlNgayBDWrap.Controls.Add(this.dtpNgayBatDau);
            this.pnlNgayBDWrap.Location = new System.Drawing.Point(0, 24);
            this.pnlNgayBDWrap.Name = "pnlNgayBDWrap";
            this.pnlNgayBDWrap.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlNgayBDWrap.Size = new System.Drawing.Size(194, 38);
            this.pnlNgayBDWrap.TabIndex = 1;
            this.pnlNgayBDWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.CalendarFont = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayBatDau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(4, 3);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(186, 29);
            this.dtpNgayBatDau.TabIndex = 0;
            // 
            // pnlColNgayKT
            // 
            this.pnlColNgayKT.Controls.Add(this.lblNgayKT);
            this.pnlColNgayKT.Controls.Add(this.pnlNgayKTWrap);
            this.pnlColNgayKT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColNgayKT.Location = new System.Drawing.Point(202, 0);
            this.pnlColNgayKT.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColNgayKT.Name = "pnlColNgayKT";
            this.pnlColNgayKT.Size = new System.Drawing.Size(203, 68);
            this.pnlColNgayKT.TabIndex = 1;
            // 
            // lblNgayKT
            // 
            this.lblNgayKT.AutoSize = true;
            this.lblNgayKT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNgayKT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNgayKT.Location = new System.Drawing.Point(8, 0);
            this.lblNgayKT.Name = "lblNgayKT";
            this.lblNgayKT.Size = new System.Drawing.Size(100, 20);
            this.lblNgayKT.TabIndex = 0;
            this.lblNgayKT.Text = "Ngày kết thúc";
            // 
            // pnlNgayKTWrap
            // 
            this.pnlNgayKTWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNgayKTWrap.BackColor = System.Drawing.Color.White;
            this.pnlNgayKTWrap.Controls.Add(this.dtpNgayHetHan);
            this.pnlNgayKTWrap.Location = new System.Drawing.Point(8, 24);
            this.pnlNgayKTWrap.Name = "pnlNgayKTWrap";
            this.pnlNgayKTWrap.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlNgayKTWrap.Size = new System.Drawing.Size(195, 38);
            this.pnlNgayKTWrap.TabIndex = 1;
            this.pnlNgayKTWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.CalendarFont = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayHetHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(4, 3);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(187, 29);
            this.dtpNgayHetHan.TabIndex = 0;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.chkTrangThai.Location = new System.Drawing.Point(24, 480);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(248, 25);
            this.chkTrangThai.TabIndex = 11;
            this.chkTrangThai.Text = "Kích hoạt hiệu lực của voucher";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomButtons.Controls.Add(this.btnXoa);
            this.pnlBottomButtons.Controls.Add(this.btnLamMoi);
            this.pnlBottomButtons.Controls.Add(this.btnLuu);
            this.pnlBottomButtons.Location = new System.Drawing.Point(24, 524);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(399, 44);
            this.pnlBottomButtons.TabIndex = 12;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(267, 0);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(132, 40);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu Voucher";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            this.btnLuu.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLamMoi.Location = new System.Drawing.Point(167, 0);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(92, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.Location = new System.Drawing.Point(0, 0);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 40);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa mã";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVoucher";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Voucher";
            this.Load += new System.EventHandler(this.frmVoucher_Load);
            this.tblMainLayout.ResumeLayout(false);
            this.cardLeft.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.pnlCodeWrap.ResumeLayout(false);
            this.pnlCodeWrap.PerformLayout();
            this.pnlLoaiGiam.ResumeLayout(false);
            this.pnlLoaiGiam.PerformLayout();
            this.pnlGiaTriWrap.ResumeLayout(false);
            this.pnlGiaTriWrap.PerformLayout();
            this.tbl2ColInputs.ResumeLayout(false);
            this.pnlColDonToiThieu.ResumeLayout(false);
            this.pnlColDonToiThieu.PerformLayout();
            this.pnlDonToiThieuWrap.ResumeLayout(false);
            this.pnlDonToiThieuWrap.PerformLayout();
            this.pnlColSoLuong.ResumeLayout(false);
            this.pnlColSoLuong.PerformLayout();
            this.pnlSoLuongWrap.ResumeLayout(false);
            this.pnlSoLuongWrap.PerformLayout();
            this.tbl2ColDates.ResumeLayout(false);
            this.pnlColNgayBD.ResumeLayout(false);
            this.pnlColNgayBD.PerformLayout();
            this.pnlNgayBDWrap.ResumeLayout(false);
            this.pnlColNgayKT.ResumeLayout(false);
            this.pnlColNgayKT.PerformLayout();
            this.pnlNgayKTWrap.ResumeLayout(false);
            this.pnlBottomButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.Panel cardLeft;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblIconSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlGridWrapper;
        private System.Windows.Forms.DataGridView dgvVoucher;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCardSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Panel pnlCodeWrap;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblLoaiGiam;
        private System.Windows.Forms.Panel pnlLoaiGiam;
        private System.Windows.Forms.RadioButton rdoPhanTram;
        private System.Windows.Forms.RadioButton rdoSoTien;
        private System.Windows.Forms.Label lblGiaTri;
        private System.Windows.Forms.Panel pnlGiaTriWrap;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.TableLayoutPanel tbl2ColInputs;
        private System.Windows.Forms.Panel pnlColDonToiThieu;
        private System.Windows.Forms.Label lblDonToiThieu;
        private System.Windows.Forms.Panel pnlDonToiThieuWrap;
        private System.Windows.Forms.TextBox txtDonToiThieu;
        private System.Windows.Forms.Panel pnlColSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Panel pnlSoLuongWrap;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TableLayoutPanel tbl2ColDates;
        private System.Windows.Forms.Panel pnlColNgayBD;
        private System.Windows.Forms.Label lblNgayBD;
        private System.Windows.Forms.Panel pnlNgayBDWrap;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Panel pnlColNgayKT;
        private System.Windows.Forms.Label lblNgayKT;
        private System.Windows.Forms.Panel pnlNgayKTWrap;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
    }
}