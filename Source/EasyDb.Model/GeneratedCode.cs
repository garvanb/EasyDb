using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class GeneratedCode
    { 
        public string DatabaseName { get; set; }
        public SqlTable Table { get; set; }
        public Template Template { get; set; }
        public string Code { get; set; }

        public GeneratedCode(string databaseName, SqlTable table, Template template, string code)
        {
            DatabaseName = databaseName;
            Table = table;
            Template = template;
            Code = code;
        }
    }
}
