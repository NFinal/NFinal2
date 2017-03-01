using System.Collections.Generic;
using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinal.Middleware
{
    [View("/NFinal/Middleware/UrlRouteJs.cshtml")]
    public static class UrlRouteJs
    {
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public static void Render(NFinal.IO.Writer writer, NFinal.Middleware.UrlRouteJsModel Model)
        {
            writer.Write("");
            writer.Write("function StringFormat() {\r\n    if (arguments.length == 0)\r\n        return null;\r\n    var str = arguments[0];\r\n    for (var i = 1; i < arguments.length; i++) {\r\n        var re = new RegExp(\'\\\\{\' + (i - 1) + \'\\\\}\', \'gm\');\r\n        str = str.replace(re, arguments[i]);\r\n    }\r\n    return str;\r\n}\r\nvar Url={\r\n");
            bool isFirst = true; writer.Write("\r\n");
            foreach (KeyValuePair<Type, Dictionary<string, NFinal.Middleware.FormatData>> formatController in Model.formatControllerDictionary)
            {
                string controllerName = formatController.Key.Namespace.Replace('.', '_') + "_" + formatController.Key.Name;
                isFirst = true;
                writer.Write("    ");
                writer.Write("\"");
                writer.Write(controllerName);
                writer.Write("\":{\r\n");
                foreach (KeyValuePair<string, NFinal.Middleware.FormatData> formatMethod in formatController.Value)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        writer.Write("            ");
                        writer.Write(",\r\n");
                    }

                    if (formatMethod.Value.actionUrlNames != null && formatMethod.Value.actionUrlNames.Length > 0)
                    {
                        writer.Write("            ");
                        writer.Write("\"");
                        writer.Write(formatMethod.Key);
                        writer.Write("\":function(");
                        writer.Write(string.Join(",", formatMethod.Value.actionUrlNames));
                        writer.Write(")\r\n            ");
                        writer.Write("{\r\n            ");
                        writer.Write("return StringFormat(\"");
                        writer.Write(formatMethod.Value.formatUrl);
                        writer.Write("\",");
                        writer.Write(string.Join(",", formatMethod.Value.actionUrlNames));
                        writer.Write(");\r\n            ");
                        writer.Write("}\r\n");
                    }
                    else
                    {
                        writer.Write("            ");
                        writer.Write("\"");
                        writer.Write(formatMethod.Key);
                        writer.Write("\":function()\r\n            ");
                        writer.Write("{\r\n            ");
                        writer.Write("return \"");
                        writer.Write(formatMethod.Value.formatUrl);
                        writer.Write("\";\r\n            ");
                        writer.Write("}\r\n");
                    }
                }
                writer.Write("    ");
                writer.Write("}\r\n");
            }
            writer.Write("};");
        }
    }
}
