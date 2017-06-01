//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DataBaseNotSupportException.cs
//        Description :数据库不支持异常
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NFinal.Exceptions
{
    /// <summary>
    /// 数据库不支持异常
    /// </summary>
    public class DataBaseNotSupportException:Exception
    {
        /// <summary>
        /// 数据库不支持异常
        /// </summary>
        /// <param name="database">数据库名称</param>
        public DataBaseNotSupportException(string database):base(string.Format("数据库{0}不支持！", database))
        {
        }
    }
}
