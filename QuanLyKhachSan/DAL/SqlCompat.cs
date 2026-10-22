using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Quản_Lý_Khách_Sạn.DAL
{
    public static class DbSafeConvert
    {
        public static DateTime? ToDateTimeNullable(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            if (val is DateOnly d) return d.ToDateTime(TimeOnly.MinValue);
            if (val is DateTime dt) return dt;
            return Convert.ToDateTime(val);
        }

        public static DateTime ToDateTime(object val)
        {
            if (val == null || val == DBNull.Value) return DateTime.MinValue;
            if (val is DateOnly d) return d.ToDateTime(TimeOnly.MinValue);
            if (val is DateTime dt) return dt;
            return Convert.ToDateTime(val);
        }
    }

    public static class NpgsqlExtensions
    {
        public static NpgsqlParameter Add(this NpgsqlParameterCollection collection, string parameterName, SqlDbType sqlDbType)
        {
            var p = new NpgsqlParameter(parameterName, ToNpgsqlDbType(sqlDbType));
            collection.Add(p);
            return p;
        }

        public static NpgsqlParameter Add(this NpgsqlParameterCollection collection, string parameterName, SqlDbType sqlDbType, int size)
        {
            var p = new NpgsqlParameter(parameterName, ToNpgsqlDbType(sqlDbType), size);
            collection.Add(p);
            return p;
        }

        public static NpgsqlDbType ToNpgsqlDbType(SqlDbType sqlDbType)
        {
            return sqlDbType switch
            {
                SqlDbType.Int => NpgsqlDbType.Integer,
                SqlDbType.BigInt => NpgsqlDbType.Bigint,
                SqlDbType.SmallInt => NpgsqlDbType.Smallint,
                SqlDbType.TinyInt => NpgsqlDbType.Smallint,
                SqlDbType.Bit => NpgsqlDbType.Boolean,
                SqlDbType.Decimal => NpgsqlDbType.Numeric,
                SqlDbType.Money => NpgsqlDbType.Numeric,
                SqlDbType.Float => NpgsqlDbType.Double,
                SqlDbType.Real => NpgsqlDbType.Real,
                SqlDbType.Date => NpgsqlDbType.Date,
                SqlDbType.DateTime => NpgsqlDbType.Timestamp,
                SqlDbType.DateTime2 => NpgsqlDbType.Timestamp,
                SqlDbType.VarChar => NpgsqlDbType.Varchar,
                SqlDbType.NVarChar => NpgsqlDbType.Varchar,
                SqlDbType.Text => NpgsqlDbType.Text,
                SqlDbType.NText => NpgsqlDbType.Text,
                _ => NpgsqlDbType.Text
            };
        }
    }

    public class SqlParameter : NpgsqlParameter
    {
        public SqlParameter() : base() { }
        public SqlParameter(string name, object value) : base(name, value ?? DBNull.Value) { }
        public SqlParameter(string name, SqlDbType type) : base(name, NpgsqlExtensions.ToNpgsqlDbType(type)) { }
        public SqlParameter(string name, SqlDbType type, int size) : base(name, NpgsqlExtensions.ToNpgsqlDbType(type), size) { }
    }
}
