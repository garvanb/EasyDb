using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Data
{
    public static class SqlDataReaderExtensions
    {
        public static int GetInt(this SqlDataReader reader, string columnName)
        {
            if (reader[columnName] == DBNull.Value) return 0;
            return (int)reader[columnName];
        }

        public static bool GetBool(this SqlDataReader reader, string columnName)
        {
            if (reader[columnName] == DBNull.Value) return false;
            return (bool)reader[columnName];
        }

        public static string GetString(this SqlDataReader reader, string columnName)
        {
            if (reader[columnName] == DBNull.Value) return "";
            return (string)reader[columnName];
        }
    }
}
