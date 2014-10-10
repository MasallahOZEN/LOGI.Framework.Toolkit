using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LOGI.Framework.Toolkit.Core.Extensions.ExtString;

namespace LOGI.Framework.Toolkit.Core.Common.Files
{
    public class FileLoader
    {
        private static Func<FileInfo, bool> _predicate;
        public static IEnumerable<string> GetDirectoryFiles(string path)
        {
            List<string> files = new List<string>();
            if (!path.IsNullOrEmpty())
            {
                foreach (var filePath in path.Split(';'))
                {
                    if (filePath.IsNullOrEmpty())
                    {
                        continue;
                    }

                    DirectoryInfo directory = new DirectoryInfo(filePath);
                    var directories = directory.GetFiles("*.dll");

                    if (_predicate != null)
                    {
                        files.AddRange(directories.Where(_predicate).Select(file => file.FullName));
                    }
                    else
                    {
                        files.AddRange(directories.Select(file => file.FullName));                        
                    }
                    
                }
            }

            _predicate = null;

            return files;
        }

        public static IEnumerable<string> GetDirectoryFiles(string path, Func<FileInfo, bool> predicate)
        {
            _predicate = predicate;
            return GetDirectoryFiles(path);
        }
    }
}
