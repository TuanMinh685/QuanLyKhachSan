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
            this.cardFilter = new System.Windows.Forms.Panel();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.tblKpi = new System.Windows.Forms.TableLayoutPanel();
            this.cardKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.lblKpi1Value = new System.Windows.Forms.Label();
            this.cardKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.lblKpi2Value = new System.Windows.Forms.Label();
            this.cardKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.lblKpi3Value = new System.Windows.Forms.Label();
            this.cardTable = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.cardFilter.SuspendLayout();
            this.tblKpi.SuspendLayout();
            this.cardKpi1.SuspendLayout();
            this.cardKpi2.SuspendLayout();
            this.cardKpi3.SuspendLayout();
            this.cardTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // cardFilter
            // 
            this.cardFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardFilter.BackColor = System.Drawing.Color.White;
            this.cardFilter.Controls.Add(this.btnLoc);
            this.cardFilter.Controls.Add(this.dtpDenNgay);
            this.cardFilter.Controls.Add(this.lblDen);
            this.cardFilter.Controls.Add(this.dtpTuNgay);
            this.cardFilter.Controls.Add(this.lblTu);
            this.cardFilter.Location = new System.Drawing.Point(24, 20);
            this.cardFilter.Name = "cardFilter";
            this.cardFilter.Size = new System.Drawing.Size(1150, 64);
            this.cardFilter.TabIndex = 0;
            this.cardFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTu.Location = new System.Drawing.Point(20, 20);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(68, 21);
            this.lblTu.TabIndex = 0;
            this.lblTu.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(95, 17);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(150, 29);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDen.Location = new System.Drawing.Point(270, 20);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(79, 21);
            this.lblDen.TabIndex = 2;
            this.lblDen.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(355, 17);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(150, 29);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatAppearance.BorderSize = 0;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(530, 14);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(130, 36);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "LỌC DỮ LIỆU";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // tblKpi
            // 
            this.tblKpi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblKpi.ColumnCount = 3;
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblKpi.Controls.Add(this.cardKpi3, 2, 0);
            this.tblKpi.Controls.Add(this.cardKpi2, 1, 0);
            this.tblKpi.Controls.Add(this.cardKpi1, 0, 0);
            this.tblKpi.Location = new System.Drawing.Point(24, 100);
            this.tblKpi.Name = "tblKpi";
            this.tblKpi.RowCount = 1;
            this.tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblKpi.Size = new System.Drawing.Size(1150, 105);
            this.tblKpi.TabIndex = 1;
            // 
            // cardKpi1
            // 
            this.cardKpi1.BackColor = System.Drawing.Color.White;
            this.cardKpi1.Controls.Add(this.lblKpi1Value);
            this.cardKpi1.Controls.Add(this.lblKpi1Title);
            this.cardKpi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi1.Location = new System.Drawing.Point(0, 0);
            this.cardKpi1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardKpi1.Name = "cardKpi1";
            this.cardKpi1.Size = new System.Drawing.Size(373, 105);
            this.cardKpi1.TabIndex = 0;
            this.cardKpi1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblKpi1Title
            // 
            this.lblKpi1Title.AutoSize = true;
            this.lblKpi1Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi1Title.Location = new System.Drawing.Point(20, 18);
            this.lblKpi1Title.Name = "lblKpi1Title";
            this.lblKpi1Title.Size = new System.Drawing.Size(155, 20);
            this.lblKpi1Title.TabIndex = 0;
            this.lblKpi1Title.Text = "TỔNG DOANH THU";
            // 
            // lblKpi1Value
            // 
            this.lblKpi1Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKpi1Value.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.lblKpi1Value.Location = new System.Drawing.Point(18, 45);
            this.lblKpi1Value.Name = "lblKpi1Value";
            this.lblKpi1Value.Size = new System.Drawing.Size(340, 42);
            this.lblKpi1Value.TabIndex = 1;
            this.lblKpi1Value.Text = "0 đ";
            // 
            // cardKpi2
            // 
            this.cardKpi2.BackColor = System.Drawing.Color.White;
            this.cardKpi2.Controls.Add(this.lblKpi2Value);
            this.cardKpi2.Controls.Add(this.lblKpi2Title);
            this.cardKpi2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi2.Location = new System.Drawing.Point(388, 0);
            this.cardKpi2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardKpi2.Name = "cardKpi2";
            this.cardKpi2.Size = new System.Drawing.Size(373, 105);
            this.cardKpi2.TabIndex = 1;
            this.cardKpi2.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblKpi2Title
            // 
            this.lblKpi2Title.AutoSize = true;
            this.lblKpi2Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi2Title.Location = new System.Drawing.Point(20, 18);
            this.lblKpi2Title.Name = "lblKpi2Title";
            this.lblKpi2Title.Size = new System.Drawing.Size(161, 20);
            this.lblKpi2Title.TabIndex = 0;
            this.lblKpi2Title.Text = "DOANH THU PHÒNG";
            // 
            // lblKpi2Value
            // 
            this.lblKpi2Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKpi2Value.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblKpi2Value.Location = new System.Drawing.Point(18, 45);
            this.lblKpi2Value.Name = "lblKpi2Value";
            this.lblKpi2Value.Size = new System.Drawing.Size(340, 42);
            this.lblKpi2Value.TabIndex = 1;
            this.lblKpi2Value.Text = "0 đ";
            // 
            // cardKpi3
            // 
            this.cardKpi3.BackColor = System.Drawing.Color.White;
            this.cardKpi3.Controls.Add(this.lblKpi3Value);
            this.cardKpi3.Controls.Add(this.lblKpi3Title);
            this.cardKpi3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKpi3.Location = new System.Drawing.Point(776, 0);
            this.cardKpi3.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cardKpi3.Name = "cardKpi3";
            this.cardKpi3.Size = new System.Drawing.Size(374, 105);
            this.cardKpi3.TabIndex = 2;
            this.cardKpi3.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblKpi3Title
            // 
            this.lblKpi3Title.AutoSize = true;
            this.lblKpi3Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKpi3Title.Location = new System.Drawing.Point(20, 18);
            this.lblKpi3Title.Name = "lblKpi3Title";
            this.lblKpi3Title.Size = new System.Drawing.Size(167, 20);
            this.lblKpi3Title.TabIndex = 0;
            this.lblKpi3Title.Text = "DOANH THU DỊCH VỤ";
            // 
            // lblKpi3Value
            // 
            this.lblKpi3Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKpi3Value.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.lblKpi3Value.Location = new System.Drawing.Point(18, 45);
            this.lblKpi3Value.Name = "lblKpi3Value";
            this.lblKpi3Value.Size = new System.Drawing.Size(340, 42);
            this.lblKpi3Value.TabIndex = 1;
            this.lblKpi3Value.Text = "0 đ";
            // 
            // cardTable
            // 
            this.cardTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardTable.BackColor = System.Drawing.Color.White;
            this.cardTable.Controls.Add(this.lblTableTitle);
            this.cardTable.Controls.Add(this.dgvBaoCao);
            this.cardTable.Location = new System.Drawing.Point(24, 225);
            this.cardTable.Name = "cardTable";
            this.cardTable.Padding = new System.Windows.Forms.Padding(16);
            this.cardTable.Size = new System.Drawing.Size(1150, 460);
            this.cardTable.TabIndex = 2;
            this.cardTable.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTableTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(268, 25);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "Chi tiết số liệu theo thời gian";
            // 
            // dgvBaoCao
            // 
            this.dgvBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao.Location = new System.Drawing.Point(16, 52);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.RowHeadersWidth = 51;
            this.dgvBaoCao.Size = new System.Drawing.Size(1118, 390);
            this.dgvBaoCao.TabIndex = 1;
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 710);
            this.Controls.Add(this.cardTable);
            this.Controls.Add(this.tblKpi);
            this.Controls.Add(this.cardFilter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo Cáo Doanh Thu";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.cardFilter.ResumeLayout(false);
            this.cardFilter.PerformLayout();
            this.tblKpi.ResumeLayout(false);
            this.cardKpi1.ResumeLayout(false);
            this.cardKpi1.PerformLayout();
            this.cardKpi2.ResumeLayout(false);
            this.cardKpi2.PerformLayout();
            this.cardKpi3.ResumeLayout(false);
            this.cardKpi3.PerformLayout();
            this.cardTable.ResumeLayout(false);
            this.cardTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cardFilter;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.TableLayoutPanel tblKpi;
        private System.Windows.Forms.Panel cardKpi1;
        private System.Windows.Forms.Label lblKpi1Title;
        private System.Windows.Forms.Label lblKpi1Value;
        private System.Windows.Forms.Panel cardKpi2;
        private System.Windows.Forms.Label lblKpi2Title;
        private System.Windows.Forms.Label lblKpi2Value;
        private System.Windows.Forms.Panel cardKpi3;
        private System.Windows.Forms.Label lblKpi3Title;
        private System.Windows.Forms.Label lblKpi3Value;
        private System.Windows.Forms.Panel cardTable;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvBaoCao;
    }
}