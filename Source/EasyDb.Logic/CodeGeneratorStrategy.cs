using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class CodeGeneratorStrategy
    {
        public ICodeGenerator GetStrategy(LanguageType language, Template template)
        {
            if (language == LanguageType.SQL)
            {
                SqlTemplate rules = (SqlTemplate)template.RulesTemplate;
                if (rules.ObjectType == SqlObjectType.Table)
                {
                    return new SqlTableGenerator();
                }
                else if(rules.ObjectType == SqlObjectType.StoredProcedure)
                {
                    return new SqlStoredProcedureGenerator();
                }
            }
            else if (language == LanguageType.CSharp)
            {
                return new CSharpCodeGenerator();
            }
            return null;
        }
    }
}
