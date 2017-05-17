using EasyDb.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class SelectDatabaseVM : ViewModelBase
    {
        private ObservableCollection<string> _databases;
        public ObservableCollection<string> Databases
        {
            get { return _databases; }
            set { SetValue(ref _databases, value, "Databases"); }
        }

        public SelectDatabaseVM()
        {
            Databases = new ObservableCollection<string>();
        }

        public void PopulateDatabases(string serverName)
        {
            DatabaseInteractor defaultDb = new DatabaseInteractor(serverName, true);
            Databases = defaultDb.GetDatabases();
        }
    }
}
