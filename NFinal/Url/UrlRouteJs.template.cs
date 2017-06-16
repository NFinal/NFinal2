using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinal.Url
{
    [View("/NFinal/Url/UrlRouteJs.cshtml")]
    public class UrlRouteJs : NFinal.View.RazorView<NFinal.Url.UrlRouteJsModel>
    {
        public UrlRouteJs(NFinal.IO.Writer writer, NFinal.Url.UrlRouteJsModel Model) : base(writer, Model)
        {
        }
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public override void Execute()
        {
            writer.Write("");
            writer.Write("function StringFormat() {\r\n    if (arguments.length == 0)\r\n        return null;\r\n    var str = arguments[0];\r\n    for (var i = 1; i < arguments.length; i++) {\r\n        var re = new RegExp(\'\\\\{\' + (i - 1) + \'\\\\}\', \'gm\');\r\n        str = str.replace(re, arguments[i]);\r\n    }\r\n    return str;\r\n}\r\nvar Url={\r\n");
            bool isFirstAction = true; bool isFirstController = true; writer.Write("\r\n");
            foreach (KeyValuePair<RuntimeTypeHandle, Dictionary<string, NFinal.Url.FormatData>> formatController in Model.formatControllerDictionary)
            {
                Type controllerType = Type.GetTypeFromHandle(formatController.Key);
                string controllerName = controllerType.Namespace.Replace('.', '_') + "_" + controllerType.Name;

                if (isFirstController)
                {
                    isFirstController = false;
                }
                else
                {
                    writer.Write("        ");
                    writer.Write(",\r\n");
                }
                isFirstAction = true;
                writer.Write("    ");
                writer.Write("\"");
                writer.Write(controllerName);
                writer.Write("\":{\r\n");
                foreach (KeyValuePair<string, NFinal.Url.FormatData> formatMethod in formatController.Value)
                {
                    if (isFirstAction)
                    {
                        isFirstAction = false;
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
