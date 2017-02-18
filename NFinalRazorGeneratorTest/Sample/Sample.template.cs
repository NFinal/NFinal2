using System.Text;
using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalRazorGeneratorTest.Sample
{
    [View("/NFinalRazorGeneratorTest/Sample/Sample.cshtml")]
    public static class Sample
    {
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public static void Render(this NFinal.IO.IWriter writer, NFinalRazorGeneratorTest.Sample.Model Model)
        {
            writer.Write("");
            writer.Write("<!DOCTYPE html>\r\n");
            /*这里是注释内容*/
            writer.Write("\r\n<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <title></title>\r\n</head>\r\n<body>\r\n");
            if (Model.a == "")
            {
                writer.Write("        ");
                writer.Write("writea ");
                writer.Write(Model.b);



                writer.Write(":dfsdfsdf\r\n");
            }
            writer.Write("\r\n</body>\r\n\r\n</html>");
        }
    }
}
