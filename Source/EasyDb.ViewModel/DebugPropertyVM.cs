using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class DebugPropertyVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsLocation { get; set; }

        public DebugPropertyVM(string name, string value, bool isLocation)
        {
            Name = name;
            Value = value;
            IsLocation = isLocation;
        }

        public ICommand CopyCommand
        { get { return new RelayCommand(parameter => Copy()); } }

        private void Copy()
        { Clipboard.SetText(Value); }

        public ICommand OpenLocationCommand
        { get { return new RelayCommand(parameter => OpenLocation()); } }

        private void OpenLocation()
        {
            if (Directory.Exists(Value))
            {
                StartProcess(Value);
            }
            else
            {
                if (File.Exists(Value))
                {
                    FileInfo fi = new FileInfo(Value);
                    StartProcess(fi.DirectoryName);
                }
            }
        }

        private void StartProcess(string location)
        {
            Process process = new Process { StartInfo = new ProcessStartInfo(location) };
            process.Start();
        }
    }
}
