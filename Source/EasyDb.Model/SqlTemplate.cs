using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class SqlTemplate : RulesTemplate
    {
        public bool AddAnsiNull { get; set; }
        public bool AddQuotedIdentifier { get; set; }
        public bool IncludeBeginEnd { get; set; }
        public bool IncludeIfExistsDrop { get; set; }
        public bool IncludeDrop { get; set; }
        public string PrefixNameWith { get; set; }
        public string PrefixNameForBulkSaveWith { get; set; }
        public SqlObjectType ObjectType { get; set; }
        public SqlStatementType StatementType { get; set; }
        public int TemplateId { get; set; }
        public bool UseParametersToGenerateData { get; set; }
    }
}
