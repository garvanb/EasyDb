using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDb.Logic
{
    public class FileOutput
    {
        public void SaveAs(string code)
        {
            string path = HandleSaveFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                SaveToLocation(path, code);
            }
        }

        public FileOutputResult SaveToLocationWithResult(string path, string code)
        {
            try
            {
                File.WriteAllText(path, code);
            }
            catch (Exception ex)
            {
                return new FileOutputResult(false, path, ex?.Message);
            }
            return new FileOutputResult(true, path);
        }

        public void SaveToLocation(string path, string code)
        {
            File.WriteAllText(path, code);
        }

        private string HandleSaveFileDialog()
        {
            string savePath = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "SQL Server files (*.sql)|*.sql|txt files (*.txt)|*.txt";
            saveDialog.AddExtension = true;
            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                savePath = saveDialog.FileName;
            }
            return savePath;
        }

    }
}
