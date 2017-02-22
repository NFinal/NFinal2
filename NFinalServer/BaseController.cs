using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer
{
    public class BaseController<TMasterPage>:NFinal.OwinAction<TMasterPage,Code.User> where TMasterPage :NFinal.MasterPageModel
    {
        public override bool Before()
        {
            if (systemConfig == null)
            {
                Dictionary<string, StringContainer> systemConfigDictionary = new Dictionary<string, StringContainer>();
                systemConfigDictionary.Add("siteName", "站点名称");
                systemConfigDictionary.Add("mobile","联系电话");
                BaseController<TMasterPage>.systemConfig = new NFinal.Collections.FastDictionary<StringContainer>(systemConfigDictionary, systemConfigDictionary.Count);
                systemConfigDictionary.Clear();
            }
            return base.Before();
        }
    }
}
