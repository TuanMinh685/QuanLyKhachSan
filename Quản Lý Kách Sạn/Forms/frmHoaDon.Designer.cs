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
            this.cardLeft = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblIconSearch = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlGridWrapper = new System.Windows.Forms.Panel();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.cardRight = new System.Windows.Forms.Panel();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblCardSubtitle = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.pnlMaHDWrap = new System.Windows.Forms.Panel();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblMaDatPhong = new System.Windows.Forms.Label();
            this.pnlMaDPWrap = new System.Windows.Forms.Panel();
            this.cboDatPhong = new System.Windows.Forms.ComboBox();
            this.tbl2ColMoney1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColTienPhong = new System.Windows.Forms.Panel();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.pnlTienPhongWrap = new System.Windows.Forms.Panel();
            this.txtTienPhong = new System.Windows.Forms.TextBox();
            this.pnlColTienDV = new System.Windows.Forms.Panel();
            this.lblTienDV = new System.Windows.Forms.Label();
            this.pnlTienDVWrap = new System.Windows.Forms.Panel();
            this.txtTienDV = new System.Windows.Forms.TextBox();
            this.tbl2ColMoney2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlColGiamGia = new System.Windows.Forms.Panel();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.pnlGiamGiaWrap = new System.Windows.Forms.Panel();
            this.txtTienGiamGia = new System.Windows.Forms.TextBox();
            this.pnlColDatCoc = new System.Windows.Forms.Panel();
            this.lblDatCoc = new System.Windows.Forms.Label();
            this.pnlDatCocWrap = new System.Windows.Forms.Panel();
            this.txtTienDatCoc = new System.Windows.Forms.TextBox();
            this.lblPTTT = new System.Windows.Forms.Label();
            this.pnlPTTTWrap = new System.Windows.Forms.Panel();
            this.cboPTTT = new System.Windows.Forms.ComboBox();
            this.pnlTongTienBox = new System.Windows.Forms.Panel();
            this.lblTongTienTitle = new System.Windows.Forms.Label();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            this.cardLeft.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.cardRight.SuspendLayout();
            this.pnlMaHDWrap.SuspendLayout();
            this.pnlMaDPWrap.SuspendLayout();
            this.tbl2ColMoney1.SuspendLayout();
            this.pnlColTienPhong.SuspendLayout();
            this.pnlTienPhongWrap.SuspendLayout();
            this.pnlColTienDV.SuspendLayout();
            this.pnlTienDVWrap.SuspendLayout();
            this.tbl2ColMoney2.SuspendLayout();
            this.pnlColGiamGia.SuspendLayout();
            this.pnlGiamGiaWrap.SuspendLayout();
            this.pnlColDatCoc.SuspendLayout();
            this.pnlDatCocWrap.SuspendLayout();
            this.pnlPTTTWrap.SuspendLayout();
            this.pnlTongTienBox.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 2;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
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
            this.cardLeft.Size = new System.Drawing.Size(752, 720);
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
            this.pnlSearch.Size = new System.Drawing.Size(712, 38);
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
            this.txtTimKiem.PlaceholderText = "Tìm hóa đơn theo số phòng, mã hóa đơn, mã đặt phòng...";
            this.txtTimKiem.Size = new System.Drawing.Size(656, 22);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlGridWrapper
            // 
            this.pnlGridWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridWrapper.Controls.Add(this.dgvHoaDon);
            this.pnlGridWrapper.Location = new System.Drawing.Point(20, 72);
            this.pnlGridWrapper.Name = "pnlGridWrapper";
            this.pnlGridWrapper.Size = new System.Drawing.Size(712, 628);
            this.pnlGridWrapper.TabIndex = 1;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 0);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.Size = new System.Drawing.Size(712, 628);
            this.dgvHoaDon.TabIndex = 0;
            this.dgvHoaDon.SelectionChanged += new System.EventHandler(this.dgvHoaDon_SelectionChanged);
            // 
            // cardRight
            // 
            this.cardRight.AutoScroll = true;
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.lblCardTitle);
            this.cardRight.Controls.Add(this.lblCardSubtitle);
            this.cardRight.Controls.Add(this.pnlDivider);
            this.cardRight.Controls.Add(this.lblMaHD);
            this.cardRight.Controls.Add(this.pnlMaHDWrap);
            this.cardRight.Controls.Add(this.lblMaDatPhong);
            this.cardRight.Controls.Add(this.pnlMaDPWrap);
            this.cardRight.Controls.Add(this.tbl2ColMoney1);
            this.cardRight.Controls.Add(this.tbl2ColMoney2);
            this.cardRight.Controls.Add(this.lblPTTT);
            this.cardRight.Controls.Add(this.pnlPTTTWrap);
            this.cardRight.Controls.Add(this.pnlTongTienBox);
            this.cardRight.Controls.Add(this.pnlBottomButtons);
            this.cardRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRight.Location = new System.Drawing.Point(768, 0);
            this.cardRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardRight.Name = "cardRight";
            this.cardRight.Padding = new System.Windows.Forms.Padding(24);
            this.cardRight.Size = new System.Drawing.Size(472, 720);
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
            this.lblCardTitle.Size = new System.Drawing.Size(206, 30);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Hóa đơn thanh toán";
            // 
            // lblCardSubtitle
            // 
            this.lblCardSubtitle.AutoSize = true;
            this.lblCardSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardSubtitle.Location = new System.Drawing.Point(24, 50);
            this.lblCardSubtitle.Name = "lblCardSubtitle";
            this.lblCardSubtitle.Size = new System.Drawing.Size(288, 20);
            this.lblCardSubtitle.TabIndex = 1;
            this.lblCardSubtitle.Text = "Chốt tiền phòng, dịch vụ và in phiếu thu ngân";
            // 
            // pnlDivider
            // 
            this.pnlDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDivider.Location = new System.Drawing.Point(24, 80);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(424, 1);
            this.pnlDivider.TabIndex = 2;
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMaHD.Location = new System.Drawing.Point(22, 95);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(99, 20);
            this.lblMaHD.TabIndex = 3;
            this.lblMaHD.Text = "Mã hóa đơn *";
            // 
            // pnlMaHDWrap
            // 
            this.pnlMaHDWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMaHDWrap.BackColor = System.Drawing.Color.White;
            this.pnlMaHDWrap.Controls.Add(this.txtMaHD);
            this.pnlMaHDWrap.Location = new System.Drawing.Point(24, 120);
            this.pnlMaHDWrap.Name = "pnlMaHDWrap";
            this.pnlMaHDWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlMaHDWrap.Size = new System.Drawing.Size(424, 38);
            this.pnlMaHDWrap.TabIndex = 4;
            this.pnlMaHDWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtMaHD
            // 
            this.txtMaHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaHD.BackColor = System.Drawing.Color.White;
            this.txtMaHD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaHD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaHD.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMaHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtMaHD.Location = new System.Drawing.Point(12, 8);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.PlaceholderText = "VÍ DỤ: HD2026-001";
            this.txtMaHD.Size = new System.Drawing.Size(400, 22);
            this.txtMaHD.TabIndex = 0;
            // 
            // lblMaDatPhong
            // 
            this.lblMaDatPhong.AutoSize = true;
            this.lblMaDatPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaDatPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMaDatPhong.Location = new System.Drawing.Point(22, 172);
            this.lblMaDatPhong.Name = "lblMaDatPhong";
            this.lblMaDatPhong.Size = new System.Drawing.Size(147, 20);
            this.lblMaDatPhong.TabIndex = 5;
            this.lblMaDatPhong.Text = "Hồ sơ nhận phòng *";
            // 
            // pnlMaDPWrap
            // 
            this.pnlMaDPWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMaDPWrap.BackColor = System.Drawing.Color.White;
            this.pnlMaDPWrap.Controls.Add(this.cboDatPhong);
            this.pnlMaDPWrap.Location = new System.Drawing.Point(24, 196);
            this.pnlMaDPWrap.Name = "pnlMaDPWrap";
            this.pnlMaDPWrap.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlMaDPWrap.Size = new System.Drawing.Size(424, 38);
            this.pnlMaDPWrap.TabIndex = 6;
            this.pnlMaDPWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // cboDatPhong
            // 
            this.cboDatPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDatPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDatPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDatPhong.FormattingEnabled = true;
            this.cboDatPhong.Location = new System.Drawing.Point(6, 4);
            this.cboDatPhong.Name = "cboDatPhong";
            this.cboDatPhong.Size = new System.Drawing.Size(412, 29);
            this.cboDatPhong.TabIndex = 0;
            this.cboDatPhong.SelectedIndexChanged += new System.EventHandler(this.cboDatPhong_SelectedIndexChanged);
            // 
            // tbl2ColMoney1
            // 
            this.tbl2ColMoney1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbl2ColMoney1.ColumnCount = 2;
            this.tbl2ColMoney1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColMoney1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColMoney1.Controls.Add(this.pnlColTienPhong, 0, 0);
            this.tbl2ColMoney1.Controls.Add(this.pnlColTienDV, 1, 0);
            this.tbl2ColMoney1.Location = new System.Drawing.Point(21, 248);
            this.tbl2ColMoney1.Name = "tbl2ColMoney1";
            this.tbl2ColMoney1.RowCount = 1;
            this.tbl2ColMoney1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl2ColMoney1.Size = new System.Drawing.Size(430, 68);
            this.tbl2ColMoney1.TabIndex = 7;
            // 
            // pnlColTienPhong
            // 
            this.pnlColTienPhong.Controls.Add(this.lblTienPhong);
            this.pnlColTienPhong.Controls.Add(this.pnlTienPhongWrap);
            this.pnlColTienPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColTienPhong.Location = new System.Drawing.Point(0, 0);
            this.pnlColTienPhong.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlColTienPhong.Name = "pnlColTienPhong";
            this.pnlColTienPhong.Size = new System.Drawing.Size(207, 68);
            this.pnlColTienPhong.TabIndex = 0;
            // 
            // lblTienPhong
            // 
            this.lblTienPhong.AutoSize = true;
            this.lblTienPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTienPhong.Location = new System.Drawing.Point(0, 0);
            this.lblTienPhong.Name = "lblTienPhong";
            this.lblTienPhong.Size = new System.Drawing.Size(83, 20);
            this.lblTienPhong.TabIndex = 0;
            this.lblTienPhong.Text = "Tiền phòng";
            // 
            // pnlTienPhongWrap
            // 
            this.pnlTienPhongWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTienPhongWrap.BackColor = System.Drawing.Color.White;
            this.pnlTienPhongWrap.Controls.Add(this.txtTienPhong);
            this.pnlTienPhongWrap.Location = new System.Drawing.Point(0, 24);
            this.pnlTienPhongWrap.Name = "pnlTienPhongWrap";
            this.pnlTienPhongWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlTienPhongWrap.Size = new System.Drawing.Size(207, 38);
            this.pnlTienPhongWrap.TabIndex = 1;
            this.pnlTienPhongWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTienPhong
            // 
            this.txtTienPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienPhong.BackColor = System.Drawing.Color.White;
            this.txtTienPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTienPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTienPhong.Location = new System.Drawing.Point(10, 8);
            this.txtTienPhong.Name = "txtTienPhong";
            this.txtTienPhong.Size = new System.Drawing.Size(187, 22);
            this.txtTienPhong.TabIndex = 0;
            this.txtTienPhong.Text = "0";
            this.txtTienPhong.TextChanged += new System.EventHandler(this.TinhTongTienTuDong);
            // 
            // pnlColTienDV
            // 
            this.pnlColTienDV.Controls.Add(this.lblTienDV);
            this.pnlColTienDV.Controls.Add(this.pnlTienDVWrap);
            this.pnlColTienDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColTienDV.Location = new System.Drawing.Point(215, 0);
            this.pnlColTienDV.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColTienDV.Name = "pnlColTienDV";
            this.pnlColTienDV.Size = new System.Drawing.Size(215, 68);
            this.pnlColTienDV.TabIndex = 1;
            // 
            // lblTienDV
            // 
            this.lblTienDV.AutoSize = true;
            this.lblTienDV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTienDV.Location = new System.Drawing.Point(8, 0);
            this.lblTienDV.Name = "lblTienDV";
            this.lblTienDV.Size = new System.Drawing.Size(87, 20);
            this.lblTienDV.TabIndex = 0;
            this.lblTienDV.Text = "Tiền dịch vụ";
            // 
            // pnlTienDVWrap
            // 
            this.pnlTienDVWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTienDVWrap.BackColor = System.Drawing.Color.White;
            this.pnlTienDVWrap.Controls.Add(this.txtTienDV);
            this.pnlTienDVWrap.Location = new System.Drawing.Point(8, 24);
            this.pnlTienDVWrap.Name = "pnlTienDVWrap";
            this.pnlTienDVWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlTienDVWrap.Size = new System.Drawing.Size(207, 38);
            this.pnlTienDVWrap.TabIndex = 1;
            this.pnlTienDVWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTienDV
            // 
            this.txtTienDV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienDV.BackColor = System.Drawing.Color.White;
            this.txtTienDV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTienDV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtTienDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTienDV.Location = new System.Drawing.Point(10, 8);
            this.txtTienDV.Name = "txtTienDV";
            this.txtTienDV.Size = new System.Drawing.Size(187, 22);
            this.txtTienDV.TabIndex = 0;
            this.txtTienDV.Text = "0";
            this.txtTienDV.TextChanged += new System.EventHandler(this.TinhTongTienTuDong);
            // 
            // tbl2ColMoney2
            // 
            this.tbl2ColMoney2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbl2ColMoney2.ColumnCount = 2;
            this.tbl2ColMoney2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColMoney2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl2ColMoney2.Controls.Add(this.pnlColGiamGia, 0, 0);
            this.tbl2ColMoney2.Controls.Add(this.pnlColDatCoc, 1, 0);
            this.tbl2ColMoney2.Location = new System.Drawing.Point(21, 320);
            this.tbl2ColMoney2.Name = "tbl2ColMoney2";
            this.tbl2ColMoney2.RowCount = 1;
            this.tbl2ColMoney2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl2ColMoney2.Size = new System.Drawing.Size(430, 68);
            this.tbl2ColMoney2.TabIndex = 8;
            // 
            // pnlColGiamGia
            // 
            this.pnlColGiamGia.Controls.Add(this.lblGiamGia);
            this.pnlColGiamGia.Controls.Add(this.pnlGiamGiaWrap);
            this.pnlColGiamGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColGiamGia.Location = new System.Drawing.Point(0, 0);
            this.pnlColGiamGia.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlColGiamGia.Name = "pnlColGiamGia";
            this.pnlColGiamGia.Size = new System.Drawing.Size(207, 68);
            this.pnlColGiamGia.TabIndex = 0;
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiamGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblGiamGia.Location = new System.Drawing.Point(0, 0);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(124, 20);
            this.lblGiamGia.TabIndex = 0;
            this.lblGiamGia.Text = "Ưu đãi / Giảm giá";
            // 
            // pnlGiamGiaWrap
            // 
            this.pnlGiamGiaWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGiamGiaWrap.BackColor = System.Drawing.Color.White;
            this.pnlGiamGiaWrap.Controls.Add(this.txtTienGiamGia);
            this.pnlGiamGiaWrap.Location = new System.Drawing.Point(0, 24);
            this.pnlGiamGiaWrap.Name = "pnlGiamGiaWrap";
            this.pnlGiamGiaWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlGiamGiaWrap.Size = new System.Drawing.Size(207, 38);
            this.pnlGiamGiaWrap.TabIndex = 1;
            this.pnlGiamGiaWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTienGiamGia
            // 
            this.txtTienGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienGiamGia.BackColor = System.Drawing.Color.White;
            this.txtTienGiamGia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTienGiamGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTienGiamGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.txtTienGiamGia.Location = new System.Drawing.Point(10, 8);
            this.txtTienGiamGia.Name = "txtTienGiamGia";
            this.txtTienGiamGia.Size = new System.Drawing.Size(187, 22);
            this.txtTienGiamGia.TabIndex = 0;
            this.txtTienGiamGia.Text = "0";
            this.txtTienGiamGia.TextChanged += new System.EventHandler(this.TinhTongTienTuDong);
            // 
            // pnlColDatCoc
            // 
            this.pnlColDatCoc.Controls.Add(this.lblDatCoc);
            this.pnlColDatCoc.Controls.Add(this.pnlDatCocWrap);
            this.pnlColDatCoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColDatCoc.Location = new System.Drawing.Point(215, 0);
            this.pnlColDatCoc.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColDatCoc.Name = "pnlColDatCoc";
            this.pnlColDatCoc.Size = new System.Drawing.Size(215, 68);
            this.pnlColDatCoc.TabIndex = 1;
            // 
            // lblDatCoc
            // 
            this.lblDatCoc.AutoSize = true;
            this.lblDatCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDatCoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDatCoc.Location = new System.Drawing.Point(8, 0);
            this.lblDatCoc.Name = "lblDatCoc";
            this.lblDatCoc.Size = new System.Drawing.Size(91, 20);
            this.lblDatCoc.TabIndex = 0;
            this.lblDatCoc.Text = "Tiền đặt cọc";
            // 
            // pnlDatCocWrap
            // 
            this.pnlDatCocWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDatCocWrap.BackColor = System.Drawing.Color.White;
            this.pnlDatCocWrap.Controls.Add(this.txtTienDatCoc);
            this.pnlDatCocWrap.Location = new System.Drawing.Point(8, 24);
            this.pnlDatCocWrap.Name = "pnlDatCocWrap";
            this.pnlDatCocWrap.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.pnlDatCocWrap.Size = new System.Drawing.Size(207, 38);
            this.pnlDatCocWrap.TabIndex = 1;
            this.pnlDatCocWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTienDatCoc
            // 
            this.txtTienDatCoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienDatCoc.BackColor = System.Drawing.Color.White;
            this.txtTienDatCoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTienDatCoc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTienDatCoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTienDatCoc.Location = new System.Drawing.Point(10, 8);
            this.txtTienDatCoc.Name = "txtTienDatCoc";
            this.txtTienDatCoc.Size = new System.Drawing.Size(187, 22);
            this.txtTienDatCoc.TabIndex = 0;
            this.txtTienDatCoc.Text = "0";
            this.txtTienDatCoc.TextChanged += new System.EventHandler(this.TinhTongTienTuDong);
            // 
            // lblPTTT
            // 
            this.lblPTTT.AutoSize = true;
            this.lblPTTT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPTTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPTTT.Location = new System.Drawing.Point(22, 396);
            this.lblPTTT.Name = "lblPTTT";
            this.lblPTTT.Size = new System.Drawing.Size(161, 20);
            this.lblPTTT.TabIndex = 9;
            this.lblPTTT.Text = "Hình thức thanh toán *";
            // 
            // pnlPTTTWrap
            // 
            this.pnlPTTTWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPTTTWrap.BackColor = System.Drawing.Color.White;
            this.pnlPTTTWrap.Controls.Add(this.cboPTTT);
            this.pnlPTTTWrap.Location = new System.Drawing.Point(24, 420);
            this.pnlPTTTWrap.Name = "pnlPTTTWrap";
            this.pnlPTTTWrap.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlPTTTWrap.Size = new System.Drawing.Size(424, 38);
            this.pnlPTTTWrap.TabIndex = 10;
            this.pnlPTTTWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // cboPTTT
            // 
            this.cboPTTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPTTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPTTT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboPTTT.FormattingEnabled = true;
            this.cboPTTT.Location = new System.Drawing.Point(6, 4);
            this.cboPTTT.Name = "cboPTTT";
            this.cboPTTT.Size = new System.Drawing.Size(412, 29);
            this.cboPTTT.TabIndex = 0;
            // 
            // pnlTongTienBox
            // 
            this.pnlTongTienBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTongTienBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.pnlTongTienBox.Controls.Add(this.lblTongTienValue);
            this.pnlTongTienBox.Controls.Add(this.lblTongTienTitle);
            this.pnlTongTienBox.Location = new System.Drawing.Point(24, 476);
            this.pnlTongTienBox.Name = "pnlTongTienBox";
            this.pnlTongTienBox.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlTongTienBox.Size = new System.Drawing.Size(424, 64);
            this.pnlTongTienBox.TabIndex = 11;
            this.pnlTongTienBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawOrangeAccentBox);
            // 
            // lblTongTienTitle
            // 
            this.lblTongTienTitle.AutoSize = true;
            this.lblTongTienTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongTienTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
            this.lblTongTienTitle.Location = new System.Drawing.Point(12, 21);
            this.lblTongTienTitle.Name = "lblTongTienTitle";
            this.lblTongTienTitle.Size = new System.Drawing.Size(126, 21);
            this.lblTongTienTitle.TabIndex = 0;
            this.lblTongTienTitle.Text = "CẦN THU TIẾP:";
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblTongTienValue.Location = new System.Drawing.Point(150, 14);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(262, 35);
            this.lblTongTienValue.TabIndex = 1;
            this.lblTongTienValue.Text = "0 đ";
            this.lblTongTienValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomButtons.Controls.Add(this.btnInHoaDon);
            this.pnlBottomButtons.Controls.Add(this.btnLamMoi);
            this.pnlBottomButtons.Controls.Add(this.btnThanhToan);
            this.pnlBottomButtons.Location = new System.Drawing.Point(24, 558);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(424, 44);
            this.pnlBottomButtons.TabIndex = 12;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(282, 0);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(142, 40);
            this.btnThanhToan.TabIndex = 2;
            this.btnThanhToan.Text = "Xác nhận thu";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            this.btnThanhToan.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
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
            this.btnLamMoi.Location = new System.Drawing.Point(182, 0);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(92, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.BackColor = System.Drawing.Color.White;
            this.btnInHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHoaDon.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnInHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnInHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.btnInHoaDon.Location = new System.Drawing.Point(0, 0);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(100, 40);
            this.btnInHoaDon.TabIndex = 0;
            this.btnInHoaDon.Text = "🖨️ In bill";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHoaDon";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Hóa Đơn";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.tblMainLayout.ResumeLayout(false);
            this.cardLeft.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.pnlMaHDWrap.ResumeLayout(false);
            this.pnlMaHDWrap.PerformLayout();
            this.pnlMaDPWrap.ResumeLayout(false);
            this.tbl2ColMoney1.ResumeLayout(false);
            this.pnlColTienPhong.ResumeLayout(false);
            this.pnlColTienPhong.PerformLayout();
            this.pnlTienPhongWrap.ResumeLayout(false);
            this.pnlTienPhongWrap.PerformLayout();
            this.pnlColTienDV.ResumeLayout(false);
            this.pnlColTienDV.PerformLayout();
            this.pnlTienDVWrap.ResumeLayout(false);
            this.pnlTienDVWrap.PerformLayout();
            this.tbl2ColMoney2.ResumeLayout(false);
            this.pnlColGiamGia.ResumeLayout(false);
            this.pnlColGiamGia.PerformLayout();
            this.pnlGiamGiaWrap.ResumeLayout(false);
            this.pnlGiamGiaWrap.PerformLayout();
            this.pnlColDatCoc.ResumeLayout(false);
            this.pnlColDatCoc.PerformLayout();
            this.pnlDatCocWrap.ResumeLayout(false);
            this.pnlDatCocWrap.PerformLayout();
            this.pnlPTTTWrap.ResumeLayout(false);
            this.pnlTongTienBox.ResumeLayout(false);
            this.pnlTongTienBox.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCardSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.Panel pnlMaHDWrap;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblMaDatPhong;
        private System.Windows.Forms.Panel pnlMaDPWrap;
        private System.Windows.Forms.ComboBox cboDatPhong;
        private System.Windows.Forms.TableLayoutPanel tbl2ColMoney1;
        private System.Windows.Forms.Panel pnlColTienPhong;
        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.Panel pnlTienPhongWrap;
        private System.Windows.Forms.TextBox txtTienPhong;
        private System.Windows.Forms.Panel pnlColTienDV;
        private System.Windows.Forms.Label lblTienDV;
        private System.Windows.Forms.Panel pnlTienDVWrap;
        private System.Windows.Forms.TextBox txtTienDV;
        private System.Windows.Forms.TableLayoutPanel tbl2ColMoney2;
        private System.Windows.Forms.Panel pnlColGiamGia;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Panel pnlGiamGiaWrap;
        private System.Windows.Forms.TextBox txtTienGiamGia;
        private System.Windows.Forms.Panel pnlColDatCoc;
        private System.Windows.Forms.Label lblDatCoc;
        private System.Windows.Forms.Panel pnlDatCocWrap;
        private System.Windows.Forms.TextBox txtTienDatCoc;
        private System.Windows.Forms.Label lblPTTT;
        private System.Windows.Forms.Panel pnlPTTTWrap;
        private System.Windows.Forms.ComboBox cboPTTT;
        private System.Windows.Forms.Panel pnlTongTienBox;
        private System.Windows.Forms.Label lblTongTienTitle;
        private System.Windows.Forms.Label lblTongTienValue;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnInHoaDon;
    }
}