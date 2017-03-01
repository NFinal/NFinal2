using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public enum ServerType
    {
        /// <summary>
        /// IIS传统服务器
        /// </summary>
        AspNET,
        /// <summary>
        /// vNext服务器
        /// </summary>
        MicrosoftOwin,
        /// <summary>
        /// NFinal实现的Owin服务器
        /// </summary>
        NFinalOwin,
        /// <summary>
        /// 无服务器，静态页生成
        /// </summary>
        IsStatic,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown
    }
}
