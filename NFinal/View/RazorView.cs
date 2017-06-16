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
using System.IO;

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
        NFinal.IO.Writer writer { get; set; }
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
        /// 模板渲染函数
        /// </summary>
        /// <typeparam name="TModel">视图数据类型</typeparam>
        /// <param name="url">视图URL路径</param>
        /// <param name="t">视图数据，即ViewBag</param>
        public bool Render<TModel>(string url, TModel t)
        {

            NFinal.ViewDelegateData dele;
            if (NFinal.ViewDelegate.viewFastDic != null)
            {
                if (NFinal.ViewDelegate.viewFastDic.TryGetValue(url, out dele))
                {
                    if (dele.renderMethod == null)
                    {
                        dele.renderMethod = NFinal.ViewDelegate.GetRenderDelegate<TModel>(url,Type.GetTypeFromHandle(dele.viewTypeHandle));
                        NFinal.ViewDelegate.viewFastDic[url] = dele;
                    }
                    var render = (NFinal.RenderMethod<TModel>)dele.renderMethod;
                    render(this.writer, t);
                    return true;
                }
                else
                {
                    //模板未找到异常
                    throw new NFinal.Exceptions.ViewNotFoundException(url);
                }
            }
            else
            {
                throw new NFinal.Exceptions.ViewNotFoundException(url);
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="Model"></param>
        public RazorView(NFinal.IO.Writer writer,T Model)
        {
            this.writer = writer;
            this.Model = Model;
        }
        /// <summary>
        /// 输出对象
        /// </summary>
        public NFinal.IO.Writer writer { get; set; }
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