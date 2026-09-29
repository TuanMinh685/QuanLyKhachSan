using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public class PhuongThucThanhToanDAL
    {
        public List<PhuongThucThanhToan> GetAll()
        {
            List<PhuongThucThanhToan> list = new List<PhuongThucThanhToan>();
            string query = "SELECT MaPhuongThuc, TenPhuongThuc FROM PhuongThucThanhToan";
            using (DataTable dt = DatabaseHelper.ExecuteQuery(query))
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PhuongThucThanhToan
                    {
                        MaPhuongThuc = row["MaPhuongThuc"].ToString() ?? "",
                        TenPhuongThuc = row["TenPhuongThuc"].ToString() ?? ""
                    });
                }
            }
            return list;
        }
    }
}