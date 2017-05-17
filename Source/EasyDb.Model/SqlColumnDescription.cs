using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Model
{
    public class SqlColumnDescription
    {
        public int Value { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }

        public SqlColumnDescription(int value, string description, string dataType)
        {
            Value = value;
            Description = description;
            DataType = dataType;
        }
    }
}
