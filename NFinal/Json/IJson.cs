//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Writer.cs
//        Description :输出类
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

namespace NFinal
{
    /// <summary>
    /// JSON接口类，必须实现WriteJson接口
    /// </summary>
    public interface IJson
    {
        /// <summary>
        /// 写Json
        /// </summary>
        /// <param name="tw"></param>
        void WriteJson(NFinal.IO.IWriter tw);
    }
}
