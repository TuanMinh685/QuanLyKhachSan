using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace Quản_Lý_Khách_Sạn.DAL
{
    internal class PhongDAL
    {
        public static SqlConnection GetConnection() => DatabaseHelper.GetConnection();
    }
}
