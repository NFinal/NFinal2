﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// html5验证属性
/// </summary>
namespace NFinal.Advanced
{
    /// <summary>
    /// 与html5前端验证相同的后端方法
    /// </summary>
    public class html5Validate
    {
        public html5Validate()
        { }
        public static bool typeValid(string value,string t)
        {
            switch(t)
            {
                case type.text: return true;
                case type.email:return new Regex(pattern.email).IsMatch(value);
                case type.url: return new Regex(pattern.url).IsMatch(value);
                case type.number: return new Regex(pattern.number).IsMatch(value);
                case type.range: return new Regex(pattern.number).IsMatch(value);
                case type.date: return new Regex(pattern.date).IsMatch(value);
                case type.month: return new Regex(pattern.month).IsMatch(value);
                case type.week: return new Regex(pattern.week).IsMatch(value);
                case type.time: return new Regex(pattern.time).IsMatch(value);
                case type.datetime_local: return new Regex(pattern.datetime_local).IsMatch(value);
                case type.search: return true;
                case type.color: return new Regex(pattern.color).IsMatch(value);
                default:return true;
            }
        }
        public static bool requiredValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool maxLengthValid(string value, int len)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length <= len)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool minLengthValid(string value, int len)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length >= len)
                {
                    return true;
                }
            }
            else
            {
                if (len >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool maxValid(string value,decimal max)
        {
            if(!string.IsNullOrEmpty(value))
            {
                if(Convert.ToDecimal(value)<=max)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool minValid(string value, decimal min)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (Convert.ToDecimal(value) >= min)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool patternValid(string value, string pattern)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new Regex(pattern).IsMatch(value);
            }
            return false;
        }
    }
    /// <summary>
    /// input类型属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class typeAttribute : Attribute
    {
        public typeAttribute(string t)
        { }
    }
    /// <summary>
    /// html5验证-用户自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class setCustomValidiltyAttribute : Attribute
    {
        public setCustomValidiltyAttribute(string msg)
        { }
    }
    /// <summary>
    /// html5验证-input标签的类型
    /// </summary>
    public static class type
    {
        public const string text="text";
        public const string email="email";
        public const string url="url";
        public const string number="number";
        public const string range="range";
        public const string date="date";
        public const string month="month";
        public const string week="week";
        public const string time="time";
        public const string datetime_local="datetime-local";
        public const string search="search";
        public const string color = "color";
    }
    /// <summary>
    /// html5验证-最大长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class maxLengthAttribute:Attribute
    {
        public maxLengthAttribute(int len)
        { }
    }
    /// <summary>
    /// html5验证-最小长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class minLengthAttribute:Attribute
    {
        public minLengthAttribute(int len)
        { }
    }
    /// <summary>
    /// html5验证-最大值
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class maxAttribute:Attribute
    {
        public maxAttribute(double val)
        { }
    }
    /// <summary>
    /// html5验证-最小值
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class minAttribute:Attribute
    {
        public minAttribute(double val)
        { }
    }
    /// <summary>
    /// html5验证-一次增加或减少的数值
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class stepAttribute:Attribute
    {
        public stepAttribute(double val)
        { }
    }
    /// <summary>
    /// html5验证-输入框的提示文字
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class placeholderAttribute:Attribute
    {
        public placeholderAttribute(string msg)
        { }
    }
    //[AttributeUsage(AttributeTargets.Parameter)]
    //public class autocompleteAttribute : Attribute
    //{
    //    public autocompleteAttribute(autocomplete auto)
    //    { 
    //    }
    //}
    //public enum autocomplete
    //{
    //    on,
    //    off,
    //    unspecified
    //}

    /// <summary>
    /// html5验证-必填项
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class requiredAttribute : Attribute
    { }
    /// <summary>
    /// html5验证-正则验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class patternAttribute:Attribute
    {
        public patternAttribute(string val)
        { }
    }
    //[AttributeUsage(AttributeTargets.Parameter)]
    //public class novalidateAttribute:Attribute
    //{ 
        
    //}
}