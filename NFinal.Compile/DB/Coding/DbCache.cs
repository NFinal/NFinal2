//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :DB.cs
//        Description :从Web.Config获取的数据库类,包括所有的类型信息等
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Compile.DB.Coding
{
    /// <summary>
    /// 从Web.Config获取的数据库类,包括所有的类型信息等
    /// </summary>
    class DbCache
    {
        public static Dictionary<string, DB.Coding.DataUtility> DbStore = new Dictionary<string, DB.Coding.DataUtility>();
    }
}
