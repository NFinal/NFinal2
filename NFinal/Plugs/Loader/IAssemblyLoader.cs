//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IAssemblyLoader.cs
//        Description :程序集加载接口
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
    /// 程序集加载接口
    /// </summary>
    public interface IAssemblyLoader
    {
        Dictionary<string, System.Reflection.Assembly> assemblyDictionary
        {
            get;
        }
        /// <summary>
        /// 加载所有的程序集
        /// </summary>
        /// <param name="assemblyFileNames"></param>
        void LoadAll(string[] assemblyFileNames);
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblyFileName"></param>
        void Load(string assemblyFileName);
    }
}
