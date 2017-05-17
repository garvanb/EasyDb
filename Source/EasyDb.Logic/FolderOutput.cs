using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class FolderOutput
    {
        public List<FileOutputDetails> Files;
        public ObservableCollection<FileOutputResult> Results;
        public string RootDirectory;

        public FolderOutput(string rootDirectory)
        {
            Files = new List<FileOutputDetails>();
            Results = new ObservableCollection<FileOutputResult>();
            RootDirectory = rootDirectory;
        }

        public ObservableCollection<FileOutputResult> SaveFiles()
        {
            if (Files.Count == 0) return Results;
            if (!Directory.Exists(RootDirectory)) { Directory.CreateDirectory(RootDirectory); }
            FileOutput fileOutput = new FileOutput();
            foreach(FileOutputDetails details in Files)
            {
                string path = Path.Combine(RootDirectory, details.Path);
                FileOutputResult result = fileOutput.SaveToLocationWithResult(path, details.Text);
                Results.Add(result);
            }
            return Results;
        }

        public void AddExtensions(string extension)
        {
            foreach (FileOutputResult result in Results)
            {
                result.Filename = result.Filename + extension;
            }
        }
    }
}
