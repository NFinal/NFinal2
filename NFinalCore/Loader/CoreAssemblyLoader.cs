using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
#if NETCORE
using Microsoft.Extensions.PlatformAbstractions;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
#endif
namespace NFinal.Loader
{
#if NETCORE
    public class AssemblyLoader : AssemblyLoadContext, IAssemblyLoader
    {
        public static Dictionary<string, System.Reflection.Assembly> _assemblyDictionary = null;
        public Dictionary<string, System.Reflection.Assembly> assemblyDictionary
        {
            get
            {
                return _assemblyDictionary;
            }
        }
        public void LoadAll(string[] assemblyFileNames)
        {
            if (_assemblyDictionary == null)
            {
                _assemblyDictionary = new Dictionary<string, System.Reflection.Assembly>();
            }
            foreach (var assemblyFileName in assemblyFileNames)
            {
                if (!_assemblyDictionary.ContainsKey(assemblyFileName))
                {
                    assemblyDictionary.Add(assemblyFileName, LoadFromAssemblyPath(assemblyFileName));
                }
            }
        }
        public void Load(string assemblyFileName)
        {
            if (_assemblyDictionary == null)
            {
                _assemblyDictionary = new Dictionary<string, System.Reflection.Assembly>();
            }
            if (!_assemblyDictionary.ContainsKey(assemblyFileName))
            {
                _assemblyDictionary.Add(assemblyFileName, LoadFromAssemblyPath(assemblyFileName));
            }
        }
        //public static void Main(string[] args)
        //{
        //    var asl = new SimpleAssemblyLoader();
        //    var asm = asl.LoadFromAssemblyPath(@"C:\Location\Of\" + "SampleClassLib.dll");

        //    var type = asm.GetType("MyClassLib.SampleClasses.Sample");
        //    dynamic obj = Activator.CreateInstance(type);
        //    Console.WriteLine(obj.SayHello("John Doe"));
        //}
        // Not exactly sure about this
        protected override Assembly Load(AssemblyName assemblyName)
        {
            //var deps = DependencyContext.Default;
            //var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            //var assembly = Assembly.Load(new AssemblyName(res.First().Name));
            var assembly = Assembly.Load(assemblyName);
            return assembly;
        }
    }
#endif
    //public class AssemblyLoader : AssemblyLoadContext
    //{
    //    private string folderPath;

    //    public AssemblyLoader(string folderPath)
    //    {
    //        this.folderPath = folderPath;
    //    }

    //    protected override Assembly Load(AssemblyName assemblyName)
    //    {
    //        var deps = DependencyContext.Default;
    //        var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
    //        if (res.Count > 0)
    //        {
    //            return Assembly.Load(new AssemblyName(res.First().Name));
    //        }
    //        else
    //        {
    //            var apiApplicationFileInfo = new FileInfo($"{folderPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");
    //            if (File.Exists(apiApplicationFileInfo.FullName))
    //            {
    //                var asl = new AssemblyLoader(apiApplicationFileInfo.DirectoryName);
    //                return asl.LoadFromAssemblyPath(apiApplicationFileInfo.FullName);
    //            }
    //        }
    //        return Assembly.Load(assemblyName);
    //    }
    //}
}
