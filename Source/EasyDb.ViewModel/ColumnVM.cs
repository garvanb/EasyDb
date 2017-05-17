using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EasyDb.Model;
using System.Collections.ObjectModel;
using EasyDb.Data;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class ColumnVM : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value, "Name"); }
        }
        private bool _isIdentity;
        public bool IsIdentity
        {
            get { return _isIdentity; }
            set { SetValue(ref _isIdentity, value, "IsIdentity"); }
        }
        public string _columnType;
        public string ColumnType
        {
            get { return _columnType; }
            set { SetValue(ref _columnType, value, "ColumnType"); }
        }
        public bool _isNullable;
        public bool IsNullable
        {
            get { return _isNullable; }
            set { SetValue(ref _isNullable, value, "IsNullable"); }
        }
        private int? _maxLength;
        public int? MaxLength
        {
            get { return _maxLength; }
            set { SetValue(ref _maxLength, value, "MaxLength"); }
        }
        private SqlColumnType _type;
        public SqlColumnType Type
        {
            get { return _type; }
            set { SetValue(ref _type, value, "Type"); }
        }
        private SqlColumn _column { get; set; }
        private ObservableCollection<SqlColumnDescription> _descriptions;

        private SqlColumnDescriptionVM _description;
        public SqlColumnDescriptionVM Description
        {
            get { return _description; }
            set { SetValue(ref _description, value, "Description"); }
        }

        public ICommand UpdateColumnCommand
        { get { return new RelayCommand(parameter => UpdateColumn()); } }

        public ColumnVM()
        {
            Description = new SqlColumnDescriptionVM();
            DatabaseInteractor easyDb = new DatabaseInteractor();
            _descriptions = easyDb.GetSqlColumnDescriptions();
        }

        public ColumnVM(SqlColumn column, ObservableCollection<SqlColumnDescription> descriptions)
        {
            _column = column;
            Name = column.Name;
            IsIdentity = column.IsIdentity;
            ColumnType = column.ColumnType;
            IsNullable = column.IsNullable;
            MaxLength = column.MaxLength;
            _descriptions = descriptions;
            SetColumnDescription();
        }

        private void SetColumnDescription()
        {
            string filter = string.Format("{0}{1}", ColumnType, _column.MaxLengthPostFix);
            var description = _descriptions.ToList().FirstOrDefault(d => d.Description == filter);
            Description = new SqlColumnDescriptionVM(description);
        }

        private void UpdateColumn()
        {
            SetColumnDescriptionByValue();
            SetColumnTypeByDataType();
            SetMaxLengthByDescription();
        }

        private void SetColumnDescriptionByValue()
        {
            var description = _descriptions.ToList().FirstOrDefault(d => d.Value == Description.Value);
            Description = new SqlColumnDescriptionVM(description);
        }

        private void SetColumnTypeByDataType()
        {
            ColumnType = Description.DataType;
        }

        private void SetMaxLengthByDescription()
        {
            int startIndex = Description.Description.IndexOf("(");
            if (startIndex < 0) { return; }
            string postfix = Description.Description.Substring(startIndex);
            if (postfix == "(MAX)")
            {
                MaxLength = -1;
                return;
            }
            else
            {
                string length = postfix.Substring(1);
                length = length.Substring(0, length.Length - 1);
                MaxLength = int.Parse(length);
                return;
            }
        }
    }
}
