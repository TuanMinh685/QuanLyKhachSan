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
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.cardGridLeft = new System.Windows.Forms.Panel();
            this.dgvTopKhachHang = new System.Windows.Forms.DataGridView();
            this.dgvCongSuat = new System.Windows.Forms.DataGridView();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.cardGridRight = new System.Windows.Forms.Panel();
            this.pnlVipInfo = new System.Windows.Forms.Panel();
            this.lblVipDesc = new System.Windows.Forms.Label();
            this.lblVipTitle = new System.Windows.Forms.Label();
            this.dgvLuotDat = new System.Windows.Forms.DataGridView();
            this.dgvTopDichVu = new System.Windows.Forms.DataGridView();
            this.lblTopDvTitle = new System.Windows.Forms.Label();
            this.tblKpiCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Value = new System.Windows.Forms.Label();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.cardKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Value = new System.Windows.Forms.Label();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.cardKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Value = new System.Windows.Forms.Label();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.cardKpi4 = new System.Windows.Forms.Panel();
            this.lblKpi4Value = new System.Windows.Forms.Label();
            this.lblKpi4Title = new System.Windows.Forms.Label();
            this.pnlNavTabs = new System.Windows.Forms.Panel();
            this.btnTabKhachHang = new System.Windows.Forms.Button();
            this.btnTabCongSuat = new System.Windows.Forms.Button();
            this.btnTabDoanhThu = new System.Windows.Forms.Button();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.pnlFilterWrap = new System.Windows.Forms.Panel();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.cardGridLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            this.cardGridRight.SuspendLayout();
            this.pnlVipInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuotDat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).BeginInit();
            this.tblKpiCards.SuspendLayout();
            this.cardKpi1.SuspendLayout();
            this.cardKpi2.SuspendLayout();
            this.cardKpi3.SuspendLayout();
            this.cardKpi4.SuspendLayout();
            this.pnlNavTabs.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.pnlFilterWrap.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblContent);
            this.pnlContainer.Controls.Add(this.tblKpiCards);
            this.pnlContainer.Controls.Add(this.pnlNavTabs);
            this.pnlContainer.Controls.Add(this.pnlTopBar);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(20, 20);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1240, 720);
            this.pnlContainer.TabIndex = 0;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblContent.Controls.Add(this.cardGridLeft, 0, 0);
            this.tblContent.Controls.Add(this.cardGridRight, 1, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 219);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0);
            this.tblContent.Name = "tblContent";
            this.tblContent.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(1240, 501);
            this.tblContent.TabIndex = 3;
            // 
            // cardGridLeft
            // 
            this.cardGridLeft.BackColor = System.Drawing.Color.White;
            this.cardGridLeft.Controls.Add(this.dgvTopKhachHang);
            this.cardGridLeft.Controls.Add(this.dgvCongSuat);
            this.cardGridLeft.Controls.Add(this.dgvDoanhThu);
            this.cardGridLeft.Controls.Add(this.lblGridTitle);
            this.cardGridLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGridLeft.Location = new System.Drawing.Point(0, 12);
            this.cardGridLeft.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.cardGridLeft.Name = "cardGridLeft";
            this.cardGridLeft.Padding = new System.Windows.Forms.Padding(18);
            this.cardGridLeft.Size = new System.Drawing.Size(779, 489);
            this.cardGridLeft.TabIndex = 0;
            this.cardGridLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // dgvTopKhachHang
            // 
            this.dgvTopKhachHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopKhachHang.Location = new System.Drawing.Point(18, 50);
            this.dgvTopKhachHang.Name = "dgvTopKhachHang";
            this.dgvTopKhachHang.RowHeadersWidth = 51;
            this.dgvTopKhachHang.Size = new System.Drawing.Size(743, 421);
            this.dgvTopKhachHang.TabIndex = 3;
            this.dgvTopKhachHang.Visible = false;
            // 
            // dgvCongSuat
            // 
            this.dgvCongSuat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCongSuat.BackgroundColor = System.Drawing.Color.White;
            this.dgvCongSuat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCongSuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCongSuat.Location = new System.Drawing.Point(18, 50);
            this.dgvCongSuat.Name = "dgvCongSuat";
            this.dgvCongSuat.RowHeadersWidth = 51;
            this.dgvCongSuat.Size = new System.Drawing.Size(743, 421);
            this.dgvCongSuat.TabIndex = 2;
            this.dgvCongSuat.Visible = false;
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
            this.dgvDoanhThu.Size = new System.Drawing.Size(743, 421);
            this.dgvDoanhThu.TabIndex = 1;
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
            // cardGridRight
            // 
            this.cardGridRight.BackColor = System.Drawing.Color.White;
            this.cardGridRight.Controls.Add(this.pnlVipInfo);
            this.cardGridRight.Controls.Add(this.dgvLuotDat);
            this.cardGridRight.Controls.Add(this.dgvTopDichVu);
            this.cardGridRight.Controls.Add(this.lblTopDvTitle);
            this.cardGridRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGridRight.Location = new System.Drawing.Point(793, 12);
            this.cardGridRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardGridRight.Name = "cardGridRight";
            this.cardGridRight.Padding = new System.Windows.Forms.Padding(18);
            this.cardGridRight.Size = new System.Drawing.Size(447, 489);
            this.cardGridRight.TabIndex = 1;
            this.cardGridRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // pnlVipInfo
            // 
            this.pnlVipInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlVipInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.pnlVipInfo.Controls.Add(this.lblVipDesc);
            this.pnlVipInfo.Controls.Add(this.lblVipTitle);
            this.pnlVipInfo.Location = new System.Drawing.Point(18, 50);
            this.pnlVipInfo.Name = "pnlVipInfo";
            this.pnlVipInfo.Padding = new System.Windows.Forms.Padding(16);
            this.pnlVipInfo.Size = new System.Drawing.Size(411, 421);
            this.pnlVipInfo.TabIndex = 3;
            this.pnlVipInfo.Visible = false;
            // 
            // lblVipDesc
            // 
            this.lblVipDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVipDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVipDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblVipDesc.Location = new System.Drawing.Point(16, 48);
            this.lblVipDesc.Name = "lblVipDesc";
            this.lblVipDesc.Size = new System.Drawing.Size(379, 357);
            this.lblVipDesc.TabIndex = 1;
            this.lblVipDesc.Text = "• Nhóm VIP: Khách lưu trú từ 10 lần trở lên. Được ưu đãi giảm 15% tổng hóa đơn v" +
    "à hỗ trợ check-in sớm.\r\n\r\n• Nhóm Thân thiết: Khách lưu trú từ 3 lần trở lên. Tặ" +
    "ng voucher 50.000đ khi đặt phòng tiếp theo.\r\n\r\n• Nhóm Mới: Khách hàng lưu trú l" +
    "ần đầu tiên.";
            // 
            // lblVipTitle
            // 
            this.lblVipTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVipTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVipTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblVipTitle.Location = new System.Drawing.Point(16, 16);
            this.lblVipTitle.Name = "lblVipTitle";
            this.lblVipTitle.Size = new System.Drawing.Size(379, 32);
            this.lblVipTitle.TabIndex = 0;
            this.lblVipTitle.Text = "🎖️ CHÍNH SÁCH KHÁCH HÀNG VIP";
            // 
            // dgvLuotDat
            // 
            this.dgvLuotDat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLuotDat.BackgroundColor = System.Drawing.Color.White;
            this.dgvLuotDat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLuotDat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLuotDat.Location = new System.Drawing.Point(18, 50);
            this.dgvLuotDat.Name = "dgvLuotDat";
            this.dgvLuotDat.RowHeadersWidth = 51;
            this.dgvLuotDat.Size = new System.Drawing.Size(411, 421);
            this.dgvLuotDat.TabIndex = 2;
            this.dgvLuotDat.Visible = false;
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
            this.dgvTopDichVu.Size = new System.Drawing.Size(411, 421);
            this.dgvTopDichVu.TabIndex = 1;
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
            this.tblKpiCards.Location = new System.Drawing.Point(0, 114);
            this.tblKpiCards.Margin = new System.Windows.Forms.Padding(0);
            this.tblKpiCards.Name = "tblKpiCards";
            this.tblKpiCards.RowCount = 1;
            this.tblKpiCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblKpiCards.Size = new System.Drawing.Size(1240, 105);
            this.tblKpiCards.TabIndex = 2;
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
            // pnlNavTabs
            // 
            this.pnlNavTabs.Controls.Add(this.btnTabKhachHang);
            this.pnlNavTabs.Controls.Add(this.btnTabCongSuat);
            this.pnlNavTabs.Controls.Add(this.btnTabDoanhThu);
            this.pnlNavTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavTabs.Location = new System.Drawing.Point(0, 68);
            this.pnlNavTabs.Name = "pnlNavTabs";
            this.pnlNavTabs.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.pnlNavTabs.Size = new System.Drawing.Size(1240, 46);
            this.pnlNavTabs.TabIndex = 1;
            // 
            // btnTabKhachHang
            // 
            this.btnTabKhachHang.BackColor = System.Drawing.Color.White;
            this.btnTabKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabKhachHang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnTabKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTabKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTabKhachHang.Location = new System.Drawing.Point(420, 4);
            this.btnTabKhachHang.Name = "btnTabKhachHang";
            this.btnTabKhachHang.Size = new System.Drawing.Size(200, 34);
            this.btnTabKhachHang.TabIndex = 2;
            this.btnTabKhachHang.Text = "👑 Top Khách Hàng VIP";
            this.btnTabKhachHang.UseVisualStyleBackColor = false;
            this.btnTabKhachHang.Click += new System.EventHandler(this.btnTabKhachHang_Click);
            this.btnTabKhachHang.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // btnTabCongSuat
            // 
            this.btnTabCongSuat.BackColor = System.Drawing.Color.White;
            this.btnTabCongSuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabCongSuat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnTabCongSuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabCongSuat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTabCongSuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTabCongSuat.Location = new System.Drawing.Point(210, 4);
            this.btnTabCongSuat.Name = "btnTabCongSuat";
            this.btnTabCongSuat.Size = new System.Drawing.Size(200, 34);
            this.btnTabCongSuat.TabIndex = 1;
            this.btnTabCongSuat.Text = "🏨 Công Suất & Lượt Đặt";
            this.btnTabCongSuat.UseVisualStyleBackColor = false;
            this.btnTabCongSuat.Click += new System.EventHandler(this.btnTabCongSuat_Click);
            this.btnTabCongSuat.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // btnTabDoanhThu
            // 
            this.btnTabDoanhThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnTabDoanhThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabDoanhThu.FlatAppearance.BorderSize = 0;
            this.btnTabDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTabDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnTabDoanhThu.Location = new System.Drawing.Point(0, 4);
            this.btnTabDoanhThu.Name = "btnTabDoanhThu";
            this.btnTabDoanhThu.Size = new System.Drawing.Size(200, 34);
            this.btnTabDoanhThu.TabIndex = 0;
            this.btnTabDoanhThu.Text = "📊 Doanh Thu & Dịch Vụ";
            this.btnTabDoanhThu.UseVisualStyleBackColor = false;
            this.btnTabDoanhThu.Click += new System.EventHandler(this.btnTabDoanhThu_Click);
            this.btnTabDoanhThu.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
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
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(224, 9);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(125, 29);
            this.dtpDenNgay.TabIndex = 3;
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
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(44, 9);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(125, 29);
            this.dtpTuNgay.TabIndex = 1;
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
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO & THỐNG KÊ";
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
            this.Text = "Báo Cáo & Thống Kê";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.pnlContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.cardGridLeft.ResumeLayout(false);
            this.cardGridLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            this.cardGridRight.ResumeLayout(false);
            this.cardGridRight.PerformLayout();
            this.pnlVipInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuotDat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).EndInit();
            this.tblKpiCards.ResumeLayout(false);
            this.cardKpi1.ResumeLayout(false);
            this.cardKpi1.PerformLayout();
            this.cardKpi2.ResumeLayout(false);
            this.cardKpi2.PerformLayout();
            this.cardKpi3.ResumeLayout(false);
            this.cardKpi3.PerformLayout();
            this.cardKpi4.ResumeLayout(false);
            this.cardKpi4.PerformLayout();
            this.pnlNavTabs.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlFilterWrap.ResumeLayout(false);
            this.pnlFilterWrap.PerformLayout();
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
        private System.Windows.Forms.Panel pnlNavTabs;
        private System.Windows.Forms.Button btnTabDoanhThu;
        private System.Windows.Forms.Button btnTabCongSuat;
        private System.Windows.Forms.Button btnTabKhachHang;
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
        private System.Windows.Forms.DataGridView dgvCongSuat;
        private System.Windows.Forms.DataGridView dgvTopKhachHang;
        private System.Windows.Forms.Panel cardGridRight;
        private System.Windows.Forms.Label lblTopDvTitle;
        private System.Windows.Forms.DataGridView dgvTopDichVu;
        private System.Windows.Forms.DataGridView dgvLuotDat;
        private System.Windows.Forms.Panel pnlVipInfo;
        private System.Windows.Forms.Label lblVipTitle;
        private System.Windows.Forms.Label lblVipDesc;
    }
}
