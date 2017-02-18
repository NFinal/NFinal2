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
            writer.Write("    ");
            int vaa = 1; writer.Write("df\r\n");
            var vbb = vaa.ToString(); writer.Write("\r\n    ");
            writer.Write(Model.a);
            writer.Write("\r\n    ");
            writer.Write(Model.b);
            writer.Write("\r\n    ");
            writer.Write(Model.c);
            writer.Write("\r\n");
            foreach (string a in Model.d)
            {
                writer.Write(a.ToString());
                ;
            }
            writer.Write("    ");
            if (Model.a == Model.b)
            {
                writer.Write(Model.a);

            }
            writer.Write("    ");
            while (true)
            {
                writer.Write(Model.a);

            }
            writer.Write("\r\n</body>\r\n\r\n</html>");
        }
    }
}
