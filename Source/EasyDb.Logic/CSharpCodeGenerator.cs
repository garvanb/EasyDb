using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class CSharpCodeGenerator : ICodeGenerator
    {
        private string _databaseName;
        private StringBuilder _code;
        private CSharpTemplate _rules;
        private SqlTable _table;
        private Template _template;
        private SqlTable _originalTable;

        public string GenerateCode(string databaseName, Template template, SqlTable table, SqlTable originalTable)
        {
            if (template?.RulesTemplate == null || table == null) { return ""; }
            _databaseName = databaseName;
            _template = template;
            string code = "";
            _code = new StringBuilder();
            _table = table;
            _rules = (CSharpTemplate)template.RulesTemplate;
            _originalTable = originalTable;

            if (template.Name == "POCO Model")
            {
                code = GenerateCode();
            }
            else if(template.Name == "WPF View Model")
            {
                code = GenerateViewModelCode();
            }
            else
            {
                code = GenerateSqlReaderAssignments();
            }
            return code;
        }

        private string GenerateSqlReaderAssignments()
        {
            string tableName = _table.Name.LowerFirstChar();
            foreach (SqlColumn column in _table.Columns)
            {
                _code.Append("command.Parameters.AddWithValue(\"@");
                _code.Append(column.Name);
                _code.Append("\", ");
                _code.Append(tableName);
                _code.Append(".");
                _code.Append(column.Name);
                _code.Append(");");
                _code.AddLine();
            }
            return _code.ToString();
        }

        private string GenerateViewModelCode()
        {
            AddUsings();
            _code.AppendWithNewLine("namespace CodeFormatterViewModel");
            AddClassSignature();
            foreach (SqlColumn column in _table.Columns)
            {
                string type = GetCSharptype(column.ColumnType);
                AddViewModelProperty(column.Name, type);
            }
            string tablePropertyName = char.ToLowerInvariant(_table.Name[0]) + _table.Name.Substring(1);
            AddViewModelProperty(_table.Name, _table.Name);

            string localVariable = char.ToLowerInvariant(_table.Name[0]) + _table.Name.Substring(1);
            _code.AddTabs(2);
            _code.AppendWithNewLine(string.Format("public {0}VM({0} {1})", _table.Name, localVariable));
            _code.AddTabs(2);
            _code.AppendWithNewLine("{");

            _code.AddTabs(3);
            _code.AppendWithNewLine(string.Format("{0} = {1};", _table.Name, localVariable));
            _code.AddTabs(2);
            _code.AppendWithNewLines("}", 2);

            _code.AddTabs(2);
            _code.AppendWithNewLine(string.Format("public {0}VM()", _table.Name));
            _code.AddTabs(2);
            _code.AppendWithNewLine("{");

            _code.AddTabs(3);
            _code.AppendWithNewLine(string.Format("{0} = new {0}();", _table.Name));
            _code.AddTabs(2);
            _code.AppendWithNewLine("}");

            AddEmptyConstructor();
            AddEndOfClass();
            return _code.ToString();
        }

        private void AddViewModelProperty(string propertyName, string type)
        {
            string privatePropertyName = "_" + char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
            _code.AppendWithTabsAndNewLine(2, string.Format("private {0} {1};", type, privatePropertyName));
            _code.AppendWithTabsAndNewLine(2, string.Format("public {0} {1}", type, propertyName));
            _code.AppendWithTabsAndNewLine(2, "{");
            string get = string.Format("get {0} return {1}; {2}", "{", privatePropertyName, "}");
            _code.AppendWithTabsAndNewLine(3, get);
            string set = string.Format("set {0} SetValue(ref {1}, value, \"{2}\"); {3}",
                "{", privatePropertyName, propertyName, "}");
            _code.AppendWithTabsAndNewLine(3, set);
            _code.AddTabs(2);
            _code.AppendWithNewLines("}", 2);
        }

        private string GenerateCode()
        {
            AddUsings();
            _code.AppendWithNewLine("namespace CodeFormatterModel");
            AddClassSignature();

            if (_rules.IncludeProperties)
            {
                foreach (SqlColumn column in _table.Columns)
                {
                    string cSharpType = GetCSharptype(column.ColumnType);
                    string text = string.Format("public {0} {1} ", cSharpType, column.Name);
                    StringBuilder columnLine = new StringBuilder();
                    columnLine.Append(text);
                    columnLine.Append("{ get; set; }");
                    _code.AddTabs(2);
                    _code.AppendWithNewLine(columnLine.ToString());
                }
            }

            AddEmptyConstructor();
            AddEndOfClass();
            return _code.ToString();
        }

        private void AddUsings()
        {
            if (!_rules.IncludeUsings) return;
            _code.AddUsing("System;");
            _code.AddUsing("System.Collections.Generic;");
            _code.AddUsing("System.Linq;");
            _code.AddUsing("System.Text;");
            _code.AddUsing("System.Threading.Tasks;");
            _code.Append(Environment.NewLine);
        }

        private void AddClassSignature()
        {
            _code.AppendWithNewLine("{");
            _code.AppendWithNewTabbedLine(string.Format("public class {0}{1}", _table.Name, _rules.ClassNameSuffix));
            _code.AppendWithNewTabbedLine("{");
        }

        private void AddEndOfClass()
        {
            _code.AppendWithNewTabbedLine("}");
            _code.Append("}");
        }

        private void AddEmptyConstructor()
        {
            if (!_rules.IncludeEmptyConstructor) return;
            _code.AddLine();
            _code.AddTabs(2);
            _code.Append("public ");
            _code.Append(_table.Name);
            _code.Append(_rules.ClassNameSuffix);
            _code.AppendWithNewLine("() { }");
        }

        private string GetCSharptype(string SqlType)
        {
            string type = "";

            switch (SqlType)
            {
                case "int":
                    type = "int";
                    break;
                case "bit":
                    type = "bool";
                    break;
                case "nvarchar":
                case "nvarchar(50)":
                case "nvarchar(max)":
                    type = "string";
                    break;
                default:
                    type = "Unknown";
                    break;
            }

            return type;
        }

    }
}
