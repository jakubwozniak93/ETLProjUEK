using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MobiParse.IO.FileSystem;
using MobiParse.iOS.IO.FileSystem;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]
namespace MobiParse.iOS.IO.FileSystem
{
    class FileManager : IFileManager
    {
        private string PersonalDirectory { get; set; }
        public string WorkspaceName { get; set; } = "Files";

        public string DBName { get; set; } = "DB";

        public string WorkspaceDirectory
        {
            get
            {
                return System.IO.Path.Combine(PersonalDirectory, WorkspaceName);
            }
        }

        public string DatabaseDirectory
        {
            get
            {
                return System.IO.Path.Combine(PersonalDirectory, DBName);
            }
        }

        public FileManager()
        {
            PersonalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public void TryRecreateWorkspace()
        {
            if (!System.IO.Directory.Exists(WorkspaceDirectory))
            {
                System.IO.Directory.CreateDirectory(WorkspaceDirectory);
            }

            if (!System.IO.Directory.Exists(DatabaseDirectory))
            {
                System.IO.Directory.CreateDirectory(DatabaseDirectory);
            }
        }

        public void TryRecreateWorkspace(string subDirectoryName)
        {
            if (!System.IO.Directory.Exists(WorkspaceDirectory))
            {
                System.IO.Directory.CreateDirectory(WorkspaceDirectory);
            }

            if (!System.IO.Directory.Exists(DatabaseDirectory))
            {
                System.IO.Directory.CreateDirectory(DatabaseDirectory);
            }

            if (!string.IsNullOrEmpty(subDirectoryName))
            {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(WorkspaceDirectory, subDirectoryName)))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(WorkspaceDirectory, subDirectoryName));
                }
            }
        }

        public void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}