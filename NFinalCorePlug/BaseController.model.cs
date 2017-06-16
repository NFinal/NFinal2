using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NFinal;
using NFinal.Http;
//此代码由NFinalControllerGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCorePlug.BaseController_Model
{
	public class UpdateA
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
		public int a;
		public string b;
	}
}
