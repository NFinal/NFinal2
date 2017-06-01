//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ProjectType.cs
//        Description :项目类型
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

namespace NFinal.Config.Global
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        /// Web应用程序
        /// </summary>
        Web,
        /// <summary>
        /// 控制台应用程序
        /// </summary>
        Console,
        /// <summary>
        /// 类库，即程序集
        /// </summary>
        Library,
        /// <summary>
        /// winform或wpf可视化应用程序
        /// </summary>
        Window
    }
}
