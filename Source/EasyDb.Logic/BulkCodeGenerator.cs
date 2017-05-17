using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class BulkCodeGenerator
    {
        public ObservableCollection<GeneratedCode> GenerateCode(string dbName, List<SqlTable> tables, List<Template> templates)
        {
            ObservableCollection<GeneratedCode> results = new ObservableCollection<GeneratedCode>();
            foreach (Template template in templates)
            {
                foreach (SqlTable table in tables)
                {
                    ICodeGenerator codeGenerator = new CodeGeneratorStrategy().GetStrategy(template.Type, template);
                    string code = codeGenerator.GenerateCode(dbName, template, table, table);
                    results.Add(new GeneratedCode(dbName, table, template, code));
                }
            }
            return results;
        }

    }
}
