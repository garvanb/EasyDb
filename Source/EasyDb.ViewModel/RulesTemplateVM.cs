using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class RulesTemplateVM : ViewModelBase
    {
        private int _id;
        public int Id
        {     
            get { return _id; }
            set { SetValue(ref _id, value, "SelectedTemplate"); }
        }
    }
}
