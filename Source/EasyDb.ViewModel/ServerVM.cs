using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class ServerVM : ViewModelBase
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            set { SetValue(ref _name, value, "Name"); }
        }        
        public string UserName
        {
            get { return Environment.UserName; }
            set {; }
        }
        private bool _promptOnStartup;
        public bool PromptOnStartup
        {
            get { return _promptOnStartup; }
            set { SetValue(ref _promptOnStartup, value, "PromptOnStartup"); }
        }

        public bool UseTrustedConnection { get; set; }
        public SelectDatabaseVM SelectDatabaseVM { get; set; }

        public ServerVM()
        {
            SelectDatabaseVM = new SelectDatabaseVM();
        }

        public void Initialise()
        {
            var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            if (File.Exists(path))
            {
                IsVisible = Data.Properties.Settings.Default.PromptOnStartup;
                PromptOnStartup = Data.Properties.Settings.Default.PromptOnStartup;
                Name = Data.Properties.Settings.Default.DefaultServer;
                if (!PromptOnStartup) { ConnectCommand.Execute(this); }
            }
        }

        public ICommand HideCommand
        { get { return new RelayCommand(parameter => ShowHide(false)); } }
        public ICommand ShowCommand
        { get { return new RelayCommand(parameter => ShowHide(true)); } }
        public ICommand ConnectCommand
        { get { return new RelayCommand(parameter => Connect()); } }

        private void ShowHide(bool isVisible)
        {
            IsVisible = isVisible;
        }

        private void Connect()
        {
            try
            {
                SelectDatabaseVM.PopulateDatabases(Name);
                IsVisible = false;
                Data.Properties.Settings.Default.PromptOnStartup = PromptOnStartup;
                Data.Properties.Settings.Default.DefaultServer = Name;
                Data.Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
