using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public class FileOutputResult
    {
        public bool Success { get; set; }
        public string Filename { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Only call this constructor when it's a success
        /// </summary>
        /// <param name="success"></param>
        /// <param name="filename"></param>
        public FileOutputResult(bool success, string filename)
        {
            Success = success;
            Filename = filename;
            Message = "Success";
        }

        public FileOutputResult(bool success, string filename, string message)
        {
            Success = success;
            Filename = filename;
            Message = message;
        }
    }
}
