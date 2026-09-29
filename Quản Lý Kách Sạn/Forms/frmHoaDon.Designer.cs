namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmHoaDon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblDatPhong;
        private System.Windows.Forms.ComboBox cboDatPhong;

        private System.Windows.Forms.Label lblDichVu;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Button btnThemDichVu;
        private System.Windows.Forms.DataGridView dgvDichVuDaChon;
        private System.Windows.Forms.Button btnXoaDongDichVu;

        private System.Windows.Forms.Label lblVoucher;
        private System.Windows.Forms.TextBox txtVoucher;
        private System.Windows.Forms.Button btnApDungVoucher;
        private System.Windows.Forms.Label lblKetQuaVoucher;

        private System.Windows.Forms.Label lblPhuongThuc;
        private System.Windows.Forms.ComboBox cboPhuongThuc;

        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.Label lblGiaTriTienPhong;
        private System.Windows.Forms.Label lblTienDichVu;
        private System.Windows.Forms.Label lblGiaTriTienDichVu;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.Label lblGiaTriTienCoc;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblGiaTriGiamGia;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblGiaTriTongTien;

        private System.Windows.Forms.Button btnLapHoaDon;

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblDatPhong = new System.Windows.Forms.Label();
            this.cboDatPhong = new System.Windows.Forms.ComboBox();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemDichVu = new System.Windows.Forms.Button();
            this.dgvDichVuDaChon = new System.Windows.Forms.DataGridView();
            this.btnXoaDongDichVu = new System.Windows.Forms.Button();
            this.lblVoucher = new System.Windows.Forms.Label();
            this.txtVoucher = new System.Windows.Forms.TextBox();
            this.btnApDungVoucher = new System.Windows.Forms.Button();
            this.lblKetQuaVoucher = new System.Windows.Forms.Label();
            this.lblPhuongThuc = new System.Windows.Forms.Label();
            this.cboPhuongThuc = new System.Windows.Forms.ComboBox();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.lblGiaTriTienPhong = new System.Windows.Forms.Label();
            this.lblTienDichVu = new System.Windows.Forms.Label();
            this.lblGiaTriTienDichVu = new System.Windows.Forms.Label();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.lblGiaTriTienCoc = new System.Windows.Forms.Label();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblGiaTriGiamGia = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblGiaTriTongTien = new System.Windows.Forms.Label();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVuDaChon)).BeginInit();
            this.SuspendLayout();

            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Text = "Lập hóa đơn thanh toán";

            this.lblDatPhong.AutoSize = true;
            this.lblDatPhong.Location = new System.Drawing.Point(20, 60);
            this.lblDatPhong.Text = "Chọn lượt đặt phòng:";
            this.cboDatPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatPhong.Location = new System.Drawing.Point(160, 57);
            this.cboDatPhong.Size = new System.Drawing.Size(500, 23);
            this.cboDatPhong.SelectedIndexChanged += new System.EventHandler(this.cboDatPhong_SelectedIndexChanged);

            this.lblDichVu.AutoSize = true;
            this.lblDichVu.Location = new System.Drawing.Point(20, 100);
            this.lblDichVu.Text = "Thêm dịch vụ:";
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDichVu.Location = new System.Drawing.Point(160, 97);
            this.cboDichVu.Size = new System.Drawing.Size(280, 23);

            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(450, 100);
            this.lblSoLuong.Text = "SL:";
            this.numSoLuong.Location = new System.Drawing.Point(480, 97);
            this.numSoLuong.Size = new System.Drawing.Size(60, 23);
            this.numSoLuong.Minimum = 1;
            this.numSoLuong.Maximum = 999;
            this.numSoLuong.Value = 1;

            this.btnThemDichVu.Location = new System.Drawing.Point(560, 96);
            this.btnThemDichVu.Size = new System.Drawing.Size(100, 25);
            this.btnThemDichVu.Text = "Thêm vào hóa đơn";
            this.btnThemDichVu.UseVisualStyleBackColor = true;
            this.btnThemDichVu.Click += new System.EventHandler(this.btnThemDichVu_Click);

            this.dgvDichVuDaChon.AllowUserToAddRows = false;
            this.dgvDichVuDaChon.AllowUserToDeleteRows = false;
            this.dgvDichVuDaChon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVuDaChon.MultiSelect = false;
            this.dgvDichVuDaChon.Location = new System.Drawing.Point(20, 135);
            this.dgvDichVuDaChon.Size = new System.Drawing.Size(640, 130);

            this.btnXoaDongDichVu.Location = new System.Drawing.Point(560, 270);
            this.btnXoaDongDichVu.Size = new System.Drawing.Size(100, 25);
            this.btnXoaDongDichVu.Text = "Xóa dòng chọn";
            this.btnXoaDongDichVu.UseVisualStyleBackColor = true;
            this.btnXoaDongDichVu.Click += new System.EventHandler(this.btnXoaDongDichVu_Click);

            this.lblVoucher.AutoSize = true;
            this.lblVoucher.Location = new System.Drawing.Point(20, 310);
            this.lblVoucher.Text = "Mã voucher:";
            this.txtVoucher.Location = new System.Drawing.Point(160, 307);
            this.txtVoucher.Size = new System.Drawing.Size(150, 23);
            this.btnApDungVoucher.Location = new System.Drawing.Point(320, 305);
            this.btnApDungVoucher.Size = new System.Drawing.Size(80, 25);
            this.btnApDungVoucher.Text = "Áp dụng";
            this.btnApDungVoucher.UseVisualStyleBackColor = true;
            this.btnApDungVoucher.Click += new System.EventHandler(this.btnApDungVoucher_Click);
            this.lblKetQuaVoucher.AutoSize = true;
            this.lblKetQuaVoucher.ForeColor = System.Drawing.Color.Red;
            this.lblKetQuaVoucher.Location = new System.Drawing.Point(410, 310);
            this.lblKetQuaVoucher.Size = new System.Drawing.Size(250, 20);

            this.lblPhuongThuc.AutoSize = true;
            this.lblPhuongThuc.Location = new System.Drawing.Point(20, 345);
            this.lblPhuongThuc.Text = "Phương thức thanh toán:";
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.Location = new System.Drawing.Point(180, 342);
            this.cboPhuongThuc.Size = new System.Drawing.Size(180, 23);

            int yTong = 380;
            this.lblTienPhong.AutoSize = true;
            this.lblTienPhong.Location = new System.Drawing.Point(400, yTong);
            this.lblTienPhong.Text = "Tiền phòng:";
            this.lblGiaTriTienPhong.AutoSize = true;
            this.lblGiaTriTienPhong.Location = new System.Drawing.Point(540, yTong);
            this.lblGiaTriTienPhong.Text = "0 đ";

            this.lblTienDichVu.AutoSize = true;
            this.lblTienDichVu.Location = new System.Drawing.Point(400, yTong + 22);
            this.lblTienDichVu.Text = "Tiền dịch vụ:";
            this.lblGiaTriTienDichVu.AutoSize = true;
            this.lblGiaTriTienDichVu.Location = new System.Drawing.Point(540, yTong + 22);
            this.lblGiaTriTienDichVu.Text = "0 đ";

            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.Location = new System.Drawing.Point(400, yTong + 44);
            this.lblTienCoc.Text = "Trừ tiền cọc:";
            this.lblGiaTriTienCoc.AutoSize = true;
            this.lblGiaTriTienCoc.Location = new System.Drawing.Point(540, yTong + 44);
            this.lblGiaTriTienCoc.Text = "0 đ";

            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Location = new System.Drawing.Point(400, yTong + 66);
            this.lblGiamGia.Text = "Giảm giá:";
            this.lblGiaTriGiamGia.AutoSize = true;
            this.lblGiaTriGiamGia.Location = new System.Drawing.Point(540, yTong + 66);
            this.lblGiaTriGiamGia.Text = "0 đ";

            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(400, yTong + 95);
            this.lblTongTien.Text = "TỔNG TIỀN:";
            this.lblGiaTriTongTien.AutoSize = true;
            this.lblGiaTriTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriTongTien.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGiaTriTongTien.Location = new System.Drawing.Point(540, yTong + 95);
            this.lblGiaTriTongTien.Text = "0 đ";

            this.btnLapHoaDon.Location = new System.Drawing.Point(20, 500);
            this.btnLapHoaDon.Size = new System.Drawing.Size(160, 35);
            this.btnLapHoaDon.Text = "LẬP HÓA ĐƠN";
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.UseVisualStyleBackColor = true;
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 560);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblDatPhong);
            this.Controls.Add(this.cboDatPhong);
            this.Controls.Add(this.lblDichVu);
            this.Controls.Add(this.cboDichVu);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.btnThemDichVu);
            this.Controls.Add(this.dgvDichVuDaChon);
            this.Controls.Add(this.btnXoaDongDichVu);
            this.Controls.Add(this.lblVoucher);
            this.Controls.Add(this.txtVoucher);
            this.Controls.Add(this.btnApDungVoucher);
            this.Controls.Add(this.lblKetQuaVoucher);
            this.Controls.Add(this.lblPhuongThuc);
            this.Controls.Add(this.cboPhuongThuc);
            this.Controls.Add(this.lblTienPhong);
            this.Controls.Add(this.lblGiaTriTienPhong);
            this.Controls.Add(this.lblTienDichVu);
            this.Controls.Add(this.lblGiaTriTienDichVu);
            this.Controls.Add(this.lblTienCoc);
            this.Controls.Add(this.lblGiaTriTienCoc);
            this.Controls.Add(this.lblGiamGia);
            this.Controls.Add(this.lblGiaTriGiamGia);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblGiaTriTongTien);
            this.Controls.Add(this.btnLapHoaDon);
            this.Name = "frmHoaDon";
            this.Text = "Lập hóa đơn thanh toán";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVuDaChon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}