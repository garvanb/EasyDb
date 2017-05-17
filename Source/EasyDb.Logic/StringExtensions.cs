using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public static class StringExtensions
    {
        public static string LowerFirstChar(this string value)
        {
            value = char.ToLowerInvariant(value[0]) + value.Substring(1);
            return value;
        }
    }
}
