using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;
using LOGI.Framework.Toolkit.Core.Common.Files;

namespace LOGI.Framework.Toolkit.Core.Reflection
{
    public class AssemblyLoader
    {
        public static List<Assembly> LoadAssemblies(string path)
        {
            return LoadAssemblies(FileLoader.GetDirectoryFiles(path));
        }

        public static List<Assembly> LoadAssemblies(string path, Func<FileInfo, bool> predicate)
        {
            return LoadAssemblies(FileLoader.GetDirectoryFiles(path,predicate));
        }

        public static List<Assembly> LoadAssemblies( IEnumerable<string> assemblies)
        {
            var loadedAssemblies=new List<Assembly>();

            var alreadyLoadedAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();

            foreach (var str in assemblies)
            {
                if (alreadyLoadedAssemblies != null && alreadyLoadedAssemblies.Count() > 0)
                {
                    string file = str;
                    if (alreadyLoadedAssemblies.Where(x => x.FullName == file).FirstOrDefault() != null)
                    {
                        continue;
                    }
                }
                loadedAssemblies.Add(Assembly.LoadFrom(str));
            }

            return loadedAssemblies;
        }

        public static List<T> GetPlugins<T>(string folder)
        {

            string[] files = Directory.GetFiles(folder, "*.dll");

            List<T> tList = new List<T>();
            
            foreach (string file in files)
            {

                try
                {

                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {

                        if (!type.IsClass || type.IsNotPublic) continue;

                        Type[] interfaces = type.GetInterfaces();

                        if (((IList)interfaces).Contains(typeof(T)))
                        {
                            object obj = Activator.CreateInstance(type);

                            T t = (T)obj;

                            tList.Add(t);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //LogError(ex);
                }

            }

            return tList;

        }
    }
}
