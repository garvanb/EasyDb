using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class CSharpTemplateVM : RulesTemplateVM
    {
        private int _templateId;
        public int TemplateId
        {
            get { return _templateId; }
            set { SetValue(ref _templateId, value, "TemplateId"); }
        }

        private string _classNameSuffix;
        public string ClassNameSuffix
        {
            get { return _classNameSuffix; }
            set { SetValue(ref _classNameSuffix, value, "ClassNameSuffix"); }
        }

        private string _namespace;
        public string Namespace
        {
            get { return _namespace; }
            set { SetValue(ref _namespace, value, "Namespace"); }
        }

        private bool _includeUsings;
        public bool IncludeUsings
        {
            get { return _includeUsings; }
            set { SetValue(ref _includeUsings, value, "IncludeUsings"); }
        }

        private bool _includeProperties;
        public bool IncludeProperties
        {
            get { return _includeProperties; }
            set { SetValue(ref _includeProperties, value, "IncludeProperties"); }
        }

        private bool _includeEmptyConstructor;
        public bool IncludeEmptyConstructor
        {
            get { return _includeEmptyConstructor; }
            set { SetValue(ref _includeEmptyConstructor, value, "IncludeEmptyConstructor"); }
        }

        private bool _includeOtherConstructor;
        public bool IncludeOtherConstructor
        {
            get { return _includeOtherConstructor; }
            set { SetValue(ref _includeOtherConstructor, value, "IncludeOtherConstructor"); }
        }

        private CSharpTemplate _cSharpTemplate;
        public CSharpTemplate CSharpTemplate
        {
            get { return _cSharpTemplate; }
            set { SetValue(ref _cSharpTemplate, value, "CSharpTemplate"); }
        }

        public CSharpTemplateVM(RulesTemplate rulesTemplate)
        {
            CSharpTemplate rules = (CSharpTemplate)rulesTemplate;
            Id = rulesTemplate.Id;
            TemplateId = rules.TemplateId;
            ClassNameSuffix = rules.ClassNameSuffix;
            Namespace = rules.Namespace;
            IncludeUsings = rules.IncludeUsings;
            IncludeProperties = rules.IncludeProperties;
            IncludeEmptyConstructor = rules.IncludeEmptyConstructor;
            IncludeOtherConstructor = rules.IncludeOtherConstructor;
        }

        public CSharpTemplateVM()
        {
            CSharpTemplate = new CSharpTemplate();
        }

        public CSharpTemplate ToCSharpTemplate()
        {
            CSharpTemplate cSharpTemplate = new CSharpTemplate();
            cSharpTemplate.Id = Id;
            cSharpTemplate.ClassNameSuffix = ClassNameSuffix;
            cSharpTemplate.Namespace = Namespace;
            cSharpTemplate.IncludeUsings = IncludeUsings;
            cSharpTemplate.IncludeProperties = IncludeProperties;
            cSharpTemplate.IncludeEmptyConstructor = IncludeEmptyConstructor;
            cSharpTemplate.IncludeOtherConstructor = IncludeOtherConstructor;
            return cSharpTemplate;
        }
    }
}
