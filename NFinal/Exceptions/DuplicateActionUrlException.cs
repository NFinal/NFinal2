//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DuplicateActionUrlException.cs
//        Description :不同的控制器行为具有相同的自定义请求路径的异常
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

namespace NFinal.Exceptions
{
    /// <summary>
    /// 不同的控制器行为具有相同的自定义请求路径的异常
    /// </summary>
    public class DuplicateActionUrlException:Exception
    {
        /// <summary>
        /// 不同的控制器行为具有相同的自定义请求路径的异常
        /// </summary>
        /// <param name="currentControllerName">当前控制器名称</param>
        /// <param name="currentMethodName">当前方法名称</param>
        /// <param name="controllerName">重复的控制器名称</param>
        /// <param name="methodName">重复的方法名称</param>
        public DuplicateActionUrlException(string currentControllerName,string currentMethodName,string controllerName,string methodName)
            :base(string.Format("重复的请求路径！{0}下的{1}方法与{2}下的{3}方法拥有相同的请求路径定义。", currentControllerName, currentMethodName, controllerName, methodName))
        {
        }
    }
}
