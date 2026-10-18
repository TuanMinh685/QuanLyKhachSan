namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmDichVu
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
            this.lblIconSearch = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlGridWrapper = new System.Windows.Forms.Panel();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.cardRight = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblCardSubtitle = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblMaDV = new System.Windows.Forms.Label();
            this.pnlMaDVWrap = new System.Windows.Forms.Panel();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.lblTenDV = new System.Windows.Forms.Label();
            this.pnlTenDVWrap = new System.Windows.Forms.Panel();
            this.txtTenDV = new System.Windows.Forms.TextBox();
            this.lblLoaiDV = new System.Windows.Forms.Label();
            this.pnlLoaiDVWrap = new System.Windows.Forms.Panel();
            this.cboLoaiDV = new System.Windows.Forms.ComboBox();
            this.tbl2ColInputs = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColDonGia = new System.Windows.Forms.Panel();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.pnlDonGiaWrap = new System.Windows.Forms.Panel();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.pnlColDonViTinh = new System.Windows.Forms.Panel();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.pnlDVTWrap = new System.Windows.Forms.Panel();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            this.cardLeft.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.cardRight.SuspendLayout();
            this.pnlMaDVWrap.SuspendLayout();
            this.pnlTenDVWrap.SuspendLayout();
            this.pnlLoaiDVWrap.SuspendLayout();
            this.tbl2ColInputs.SuspendLayout();
            this.pnlColDonGia.SuspendLayout();
            this.pnlDonGiaWrap.SuspendLayout();
            this.pnlColDonViTinh.SuspendLayout();
            this.pnlDVTWrap.SuspendLayout();
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
            this.txtTimKiem.PlaceholderText = "Tìm kiếm theo tên dịch vụ, mã dịch vụ hoặc loại...";
            this.txtTimKiem.Size = new System.Drawing.Size(681, 22);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlGridWrapper
            // 
            this.pnlGridWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridWrapper.Controls.Add(this.dgvDichVu);
            this.pnlGridWrapper.Location = new System.Drawing.Point(20, 72);
            this.pnlGridWrapper.Name = "pnlGridWrapper";
            this.pnlGridWrapper.Size = new System.Drawing.Size(737, 628);
            this.pnlGridWrapper.TabIndex = 1;
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.Location = new System.Drawing.Point(0, 0);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.Size = new System.Drawing.Size(737, 628);
            this.dgvDichVu.TabIndex = 0;
            this.dgvDichVu.SelectionChanged += new System.EventHandler(this.dgvDichVu_SelectionChanged);
            // 
            // cardRight
            // 
            this.cardRight.AutoScroll = true;
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.lblCardTitle);
            this.cardRight.Controls.Add(this.lblCardSubtitle);
            this.cardRight.Controls.Add(this.pnlDivider);
            this.cardRight.Controls.Add(this.lblMaDV);
            this.cardRight.Controls.Add(this.pnlMaDVWrap);
            this.cardRight.Controls.Add(this.lblTenDV);
            this.cardRight.Controls.Add(this.pnlTenDVWrap);
            this.cardRight.Controls.Add(this.lblLoaiDV);
            this.cardRight.Controls.Add(this.pnlLoaiDVWrap);
            this.cardRight.Controls.Add(this.tbl2ColInputs);
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
            this.lblCardTitle.Size = new System.Drawing.Size(189, 30);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Thông tin dịch vụ";
            // 
            // lblCardSubtitle
            // 
            this.lblCardSubtitle.AutoSize = true;
            this.lblCardSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardSubtitle.Location = new System.Drawing.Point(24, 50);
            this.lblCardSubtitle.Name = "lblCardSubtitle";
            this.lblCardSubtitle.Size = new System.Drawing.Size(263, 20);
            this.lblCardSubtitle.TabIndex = 1;
            this.lblCardSubtitle.Text = "Danh mục ăn uống, giặt ủi, tiện ích phòng";
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
            // lblMaDV
            // 
            this.lblMaDV.AutoSize = true;
            this.lblMaDV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMaDV.Location = new System.Drawing.Point(22, 95);
            this.lblMaDV.Name = "lblMaDV";
            this.lblMaDV.Size = new System.Drawing.Size(94, 20);
            this.lblMaDV.TabIndex = 3;
            this.lblMaDV.Text = "Mã dịch vụ *";
            // 
            // pnlMaDVWrap
            // 
            this.pnlMaDVWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMaDVWrap.BackColor = System.Drawing.Color.White;
            this.pnlMaDVWrap.Controls.Add(this.txtMaDV);
            this.pnlMaDVWrap.Location = new System.Drawing.Point(24, 120);
            this.pnlMaDVWrap.Name = "pnlMaDVWrap";
            this.pnlMaDVWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlMaDVWrap.Size = new System.Drawing.Size(399, 38);
            this.pnlMaDVWrap.TabIndex = 4;
            this.pnlMaDVWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtMaDV
            // 
            this.txtMaDV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaDV.BackColor = System.Drawing.Color.White;
            this.txtMaDV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaDV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaDV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMaDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtMaDV.Location = new System.Drawing.Point(12, 8);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.PlaceholderText = "VÍ DỤ: DV01";
            this.txtMaDV.Size = new System.Drawing.Size(375, 22);
            this.txtMaDV.TabIndex = 0;
            // 
            // lblTenDV
            // 
            this.lblTenDV.AutoSize = true;
            this.lblTenDV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTenDV.Location = new System.Drawing.Point(22, 172);
            this.lblTenDV.Name = "lblTenDV";
            this.lblTenDV.Size = new System.Drawing.Size(96, 20);
            this.lblTenDV.TabIndex = 5;
            this.lblTenDV.Text = "Tên dịch vụ *";
            // 
            // pnlTenDVWrap
            // 
            this.pnlTenDVWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTenDVWrap.BackColor = System.Drawing.Color.White;
            this.pnlTenDVWrap.Controls.Add(this.txtTenDV);
            this.pnlTenDVWrap.Location = new System.Drawing.Point(24, 196);
            this.pnlTenDVWrap.Name = "pnlTenDVWrap";
            this.pnlTenDVWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlTenDVWrap.Size = new System.Drawing.Size(399, 38);
            this.pnlTenDVWrap.TabIndex = 6;
            this.pnlTenDVWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTenDV
            // 
            this.txtTenDV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenDV.BackColor = System.Drawing.Color.White;
            this.txtTenDV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTenDV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTenDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtTenDV.Location = new System.Drawing.Point(12, 8);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.PlaceholderText = "VÍ DỤ: Giặt ủi nhanh, Bữa sáng Buffet...";
            this.txtTenDV.Size = new System.Drawing.Size(375, 22);
            this.txtTenDV.TabIndex = 0;
            // 
            // lblLoaiDV
            // 
            this.lblLoaiDV.AutoSize = true;
            this.lblLoaiDV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblLoaiDV.Location = new System.Drawing.Point(22, 248);
            this.lblLoaiDV.Name = "lblLoaiDV";
            this.lblLoaiDV.Size = new System.Drawing.Size(100, 20);
            this.lblLoaiDV.TabIndex = 7;
            this.lblLoaiDV.Text = "Nhóm loại DV";
            // 
            // pnlLoaiDVWrap
            // 
            this.pnlLoaiDVWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLoaiDVWrap.BackColor = System.Drawing.Color.White;
            this.pnlLoaiDVWrap.Controls.Add(this.cboLoaiDV);
            this.pnlLoaiDVWrap.Location = new System.Drawing.Point(24, 272);
            this.pnlLoaiDVWrap.Name = "pnlLoaiDVWrap";
            this.pnlLoaiDVWrap.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlLoaiDVWrap.Size = new System.Drawing.Size(399, 38);
            this.pnlLoaiDVWrap.TabIndex = 8;
            this.pnlLoaiDVWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // cboLoaiDV
            // 
            this.cboLoaiDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboLoaiDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLoaiDV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiDV.FormattingEnabled = true;
            this.cboLoaiDV.Items.AddRange(new object[] {
            "Ăn uống",
            "Giặt ủi",
            "Đưa đón",
            "Giải trí & Spa",
            "Tiện ích khác"});
            this.cboLoaiDV.Location = new System.Drawing.Point(6, 4);
            this.cboLoaiDV.Name = "cboLoaiDV";
            this.cboLoaiDV.Size = new System.Drawing.Size(387, 29);
            this.cboLoaiDV.TabIndex = 0;
            // 
            // tbl2ColInputs
            // 
            this.tbl2ColInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbl2ColInputs.ColumnCount = 2;
            this.tbl2ColInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tbl2ColInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tbl2ColInputs.Controls.Add(this.pnlColDonGia, 0, 0);
            this.tbl2ColInputs.Controls.Add(this.pnlColDonViTinh, 1, 0);
            this.tbl2ColInputs.Location = new System.Drawing.Point(21, 324);
            this.tbl2ColInputs.Name = "tbl2ColInputs";
            this.tbl2ColInputs.RowCount = 1;
            this.tbl2ColInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl2ColInputs.Size = new System.Drawing.Size(405, 68);
            this.tbl2ColInputs.TabIndex = 9;
            // 
            // pnlColDonGia
            // 
            this.pnlColDonGia.Controls.Add(this.lblDonGia);
            this.pnlColDonGia.Controls.Add(this.pnlDonGiaWrap);
            this.pnlColDonGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColDonGia.Location = new System.Drawing.Point(0, 0);
            this.pnlColDonGia.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlColDonGia.Name = "pnlColDonGia";
            this.pnlColDonGia.Size = new System.Drawing.Size(214, 68);
            this.pnlColDonGia.TabIndex = 0;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDonGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDonGia.Location = new System.Drawing.Point(0, 0);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(73, 20);
            this.lblDonGia.TabIndex = 0;
            this.lblDonGia.Text = "Đơn giá *";
            // 
            // pnlDonGiaWrap
            // 
            this.pnlDonGiaWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDonGiaWrap.BackColor = System.Drawing.Color.White;
            this.pnlDonGiaWrap.Controls.Add(this.txtDonGia);
            this.pnlDonGiaWrap.Location = new System.Drawing.Point(0, 24);
            this.pnlDonGiaWrap.Name = "pnlDonGiaWrap";
            this.pnlDonGiaWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlDonGiaWrap.Size = new System.Drawing.Size(214, 38);
            this.pnlDonGiaWrap.TabIndex = 1;
            this.pnlDonGiaWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtDonGia
            // 
            this.txtDonGia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonGia.BackColor = System.Drawing.Color.White;
            this.txtDonGia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDonGia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtDonGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.txtDonGia.Location = new System.Drawing.Point(10, 8);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.PlaceholderText = "0 đ";
            this.txtDonGia.Size = new System.Drawing.Size(194, 22);
            this.txtDonGia.TabIndex = 0;
            // 
            // pnlColDonViTinh
            // 
            this.pnlColDonViTinh.Controls.Add(this.lblDonViTinh);
            this.pnlColDonViTinh.Controls.Add(this.pnlDVTWrap);
            this.pnlColDonViTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColDonViTinh.Location = new System.Drawing.Point(222, 0);
            this.pnlColDonViTinh.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColDonViTinh.Name = "pnlColDonViTinh";
            this.pnlColDonViTinh.Size = new System.Drawing.Size(183, 68);
            this.pnlColDonViTinh.TabIndex = 1;
            // 
            // lblDonViTinh
            // 
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDonViTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDonViTinh.Location = new System.Drawing.Point(8, 0);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Size = new System.Drawing.Size(81, 20);
            this.lblDonViTinh.TabIndex = 0;
            this.lblDonViTinh.Text = "Đơn vị tính";
            // 
            // pnlDVTWrap
            // 
            this.pnlDVTWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDVTWrap.BackColor = System.Drawing.Color.White;
            this.pnlDVTWrap.Controls.Add(this.cboDonViTinh);
            this.pnlDVTWrap.Location = new System.Drawing.Point(8, 24);
            this.pnlDVTWrap.Name = "pnlDVTWrap";
            this.pnlDVTWrap.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlDVTWrap.Size = new System.Drawing.Size(175, 38);
            this.pnlDVTWrap.TabIndex = 1;
            this.pnlDVTWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // cboDonViTinh
            // 
            this.cboDonViTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDonViTinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDonViTinh.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDonViTinh.FormattingEnabled = true;
            this.cboDonViTinh.Items.AddRange(new object[] {
            "Lon",
            "Chai",
            "Phần",
            "Kg",
            "Chuyến",
            "Giờ",
            "Lần"});
            this.cboDonViTinh.Location = new System.Drawing.Point(6, 4);
            this.cboDonViTinh.Name = "cboDonViTinh";
            this.cboDonViTinh.Size = new System.Drawing.Size(163, 29);
            this.cboDonViTinh.TabIndex = 0;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.chkTrangThai.Location = new System.Drawing.Point(24, 412);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(215, 25);
            this.chkTrangThai.TabIndex = 10;
            this.chkTrangThai.Text = "Đang phục vụ kinh doanh";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomButtons.Controls.Add(this.btnXoa);
            this.pnlBottomButtons.Controls.Add(this.btnLamMoi);
            this.pnlBottomButtons.Controls.Add(this.btnLuu);
            this.pnlBottomButtons.Location = new System.Drawing.Point(24, 460);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(399, 44);
            this.pnlBottomButtons.TabIndex = 11;
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
            this.btnLuu.Text = "Lưu dịch vụ";
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
            this.btnXoa.Text = "Xóa DV";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDichVu";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Dịch Vụ";
            this.Load += new System.EventHandler(this.frmDichVu_Load);
            this.tblMainLayout.ResumeLayout(false);
            this.cardLeft.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.pnlMaDVWrap.ResumeLayout(false);
            this.pnlMaDVWrap.PerformLayout();
            this.pnlTenDVWrap.ResumeLayout(false);
            this.pnlTenDVWrap.PerformLayout();
            this.pnlLoaiDVWrap.ResumeLayout(false);
            this.tbl2ColInputs.ResumeLayout(false);
            this.pnlColDonGia.ResumeLayout(false);
            this.pnlColDonGia.PerformLayout();
            this.pnlDonGiaWrap.ResumeLayout(false);
            this.pnlDonGiaWrap.PerformLayout();
            this.pnlColDonViTinh.ResumeLayout(false);
            this.pnlColDonViTinh.PerformLayout();
            this.pnlDVTWrap.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCardSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblMaDV;
        private System.Windows.Forms.Panel pnlMaDVWrap;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.Label lblTenDV;
        private System.Windows.Forms.Panel pnlTenDVWrap;
        private System.Windows.Forms.TextBox txtTenDV;
        private System.Windows.Forms.Label lblLoaiDV;
        private System.Windows.Forms.Panel pnlLoaiDVWrap;
        private System.Windows.Forms.ComboBox cboLoaiDV;
        private System.Windows.Forms.TableLayoutPanel tbl2ColInputs;
        private System.Windows.Forms.Panel pnlColDonGia;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Panel pnlDonGiaWrap;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Panel pnlColDonViTinh;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.Panel pnlDVTWrap;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
    }
}