using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{

    public class SqlColumn
    {
        public string Name;
        public bool IsIdentity;
        public string ColumnType;
        public bool IsNullable;
        public int? MaxLength;
        public string MaxLengthPostFix;

        public SqlColumn() { }

        public SqlColumn(DataRow row, List<string> columnIdentities)
        {
            Name = row["COLUMN_NAME"].ToString();
            IsIdentity = columnIdentities.Any(s => s == Name);
            ColumnType = row["DATA_TYPE"].ToString();
            IsNullable = (row["IS_NULLABLE"].ToString() == "YES") ? true: false;
            MaxLength = row.Field<int?>("CHARACTER_MAXIMUM_LENGTH");
            MaxLengthPostFix = GetStringPostFix();
        }

        public string GetStringPostFix()
        {
            string postfix = "";
            if (MaxLength == null)
            {
                return postfix;
            }
            else if (MaxLength == -1)
            {
                return "(MAX)";
            }
            else
            {
                return string.Format("({0})", MaxLength.ToString());
            }
        }
    }
}
