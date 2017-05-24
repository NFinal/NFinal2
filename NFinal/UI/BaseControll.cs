using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.UI
{
    public class BaseControl
    {
        private NFinal.IO.Writer writer;
        public BaseControl(NFinal.IO.Writer writer)
        {
            this.writer = writer;
        }
        public void Render()
        {
            Type t= this.GetType();
            string ViewPath= '/' + t.Namespace.Replace('.', '/') + '/' + t.Name + "Template.cshtml";
            this.Render(ViewPath);
        }
        public void Render(string ViewPath)
        {
            Render(ViewPath, this);
        }
        private void Render<T>(string ViewPath, T ViewBag)
        {
            NFinal.ViewDelegateData dele;
            if (NFinal.ViewHelper.viewFastDic != null)
            {
                if (NFinal.ViewHelper.viewFastDic.TryGetValue(ViewPath, out dele))
                {
                    if (dele.renderMethod == null)
                    {
                        dele.renderMethod = NFinal.ViewHelper.GetRenderDelegate<T>(ViewPath, dele.viewType);
                        NFinal.ViewHelper.viewFastDic[ViewPath] = dele;
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