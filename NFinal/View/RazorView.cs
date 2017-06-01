//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : RazorView.cs
//        Description :视图基础类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.View
{
    /// <summary>
    /// 视图接口
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 输入对象
        /// </summary>
        NFinal.IO.IWriter writer { get; set; }
        /// <summary>
        /// 输出模板
        /// </summary>
        void Execute();
    }
    /// <summary>
    /// cshtml基础视图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RazorView<T>:IView
    {
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="Model"></param>
        public RazorView(NFinal.IO.IWriter writer,T Model)
        {
            this.writer = writer;
            this.Model = Model;
        }
        /// <summary>
        /// 输出对象
        /// </summary>
        public NFinal.IO.IWriter writer { get; set; }
        /// <summary>
        /// 模板实体类
        /// </summary>
        public T Model { get; set; }
        /// <summary>
        /// 输出模板
        /// </summary>
        public abstract void Execute();
    }
}