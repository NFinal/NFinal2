using System;
using System.Collections.Specialized;
using System.Text;

namespace NFinal.Compile
{
    public enum VerbMethod
    {
        Request = 0,
        Post = 2,
        Get = 1
    }
    /// <summary>
    /// 方法实体类
    /// </summary>
    public class ControllerMethodData
    {
        //方法头开始,及长度
        public int start = 0;
        public int length = 0;
        public string AttributeString = string.Empty;
        public System.Collections.Generic.List<ControllerAttributeData> Attributes;
        public VerbMethod verbMethod = 0;
        public string contentType = "text/html; charset=utf-8";
        public string url = string.Empty;
        public UrlParser urlParser = null;

        public int optimizing = 0;
        public string minutes = "20";

        //注释
        public string CommitString = string.Empty;
        public string methodCommit = string.Empty;
        public string returnCommit = string.Empty;
        public NameValueCollection paramsCommit = null;
        //方法体开始,及内容
        public int position = 0;
        public string Content = string.Empty;
        public StringBuilder verifyCode = new StringBuilder();

        public string publicStr = string.Empty;
        public bool isPublic = false;
        public string returnType = string.Empty;
        public int returnTypeIndex = 0;
        public int returnTypeLength = 0;
        public string returnVarName = string.Empty;
        public string name = string.Empty;
        public bool hasParameters = false;
        public string parameters = string.Empty;
        public string parameterNames = string.Empty;
        public string parameterTypeAndNames = string.Empty;
        public int parametersIndex = 0;
        public int parametersLength = 0;

        //数据库方法
        public System.Collections.Generic.List<DbFunctionData> dbFunctions = new System.Collections.Generic.List<DbFunctionData>();
        //View方法
        public System.Collections.Generic.List<ViewData> views = new System.Collections.Generic.List<ViewData>();
        //函数参数
        public System.Collections.Generic.List<ControllerParameterData> parameterDataList = new System.Collections.Generic.List<ControllerParameterData>();
    }
}
