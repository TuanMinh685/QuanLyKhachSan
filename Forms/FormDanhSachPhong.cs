using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormDanhSachPhong : Form
    {
        private const string TatCa = "-- Tất cả --";

        private DataGridView dgv;
        private ComboBox cboLocTrangThai, cboTrangThaiMoi;
        private Button btnThem, btnSua, btnXoa, btnCapNhatTrangThai;

        // Mã trạng thái trong CSDL -> nhãn hiển thị
        private readonly Dictionary<string, string> _nhanTrangThai = new()
        {
            ["Trong"] = "Trống",
            ["DaDat"] = "Đã đặt",
            ["DangSuDung"] = "Đang sử dụng",
            ["DangDon"] = "Đang dọn",
            ["BaoTri"] = "Bảo trì",
        };

        public FormDanhSachPhong()
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
                Text = "Quản lý phòng",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(0, 0),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            if (Session.LaAdmin)
            {
                btnThem = new Button { Text = "+ Thêm phòng", Location = new Point(760, 4), Size = new Size(150, 34) };
                GiaoDien.ApDungNutChinh(btnThem);
                btnThem.Click += (s, e) =>
                {
                    using var f = new FormThemSuaPhong(null);
                    if (f.ShowDialog() == DialogResult.OK) TaiDuLieu();
                };
                Controls.Add(btnThem);
            }

            var lblLoc = new Label { Text = "Lọc theo trạng thái:", Font = GiaoDien.FontNhan, Location = new Point(0, 52), AutoSize = true };
            Controls.Add(lblLoc);

            cboLocTrangThai = new ComboBox
            {
                Location = new Point(160, 48),
                Size = new Size(180, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan
            };
            cboLocTrangThai.Items.Add(TatCa);
            foreach (var kv in _nhanTrangThai) cboLocTrangThai.Items.Add(kv.Value);
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (s, e) => TaiDuLieu();
            Controls.Add(cboLocTrangThai);

            if (Session.LaAdmin)
            {
                btnSua = new Button { Text = "Sửa", Location = new Point(360, 46), Size = new Size(90, 30) };
                GiaoDien.ApDungNutPhu(btnSua);
                btnSua.Click += BtnSua_Click;
                Controls.Add(btnSua);

                btnXoa = new Button { Text = "Xóa", Location = new Point(458, 46), Size = new Size(90, 30) };
                GiaoDien.ApDungNutNguyHiem(btnXoa);
                btnXoa.Click += BtnXoa_Click;
                Controls.Add(btnXoa);
            }

            dgv = new DataGridView { Location = new Point(0, 92), Size = new Size(920, 350) };
            GiaoDien.ApDungDataGridView(dgv);
            dgv.SelectionChanged += (s, e) => CapNhatMauChonTrangThai();
            dgv.CellFormatting += Dgv_CellFormatting;
            Controls.Add(dgv);

            // ===== Khu đổi trạng thái nhanh cho phòng đang chọn =====
            var panelDoiTrangThai = new Panel
            {
                Location = new Point(0, 452),
                Size = new Size(920, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(panelDoiTrangThai);

            var lblDoiTrangThai = new Label
            {
                Text = "Đổi trạng thái phòng đang chọn:",
                Font = GiaoDien.FontNhan,
                Location = new Point(12, 15),
                AutoSize = true
            };
            panelDoiTrangThai.Controls.Add(lblDoiTrangThai);

            cboTrangThaiMoi = new ComboBox
            {
                Location = new Point(260, 10),
                Size = new Size(180, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan
            };
            foreach (var kv in _nhanTrangThai) cboTrangThaiMoi.Items.Add(kv.Value);
            panelDoiTrangThai.Controls.Add(cboTrangThaiMoi);

            btnCapNhatTrangThai = new Button { Text = "Cập nhật", Location = new Point(452, 8), Size = new Size(110, 32) };
            GiaoDien.ApDungNutChinh(btnCapNhatTrangThai);
            btnCapNhatTrangThai.Click += BtnCapNhatTrangThai_Click;
            panelDoiTrangThai.Controls.Add(btnCapNhatTrangThai);

            Resize += (s, e) =>
            {
                dgv.Size = new Size(Width - 40, Height - 210);
                if (btnThem != null) btnThem.Location = new Point(Width - 190, 4);
                panelDoiTrangThai.Location = new Point(0, dgv.Bottom + 10);
                panelDoiTrangThai.Width = Width - 40;
            };
        }

        // Tô màu chữ trạng thái phòng theo từng loại
        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Columns[e.ColumnIndex].Name != "trang_thai_hien_thi") return;
            string ma = dgv.Rows[e.RowIndex].Cells["trang_thai"].Value?.ToString();
            if (ma == null) return;
            e.CellStyle.ForeColor = GiaoDien.MauTrangThaiPhong(ma);
            e.CellStyle.Font = GiaoDien.FontDam;
        }

        private string MaTrangThaiTuNhan(string nhan)
        {
            foreach (var kv in _nhanTrangThai)
                if (kv.Value == nhan) return kv.Key;
            return null;
        }

        private void TaiDuLieu()
        {
            try
            {
                string sql = @"SELECT p.id, p.so_phong, lp.ten_loai_phong, p.tang, lp.gia_co_ban, p.trang_thai
                               FROM phong p JOIN loai_phong lp ON lp.id = p.loai_phong_id";
                string locChon = cboLocTrangThai.SelectedItem?.ToString();
                string maTrangThaiLoc = null;

                if (locChon != null && locChon != TatCa)
                {
                    maTrangThaiLoc = MaTrangThaiTuNhan(locChon);
                    sql += " WHERE p.trang_thai = @tt";
                }
                sql += " ORDER BY p.so_phong";

                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand(sql, conn);
                if (maTrangThaiLoc != null) cmd.Parameters.AddWithValue("@tt", maTrangThaiLoc);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                // Thêm cột nhãn tiếng Việt để hiển thị (giữ cột mã gốc, ẩn đi, để xử lý logic)
                dt.Columns.Add("trang_thai_hien_thi", typeof(string));
                foreach (DataRow row in dt.Rows)
                    row["trang_thai_hien_thi"] = GiaoDien.NhanTrangThaiPhong(row["trang_thai"].ToString());

                dgv.DataSource = dt;

                dgv.Columns["id"].Visible = false;
                dgv.Columns["trang_thai"].Visible = false;
                dgv.Columns["so_phong"].HeaderText = "Số phòng";
                dgv.Columns["ten_loai_phong"].HeaderText = "Loại phòng";
                dgv.Columns["tang"].HeaderText = "Tầng";
                dgv.Columns["gia_co_ban"].HeaderText = "Giá cơ bản (VND)";
                dgv.Columns["gia_co_ban"].DefaultCellStyle.Format = "N0";
                dgv.Columns["trang_thai_hien_thi"].HeaderText = "Trạng thái";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatMauChonTrangThai()
        {
            if (dgv.CurrentRow == null || !dgv.Columns.Contains("trang_thai_hien_thi")) return;
            string trangThaiHienThi = dgv.CurrentRow.Cells["trang_thai_hien_thi"].Value?.ToString();
            if (trangThaiHienThi != null) cboTrangThaiMoi.SelectedItem = trangThaiHienThi;
        }

        private void BtnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 phòng trong danh sách.", "Thông báo");
                return;
            }
            if (cboTrangThaiMoi.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái mới.", "Thông báo");
                return;
            }

            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            string maMoi = MaTrangThaiTuNhan(cboTrangThaiMoi.SelectedItem.ToString());

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("UPDATE phong SET trang_thai = @tt WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@tt", maMoi);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                TaiDuLieu();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 phòng.", "Thông báo");
                return;
            }
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            using var f = new FormThemSuaPhong(id);
            if (f.ShowDialog() == DialogResult.OK) TaiDuLieu();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 phòng.", "Thông báo");
                return;
            }
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            string soPhong = dgv.CurrentRow.Cells["so_phong"].Value.ToString();

            var xacNhan = MessageBox.Show($"Xác nhận xóa phòng \"{soPhong}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("DELETE FROM phong WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                TaiDuLieu();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa: phòng này đang có dữ liệu liên quan.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
