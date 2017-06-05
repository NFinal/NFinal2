using System;
using NFinal;

//此代码由NFinalRazorGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCompiler.Sql
{
	[View("/NFinalCompiler/Sql/ModelTemplate.cshtml")]
	public static class ModelTemplate
	{
		//如果此处报错，请添加NFinal引用
		//PMC命令为：Install-Package NFinal
		public static void Render(NFinal.IO.Writer writer,Data.TableData Model)
		{
			writer.Write("");
			writer.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Text;\r\n\r\nnamespace ");
			writer.Write(SqlDocument.nameSpace);
			writer.Write("\r\n{\r\n    /// <summary>\r\n    /// ");
			writer.Write(Model.Name);
			writer.Write("\r\n    ///</summary>\r\n    public class ");
			writer.Write(Model.Name);
			writer.Write("\r\n    {\r\n");
    foreach (var column in Model.ColumnDataList)
    {
			writer.Write("        ");
			writer.Write("/// <summary>\r\n        ");
			writer.Write("/// ");
			writer.Write(column.Name);
			writer.Write("\r\n        ");
			writer.Write("///</summary>\r\n        ");
			writer.Write("public ");
			writer.Write(column.TypeString);
			writer.Write(" ");
			writer.Write(column.Name);
			writer.Write(" { get; set; }\r\n");
    }
			writer.Write("    }\r\n}");
		}
	}
}
