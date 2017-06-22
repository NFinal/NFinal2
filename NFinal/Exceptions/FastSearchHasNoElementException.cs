using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    /// <summary>
    /// FastSearch对象中没有任何元素
    /// </summary>
    public class FastSearchHasNoElementException : System.Exception
    {
        /// <summary>
        /// 快速搜索类没有任何元素
        /// </summary>
        public FastSearchHasNoElementException()
            :base("快速搜索类初始化时未包含任何元素！")
        {

        }
    }
}
