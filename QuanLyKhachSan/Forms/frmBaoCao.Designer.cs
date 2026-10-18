namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmBaoCao
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlFilterWrap = new System.Windows.Forms.Panel();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.tblKpiCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.lblKpi1Value = new System.Windows.Forms.Label();
            this.cardKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.lblKpi2Value = new System.Windows.Forms.Label();
            this.cardKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.lblKpi3Value = new System.Windows.Forms.Label();
            this.cardKpi4 = new System.Windows.Forms.Panel();
            this.lblKpi4Title = new System.Windows.Forms.Label();
            this.lblKpi4Value = new System.Windows.Forms.Label();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.cardGridLeft = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.cardGridRight = new System.Windows.Forms.Panel();
            this.lblTopDvTitle = new System.Windows.Forms.Label();
            this.dgvTopDichVu = new System.Windows.Forms.DataGridView();
            this.pnlContainer.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.pnlFilterWrap.SuspendLayout();
            this.tblKpiCards.SuspendLayout();
            this.cardKpi1.SuspendLayout();
            this.cardKpi2.SuspendLayout();
            this.cardKpi3.SuspendLayout();
            this.cardKpi4.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.cardGridLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            this.cardGridRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblContent);
            this.pnlContainer.Controls.Add(this.tblKpiCards);
            this.pnlContainer.Controls.Add(this.pnlTopBar);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(20, 20);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1240, 720);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.Controls.Add(this.pnlFilterWrap);
            this.pnlTopBar.Controls.Add(this.lblSubtitle);
            this.pnlTopBar.Controls.Add(this.lblTitle);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1240, 68);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO DOANH THU";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(2, 38);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(342, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Thống kê tổng quan tình hình kinh doanh khách sạn";
            // 
            // pnlFilterWrap
            // 
            this.pnlFilterWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilterWrap.Controls.Add(this.btnXuatExcel);
            this.pnlFilterWrap.Controls.Add(this.btnLoc);
            this.pnlFilterWrap.Controls.Add(this.dtpDenNgay);
            this.pnlFilterWrap.Controls.Add(this.lblDenNgay);
            this.pnlFilterWrap.Controls.Add(this.dtpTuNgay);
            this.pnlFilterWrap.Controls.Add(this.lblTuNgay);
            this.pnlFilterWrap.Location = new System.Drawing.Point(540, 10);
            this.pnlFilterWrap.Name = "pnlFilterWrap";
            this.pnlFilterWrap.Size = new System.Drawing.Size(700, 48);
            this.pnlFilterWrap.TabIndex = 2;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTuNgay.Location = new System.Drawing.Point(8, 14);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(31, 20);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(44, 9);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(125, 29);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDenNgay.Location = new System.Drawing.Point(180, 14);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(39, 20);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(224, 9);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(125, 29);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatAppearance.BorderSize = 0;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(365, 6);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(140, 36);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            this.btnLoc.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.White;
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnXuatExcel.Location = new System.Drawing.Point(515, 6);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(170, 36);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "📊 Xuất Báo Cáo";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // tblKpiCards
            // 
            this.tblKpiCards.ColumnCount = 4;
            this.tblKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpiCards.Controls.Add(this.cardKpi1, 0, 0);
            this.tblKpiCards.Controls.Add(this.cardKpi2, 1, 0);
            this.tblKpiCards.Controls.Add(this.cardKpi3, 2, 0);
            this.tblKpiCards.Controls.Add(this.cardKpi4, 3, 0);
            this.tblKpiCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblKpiCards.Location = new System.Drawing.Point(0, 68);
            this.tblKpiCards.Margin = new System.Windows.Forms.Padding(0);
            this.tblKpiCards.Name = "tblKpiCards";
            this.tblKpiCards.RowCount = 1;
            this.tblKpiCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblKpiCards.Size = new System.Drawing.Size(1240, 105);
            this.tblKpiCards.TabIndex = 1;
            // 
            // cardKpi1
            // 
            this.cardKpi1.BackColor = System.Drawing.Color.White;
            this.cardKpi1.Controls.Add(this.lblKpi1Value);
            this.cardKpi1.Controls.Add(this.lblKpi1Title);
            this.cardKpi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi1.Location = new System.Drawing.Point(0, 0);
            this.cardKpi1.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.cardKpi1.Name = "cardKpi1";
            this.cardKpi1.Padding = new System.Windows.Forms.Padding(16);
            this.cardKpi1.Size = new System.Drawing.Size(298, 105);
            this.cardKpi1.TabIndex = 0;
            this.cardKpi1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblKpi1Title
            // 
            this.lblKpi1Title.AutoSize = true;
            this.lblKpi1Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKpi1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi1Title.Location = new System.Drawing.Point(14, 16);
            this.lblKpi1Title.Name = "lblKpi1Title";
            this.lblKpi1Title.Size = new System.Drawing.Size(142, 20);
            this.lblKpi1Title.TabIndex = 0;
            this.lblKpi1Title.Text = "TỔNG DOANH THU";
            // 
            // lblKpi1Value
            // 
            this.lblKpi1Value.AutoSize = true;
            this.lblKpi1Value.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblKpi1Value.Location = new System.Drawing.Point(12, 45);
            this.lblKpi1Value.Name = "lblKpi1Value";
            this.lblKpi1Value.Size = new System.Drawing.Size(65, 33);
            this.lblKpi1Value.TabIndex = 1;
            this.lblKpi1Value.Text = "0 đ";
            // 
            // cardKpi2
            // 
            this.cardKpi2.BackColor = System.Drawing.Color.White;
            this.cardKpi2.Controls.Add(this.lblKpi2Value);
            this.cardKpi2.Controls.Add(this.lblKpi2Title);
            this.cardKpi2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi2.Location = new System.Drawing.Point(310, 0);
            this.cardKpi2.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.cardKpi2.Name = "cardKpi2";
            this.cardKpi2.Padding = new System.Windows.Forms.Padding(16);
            this.cardKpi2.Size = new System.Drawing.Size(298, 105);
            this.cardKpi2.TabIndex = 1;
            this.cardKpi2.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblKpi2Title
            // 
            this.lblKpi2Title.AutoSize = true;
            this.lblKpi2Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKpi2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi2Title.Location = new System.Drawing.Point(14, 16);
            this.lblKpi2Title.Name = "lblKpi2Title";
            this.lblKpi2Title.Size = new System.Drawing.Size(147, 20);
            this.lblKpi2Title.TabIndex = 0;
            this.lblKpi2Title.Text = "DOANH THU PHÒNG";
            // 
            // lblKpi2Value
            // 
            this.lblKpi2Value.AutoSize = true;
            this.lblKpi2Value.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblKpi2Value.Location = new System.Drawing.Point(12, 45);
            this.lblKpi2Value.Name = "lblKpi2Value";
            this.lblKpi2Value.Size = new System.Drawing.Size(65, 33);
            this.lblKpi2Value.TabIndex = 1;
            this.lblKpi2Value.Text = "0 đ";
            // 
            // cardKpi3
            // 
            this.cardKpi3.BackColor = System.Drawing.Color.White;
            this.cardKpi3.Controls.Add(this.lblKpi3Value);
            this.cardKpi3.Controls.Add(this.lblKpi3Title);
            this.cardKpi3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi3.Location = new System.Drawing.Point(620, 0);
            this.cardKpi3.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.cardKpi3.Name = "cardKpi3";
            this.cardKpi3.Padding = new System.Windows.Forms.Padding(16);
            this.cardKpi3.Size = new System.Drawing.Size(298, 105);
            this.cardKpi3.TabIndex = 2;
            this.cardKpi3.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblKpi3Title
            // 
            this.lblKpi3Title.AutoSize = true;
            this.lblKpi3Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKpi3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi3Title.Location = new System.Drawing.Point(14, 16);
            this.lblKpi3Title.Name = "lblKpi3Title";
            this.lblKpi3Title.Size = new System.Drawing.Size(155, 20);
            this.lblKpi3Title.TabIndex = 0;
            this.lblKpi3Title.Text = "DOANH THU DỊCH VỤ";
            // 
            // lblKpi3Value
            // 
            this.lblKpi3Value.AutoSize = true;
            this.lblKpi3Value.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblKpi3Value.Location = new System.Drawing.Point(12, 45);
            this.lblKpi3Value.Name = "lblKpi3Value";
            this.lblKpi3Value.Size = new System.Drawing.Size(65, 33);
            this.lblKpi3Value.TabIndex = 1;
            this.lblKpi3Value.Text = "0 đ";
            // 
            // cardKpi4
            // 
            this.cardKpi4.BackColor = System.Drawing.Color.White;
            this.cardKpi4.Controls.Add(this.lblKpi4Value);
            this.cardKpi4.Controls.Add(this.lblKpi4Title);
            this.cardKpi4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi4.Location = new System.Drawing.Point(930, 0);
            this.cardKpi4.Margin = new System.Windows.Forms.Padding(0);
            this.cardKpi4.Name = "cardKpi4";
            this.cardKpi4.Padding = new System.Windows.Forms.Padding(16);
            this.cardKpi4.Size = new System.Drawing.Size(310, 105);
            this.cardKpi4.TabIndex = 3;
            this.cardKpi4.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblKpi4Title
            // 
            this.lblKpi4Title.AutoSize = true;
            this.lblKpi4Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKpi4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi4Title.Location = new System.Drawing.Point(14, 16);
            this.lblKpi4Title.Name = "lblKpi4Title";
            this.lblKpi4Title.Size = new System.Drawing.Size(150, 20);
            this.lblKpi4Title.TabIndex = 0;
            this.lblKpi4Title.Text = "SỐ HÓA ĐƠN ĐÃ THU";
            // 
            // lblKpi4Value
            // 
            this.lblKpi4Value.AutoSize = true;
            this.lblKpi4Value.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi4Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblKpi4Value.Location = new System.Drawing.Point(12, 45);
            this.lblKpi4Value.Name = "lblKpi4Value";
            this.lblKpi4Value.Size = new System.Drawing.Size(28, 33);
            this.lblKpi4Value.TabIndex = 1;
            this.lblKpi4Value.Text = "0";
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblContent.Controls.Add(this.cardGridLeft, 0, 0);
            this.tblContent.Controls.Add(this.cardGridRight, 1, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 173);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0);
            this.tblContent.Name = "tblContent";
            this.tblContent.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(1240, 547);
            this.tblContent.TabIndex = 2;
            // 
            // cardGridLeft
            // 
            this.cardGridLeft.BackColor = System.Drawing.Color.White;
            this.cardGridLeft.Controls.Add(this.dgvDoanhThu);
            this.cardGridLeft.Controls.Add(this.lblGridTitle);
            this.cardGridLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGridLeft.Location = new System.Drawing.Point(0, 16);
            this.cardGridLeft.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.cardGridLeft.Name = "cardGridLeft";
            this.cardGridLeft.Padding = new System.Windows.Forms.Padding(18);
            this.cardGridLeft.Size = new System.Drawing.Size(779, 531);
            this.cardGridLeft.TabIndex = 0;
            this.cardGridLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblGridTitle.Location = new System.Drawing.Point(18, 14);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(252, 25);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Danh Sách Hóa Đơn Đã Thu";
            // 
            // dgvDoanhThu
            // 
            this.dgvDoanhThu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDoanhThu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoanhThu.Location = new System.Drawing.Point(18, 50);
            this.dgvDoanhThu.Name = "dgvDoanhThu";
            this.dgvDoanhThu.RowHeadersWidth = 51;
            this.dgvDoanhThu.Size = new System.Drawing.Size(743, 463);
            this.dgvDoanhThu.TabIndex = 1;
            // 
            // cardGridRight
            // 
            this.cardGridRight.BackColor = System.Drawing.Color.White;
            this.cardGridRight.Controls.Add(this.dgvTopDichVu);
            this.cardGridRight.Controls.Add(this.lblTopDvTitle);
            this.cardGridRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGridRight.Location = new System.Drawing.Point(793, 16);
            this.cardGridRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardGridRight.Name = "cardGridRight";
            this.cardGridRight.Padding = new System.Windows.Forms.Padding(18);
            this.cardGridRight.Size = new System.Drawing.Size(447, 531);
            this.cardGridRight.TabIndex = 1;
            this.cardGridRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblTopDvTitle
            // 
            this.lblTopDvTitle.AutoSize = true;
            this.lblTopDvTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTopDvTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTopDvTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTopDvTitle.Name = "lblTopDvTitle";
            this.lblTopDvTitle.Size = new System.Drawing.Size(262, 25);
            this.lblTopDvTitle.TabIndex = 0;
            this.lblTopDvTitle.Text = "Top Dịch Vụ Doanh Thu Cao";
            // 
            // dgvTopDichVu
            // 
            this.dgvTopDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopDichVu.Location = new System.Drawing.Point(18, 50);
            this.dgvTopDichVu.Name = "dgvTopDichVu";
            this.dgvTopDichVu.RowHeadersWidth = 51;
            this.dgvTopDichVu.Size = new System.Drawing.Size(411, 463);
            this.dgvTopDichVu.TabIndex = 1;
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBaoCao";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo Cáo Doanh Thu";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlFilterWrap.ResumeLayout(false);
            this.pnlFilterWrap.PerformLayout();
            this.tblKpiCards.ResumeLayout(false);
            this.cardKpi1.ResumeLayout(false);
            this.cardKpi1.PerformLayout();
            this.cardKpi2.ResumeLayout(false);
            this.cardKpi2.PerformLayout();
            this.cardKpi3.ResumeLayout(false);
            this.cardKpi3.PerformLayout();
            this.cardKpi4.ResumeLayout(false);
            this.cardKpi4.PerformLayout();
            this.tblContent.ResumeLayout(false);
            this.cardGridLeft.ResumeLayout(false);
            this.cardGridLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            this.cardGridRight.ResumeLayout(false);
            this.cardGridRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFilterWrap;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.TableLayoutPanel tblKpiCards;
        private System.Windows.Forms.Panel cardKpi1;
        private System.Windows.Forms.Label lblKpi1Title;
        private System.Windows.Forms.Label lblKpi1Value;
        private System.Windows.Forms.Panel cardKpi2;
        private System.Windows.Forms.Label lblKpi2Title;
        private System.Windows.Forms.Label lblKpi2Value;
        private System.Windows.Forms.Panel cardKpi3;
        private System.Windows.Forms.Label lblKpi3Title;
        private System.Windows.Forms.Label lblKpi3Value;
        private System.Windows.Forms.Panel cardKpi4;
        private System.Windows.Forms.Label lblKpi4Title;
        private System.Windows.Forms.Label lblKpi4Value;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Panel cardGridLeft;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvDoanhThu;
        private System.Windows.Forms.Panel cardGridRight;
        private System.Windows.Forms.Label lblTopDvTitle;
        private System.Windows.Forms.DataGridView dgvTopDichVu;
    }
}