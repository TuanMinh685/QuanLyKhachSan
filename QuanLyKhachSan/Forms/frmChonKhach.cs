using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmChonKhach : Form
    {
        // Kết quả trả về cho màn hình đặt phòng.
        public int MaKH { get; private set; }
        public string HoTen { get; private set; }
        public string SoDienThoai { get; private set; }
        public string MaKHHienThi { get; private set; }

        private Button btnThemKhachTop;
        private Button btnThemKhachBottom;
        private Label lblThongBaoTrong;

        public frmChonKhach()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDienBoSung();
        }

        private void KhoiTaoGiaoDienBoSung()
        {
            Text = "Chọn hoặc Thêm mới khách đặt phòng";
            Size = new Size(640, 490);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            AcceptButton = btnTimKhach;
            CancelButton = btnDong;

            // 1. Thanh tìm kiếm trên cùng
            lblTimKhach.Text = "Tìm theo tên, SĐT hoặc mã KH:";
            lblTimKhach.Location = new Point(14, 12);
            lblTimKhach.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTimKhach.ForeColor = UITheme.DarkNavy;

            txtTimKhach.Location = new Point(14, 34);
            txtTimKhach.Size = new Size(310, 30);
            txtTimKhach.Font = new Font("Segoe UI", 9.5F);
            txtTimKhach.PlaceholderText = "Nhập tên, số điện thoại, CMND/CCCD...";

            btnTimKhach.Text = "🔍 Tìm";
            btnTimKhach.Location = new Point(332, 33);
            btnTimKhach.Size = new Size(80, 32);
            btnTimKhach.BackColor = Color.FromArgb(241, 245, 249);
            btnTimKhach.ForeColor = UITheme.DarkNavy;
            btnTimKhach.FlatStyle = FlatStyle.Flat;
            btnTimKhach.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnTimKhach.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTimKhach.Cursor = Cursors.Hand;

            // NÚT "➕ THÊM KHÁCH MỚI" Ở TRÊN CÙNG (CỰC KỲ DỄ THẤY)
            btnThemKhachTop = new Button
            {
                Text = "➕ Thêm khách mới",
                Location = new Point(420, 33),
                Size = new Size(190, 32),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemKhachTop.FlatAppearance.BorderSize = 0;
            btnThemKhachTop.Click += (_, __) => MoModalThemKhachMoi();
            Controls.Add(btnThemKhachTop);

            // 2. Bảng dữ liệu dgvKhach
            dgvKhach.Location = new Point(14, 76);
            dgvKhach.Size = new Size(596, 315);
            dgvKhach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UITheme.FormatDataGridView(dgvKhach);
            dgvKhach.CellDoubleClick += dgvKhach_CellDoubleClick;

            // Nhãn gợi ý khi không có kết quả tìm kiếm
            lblThongBaoTrong = new Label
            {
                Text = "Không tìm thấy khách hàng trùng khớp. Bấm '➕ Thêm khách mới' để tạo hồ sơ khách.",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(14, 200),
                Size = new Size(596, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            Controls.Add(lblThongBaoTrong);
            lblThongBaoTrong.BringToFront();

            // 3. Thanh nút bấm dưới đáy
            btnThemKhachBottom = new Button
            {
                Text = "➕ Thêm khách mới",
                Location = new Point(14, 404),
                Size = new Size(175, 34),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnThemKhachBottom.FlatAppearance.BorderSize = 0;
            btnThemKhachBottom.Click += (_, __) => MoModalThemKhachMoi();
            Controls.Add(btnThemKhachBottom);

            btnChon.Text = "✓ Chọn khách";
            btnChon.Location = new Point(400, 404);
            btnChon.Size = new Size(110, 34);
            btnChon.BackColor = UITheme.PrimaryBlue;
            btnChon.ForeColor = Color.White;
            btnChon.FlatStyle = FlatStyle.Flat;
            btnChon.FlatAppearance.BorderSize = 0;
            btnChon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChon.Cursor = Cursors.Hand;
            btnChon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            btnDong.Text = "✕ Đóng";
            btnDong.Location = new Point(518, 404);
            btnDong.Size = new Size(92, 34);
            btnDong.BackColor = Color.White;
            btnDong.ForeColor = UITheme.TextMuted;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnDong.Font = new Font("Segoe UI", 9F);
            btnDong.Cursor = Cursors.Hand;
            btnDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            btnThemKhachTop.BringToFront();
            btnThemKhachBottom.BringToFront();
            btnChon.BringToFront();
            btnDong.BringToFront();
        }

        private void frmChonKhach_Load(object sender, EventArgs e)
        {
            CauHinhLuoi();
            TimKhach();
        }

        private void CauHinhLuoi()
        {
            dgvKhach.Columns.Clear();
            dgvKhach.AutoGenerateColumns = false;
            dgvKhach.ReadOnly = true;
            dgvKhach.AllowUserToAddRows = false;
            dgvKhach.AllowUserToDeleteRows = false;
            dgvKhach.RowHeadersVisible = false;
            dgvKhach.MultiSelect = false;
            dgvKhach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhach.BackgroundColor = Color.White;
            dgvKhach.BorderStyle = BorderStyle.None;

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKHHienThi",
                HeaderText = "MÃ KH",
                FillWeight = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = UITheme.DarkNavy
                }
            });

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "HỌ VÀ TÊN KHÁCH HÀNG",
                FillWeight = 180,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                }
            });

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoDienThoai",
                HeaderText = "SỐ ĐIỆN THOẠI",
                FillWeight = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        private DataTable LayDanhSachKhach(string tuKhoa)
        {
            return LeTanDatPhongBLL.TimKhach(tuKhoa);
        }

        private void TimKhach()
        {
            try
            {
                var dt = LayDanhSachKhach(txtTimKhach.Text.Trim());
                dgvKhach.DataSource = dt;

                bool coDuLieu = dt != null && dt.Rows.Count > 0;
                lblThongBaoTrong.Visible = !coDuLieu;
                btnChon.Enabled = coDuLieu;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách khách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKhach_Click(object sender, EventArgs e)
        {
            TimKhach();
        }

        private void MoModalThemKhachMoi()
        {
            using var modal = new frmThemKhachNhanhModal();
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                MaKH = modal.MaKH;
                HoTen = modal.HoTen;
                SoDienThoai = modal.SoDienThoai;
                MaKHHienThi = modal.MaKHHienThi;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonKhachDangChon();
        }

        private void dgvKhach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ChonKhachDangChon();
            }
        }

        private void ChonKhachDangChon()
        {
            if (dgvKhach.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một khách trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var r = (DataRowView)dgvKhach.CurrentRow.DataBoundItem;

            MaKH = Convert.ToInt32(r["MaKH"]);
            HoTen = r["HoTen"].ToString();
            SoDienThoai = r["SoDienThoai"].ToString();
            MaKHHienThi = r["MaKHHienThi"].ToString();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dgvKhach_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
