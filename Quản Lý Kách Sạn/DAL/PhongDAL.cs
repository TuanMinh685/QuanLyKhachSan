using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace Quản_Lý_Khách_Sạn.DAL
{
    internal class PhongDAL
    {
        public static readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB; Database=QuanLyKhachSan; Integrated Security=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
