using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NFinal.Config
{
    public class Configration
    {
        public static bool isInit=false;
        public static IDictionary<string,NFinal.Config.Plug.PlugConfig> plugConfigDictionary = null;
        public static NFinal.Config.Global.GlobalConfig globalConfig = null;
        public static string DeleteComment(string source)
        {
            System.Text.RegularExpressions.Regex commentRegex =
                            new System.Text.RegularExpressions.Regex("//[^\r\n]*");
            source = commentRegex.Replace(source, string.Empty);
            return source;
        }
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
                    Configration.globalConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<NFinal.Config.Global.GlobalConfig>(nfinalJsonText);
                    Configration.globalConfig.JsonObject = SimpleJSON.JSON.Parse(nfinalJsonText).AsObject;
                    NFinal.Config.Plug.PlugConfig plugConfig = null;
                    for (int i = 0; i < Configration.globalConfig.plugs.Length; i++)
                    {
                        if (globalConfig.plugs[i].enable)
                        {
                            string plugConfigPath = NFinal.IO.Path.GetApplicationPath(globalConfig.plugs[i].configPath);
                            if (File.Exists(plugConfigPath))
                            {
                                using (StreamReader streamReader = System.IO.File.OpenText(plugConfigPath))
                                {
                                    string plugJsonText = streamReader.ReadToEnd();
                                    streamReader.Dispose();
                                    plugJsonText = DeleteComment(plugJsonText);
                                    plugConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<NFinal.Config.Plug.PlugConfig>(plugJsonText);
                                    plugConfig.JsonObject = SimpleJSON.JSON.Parse(nfinalJsonText).AsObject;
                                    plugConfigDictionary.Add(globalConfig.plugs[i].name,plugConfig);
                                }
                            }
                            else
                            {
                                throw new FileNotFoundException("找不到NFinal插件配置文件："
                                    + plugConfigPath,
                                    plugConfigPath);
                            }
                        }
                    }

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
