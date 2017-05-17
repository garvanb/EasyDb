using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ExceptionVM ExceptionDetails { get; set; }

        private bool showException;
        public bool ShowException
        {
            get { return showException; }
            set { SetValue(ref showException, value, "ShowException"); }
        }

        private bool mainGridEnabled;
        public bool MainGridEnabled
        {
            get { return mainGridEnabled; }
            set { SetValue(ref mainGridEnabled, value, "MainGridEnabled"); }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetValue(ref _isVisible, value, "IsVisible"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Create the OnPropertyChanged method to raise the event 
        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Implement series of set methods here to avoid repetition
        protected bool SetValue<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public ICommand CloseExceptionPopupCommand
        {
            get { return new RelayCommand(parameter => CloseException()); }
        }

        private void CloseException()
        {
            ShowException = false;
            MainGridEnabled = true;
        }

        public void HandleException(Exception ex)
        {
            ExceptionDetails = new ExceptionVM();
            ExceptionDetails.Message = ex.Message;
            ExceptionDetails.InnerExceptionMessage = (ex.InnerException == null) ? "" : ex.InnerException.Message;
            ShowException = true;
            MainGridEnabled = false;
        }
    }
}
