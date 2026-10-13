using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormDanhSachTaiKhoan : Form
    {
        private DataGridView dgv;
        private Button btnThem, btnDoiTrangThai;

        public FormDanhSachTaiKhoan()
        {
            KhoiTaoGiaoDien();
            TaiDuLieu();
        }

        private void KhoiTaoGiaoDien()
        {
            BackColor = GiaoDien.MauNen;
            Font = GiaoDien.FontChuan;

            var lblTieuDe = new Label
            {
                Text = "Quản lý tài khoản",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(0, 0),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            btnThem = new Button { Text = "+ Thêm tài khoản", Location = new Point(700, 4), Size = new Size(170, 34) };
            GiaoDien.ApDungNutChinh(btnThem);
            btnThem.Click += (s, e) =>
            {
                using var f = new FormThemTaiKhoan();
                if (f.ShowDialog() == DialogResult.OK) TaiDuLieu();
            };
            Controls.Add(btnThem);

            btnDoiTrangThai = new Button { Text = "Khóa / Mở khóa", Location = new Point(0, 46), Size = new Size(160, 32) };
            GiaoDien.ApDungNutPhu(btnDoiTrangThai);
            btnDoiTrangThai.Click += BtnDoiTrangThai_Click;
            Controls.Add(btnDoiTrangThai);

            dgv = new DataGridView { Location = new Point(0, 90), Size = new Size(920, 460) };
            GiaoDien.ApDungDataGridView(dgv);
            Controls.Add(dgv);

            Resize += (s, e) =>
            {
                dgv.Size = new Size(Width - 40, Height - 120);
                btnThem.Location = new Point(Width - 210, 4);
            };
        }

        private void TaiDuLieu()
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand(
                    @"SELECT id, ten_dang_nhap, ho_ten, email, sdt, vai_tro, trang_thai, ngay_tao
                      FROM tai_khoan ORDER BY ngay_tao DESC", conn);
                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                // Cột hiển thị tiếng Việt; giữ nguyên cột mã gốc (ẩn) để xử lý logic
                dt.Columns.Add("vai_tro_hien_thi", typeof(string));
                dt.Columns.Add("trang_thai_hien_thi", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["vai_tro_hien_thi"] = GiaoDien.NhanVaiTro(row["vai_tro"].ToString());
                    row["trang_thai_hien_thi"] = GiaoDien.NhanTrangThaiTaiKhoan(row["trang_thai"].ToString());
                }

                dgv.DataSource = dt;

                dgv.Columns["id"].Visible = false;
                dgv.Columns["vai_tro"].Visible = false;
                dgv.Columns["trang_thai"].Visible = false;
                dgv.Columns["ten_dang_nhap"].HeaderText = "Tên đăng nhập";
                dgv.Columns["ho_ten"].HeaderText = "Họ tên";
                dgv.Columns["email"].HeaderText = "Email";
                dgv.Columns["sdt"].HeaderText = "SĐT";
                dgv.Columns["vai_tro_hien_thi"].HeaderText = "Vai trò";
                dgv.Columns["trang_thai_hien_thi"].HeaderText = "Trạng thái";
                dgv.Columns["ngay_tao"].HeaderText = "Ngày tạo";
                dgv.Columns["ngay_tao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgv.CellFormatting -= Dgv_CellFormatting;
                dgv.CellFormatting += Dgv_CellFormatting;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tô đỏ chữ "Đã khóa" cho dễ nhìn
        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Columns[e.ColumnIndex].Name != "trang_thai_hien_thi") return;
            string ma = dgv.Rows[e.RowIndex].Cells["trang_thai"].Value?.ToString();
            e.CellStyle.ForeColor = ma == "KhoaTaiKhoan" ? GiaoDien.MauNguyHiem : Color.FromArgb(31, 122, 61);
            e.CellStyle.Font = GiaoDien.FontDam;
        }

        private void BtnDoiTrangThai_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 tài khoản trong danh sách.", "Thông báo");
                return;
            }

            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            string tenDangNhap = dgv.CurrentRow.Cells["ten_dang_nhap"].Value.ToString();
            string trangThaiHienTai = dgv.CurrentRow.Cells["trang_thai"].Value.ToString();

            if (id == Session.Id)
            {
                MessageBox.Show("Bạn không thể tự khóa tài khoản của chính mình.", "Thông báo");
                return;
            }

            string trangThaiMoi = trangThaiHienTai == "HoatDong" ? "KhoaTaiKhoan" : "HoatDong";
            string hanhDong = trangThaiMoi == "KhoaTaiKhoan" ? "khóa" : "mở khóa";
            var xacNhan = MessageBox.Show(
                $"Xác nhận {hanhDong} tài khoản \"{tenDangNhap}\"?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("UPDATE tai_khoan SET trang_thai = @tt WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                TaiDuLieu();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
