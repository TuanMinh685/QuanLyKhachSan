using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

internal static class Program
{
    private static int passed;
    private static void Check(bool condition, string name)
    {
        if (!condition) throw new Exception("FAIL: " + name);
        Console.WriteLine("PASS: " + name);
        passed++;
    }

    private static void Reject(Action action, string name)
    {
        try { action(); }
        catch (NghiepVuException) { Check(true, name); return; }
        catch (SqlException ex) when (ex.Number == 547 || ex.Number == 51000) { Check(true, name); return; }
        throw new Exception("FAIL: expected rejection: " + name);
    }

    private static object Scalar(string sql) => DatabaseHelper.ExecuteScalar(sql);
    private static int Number(string sql) => Convert.ToInt32(Scalar(sql));
    private static decimal Money(string sql) => Convert.ToDecimal(Scalar(sql));
    private static void Sql(string sql) => DatabaseHelper.ExecuteNonQuery(sql);
    private static int Id(string maDat) => Convert.ToInt32(DatabaseHelper.ExecuteScalar(
        "SELECT MaDatPhong FROM DatPhong WHERE MaDat=@Ma", new[] { new SqlParameter("@Ma", maDat) }));
    private static string Book(int kh, int phong, DateTime nhan, decimal gia, decimal coc) =>
        LeTanDatPhongBLL.TaoDatPhong(kh, phong, nhan, nhan.AddDays(1), 2, 0, coc, "TM", 1, gia);

    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            var day = DateTime.Today;
            var cp = LeTanDatPhongBLL.TinhChiPhi(500000, day, day.AddDays(1));
            Check(cp.TongTien == 550000 && cp.CocToiThieu == 165000, "Room total and minimum deposit");
            Check(LeTanDatPhongBLL.DocTienCoc("165.000") == 165000 && LeTanDatPhongBLL.DocTienCoc("165000") == 165000, "Vietnamese money input");
            foreach (string bad in new[] { "", "abc", "1.2", "165,000", "-10", "1e6" })
                Reject(() => LeTanDatPhongBLL.DocTienCoc(bad), "Reject invalid money: " + bad);
            Reject(() => LeTanDatPhongBLL.KiemTraTienCoc(164999, cp), "Reject deposit below 30 percent");
            Reject(() => LeTanDatPhongBLL.KiemTraTienCoc(550001, cp), "Reject overpayment at booking");
            Reject(() => LeTanDatPhongBLL.KiemTraTienCoc(165000.5m, cp), "Reject fractional VND");
            LeTanDatPhongBLL.KiemTraTienCoc(cp.TongTien, cp);
            Check(true, "Allow full advance payment");
            Reject(() => LeTanLuuTruBLL.KiemTraHoanCoc(165001, 165000), "Reject refund above deposit");
            Reject(() => LeTanLuuTruBLL.KiemTraHoanCoc(-1, 165000), "Reject negative refund");
            Session.MaNhanVien = 1; Session.DangXuat();
            Check(Session.MaNhanVien == null, "Logout clears cashier identity");
            if (args.Contains("--integration")) Integration();
            Console.WriteLine($"SUCCESS: {passed} checks passed.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }

    private static void Integration()
    {
        // Chỉ tạo/xóa database tạm có tên ngẫu nhiên. Không dùng database thật cho test.
        string db = "QLKS_Verify_" + Guid.NewGuid().ToString("N");
        if (!Regex.IsMatch(db, "^QLKS_Verify_[a-f0-9]{32}$")) throw new Exception("Invalid test database name");
        var configField = typeof(DatabaseHelper).GetField("connectionString", BindingFlags.Static | BindingFlags.NonPublic);
        string original = (string)configField.GetValue(null);
        var builder = new SqlConnectionStringBuilder(original) { InitialCatalog = "master", ConnectTimeout = 5 };
        using var master = new SqlConnection(builder.ConnectionString);
        master.Open();
        bool created = false;
        try
        {
            using (var create = new SqlCommand($"CREATE DATABASE [{db}]", master)) create.ExecuteNonQuery();
            created = true;
            builder.InitialCatalog = db;
            configField.SetValue(null, builder.ConnectionString);
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string schema = File.ReadAllText(Path.Combine(root, "..", "Database", "QLKS.sql"));
            // Bỏ toàn bộ phần xóa/tạo database của script gốc.
            int start = schema.IndexOf("SET QUOTED_IDENTIFIER ON;", StringComparison.Ordinal);
            if (start < 0) throw new Exception("Schema marker not found");
            foreach (string batch in Regex.Split(schema.Substring(start), @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                if (!string.IsNullOrWhiteSpace(batch)) Sql(batch);
            string migration = File.ReadAllText(Path.Combine(root, "Database", "Migrations", "001_PhieuDatCoc.sql"));
            Sql(migration); Sql(migration);
            Check(Number("SELECT COUNT(*) FROM sys.tables WHERE name='PhieuDatCoc'") == 1, "Migration is repeatable");

            var customers = new KhachHangBLL();
            var kh = customers.Them(new KhachHang { HoTen = "Khach Kiem Thu", DienThoai = "0901234567", QuocTich = "Việt Nam", SoGiayTo = "012345678901" });
            Check(kh.ThanhCong, "Create customer");
            int customer = kh.MaKhachHang;
            var loaded = customers.LayTheoMa(customer);
            loaded.HoTen = "Khach Cap Nhat";
            Check(customers.CapNhat(loaded).ThanhCong && customers.LayTheoMa(customer).HoTen == loaded.HoTen, "Update customer");
            DateTime today = Convert.ToDateTime(Scalar("SELECT CAST(GETDATE() AS date)"));

            string first = Book(customer, 1, today, 500000, 165000);
            int firstId = Id(first);
            Check(Money($"SELECT TienCoc FROM DatPhong WHERE MaDatPhong={firstId}") == 165000
                && Money($"SELECT SoTien FROM PhieuDatCoc WHERE MaDatPhong={firstId} AND LoaiGiaoDich=N'Thu'") == 165000,
                "Booking and deposit receipt saved together");
            Check(Number($"SELECT MaNhanVien FROM DatPhong WHERE MaDatPhong={firstId}") == 1, "Booking saves employee");
            Reject(() => Book(customer, 1, today, 500000, 165000), "Reject overlapping booking");
            Reject(() => LeTanDatPhongBLL.TaoDatPhong(customer, 2, today, today.AddDays(1), 9, 0, 165000, "TM", 1, 500000), "Validate capacity again at booking");
            Reject(() => LeTanDatPhongBLL.TaoDatPhong(customer, 2, today, today.AddDays(1), 2, 0, 165000, "TM", 1, 400000), "Reject stale displayed price");

            int before = Number("SELECT COUNT(*) FROM DatPhong");
            Sql("CREATE TRIGGER dbo.VerifyRejectReceipt ON dbo.PhieuDatCoc AFTER INSERT AS THROW 51000, 'Injected receipt failure', 1;");
            try { Reject(() => Book(customer, 2, today, 500000, 165000), "Receipt write failure reaches caller"); }
            finally { Sql("DROP TRIGGER dbo.VerifyRejectReceipt"); }
            Check(Number("SELECT COUNT(*) FROM DatPhong") == before, "Receipt failure rolls back booking");

            var attempts = Enumerable.Range(0, 2).Select(_ => Task.Run(() =>
            {
                try { return Book(customer, 2, today.AddDays(3), 500000, 165000); }
                catch (NghiepVuException) { return null; }
            })).ToArray();
            Task.WaitAll(attempts);
            Check(attempts.Count(t => t.Result != null) == 1, "Concurrent bookings reserve room exactly once");
            int cancelId = Id(attempts.Single(t => t.Result != null).Result);
            Reject(() => LeTanLuuTruBLL.NhanPhong(cancelId), "Reject check-in before arrival date");
            Reject(() => LeTanLuuTruBLL.HuyPhong(cancelId, "Test", 50000, 165000, "CK", 999999), "Refund employee FK failure");
            Check(Convert.ToString(Scalar($"SELECT TrangThai FROM DatPhong WHERE MaDatPhong={cancelId}")) == "Đã đặt", "Refund failure rolls back cancellation");
            LeTanLuuTruBLL.HuyPhong(cancelId, "Khách đổi lịch", 50000, 165000, "CK", 1);
            Check(Money($"SELECT SoTien FROM PhieuDatCoc WHERE MaDatPhong={cancelId} AND LoaiGiaoDich=N'Hoan'") == 50000
                && Money($"SELECT TienCoc FROM DatPhong WHERE MaDatPhong={cancelId}") == 165000, "Partial refund preserves original receipt");
            Reject(() => LeTanLuuTruBLL.HuyPhong(cancelId, "Lặp", 50000, 165000, "TM", 1), "Reject repeated cancellation/refund");
            int keepId = Id(Book(customer, 3, today.AddDays(3), 800000, 264000));
            LeTanLuuTruBLL.HuyPhong(keepId, "Giữ cọc theo thỏa thuận", 0, 264000, "TM", 1);
            Check(Number($"SELECT COUNT(*) FROM PhieuDatCoc WHERE MaDatPhong={keepId} AND LoaiGiaoDich=N'Hoan' AND SoTien=0") == 1, "Zero refund is explicitly audited");

            var noId = customers.Them(new KhachHang { HoTen = "Khach Thieu Giay To", DienThoai = "0909999999", QuocTich = "Việt Nam" });
            Check(noId.ThanhCong, "Allow customer profile without identity document before check-in");
            int noIdBooking = Id(Book(noId.MaKhachHang, 2, today, 500000, 165000));
            Reject(() => LeTanLuuTruBLL.NhanPhong(noIdBooking), "Require identity document at check-in");

            LeTanLuuTruBLL.NhanPhong(firstId);
            var history = customers.LayLichSu(customer);
            Check(history.Count == 1 && history[0].GioNhanThucTe.HasValue && !history[0].GioTraThucTe.HasValue, "Stay history includes real check-in time");
            var bills = new HoaDonBLL();
            HoaDon Bill(string code, string booking, decimal room, decimal deposit, decimal discount = 0) => new()
            { MaHoaDon = code, MaDatPhong = booking, TienPhong = room, TienDatCoc = deposit, TienGiamGia = discount, MaPTTT = "TM" };
            Reject(() => bills.ThanhToanVaTraPhong(Bill("FAIL", first, 550000, 165000), first,
                new[] { new ChiTietHoaDon { MaDichVu = "NOT_EXISTS", SoLuong = 1, DonGia = 1000 } }, 1), "Invoice detail FK failure");
            Check(Number("SELECT COUNT(*) FROM HoaDon") == 0
                && Convert.ToString(Scalar($"SELECT TrangThai FROM DatPhong WHERE MaDatPhong={firstId}")) == "Đang ở", "Invoice failure rolls back header and checkout");
            var bill = Bill("VERIFY1", first, 550000, 165000);
            bills.ThanhToanVaTraPhong(bill, first, new[] { new ChiTietHoaDon { MaDichVu = "DV001", SoLuong = 1, DonGia = 120000 } }, 1);
            Check(bill.TongTien == 505000 && Number("SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaHoaDon='VERIFY1'") == 1, "Checkout deducts deposit and saves details");
            Check(customers.LayLichSu(customer)[0].GioTraThucTe.HasValue, "Stay history includes real checkout time");
            Reject(() => bills.ThanhToanVaTraPhong(Bill("VERIFY_DUP", first, 550000, 165000), first, Array.Empty<ChiTietHoaDon>(), 1), "Reject duplicate invoice");

            string prepaid = Book(customer, 1, today, 500000, 550000);
            int prepaidId = Id(prepaid);
            LeTanLuuTruBLL.NhanPhong(prepaidId);
            LeTanLuuTruBLL.TraPhong(prepaidId);
            var prepaidBill = Bill("VERIFY2", prepaid, 550000, 550000, 50000);
            bills.ThanhToanVaTraPhong(prepaidBill, prepaid, Array.Empty<ChiTietHoaDon>(), 1);
            Check(prepaidBill.TongTien == 0 && Money($"SELECT SoTien FROM PhieuDatCoc WHERE MaDatPhong={prepaidId} AND LoaiGiaoDich=N'Hoan'") == 50000, "Refund excess advance payment at checkout");
            Check(customers.LayGiaoDichCoc(customer).Rows.Count >= 7, "Customer ledger includes receipts and refunds");
        }
        finally
        {
            configField.SetValue(null, original);
            SqlConnection.ClearAllPools();
            if (created)
            {
                using var cleanup = new SqlCommand($"ALTER DATABASE [{db}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{db}]", master);
                cleanup.ExecuteNonQuery();
                Console.WriteLine("Removed temporary database " + db);
            }
        }
    }
}
