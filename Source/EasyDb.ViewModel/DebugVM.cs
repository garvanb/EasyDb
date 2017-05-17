using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;

namespace EasyDb.ViewModel
{
    public class DebugVM : ViewModelBase
    {
        private ObservableCollection<DebugPropertyVM> _debugProperties;
        public ObservableCollection<DebugPropertyVM> DebugProperties
        {
            get { return _debugProperties; }
            set { SetValue(ref _debugProperties, value, "DebugProperties"); }
        }

        public DebugVM()
        {
            SetDebugProperties();
        }

        public ICommand CopyDebugPropertiesCommand
        { get { return new RelayCommand(parameter => CopyDebugProperties()); } }

        private void CopyDebugProperties()
        {
            StringBuilder properties = new StringBuilder();
            foreach (DebugPropertyVM dpvm in DebugProperties)
            {
                if (properties.Length > 0)
                {
                    properties.Append(Environment.NewLine);
                    properties.Append(Environment.NewLine);
                }
                properties.Append(string.Format("{0}: {1}", dpvm.Name, dpvm.Value));
            }
            Clipboard.SetText(properties.ToString());
        }

        private void SetDebugProperties()
        {
            DebugProperties = new ObservableCollection<DebugPropertyVM>();
            //User Config
            var userConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            DebugProperties.Add(new DebugPropertyVM("User Config", userConfig, true));
            //App config
            var appConfig = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            DebugProperties.Add(new DebugPropertyVM("App Config", appConfig, true));
            //Executable directory
            var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DebugProperties.Add(new DebugPropertyVM("Exe Directory", exeDirectory, true));
        }

    }
}
