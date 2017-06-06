using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    /// <summary>
    /// 该项目中没有任何控制器
    /// </summary>
    public class HasNoControllerInProjectException: System.Exception
    {
        /// <summary>
        /// 控制器未找到异常。
        /// </summary>
        public HasNoControllerInProjectException()
            :base("控制器未找到！请确认是否添加了自定义控制器！通常在项目刚建立时容易引起该错误。")
        {
        }
    }
}
