using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;

namespace NFinalCoreServer
{
    /// <summary>
    /// 控制器的父类必须是泛型
    /// </summary>
    /// <typeparam name="TMasterPage">母页模板数据</typeparam>
    public class BaseController<TMasterPage>:NFinal.CoreAction<TMasterPage,Code.User> where TMasterPage :NFinal.MasterPageModel
    {
        /// <summary>
        /// 此字段加上ViewBagMember属性将会自动添加到ViewBag中。
        /// </summary>
        [ViewBagMember]
        [Newtonsoft.Json.JsonIgnore]
        public static string imageServerUrl = "";
        public override bool Before()
        {
            //systemConfig通常用于全局缓存。
            if (systemConfig == null)
            {
                Dictionary<string, StringContainer> systemConfigDictionary = new Dictionary<string, StringContainer>();
                systemConfigDictionary.Add("siteName", "站点名称");
                systemConfigDictionary.Add("mobile","联系电话");
                BaseController<TMasterPage>.systemConfig = new NFinal.Collections.FastDictionary<StringContainer>(systemConfigDictionary, systemConfigDictionary.Count);
                systemConfigDictionary.Clear();
            }
            return true;
        }
    }
}
