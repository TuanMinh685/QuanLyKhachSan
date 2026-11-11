using Quản_Lý_Khách_Sạn.Forms;

namespace Quản_Lý_Khách_Sạn
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Xử lý ngoại lệ toàn cục đảm bảo app luôn hiển thị thông báo thân thiện, không văng ứng dụng đột ngột
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"Hệ thống ghi nhận thông báo:\n{e.Exception.Message}",
                    "Khách Sạn An Nhiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"Lỗi hệ thống:\n{ex.Message}",
                        "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            while (true)
            {
                using (var dangNhap = new frmDangNhap())
                {
                    if (dangNhap.ShowDialog() != DialogResult.OK)
                        break;
                }

                using (var trangChu = new frmTrangChu())
                {
                    Application.Run(trangChu);
                }
            }
        }
    }
}
