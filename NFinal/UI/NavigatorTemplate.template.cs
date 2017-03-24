using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCore.UI
{
	[View("/NFinalCore/UI/NavigatorTemplate.cshtml")]
	public static class NavigatorTemplate
	{
		//如果此处报错，请添加NFinal引用
		//PMC命令为：Install-Package NFinal
		public static void Render(NFinal.IO.Writer writer,NFinal.UI.Navigator Model)
		{
			writer.Write("");
			writer.Write("<link href=\"<%=ViewBag.bucketImageUrl%>/App/Content/css/navigator.css\" rel=\"stylesheet\" />\r\n\r\n<div class=\"pageDiv\">\r\n    <a href=\"");
			writer.Write(Model.GetUrlFunction(1));
			writer.Write("\" class=\"currentpage\">首页</a>\r\n");
     bool isFirst = Model.index <= 1;			writer.Write("\r\n");
    
        string UrlPre = isFirst ? " #" : Model.GetUrlFunction(Model.index - 1);
    			writer.Write("\r\n    \r\n");
    if (isFirst)
    {
			writer.Write("        <a href=\"");
			writer.Write(UrlPre);
			writer.Write("\" class=\"currentpage\">上一页</a>\r\n");
    }
    else
    {
			writer.Write("        <a href=\"");
			writer.Write(UrlPre);
			writer.Write("\" class=\"currentpage\">上一页</a>\r\n");
    }
			writer.Write("    <a href=\"");
			writer.Write(UrlPre);
			writer.Write("\" style=\"");
			writer.Write(Model.index);
			writer.Write(" <= 1 ? \"display: inline-block;padding: 4px 8px;margin: 0 2px;color: #bfbfbf;background: #f2f2f2;border: 1px solid #ddd;vertical-align: middle;cursor:default;\":\"\"%>\" class=\"currentpage\">上一页</a>\r\n");
    for (int i = ((Model.index - 1) / Model.navigatorSize) * Model.navigatorSize + 1; i <= Model.count && i <= ((Model.index - 1) / Model.navigatorSize + 1) * Model.navigatorSize; i++)
    {
        if (i == Model.index)
        {
			writer.Write("            <span class=\"current\" style=\"width:30px;\"> ");
			writer.Write(i);
			writer.Write("</span>\r\n");
        }
        else
        {
			writer.Write("            <a href=\"");
			writer.Write(Model.GetUrlFunction(i));
			writer.Write("\" class=\"currentpage\" style=\"width:30px;\">");
			writer.Write(i);
			writer.Write("</a>\r\n");
        }
    }
			writer.Write("    ");
 bool isLast = Model.index >= Model.count;			writer.Write("\r\n");
     string UrlNext = isLast ? " #" : Model.GetUrlFunction(Model.index + 1);			writer.Write("\r\n   \r\n");
    if (isLast)
    {
			writer.Write("        <a href=\"");
			writer.Write(UrlNext);
			writer.Write("\" class=\"currentpage\">下一页</a>\r\n");
    }
    else
    {
			writer.Write("        <a href=\"");
			writer.Write(UrlNext);
			writer.Write("\" class=\"currentpage\">下一页</a>\r\n");
    }
			writer.Write("    <a href=\"");
			writer.Write(Model.GetUrlFunction(Model.count));
			writer.Write("\" class=\"currentpage\">末页</a>\r\n    <span class=\"pagecount\">共@Model.count页</span>\r\n</div>");
		}
	}
}
