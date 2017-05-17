using EasyDb.Data;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class DatabaseVM : ViewModelBase
    {
        public string Name { get; set; }
        private ObservableCollection<TableVM> _tableVMs;
        public ObservableCollection<TableVM> TableVMs
        {
            get { return _tableVMs; }
            set { SetValue(ref _tableVMs, value, "TableVMs"); }
        }

        private TableVM _selectedTable;
        public TableVM SelectedTable
        {
            get { return _selectedTable; }
            set { SetValue(ref _selectedTable, value, "SelectedTable"); }
        }

        private TableVM _newTable;
        public TableVM NewTable
        {
            get { return _newTable; }
            set { SetValue(ref _newTable, value, "NewTable"); }
        }

        public ICommand AddTableCommand
        { get { return new RelayCommand(parameter => AddTable()); } }
        public ICommand DatabaseChangedCommand
        { get { return new RelayCommand(parameter => LoadTables()); } }

        public DatabaseVM()
        {
            TableVMs = new ObservableCollection<TableVM>();
            NewTable = new TableVM();
        }

        public DatabaseVM(string name)
        {
            Name = name;
        }

        public void LoadTables()
        {
            try
            {
                DatabaseInteractor currentDb = new DatabaseInteractor(Name);
                SetTables(currentDb.GetTables());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void SetTables(DataTable table)
        {
            TableVMs.Clear();
            foreach (DataRow row in table.Rows)
            {
                TableVMs.Add(new TableVM(Name, row));
            }
        }

        private void AddTable()
        {
            if (TableVMs.Any(t => t.Name == NewTable.Name) || string.IsNullOrEmpty(NewTable.Name)) { return; }
            SqlColumnDescriptionVM columnDescription = new SqlColumnDescriptionVM() { Value = 9 };
            ColumnVM idColumn = new ColumnVM() { Name = "Id", ColumnType = "int", Description = columnDescription, IsIdentity = true, IsNullable = false };
            NewTable.ColumnVMs.Add(idColumn);
            TableVMs.Add(NewTable);
            NewTable = new TableVM();
            SelectedTable = NewTable;
        }

        public List<SqlTable> ToTables()
        {
            List<SqlTable> tables = new List<SqlTable>();
            foreach (var tableVM in TableVMs)
            {
                tables.Add(tableVM.Table);
            }
            return tables;
        }
    }
}
