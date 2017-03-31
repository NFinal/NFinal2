using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.Plugs
{
    
    public class PlugManager
    {
        public static List<NFinal.Plugs.PlugInfo> plugInfoList =null;
        public static bool isInit = false;
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
                foreach (var plug in NFinal.Config.Configration.globalConfig.plugs)
                {
                    loadSuccess = false;
                    assemblyFilePath =NFinal.IO.Path.GetApplicationPath(plug.assemblyPath);
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
                        loadSuccess = false;
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
                    plugInfo.config= NFinal.Config.Configration.plugConfigDictionary[plug.name];
                    plugInfo.assemblyPath = plug.assemblyPath;
                    plugInfo.configPath = plug.configPath;
                    plugInfo.description = plug.description;
                    plugInfo.enable = plug.enable;
                    plugInfo.name = plug.name;
                    plugInfo.urlPrefix = plug.urlPrefix;
                    plugInfoList.Add(plugInfo);
                }
            }
        }
    }
}
