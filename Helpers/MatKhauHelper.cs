using System.Security.Cryptography;

namespace QuanLyKhachSan_Phan1.Helpers
{
    /// <summary>
    /// Bam mat khau bang PBKDF2 (khong luu mat khau dang chu thuong).
    /// Chuoi luu vao CSDL co dang: "salt_base64:hash_base64".
    /// </summary>
    public static class MatKhauHelper
    {
        private const int SoVongLap = 100_000;
        private const int DoDaiHash = 32;

        public static string Bam(string matKhau)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(matKhau, salt, SoVongLap, HashAlgorithmName.SHA256, DoDaiHash);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public static bool KiemTra(string matKhau, string chuoiDaLuu)
        {
            var phan = chuoiDaLuu.Split(':');
            if (phan.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(phan[0]);
            byte[] hashDaLuu = Convert.FromBase64String(phan[1]);
            byte[] hashMoi = Rfc2898DeriveBytes.Pbkdf2(matKhau, salt, SoVongLap, HashAlgorithmName.SHA256, DoDaiHash);

            return CryptographicOperations.FixedTimeEquals(hashDaLuu, hashMoi);
        }
    }
}
