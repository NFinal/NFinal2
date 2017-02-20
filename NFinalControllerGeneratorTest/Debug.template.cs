using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalControllerGeneratorTest
{
    [View("/NFinalControllerGeneratorTest/Debug.cshtml")]
    public static class Debug
    {
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public static void Render(this NFinal.IO.IWriter writer, NFinalControllerGeneratorTest.DebugData Model)
        {
            writer.Write("");
            writer.Write("﻿<!DOCTYPE html>\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <meta http-equiv=\"pragma\" content=\"no-cache\">\r\n    <meta http-equiv=\"Cache-Control\" content=\"no-cache, must-revalidate\">\r\n    <meta http-equiv=\"expires\" content=\"Wed, 26 Feb 1997 08:21:57 GMT\">\r\n    <title></title>\r\n    <script src=\"/Scripts/Url.js\"></script>\r\n</head>\r\n<body>\r\n    此文件负责跳转到CoreWebTest.Controllers.Index下的INN方法\r\n    把此文件设为首页，即可调试对应的函数。\r\n</body>\r\n</html>\r\n<script>\r\n");
            if (Model.formatData.actionUrlNames != null && Model.formatData.actionUrlNames.Length > 0)
            {
                foreach (string name in Model.formatData.actionUrlNames)
                {
                    writer.Write("            ");
                    writer.Write("var ");
                    writer.Write(name);
                    writer.Write(" = \'");
                    writer.Write(name);
                    writer.Write("\';\r\n");
                }
                writer.Write("        ");
                writer.Write("var urlString = Url.CoreWebTest_Controllers_Index.INN(");
                writer.Write(string.Join(",", Model.formatData.actionUrlNames));
                writer.Write(");\r\n        ");
                writer.Write("window.location.href = urlString;\r\n");
            }
            writer.Write("</script>");
        }
    }
}
