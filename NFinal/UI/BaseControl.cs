//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : BaseControl.cs
//        Description :基础控件类,自定义控件需要继承该类，并新建一个以Template.cshtml结尾以该自定义类为视图数据的Razor视图。
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.UI
{
    /// <summary>
    /// 基础控件类
    /// </summary>
    public class BaseControl
    {
        private NFinal.IO.Writer writer;
        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="writer"></param>
        public BaseControl(NFinal.IO.Writer writer)
        {
            this.writer = writer;
        }
        /// <summary>
        /// 按照默认路径渲染
        /// </summary>
        public void Render()
        {
            Type t= this.GetType();
            string ViewPath= '/' + t.Namespace.Replace('.', '/') + '/' + t.Name + "Template.cshtml";
            this.Render(ViewPath);
        }
        /// <summary>
        /// 按照拽认路径渲染
        /// </summary>
        /// <param name="ViewPath"></param>
        public void Render(string ViewPath)
        {
            Render(ViewPath, this);
        }
        private void Render<T>(string ViewPath, T ViewBag)
        {
            NFinal.ViewDelegateData dele;
            if (NFinal.ViewDelegate.viewFastDic != null)
            {
                if (NFinal.ViewDelegate.viewFastDic.TryGetValue(ViewPath, out dele))
                {
                    if (dele.renderMethod == null)
                    {
                        dele.renderMethod = NFinal.ViewDelegate.GetRenderDelegate<T>(ViewPath,Type.GetTypeFromHandle(dele.viewTypeHandle));
                        NFinal.ViewDelegate.viewFastDic[ViewPath] = dele;
                    }
                    var render = (NFinal.RenderMethod<T>)dele.renderMethod;
                    render(writer, ViewBag);
                }
                else
                {
                    //模板未找到异常
                    throw new NFinal.Exceptions.ViewNotFoundException(ViewPath);
                }
            }
            else
            {
                throw new NFinal.Exceptions.ViewNotFoundException(ViewPath);
            }
        }
    }
}