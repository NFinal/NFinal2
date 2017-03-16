using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Plugs.Loader
{
    public interface IAssemblyLoader
    {
        Dictionary<string, System.Reflection.Assembly> assemblyDictionary
        {
            get;
        }
        void LoadAll(string[] assemblyFileNames);
        void Load(string assemblyFileName);
    }
}
