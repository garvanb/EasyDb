using EasyDb.Data;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class TemplatesVM : ViewModelBase
    {
        private ObservableCollection<TemplateVM> _templates;
        public ObservableCollection<TemplateVM> Templates
        {
            get { return _templates; }
            set { SetValue(ref _templates, value, "Templates"); }
        }

        private TemplateVM _selectedTemplate;
        public TemplateVM SelectedTemplate
        {
            get { return _selectedTemplate; }
            set { SetValue(ref _selectedTemplate, value, "SelectedTemplate"); }
        }

        public ICommand SelectAllCommand
        { get { return new RelayCommand(parameter => SelectAll(true)); } }
        public ICommand DeSelectAllCommand
        { get { return new RelayCommand(parameter => SelectAll(false)); } }

        public TemplatesVM()
        {
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            DatabaseInteractor defaultDb = new DatabaseInteractor();
            List<Template> templates = defaultDb.GetTemplates();
            List<TemplateVM> templatesVM = templates.ConvertAll(x => new TemplateVM(x));
            Templates = new ObservableCollection<TemplateVM>(templatesVM);
        }

        public List<Template> SelectedToTemplates()
        {
            List<Template> templates = new List<Template>();
            foreach (TemplateVM templateVM in Templates)
            {
               if(templateVM.IsSelected) templates.Add(templateVM.Template);
            }
            return templates;
        }

        private void SelectAll(bool select)
        {
            foreach (TemplateVM templateVM in Templates)
            {
                templateVM.IsSelected = select;
            }
        }
    }
}
