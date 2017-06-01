//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : KeyValue.cs
//        Description :KeyValue类型，Key为字符串类型，Value为泛型。
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
    /// KeyValue类型，Key为字符串类型，Value为泛型。
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    public struct KeyValue<TValue>
    {
        /// <summary>
        /// KeyValue初始化
        /// </summary>
        /// <param name="key">字符串key</param>
        /// <param name="value">value值</param>
        /// <param name="index">索引</param>
        public KeyValue(string key, TValue value,int index)
        {
            this.key = key;
            this.length = key.Length;
            this.value = value;
            this.index = index;
        }
        /// <summary>
        /// key
        /// </summary>
        public string key;
        /// <summary>
        /// key的长度
        /// </summary>
        public int length;
        /// <summary>
        /// 索引
        /// </summary>
        public int index;
        /// <summary>
        /// value
        /// </summary>
        public TValue value;
    }
}
