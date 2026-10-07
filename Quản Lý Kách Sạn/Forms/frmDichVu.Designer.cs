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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblRightTitle = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblLoaiDV = new System.Windows.Forms.Label();
            this.cboLoaiDVForm = new System.Windows.Forms.ComboBox();
            this.lblGia = new System.Windows.Forms.Label();
            this.numGia = new System.Windows.Forms.NumericUpDown();
            this.lblDVT = new System.Windows.Forms.Label();
            this.cboDVT = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1164, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(364, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "QUẢN LÝ DỊCH VỤ - AN NHIÊN HOTEL";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.txtTimKiem);
            this.pnlLeft.Controls.Add(this.cboLoai);
            this.pnlLeft.Controls.Add(this.dgvDichVu);
            this.pnlLeft.Location = new System.Drawing.Point(25, 75);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(720, 580);
            this.pnlLeft.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.Location = new System.Drawing.Point(15, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm theo tên dịch vụ, mã...";
            this.txtTimKiem.Size = new System.Drawing.Size(260, 29);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Items.AddRange(new object[] {
            "Tất cả",
            "Ăn uống",
            "Giặt ủi",
            "Đưa đón",
            "Khác"});
            this.cboLoai.Location = new System.Drawing.Point(290, 15);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(160, 29);
            this.cboLoai.TabIndex = 1;
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(15, 60);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.Size = new System.Drawing.Size(690, 500);
            this.dgvDichVu.TabIndex = 2;
            this.dgvDichVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDichVu_CellClick);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.lblRightTitle);
            this.pnlRight.Controls.Add(this.lblTen);
            this.pnlRight.Controls.Add(this.txtTen);
            this.pnlRight.Controls.Add(this.lblLoaiDV);
            this.pnlRight.Controls.Add(this.cboLoaiDVForm);
            this.pnlRight.Controls.Add(this.lblGia);
            this.pnlRight.Controls.Add(this.numGia);
            this.pnlRight.Controls.Add(this.lblDVT);
            this.pnlRight.Controls.Add(this.cboDVT);
            this.pnlRight.Controls.Add(this.btnSave);
            this.pnlRight.Controls.Add(this.btnXoa);
            this.pnlRight.Controls.Add(this.btnLamMoi);
            this.pnlRight.Location = new System.Drawing.Point(765, 75);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(370, 580);
            this.pnlRight.TabIndex = 2;
            // 
            // lblRightTitle
            // 
            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRightTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRightTitle.Location = new System.Drawing.Point(20, 15);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(182, 28);
            this.lblRightTitle.TabIndex = 0;
            this.lblRightTitle.Text = "Thông tin dịch vụ";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTen.Location = new System.Drawing.Point(20, 60);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(91, 21);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Tên dịch vụ:";
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTen.Location = new System.Drawing.Point(20, 85);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(325, 29);
            this.txtTen.TabIndex = 2;
            // 
            // lblLoaiDV
            // 
            this.lblLoaiDV.AutoSize = true;
            this.lblLoaiDV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLoaiDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoaiDV.Location = new System.Drawing.Point(20, 130);
            this.lblLoaiDV.Name = "lblLoaiDV";
            this.lblLoaiDV.Size = new System.Drawing.Size(96, 21);
            this.lblLoaiDV.TabIndex = 3;
            this.lblLoaiDV.Text = "Loại dịch vụ:";
            // 
            // cboLoaiDVForm
            // 
            this.cboLoaiDVForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDVForm.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiDVForm.FormattingEnabled = true;
            this.cboLoaiDVForm.Items.AddRange(new object[] {
            "Ăn uống",
            "Giặt ủi",
            "Đưa đón",
            "Khác"});
            this.cboLoaiDVForm.Location = new System.Drawing.Point(20, 155);
            this.cboLoaiDVForm.Name = "cboLoaiDVForm";
            this.cboLoaiDVForm.Size = new System.Drawing.Size(325, 29);
            this.cboLoaiDVForm.TabIndex = 4;
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGia.Location = new System.Drawing.Point(20, 200);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(110, 21);
            this.lblGia.TabIndex = 5;
            this.lblGia.Text = "Đơn giá (VNĐ):";
            // 
            // numGia
            // 
            this.numGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numGia.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGia.Location = new System.Drawing.Point(20, 225);
            this.numGia.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGia.Name = "numGia";
            this.numGia.Size = new System.Drawing.Size(325, 29);
            this.numGia.TabIndex = 6;
            // 
            // lblDVT
            // 
            this.lblDVT.AutoSize = true;
            this.lblDVT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDVT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDVT.Location = new System.Drawing.Point(20, 270);
            this.lblDVT.Name = "lblDVT";
            this.lblDVT.Size = new System.Drawing.Size(90, 21);
            this.lblDVT.TabIndex = 7;
            this.lblDVT.Text = "Đơn vị tính:";
            // 
            // cboDVT
            // 
            this.cboDVT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDVT.FormattingEnabled = true;
            this.cboDVT.Items.AddRange(new object[] {
            "Lần",
            "Phần",
            "Chai/Lon",
            "Kg",
            "Chuyến"});
            this.cboDVT.Location = new System.Drawing.Point(20, 295);
            this.cboDVT.Name = "cboDVT";
            this.cboDVT.Size = new System.Drawing.Size(325, 29);
            this.cboDVT.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(119)))), ((int)(((byte)(26)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(325, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "LƯU DỊCH VỤ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.Location = new System.Drawing.Point(20, 415);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(155, 36);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa dịch vụ";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.Location = new System.Drawing.Point(190, 415);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(155, 36);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1164, 675);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmDichVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Dịch Vụ";
            this.Load += new System.EventHandler(this.frmDichVu_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblRightTitle;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblLoaiDV;
        private System.Windows.Forms.ComboBox cboLoaiDVForm;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGia;
        private System.Windows.Forms.Label lblDVT;
        private System.Windows.Forms.ComboBox cboDVT;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}