﻿//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ValidObjectExtension.cs
//        Description :验证扩展函数
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Validation
{
    /// <summary>
    /// 验证代理 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public delegate bool ValidFunction(ValidObject obj);
    /// <summary>
    /// 验证扩展函数
    /// </summary>
    public static class ValidObjectExtension
    {
        public static ValidResult GetResult(this ValidObject obj)
        {
            return obj.validResult;
        }
        public static ValidObject AddCustomValid(this ValidObject obj, ValidFunction function)
        {
            obj.IsValid = function(obj.value);
            return obj;
        }
        public static ValidObject GetValidObject(this NFinal.NameValueCollection nvc, string name)
        {
            return new ValidObject(name,nvc[name]);
        }
        public static ValidObject AsValidObject(this string value)
        {
            return value;
        }
        public static ValidObject Required(this ValidObject obj)
        {
            if (string.IsNullOrEmpty(obj.value))
            {
                obj.IsValid = false;
            }
            return obj;
        }

        public static bool IsValid(this ValidObject obj)
        {
            return obj.IsValid;
        }
        public static ValidObject WithMessage(this ValidObject obj, string message)
        {
            if (!string.IsNullOrEmpty(message) && obj.validResult.Count > 1)
            {
                obj.validResult[obj.validResult.Count - 1].message = message;
            }
            return obj;
        }

        public static ValidObject IsNumber(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为数字", false);
            Regex regex = new Regex(Pattern.number);
            status.isDirty= !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsEmail(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为邮箱", false);
            Regex regex = new Regex(Pattern.email);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsUrl(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为Url", false);
            Regex regex = new Regex(Pattern.url);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsDomain(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为域名", false);
            Regex regex = new Regex(Pattern.domain);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsMobile(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为手机号", false);
            Regex regex = new Regex(Pattern.mobile);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsTelephone(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为电话号码", false);
            Regex regex = new Regex(Pattern.tel);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsIDCard(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为身份证号", false);
            Regex regex = new Regex(Pattern.idcard);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsChinese(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为中文", false);
            Regex regex = new Regex(Pattern.chinese);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsQQ(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为QQ", false);
            Regex regex = new Regex(Pattern.qq);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsPostCode(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为邮政编码", false);
            Regex regex = new Regex(Pattern.postcode);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject IsIp(this ValidObject obj)
        {
            ValidateStatus status = new ValidateStatus("参数必须为IP地址", false);
            Regex regex = new Regex(Pattern.ip);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Range(this ValidObject obj, float min, float max)
        {
            ValidateStatus status = new ValidateStatus("参数必须大于"+min+"且小于"+max, false);
            float result = obj.value.AsVar();
            if (result >= min && result <= max)
            {
                status.isDirty = false;
            }
            else
            {
                status.isDirty = true;
            }
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Max(this ValidObject obj, float value)
        {
            ValidateStatus status = new ValidateStatus("参数必须小于" + value, false);
            float result = obj.value.AsVar();
            if (result <= value)
            {
                status.isDirty = false;
            }
            else
            {
                status.isDirty = true;
            }
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Min(this ValidObject obj, float value)
        {
            ValidateStatus status = new ValidateStatus("参数必须大于" + value, false);
            float result = obj.value.AsVar();
            if (result >= value)
            {
                status.isDirty = false;
            }
            else
            {
                status.isDirty = true;
            }
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Regex(this ValidObject obj, string pattern)
        {
            ValidateStatus status = new ValidateStatus("参数必须符合正则"+pattern, false);
            Regex regex = new Regex(pattern);
            status.isDirty = !regex.IsMatch(obj.value);
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Length(this ValidObject obj, int len)
        {
            ValidateStatus status = new ValidateStatus("参数长度必须为"+len, false);
            if (obj.value.Length==len)
            {
                status.isDirty = false;
            }
            else
            {
                status.isDirty = true;
            }
            obj.validResult.Add(status);
            return obj;
        }
        public static ValidObject Length(this ValidObject obj, int min, int max)
        {
            ValidateStatus status = new ValidateStatus("参数长度必须大于"+min+"小于"+max, false);
            if (obj.value.Length >= min && obj.value.Length<=max)
            {
                status.isDirty = false;
            }
            else
            {
                status.isDirty = true;
            }
            obj.validResult.Add(status);
            return obj;
        }
    }
}
