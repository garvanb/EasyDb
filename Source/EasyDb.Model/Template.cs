using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }
        public LanguageType Type { get; set; }
        public RulesTemplate RulesTemplate { get; set; }
    }
}
