using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NFinal;
using NFinal.Http;

namespace NFinalCorePlug
{
    /// <summary>
    /// 控制器的父类必须是泛型
    /// </summary>
    /// <typeparam name="TMasterPage">母页模板数据</typeparam>
    //[ActionExport("UpdateA",typeof(object))]
    public class BaseController:NFinal.CoreAction
    {
        public void UpdateA(int a,string b)
        {
            this.ViewBag.a = 1;
            //this.ViewBag.a = model;
        }
        public override IDbConnection GetDbConnection()
        {
            System.Data.IDbConnection con=new System.Data.SqlClient.SqlConnection(this.config.connectionStrings["Common"].connectionString);
            return con;
        }
        /// <summary>
        /// 此字段加上ViewBagMember属性将会自动添加到ViewBag中。
        /// </summary>
        [ViewBagMember]
        [Newtonsoft.Json.JsonIgnore]
        public static string imageServerUrl = "";
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
        public override bool Before()
        {
            
            return true;
        }
    }
}
