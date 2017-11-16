using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.IO.FileSystem
{
    public interface IFileManager
    {
        string WorkspaceName { get; set; }
        void TryRecreateWorkspace();
        void DeleteFile(string fileName);
    }
}
