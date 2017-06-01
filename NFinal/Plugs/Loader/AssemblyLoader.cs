//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : AssemblyLoader.cs
//        Description :基于.net framework的程序集加载类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Plugs.Loader
{
    /// <summary>
    /// 基于.net framework的程序集加载类
    /// </summary>
#if (NET40 || NET451 || NET461)
    public class AssemblyLoader:IAssemblyLoader
    {
        public static Dictionary<string, System.Reflection.Assembly> _assemblyDictionary =null;
        public Dictionary<string, System.Reflection.Assembly> assemblyDictionary { get {
                return _assemblyDictionary;
            } }
        /// <summary>
        /// 加载所有的程序集
        /// </summary>
        /// <param name="assemblyFileNames"></param>
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
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblyFileName"></param>
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
