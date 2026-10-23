namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmVietQR
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblBankTitle = new System.Windows.Forms.Label();
            this.lblBankValue = new System.Windows.Forms.Label();
            this.lblStkTitle = new System.Windows.Forms.Label();
            this.lblStkValue = new System.Windows.Forms.Label();
            this.lblCtkTitle = new System.Windows.Forms.Label();
            this.lblCtkValue = new System.Windows.Forms.Label();
            this.lblTienTitle = new System.Windows.Forms.Label();
            this.lblTienValue = new System.Windows.Forms.Label();
            this.lblMemoTitle = new System.Windows.Forms.Label();
            this.lblMemoValue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlHeader.Size = new System.Drawing.Size(460, 75);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(250, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "⚡ CỔNG THANH TOÁN VNPAY-QR (EMVCo)";
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(20, 42);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(306, 15);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Chuẩn quốc tế EMVCo • Quét bằng 40+ app Ngân hàng, Ví điện tử & Thẻ quốc tế";
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlContainer.Controls.Add(this.picQR);
            this.pnlContainer.Controls.Add(this.lblStatus);
            this.pnlContainer.Controls.Add(this.pnlInfo);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 75);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContainer.Size = new System.Drawing.Size(460, 470);
            this.pnlContainer.TabIndex = 1;
            // 
            // picQR
            // 
            this.picQR.BackColor = System.Drawing.Color.White;
            this.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQR.Location = new System.Drawing.Point(100, 15);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(260, 260);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 280);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(420, 18);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Đang tải mã VietQR tự động...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.Controls.Add(this.lblBankTitle);
            this.pnlInfo.Controls.Add(this.lblBankValue);
            this.pnlInfo.Controls.Add(this.lblStkTitle);
            this.pnlInfo.Controls.Add(this.lblStkValue);
            this.pnlInfo.Controls.Add(this.lblCtkTitle);
            this.pnlInfo.Controls.Add(this.lblCtkValue);
            this.pnlInfo.Controls.Add(this.lblTienTitle);
            this.pnlInfo.Controls.Add(this.lblTienValue);
            this.pnlInfo.Controls.Add(this.lblMemoTitle);
            this.pnlInfo.Controls.Add(this.lblMemoValue);
            this.pnlInfo.Location = new System.Drawing.Point(20, 305);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(12);
            this.pnlInfo.Size = new System.Drawing.Size(420, 150);
            this.pnlInfo.TabIndex = 2;
            this.pnlInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawInfoCard);
            // 
            // lblBankTitle
            // 
            this.lblBankTitle.AutoSize = true;
            this.lblBankTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBankTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBankTitle.Location = new System.Drawing.Point(12, 10);
            this.lblBankTitle.Name = "lblBankTitle";
            this.lblBankTitle.Size = new System.Drawing.Size(68, 15);
            this.lblBankTitle.TabIndex = 0;
            this.lblBankTitle.Text = "Ngân hàng:";
            // 
            // lblBankValue
            // 
            this.lblBankValue.AutoSize = true;
            this.lblBankValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.lblBankValue.Location = new System.Drawing.Point(150, 10);
            this.lblBankValue.Name = "lblBankValue";
            this.lblBankValue.Size = new System.Drawing.Size(161, 15);
            this.lblBankValue.TabIndex = 1;
            this.lblBankValue.Text = "MB Bank (Ngân hàng Quân Đội)";
            // 
            // lblStkTitle
            // 
            this.lblStkTitle.AutoSize = true;
            this.lblStkTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStkTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStkTitle.Location = new System.Drawing.Point(12, 36);
            this.lblStkTitle.Name = "lblStkTitle";
            this.lblStkTitle.Size = new System.Drawing.Size(75, 15);
            this.lblStkTitle.TabIndex = 2;
            this.lblStkTitle.Text = "Số tài khoản:";
            // 
            // lblStkValue
            // 
            this.lblStkValue.AutoSize = true;
            this.lblStkValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStkValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblStkValue.Location = new System.Drawing.Point(150, 35);
            this.lblStkValue.Name = "lblStkValue";
            this.lblStkValue.Size = new System.Drawing.Size(85, 17);
            this.lblStkValue.TabIndex = 3;
            this.lblStkValue.Text = "0901234567";
            // 
            // lblCtkTitle
            // 
            this.lblCtkTitle.AutoSize = true;
            this.lblCtkTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCtkTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCtkTitle.Location = new System.Drawing.Point(12, 62);
            this.lblCtkTitle.Name = "lblCtkTitle";
            this.lblCtkTitle.Size = new System.Drawing.Size(83, 15);
            this.lblCtkTitle.TabIndex = 4;
            this.lblCtkTitle.Text = "Chủ tài khoản:";
            // 
            // lblCtkValue
            // 
            this.lblCtkValue.AutoSize = true;
            this.lblCtkValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCtkValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.lblCtkValue.Location = new System.Drawing.Point(150, 62);
            this.lblCtkValue.Name = "lblCtkValue";
            this.lblCtkValue.Size = new System.Drawing.Size(188, 15);
            this.lblCtkValue.TabIndex = 5;
            this.lblCtkValue.Text = "KHACH SAN LUXURY RESORT";
            // 
            // lblTienTitle
            // 
            this.lblTienTitle.AutoSize = true;
            this.lblTienTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTienTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTienTitle.Location = new System.Drawing.Point(12, 89);
            this.lblTienTitle.Name = "lblTienTitle";
            this.lblTienTitle.Size = new System.Drawing.Size(117, 15);
            this.lblTienTitle.TabIndex = 6;
            this.lblTienTitle.Text = "Số tiền thanh toán:";
            // 
            // lblTienValue
            // 
            this.lblTienValue.AutoSize = true;
            this.lblTienValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblTienValue.Location = new System.Drawing.Point(150, 86);
            this.lblTienValue.Name = "lblTienValue";
            this.lblTienValue.Size = new System.Drawing.Size(53, 20);
            this.lblTienValue.TabIndex = 7;
            this.lblTienValue.Text = "0 VND";
            // 
            // lblMemoTitle
            // 
            this.lblMemoTitle.AutoSize = true;
            this.lblMemoTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMemoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMemoTitle.Location = new System.Drawing.Point(12, 118);
            this.lblMemoTitle.Name = "lblMemoTitle";
            this.lblMemoTitle.Size = new System.Drawing.Size(127, 15);
            this.lblMemoTitle.TabIndex = 8;
            this.lblMemoTitle.Text = "Nội dung chuyển tiền:";
            // 
            // lblMemoValue
            // 
            this.lblMemoValue.AutoSize = true;
            this.lblMemoValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMemoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.lblMemoValue.Location = new System.Drawing.Point(150, 117);
            this.lblMemoValue.Name = "lblMemoValue";
            this.lblMemoValue.Size = new System.Drawing.Size(71, 17);
            this.lblMemoValue.TabIndex = 9;
            this.lblMemoValue.Text = "HD000000";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Controls.Add(this.btnXacNhan);
            this.pnlButtons.Controls.Add(this.btnDong);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 545);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlButtons.Size = new System.Drawing.Size(460, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(230, 10);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(210, 40);
            this.btnXacNhan.TabIndex = 0;
            this.btnXacNhan.Text = "✅ Đã Nhận Tiền / Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnDong.Location = new System.Drawing.Point(20, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(195, 40);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmVietQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 605);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVietQR";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cổng Thanh Toán VNPAY-QR (Chuẩn EMVCo)";
            this.Load += new System.EventHandler(this.frmVietQR_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblBankTitle;
        private System.Windows.Forms.Label lblBankValue;
        private System.Windows.Forms.Label lblStkTitle;
        private System.Windows.Forms.Label lblStkValue;
        private System.Windows.Forms.Label lblCtkTitle;
        private System.Windows.Forms.Label lblCtkValue;
        private System.Windows.Forms.Label lblTienTitle;
        private System.Windows.Forms.Label lblTienValue;
        private System.Windows.Forms.Label lblMemoTitle;
        private System.Windows.Forms.Label lblMemoValue;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnDong;
    }
}
