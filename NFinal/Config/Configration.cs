using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;

namespace NFinal.Config
{
    public class Configration
    {
        public static bool isInit=false;
        public static IDictionary<string, CommonConfig> configDic = null;
        public static T LoadConfig<T>(string folderSpace,string fileName)
        {
            string filePath = folderSpace.Replace('.', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + fileName;
            var configReader = System.IO.File.OpenText(filePath);
            var configJson = configReader.ReadToEnd();
            configReader.Dispose();
            T config = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(configJson);
            return config;
        }
        public static NFinal.Config.CommonConfig LoadCommonConfig(string folderSpace,string fileName)
        {
            string filePath = folderSpace.Replace('.', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + fileName;
            var configReader = System.IO.File.OpenText(filePath);
            var configJson = configReader.ReadToEnd();
            configReader.Dispose();
            CommonConfig config = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonConfig>(configJson);
            return config;
        }
        public static void Init(Middleware.MiddlewareConfigOptions options)
        {
            Configration.isInit = true;
            Assembly assembly = null;
            Module[] modules = null;
            MethodInfo methodInfo = null;
            Type[] types = null;
            configDic = new Dictionary<string, CommonConfig>(StringComparer.Ordinal);
            CommonConfig commonConfig;
            NFinal.Loader.IAssemblyLoader assemblyLoader = new NFinal.Loader.AssemblyLoader();
            for (int i = 0; i < options.plugs.Length; i++)
            {
                assemblyLoader.Load(options.plugs[i].filePath);
                assembly = assemblyLoader.assemblyDictionary[options.plugs[i].filePath];
                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    types = modules[j].GetTypes();
                    object[] attributes= types[j].GetCustomAttributes(typeof(ConfigAttribute),true);
                    if (attributes.Length > 0)
                    {
                        methodInfo= types[j].GetMethod("Init", BindingFlags.Static | BindingFlags.Public);
                        //执行配置读取函数
                        methodInfo.Invoke(null,null);
                        commonConfig =(CommonConfig)types[j].GetField("Common").GetValue(null);
                        configDic.Add(assembly.FullName, commonConfig);
                    }
                }
            }
        }
    }
}
