//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : PlugManager.cs
//        Description :插件管理
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.Plugs
{
    /// <summary>
    /// 插件管理
    /// </summary>
    public class PlugManager
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public static List<NFinal.Plugs.PlugInfo> plugInfoList =null;
        /// <summary>
        /// 插件是否加载
        /// </summary>
        public static bool isInit = false;
        /// <summary>
        /// 加载插件
        /// </summary>
        public static void Init()
        {
            if (plugInfoList == null)
            {
                if (!NFinal.Config.Configration.isInit)
                {
                    NFinal.Config.Configration.Init();
                }
                plugInfoList = new List<PlugInfo>();
                NFinal.Plugs.Loader.IAssemblyLoader assemblyLoader = new NFinal.Plugs.Loader.AssemblyLoader();
                NFinal.Plugs.PlugInfo plugInfo = null;
                bool loadSuccess = false;
                string assemblyFilePath = null;
                foreach (var plug in NFinal.Config.Configration.plugConfigDictionary)
                {
                    loadSuccess = false;
                    assemblyFilePath =NFinal.IO.Path.GetApplicationPath(plug.Value.plug.assemblyPath);
                    if (File.Exists(assemblyFilePath))
                    {
                        try
                        {
                            assemblyLoader.Load(assemblyFilePath);
                            loadSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            loadSuccess = false;
                            //throw ex;
                        }
                    }
                    else
                    {
                        throw new NFinal.Exceptions.PlugAssemblyNotFoundException(assemblyFilePath);
                    }
                    plugInfo = new PlugInfo();
                    if (loadSuccess)
                    {
                        plugInfo.assembly = assemblyLoader.assemblyDictionary[assemblyFilePath];
                    }
                    else
                    {
                        plugInfo.assembly = null;
                    }
                    plugInfo.loadSuccess = loadSuccess;
                    plugInfo.config= NFinal.Config.Configration.plugConfigDictionary[plug.Value.plug.name];
                    plugInfo.assemblyFullPath = assemblyFilePath;
                    //plugInfo.configPath = plug.configPath;
                    plugInfo.description = plug.Value.plug.description;
                    plugInfo.enable = plug.Value.plug.enable;
                    plugInfo.name = plug.Value.plug.name;
                    plugInfo.urlPrefix = plug.Value.plug.urlPrefix;
                    plugInfoList.Add(plugInfo);
                }
            }
        }
    }
}
