using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using NFinal;

//此代码由NFinalCompiler生成。
//http://bbs.nfinal.com
namespace $safeprojectname$.Views.Index
{
    [View("/$safeprojectname$/Views/Index/Template.cshtml")]
    public class Template : NFinal.View.RazorView<$safeprojectname$.Controllers.IndexController_Model.Template>
    {
        public Template(NFinal.IO.Writer writer, $safeprojectname$.Controllers.IndexController_Model.Template Model) : base(writer, Model)
        {
        }
        //如果此处报错，请添加NFinal引用
        //PMC命令为：Install-Package NFinal2
        public override void Execute()
        {
            writer.Write("");
            writer.Write("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <script>1</script>\r\n    <title></title>\r\n    <meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    ");
            writer.Write(Model.Message);
            writer.Write("\r\n</body>\r\n</html>");
        }
    }
}
