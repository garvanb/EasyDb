using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class FolderSelectorVM : ViewModelBase
    {
        private string _folder;
        public string Folder
        {
            get { return _folder; }
            set { SetValue(ref _folder, value, "Folder"); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { SetValue(ref _isValid, value, "IsValid"); }
        }

        public ICommand CheckDirectoryExistsCommand
        { get { return new RelayCommand(parameter => CheckDirectoryExists()); } }

        private void CheckDirectoryExists()
        {
            IsValid = Directory.Exists(Folder);
        }
    }
}
