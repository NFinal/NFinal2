using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
using NFinal.Http;

namespace NFinalServerSample
{
    /// <summary>
    /// 控制器的父类必须是泛型
    /// </summary>
    /// <typeparam name="TMasterPage">母页模板数据</typeparam>
    public class BaseController:NFinal.OwinAction
    {
        public void UpdateA<TModel>(TModel model)
        {

        }
        /// <summary>
        /// 此字段加上ViewBagMember属性将会自动添加到ViewBag中。
        /// </summary>
        [ViewBagMember]
        [Newtonsoft.Json.JsonIgnore]
        public static string imageServerUrl = "";
        public static void Configure(NFinal.Config.Plug.PlugConfig plugConfig)
        {
            Dictionary<string, StringContainer> systemConfigDictionary = new Dictionary<string, StringContainer>();
            systemConfigDictionary.Add("siteName", "站点名称");
            systemConfigDictionary.Add("mobile","联系电话");
            plugConfig.keyValueCache = new NFinal.Collections.FastSearch.FastSearch<StringContainer>(systemConfigDictionary);
            systemConfigDictionary.Clear();
        }
    }
}
