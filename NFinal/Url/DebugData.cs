//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DebugData.cs
//        Description :生成调试Html所需的视图数据
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinal.Url
{
    /// <summary>
    /// 生成调试Html所需的视图数据
    /// </summary>
    public class DebugData
    {
        /// <summary>
        /// URL格式化数据
        /// </summary>
        public NFinal.Url.FormatData formatData;
        /// <summary>
        /// 类名
        /// </summary>
        public string classFullName;
        /// <summary>
        /// 方法名
        /// </summary>
        public string methodName;
        /// <summary>
        /// 调试用的url
        /// </summary>
        public string debugUrl;
    }
}
