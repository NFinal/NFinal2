using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.Compile
{
    /// <summary>
    /// 重写配置实体类
    /// </summary>
    public struct ReWriteData
    {
        public List<NFinal.Compile.RewriteDirectory> rewriteDirectoryList;
        public List<NFinal.Compile.RewriteFile> rewriteFileList;
        public void AddReWriteData(ReWriteData rewriteData)
        {
            this.rewriteDirectoryList.AddRange(rewriteData.rewriteDirectoryList);
            this.rewriteFileList.AddRange(rewriteData.rewriteFileList);
        }
    }
    /// <summary>
    /// 数据库连接字符串配置类
    /// </summary>
    public class ConnectionStringSettings
    {
        public string Name;
        public string ConnectionString;
        public string ProviderName;
        public ConnectionStringSettings()
        { }
        public ConnectionStringSettings(string name, string connectionString)
        {
            this.Name = name;
        }
        public ConnectionStringSettings(string name, string connectionString, string providerName)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }
    }
    /// <summary>
    /// 数据库连接字符串集合类
    /// </summary>
    public class ConnectionStringSettingsCollection : Dictionary<string, ConnectionStringSettings>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ConnectionStringSettingsCollection()
        {

        }
        /// <summary>
        /// 添加新的连接字符串
        /// </summary>
        /// <param name="settings"></param>
        public void Add(ConnectionStringSettings settings)
        {
            this.Add(settings.Name, settings);
        }
    }
    /// <summary>
    /// 配置管理类
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectionStrings = null;
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="appName">模块名</param>
        /// <param name="root">项目根目录</param>
        /// <returns></returns>
        public static Config GetConfig(string appName, string root)
        {
            string configFileName = Frame.MapPath(appName + "/config.json");
            return GetConfig(appName, configFileName, root);
        }
        /// <summary>
        /// 加载全局的数据库配置
        /// </summary>
        /// <param name="appRoot">项目根目录</param>
        public static void Load(string appRoot)
        {
            if (ConnectionStrings == null)
            {
                string[] dirs = Directory.GetDirectories(appRoot);
                ConnectionStrings = new ConnectionStringSettingsCollection();
                ConnectionStringSettings setting = null;
                if (dirs.Length > 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        string app = Path.GetFileName(dirs[i]);
                        string configFileName = Path.Combine(dirs[i], "config.json");
                        if (File.Exists(configFileName))
                        {
                            Config config = GetConfig(app, configFileName, appRoot);
                            for (int j = 0; j < config.connectionStrings.Count; j++)
                            {
                                DB.ConnectionString conStr = config.connectionStrings[j];

                                if (!ConnectionStrings.ContainsKey(conStr.name))
                                {
                                    setting = new ConnectionStringSettings();
                                    setting.Name = conStr.name;
                                    setting.ConnectionString = conStr.value;
                                    setting.ProviderName = conStr.provider;
                                    ConnectionStrings.Add(setting);
                                }
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取配置文件中所有的重写配置
        /// </summary>
        /// <returns></returns>
        public static ReWriteData GetReWriteData()
        {
            string[] dirs = Directory.GetDirectories(Frame.MapPath("/"));
            string configJsonFileName = string.Empty;
            System.Collections.Generic.List<string> apps = new System.Collections.Generic.List<string>();
            ReWriteData rewriteData = new ReWriteData();
            //if (rewriteData.rewriteDirectoryList == null)
            {
                rewriteData.rewriteDirectoryList = new List<NFinal.Compile.RewriteDirectory>();
            }
            //if (rewriteData.rewriteFileList == null)
            {
                rewriteData.rewriteFileList = new List<NFinal.Compile.RewriteFile>();
            }
            for (int k = 0; k < dirs.Length; k++)
            {
                configJsonFileName = System.IO.Path.Combine(dirs[k], "config.json");
                if (File.Exists(configJsonFileName))
                {
                    StreamReader sr = new StreamReader(configJsonFileName, System.Text.Encoding.UTF8);
                    string json = sr.ReadToEnd();
                    LitJson.JsonData configData = LitJson.JsonMapper.ToObject(json);
                    LitJson.JsonData rewriteDirectory = configData["rewriteDirectory"];
                    NFinal.Compile.RewriteDirectory rd;
                    rewriteData.rewriteDirectoryList.Clear();
                    for (int i = 0; i < rewriteDirectory.Count; i++)
                    {
                        rd = new NFinal.Compile.RewriteDirectory();
                        rd.from = rewriteDirectory[i]["from"].ToString();
                        rd.to = rewriteDirectory[i]["to"].ToString();
                        rewriteData.rewriteDirectoryList.Add(rd);
                    }
                    LitJson.JsonData rewriteFile = configData["rewriteFile"];
                    NFinal.Compile.RewriteFile rf;
                    rewriteData.rewriteFileList.Clear();
                    for (int i = 0; i < rewriteFile.Count; i++)
                    {
                        rf = new NFinal.Compile.RewriteFile();
                        rf.from = rewriteFile[i]["from"].ToString();
                        rf.to = rewriteFile[i]["to"].ToString();
                        rewriteData.rewriteFileList.Add(rf);
                    }
                }
            }
            return rewriteData;
        }
        /// <summary>
        /// 模块配置初始化
        /// </summary>
        /// <param name="appName">模块名</param>
        /// <param name="configFileName">配置文件中径</param>
        /// <param name="root">项目根目录</param>
        /// <returns></returns>
        public static Config GetConfig(string appName, string configFileName, string root)
        {
            Config config = new Config();
            if (File.Exists(configFileName))
            {
                StreamReader sr = new StreamReader(configFileName, System.Text.Encoding.UTF8);
                string json = sr.ReadToEnd();
                sr.Dispose();
                LitJson.JsonData configData = LitJson.JsonMapper.ToObject(json);
                config.defaultStyle = configData["defaultStyle"].ToString();
                int.TryParse(configData["urlMode"].ToString(), out config.UrlMode);
                config.connectionStrings = new System.Collections.Generic.List<DB.ConnectionString>();
                LitJson.JsonData connectionStringArray = configData["connectionStrings"];
                bool.TryParse(configData["compressHTML"].ToString(), out config.CompressHTML);
                config.cookiePrefix = configData["cookiePrefix"].ToString();
                config.sessionPrefix = configData["sessionPrefix"].ToString();
                config.urlPrefix = configData["urlPrefix"].ToString();
                config.urlExtension = configData["urlExtension"].ToString();
                DB.ConnectionString conStr = null;

                for (int i = 0; i < connectionStringArray.Count; i++)
                {
                    conStr = new DB.ConnectionString();
                    conStr.name = connectionStringArray[i]["name"].ToString();
                    conStr.provider = connectionStringArray[i]["providerName"].ToString();
                    conStr.value = connectionStringArray[i]["connectionString"].ToString();
                    //修正conStr.value|ModuleDataDirectory|,|DataDirectory|
                    if (conStr.value.IndexOf("|ModuleDataDirectory|") > -1)
                    {
                        conStr.value = conStr.value.Replace("|ModuleDataDirectory|", root + appName + "\\Data\\");
                    }
                    if (conStr.value.IndexOf("|DataDirectory|") > -1)
                    {
                        conStr.value = conStr.value.Replace("|DataDirectory|", root + "App_Data\\");
                    }
                    config.connectionStrings.Add(conStr);
                }
                LitJson.JsonData rewriteDirectory = configData["rewriteDirectory"];
                NFinal.Compile.RewriteDirectory rd;
                config.rewriteDirectoryList.Clear();
                for (int i = 0; i < rewriteDirectory.Count; i++)
                {
                    rd = new NFinal.Compile.RewriteDirectory();
                    rd.from = rewriteDirectory[i]["from"].ToString();
                    rd.to = rewriteDirectory[i]["to"].ToString();
                    config.rewriteDirectoryList.Add(rd);
                }
                config.rewriteFileList.Clear();
                LitJson.JsonData rewriteFile = configData["rewriteFile"];
                NFinal.Compile.RewriteFile rf;
                for (int i = 0; i < rewriteFile.Count; i++)
                {
                    rf = new NFinal.Compile.RewriteFile();
                    rf.from = rewriteFile[i]["from"].ToString();
                    rf.to = rewriteFile[i]["to"].ToString();
                    config.rewriteFileList.Add(rf);
                }
                LitJson.JsonData redisConfig = configData["redis"];
                bool.TryParse(redisConfig["autoStart"].ToString(), out config.redisConfigAutoStart);
                int.TryParse(redisConfig["maxReadPoolSize"].ToString(), out config.redisConfigMaxReadPoolSize);
                int.TryParse(redisConfig["maxWritePoolSize"].ToString(), out config.redisConfigMaxWritePoolSize);
                LitJson.JsonData readWriteHosts = redisConfig["readWriteHosts"];
                if (readWriteHosts == null || readWriteHosts.Count < 1)
                {
                    config.redisReadWriteHosts = null;
                }
                else
                {
                    config.redisReadWriteHosts = new string[readWriteHosts.Count];
                }
                for (int i = 0; i < readWriteHosts.Count; i++)
                {
                    config.redisReadWriteHosts[i] = readWriteHosts[i].ToString();
                }
                LitJson.JsonData readOnlyHosts = redisConfig["readOnlyHosts"];
                if (readOnlyHosts == null || readOnlyHosts.Count < 1)
                {
                    config.redisReadOnlyHosts = null;
                }
                else
                {
                    config.redisReadOnlyHosts = new string[readWriteHosts.Count];
                }
                for (int i = 0; i < readOnlyHosts.Count; i++)
                {
                    config.redisReadOnlyHosts[i] = readOnlyHosts[i].ToString();
                }
                //if (NFinal.Cache.clientManager == null)
                //{
                //    ServiceStack.Redis.RedisClientManagerConfig redisClientManagerConfig = new ServiceStack.Redis.RedisClientManagerConfig();
                //    redisClientManagerConfig.AutoStart = config.redisConfigAutoStart;
                //    redisClientManagerConfig.MaxReadPoolSize = config.redisConfigMaxReadPoolSize;
                //    redisClientManagerConfig.MaxWritePoolSize = config.redisConfigMaxWritePoolSize;
                //    NFinal.Cache.clientManager = new ServiceStack.Redis.PooledRedisClientManager(config.redisReadWriteHosts, config.redisReadOnlyHosts, redisClientManagerConfig);
                //}
            }

            if (string.IsNullOrEmpty(appName))
            {
                config.APP = "/";
            }
            else
            {
                config.APP = "/" + appName.Trim(new char[] { '/' }) + "/";
            }
            config.Controller = config.APP + config.Controller;
            config.Code = config.APP + config.Code;
            config.Views = config.APP + config.Views;
            config.Models = config.APP + config.Models;
            config.BLL = config.Models + config.BLL;
            config.DAL = config.Models + config.DAL;
            config.Web = config.APP + config.Web;
            config.Content = config.APP + config.Content;
            config.ContentCss = config.Content + config.ContentCss;
            config.ContentJS = config.Content + config.ContentJS;
            config.ContentImages = config.Content + config.ContentImages;
            return config;
        }
    }
}