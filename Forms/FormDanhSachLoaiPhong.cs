using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormDanhSachLoaiPhong : Form
    {
        private DataGridView dgv;
        private Button btnThem, btnSua, btnXoa;

        public FormDanhSachLoaiPhong()
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
                Text = "Quản lý loại phòng",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(0, 0),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            btnThem = new Button { Text = "+ Thêm loại phòng", Location = new Point(680, 4), Size = new Size(180, 34) };
            GiaoDien.ApDungNutChinh(btnThem);
            btnThem.Click += (s, e) =>
            {
                using var f = new FormThemSuaLoaiPhong(null);
                if (f.ShowDialog() == DialogResult.OK) TaiDuLieu();
            };
            Controls.Add(btnThem);

            btnSua = new Button { Text = "Sửa", Location = new Point(0, 46), Size = new Size(110, 32) };
            GiaoDien.ApDungNutPhu(btnSua);
            btnSua.Click += BtnSua_Click;
            Controls.Add(btnSua);

            btnXoa = new Button { Text = "Xóa", Location = new Point(118, 46), Size = new Size(110, 32) };
            GiaoDien.ApDungNutNguyHiem(btnXoa);
            btnXoa.Click += BtnXoa_Click;
            Controls.Add(btnXoa);

            dgv = new DataGridView { Location = new Point(0, 90), Size = new Size(920, 460) };
            GiaoDien.ApDungDataGridView(dgv);
            Controls.Add(dgv);

            Resize += (s, e) =>
            {
                dgv.Size = new Size(Width - 40, Height - 120);
                btnThem.Location = new Point(Width - 220, 4);
            };
        }

        private void TaiDuLieu()
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand(
                    @"SELECT id, ten_loai_phong, mo_ta, gia_co_ban, suc_chua, dien_tich
                      FROM loai_phong ORDER BY id", conn);
                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;

                dgv.Columns["id"].Visible = false;
                dgv.Columns["ten_loai_phong"].HeaderText = "Tên loại phòng";
                dgv.Columns["mo_ta"].HeaderText = "Mô tả";
                dgv.Columns["gia_co_ban"].HeaderText = "Giá cơ bản / đêm (VND)";
                dgv.Columns["gia_co_ban"].DefaultCellStyle.Format = "N0";
                dgv.Columns["suc_chua"].HeaderText = "Sức chứa (người)";
                dgv.Columns["dien_tich"].HeaderText = "Diện tích (m²)";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 loại phòng.", "Thông báo");
                return;
            }
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            using var f = new FormThemSuaLoaiPhong(id);
            if (f.ShowDialog() == DialogResult.OK) TaiDuLieu();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 loại phòng.", "Thông báo");
                return;
            }
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            string ten = dgv.CurrentRow.Cells["ten_loai_phong"].Value.ToString();

            var xacNhan = MessageBox.Show($"Xác nhận xóa loại phòng \"{ten}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("DELETE FROM loai_phong WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                TaiDuLieu();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa: vẫn còn phòng hoặc đơn đặt phòng thuộc loại này.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
