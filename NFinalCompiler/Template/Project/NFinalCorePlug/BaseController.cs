using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NFinal;
using NFinal.Http;

namespace $safeprojectname$
{
    /// <summary>
    /// 控制器
    /// </summary>
    public class BaseController : NFinal.CoreAction
    {
        public override IDbConnection GetDbConnection()
        {
            System.Data.IDbConnection con = new System.Data.SqlClient.SqlConnection(this.config.connectionStrings["Common"].connectionString);
            return con;
        }
        public static void Configure(NFinal.Config.Plug.PlugConfig plugConfig)
        {
            //systemConfig通常用于全局缓存。
            if (plugConfig.keyValueCache == null)
            {
                Dictionary<string, StringContainer> systemConfigDictionary = new Dictionary<string, StringContainer>();
                systemConfigDictionary.Add("siteName", "站点名称");
                systemConfigDictionary.Add("mobile", "联系电话");
                plugConfig.keyValueCache = new NFinal.Collections.FastSearch.FastSearch<StringContainer>(systemConfigDictionary);
                systemConfigDictionary.Clear();
            }
        }
    }
}