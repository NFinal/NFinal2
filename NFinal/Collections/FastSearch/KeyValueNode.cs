//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : KeyValueNode.cs
//        Description :代码节点信息类，用于生成IL代码提供比较信息。
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

namespace NFinal.Collections.FastSearch
{
    /// <summary>
    /// 代码节点信息类，用于生成IL代码提供比较信息。
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    public class KeyValueNode<TValue>
    {
        /// <summary>
        /// 小于的值
        /// </summary>
        public KeyValue<TValue> kvLeft;
        /// <summary>
        /// 比较的值
        /// </summary>
        public long compareNumber;
        /// <summary>
        /// 大于的值
        /// </summary>
        public KeyValue<TValue> kvRight;
    }
}
