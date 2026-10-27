namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmPhuongThucThanhToan
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

        private void InitializeComponent()
        {
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cardLeft = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblIconSearch = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlGridWrapper = new System.Windows.Forms.Panel();
            this.dgvPTTT = new System.Windows.Forms.DataGridView();
            this.cardRight = new System.Windows.Forms.Panel();
            this.lblRightTitle = new System.Windows.Forms.Label();
            this.lblRightSubtitle = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblMaPTTT = new System.Windows.Forms.Label();
            this.pnlMaWrap = new System.Windows.Forms.Panel();
            this.txtMaPTTT = new System.Windows.Forms.TextBox();
            this.lblTenPTTT = new System.Windows.Forms.Label();
            this.pnlTenWrap = new System.Windows.Forms.Panel();
            this.txtTenPTTT = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.pnlMoTaWrap = new System.Windows.Forms.Panel();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.chkKichHoat = new System.Windows.Forms.CheckBox();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.tblLayout.SuspendLayout();
            this.cardLeft.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTTT)).BeginInit();
            this.cardRight.SuspendLayout();
            this.pnlMaWrap.SuspendLayout();
            this.pnlTenWrap.SuspendLayout();
            this.pnlMoTaWrap.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLayout
            // 
            this.tblLayout.ColumnCount = 2;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblLayout.Controls.Add(this.cardLeft, 0, 0);
            this.tblLayout.Controls.Add(this.cardRight, 1, 0);
            this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayout.Location = new System.Drawing.Point(20, 20);
            this.tblLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 1;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.Size = new System.Drawing.Size(1200, 680);
            this.tblLayout.TabIndex = 0;
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
            this.cardLeft.Size = new System.Drawing.Size(752, 680);
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
            this.lblIconSearch.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
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
            this.txtTimKiem.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtTimKiem.Location = new System.Drawing.Point(44, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm theo tên phương thức thanh toán hoặc ghi chú...";
            this.txtTimKiem.Size = new System.Drawing.Size(656, 22);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlGridWrapper
            // 
            this.pnlGridWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridWrapper.Controls.Add(this.dgvPTTT);
            this.pnlGridWrapper.Location = new System.Drawing.Point(20, 72);
            this.pnlGridWrapper.Name = "pnlGridWrapper";
            this.pnlGridWrapper.Size = new System.Drawing.Size(712, 588);
            this.pnlGridWrapper.TabIndex = 1;
            // 
            // dgvPTTT
            // 
            this.dgvPTTT.BackgroundColor = System.Drawing.Color.White;
            this.dgvPTTT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPTTT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPTTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPTTT.Location = new System.Drawing.Point(0, 0);
            this.dgvPTTT.Name = "dgvPTTT";
            this.dgvPTTT.RowHeadersWidth = 51;
            this.dgvPTTT.Size = new System.Drawing.Size(712, 588);
            this.dgvPTTT.TabIndex = 0;
            this.dgvPTTT.SelectionChanged += new System.EventHandler(this.dgvPTTT_SelectionChanged);
            // 
            // cardRight
            // 
            this.cardRight.AutoScroll = true;
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.lblRightTitle);
            this.cardRight.Controls.Add(this.lblRightSubtitle);
            this.cardRight.Controls.Add(this.pnlDivider);
            this.cardRight.Controls.Add(this.lblMaPTTT);
            this.cardRight.Controls.Add(this.pnlMaWrap);
            this.cardRight.Controls.Add(this.lblTenPTTT);
            this.cardRight.Controls.Add(this.pnlTenWrap);
            this.cardRight.Controls.Add(this.lblMoTa);
            this.cardRight.Controls.Add(this.pnlMoTaWrap);
            this.cardRight.Controls.Add(this.chkKichHoat);
            this.cardRight.Controls.Add(this.pnlBottomButtons);
            this.cardRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRight.Location = new System.Drawing.Point(768, 0);
            this.cardRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardRight.Name = "cardRight";
            this.cardRight.Padding = new System.Windows.Forms.Padding(22);
            this.cardRight.Size = new System.Drawing.Size(432, 680);
            this.cardRight.TabIndex = 1;
            this.cardRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // lblRightTitle
            // 
            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRightTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblRightTitle.Location = new System.Drawing.Point(20, 18);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(262, 28);
            this.lblRightTitle.TabIndex = 0;
            this.lblRightTitle.Text = "Thông tin phương thức";
            // 
            // lblRightSubtitle
            // 
            this.lblRightSubtitle.AutoSize = true;
            this.lblRightSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRightSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblRightSubtitle.Location = new System.Drawing.Point(21, 44);
            this.lblRightSubtitle.Name = "lblRightSubtitle";
            this.lblRightSubtitle.Size = new System.Drawing.Size(260, 19);
            this.lblRightSubtitle.TabIndex = 1;
            this.lblRightSubtitle.Text = "Cấu hình hình thức thu tiền tại quầy lễ tân";
            // 
            // pnlDivider
            // 
            this.pnlDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlDivider.Location = new System.Drawing.Point(22, 72);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(388, 1);
            this.pnlDivider.TabIndex = 2;
            // 
            // lblMaPTTT
            // 
            this.lblMaPTTT.AutoSize = true;
            this.lblMaPTTT.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblMaPTTT.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMaPTTT.Location = new System.Drawing.Point(20, 88);
            this.lblMaPTTT.Name = "lblMaPTTT";
            this.lblMaPTTT.Size = new System.Drawing.Size(126, 20);
            this.lblMaPTTT.TabIndex = 3;
            this.lblMaPTTT.Text = "Mã phương thức";
            // 
            // pnlMaWrap
            // 
            this.pnlMaWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMaWrap.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlMaWrap.Controls.Add(this.txtMaPTTT);
            this.pnlMaWrap.Location = new System.Drawing.Point(22, 111);
            this.pnlMaWrap.Name = "pnlMaWrap";
            this.pnlMaWrap.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlMaWrap.Size = new System.Drawing.Size(388, 36);
            this.pnlMaWrap.TabIndex = 4;
            this.pnlMaWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtMaPTTT
            // 
            this.txtMaPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaPTTT.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtMaPTTT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMaPTTT.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.txtMaPTTT.Location = new System.Drawing.Point(12, 8);
            this.txtMaPTTT.Name = "txtMaPTTT";
            this.txtMaPTTT.ReadOnly = true;
            this.txtMaPTTT.Size = new System.Drawing.Size(364, 22);
            this.txtMaPTTT.TabIndex = 0;
            // 
            // lblTenPTTT
            // 
            this.lblTenPTTT.AutoSize = true;
            this.lblTenPTTT.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblTenPTTT.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTenPTTT.Location = new System.Drawing.Point(20, 160);
            this.lblTenPTTT.Name = "lblTenPTTT";
            this.lblTenPTTT.Size = new System.Drawing.Size(166, 20);
            this.lblTenPTTT.TabIndex = 5;
            this.lblTenPTTT.Text = "Tên phương thức *";
            // 
            // pnlTenWrap
            // 
            this.pnlTenWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTenWrap.BackColor = System.Drawing.Color.White;
            this.pnlTenWrap.Controls.Add(this.txtTenPTTT);
            this.pnlTenWrap.Location = new System.Drawing.Point(22, 183);
            this.pnlTenWrap.Name = "pnlTenWrap";
            this.pnlTenWrap.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlTenWrap.Size = new System.Drawing.Size(388, 36);
            this.pnlTenWrap.TabIndex = 6;
            this.pnlTenWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtTenPTTT
            // 
            this.txtTenPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenPTTT.BackColor = System.Drawing.Color.White;
            this.txtTenPTTT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTenPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTenPTTT.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtTenPTTT.Location = new System.Drawing.Point(12, 8);
            this.txtTenPTTT.Name = "txtTenPTTT";
            this.txtTenPTTT.PlaceholderText = "Ví dụ: Tiền mặt, Chuyển khoản VietQR, Thẻ POS...";
            this.txtTenPTTT.Size = new System.Drawing.Size(364, 22);
            this.txtTenPTTT.TabIndex = 0;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMoTa.Location = new System.Drawing.Point(20, 232);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(107, 20);
            this.lblMoTa.TabIndex = 7;
            this.lblMoTa.Text = "Mô tả / Ghi chú";
            // 
            // pnlMoTaWrap
            // 
            this.pnlMoTaWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMoTaWrap.BackColor = System.Drawing.Color.White;
            this.pnlMoTaWrap.Controls.Add(this.txtMoTa);
            this.pnlMoTaWrap.Location = new System.Drawing.Point(22, 255);
            this.pnlMoTaWrap.Name = "pnlMoTaWrap";
            this.pnlMoTaWrap.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlMoTaWrap.Size = new System.Drawing.Size(388, 36);
            this.pnlMoTaWrap.TabIndex = 8;
            this.pnlMoTaWrap.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaInputBox);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoTa.BackColor = System.Drawing.Color.White;
            this.txtMoTa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMoTa.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtMoTa.Location = new System.Drawing.Point(12, 8);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "Ghi chú hướng dẫn thu ngân hoặc số tài khoản...";
            this.txtMoTa.Size = new System.Drawing.Size(364, 22);
            this.txtMoTa.TabIndex = 0;
            // 
            // chkKichHoat
            // 
            this.chkKichHoat.AutoSize = true;
            this.chkKichHoat.Checked = true;
            this.chkKichHoat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKichHoat.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.chkKichHoat.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.chkKichHoat.Location = new System.Drawing.Point(22, 308);
            this.chkKichHoat.Name = "chkKichHoat";
            this.chkKichHoat.Size = new System.Drawing.Size(199, 25);
            this.chkKichHoat.TabIndex = 9;
            this.chkKichHoat.Text = "Đang kích hoạt sử dụng";
            this.chkKichHoat.UseVisualStyleBackColor = true;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomButtons.Controls.Add(this.btnLuu);
            this.pnlBottomButtons.Controls.Add(this.btnLamMoi);
            this.pnlBottomButtons.Controls.Add(this.btnXoa);
            this.pnlBottomButtons.Location = new System.Drawing.Point(22, 348);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(388, 42);
            this.pnlBottomButtons.TabIndex = 10;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(252, 165, 165);
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnXoa.Location = new System.Drawing.Point(0, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(88, 38);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnLamMoi.Location = new System.Drawing.Point(162, 2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(96, 38);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(234, 88, 12);
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(266, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(122, 38);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu phương thức";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            this.btnLuu.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // PhuongThucThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Controls.Add(this.tblLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhuongThucThanhToan";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phương Thức Thanh Toán";
            this.Load += new System.EventHandler(this.frmPhuongThucThanhToan_Load);
            this.tblLayout.ResumeLayout(false);
            this.cardLeft.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTTT)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.pnlMaWrap.ResumeLayout(false);
            this.pnlMaWrap.PerformLayout();
            this.pnlTenWrap.ResumeLayout(false);
            this.pnlTenWrap.PerformLayout();
            this.pnlMoTaWrap.ResumeLayout(false);
            this.pnlMoTaWrap.PerformLayout();
            this.pnlBottomButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private System.Windows.Forms.Panel cardLeft;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblIconSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlGridWrapper;
        private System.Windows.Forms.DataGridView dgvPTTT;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblRightTitle;
        private System.Windows.Forms.Label lblRightSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblMaPTTT;
        private System.Windows.Forms.Panel pnlMaWrap;
        private System.Windows.Forms.TextBox txtMaPTTT;
        private System.Windows.Forms.Label lblTenPTTT;
        private System.Windows.Forms.Panel pnlTenWrap;
        private System.Windows.Forms.TextBox txtTenPTTT;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Panel pnlMoTaWrap;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.CheckBox chkKichHoat;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
    }
}