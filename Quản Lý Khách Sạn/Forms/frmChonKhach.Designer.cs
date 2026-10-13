namespace Quản_Lý_Khách_Sạn.Forms
{
    partial class frmChonKhach
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
            lblTimKhach = new Label();
            txtTimKhach = new TextBox();
            btnTimKhach = new Button();
            dgvKhach = new DataGridView();
            btnChon = new Button();
            btnDong = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvKhach)
                .BeginInit();
            SuspendLayout();

            // lblTimKhach
            lblTimKhach.AutoSize = true;
            lblTimKhach.Location = new Point(12, 14);
            lblTimKhach.Name = "lblTimKhach";
            lblTimKhach.Size = new Size(157, 15);
            lblTimKhach.TabIndex = 0;
            lblTimKhach.Text = "Tìm theo tên / SĐT / mã KH:";

            // txtTimKhach
            txtTimKhach.Location = new Point(12, 36);
            txtTimKhach.Name = "txtTimKhach";
            txtTimKhach.Size = new Size(430, 23);
            txtTimKhach.TabIndex = 1;

            // btnTimKhach
            btnTimKhach.Location = new Point(450, 34);
            btnTimKhach.Name = "btnTimKhach";
            btnTimKhach.Size = new Size(100, 28);
            btnTimKhach.TabIndex = 2;
            btnTimKhach.Text = "Tìm";
            btnTimKhach.UseVisualStyleBackColor = true;
            btnTimKhach.Click += btnTimKhach_Click;

            // dgvKhach
            dgvKhach.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhach.Location = new Point(12, 72);
            dgvKhach.Name = "dgvKhach";
            dgvKhach.Size = new Size(538, 280);
            dgvKhach.TabIndex = 3;
            dgvKhach.CellContentClick += dgvKhach_CellContentClick;

            // btnChon
            btnChon.Location = new Point(340, 364);
            btnChon.Name = "btnChon";
            btnChon.Size = new Size(100, 30);
            btnChon.TabIndex = 4;
            btnChon.Text = "Chọn";
            btnChon.UseVisualStyleBackColor = true;
            btnChon.Click += btnChon_Click;

            // btnDong
            btnDong.Location = new Point(450, 364);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(100, 30);
            btnDong.TabIndex = 5;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;

            // frmChonKhach
            AcceptButton = btnTimKhach;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnDong;
            ClientSize = new Size(564, 401);

            Controls.Add(btnDong);
            Controls.Add(btnChon);
            Controls.Add(dgvKhach);
            Controls.Add(btnTimKhach);
            Controls.Add(txtTimKhach);
            Controls.Add(lblTimKhach);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmChonKhach";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chọn khách đặt phòng";
            Load += frmChonKhach_Load;

            ((System.ComponentModel.ISupportInitialize)dgvKhach)
                .EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTimKhach;
        private TextBox txtTimKhach;
        private Button btnTimKhach;
        private DataGridView dgvKhach;
        private Button btnChon;
        private Button btnDong;
    }
}