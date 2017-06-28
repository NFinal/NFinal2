//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Configration.cs
//        Description :NFinal2配置类
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
using System.IO;

namespace NFinal.Config
{
    /// <summary>
    /// NFinal2配置类
    /// </summary>
    public class Configration
    {
        /// <summary>
        /// 依赖注入容量
        /// </summary>
        public static DependencyInjection.IServiceCollection serviceCollection = new DependencyInjection.ServiceCollection();
        /// <summary>
        /// 是否初始化
        /// </summary>
        public static bool isInit=false;
        /// <summary>
        /// 插件配置
        /// </summary>
        public static IDictionary<string,NFinal.Config.Plug.PlugConfig> plugConfigDictionary = null;
        /// <summary>
        /// 全局配置
        /// </summary>
        public static NFinal.Config.Global.GlobalConfig globalConfig = null;
        /// <summary>
        /// 删除Json配置中的注释，以免解释失败
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DeleteComment(string source)
        {
            System.Text.RegularExpressions.Regex commentRegex =
                            new System.Text.RegularExpressions.Regex("(^//[^\r\n]*|[^:]//[^\r\n]*)");
            source = commentRegex.Replace(source, string.Empty);
            return source;
        }
        /// <summary>
        /// 加载配置
        /// </summary>
        public static void Init()
        {
            Configration.isInit = true;
            plugConfigDictionary = new Dictionary<string, NFinal.Config.Plug.PlugConfig>();
            string fileName = Directory.GetCurrentDirectory();
            string nfinalConfigPath= NFinal.IO.Path.GetApplicationPath("/nfinal.json");
            if (File.Exists(nfinalConfigPath))
            {
                using (StreamReader nfinalConfigReader = System.IO.File.OpenText(nfinalConfigPath))
                {
                    string nfinalJsonText = nfinalConfigReader.ReadToEnd();
                    nfinalConfigReader.Dispose();
                    nfinalJsonText = DeleteComment(nfinalJsonText);
                    try
                    {
                        Configration.globalConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<NFinal.Config.Global.GlobalConfig>(nfinalJsonText);
                        Configration.globalConfig.JsonObject = SimpleJSON.JSON.Parse(nfinalJsonText).AsObject;
                    }
                    catch
                    {
                        throw new NFinal.Exceptions.NFinalConfigLoadException(nfinalJsonText);
                    }
                    NFinal.Config.Plug.PlugConfig plugConfig = null;
                    string nfinalPlugFolder = NFinal.IO.Path.GetApplicationPath("/Plugs/");
                    string[] plugJsonFileNameList = Directory.GetFiles(nfinalPlugFolder, "plug.json", SearchOption.AllDirectories);
                    //Configration.globalConfig.plugs = new Plug.Plug[plugJsonFileNameList.Length];
                    for (int i = 0; i < plugJsonFileNameList.Length; i++)
                    {
                        string plugJsonFileName = plugJsonFileNameList[i];
                        if (File.Exists(plugJsonFileName))
                        {
                            string plugJsonText = "";
                            using (StreamReader streamReader = System.IO.File.OpenText(plugJsonFileName))
                            {
                                plugJsonText = streamReader.ReadToEnd();
                            }
                            plugJsonText = DeleteComment(plugJsonText);
                            try
                            {
                                plugConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<NFinal.Config.Plug.PlugConfig>(plugJsonText);
                                plugConfig.JsonObject = SimpleJSON.JSON.Parse(nfinalJsonText).AsObject;
                                plugConfigDictionary.Add(plugConfig.plug.name, plugConfig);
                            }
                            catch
                            {
                                throw new NFinal.Exceptions.PlugConfigLoadException(plugJsonText);
                            }
                        }
                        else
                        {
                            throw new FileNotFoundException("找不到NFinal插件配置文件："
                                + plugJsonFileName,
                                plugJsonFileName);
                        }
                    }
                    //for (int i = 0; i < Configration.globalConfig.plugs.Length; i++)
                    //{
                    //    if (globalConfig.plugs[i].enable)
                    //    {
                    //        string plugConfigPath = NFinal.IO.Path.GetApplicationPath(globalConfig.plugs[i].configPath);
                    //        if (File.Exists(plugConfigPath))
                    //        {
                    //            using (StreamReader streamReader = System.IO.File.OpenText(plugConfigPath))
                    //            {
                    //                string plugJsonText = streamReader.ReadToEnd();
                    //                streamReader.Dispose();
                    //                plugJsonText = DeleteComment(plugJsonText);
                    //                plugConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<NFinal.Config.Plug.PlugConfig>(plugJsonText);
                    //                plugConfig.JsonObject = SimpleJSON.JSON.Parse(nfinalJsonText).AsObject;
                    //                plugConfigDictionary.Add(globalConfig.plugs[i].name,plugConfig);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            throw new FileNotFoundException("找不到NFinal插件配置文件："
                    //                + plugConfigPath,
                    //                plugConfigPath);
                    //        }
                    //    }
                    //}

                }
            }
            else
            {
                throw new FileNotFoundException("找不到NFinal全局配置文件："
                                    + nfinalConfigPath,
                                    nfinalConfigPath);
            }
        }
    }
}
