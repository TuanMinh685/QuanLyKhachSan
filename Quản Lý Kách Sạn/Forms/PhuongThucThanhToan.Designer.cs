namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class PhuongThucThanhToan
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
            this.dgvPTTT = new System.Windows.Forms.DataGridView();
            this.lblLeftTitle = new System.Windows.Forms.Label();
            this.cardRight = new System.Windows.Forms.Panel();
            this.chkKichHoat = new System.Windows.Forms.CheckBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtTenPTTT = new System.Windows.Forms.TextBox();
            this.lblTenPTTT = new System.Windows.Forms.Label();
            this.txtMaPTTT = new System.Windows.Forms.TextBox();
            this.lblMaPTTT = new System.Windows.Forms.Label();
            this.lblRightTitle = new System.Windows.Forms.Label();
            this.tblLayout.SuspendLayout();
            this.cardLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTTT)).BeginInit();
            this.cardRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLayout
            // 
            this.tblLayout.ColumnCount = 2;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblLayout.Controls.Add(this.cardLeft, 0, 0);
            this.tblLayout.Controls.Add(this.cardRight, 1, 0);
            this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayout.Location = new System.Drawing.Point(20, 20);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 1;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.Size = new System.Drawing.Size(1200, 680);
            this.tblLayout.TabIndex = 0;
            // 
            // cardLeft
            // 
            this.cardLeft.BackColor = System.Drawing.Color.White;
            this.cardLeft.Controls.Add(this.dgvPTTT);
            this.cardLeft.Controls.Add(this.lblLeftTitle);
            this.cardLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLeft.Location = new System.Drawing.Point(0, 0);
            this.cardLeft.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.cardLeft.Name = "cardLeft";
            this.cardLeft.Padding = new System.Windows.Forms.Padding(16);
            this.cardLeft.Size = new System.Drawing.Size(704, 680);
            this.cardLeft.TabIndex = 0;
            this.cardLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // dgvPTTT
            // 
            this.dgvPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPTTT.BackgroundColor = System.Drawing.Color.White;
            this.dgvPTTT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPTTT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPTTT.Location = new System.Drawing.Point(16, 50);
            this.dgvPTTT.Name = "dgvPTTT";
            this.dgvPTTT.RowHeadersWidth = 51;
            this.dgvPTTT.Size = new System.Drawing.Size(672, 614);
            this.dgvPTTT.TabIndex = 1;
            this.dgvPTTT.SelectionChanged += new System.EventHandler(this.dgvPTTT_SelectionChanged);
            // 
            // lblLeftTitle
            // 
            this.lblLeftTitle.AutoSize = true;
            this.lblLeftTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblLeftTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLeftTitle.Location = new System.Drawing.Point(16, 14);
            this.lblLeftTitle.Name = "lblLeftTitle";
            this.lblLeftTitle.Size = new System.Drawing.Size(356, 28);
            this.lblLeftTitle.TabIndex = 0;
            this.lblLeftTitle.Text = "Danh Sách Phương Thức Thanh Toán";
            // 
            // cardRight
            // 
            this.cardRight.BackColor = System.Drawing.Color.White;
            this.cardRight.Controls.Add(this.chkKichHoat);
            this.cardRight.Controls.Add(this.btnLamMoi);
            this.cardRight.Controls.Add(this.btnXoa);
            this.cardRight.Controls.Add(this.btnLuu);
            this.cardRight.Controls.Add(this.txtMoTa);
            this.cardRight.Controls.Add(this.lblMoTa);
            this.cardRight.Controls.Add(this.txtTenPTTT);
            this.cardRight.Controls.Add(this.lblTenPTTT);
            this.cardRight.Controls.Add(this.txtMaPTTT);
            this.cardRight.Controls.Add(this.lblMaPTTT);
            this.cardRight.Controls.Add(this.lblRightTitle);
            this.cardRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRight.Location = new System.Drawing.Point(720, 0);
            this.cardRight.Margin = new System.Windows.Forms.Padding(0);
            this.cardRight.Name = "cardRight";
            this.cardRight.Padding = new System.Windows.Forms.Padding(20);
            this.cardRight.Size = new System.Drawing.Size(480, 680);
            this.cardRight.TabIndex = 1;
            this.cardRight.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawFigmaCard);
            // 
            // chkKichHoat
            // 
            this.chkKichHoat.AutoSize = true;
            this.chkKichHoat.Checked = true;
            this.chkKichHoat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKichHoat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkKichHoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkKichHoat.Location = new System.Drawing.Point(20, 245);
            this.chkKichHoat.Name = "chkKichHoat";
            this.chkKichHoat.Size = new System.Drawing.Size(199, 25);
            this.chkKichHoat.TabIndex = 10;
            this.chkKichHoat.Text = "Kích hoạt sử dụng ngay";
            this.chkKichHoat.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLamMoi.Location = new System.Drawing.Point(120, 622);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 38);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Location = new System.Drawing.Point(230, 622);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 38);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(340, 622);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 38);
            this.btnLuu.TabIndex = 7;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            this.btnLuu.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPillBorder);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMoTa.Location = new System.Drawing.Point(20, 190);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(440, 29);
            this.txtMoTa.TabIndex = 6;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMoTa.Location = new System.Drawing.Point(16, 165);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(107, 20);
            this.lblMoTa.TabIndex = 5;
            this.lblMoTa.Text = "Mô tả / Ghi chú";
            // 
            // txtTenPTTT
            // 
            this.txtTenPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTenPTTT.Location = new System.Drawing.Point(20, 125);
            this.txtTenPTTT.Name = "txtTenPTTT";
            this.txtTenPTTT.Size = new System.Drawing.Size(440, 29);
            this.txtTenPTTT.TabIndex = 4;
            // 
            // lblTenPTTT
            // 
            this.lblTenPTTT.AutoSize = true;
            this.lblTenPTTT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenPTTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTenPTTT.Location = new System.Drawing.Point(16, 100);
            this.lblTenPTTT.Name = "lblTenPTTT";
            this.lblTenPTTT.Size = new System.Drawing.Size(166, 20);
            this.lblTenPTTT.TabIndex = 3;
            this.lblTenPTTT.Text = "Tên phương thức (*)";
            // 
            // txtMaPTTT
            // 
            this.txtMaPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaPTTT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtMaPTTT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaPTTT.Location = new System.Drawing.Point(20, 60);
            this.txtMaPTTT.Name = "txtMaPTTT";
            this.txtMaPTTT.ReadOnly = true;
            this.txtMaPTTT.Size = new System.Drawing.Size(440, 29);
            this.txtMaPTTT.TabIndex = 2;
            // 
            // lblMaPTTT
            // 
            this.lblMaPTTT.AutoSize = true;
            this.lblMaPTTT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaPTTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaPTTT.Location = new System.Drawing.Point(16, 35);
            this.lblMaPTTT.Name = "lblMaPTTT";
            this.lblMaPTTT.Size = new System.Drawing.Size(126, 20);
            this.lblMaPTTT.TabIndex = 1;
            this.lblMaPTTT.Text = "Mã phương thức";
            // 
            // lblRightTitle
            // 
            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblRightTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblRightTitle.Location = new System.Drawing.Point(16, 5);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(262, 28);
            this.lblRightTitle.TabIndex = 0;
            this.lblRightTitle.Text = "Thông Tin Phương Thức";
            // 
            // PhuongThucThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Controls.Add(this.tblLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PhuongThucThanhToan";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phương Thức Thanh Toán";
            this.Load += new System.EventHandler(this.PhuongThucThanhToan_Load);
            this.tblLayout.ResumeLayout(false);
            this.cardLeft.ResumeLayout(false);
            this.cardLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTTT)).EndInit();
            this.cardRight.ResumeLayout(false);
            this.cardRight.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private System.Windows.Forms.Panel cardLeft;
        private System.Windows.Forms.Label lblLeftTitle;
        private System.Windows.Forms.DataGridView dgvPTTT;
        private System.Windows.Forms.Panel cardRight;
        private System.Windows.Forms.Label lblRightTitle;
        private System.Windows.Forms.TextBox txtMaPTTT;
        private System.Windows.Forms.Label lblMaPTTT;
        private System.Windows.Forms.TextBox txtTenPTTT;
        private System.Windows.Forms.Label lblTenPTTT;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.CheckBox chkKichHoat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
    }
}