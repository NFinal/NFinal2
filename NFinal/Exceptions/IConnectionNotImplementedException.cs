//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IConnectionNotImplementedException.cs
//        Description :数据库对象未初始化的异常
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    /// <summary>
    /// 数据库对象未初始化的异常
    /// </summary>
    public class IConnectionNotImplementedException : NotImplementedException
    {
        /// <summary>
        /// 数据库对象未初始化的异常
        /// </summary>
        /// <param name="type">控制器类型</param>
        public IConnectionNotImplementedException(Type type)
            :base(string.Format("控制器类型{0}.{1}必须继承NFinal.Action.IConnection接口，并重写GetDbConnection方法。", type.Namespace, type.Name))
        {

        }
    }
}