using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class SqlCodeGenerator
    {
        protected string _databaseName;
        protected StringBuilder _code;
        protected SqlTemplate _rules;
        protected SqlTable _table;
        protected Template _template;
        protected SqlTable _originalTable;

        protected void Initialise(string databaseName, Template template, SqlTable table, SqlTable originalTable)
        {
            _databaseName = databaseName;
            _template = template;
            _code = new StringBuilder();
            _table = table;
            _rules = (SqlTemplate)template.RulesTemplate;
            _originalTable = originalTable;
        }

        protected void AddIfExists()
        {
            if (!_rules.IncludeIfExistsDrop) return;
            if (_rules.ObjectType == SqlObjectType.Table)
            {
                _code.Append("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'");
            }
            else if (_rules.ObjectType == SqlObjectType.StoredProcedure)
            {
                _code.Append("IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' AND Name = '");
                _code.Append(_rules.PrefixNameWith);
            }
            _code.Append(_table.Name);
            _code.Append("')");
            _code.AddLine();
        }

        protected void AddDrop()
        {
            if (!_rules.IncludeDrop) { return; }
            string objectType = (_rules.ObjectType == SqlObjectType.Table) ? "TABLE" : "PROCEDURE";
            string prefix = (_rules.ObjectType == SqlObjectType.Table) ? "" : _rules.PrefixNameWith;
            _code.Append(string.Format("DROP {0} [{1}{2}]", objectType, prefix, _table.Name));
            _code.Append(Environment.NewLine);
            _code.AddGo();
        }

        protected void AddAnsiNull()
        {
            if (!_rules.AddAnsiNull) { return; }
            _code.AppendWithNewLine("SET ANSI_NULLS ON");
            _code.AddGo();
        }

        protected void AddQuotedIdentifier()
        {
            if (!_rules.AddQuotedIdentifier) { return; }
            _code.AppendWithNewLine("SET QUOTED_IDENTIFIER ON");
            _code.AddGo();
        }

        protected string GetIdentityString(bool isIdentity)
        {
            return (isIdentity) ? " IDENTITY(1,1)" : "";
        }

        protected string GetNullString(bool isNullable)
        {
            return (isNullable) ? " NULL" : " NOT NULL";
        }
    }
}
