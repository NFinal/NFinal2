using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.Plugs.Base
{
    /// <summary>
    /// 输出图标
    /// </summary>
    public class FaviconController:NFinal.OwinAction
    {
        /// <summary>
        /// 网站图标的Stream流
        /// </summary>
        public static Stream faviconStream = null;
        /// <summary>
        /// 默认控制器
        /// </summary>
        [Url("/favicon.ico")]
        public void Index()
        {
            if (faviconStream == null)
            {
                string fileName = MapPath("/favicon.ico");
                if (File.Exists(fileName))
                {
                    faviconStream = System.IO.File.OpenRead(fileName);
                    faviconStream.Seek(0, SeekOrigin.Begin);
                    faviconStream.CopyTo(this.response.stream);
                }
                else
                {
                    //none;
                }
            }
            else
            {
                faviconStream.Seek(0, SeekOrigin.Begin);
                faviconStream.CopyTo(this.response.stream);
            }
        }
    }
}
