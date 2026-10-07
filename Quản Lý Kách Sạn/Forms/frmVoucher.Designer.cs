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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tblMainLayout = new TableLayoutPanel();
            cardLeft = new Panel();
            pnlGridWrapper = new Panel();
            dgvVoucher = new DataGridView();
            pnlSearch = new Panel();
            txtTimKiem = new TextBox();
            lblIconSearch = new Label();
            cardRight = new Panel();
            lblCardTitle = new Label();
            lblCardSubtitle = new Label();
            pnlDivider = new Panel();
            lblCode = new Label();
            pnlCodeWrap = new Panel();
            txtCode = new TextBox();
            lblLoaiGiam = new Label();
            pnlLoaiGiam = new Panel();
            rdoPhanTram = new RadioButton();
            rdoSoTien = new RadioButton();
            lblGiaTri = new Label();
            pnlGiaTriWrap = new Panel();
            txtGiaTri = new TextBox();
            tbl2ColInputs = new TableLayoutPanel();
            pnlColDonToiThieu = new Panel();
            lblDonToiThieu = new Label();
            pnlDonToiThieuWrap = new Panel();
            txtDonToiThieu = new TextBox();
            pnlColSoLuong = new Panel();
            lblSoLuong = new Label();
            pnlSoLuongWrap = new Panel();
            txtSoLuong = new TextBox();
            tbl2ColDates = new TableLayoutPanel();
            pnlColNgayBD = new Panel();
            lblNgayBD = new Label();
            pnlNgayBDWrap = new Panel();
            dtpNgayBatDau = new DateTimePicker();
            pnlColNgayKT = new Panel();
            lblNgayKT = new Label();
            pnlNgayKTWrap = new Panel();
            dtpNgayHetHan = new DateTimePicker();
            chkTrangThai = new CheckBox();
            pnlBottomButtons = new Panel();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnLuu = new Button();
            tblMainLayout.SuspendLayout();
            cardLeft.SuspendLayout();
            pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVoucher).BeginInit();
            pnlSearch.SuspendLayout();
            cardRight.SuspendLayout();
            pnlCodeWrap.SuspendLayout();
            pnlLoaiGiam.SuspendLayout();
            pnlGiaTriWrap.SuspendLayout();
            tbl2ColInputs.SuspendLayout();
            pnlColDonToiThieu.SuspendLayout();
            pnlDonToiThieuWrap.SuspendLayout();
            pnlColSoLuong.SuspendLayout();
            pnlSoLuongWrap.SuspendLayout();
            tbl2ColDates.SuspendLayout();
            pnlColNgayBD.SuspendLayout();
            pnlNgayBDWrap.SuspendLayout();
            pnlColNgayKT.SuspendLayout();
            pnlNgayKTWrap.SuspendLayout();
            pnlBottomButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tblMainLayout
            // 
            tblMainLayout.ColumnCount = 2;
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tblMainLayout.Controls.Add(cardLeft, 0, 0);
            tblMainLayout.Controls.Add(cardRight, 1, 0);
            tblMainLayout.Dock = DockStyle.Fill;
            tblMainLayout.Location = new Point(20, 20);
            tblMainLayout.Margin = new Padding(0);
            tblMainLayout.Name = "tblMainLayout";
            tblMainLayout.RowCount = 1;
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMainLayout.Size = new Size(1240, 720);
            tblMainLayout.TabIndex = 0;
            // 
            // cardLeft
            // 
            cardLeft.BackColor = Color.White;
            cardLeft.Controls.Add(pnlGridWrapper);
            cardLeft.Controls.Add(pnlSearch);
            cardLeft.Dock = DockStyle.Fill;
            cardLeft.Location = new Point(0, 0);
            cardLeft.Margin = new Padding(0, 0, 16, 0);
            cardLeft.Name = "cardLeft";
            cardLeft.Padding = new Padding(20);
            cardLeft.Size = new Size(777, 720);
            cardLeft.TabIndex = 0;
            cardLeft.Paint += DrawFigmaCard;
            // 
            // pnlGridWrapper
            // 
            pnlGridWrapper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlGridWrapper.Controls.Add(dgvVoucher);
            pnlGridWrapper.Location = new Point(20, 72);
            pnlGridWrapper.Name = "pnlGridWrapper";
            pnlGridWrapper.Size = new Size(737, 628);
            pnlGridWrapper.TabIndex = 1;
            // 
            // dgvVoucher
            // 
            dgvVoucher.BackgroundColor = Color.White;
            dgvVoucher.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvVoucher.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvVoucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvVoucher.DefaultCellStyle = dataGridViewCellStyle2;
            dgvVoucher.Dock = DockStyle.Fill;
            dgvVoucher.Location = new Point(0, 0);
            dgvVoucher.Name = "dgvVoucher";
            dgvVoucher.RowHeadersWidth = 51;
            dgvVoucher.Size = new Size(737, 628);
            dgvVoucher.TabIndex = 0;
            dgvVoucher.SelectionChanged += dgvVoucher_SelectionChanged;
            // 
            // pnlSearch
            // 
            pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(txtTimKiem);
            pnlSearch.Controls.Add(lblIconSearch);
            pnlSearch.Location = new Point(20, 20);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(12, 0, 12, 0);
            pnlSearch.Size = new Size(737, 38);
            pnlSearch.TabIndex = 0;
            pnlSearch.Paint += DrawFigmaInputBox;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.BackColor = Color.White;
            txtTimKiem.BorderStyle = BorderStyle.None;
            txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            txtTimKiem.ForeColor = Color.FromArgb(30, 41, 59);
            txtTimKiem.Location = new Point(44, 9);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Tìm kiếm theo mã code voucher, tên chương trình...";
            txtTimKiem.Size = new Size(681, 22);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // lblIconSearch
            // 
            lblIconSearch.Dock = DockStyle.Left;
            lblIconSearch.Font = new Font("Segoe UI", 10F);
            lblIconSearch.ForeColor = Color.FromArgb(148, 163, 184);
            lblIconSearch.Location = new Point(12, 0);
            lblIconSearch.Name = "lblIconSearch";
            lblIconSearch.Size = new Size(26, 38);
            lblIconSearch.TabIndex = 0;
            lblIconSearch.Text = "🔍";
            lblIconSearch.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardRight
            // 
            cardRight.AutoScroll = true;
            cardRight.BackColor = Color.White;
            cardRight.Controls.Add(lblCardTitle);
            cardRight.Controls.Add(lblCardSubtitle);
            cardRight.Controls.Add(pnlDivider);
            cardRight.Controls.Add(lblCode);
            cardRight.Controls.Add(pnlCodeWrap);
            cardRight.Controls.Add(lblLoaiGiam);
            cardRight.Controls.Add(pnlLoaiGiam);
            cardRight.Controls.Add(lblGiaTri);
            cardRight.Controls.Add(pnlGiaTriWrap);
            cardRight.Controls.Add(tbl2ColInputs);
            cardRight.Controls.Add(tbl2ColDates);
            cardRight.Controls.Add(chkTrangThai);
            cardRight.Controls.Add(pnlBottomButtons);
            cardRight.Dock = DockStyle.Fill;
            cardRight.Location = new Point(793, 0);
            cardRight.Margin = new Padding(0);
            cardRight.Name = "cardRight";
            cardRight.Padding = new Padding(24);
            cardRight.Size = new Size(447, 720);
            cardRight.TabIndex = 1;
            cardRight.Paint += DrawFigmaCard;
            // 
            // lblCardTitle
            // 
            lblCardTitle.AutoSize = true;
            lblCardTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblCardTitle.Location = new Point(22, 20);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.Size = new Size(195, 30);
            lblCardTitle.TabIndex = 0;
            lblCardTitle.Text = "Thông tin voucher";
            // 
            // lblCardSubtitle
            // 
            lblCardSubtitle.AutoSize = true;
            lblCardSubtitle.Font = new Font("Segoe UI", 9F);
            lblCardSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblCardSubtitle.Location = new Point(24, 50);
            lblCardSubtitle.Name = "lblCardSubtitle";
            lblCardSubtitle.Size = new Size(254, 20);
            lblCardSubtitle.TabIndex = 1;
            lblCardSubtitle.Text = "Cấu hình chiết khấu cho khách lưu trú";
            // 
            // pnlDivider
            // 
            pnlDivider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlDivider.BackColor = Color.FromArgb(241, 245, 249);
            pnlDivider.Location = new Point(24, 80);
            pnlDivider.Name = "pnlDivider";
            pnlDivider.Size = new Size(399, 1);
            pnlDivider.TabIndex = 2;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 9F);
            lblCode.ForeColor = Color.FromArgb(51, 65, 85);
            lblCode.Location = new Point(22, 95);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(145, 20);
            lblCode.TabIndex = 3;
            lblCode.Text = "Mã voucher (Code) *";
            // 
            // pnlCodeWrap
            // 
            pnlCodeWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCodeWrap.BackColor = Color.White;
            pnlCodeWrap.Controls.Add(txtCode);
            pnlCodeWrap.Location = new Point(24, 120);
            pnlCodeWrap.Name = "pnlCodeWrap";
            pnlCodeWrap.Padding = new Padding(12, 0, 12, 0);
            pnlCodeWrap.Size = new Size(399, 38);
            pnlCodeWrap.TabIndex = 4;
            pnlCodeWrap.Paint += DrawFigmaInputBox;
            // 
            // txtCode
            // 
            txtCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCode.BackColor = Color.White;
            txtCode.BorderStyle = BorderStyle.None;
            txtCode.CharacterCasing = CharacterCasing.Upper;
            txtCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtCode.ForeColor = Color.FromArgb(15, 23, 42);
            txtCode.Location = new Point(12, 8);
            txtCode.Name = "txtCode";
            txtCode.PlaceholderText = "VÍ DỤ: CHAOHET2026";
            txtCode.Size = new Size(375, 22);
            txtCode.TabIndex = 0;
            // 
            // lblLoaiGiam
            // 
            lblLoaiGiam.AutoSize = true;
            lblLoaiGiam.Font = new Font("Segoe UI", 9F);
            lblLoaiGiam.ForeColor = Color.FromArgb(51, 65, 85);
            lblLoaiGiam.Location = new Point(22, 172);
            lblLoaiGiam.Name = "lblLoaiGiam";
            lblLoaiGiam.Size = new Size(142, 20);
            lblLoaiGiam.TabIndex = 5;
            lblLoaiGiam.Text = "Hình thức áp dụng *";
            // 
            // pnlLoaiGiam
            // 
            pnlLoaiGiam.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlLoaiGiam.BackColor = Color.FromArgb(248, 250, 252);
            pnlLoaiGiam.Controls.Add(rdoPhanTram);
            pnlLoaiGiam.Controls.Add(rdoSoTien);
            pnlLoaiGiam.Location = new Point(24, 196);
            pnlLoaiGiam.Name = "pnlLoaiGiam";
            pnlLoaiGiam.Size = new Size(399, 40);
            pnlLoaiGiam.TabIndex = 6;
            pnlLoaiGiam.Paint += DrawFigmaInputBox;
            // 
            // rdoPhanTram
            // 
            rdoPhanTram.AutoSize = true;
            rdoPhanTram.Checked = true;
            rdoPhanTram.Cursor = Cursors.Hand;
            rdoPhanTram.Font = new Font("Segoe UI", 9.25F);
            rdoPhanTram.ForeColor = Color.FromArgb(15, 23, 42);
            rdoPhanTram.Location = new Point(16, 9);
            rdoPhanTram.Name = "rdoPhanTram";
            rdoPhanTram.Size = new Size(130, 25);
            rdoPhanTram.TabIndex = 0;
            rdoPhanTram.TabStop = true;
            rdoPhanTram.Text = "Phần trăm (%)";
            rdoPhanTram.UseVisualStyleBackColor = true;
            // 
            // rdoSoTien
            // 
            rdoSoTien.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoSoTien.AutoSize = true;
            rdoSoTien.Cursor = Cursors.Hand;
            rdoSoTien.Font = new Font("Segoe UI", 9.25F);
            rdoSoTien.ForeColor = Color.FromArgb(15, 23, 42);
            rdoSoTien.Location = new Point(230, 9);
            rdoSoTien.Name = "rdoSoTien";
            rdoSoTien.Size = new Size(157, 25);
            rdoSoTien.TabIndex = 1;
            rdoSoTien.Text = "Số tiền cố định (đ)";
            rdoSoTien.UseVisualStyleBackColor = true;
            // 
            // lblGiaTri
            // 
            lblGiaTri.AutoSize = true;
            lblGiaTri.Font = new Font("Segoe UI", 9F);
            lblGiaTri.ForeColor = Color.FromArgb(51, 65, 85);
            lblGiaTri.Location = new Point(22, 248);
            lblGiaTri.Name = "lblGiaTri";
            lblGiaTri.Size = new Size(86, 20);
            lblGiaTri.TabIndex = 7;
            lblGiaTri.Text = "Mức giảm *";
            // 
            // pnlGiaTriWrap
            // 
            pnlGiaTriWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlGiaTriWrap.BackColor = Color.White;
            pnlGiaTriWrap.Controls.Add(txtGiaTri);
            pnlGiaTriWrap.Location = new Point(24, 272);
            pnlGiaTriWrap.Name = "pnlGiaTriWrap";
            pnlGiaTriWrap.Padding = new Padding(12, 0, 12, 0);
            pnlGiaTriWrap.Size = new Size(399, 38);
            pnlGiaTriWrap.TabIndex = 8;
            pnlGiaTriWrap.Paint += DrawFigmaInputBox;
            // 
            // txtGiaTri
            // 
            txtGiaTri.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtGiaTri.BackColor = Color.White;
            txtGiaTri.BorderStyle = BorderStyle.None;
            txtGiaTri.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtGiaTri.ForeColor = Color.FromArgb(234, 88, 12);
            txtGiaTri.Location = new Point(12, 8);
            txtGiaTri.Name = "txtGiaTri";
            txtGiaTri.PlaceholderText = "Nhập mức giảm...";
            txtGiaTri.Size = new Size(375, 22);
            txtGiaTri.TabIndex = 0;
            // 
            // tbl2ColInputs
            // 
            tbl2ColInputs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbl2ColInputs.ColumnCount = 2;
            tbl2ColInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl2ColInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl2ColInputs.Controls.Add(pnlColDonToiThieu, 0, 0);
            tbl2ColInputs.Controls.Add(pnlColSoLuong, 1, 0);
            tbl2ColInputs.Location = new Point(21, 320);
            tbl2ColInputs.Name = "tbl2ColInputs";
            tbl2ColInputs.RowCount = 1;
            tbl2ColInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tbl2ColInputs.Size = new Size(405, 68);
            tbl2ColInputs.TabIndex = 9;
            // 
            // pnlColDonToiThieu
            // 
            pnlColDonToiThieu.Controls.Add(lblDonToiThieu);
            pnlColDonToiThieu.Controls.Add(pnlDonToiThieuWrap);
            pnlColDonToiThieu.Dock = DockStyle.Fill;
            pnlColDonToiThieu.Location = new Point(0, 0);
            pnlColDonToiThieu.Margin = new Padding(0, 0, 8, 0);
            pnlColDonToiThieu.Name = "pnlColDonToiThieu";
            pnlColDonToiThieu.Size = new Size(194, 68);
            pnlColDonToiThieu.TabIndex = 0;
            // 
            // lblDonToiThieu
            // 
            lblDonToiThieu.AutoSize = true;
            lblDonToiThieu.Font = new Font("Segoe UI", 9F);
            lblDonToiThieu.ForeColor = Color.FromArgb(51, 65, 85);
            lblDonToiThieu.Location = new Point(0, 0);
            lblDonToiThieu.Name = "lblDonToiThieu";
            lblDonToiThieu.Size = new Size(96, 20);
            lblDonToiThieu.TabIndex = 0;
            lblDonToiThieu.Text = "Đơn tối thiểu";
            // 
            // pnlDonToiThieuWrap
            // 
            pnlDonToiThieuWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlDonToiThieuWrap.BackColor = Color.White;
            pnlDonToiThieuWrap.Controls.Add(txtDonToiThieu);
            pnlDonToiThieuWrap.Location = new Point(0, 24);
            pnlDonToiThieuWrap.Name = "pnlDonToiThieuWrap";
            pnlDonToiThieuWrap.Padding = new Padding(12, 0, 12, 0);
            pnlDonToiThieuWrap.Size = new Size(194, 38);
            pnlDonToiThieuWrap.TabIndex = 1;
            pnlDonToiThieuWrap.Paint += DrawFigmaInputBox;
            // 
            // txtDonToiThieu
            // 
            txtDonToiThieu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDonToiThieu.BackColor = Color.White;
            txtDonToiThieu.BorderStyle = BorderStyle.None;
            txtDonToiThieu.Font = new Font("Segoe UI", 9.5F);
            txtDonToiThieu.ForeColor = Color.FromArgb(15, 23, 42);
            txtDonToiThieu.Location = new Point(10, 8);
            txtDonToiThieu.Name = "txtDonToiThieu";
            txtDonToiThieu.PlaceholderText = "0 đ";
            txtDonToiThieu.Size = new Size(156, 22);
            txtDonToiThieu.TabIndex = 0;
            // 
            // pnlColSoLuong
            // 
            pnlColSoLuong.Controls.Add(lblSoLuong);
            pnlColSoLuong.Controls.Add(pnlSoLuongWrap);
            pnlColSoLuong.Dock = DockStyle.Fill;
            pnlColSoLuong.Location = new Point(202, 0);
            pnlColSoLuong.Margin = new Padding(0);
            pnlColSoLuong.Name = "pnlColSoLuong";
            pnlColSoLuong.Size = new Size(203, 68);
            pnlColSoLuong.TabIndex = 1;
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Font = new Font("Segoe UI", 9F);
            lblSoLuong.ForeColor = Color.FromArgb(51, 65, 85);
            lblSoLuong.Location = new Point(8, 0);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(69, 20);
            lblSoLuong.TabIndex = 0;
            lblSoLuong.Text = "Số lượng";
            // 
            // pnlSoLuongWrap
            // 
            pnlSoLuongWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSoLuongWrap.BackColor = Color.White;
            pnlSoLuongWrap.Controls.Add(txtSoLuong);
            pnlSoLuongWrap.Location = new Point(8, 24);
            pnlSoLuongWrap.Name = "pnlSoLuongWrap";
            pnlSoLuongWrap.Padding = new Padding(12, 0, 12, 0);
            pnlSoLuongWrap.Size = new Size(195, 38);
            pnlSoLuongWrap.TabIndex = 1;
            pnlSoLuongWrap.Paint += DrawFigmaInputBox;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSoLuong.BackColor = Color.White;
            txtSoLuong.BorderStyle = BorderStyle.None;
            txtSoLuong.Font = new Font("Segoe UI", 9.5F);
            txtSoLuong.ForeColor = Color.FromArgb(15, 23, 42);
            txtSoLuong.Location = new Point(10, 8);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.PlaceholderText = "100";
            txtSoLuong.Size = new Size(158, 22);
            txtSoLuong.TabIndex = 0;
            // 
            // tbl2ColDates
            // 
            tbl2ColDates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbl2ColDates.ColumnCount = 2;
            tbl2ColDates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl2ColDates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl2ColDates.Controls.Add(pnlColNgayBD, 0, 0);
            tbl2ColDates.Controls.Add(pnlColNgayKT, 1, 0);
            tbl2ColDates.Location = new Point(21, 396);
            tbl2ColDates.Name = "tbl2ColDates";
            tbl2ColDates.RowCount = 1;
            tbl2ColDates.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tbl2ColDates.Size = new Size(405, 68);
            tbl2ColDates.TabIndex = 10;
            // 
            // pnlColNgayBD
            // 
            pnlColNgayBD.Controls.Add(lblNgayBD);
            pnlColNgayBD.Controls.Add(pnlNgayBDWrap);
            pnlColNgayBD.Dock = DockStyle.Fill;
            pnlColNgayBD.Location = new Point(0, 0);
            pnlColNgayBD.Margin = new Padding(0, 0, 8, 0);
            pnlColNgayBD.Name = "pnlColNgayBD";
            pnlColNgayBD.Size = new Size(194, 68);
            pnlColNgayBD.TabIndex = 0;
            // 
            // lblNgayBD
            // 
            lblNgayBD.AutoSize = true;
            lblNgayBD.Font = new Font("Segoe UI", 9F);
            lblNgayBD.ForeColor = Color.FromArgb(51, 65, 85);
            lblNgayBD.Location = new Point(0, 0);
            lblNgayBD.Name = "lblNgayBD";
            lblNgayBD.Size = new Size(99, 20);
            lblNgayBD.TabIndex = 0;
            lblNgayBD.Text = "Ngày bắt đầu";
            // 
            // pnlNgayBDWrap
            // 
            pnlNgayBDWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlNgayBDWrap.BackColor = Color.White;
            pnlNgayBDWrap.Controls.Add(dtpNgayBatDau);
            pnlNgayBDWrap.Location = new Point(0, 24);
            pnlNgayBDWrap.Name = "pnlNgayBDWrap";
            pnlNgayBDWrap.Padding = new Padding(4, 3, 4, 3);
            pnlNgayBDWrap.Size = new Size(194, 38);
            pnlNgayBDWrap.TabIndex = 1;
            pnlNgayBDWrap.Paint += DrawFigmaInputBox;
            // 
            // dtpNgayBatDau
            // 
            dtpNgayBatDau.CalendarFont = new Font("Segoe UI", 9.5F);
            dtpNgayBatDau.Dock = DockStyle.Fill;
            dtpNgayBatDau.Font = new Font("Segoe UI", 9.5F);
            dtpNgayBatDau.Format = DateTimePickerFormat.Short;
            dtpNgayBatDau.Location = new Point(4, 3);
            dtpNgayBatDau.Name = "dtpNgayBatDau";
            dtpNgayBatDau.Size = new Size(186, 29);
            dtpNgayBatDau.TabIndex = 0;
            // 
            // pnlColNgayKT
            // 
            pnlColNgayKT.Controls.Add(lblNgayKT);
            pnlColNgayKT.Controls.Add(pnlNgayKTWrap);
            pnlColNgayKT.Dock = DockStyle.Fill;
            pnlColNgayKT.Location = new Point(202, 0);
            pnlColNgayKT.Margin = new Padding(0);
            pnlColNgayKT.Name = "pnlColNgayKT";
            pnlColNgayKT.Size = new Size(203, 68);
            pnlColNgayKT.TabIndex = 1;
            // 
            // lblNgayKT
            // 
            lblNgayKT.AutoSize = true;
            lblNgayKT.Font = new Font("Segoe UI", 9F);
            lblNgayKT.ForeColor = Color.FromArgb(51, 65, 85);
            lblNgayKT.Location = new Point(8, 0);
            lblNgayKT.Name = "lblNgayKT";
            lblNgayKT.Size = new Size(100, 20);
            lblNgayKT.TabIndex = 0;
            lblNgayKT.Text = "Ngày kết thúc";
            // 
            // pnlNgayKTWrap
            // 
            pnlNgayKTWrap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlNgayKTWrap.BackColor = Color.White;
            pnlNgayKTWrap.Controls.Add(dtpNgayHetHan);
            pnlNgayKTWrap.Location = new Point(8, 24);
            pnlNgayKTWrap.Name = "pnlNgayKTWrap";
            pnlNgayKTWrap.Padding = new Padding(4, 3, 4, 3);
            pnlNgayKTWrap.Size = new Size(195, 38);
            pnlNgayKTWrap.TabIndex = 1;
            pnlNgayKTWrap.Paint += DrawFigmaInputBox;
            // 
            // dtpNgayHetHan
            // 
            dtpNgayHetHan.CalendarFont = new Font("Segoe UI", 9.5F);
            dtpNgayHetHan.Dock = DockStyle.Fill;
            dtpNgayHetHan.Font = new Font("Segoe UI", 9.5F);
            dtpNgayHetHan.Format = DateTimePickerFormat.Short;
            dtpNgayHetHan.Location = new Point(4, 3);
            dtpNgayHetHan.Name = "dtpNgayHetHan";
            dtpNgayHetHan.Size = new Size(187, 29);
            dtpNgayHetHan.TabIndex = 0;
            // 
            // chkTrangThai
            // 
            chkTrangThai.AutoSize = true;
            chkTrangThai.Checked = true;
            chkTrangThai.CheckState = CheckState.Checked;
            chkTrangThai.Cursor = Cursors.Hand;
            chkTrangThai.Font = new Font("Segoe UI", 9.5F);
            chkTrangThai.ForeColor = Color.FromArgb(30, 41, 59);
            chkTrangThai.Location = new Point(24, 480);
            chkTrangThai.Name = "chkTrangThai";
            chkTrangThai.Size = new Size(242, 25);
            chkTrangThai.TabIndex = 11;
            chkTrangThai.Text = "Kích hoạt hiệu lực của voucher";
            chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // pnlBottomButtons
            // 
            pnlBottomButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBottomButtons.Controls.Add(btnXoa);
            pnlBottomButtons.Controls.Add(btnLamMoi);
            pnlBottomButtons.Controls.Add(btnLuu);
            pnlBottomButtons.Location = new Point(24, 524);
            pnlBottomButtons.Name = "pnlBottomButtons";
            pnlBottomButtons.Size = new Size(399, 44);
            pnlBottomButtons.TabIndex = 12;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.White;
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9.5F);
            btnXoa.ForeColor = Color.FromArgb(220, 38, 38);
            btnXoa.Location = new Point(0, 0);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 40);
            btnXoa.TabIndex = 0;
            btnXoa.Text = "Xóa mã";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.BackColor = Color.White;
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9.5F);
            btnLamMoi.ForeColor = Color.FromArgb(71, 85, 105);
            btnLamMoi.Location = new Point(167, 0);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(92, 40);
            btnLamMoi.TabIndex = 1;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnLuu
            // 
            btnLuu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLuu.BackColor = Color.FromArgb(234, 88, 12);
            btnLuu.Cursor = Cursors.Hand;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(267, 0);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(132, 40);
            btnLuu.TabIndex = 2;
            btnLuu.Text = "Lưu Voucher";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            btnLuu.Paint += DrawPillBorder;
            // 
            // frmVoucher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1280, 760);
            Controls.Add(tblMainLayout);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmVoucher";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Voucher";
            Load += frmVoucher_Load;
            tblMainLayout.ResumeLayout(false);
            cardLeft.ResumeLayout(false);
            pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVoucher).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            cardRight.ResumeLayout(false);
            cardRight.PerformLayout();
            pnlCodeWrap.ResumeLayout(false);
            pnlCodeWrap.PerformLayout();
            pnlLoaiGiam.ResumeLayout(false);
            pnlLoaiGiam.PerformLayout();
            pnlGiaTriWrap.ResumeLayout(false);
            pnlGiaTriWrap.PerformLayout();
            tbl2ColInputs.ResumeLayout(false);
            pnlColDonToiThieu.ResumeLayout(false);
            pnlColDonToiThieu.PerformLayout();
            pnlDonToiThieuWrap.ResumeLayout(false);
            pnlDonToiThieuWrap.PerformLayout();
            pnlColSoLuong.ResumeLayout(false);
            pnlColSoLuong.PerformLayout();
            pnlSoLuongWrap.ResumeLayout(false);
            pnlSoLuongWrap.PerformLayout();
            tbl2ColDates.ResumeLayout(false);
            pnlColNgayBD.ResumeLayout(false);
            pnlColNgayBD.PerformLayout();
            pnlNgayBDWrap.ResumeLayout(false);
            pnlColNgayKT.ResumeLayout(false);
            pnlColNgayKT.PerformLayout();
            pnlNgayKTWrap.ResumeLayout(false);
            pnlBottomButtons.ResumeLayout(false);
            ResumeLayout(false);

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