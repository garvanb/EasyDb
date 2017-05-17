using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public interface ICodeGenerator
    {
        string GenerateCode(string databaseName, Template template, SqlTable table, SqlTable originalTable);
    }
}
