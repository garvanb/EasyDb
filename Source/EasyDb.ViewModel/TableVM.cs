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
    public class TableVM : ViewModelBase
    {
        public SqlTable Table;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value, "Name"); }
        }

        private ObservableCollection<ColumnVM> _columnVMs;
        public ObservableCollection<ColumnVM> ColumnVMs
        {
            get { return _columnVMs; }
            set { SetValue(ref _columnVMs, value, "ColumnVMs"); }
        }

        private ColumnVM _newColumn;
        public ColumnVM NewColumn
        {
            get { return _newColumn; }
            set { SetValue(ref _newColumn, value, "NewColumn"); }
        }

        //Commands
        public ICommand AddColumnCommand
        { get { return new RelayCommand(parameter => AddColumn()); } }
        public ICommand DeleteColumnCommand
        { get { return new RelayCommand(parameter => DeleteColumn(parameter as ColumnVM)); } }

        public TableVM()
        {
            ColumnVMs = new ObservableCollection<ColumnVM>();
            NewColumn = new ColumnVM();
        }

        public TableVM(string dbName, DataRow row)
        {
            Name = row["TABLE_NAME"].ToString();
            List<string> identityColumns = GetIdentityColumns(dbName);
            BuildTable(dbName, row, identityColumns);
            ColumnVMs = new ObservableCollection<ColumnVM>();
            DatabaseInteractor easyDb = new DatabaseInteractor();
            ObservableCollection<SqlColumnDescription> descriptions = easyDb.GetSqlColumnDescriptions();
            foreach (SqlColumn column in Table.Columns)
            {
                ColumnVMs.Add(new ColumnVM(column, descriptions));
            }
            NewColumn = new ColumnVM();
        }

        private void BuildTable(string dbName, DataRow row, List<string> identityColumns)
        {
            DatabaseInteractor db = new DatabaseInteractor(dbName);
            DataTable table = db.GetColumns(Name);
            DataRow[] columns = table.Select("", "ORDINAL_POSITION ASC");
            Table = new SqlTable(dbName, row, columns, identityColumns);
        }

        private List<string> GetIdentityColumns(string dbName)
        {
            DatabaseInteractor db = new DatabaseInteractor(dbName);
            return db.GetIdentityColumns(Name);
        }

        private void AddColumn()
        {
            if (ColumnVMs.Any(c => c.Name == NewColumn.Name)) { return; }
            //NewColumn.SetColumnDescriptionByValue();
            //NewColumn.SetColumnTypeByDataType();
            ColumnVMs.Add(NewColumn);
            NewColumn = new ColumnVM();
        }

        private void DeleteColumn(ColumnVM columnVM)
        {
            ColumnVMs.Remove(columnVM);
        }

        public SqlTable ToSqlTable()
        {
            SqlTable table = new SqlTable();
            table.Name = Name;
            foreach (ColumnVM columnVM in ColumnVMs)
            {
                SqlColumn column = new SqlColumn();
                column.Name = columnVM.Name;
                column.IsIdentity = columnVM.IsIdentity;
                column.ColumnType = columnVM.ColumnType;
                column.IsNullable = columnVM.IsNullable;
                column.MaxLength = columnVM.MaxLength;
                column.MaxLengthPostFix = column.GetStringPostFix();
                table.Columns.Add(column);
            }

            return table;
        }
    }
}
