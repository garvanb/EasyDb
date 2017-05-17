using EasyDb.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class OutputResultVM : ViewModelBase
    {
        private bool _success;
        public bool Success
        {
            get { return _success; }
            set { SetValue(ref _success, value, "Success"); }
        }

        private string _filename;
        public string Filename
        {
            get { return _filename; }
            set { SetValue(ref _filename, value, "Filename"); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetValue(ref _message, value, "Message"); }
        }

        public OutputResultVM(FileOutputResult result)
        {
            Success = result.Success;
            Filename = result.Filename;
            Message = result.Message;
        }
    }
}
