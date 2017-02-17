using System;
using System.Xml;
using System.Text;
using System.Web;
using System.Collections.Generic;

namespace NFinal.Lib.Oauth
{
    
    public class oauth_helper
    {
        private static Dictionary<string, oauth_config> configs = null;
        //public static void LoadConfig()
        //{
        //    if (configs == null)
        //    {
        //        ///<summery>网站基地址</summery>
        //        const string HomeUrl = "http://www.lezhaiquan.cn";
        //        configs = new Dictionary<string, oauth_config>();
        //        if (Config.ConfigurationManager.oauth2DataList.Count > 0)
        //        {
        //            foreach (Config.Oauth2Data oauth in Config.ConfigurationManager.oauth2DataList.Values)
        //            {
        //                configs.Add(oauth.name,new oauth_config() { oauth_name=oauth.name,oauth_app_id=oauth.id,oauth_app_key=oauth.key,return_uri=oauth.return_url});
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        /// <param name="oauth_name"></param>
        public static oauth_config get_config(string type)
        {
            //LoadConfig();
            if (configs.ContainsKey(type))
            {
                return configs[type];
            }
            return null;
        }
    }
}
