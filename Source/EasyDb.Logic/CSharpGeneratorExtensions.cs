using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public static class CSharpGeneratorExtensions
    {
        public static void AddUsing(this StringBuilder code, string text)
        {
            code.Append("using ");
            code.Append(text);
            code.Append(Environment.NewLine);
        }

    }
}
