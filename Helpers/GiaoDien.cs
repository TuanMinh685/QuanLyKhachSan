namespace QuanLyKhachSan_Phan1.Helpers
{
    /// <summary>
    /// Màu sắc và font dùng chung, giúp giao diện các Form đồng nhất.
    /// </summary>
    public static class GiaoDien
    {
        public static readonly Color MauChinh = ColorTranslator.FromHtml("#2b5cab");
        public static readonly Color MauChinhDam = ColorTranslator.FromHtml("#1e4381");
        public static readonly Color MauNen = ColorTranslator.FromHtml("#f4f6fa");
        public static readonly Color MauVien = ColorTranslator.FromHtml("#dfe3ea");
        public static readonly Color MauNguyHiem = ColorTranslator.FromHtml("#d9455f");
        public static readonly Color MauChuMo = ColorTranslator.FromHtml("#767c88");

        public static readonly Font FontChuan = new Font("Segoe UI", 10F);
        public static readonly Font FontTieuDe = new Font("Segoe UI", 15F, FontStyle.Bold);
        public static readonly Font FontNhan = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font FontDam = new Font("Segoe UI", 10F, FontStyle.Bold);

        public static void ApDungNutChinh(Button btn)
        {
            btn.BackColor = MauChinh;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontChuan;
            btn.Cursor = Cursors.Hand;
            btn.Height = 34;
        }

        public static void ApDungNutPhu(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = ColorTranslator.FromHtml("#2c2f36");
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = MauVien;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = FontChuan;
            btn.Cursor = Cursors.Hand;
            btn.Height = 34;
        }

        public static void ApDungNutNguyHiem(Button btn)
        {
            btn.BackColor = MauNguyHiem;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontChuan;
            btn.Cursor = Cursors.Hand;
            btn.Height = 30;
        }

        public static void ApDungDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f0f3f9");
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 34;
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.Font = FontChuan;
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#eef3fb");
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 32;
            dgv.GridColor = ColorTranslator.FromHtml("#e8eaef");
        }

        public static Color MauTrangThaiPhong(string trangThai) => trangThai switch
        {
            "Trong" => ColorTranslator.FromHtml("#2fa84f"),
            "DaDat" => ColorTranslator.FromHtml("#e0a12b"),
            "DangSuDung" => MauChinh,
            "DangDon" => ColorTranslator.FromHtml("#8a6fd6"),
            "BaoTri" => MauNguyHiem,
            _ => Color.Gray
        };

        public static string NhanTrangThaiPhong(string trangThai) => trangThai switch
        {
            "Trong" => "Trống",
            "DaDat" => "Đã đặt",
            "DangSuDung" => "Đang sử dụng",
            "DangDon" => "Đang dọn",
            "BaoTri" => "Bảo trì",
            _ => trangThai
        };

        public static string NhanVaiTro(string vaiTro) => vaiTro switch
        {
            "Admin" => "Admin",
            "NhanVien" => "Nhân viên",
            _ => vaiTro
        };

        public static string NhanTrangThaiTaiKhoan(string trangThai) => trangThai switch
        {
            "HoatDong" => "Hoạt động",
            "KhoaTaiKhoan" => "Đã khóa",
            _ => trangThai
        };
    }
}
