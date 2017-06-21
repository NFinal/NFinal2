using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFinal;
//此代码由NFinalControllerGenerator生成。
//http://bbs.nfinal.com
namespace NFinalCoreWebSample.Controllers.IndexController_Model
{
	public class Index
	{
		[ViewBagMember]
		[Newtonsoft.Json.JsonIgnore]
		public NFinal.Collections.FastSearch.FastSearch<NFinal.StringContainer> systemConfig;
	}
}
