using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using NFinal;

//此代码由NFinalCompiler生成。
//http://bbs.nfinal.com
namespace $safeprojectname$.Views.Index
{
    [View("/$safeprojectname$/Views/Index/Html.cshtml")]
    public class Html : NFinal.View.RazorView<$safeprojectname$.Controllers.IndexController_Model.Html>
    {
        public Html(NFinal.IO.Writer writer, $safeprojectname$.Controllers.IndexController_Model.Html Model) : base(writer, Model)
        {
        }
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal2
        public override void Execute()
        {
            writer.Write("");
            writer.Write("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <script>1</script>\r\n    <title>");
            writer.Write(Model.Title);
            writer.Write("</title>\r\n    <meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    <h2>Message:");
            writer.Write(Model.Message);
            writer.Write("</h2>\r\n    <h2>站点名称:");
            writer.Write(Model.config.keyValueCache["siteName"]);
            writer.Write("</h2>\r\n    <h2>联系电话:");
            writer.Write(Model.config.keyValueCache["mobile"]);
            writer.Write("</h2>\r\n</body>\r\n</html>");
        }
    }
}
