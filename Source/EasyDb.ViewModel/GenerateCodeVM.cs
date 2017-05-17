using EasyDb.Logic;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class GenerateCodeVM : ViewModelBase
    {
        public string DatabaseName { get; set; }

        private TableVM _selectedTable;
        public TableVM SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                SetSelectedCode();
            }
        }

        private TemplateVM _selectedTemplate;
        public TemplateVM SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;
                SetSelectedCode();
            }
        }

        List<GeneratedCodeVM> GeneratedCodes { get; set; }
      
        private string _selectedCode;
        public string SelectedCode
        {
            get { return _selectedCode; }
            set { SetValue(ref _selectedCode, value, "SelectedCode"); }
        }

        //Commands
        public ICommand GenerateCodeCommand
        { get { return new RelayCommand(parameter => GenerateCode()); } }
        public ICommand ClearCodeCommand
        { get { return new RelayCommand(parameter => ClearCode()); } }

        public GenerateCodeVM()
        {
            GeneratedCodes = new List<GeneratedCodeVM>();
        }

        private void GenerateCode()
        {
            if (SelectedTable == null || SelectedTemplate == null) return;
            ICodeGenerator codeGenerator = new CodeGeneratorStrategy().GetStrategy(SelectedTemplate.SelectedLanguageType, SelectedTemplate.Template);
            SelectedCode = codeGenerator.GenerateCode(DatabaseName, SelectedTemplate.Template, SelectedTable.ToSqlTable(), SelectedTable.Table);

            if (!GeneratedCodes.Any(gcvm => gcvm.DatabaseName == DatabaseName && gcvm.Table == SelectedTable && gcvm.Template == SelectedTemplate))
            {
                GeneratedCodes.Add(new GeneratedCodeVM(DatabaseName, SelectedTable, SelectedTemplate, SelectedCode));
            }
        }

        private void ClearCode()
        {
            SelectedCode = "";
        }

        /// <summary>
        /// If we've already generated code for this
        /// Table and Template combo then retrieve it
        /// </summary>
        private void SetSelectedCode()
        {
            var temp = GeneratedCodes.FirstOrDefault(gcvm => gcvm.Table == SelectedTable && gcvm.Template == SelectedTemplate);
            SelectedCode = temp?.Code;
        }
    }
}
