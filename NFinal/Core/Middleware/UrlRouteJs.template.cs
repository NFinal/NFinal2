using System.Collections.Generic;
using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinal.Core.Middleware
{
    [View("/NFinal/Core/Middleware/UrlRouteJs.cshtml")]
    public static class UrlRouteJs
    {
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public static void Render(this NFinal.IO.IWriter writer, NFinal.Middleware.UrlRouteJsModel Model)
        {
            writer.Write("");
            writer.Write("var Url={\r\n");
            foreach (KeyValuePair<Type, Dictionary<string, string>> formatController in Model.formatControllerDictionary)
            {
                string controllerName = formatController.Key.Namespace.Replace(',', '_') + "_" + formatController.Key.Name;
                writer.Write("    ");
                writer.Write("\"");
                writer.Write(controllerName);
                writer.Write("\":{\r\n");
                foreach (KeyValuePair<string, string> formatMethod in formatController.Value)
                {
                    writer.Write("        ");
                    writer.Write("function ");
                    writer.Write(formatMethod.Key);
                    writer.Write("");
                    writer.Write("(parameters)\r\n        ");
                    writer.Write("{\r\n            ");
                    writer.Write("string.Format(\"");
                    writer.Write(formatMethod.Value);
                    writer.Write("\",parameters);\r\n        ");
                    writer.Write("}\r\n");
                }
            }
            writer.Write("};");
        }
    }
}
