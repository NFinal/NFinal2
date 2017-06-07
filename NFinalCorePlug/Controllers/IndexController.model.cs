﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
using Dapper;
//此代码由NFinalControllerGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCorePlug.Controllers.IndexController_Model
{
	public class Default
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
		public string Message;
		public string Title;
		public NFinalCorePlug.Code.User model;
	}
	public class Ajax
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
		public string Message;
		public int id;
		public System.DateTime time;
	}
}
