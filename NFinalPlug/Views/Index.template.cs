using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalPlug.Views
{
	[View("/NFinalPlug/Views/Index.cshtml")]
	public static class Index
	{
		//如果此处报错，请添加NFinal引用
		//PMC命令为：Install-Package NFinal
		public static void Render(NFinal.IO.Writer writer,NFinalPlug.Controllers.IndexController_Model.Show Model)
		{
			writer.Write("");
			writer.Write("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <title></title>\r\n\t<meta http-equiv=\"Cache-Control\" content=\"no-cache\">\r\n</head>\r\n<body>\r\n    ");
			writer.Write(Model.a);
			writer.Write(";\r\n    ");
			writer.Write(Model.cc2);
			writer.Write(";\r\n    你在哪？ffff\r\n</body>\r\n</html>");
		}
	}
}
