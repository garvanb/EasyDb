using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class GeneratedCodeVM : ViewModelBase
    {
        public string DatabaseName { get; set; }
        public TableVM Table { get; set; }
        public TemplateVM Template { get; set; }
        public string Code { get; set; }

        public GeneratedCodeVM(string databaseName, TableVM table, TemplateVM template, string code)
        {
            DatabaseName = databaseName;
            Table = table;
            Template = template;
            Code = code;
        }

        public GeneratedCodeVM(GeneratedCode generateCode)
        {
            DatabaseName = generateCode.DatabaseName;
            Table = new TableVM() { Table = generateCode.Table, Name = generateCode.Table.Name };
            Template = new TemplateVM(generateCode.Template);
            Code = generateCode.Code;
        }
    }
}
