using EasyDb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    /// <summary>
    /// A DatabaseInteractor factory to
    /// save having to repeatedly input 
    /// Properties.Settings... 
    /// </summary>
    public class DbiFactory
    {

        public DatabaseInteractor GetDatabaseInteractor(string dbName)
        {
            DatabaseInteractor db = new DatabaseInteractor(Properties.Settings.Default.DefaultServer, dbName);
            return db;
        }
    }
}
