using System;
using System.Collections.Generic;
using NFinal;

//此代码由NFinalCompiler生成。
//http://bbs.nfinal.com
namespace $rootnamespace$
{
	[View("$rootnamespace$.$safeitemname$.html")]
	public class $safeitemname$ : NFinal.View.RazorView<string>
	{
		public Default(NFinal.IO.Writer writer, string Model) : base(writer, Model)
        {
        }
		//如果此处报错，请添加NFinal引用
		//PMC命令为：Install-Package NFinal2
		public override void Execute()
		{
			writer.Write("");
			writer.Write("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <title></title>\r\n\t<meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    ");
			writer.Write(Model);
			writer.Write("\r\n</body>\r\n</html>");
		}
	}
}
