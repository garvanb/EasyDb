using EasyDb.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    /// <summary>
    /// Responsible for handling file save dialogs,
    /// writing files to network etc.
    /// </summary>
    public class OutputVM
    {
        public ICommand SaveAsCodeCommand
        { get { return new RelayCommand(parameter => SaveAsCode(parameter as string)); } }
        public ICommand CopyCodeCommand
        { get { return new RelayCommand(parameter => CopyCode(parameter as string)); } }

        private void SaveAsCode(string code)
        {
            FileOutput fileOutput = new FileOutput();
            fileOutput.SaveAs(code);
        }

        private void CopyCode(string code)
        {
            if (!string.IsNullOrEmpty(code)) { Clipboard.SetText(code); }
        }
    }
}
