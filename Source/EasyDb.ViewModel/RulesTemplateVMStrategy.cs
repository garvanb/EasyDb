using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class RulesTemplateVMStrategy
    {
        public RulesTemplateVM GetRulesTemplateVM(LanguageType languageType, RulesTemplate rulesTemplate)
        {
            if (rulesTemplate == null) return null;
            if (languageType == LanguageType.SQL)
            {
                return new SqlTemplateVM(rulesTemplate);
            }
            else if (languageType == LanguageType.CSharp)
            {
                return new CSharpTemplateVM(rulesTemplate);
            }
            return null;
        }
    }
}
