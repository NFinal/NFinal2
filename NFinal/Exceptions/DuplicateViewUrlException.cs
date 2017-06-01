//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DuplicateViewUrlException.cs
//        Description :不同的RazorView类具有相同的模板路径的异常
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
    /// 不同的RazorView类具有相同的模板路径的异常
    /// </summary>
    public class DuplicateViewUrlException:Exception
    {
        /// <summary>
        /// 不同的RazorView类具有相同的模板路径的异常
        /// </summary>
        /// <param name="oldViewClassName">类名称</param>
        /// <param name="viewClassName">重复的类名称</param>
        public DuplicateViewUrlException(string oldViewClassName,string viewClassName)
            :base(string.Format("重复的模板路径！{0}下的Render方法与{1}下的Render方法拥有相同的View定义。", oldViewClassName, viewClassName))
        {
        }
    }
}
