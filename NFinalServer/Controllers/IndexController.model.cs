using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
//此代码由NFinalControllerGenerator生成。
//http://bbs.nfinal.com
namespace NFinalServer.Controllers.IndexController_Model
{
	public class Default
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastDictionary<NFinal.StringContainer> systemConfig;
		public string Message;
		public string Title;
	}
	public class Ajax
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastDictionary<NFinal.StringContainer> systemConfig;
		public string Message;
		public int id;
		public System.DateTime time;
	}
}
