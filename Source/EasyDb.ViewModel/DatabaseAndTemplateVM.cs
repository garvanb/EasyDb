using EasyDb.Data;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class DatabaseAndTemplateVM : ViewModelBase
    {
        public ViewModelBase CurrentVM { get; set; }
        public DebugVM DebugVM { get; set; }
        public DatabaseVM SelectedDatabaseVM { get; set; }
        public GenerateCodeVM GenerateCodeVM { get; set; }
        public TemplatesVM TemplatesVM { get; set; }
        public OutputVM OutputVM { get; set; }
        public ServerVM ServerVM { get; set; }

        public ObservableCollection<SqlColumnDescription> ColumnDescriptions { get; set; }
        public List<string> Languages { get; set; }

        //Commands
        public ICommand ShowHideDebugCommand
        { get { return new RelayCommand(parameter => ShowHideDebug()); } }
        public ICommand DatabaseChangedCommand
        { get { return new RelayCommand(parameter => ChangeDatabase()); } }
        public ICommand SelectedTableChangedCommand
        { get { return new RelayCommand(parameter => ChangeTable(parameter as TableVM)); } }
        public ICommand SelectedTemplateChangedCommand
        { get { return new RelayCommand(parameter => ChangeTemplate(parameter as TemplateVM)); } }

        private bool _showDebug;
        public bool ShowDebug
        {
            get { return _showDebug; }
            set { SetValue(ref _showDebug, value, "ShowDebug"); }
        }

        public void Initialise()
        {
            ExceptionDetails = new ExceptionVM();
            DebugVM = new DebugVM();
            TemplatesVM = new TemplatesVM();
            SetColumnDescriptions();
            SetLanguages();
            SelectedDatabaseVM = new DatabaseVM();
            GenerateCodeVM = new GenerateCodeVM();
            GenerateCodeVM.DatabaseName = SelectedDatabaseVM.Name;
            OutputVM = new OutputVM();
            MainGridEnabled = true;
        }

        private void ChangeDatabase()
        {
            SelectedDatabaseVM.LoadTables();
            GenerateCodeVM.DatabaseName = SelectedDatabaseVM.Name;
        }

        private void SetColumnDescriptions()
        {
            try
            {
                DatabaseInteractor defaultDb = new DatabaseInteractor();
                ColumnDescriptions = defaultDb.GetSqlColumnDescriptions();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void SetLanguages()
        {
            try
            {
                DatabaseInteractor defaultDb = new DatabaseInteractor();
                Languages = defaultDb.GetLanguages();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ShowHideDebug()
        {
            ShowDebug = !ShowDebug;
            MainGridEnabled = !ShowDebug;
        }

        private void ChangeTable(TableVM selectedTable)
        {
            SelectedDatabaseVM.SelectedTable = selectedTable;
            GenerateCodeVM.SelectedTable = selectedTable;
        }

        private void ChangeTemplate(TemplateVM selectedTemplate)
        {
            TemplatesVM.SelectedTemplate = selectedTemplate;
            GenerateCodeVM.SelectedTemplate = selectedTemplate;
        }

    }
}
