using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;

namespace NFinal.Config
{
    /// <summary>
    /// nfinal项目配置管理类
    /// </summary>
    public class ConfigurationManager
    {
        public static NFinal.Config.ConnectionStringSettingsCollection ConnectionStrings = null;
        //public static Dictionary<string, NFinal.Config.RedisData> RedisConfigs = null;
        //public static bool? debug=null;
        public static Dictionary<string,NFinal.Config.config> configDic = null;
        //public static Dictionary<string, Oauth2Data> oauth2DataList = null;
        //public static AlipayPcData alipayPc = null;
        //public static TenpayPcData tenpayPc = null;
        public static config config;
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="appName">模块名</param>
        /// <param name="root">项目根目录</param>
        /// <returns></returns>
        public static config GetConfig(string appName,string root)
        {
            string configFileName =System.IO.Path.Combine (root,"config.json");

            Json.IJsonSerialize serialize =new Json.NewtonsoftJsonSerialize();
            config temp = null;
            if (File.Exists(configFileName))
            {
                 temp= serialize.DeserializeObject<config>(File.OpenText(configFileName).ReadToEnd());
            }
            return temp;
            //return GetConfig(appName, configFileName,root);
        }

        /// <summary>
        /// 获取全局配置
        /// </summary>
        /// <param name="appRoot">项目根目录</param>
        public static void Load(string appRoot)
        {
            if (ConfigurationManager.configDic == null)
            {
                //debug
                //if (ConfigurationManager.debug == null)
                //{
                //    string webConfig = Path.Combine(appRoot, "Web.config");
                //    XmlDocument doc = new XmlDocument();
                //    if (File.Exists(webConfig))
                //    {
                //        doc.Load(webConfig);
                //        XmlNode compilationNode = doc.DocumentElement.SelectSingleNode("configuration/system.web/compilation[@debug]");
                //        if (compilationNode != null)
                //        {
                //            ConfigurationManager.debug = compilationNode.Attributes["debug"].Value == "true";
                //        }
                //        else
                //        {
                //            ConfigurationManager.debug = true;
                //        }
                //    }
                //    else
                //    {
                //        ConfigurationManager.debug = true;
                //    }
                //}
                //连接字符串
                if (ConfigurationManager.ConnectionStrings == null)
                {
                    string[] dirs = Directory.GetDirectories(appRoot);
                    ConfigurationManager.ConnectionStrings = new NFinal.Config.ConnectionStringSettingsCollection();
                    ConfigurationManager.configDic = new Dictionary<string, config>();
                    NFinal.Config.ConnectionStringSettings setting = null;
                    //if (ConfigurationManager.RedisConfigs != null)
                    //{
                    //    ConfigurationManager.RedisConfigs.Clear();
                    //}
                    //else
                    //{
                    //    ConfigurationManager.RedisConfigs = new Dictionary<string, RedisData>();
                    //}
                    if (dirs.Length > 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            string app = Path.GetFileName(dirs[i]);
                            string configFileName = Path.Combine(dirs[i], "config.json");
                            if (File.Exists(configFileName))
                            {
                                Json.IJsonSerialize serialize = new Json.NewtonsoftJsonSerialize();
                                string text = File.OpenText(configFileName).ReadToEnd();
                                serialize.DeserializeObject<config>(File.OpenText(configFileName).ReadToEnd());
                                //Config config = GetConfig(app, configFileName, appRoot);
                                //ConfigurationManager.JsonDataList.Add(app, config);
                                for (int j = 0; j < config.connectionStrings.Count; j++)
                                {
                                    ConnectionString conStr = config.connectionStrings[j];

                                    if (!ConfigurationManager.ConnectionStrings.ContainsKey(conStr.name))
                                    {
                                        setting = new ConnectionStringSettings();
                                        setting.Name = conStr.name;
                                        setting.ConnectionString = conStr.connectionString;
                                        setting.ProviderName = conStr.providerName;
                                        ConfigurationManager.ConnectionStrings.Add(setting);
                                    }
                                }
                                //RedisData redisData = new RedisData();
                                //redisData.redisConfigAutoStart = config.redisConfigAutoStart;
                                //redisData.redisConfigMaxReadPoolSize = config.redisConfigMaxReadPoolSize;
                                //redisData.redisConfigMaxWritePoolSize = config.redisConfigMaxWritePoolSize;
                                //redisData.redisReadOnlyHosts = config.redisReadOnlyHosts;
                                //redisData.redisReadWriteHosts = config.redisReadWriteHosts;
                                //ConfigurationManager.RedisConfigs.Add(app, redisData);
                            }
                        }
                    }
                }
            }   
        }
        ///// <summary>
        ///// 获取模块配置并初始化
        ///// </summary>
        ///// <param name="appName">模块名</param>
        ///// <param name="configFileName">配置文件路径</param>
        ///// <param name="root">项目根目录</param>
        ///// <returns></returns>
        //public static Config GetConfig(string appName, string configFileName,string root)
        //{
        //    Config config = new Config();
        //    if (File.Exists(configFileName))
        //    {
        //        StreamReader sr = new StreamReader(configFileName,System.Text.Encoding.UTF8);
        //        string json = sr.ReadToEnd();
        //        sr.Dispose();
        //        //json= NFinal.JsonConfig.DeleteComment(json);
        //        config.JsonData = SimpleJson.DeserializeObject(json) as JsonObject;
        //        config.defaultStyle = config.JsonData["defaultStyle"].ToString();
        //        int.TryParse(config.JsonData["urlMode"].ToString(), out config.UrlMode);
        //        config.connectionStrings = new System.Collections.Generic.List<ConnectionString>();
                
