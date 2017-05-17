using System.Collections.Generic;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class MainVM : ViewModelBase
    {
        public DebugVM DebugVM { get; set; }
        public DatabaseAndTemplateVM DatabaseAndTemplateVM { get; set; }
        public BulkOutputVM BulkOutputVM { get; set; }
        public List<ViewModelBase> Views { get; set; }
        public ServerVM ServerVM { get; set; }
        private bool _showDebug;
        public bool ShowDebug
        {
            get { return _showDebug; }
            set { SetValue(ref _showDebug, value, "ShowDebug"); }
        }

        //Commands
        public ICommand ShowHideDebugCommand
        { get { return new RelayCommand(parameter => ShowHideDebug()); } }
        public ICommand SwitchViewCommand
        { get { return new RelayCommand(parameter => SwitchView(parameter as ViewModelBase)); } }

        public void Initialise()
        {
            ExceptionDetails = new ExceptionVM();
            ServerVM = new ServerVM();
            ServerVM.Initialise();
            DebugVM = new DebugVM();
            DatabaseAndTemplateVM = new DatabaseAndTemplateVM();
            DatabaseAndTemplateVM.Initialise();
            BulkOutputVM = new BulkOutputVM();
            BulkOutputVM.Initialise();
            Views = new List<ViewModelBase>();
            Views.Add(DatabaseAndTemplateVM);
            Views.Add(BulkOutputVM);
            MainGridEnabled = true;
            //SwitchView(BulkOutputVM);
        }

        private void SwitchView(ViewModelBase selectedView)
        {
            foreach(ViewModelBase vmBase in Views)
            {
                vmBase.IsVisible = (vmBase == selectedView);
            }
        }

        private void ShowHideDebug()
        {
            ShowDebug = !ShowDebug;
            MainGridEnabled = !ShowDebug;
        }
    }
}
