using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public class ExceptionVM : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetValue(ref _message, value, "Message"); }
        }

        private string _innerExceptionMessage;
        public string InnerExceptionMessage
        {
            get { return _innerExceptionMessage; }
            set { SetValue(ref _innerExceptionMessage, value, "InnerExceptionMessage"); }
        }

        public ExceptionVM() { }
    }
}
