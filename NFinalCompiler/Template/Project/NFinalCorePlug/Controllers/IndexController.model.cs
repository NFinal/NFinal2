using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
using Dapper;
//此代码由NFinalCompiler生成。
//http://bbs.nfinal.com
namespace $safeprojectname$.Controllers.IndexController_Model
{
	public class Html
	{
		[NFinal.ViewBagMember]
		public NFinal.Config.Plug.PlugConfig config;
		public string Message;
		public string Title;
	}
	public class Ajax
	{
		[NFinal.ViewBagMember]
		public NFinal.Config.Plug.PlugConfig config;
		public string Message;
		public int id;
		public System.DateTime time;
	}
}
