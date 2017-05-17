using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class SqlTableGenerator : SqlCodeGenerator, ICodeGenerator
    {
        public string GenerateCode(string databaseName, Template template, SqlTable table, SqlTable originalTable)
        {
            Initialise(databaseName, template, table, originalTable);
            return SqlTableCodeGenerator(table);
        }

        private string SqlTableCodeGenerator(SqlTable table)
        {
            AddIfExists();
            AddDrop();
            AddAnsiNull();
            AddQuotedIdentifier();
            AddTableBody();

            return _code.ToString();
        }

        protected void AddTableBody()
        {
            _code.AppendWithNewLine(string.Format("CREATE TABLE [{0}](", _table.Name));
            int columnCounter = 0;
            foreach (SqlColumn column in _table.Columns)
            {
                columnCounter += 1;
                StringBuilder columnLine = new StringBuilder();
                columnLine.Append(string.Format("[{0}] [{1}]", column.Name, column.ColumnType));
                columnLine.AppendIfThere(column.MaxLengthPostFix);
                columnLine.Append(GetIdentityString(column.IsIdentity));
                columnLine.Append(GetNullString(column.IsNullable));
                _code.AddTab();
                _code.Append(columnLine);
                if (columnCounter != _table.Columns.Count)
                {
                    _code.Append(",");
                    _code.Append(Environment.NewLine);
                }
            }
            _code.AppendWithNewLine(")");
            _code.AppendWithNewLine("ON [PRIMARY]");
            _code.Append("GO");
        }
    }
}
