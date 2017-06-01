//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : CompressMode.cs
//        Description :Http压缩模式
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

namespace NFinal.Http
{
    /// <summary>
    /// Http压缩模式
    /// </summary>
    public enum CompressMode
    {
        /// <summary>
        /// 不压缩
        /// </summary>
        None,
        /// <summary>
        /// GZip压缩
        /// </summary>
        GZip,
        /// <summary>
        /// Deflate压缩
        /// </summary>
        Deflate
    }
}