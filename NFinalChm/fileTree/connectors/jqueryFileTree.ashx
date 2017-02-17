<%@ webhandler Language="C#" class="NFinal.Resource.fileTree.connectors.jqueryFileTree" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace NFinal.Resource.fileTree.connectors
{
    /// <summary>
    /// jqueryFileTree 的摘要说明
    /// </summary>
    public class jqueryFileTree : IHttpHandler
    {
        public static Dictionary<string, string> htmlCache = new Dictionary<string, string>();
        public void ProcessRequest(HttpContext context)
        {
            bool hasValue = false;
            context.Response.ContentType = "text/plain";
            string dir;
            if (context.Request.Form["dir"] == null || context.Request.Form["dir"].Length <= 0)
            {
                dir = "/NFinalChm/";
            }
            else
            {
                dir = context.Server.UrlDecode(context.Request.Form["dir"]);
            }
            string html;
            if (htmlCache.TryGetValue(dir, out html))
            {
                context.Response.Write(html);
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(context.Server.MapPath(dir));
                sb.Append("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");
                foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
                {
                    sb.Append("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"");
                    sb.Append(dir);
                    sb.Append(di_child.Name);
                    sb.Append("/\">");
                    sb.Append(di_child.Name);
                    sb.Append("</a></li>\n");
                }
                foreach (System.IO.FileInfo fi in di.GetFiles())
                {
                    string ext = "";
                    if (fi.Extension.Length > 1)
                    {
                        ext = fi.Extension.Substring(1).ToLower();
                    }
                    switch (ext)
                    {
                        case "txt": hasValue = true; break;
                        case "config": hasValue = true; break;
                        case "cs": hasValue = true; break;
                        case "html": hasValue = true; break;
                        case "js": hasValue = true; break;
                        case "css": hasValue = true; break;
                        case "htm": hasValue = true; break;
                        case "aspx": hasValue = true; break;
                        case "ascx": hasValue = true; break;
                        case "ashx": hasValue = true; break;
                        default: hasValue = false; break;
                    }
                    if (hasValue)
                    {
                        sb.Append("\t<li class=\"file ext_");
                        sb.Append(ext);
                        sb.Append("\"><a href=\"#\" rel=\"");
                        sb.Append(dir);
                        sb.Append(fi.Name);
                        sb.Append("\">");
                        sb.Append(fi.Name);
                        sb.Append("</a></li>\n");
                    }
                }
                sb.Append("</ul>");
                html = sb.ToString();
                htmlCache.Add(dir, html);
                context.Response.Write(html);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}