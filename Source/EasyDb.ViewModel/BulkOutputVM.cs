using EasyDb.Data;
using EasyDb.Logic;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class BulkOutputVM : ViewModelBase
    {
        public DatabaseVM SelectedDatabaseVM { get; set; }
        public OutputOptionsVM OutputOptionsVM { get; set; }
        public TemplatesVM TemplatesVM { get; set; }
        public ObservableCollection<OutputResultVM> Results { get; set; }

        public ICommand OutputDatabaseScriptsCommand
        { get { return new RelayCommand(parameter => OutputDatabaseScripts()); } }

        public void Initialise()
        {
            SelectedDatabaseVM = new DatabaseVM();
            OutputOptionsVM = new OutputOptionsVM();
            TemplatesVM = new TemplatesVM();
            Results = new ObservableCollection<OutputResultVM>();
            IsVisible = false;
        }

        private void OutputDatabaseScripts()
        {
            if (string.IsNullOrEmpty(SelectedDatabaseVM.Name) || OutputOptionsVM.OutputDirectory == "") return;
            Results.Clear();
            List<GeneratedCodeVM> codeResults = GenerateDatabaseScripts();
            FolderOutput folderOutput = BuildFilesToOutput(codeResults);
            ObservableCollection<FileOutputResult> results = folderOutput.SaveFiles();
            UpdateResults(results);
        }

        private void UpdateResults(ObservableCollection<FileOutputResult> results)
        {
            Results.AddToCollection(results.Select(r => new OutputResultVM(r)).ToList());
        }


        private List<GeneratedCodeVM> GenerateDatabaseScripts()
        {
            if (!TemplatesVM.Templates.Any(t => t.IsSelected)) return new List<GeneratedCodeVM>();
            BulkCodeGenerator bulkCodeGenerator = new BulkCodeGenerator();
            ObservableCollection<GeneratedCode> codes = bulkCodeGenerator.GenerateCode(SelectedDatabaseVM.Name, SelectedDatabaseVM.ToTables(), TemplatesVM.SelectedToTemplates());
            List<GeneratedCodeVM> results = codes.Select(c => new GeneratedCodeVM(c)).ToList();
            return results.ToList();
        }

        private FolderOutput BuildFilesToOutput(List<GeneratedCodeVM> codeResults)
        {
            string outputDirectory = Path.Combine(OutputOptionsVM.OutputDirectory, SelectedDatabaseVM.Name);
            FolderOutput folderOutput = new FolderOutput(outputDirectory);
            if (codeResults.Count == 0)
                return folderOutput;
            foreach (GeneratedCodeVM codeResult in codeResults)
            {
                FileOutputDetails details = GetFileOutputDetails(codeResult);
                folderOutput.Files.Add(details);
            }
            return folderOutput;
        }

        private FileOutputDetails GetFileOutputDetails(GeneratedCodeVM codeResult)
        {
            FileOutputDetails details = new FileOutputDetails();
            string extension = codeResult.Template.IsSql ? ".sql" : ".cs";
            string prefix = "";
            if (codeResult.Template.IsSql)
            {
                SqlTemplate template = (SqlTemplate)codeResult.Template.Template.RulesTemplate;
                prefix = template.PrefixNameWith;
            }
            details.Path = string.Format("{0}{1}{2}", prefix, codeResult.Table.Name, extension);
            details.Text = codeResult.Code;
            return details;
        }
    }
}
