using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalServerSample.Views.Index
{
    [View("/NFinalServerSample/Views/Index/Default.cshtml")]
    public class Default : NFinal.View.RazorView<NFinalServerSample.Controllers.IndexController_Model.Default>
    {
        public Default(NFinal.IO.Writer writer, NFinalServerSample.Controllers.IndexController_Model.Default Model) : base(writer, Model)
        {
        }
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal
        public override void Execute()
        {
            writer.Write("");
            writer.Write("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <title>33</title>\r\n\t<meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    <h2>Message:");
            writer.Write(Model.Message);
            writer.Write("</h2>\r\n    <h2>siteName：");
            writer.Write(Model.systemConfig["siteName"].value);
            writer.Write("</h2>\r\n    <h2>mobile:");
            writer.Write(Model.systemConfig["mobile"].value);
            writer.Write("</h2>\r\n</body>\r\n</html> ");
        }
    }
}
