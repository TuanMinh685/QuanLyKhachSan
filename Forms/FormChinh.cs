using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormChinh : Form
    {
        private Panel panelNoiDung;

        /// <summary>True nếu người dùng chọn Đăng xuất (Program sẽ quay lại màn hình đăng nhập).</summary>
        public bool YeuCauDangXuat { get; private set; }

        public FormChinh()
        {
            KhoiTaoGiaoDien();
            TaiTrangChu();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Quản lý khách sạn";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Font = GiaoDien.FontChuan;
            BackColor = GiaoDien.MauNen;

            // ===== Thanh menu trên cùng =====
            var menuStrip = new MenuStrip
            {
                BackColor = GiaoDien.MauChinhDam,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(6)
            };

            var mnuTrangChu = TaoMenuItem("Trang chủ", (s, e) => TaiTrangChu());
            var mnuPhong = TaoMenuItem("Phòng", (s, e) => TaiForm(new FormDanhSachPhong()));
            var mnuCheckIn = TaoMenuItem("Check-in", (s, e) => TaiForm(new FormCheckIn()));

            menuStrip.Items.Add(mnuTrangChu);
            menuStrip.Items.Add(mnuPhong);

            if (Session.LaAdmin)
            {
                var mnuLoaiPhong = TaoMenuItem("Loại phòng", (s, e) => TaiForm(new FormDanhSachLoaiPhong()));
                var mnuTaiKhoan = TaoMenuItem("Tài khoản", (s, e) => TaiForm(new FormDanhSachTaiKhoan()));
                menuStrip.Items.Add(mnuLoaiPhong);
                menuStrip.Items.Add(mnuTaiKhoan);
            }

            menuStrip.Items.Add(mnuCheckIn);

            var mnuTaiKhoanHienTai = new ToolStripMenuItem($"{Session.HoTen} ({GiaoDien.NhanVaiTro(Session.VaiTro)})")
            {
                ForeColor = Color.White,
                Alignment = ToolStripItemAlignment.Right
            };
            var mnuDangXuat = new ToolStripMenuItem("Đăng xuất") { ForeColor = Color.Black };
            mnuDangXuat.Click += (s, e) => DangXuat();
            mnuTaiKhoanHienTai.DropDownItems.Add(mnuDangXuat);
            menuStrip.Items.Add(mnuTaiKhoanHienTai);

            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            // ===== Vùng nội dung chính =====
            panelNoiDung = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = GiaoDien.MauNen,
                Padding = new Padding(24)
            };
            Controls.Add(panelNoiDung);
            panelNoiDung.BringToFront();
        }

        private ToolStripMenuItem TaoMenuItem(string text, EventHandler onClick)
        {
            var item = new ToolStripMenuItem(text) { ForeColor = Color.White };
            item.Click += onClick;
            return item;
        }

        private void XoaNoiDung()
        {
            var cu = panelNoiDung.Controls.Cast<Control>().ToList();
            panelNoiDung.Controls.Clear();
            foreach (var c in cu) c.Dispose();
        }

        /// <summary>Hiện một Form khác bên trong panel nội dung.</summary>
        private void TaiForm(Form formCon)
        {
            XoaNoiDung();
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;
            panelNoiDung.Controls.Add(formCon);
            formCon.Show();
        }

        private void TaiTrangChu()
        {
            XoaNoiDung();

            var lblChaoMung = new Label
            {
                Text = $"Xin chào, {Session.HoTen}",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(0, 0),
                AutoSize = true
            };
            panelNoiDung.Controls.Add(lblChaoMung);

            var lblPhu = new Label
            {
                Text = "Tổng quan tình trạng phòng hiện tại",
                ForeColor = GiaoDien.MauChuMo,
                Location = new Point(0, 34),
                AutoSize = true
            };
            panelNoiDung.Controls.Add(lblPhu);

            try
            {
                var dem = new Dictionary<string, int>
                {
                    ["Trong"] = 0, ["DaDat"] = 0, ["DangSuDung"] = 0, ["DangDon"] = 0, ["BaoTri"] = 0
                };

                using (var conn = DatabaseHelper.LayKetNoi())
                using (var cmd = new SqlCommand(
                    "SELECT trang_thai, COUNT(*) AS so_luong FROM phong GROUP BY trang_thai", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dem[(string)reader["trang_thai"]] = Convert.ToInt32(reader["so_luong"]);
                    }
                }

                int x = 0;
                foreach (var kv in dem)
                {
                    var hop = TaoO(GiaoDien.NhanTrangThaiPhong(kv.Key), kv.Value.ToString(), GiaoDien.MauTrangThaiPhong(kv.Key));
                    hop.Location = new Point(x, 80);
                    panelNoiDung.Controls.Add(hop);
                    x += 170;
                }
            }
            catch (SqlException ex)
            {
                var lblLoi = new Label
                {
                    Text = "Không thể tải dữ liệu thống kê: " + ex.Message,
                    ForeColor = GiaoDien.MauNguyHiem,
                    Location = new Point(0, 90),
                    AutoSize = true
                };
                panelNoiDung.Controls.Add(lblLoi);
            }
        }

        private Panel TaoO(string nhan, string soLuong, Color mau)
        {
            var panel = new Panel
            {
                Size = new Size(155, 90),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lblSo = new Label
            {
                Text = soLuong,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = mau,
                Location = new Point(14, 12),
                AutoSize = true
            };
            var lblNhan = new Label
            {
                Text = nhan,
                ForeColor = GiaoDien.MauChuMo,
                Location = new Point(14, 56),
                AutoSize = true
            };
            panel.Controls.Add(lblSo);
            panel.Controls.Add(lblNhan);
            return panel;
        }

        private void DangXuat()
        {
            var xacNhan = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            // Đóng form chính, Program.cs sẽ thấy cờ này và mở lại màn hình đăng nhập.
            YeuCauDangXuat = true;
            Close();
        }
    }
}
