namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.TabControl tabBaoCao;
        private System.Windows.Forms.TabPage tabDoanhThu;
        private System.Windows.Forms.TabPage tabTopDichVu;
        private System.Windows.Forms.TabPage tabCongSuat;
        private System.Windows.Forms.DataGridView dgvDoanhThu;
        private System.Windows.Forms.DataGridView dgvTopDichVu;
        private System.Windows.Forms.DataGridView dgvCongSuat;

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.tabBaoCao = new System.Windows.Forms.TabControl();
            this.tabDoanhThu = new System.Windows.Forms.TabPage();
            this.tabTopDichVu = new System.Windows.Forms.TabPage();
            this.tabCongSuat = new System.Windows.Forms.TabPage();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.dgvTopDichVu = new System.Windows.Forms.DataGridView();
            this.dgvCongSuat = new System.Windows.Forms.DataGridView();
            this.tabBaoCao.SuspendLayout();
            this.tabDoanhThu.SuspendLayout();
            this.tabTopDichVu.SuspendLayout();
            this.tabCongSuat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongSuat)).BeginInit();
            this.SuspendLayout();

            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Text = "Thống kê & Báo cáo";

            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(20, 60);
            this.lblTu.Text = "Từ ngày:";
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Location = new System.Drawing.Point(90, 57);
            this.dtpTu.Size = new System.Drawing.Size(150, 23);

            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(260, 60);
            this.lblDen.Text = "Đến ngày:";
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Location = new System.Drawing.Point(330, 57);
            this.dtpDen.Size = new System.Drawing.Size(150, 23);

            this.btnXemBaoCao.Location = new System.Drawing.Point(500, 55);
            this.btnXemBaoCao.Size = new System.Drawing.Size(120, 27);
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = true;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            this.dgvDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoanhThu.AllowUserToAddRows = false;
            this.dgvDoanhThu.ReadOnly = true;
            this.tabDoanhThu.Controls.Add(this.dgvDoanhThu);
            this.tabDoanhThu.Text = "Doanh thu theo tháng";
            this.tabDoanhThu.UseVisualStyleBackColor = true;

            this.dgvTopDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopDichVu.AllowUserToAddRows = false;
            this.dgvTopDichVu.ReadOnly = true;
            this.tabTopDichVu.Controls.Add(this.dgvTopDichVu);
            this.tabTopDichVu.Text = "Top dịch vụ";
            this.tabTopDichVu.UseVisualStyleBackColor = true;

            this.dgvCongSuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCongSuat.AllowUserToAddRows = false;
            this.dgvCongSuat.ReadOnly = true;
            this.tabCongSuat.Controls.Add(this.dgvCongSuat);
            this.tabCongSuat.Text = "Công suất phòng";
            this.tabCongSuat.UseVisualStyleBackColor = true;

            this.tabBaoCao.Location = new System.Drawing.Point(20, 100);
            this.tabBaoCao.Size = new System.Drawing.Size(650, 400);
            this.tabBaoCao.Controls.Add(this.tabDoanhThu);
            this.tabBaoCao.Controls.Add(this.tabTopDichVu);
            this.tabBaoCao.Controls.Add(this.tabCongSuat);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 530);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblTu);
            this.Controls.Add(this.dtpTu);
            this.Controls.Add(this.lblDen);
            this.Controls.Add(this.dtpDen);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.tabBaoCao);
            this.Name = "frmBaoCao";
            this.Text = "Thống kê & Báo cáo";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.tabDoanhThu.ResumeLayout(false);
            this.tabTopDichVu.ResumeLayout(false);
            this.tabCongSuat.ResumeLayout(false);
            this.tabBaoCao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongSuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}