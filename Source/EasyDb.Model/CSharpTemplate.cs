using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class CSharpTemplate : RulesTemplate
    {
        public int TemplateId { get; set; }
        public string ClassNameSuffix { get; set; }
        public string Namespace { get; set; }
        public bool IncludeUsings { get; set; }
        public bool IncludeProperties { get; set; }
        public bool IncludeEmptyConstructor { get; set; }
        public bool IncludeOtherConstructor { get; set; }   
    }
}
