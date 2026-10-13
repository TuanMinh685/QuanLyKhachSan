using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class PhuongThucThanhToanModel
    {
        public int MaPhuongThuc { get; set; }
        public string TenPhuongThuc { get; set; }
        public string MoTa { get; set; }
        public bool KichHoat { get; set; }
    }

    public class PhuongThucThanhToanDAL
    {
        public List<PhuongThucThanhToanModel> GetAll()
        {
            List<PhuongThucThanhToanModel> list = new List<PhuongThucThanhToanModel>();
            string sql = "SELECT MaPhuongThuc, TenPhuongThuc, ISNULL(MoTa, '') AS MoTa, ISNULL(KichHoat, 1) AS KichHoat FROM PhuongThucThanhToan";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new PhuongThucThanhToanModel
                {
                    MaPhuongThuc = Convert.ToInt32(r["MaPhuongThuc"]),
                    TenPhuongThuc = r["TenPhuongThuc"].ToString(),
                    MoTa = r["MoTa"].ToString(),
                    KichHoat = Convert.ToBoolean(r["KichHoat"])
                });
            }
            return list;
        }

        public bool Insert(string ten, string moTa, bool kichHoat)
        {
            string sql = "INSERT INTO PhuongThucThanhToan (TenPhuongThuc, MoTa, KichHoat) VALUES (@Ten, @MoTa, @KichHoat)";
            SqlParameter[] pars = {
                new SqlParameter("@Ten", ten),
                new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                new SqlParameter("@KichHoat", kichHoat)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, pars) > 0;
        }

        public bool Update(int ma, string ten, string moTa, bool kichHoat)
        {
            string sql = "UPDATE PhuongThucThanhToan SET TenPhuongThuc = @Ten, MoTa = @MoTa, KichHoat = @KichHoat WHERE MaPhuongThuc = @Ma";
            SqlParameter[] pars = {
                new SqlParameter("@Ma", ma),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                new SqlParameter("@KichHoat", kichHoat)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, pars) > 0;
        }

        public bool Delete(int ma)
        {
            string sql = "DELETE FROM PhuongThucThanhToan WHERE MaPhuongThuc = @Ma";
            SqlParameter[] pars = { new SqlParameter("@Ma", ma) };
            return DatabaseHelper.ExecuteNonQuery(sql, pars) > 0;
        }
    }
}