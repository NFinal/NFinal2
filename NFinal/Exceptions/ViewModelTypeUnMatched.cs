﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class ViewModelTypeUnMatchedException : System.Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public ViewModelTypeUnMatchedException(string url,Type input,Type need)
        {

            message = "模板输入类型与所需类型不匹配！\r\n模板路径为:" + url+"\r\n"+
                "输入类型为:"+input.FullName+"\r\n"+
                "所需类型为:"+need.FullName;
        }
    }
}