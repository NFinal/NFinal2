using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Loader
{
#if (NET40 || NET451 || NET461)
    public class AssemblyLoader:IAssemblyLoader
    {
        public static Dictionary<string, System.Reflection.Assembly> _assemblyDictionary =null;
        public Dictionary<string, System.Reflection.Assembly> assemblyDictionary { get {
                return _assemblyDictionary;
            } }
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
                    assemblyDictionary.Add(assemblyFileName, System.Reflection.Assembly.LoadFrom(assemblyFileName));
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
                _assemblyDictionary.Add(assemblyFileName, System.Reflection.Assembly.LoadFrom(assemblyFileName));
            }
        }
    }
#endif
}
