using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// url重写基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ReWriteAttribute : Attribute
    {

    }
    /// <summary>
    /// 页面重写属性
    /// </summary>
    public class RewriteFileAttribute : ReWriteAttribute
    {
        public RewriteFileAttribute(string from, string to)
        { }
    }
    /// <summary>
    /// 页面文件夹重写属性
    /// </summary>
    public class RewriteDirectoryAttribute : ReWriteAttribute
    {
        public RewriteDirectoryAttribute(string from, string to)
        { }
    }
}
