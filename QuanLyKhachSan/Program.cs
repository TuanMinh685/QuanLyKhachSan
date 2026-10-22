using Quản_Lý_Khách_Sạn.Forms;

namespace Quản_Lý_Khách_Sạn
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
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
