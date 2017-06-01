//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : UrlSegment.cs
//        Description :从Url表达式中解析出相关参数信息的类
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

namespace NFinal.Url
{
    /// <summary>
    /// 从Url表达式中解析出相关参数信息的类
    /// </summary>
    public class UrlSegment
    {
        /// <summary>
        /// 参数开始位置
        /// </summary>
        public int start=0;
        /// <summary>
        /// 参数长度
        /// </summary>
        public int length=0;
        /// <summary>
        /// 参数表达式
        /// </summary>
        public string expression=null;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string name=null;
        /// <summary>
        /// 生成Url时参数所需的格式化字符串
        /// </summary>
        public string format=null;
        /// <summary>
        /// 解析Url时参数所需的正则表达式
        /// </summary>
        public string regex=null;

        /// <summary>
        /// 解析出Url表达式中所包含的参数信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public UrlSegment(string url, int start, int length)
        {
            this.expression = url.Substring(start, length);
            this.start = start;
            this.length = length;
            int left = 0;
            int right = -1;
            int found = 0;
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == ':')
                {
                    if (i < 2)
                    {
                        left = right + 1;
                        right = i;
                        found++;
                        if (found == 1)
                        {
                            this.name = expression.Substring(left, right - left);
                            this.regex = expression.Substring(right + 1);
                        }
                    }
                    else
                    {
                        if (expression[i - 2] == '(' && expression[i - 1] == '?')
                        {

                        }
                        else
                        {

                            found++;
                            if (found == 1)
                            {
                                left = right + 1;
                                right = i;
                                this.name = expression.Substring(left, right - left);
                                this.regex = expression.Substring(right + 1);
                            }
                            else if (found == 2)
                            {
                                left = right + 1;
                                right = i;
                                this.regex = expression.Substring(left, right - left);
                                this.format = expression.Substring(right + 1);
                            }
                        }
                    }
                }

            }
            if (found < 1)
            {
                this.name = expression;
                this.regex = "[\\S]+";
            }
            else
            {
                if (this.regex == "string")
                {
                    this.regex = "[\\S]+";
                }
                else if (this.regex == "int")
                {
                    this.regex = "[0-9]+";
                }
                else if (this.regex == "float")
                {
                    this.regex = "[0-9]*.[0-9]+";
                }
                else if (this.regex == "date")
                {
                    this.regex = "[0-9]{4}-[0-9]{2}-[0-9]{2}";
                }
            }
        }

    }
}
