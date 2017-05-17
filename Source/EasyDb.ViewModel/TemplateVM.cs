using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyDb.Model;
using System.Windows.Input;
using EasyDb.Data;

namespace EasyDb.ViewModel
{
    public class TemplateVM : ViewModelBase
    {
        private bool _isSql;
        public bool IsSql
        {
            get { return _isSql; }
            set { SetValue(ref _isSql, value, "IsSql"); }
        }

        private bool _isCSharp;
        public bool IsCSharp
        {
            get { return _isCSharp; }
            set { SetValue(ref _isCSharp, value, "IsCSharp"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value, "Name"); }
        }

        private LanguageType _selectedLanguageType;
        public LanguageType SelectedLanguageType
        {
            get { return _selectedLanguageType; }
            set { SetValue(ref _selectedLanguageType, value, "SelectedLanguageType"); }
        }

        public Template Template { get; set; }
        private RulesTemplateVM _rulesTemplateVM;
        public RulesTemplateVM RulesTemplateVM
        {
            get { return _rulesTemplateVM; }
            set { SetValue(ref _rulesTemplateVM, value, "RulesTemplateVM"); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetValue(ref _isSelected, value, "IsSelected"); }
        }

        //Commands
        public ICommand SaveTemplateCommand
        { get { return new RelayCommand(parameter => SaveTemplate()); } }
        public ICommand LanguageChangedCommand
        { get { return new RelayCommand(parameter => CheckLanguage()); } }

        private void CheckLanguage()
        {
            IsSql = (SelectedLanguageType == LanguageType.SQL);
            IsCSharp = (SelectedLanguageType == LanguageType.CSharp);
        }


        public TemplateVM(Template template)
        {
            ExceptionDetails = new ExceptionVM();
            Template = template;
            Name = template.Name;
            RulesTemplateVMStrategy strategy = new RulesTemplateVMStrategy();
            RulesTemplateVM = strategy.GetRulesTemplateVM(template.Type, template.RulesTemplate);
            SelectedLanguageType = template.Type;
            CheckLanguage();
        }

        private void SaveTemplate()
        {
            try
            {
                DatabaseInteractor db = new DatabaseInteractor();
                Template.Name = _name;
                Template.Type = _selectedLanguageType;
                db.UpdateTemplate(Template);
                SaveRulesTemplate();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Updates the underpinning Template property
        /// locally after succesful database save
        /// </summary>
        private void UpdateTemplate(IRulesTemplate rulesTemplate)
        {
            RulesTemplateStrategy rulesTemplateStrategy = new RulesTemplateStrategy();
            Template.RulesTemplate = rulesTemplateStrategy.GetRulesTemplate(Template.Type, rulesTemplate);
        }

        /// <summary>
        /// Updates the template in the database
        /// </summary>
        private void SaveRulesTemplate()
        {
            DatabaseInteractor db = new DatabaseInteractor();
            if (Template.Type == LanguageType.SQL)
            {
                SqlTemplateVM sqlTemplateVM = (SqlTemplateVM)RulesTemplateVM;
                SqlTemplate sqlTemplate = sqlTemplateVM.ToSqlTemplate();
                db.UpdateSqlTemplate(sqlTemplate);
                UpdateTemplate(sqlTemplate);
            }
            else if(Template.Type == LanguageType.CSharp)
            {
                CSharpTemplateVM cSharpTemplateVM = (CSharpTemplateVM)RulesTemplateVM;
                CSharpTemplate cSharpTemplate = cSharpTemplateVM.ToCSharpTemplate();
                db.UpdateCSharpTemplate(cSharpTemplate);
                UpdateTemplate(cSharpTemplate);
            }  
        }
    }
}