        //        bool.TryParse(config.JsonData["compressHTML"].ToString(), out config.CompressHTML);
        //        config.cookiePrefix = config.JsonData["cookiePrefix"].ToString();
        //        config.sessionPrefix = config.JsonData["sessionPrefix"].ToString();
        //        config.urlPrefix = config.JsonData["urlPrefix"].ToString();
        //        config.urlExtension = config.JsonData["urlExtension"].ToString();
        //        config.version = config.JsonData["version"].ToString();
        //        bool.TryParse(config.JsonData["autoVersion"].ToString(), out config.autoVersion);
        //        ConnectionString conStr = null;
        //        if (config.JsonData.ContainsKey("connectionStrings"))
        //        {
        //            JsonArray connectionStringArray = config.JsonData["connectionStrings"] as JsonArray;
        //            JsonObject connectionStringObject = null;
        //            for (int i = 0; i < connectionStringArray.Count; i++)
        //            {
        //                connectionStringObject = connectionStringArray[i] as JsonObject;
        //                conStr = new ConnectionString();
        //                conStr.name = connectionStringObject["name"].ToString();
        //                conStr.provider = connectionStringObject["providerName"].ToString();
        //                conStr.value = connectionStringObject["connectionString"].ToString();
        //                //修正conStr.value|ModuleDataDirectory|,|DataDirectory|
        //                if (conStr.value.IndexOf("|ModuleDataDirectory|") > -1)
        //                {
        //                    conStr.value = conStr.value.Replace("|ModuleDataDirectory|", root + appName + "\\Data\\");
        //                }
        //                if (conStr.value.IndexOf("|DataDirectory|") > -1)
        //                {
        //                    conStr.value = conStr.value.Replace("|DataDirectory|", root + "App_Data\\");
        //                }
        //                config.connectionStrings.Add(conStr);
        //            }
        //        }
        //        if (config.JsonData.ContainsKey("rewriteDirectory"))
        //        {
        //            JsonArray rewriteDirectory = config.JsonData["rewriteDirectory"] as JsonArray;
        //            JsonObject rewriteDirectoryObject = null;
        //            config.rewriteDirectoryList.Clear();
        //            RewriteDirectory rd;
        //            for (int i = 0; i < rewriteDirectory.Count; i++)
        //            {
        //                rewriteDirectoryObject = rewriteDirectory[i] as JsonObject;
        //                rd = new RewriteDirectory();
        //                rd.from = rewriteDirectoryObject["from"].ToString();
        //                rd.to = rewriteDirectoryObject["to"].ToString();
        //                config.rewriteDirectoryList.Add(rd);
        //            }
        //        }
        //        if (config.JsonData.ContainsKey("rewriteFile"))
        //        {
        //            JsonArray rewriteFile = config.JsonData["rewriteFile"] as JsonArray;
        //            JsonObject rewriteFileObject = null;
        //            RewriteFile rf;
        //            config.rewriteFileList.Clear();
        //            for (int i = 0; i < rewriteFile.Count; i++)
        //            {
        //                rewriteFileObject = rewriteFile[i] as JsonObject;
        //                rf = new RewriteFile();
        //                rf.from = rewriteFileObject["from"].ToString();
        //                rf.to = rewriteFileObject["to"].ToString();
        //                config.rewriteFileList.Add(rf);
        //            }
        //        }
        //        if (config.JsonData.ContainsKey("payments"))
        //        {
        //            JsonObject paymentsObject = config.JsonData["payments"] as JsonObject;
                    
