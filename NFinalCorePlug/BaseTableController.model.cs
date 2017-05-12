using System;
using System.Collections.Generic;
using System.Text;
using NFinal;
//此代码由NFinalControllerGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCorePlug.BaseTableController_Model
{
	public class Insert
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
	}
	public class Update
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
	}
	public class Delete
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
	}
	public class Page
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public string imageServerUrl;
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
		public int pageIndex;
		public int pageSize;
	}
}
