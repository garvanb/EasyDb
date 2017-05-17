using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class SqlTable
    {
        public string Name;
        public ObservableCollection<SqlColumn> Columns;

        public SqlTable()
        {
            Columns = new ObservableCollection<SqlColumn>();
        }

        public SqlTable(string name, ObservableCollection<SqlColumn> columns)
        {
            Name = name;
            Columns = columns;
        }

        public SqlTable(string dbName, DataRow row, DataRow[] columns, List<string> identityColumns)
        {
            Name = row["TABLE_NAME"].ToString();
         
            Columns = new ObservableCollection<SqlColumn>();
            foreach (DataRow columnRow in columns)
            {
                Columns.Add(new SqlColumn(columnRow, identityColumns));
            }
        }
    }
}