        //            if (paymentsObject.ContainsKey("alipaypc"))
        //            {
        //                JsonObject alipayPcJsonObject = paymentsObject["alipaypc"] as JsonObject;
        //                alipayPc = new AlipayPcData();
        //                alipayPc.partner = alipayPcJsonObject["partner"].ToString();
        //                alipayPc.key = alipayPcJsonObject["key"].ToString();
        //                alipayPc.email = alipayPcJsonObject["email"].ToString();
        //                alipayPc.type = alipayPcJsonObject["type"].ToString();
        //                alipayPc.return_url = alipayPcJsonObject["return_url"].ToString();
        //                alipayPc.notify_url = alipayPcJsonObject["notify_url"].ToString();
        //            }
        //            if (paymentsObject.ContainsKey("tenpaypc"))
        //            {
        //                JsonObject tenpayPcJsonObject = paymentsObject["tenpaypc"] as JsonObject;
        //                tenpayPc = new TenpayPcData();
        //                tenpayPc.partner = tenpayPcJsonObject["partner"].ToString();
        //                tenpayPc.key = tenpayPcJsonObject["key"].ToString();
        //                tenpayPc.type = tenpayPcJsonObject["type"].ToString();
        //                tenpayPc.return_url = tenpayPcJsonObject["return_url"].ToString();
        //                tenpayPc.notify_url = tenpayPcJsonObject["notify_url"].ToString();
        //            }
        //        }
        //        if (config.JsonData.ContainsKey("oauth2"))
        //        {
        //            JsonArray Oauth2 = config.JsonData["oauth2"] as JsonArray;
        //            Oauth2Data oauth2Data = null;
        //            if (ConfigurationManager.oauth2DataList == null)
        //            {
        //                ConfigurationManager.oauth2DataList = new Dictionary<string, Oauth2Data>();
        //            }
        //            JsonObject Oauth2Object = null;
        //            for (int i = 0; i < Oauth2.Count; i++)
        //            {
        //                Oauth2Object = Oauth2[i] as JsonObject;
        //                oauth2Data = new Oauth2Data();
        //                oauth2Data.name = Oauth2Object["name"].ToString();
        //                oauth2Data.id = Oauth2Object["id"].ToString();
        //                oauth2Data.key = Oauth2Object["key"].ToString();
        //                oauth2Data.return_url = Oauth2Object["return_url"].ToString();
        //                if (!ConfigurationManager.oauth2DataList.ContainsKey(oauth2Data.name))
        //                {
        //                    ConfigurationManager.oauth2DataList.Add(oauth2Data.name, oauth2Data);
        //                }
        //            }
        //        }
        //        if (config.JsonData.ContainsKey("redis"))
        //        {
        //            JsonObject redisConfig = config.JsonData["redis"] as JsonObject;
        //            bool.TryParse(redisConfig["autoStart"].ToString(), out config.redisConfigAutoStart);
        //            int.TryParse(redisConfig["maxReadPoolSize"].ToString(), out config.redisConfigMaxReadPoolSize);
        //            int.TryParse(redisConfig["maxWritePoolSize"].ToString(), out config.redisConfigMaxWritePoolSize);

        //            JsonArray readWriteHosts = redisConfig["readWriteHosts"] as JsonArray;
        //            if (readWriteHosts == null || readWriteHosts.Count < 1)
        //            {
        //                config.redisReadWriteHosts = null;
        //            }
        //            else
        //            {
        //                config.redisReadWriteHosts = new string[readWriteHosts.Count];
        //            }
        //            for (int i = 0; i < readWriteHosts.Count; i++)
        //            {
        //                config.redisReadWriteHosts[i] = readWriteHosts[i].ToString();
        //            }
        //            JsonArray readOnlyHosts = redisConfig["readOnlyHosts"] as JsonArray;
        //            if (readOnlyHosts == null || readOnlyHosts.Count < 1)
        //            {
        //                config.redisReadOnlyHosts = null;
        //            }
        //            else
        //            {
        //                config.redisReadOnlyHosts = new string[readWriteHosts.Count];
        //            }
        //            for (int i = 0; i < readOnlyHosts.Count; i++)
        //            {
        //                config.redisReadOnlyHosts[i] = readOnlyHosts[i].ToString();
        //            }
        //        }
        //    }
            
        //    return config;
        //}
    }
}
