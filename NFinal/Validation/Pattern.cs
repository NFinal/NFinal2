//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Pattern.cs
//        Description :常用的正则表达式
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFinal.Validation
{
    /// <summary>
    /// 常用正则验证表达式
    /// </summary>
    public static class Pattern
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public const string email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        /// <summary>
        /// URL
        /// </summary>
        public const string url = @"[a-zA-z]+://[^\s]*";
        /// <summary>
        /// 数字
        /// </summary>
        public const string number = @"^[0-9]+(\.[0-9]+)?$";
        /// <summary>
        /// 域名
        /// </summary>
        public const string domain = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?";
        /// <summary>
        /// 手机
        /// </summary>
        public const string mobile = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";
        /// <summary>
        /// 电话
        /// </summary>
        public const string tel = @"\d{3}-\d{8}|\d{4}-\d{7}";
        /// <summary>
        /// 身份证
        /// </summary>
        public const string idcard = @"^\d{15}|\d{18}$";
        /// <summary>
        /// 中文
        /// </summary>
        public const string chinese = @"[\u4e00-\u9fa5]";
        /// <summary>
        /// qq号
        /// </summary>
        public const string qq = @"[1-9][0-9]{4,} ";
        /// <summary>
        /// 邮编
        /// </summary>
        public const string postcode = @"[1-9]\d{5}(?!\d)";
        /// <summary>
        /// ip
        /// </summary>
        public const string ip = @"\d+\.\d+\.\d+\.\d+";
        /// <summary>
        /// (字母开头，允许5-16字节，允许字母数字下划线)
        /// </summary>
        public const string account = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$";
        /// <summary>
        /// 以字母开头，长度在6-18之间,只能包含字符、数字和下划线。
        /// </summary>
        public const string pwd = @"^[a-zA-Z]w{5,17}$";
        /// <summary>
        /// 正整数
        /// </summary>
        public const string zhengzhengshu = @"^[1-9]\d*$";
        /// <summary>
        /// 负整数
        /// </summary>
        public const string fuzhengshu = @"^-[1-9]\d*$";
        /// <summary>
        /// 整数
        /// </summary>
        public const string zhengshu = @"^-?[1-9]\d*$";
        /// <summary>
        /// 非负整数
        /// </summary>
        public const string feifuzhengshu = @"^[1-9]\d*|0$";
        /// <summary>
        /// 非正整数
        /// </summary>
        public const string feizhengzhengshu = @"^-[1-9]\d*|0$";
        /// <summary>
        /// 正浮点数
        /// </summary>
        public const string zhengfudianshu = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$";
        /// <summary>
        /// 负浮点数
        /// </summary>
        public const string fufudianshu = @"^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$";
        /// <summary>
        /// 浮点数
        /// </summary>
        public const string fudianshu = @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$";
        /// <summary>
        /// 非负浮点数
        /// </summary>
        public const string feifufudianshu = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$";
        /// <summary>
        /// 非正浮点数
        /// </summary>
        public const string feizhengfudianshu = @"^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$";
        /// <summary>
        /// 日期
        /// </summary>
        public const string date = @"^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))$";
        /// <summary>
        /// 月
        /// </summary>
        public const string month = @"^[0-9]{4}-(1|2|3|4|5|6|7|8|9|10|11|12)$";
        /// <summary>
        /// 周
        /// </summary>
        public const string week = @"^[0-9]{4}-[0-9][0-9]?$";
        /// <summary>
        /// 时间
        /// </summary>
        public const string time = @"^(([1-9]{1})|([0-1][0-9])|([1-2][0-3])):([0-5][0-9])$";
        /// <summary>
        /// 本地日期和时间
        /// </summary>
        public const string datetime_local = @"^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30))) (([1-9]{1})|([0-1][0-9])|([1-2][0-3])):([0-5][0-9])$";
        /// <summary>
        /// 颜色
        /// </summary>
        public const string color = @"\#[0-9a-fA-F]{6}";
    }
}