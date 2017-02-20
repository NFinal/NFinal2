using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace CoreWebTest
{
    [View("/CoreWebTest/Index2.cshtml")]
    public static class Index2
    {
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public static void Render(this NFinal.IO.IWriter writer, string Model)
        {
            writer.Write("");
            writer.Write("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <title></title>\r\n\t<meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    ");
            writer.Write(Model);
            writer.Write("\r\n</body>\r\n</html>");
        }
    }
}
