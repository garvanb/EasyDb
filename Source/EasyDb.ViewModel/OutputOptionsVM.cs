using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class OutputOptionsVM : ViewModelBase
    {
        private bool _useDefaultWorkspace;
        public bool UseDefaultWorkspace
        {
            get { return _useDefaultWorkspace; }
            set { SetValue(ref _useDefaultWorkspace, value, "UseDefaultWorkspace"); }
        }

        private bool _allowFolderOutputSelection;
        public bool AllowFolderOutputSelection
        {
            get { return _allowFolderOutputSelection; }
            set { SetValue(ref _allowFolderOutputSelection, value, "AllowFolderOutputSelection"); }
        }

        private string _outputDirectory;
        public string OutputDirectory
        {
            get { return _outputDirectory; }
            set { SetValue(ref _outputDirectory, value, "OutputDirectory"); }
        }

        //Commands OutputDatabaseScriptsCommand
        public ICommand AllowFolderSelectionCommand
        { get { return new RelayCommand(parameter => AllowFolderSelection()); } }
        public ICommand OpenOutputFolderCommand
        { get { return new RelayCommand(parameter => OpenOutputFolder()); } }


        public OutputOptionsVM()
        {
            UseDefaultWorkspace = true;
            OutputDirectory = @"C:\Users\Garvan\Documents";
        }

        private void AllowFolderSelection()
        {
            AllowFolderOutputSelection = !UseDefaultWorkspace;
            OutputDirectory = @"C:\Users\Garvan\Documents";
        }

        private void OpenOutputFolder()
        {
            if (!string.IsNullOrEmpty(OutputDirectory))
            {
                if (Directory.Exists(OutputDirectory))
                {
                    Process.Start(OutputDirectory);
                }
            }
        }

    }
}
