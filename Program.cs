using QuanLyKhachSan_Phan1.Forms;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Vòng lặp: Đăng nhập -> Form chính -> (Đăng xuất thì quay lại Đăng nhập).
            // Đóng cửa sổ bằng nút X thì thoát hẳn ứng dụng.
            while (true)
            {
                Session.DangXuat();

                using (var formDangNhap = new FormDangNhap())
                {
                    if (formDangNhap.ShowDialog() != DialogResult.OK)
                        return; // Đóng form đăng nhập => thoát ứng dụng
                }

                var formChinh = new FormChinh();
                Application.Run(formChinh);

                if (!formChinh.YeuCauDangXuat)
                    return; // Người dùng đóng cửa sổ chính => thoát ứng dụng
            }
        }
    }
}
