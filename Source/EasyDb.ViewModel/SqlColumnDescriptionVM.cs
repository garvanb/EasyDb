using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class SqlColumnDescriptionVM : ViewModelBase
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set { SetValue(ref _value, value, "Value"); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value, "Description"); }
        }
        private string _dataType;
        public string DataType
        {
            get { return _dataType; }
            set { SetValue(ref _dataType, value, "DataType"); }
        }

        public SqlColumnDescriptionVM() { }

        public SqlColumnDescriptionVM(SqlColumnDescription columnDescription)
        {
            Value = columnDescription.Value;
            Description = columnDescription.Description;
            DataType = columnDescription.DataType;
        }
    }
}
