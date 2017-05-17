using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class RulesTemplateStrategy
    {
        public RulesTemplate GetRulesTemplate(LanguageType languageType, IRulesTemplate rulesTemplate)
        {
            if (languageType == LanguageType.SQL)
            {
                return (SqlTemplate)rulesTemplate;
            }
            else if (languageType == LanguageType.CSharp)
            {
                return (CSharpTemplate)rulesTemplate;
            }
            return null;
        }
    }
}
