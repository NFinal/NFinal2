//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :AbtractAction.cs
//        Description :控制器抽象类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using NFinal.Owin;
using System.Data;
using NFinal.Http;
using Dapper;

namespace NFinal.Action
{
    /// <summary>
    /// NFinal的控制器基类，核心类，类似于.net mvc中的BaseController类或者是asp.net中的Page类
    /// </summary>
    /// <typeparam name="TContext">上下文IOwinContext,Enviroment,Context</typeparam>
    /// <typeparam name="TRequest">请求信息</typeparam>
    public abstract class AbstractAction<TContext,TRequest> :NFinal.IO.Writer, IAction<TContext, TRequest>
    {
        /// <summary>
        /// 获取Session对象
        /// </summary>
        /// <param name="sessionId">存储在Cookie的SessionId的名称</param>
        /// <param name="userKey">存储用户Session的Key</param>
        /// <returns></returns>
        public virtual NFinal.Http.ISession GetSession(string sessionId)
        {
            return new Session(sessionId,"User", new NFinal.Cache.SimpleCache(30));
        }
        /// <summary>
        /// 配置数据
        /// </summary>
        public Config.Plug.PlugConfig config=null;
        #region 数据库相关
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        protected IDbConnection con=null;
        /// <summary>
        /// 获取并打开数据库连接
        /// </summary>
        public IDbConnection Con
		{
			get 
			{
                if (this is NFinal.Model.IConnection)
                {
                    if (con == null)
                    {
                        con = GetDbConnection();
                        con.Open();
                    }
                    return con;
                }
                else
                {
                    throw new NFinal.Exceptions.IConnectionNotImplementedException(this.GetType());
                }
			}
		}
        /// <summary>
        /// 解析请求参数组成实体类并插入数据库中
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
		public int Insert<TModel>() where TModel : class
		{
            TModel model = this.GetModel<TModel>();
            return Con.SimpleInsert<int,TModel>(model);
		}
        /// <summary>
        /// 插入Model信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TKey Insert<TKey, TModel>() where TModel : class
        {
            TModel model = this.GetModel<TModel>();
            return Con.SimpleInsert<TKey, TModel>(model);
        }
        /// <summary>
        /// 解析请求参数组成实体类并插入数据库中
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
		public int InsertKeyInt<TModel>() where TModel : class
		{
			TModel model =this.GetModel<TModel>();
			return Con.SimpleInsert<int,TModel>(model);
		}
        /// <summary>
        /// 解析请求参数组成实体类并插入数据库中
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
		public long InsertKeyLong<TModel>() where TModel : class
		{
            TModel model = this.GetModel<TModel>();
            return Con.SimpleInsert<long,TModel>(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public bool InsertOrUpdate<TModel>(string sqlWhere) where TModel : class
        {
            Type modelType = typeof(TModel);
            TModel model = this.GetModel<TModel>();
            int count= Con.ExecuteScalar<int>($"select count(*) from {modelType.Name} where "+sqlWhere);
            if (count > 0)
            {
                return Con.SimpleUpdate(model,sqlWhere);
            }
            else
            {
                return Con.SimpleUpdate(model,sqlWhere);
            }
        }
        /// <summary>
        /// 解析请求参数组成实体类并更新数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlWhere"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Update<TModel>(string sqlWhere=null,object param=null) where TModel : class
		{
			TModel model=this.GetModel<TModel>();
			return Con.SimpleUpdate(model,sqlWhere);
		}
        /// <summary>
        /// 解析请求参数组中的id并放入实体中，然后删除该记录
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="strWhere"></param>
        /// <returns></returns>
		public bool Delete<TModel>(string strWhere) where TModel : class
		{
            TModel model = this.GetModel<TModel>();
            return Con.SimpleDelete(model,strWhere);
		}
        /// <summary>
        /// 解析请求参数组中的id并放入实体中，然后删除该记录
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public bool Delete<TModel>() where TModel : class
        {
            TModel model = this.GetModel<TModel>();
            return Con.SimpleDelete(model);
        }
        /// <summary>
        /// 获取Model信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel Get<TModel>() where TModel : class
        {
            TModel model = this.GetModel<TModel>();
            return Con.SimpleGet(model);
        }
        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public TModel Get<TModel>(string sqlWhere) where TModel : class
        {
            TModel model = this.GetModel<TModel>();
            return Con.SimpleGet(model,sqlWhere);
        }

        /// <summary>
        /// 根据查询条件获取实体列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlWhere"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<TModel> GetAll<TModel>(string sqlWhere, object param = null) where TModel : class
        {
            TModel model = this.GetModel<TModel>();
            return Con.SimpleGetAll(model,sqlWhere);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<TModel> Page<TModel>(int pageIndex,int pageSize,out int count)
        {
            return Con.SimpleGetPage<TModel>(pageIndex, pageSize,out count);
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        /// <summary>
        /// 获取数据库接连，需要子类重写
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection GetDbConnection()
        {
            Type currentType = this.GetType();
            throw new NotImplementedException(string.Format("{0}.{1}必须重写GetDbConnection方法",
                currentType.Namespace, currentType.Name));
        }
#endregion
        /// <summary>
        /// 常用系统变量
        /// </summary>
        [ViewBagMember]
        [Newtonsoft.Json.JsonIgnore]
        public static NFinal.Collections.FastSearch.FastSearch<StringContainer> systemConfig = null;
        /// <summary>
        /// 请求参数信息
        /// </summary>
        public NameValueCollection parameters;
        private NFinal.Collections.FastDictionary<string, NFinal.Http.HttpMultipart.HttpFile> _files;
        /// <summary>
        /// 请求的文件
        /// </summary>
        public NFinal.Collections.FastDictionary<string, NFinal.Http.HttpMultipart.HttpFile> files {
            get {
                string requestContentType = GetRequestHeader(NFinal.Constant.HeaderContentType);
                if (methodName == NFinal.Constant.MethodTypePOST
                   && requestContentType.Split(NFinal.Constant.CharSemicolon)[0] == NFinal.Constant.ContentType_Multipart_form_data
                   )
                {
                    string boundary = NFinal.Http.HttpMultipart.HttpMultipart.boundaryReg.Match(requestContentType).Value;
                    var multipart = new NFinal.Http.HttpMultipart.HttpMultipart(GetRequestBody(), boundary);
                    _files = new NFinal.Collections.FastDictionary<string, NFinal.Http.HttpMultipart.HttpFile>(StringComparer.Ordinal);
                    foreach (var httpMultipartBoundary in multipart.GetBoundaries())
                    {
                        if (string.IsNullOrEmpty(httpMultipartBoundary.Filename))
                        {
                            string name = httpMultipartBoundary.Name;
                            if (!string.IsNullOrEmpty(name))
                            {
                                string value = new System.IO.StreamReader(httpMultipartBoundary.Value).ReadToEnd();
                                parameters.Add(name, value);
                            }
                        }
                        else
                        {
                            _files.Add(httpMultipartBoundary.Name, new NFinal.Http.HttpMultipart.HttpFile(httpMultipartBoundary));
                        }
                    }
                }
                return _files;
            }
        }
        /// <summary>
        /// 输出类型
        /// </summary>
        public string contentType = "text/html; charset=utf-8";
        /// <summary>
        /// 内容
        /// </summary>
        public Stream writeStream;
        /// <summary>
        /// 服务器类型
        /// </summary>
        public ServerType _serverType = ServerType.UnKnown;
        /// <summary>
        /// Cookie
        /// </summary>
        public ICookie Cookie;
        /// <summary>
        /// Session
        /// </summary>
        public ISession Session;
        //private TUser _user;
        private string _methodName;
        /// <summary>
        /// 方法名，只读
        /// </summary>
        public string methodName { get { return _methodName; } }
        ///// <summary>
        ///// 用户
        ///// </summary>
        //public TUser user
        //{
        //    get
        //    {
        //        return Session.Get<TUser>("user");
        //    }

        //    set
        //    {
        //        _user = value;
        //        Session.Set<TUser>("user",_user);
        //    }
        //}
        /// <summary>
        /// 视图数据，通过ViewBag.字段名=值;可直接添加视图数据
        /// </summary>
        public dynamic ViewBag=null;
        /// <summary>
        /// 上下文
        /// </summary>
        public TContext context { get; set; }
        /// <summary>
        /// 请求信息
        /// </summary>
        public TRequest request { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public Response response { get; set; }
        /// <summary>
        /// 输出内容
        /// </summary>
        public Stream outputStream { get; set; }
        /// <summary>
        /// 压缩方式
        /// </summary>
        public CompressMode compressMode { get; set; }
        #region 子类必须实现的方法
        /// <summary>
        /// 基础初始化函数
        /// </summary>
        /// <param name="plugConfig">插件配置</param>
        /// <param name="context">Http上下文</param>
        /// <param name="methodName">Http请求方法</param>
        public virtual void BaseInitialization(TContext context, string methodName, NFinal.Config.Plug.PlugConfig plugConfig)
        {
            this.config = plugConfig;
            this.context = context;
            this._methodName = methodName;
            SetResponse(CompressMode.Deflate);
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="plugConfig">插件配置</param>
        /// <param name="context">Http上下文</param>
        /// <param name="methodName">Http请求方法</param>
        /// <param name="outputStream">Http输出流</param>
        /// <param name="request">Http请求信息</param>
        /// <param name="compressMode">压缩模式</param>
        public virtual void Initialization(TContext context, string methodName, Stream outputStream, TRequest request, CompressMode compressMode, NFinal.Config.Plug.PlugConfig plugConfig)
        {
            this.config = plugConfig;
            this.context = context;
            this._methodName = methodName;
            this.outputStream = outputStream;
            this.request = request;
            SetResponse(compressMode);
        }
        /// <summary>
        /// 设置压缩方式
        /// </summary>
        /// <param name="compressMode"></param>
        public void SetResponse(CompressMode compressMode)
        {
            this.response = new Owin.Response();
            this.response.headers = new Dictionary<string, string[]>(StringComparer.Ordinal);
            this.response.statusCode = 200;
            this.response.stream = new MemoryStream();
            this.compressMode = compressMode;
            if (compressMode == CompressMode.None)
            {
                this.writeStream = this.response.stream;
            }
            else
            {
                if (compressMode == CompressMode.GZip)
                {
                    this.writeStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                    this.response.headers.Add(NFinal.Constant.HeaderContentEncoding, NFinal.Constant.HeaderContentEncodingGzip);
                }
                else if (compressMode == CompressMode.Deflate)
                {
                    this.writeStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                    this.response.headers.Add(NFinal.Constant.HeaderContentEncoding, NFinal.Constant.HeaderContentEncodingDeflate);
                }
            }
        }
        /// <summary>
        /// 获取请求Url
        /// </summary>
        /// <returns></returns>
        public abstract string GetRequestPath();
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public abstract string GetRemoteIpAddress();
        /// <summary>
        /// 获取请求头信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract string GetRequestHeader(string key);
        /// <summary>
        /// 获取请求内容
        /// </summary>
        /// <returns></returns>
        public abstract Stream GetRequestBody();
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public abstract void SetResponseHeader(string key, string value);
        /// <summary>
        /// 设置请求头(多项)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public abstract void SetResponseHeader(string key, string[] value);
        /// <summary>
        /// 设置返回状态
        /// </summary>
        /// <param name="statusCode"></param>
        public abstract void SetResponseStatusCode(int statusCode);
        /// <summary>
        /// 控制器执行前方法
        /// </summary>
        /// <returns></returns>
        public abstract bool Before();
        /// <summary>
        /// 控制器执行后方法
        /// </summary>
        public abstract void After();
        /// <summary>
        /// 关闭控制器相关资源，如输入输出流等
        /// </summary>
        public abstract void Close();

        //public abstract void Write(byte[] buffer, int offset, int count);

        //public abstract void Write(string value);
        /// <summary>
        /// 释放控制器相关资源，如输入输出流等
        /// </summary>
        public abstract void Dispose();
        #endregion
        #region 扩展方法
        /// <summary>
        /// 输出字节流，用于输出二进制流，如图象，文件等。
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="count">缓冲区大小</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.writeStream.Write(buffer, 0, count);
        }
        /// <summary>
        /// 输出文本内容
        /// </summary>
        /// <param name="value">文本</param>
        public override void Write(string value)
        {
            if (value != null && value.Length != 0)
            {
                byte[] buffer = NFinal.Constant.encoding.GetBytes(value);
                this.writeStream.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 页面重定向
        /// </summary>
        /// <param name="url">请求URL字符串</param>
        public void Redirect(string url)
        {
            CloseConnection();
            this.SetResponseStatusCode(302);
            this.SetResponseHeader(NFinal.Constant.HeaderContentType, NFinal.Constant.ResponseContentType_Text_html);
            this.SetResponseHeader(NFinal.Constant.HeaderLocation, url);
        }
        /// <summary>
        /// 把视图数据转换为JSON格式，并输出
        /// </summary>
        public void AjaxReturn()
        {
            CloseConnection();
            this.contentType = "application/json; charset=utf-8";
            this.Write(Newtonsoft.Json.JsonConvert.SerializeObject(this.ViewBag, new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter()));
        }
        /// <summary>
        /// 输出json{code:1,msg:"",result:[json字符串]}
        /// </summary>
        /// <param name="json">json字符串</param>
        public void AjaxReturn(string json)
        {
            CloseConnection();
            this.SetResponseHeader(NFinal.Constant.HeaderContentType, NFinal.Constant.ResponseContentType_Application_json);
            this.Write(json);
        }
        /// <summary>
        /// 输出true或false
        /// </summary>
        /// <param name="obj">bool变量</param>
        public  void AjaxReturn(bool obj)
        {
            CloseConnection();obj.ToJson();
            this.SetResponseHeader(NFinal.Constant.HeaderContentType, NFinal.Constant.ResponseContentType_Application_json);
            this.Write(obj ? NFinal.Constant.trueString : NFinal.Constant.falseString);
        }
        /// <summary>
        /// 把请求信息根据名称自动封装进TModel类型中，并返回该类型对象
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel GetModel<TModel>()
        {
            TModel model= NFinal.Model.ModelHelper.GetModel<TModel>(this.parameters);
            System.Diagnostics.Debug.WriteLine(parameters.ToString());
            System.Diagnostics.Debug.WriteLine(model);
            return model;
        }
        /// <summary>
        /// 返回数据库实体类对应的json对象（对时间进行了数字处理）
        /// </summary>
        /// <param name="model">数据库实体类</param>
        public string GetModelJson<TModel>(TModel model)
        {
            if (model == null)
            {
                return Constant.nullString;
            }
            else
            {
                NFinal.IO.StringWriter sw = new IO.StringWriter();
                NFinal.Json.JsonHelper.GetJson(model, sw, Json.DateTimeFormat.LocalTimeNumber);
                return sw.ToString();
            }
        }
        /// <summary>
        /// 从请求内容中解析出Json类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T GetJson<T>()
        {
            Stream stream = GetRequestBody();
            StreamReader sr = new StreamReader(stream, NFinal.Constant.encoding);
            string content = sr.ReadToEnd();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
        /// <summary>
        /// 把任意类对象转换为Json字符串
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        public string GetJson<TModel>(TModel model)
        {
            if (model == null)
            {
                return Constant.nullString;
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(model);
            }
        }
        /// <summary>
        /// 返回json字符串{code:[code状态码],msg:[msg消息],result:[json字符串]}
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        public void AjaxReturn(string json, int code, string msg)
        {
            CloseConnection();
            this.SetResponseHeader(Constant.HeaderContentType, NFinal.Constant.ResponseContentType_Application_json);
            this.Write(Constant.AjaxReturnJson_Code);
            this.Write(code);
            this.Write(Constant.AjaxReturnJson_Msg);
            this.Write(msg);
            this.Write(Constant.AjaxReturnJson_Json);
            this.Write(json);
            this.Write(Constant.AjaxReturnJson_End);
        }
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public abstract string GetSubDomain(TContext context);
        /// <summary>
        /// 是否是手机端
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public bool IsMobile(string userAgent)
        {
            string agent = userAgent;
            bool isMobile = false;
            //排除 苹果桌面系统  
            if (!userAgent.Contains(NFinal.Constant.UserAgnet_Windows_NT) && !userAgent.Contains(NFinal.Constant.UserAgent_Macintosh))
            {
                for (int i = 0; i < NFinal.Constant.UserAgent_MobileKeyWords.Length; i++)
                {
                    if (agent.Contains(NFinal.Constant.UserAgent_MobileKeyWords[i]))
                    {
                        isMobile = true;
                        break;
                    }
                }
            }
            return isMobile;
        }
        /// <summary>
        /// 返回服务器路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return NFinal.IO.Path.MapPath(NFinal.Config.Configration.globalConfig.projectType,path);
        }
        /// <summary>
        /// 获取Url
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="urlParameters">Url中的get参数</param>
        /// <returns></returns>
        public static string Url<TController>(string methodName, params StringContainer[] urlParameters)
        {
            return NFinal.Url.ActionUrlHelper.Format(NFinal.Url.ActionUrlHelper.formatControllerDictionary[typeof(TController).TypeHandle][methodName].formatUrl, urlParameters);
        }
        /// <summary>
        /// 获取Url
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="urlParameters">Url参数</param>
        /// <returns></returns>
        public string Url(string methodName, params StringContainer[] urlParameters)
        {
            return NFinal.Url.ActionUrlHelper.Format(NFinal.Url.ActionUrlHelper.formatControllerDictionary[this.GetType().TypeHandle][methodName].formatUrl, urlParameters);
        }
        /// <summary>
        /// 模板渲染函数
        /// </summary>
        /// <typeparam name="T">视图数据类型</typeparam>
        /// <param name="url">视图URL路径</param>
        /// <param name="t">视图数据，即ViewBag</param>
        public bool RenderModel<T>(string url, T t)
        {
            CloseConnection();
            NFinal.ViewDelegateData dele;
            if (NFinal.ViewDelegate.viewFastDic != null)
            {
                if (NFinal.ViewDelegate.viewFastDic.TryGetValue(url, out dele))
                {
                    if (dele.renderMethod == null)
                    {
                        dele.renderMethod = NFinal.ViewDelegate.GetRenderDelegate<T>(url,Type.GetTypeFromHandle(dele.viewTypeHandle));
                        NFinal.ViewDelegate.viewFastDic[url] = dele;
                    }
                    var render = (NFinal.RenderMethod<T>)dele.renderMethod;
                    render(this, t);
                    return true;
                }
                else
                {
                    //模板未找到异常
                    throw new NFinal.Exceptions.ViewNotFoundException(url);
                }
            }
            else
            {
                throw new NFinal.Exceptions.ViewNotFoundException(url);
            }
        }
        /// <summary>
        /// 按照默认路径渲染模板
        /// </summary>
        public void Render()
        {
            //CoreWebTest/Index1.cshtml
            Type controllerType= this.GetType();
            string[] nameSpace= controllerType.Namespace.Split('.');
            using (StringWriter sw = new StringWriter())
            {
                sw.Write("/");
                if (!string.IsNullOrEmpty(config.defaultSkin))
                {
                    sw.Write(config.defaultSkin);
                }
                else
                {
                    sw.Write(nameSpace[0]);
                }
                for (int i = 1; i < nameSpace.Length; i++)
                {
                    sw.Write("/");
                    if (nameSpace[i] == "Controllers")
                    {
                        sw.Write("Views");
                    }
                    else
                    {
                        sw.Write(nameSpace[i]);
                    }
                }
                sw.Write("/");
                sw.Write(NFinal.Url.ActionUrlHelper.GetControllerName(controllerType));
                sw.Write("/");
                sw.Write(this.methodName);
                sw.Write(".cshtml"); sw.Dispose();
                this.RenderModel(sw.ToString(), ViewBag);
                sw.Dispose();
            }
        }
        /// <summary>
        /// 按照指定路径渲染模板
        /// </summary>
        /// <param name="templateUrl"></param>
        public void Render(string templateUrl)
        {
            this.RenderModel(templateUrl, ViewBag);
        }
        ///// <summary>
        ///// 根据母模板路径和当前路径渲染模板
        ///// </summary>
        ///// <param name="masterPagePath"></param>
        ///// <param name="templateUrl"></param>
        //public void Render(string masterPagePath, string templateUrl)
        //{
        //    if (this.MasterPage.GetType() == typeof(object))
        //    {
        //        this.Write("MasterPage必须为非dynamic以及object类型");
        //    }
        //    else
        //    {
        //        this.MasterPage.ViewData = new ViewData(templateUrl, this.ViewBag);
        //        this.RenderModel(masterPagePath,this.MasterPage);
        //    }
        //}

        
        #endregion
    }
}