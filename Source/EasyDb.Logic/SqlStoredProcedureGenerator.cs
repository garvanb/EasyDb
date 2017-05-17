using EasyDb.Data;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class SqlStoredProcedureGenerator : SqlCodeGenerator, ICodeGenerator
    {
        public string GenerateCode(string databaseName, Template template, SqlTable table, SqlTable originalTable)
        {
            Initialise(databaseName, template, table, originalTable);
            return GenerateStoredProcedure();
        }

        private string GenerateStoredProcedure()
        {
            if (_rules.StatementType == SqlStatementType.Select)
            {
                BuildStart();
                BuildMiddle();
                AddSelectBody();
            }
            else if(_rules.StatementType == SqlStatementType.Insert)
            {
                if (_rules.UseParametersToGenerateData)
                {
                    AddInsertBodyData();
                }
                else
                {
                    BuildStart();
                    AddParameters(false);
                    BuildMiddle();
                    AddInsertBody();
                }
            }
            else if (_rules.StatementType == SqlStatementType.Update)
            {
                BuildStart();
                AddParameters(true);
                BuildMiddle();
                AddUpdateBody();
                AddWhere();
            }
            else if (_rules.StatementType == SqlStatementType.Delete)
            {
                BuildStart();
                BuildMiddle();
                AddDeleteBody();
            }
            BuildEnd();

            return _code.ToString();
        }

        private void AddInsertBodyData()
        {
            string prefix = GetPrefix(0);
            string columnClause = GetColumnClause();
            
            DatabaseInteractor db = new DatabaseInteractor(_databaseName);
            DataTable data = db.GetTableData(_table.Name, columnClause);
            foreach(DataRow row in data.Rows)
            {
                _code.AppendWithNewLine(prefix);
                _code.AppendWithNewLine("VALUES");
                _code.AppendWithNewLine("(");
                int dataColumnCounter = 0;
                foreach(DataColumn column in data.Columns)
                {
                    _code.AddTab();
                    var t = FormatColumnValue(row[dataColumnCounter].ToString(), dataColumnCounter);
                    _code.Append(t);
                    if (dataColumnCounter != data.Columns.Count - 1) { _code.Append(", "); }
                    _code.AddLine();
                    dataColumnCounter += 1;
                }
                _code.AppendWithNewLines(")", 2);
            }
        }

        /// <summary>
        /// Formats the column based on the column type
        /// Identity column is ignored
        /// </summary>
        /// <param name="value">Value in the column</param>
        /// <param name="index">Index of column</param>
        /// <returns></returns>
        private string FormatColumnValue(string value, int index)
        {
            List<SqlColumn> columns = _table.Columns.Where(c => c.IsIdentity == false).ToList();
            string type = columns[index].ColumnType;

            if (type == "bit")
            {
                value = (value == "True") ? "1" : "0";
            }
            else if (type != "int")
            {
                value = "'" + value + "'";
            }
            return value;
        }

        /// <summary>
        /// Gets a list of columns to select in a SQL query
        /// excluding the Identity column
        /// </summary>
        /// <returns></returns>
        private string GetColumnClause()
        {
            StringBuilder columnClause = new StringBuilder();
            var columns = _originalTable.Columns.Where(c => c.IsIdentity == false);
            int columnCount = columns.Count();
            int columnCounter = 0;
            foreach (SqlColumn column in columns)
            {
                columnCounter += 1;
                columnClause.AppendWithAngleBrackets(column.Name);
                if (columnCounter != columnCount) { columnClause.Append(","); }
            }
            return columnClause.ToString();
        }

        private string GetPrefix(int tabs)
        {
            StringBuilder prefix = new StringBuilder();
            prefix.AddTabs(tabs);
            prefix.Append("INSERT INTO [");
            prefix.Append(_table.Name);
            prefix.AppendWithNewLine("]");
            prefix.AddTabs(tabs);
            prefix.AppendWithNewLine("(");

            var columns = _table.Columns.Where(c => c.IsIdentity == false);
            int columnCount = columns.Count();
            int columnCounter = 0;
            foreach (SqlColumn column in columns)
            {
                columnCounter += 1;
                prefix.AddTabs(tabs + 1);
                prefix.AppendWithAngleBrackets(column.Name);
                if (columnCounter != columnCount) { prefix.Append(","); }
                prefix.AddLine();
            }
            prefix.AddTabs(tabs);
            prefix.Append(")");

            return prefix.ToString();
        }

        private void BuildStart()
        {
            AddIfExists();
            AddDrop();
            AddAnsiNull();
            AddQuotedIdentifier();
            _code.Append(string.Format("CREATE PROCEDURE [{0}{1}]", _rules.PrefixNameWith, _table.Name));
        }

        private void BuildMiddle()
        {
            _code.AddLine();
            _code.AppendWithNewLine("AS");
            if (_rules.IncludeBeginEnd) { _code.AppendWithNewLines("BEGIN", 2); }
            _code.AppendWithNewTabbedLines("SET NOCOUNT ON;", 2);
        }

        private void BuildEnd()
        {
            if (_rules.IncludeBeginEnd) { _code.AppendWithNewLines("END", 2); }
            _code.AppendWithNewLine("GO");
        }

        private void AddParameters(bool includeId)
        {
            _code.AddLine();
            int columnCounter = 0;
            var columns = (includeId) ? _table.Columns : _table.Columns.Where(c => c.IsIdentity == false);
            int totalColumns = columns.Count();
            foreach (SqlColumn column in columns)
            {
                columnCounter += 1;
                StringBuilder columnLine = new StringBuilder();
                columnLine.AddTab();
                columnLine.Append("@");
                columnLine.Append(column.Name);
                columnLine.Append(" ");
                columnLine.Append(column.ColumnType);
                columnLine.AppendIfThere(column.MaxLengthPostFix);
                _code.Append(columnLine);
                if (columnCounter != totalColumns) { _code.Append(","); }
                _code.Append(Environment.NewLine);
            }
        }

        private void AddSelectBody()
        {
            _code.AppendWithNewTabbedLine("SELECT");
            int columnCounter = 0;
            foreach (SqlColumn column in _table.Columns)
            {
                columnCounter += 1;
                StringBuilder columnLine = new StringBuilder();
                columnLine.Append(column.Name);
                _code.AddTabs(2);
                _code.Append(columnLine);
                if (columnCounter != _table.Columns.Count) { _code.Append(","); }
                _code.Append(Environment.NewLine);
            }
            _code.AppendWithNewTabbedLine("FROM");
            _code.AddTabs(2);
            _code.AppendWithNewLine(string.Format("[{0}]", _table.Name));
        }

        private void AddUpdateBody()
        {
            _code.AppendWithTabsAndNewLine(1, "UPDATE");
            _code.AppendWithTabsAndNewLine(2, "[" + _table.Name + "]");
            _code.AppendWithTabsAndNewLine(1, "SET");
            var columns = _table.Columns.Where(c => c.IsIdentity == false);
            int columnCount = columns.Count();
            int columnCounter = 0;
            foreach (SqlColumn column in columns)
            {
                columnCounter += 1;
                _code.AddTabs(2);
                _code.AppendWithAngleBrackets(column.Name);
                _code.Append(" = @");
                _code.Append(column.Name);
                if (columnCounter != columnCount) { _code.Append(","); }
                _code.Append(Environment.NewLine);
            }
        }

        private void AddInsertBody()
        {
            string prefix = GetPrefix(1);
            _code.AppendWithNewLine(prefix);
            _code.AppendWithTabsAndNewLine(1, "VALUES");
            _code.AppendWithTabsAndNewLine(1, "(");

            var columns = _table.Columns.Where(c => c.IsIdentity == false);
            int totalColumns = columns.Count();
            int columnCounter = 0;
            foreach (SqlColumn column in columns)
            {
                columnCounter += 1;
                _code.AddTabs(2);
                _code.Append("@");
                _code.Append(column.Name);
                if (columnCounter != totalColumns) { _code.Append(", "); }
                _code.AddLine();
            }
            _code.AppendWithNewTabbedLine(")");
        }

        private void AddWhere()
        {
            SqlColumn idColumn = (_table.Columns.FirstOrDefault(c => c.IsIdentity));
            if (idColumn != null)
            {
                _code.AppendWithNewTabbedLine("WHERE");
                _code.AddTabs(2);
                _code.AppendWithAngleBrackets(idColumn.Name);
                _code.AppendWithNewLine(string.Format(" = @{0}", idColumn.Name));
            }
        }

        private void AddDeleteBody()
        {
            _code.AppendWithNewTabbedLine(string.Format("DELETE FROM [{0}]", _table.Name)); ;
        }
    }
}
