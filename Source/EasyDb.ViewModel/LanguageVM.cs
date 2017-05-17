using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class LanguageVM : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value, "Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value, "Name"); }
        }

        private Language _language;
        public Language Language
        {
            get { return _language; }
            set { SetValue(ref _language, value, "Languages"); }
        }

        public LanguageVM(Language language)
        {
            Language = language;
        }

        public LanguageVM()
        {
            Language = new Language();
        }
    }
}