//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Navigator.cs
//        Description :分页控件
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

namespace NFinal.UI
{
    /// <summary>
    /// URL函数代理
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public delegate string GetUrlDelegate(int index);
    /// <summary>
    /// 分页控件
    /// </summary>
    public class Navigator:NFinal.UI.BaseControl
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int index;
        /// <summary>
        /// 总页数
        /// </summary>
        public int count;
        /// <summary>
        /// 每页多少行记录
        /// </summary>
        public int size;
        /// <summary>
        /// 总记录总
        /// </summary>
        public int recordCount;
        /// <summary>
        /// 控件最多显示页码标签数
        /// </summary>
        public int navigatorSize;
        /// <summary>
        /// 生成URL函数
        /// </summary>
        public GetUrlDelegate GetUrlFunction;
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页记录数</param>
        public Navigator(NFinal.IO.Writer writer,int index, int size):base(writer)
        {
            this.index = index;
            this.size = size;
            this.navigatorSize = 5;
        }
    }
}
