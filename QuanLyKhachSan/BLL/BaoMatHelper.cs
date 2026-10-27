using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.BLL
{
    public static class BaoMatHelper
    {
        private const int Iterations = 210000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        // Lưu vết số lần thử đăng nhập thất bại theo username
        // Key: Username lowercase, Value: (Số lần sai, Thời điểm khóa tới)
        private static readonly ConcurrentDictionary<string, (int LanSai, DateTime KhoaDen)> DanhSachKhoa = new();

        public const int SoLanSaiToiDa = 5;
        public const int ThoiGianKhoaGiay = 60;

        /// <summary>
        /// Băm mật khẩu theo chuẩn quốc tế PBKDF2-SHA256 kèm muối ngẫu nhiên (Salt)
        /// </summary>
        public static string BamMatKhau(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return $"PBKDF2-SHA256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Kiểm tra mật khẩu khớp và tự động nâng cấp nếu database đang lưu dạng cũ (plaintext)
        /// </summary>
        public static bool KiemTraVaNangCap(string password, string stored, string tenDangNhap, out bool daNangCap)
        {
            daNangCap = false;
            if (string.IsNullOrEmpty(stored) || string.IsNullOrEmpty(password))
                return false;

            // 1. Kiểm tra định dạng chuẩn PBKDF2-SHA256
            var parts = stored.Split('$');
            if (parts.Length == 4 && parts[0] == "PBKDF2-SHA256" && int.TryParse(parts[1], out int iters))
            {
                try
                {
                    byte[] salt = Convert.FromBase64String(parts[2]);
                    byte[] expected = Convert.FromBase64String(parts[3]);
                    byte[] actual = Rfc2898DeriveBytes.Pbkdf2(
                        password,
                        salt,
                        iters,
                        HashAlgorithmName.SHA256,
                        expected.Length);

                    return CryptographicOperations.FixedTimeEquals(actual, expected);
                }
                catch
                {
                    return false;
                }
            }

            // 2. Dữ liệu cũ dạng Plaintext -> Kiểm tra và tự động nâng cấp
            if (string.Equals(password, stored, StringComparison.Ordinal))
            {
                try
                {
                    string hashMoi = BamMatKhau(password);
                    NangCapMatKhauDatabase(tenDangNhap, hashMoi);
                    daNangCap = true;
                }
                catch { }

                return true;
            }

            return false;
        }

        private static void NangCapMatKhauDatabase(string tenDangNhap, string hashMoi)
        {
            string sql = "UPDATE TaiKhoan SET MatKhau = @pass WHERE TenDangNhap = @user";
            DatabaseHelper.ExecuteNonQuery(sql, new[] {
                new SqlParameter("@pass", hashMoi),
                new SqlParameter("@user", tenDangNhap)
            });
            GhiNhatKy(tenDangNhap, "NANG_CAP_MAT_KHAU", "Tự động mã hóa mật khẩu theo chuẩn PBKDF2-SHA256");
        }

        /// <summary>
        /// Kiểm tra chính sách độ mạnh của mật khẩu
        /// </summary>
        public static (bool HopLe, string ThongBao, int DiemDanhGia) KiemTraDoManhMatKhau(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Mật khẩu không được để trống.", 0);

            if (password.Length < 8)
                return (false, "Mật khẩu phải có tối thiểu 8 ký tự.", 1);

            int diem = 1;
            bool coChuHoa = Regex.IsMatch(password, "[A-Z]");
            bool coChuThuong = Regex.IsMatch(password, "[a-z]");
            bool coChuSo = Regex.IsMatch(password, "[0-9]");
            bool coKyTuDacBiet = Regex.IsMatch(password, @"[!@#$%^&*(),.?"":{}|<>_\\-]");

            if (coChuHoa) diem++;
            if (coChuThuong) diem++;
            if (coChuSo) diem++;
            if (coKyTuDacBiet) diem++;
            if (password.Length >= 12) diem++;

            if (!coChuHoa || !coChuThuong || !coChuSo)
            {
                return (false, "Mật khẩu cần chứa cả chữ hoa, chữ thường và chữ số để đảm bảo an toàn.", Math.Min(diem, 3));
            }

            return (true, "Mật khẩu đạt tiêu chuẩn an toàn cao.", diem);
        }

        /// <summary>
        /// Quản lý chống tấn công Brute-force (dò quét mật khẩu)
        /// </summary>
        public static (bool BiKhoa, int GiayConLai) KiemTraKhoaDangNhap(string username)
        {
            string key = (username ?? "").Trim().ToLower();
            if (DanhSachKhoa.TryGetValue(key, out var record))
            {
                if (DateTime.Now < record.KhoaDen)
                {
                    int giay = (int)(record.KhoaDen - DateTime.Now).TotalSeconds;
                    return (true, Math.Max(1, giay));
                }
                else if (record.LanSai >= SoLanSaiToiDa)
                {
                    // Đã hết thời gian phạt -> Reset
                    DanhSachKhoa.TryRemove(key, out _);
                }
            }
            return (false, 0);
        }

        public static void GhiNhanDangNhapThatBai(string username, out int soLanConLai)
        {
            string key = (username ?? "").Trim().ToLower();
            var record = DanhSachKhoa.GetOrAdd(key, _ => (0, DateTime.MinValue));

            int lanMoi = record.LanSai + 1;
            DateTime khoaDen = DateTime.MinValue;

            if (lanMoi >= SoLanSaiToiDa)
            {
                khoaDen = DateTime.Now.AddSeconds(ThoiGianKhoaGiay);
                GhiNhatKy(username, "KHOA_TAI_KHOAN_TAM_THOI", $"Đăng nhập sai {lanMoi} lần. Tạm khóa {ThoiGianKhoaGiay}s.");
            }
            else
            {
                GhiNhatKy(username, "DANG_NHAP_THAT_BAI", $"Đăng nhập sai lần thứ {lanMoi}/{SoLanSaiToiDa}");
            }

            DanhSachKhoa[key] = (lanMoi, khoaDen);
            soLanConLai = Math.Max(0, SoLanSaiToiDa - lanMoi);
        }

        public static void XoaVepKhoaDangNhap(string username)
        {
            string key = (username ?? "").Trim().ToLower();
            DanhSachKhoa.TryRemove(key, out _);
        }

        /// <summary>
        /// Ghi nhật ký bảo mật (Security Audit Trail)
        /// </summary>
        public static void GhiNhatKy(string taiKhoan, string hanhDong, string chiTiet)
        {
            try
            {
                string mayTram = Environment.MachineName;
                string thoiGian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // 1. Thử ghi vào CSDL nếu có bảng NhatKyBaoMat
                try
                {
                    string sql = @"
                        INSERT INTO NhatKyBaoMat (TaiKhoan, HanhDong, ChiTiet, MayTram, ThoiGian)
                        VALUES (@tk, @hd, @ct, @mt, CURRENT_TIMESTAMP)";
                    DatabaseHelper.ExecuteNonQuery(sql, new[] {
                        new SqlParameter("@tk", taiKhoan ?? "N/A"),
                        new SqlParameter("@hd", hanhDong ?? "ACTION"),
                        new SqlParameter("@ct", chiTiet ?? ""),
                        new SqlParameter("@mt", mayTram)
                    });
                }
                catch
                {
                    // 2. Fallback ghi vào file audit log nội bộ để không bao giờ mất nhật ký bảo mật
                    string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
                    string logFile = Path.Combine(logDir, $"Security_{DateTime.Now:yyyyMM}.log");
                    string dong = $"[{thoiGian}] [{mayTram}] [{taiKhoan}] [{hanhDong}]: {chiTiet}{Environment.NewLine}";
                    File.AppendAllText(logFile, dong, Encoding.UTF8);
                }
            }
            catch { }
        }
    }
}
