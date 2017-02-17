using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    public class UrlParser
    {
        public string actionUrl = null;
        public string formatParameterTypeAndNames = string.Empty;
        public string formatParameterNames = string.Empty;
        public string formatUrl = null;
        public string formatParameters = null;
        public string parameterRegex = null;
        public bool isSimpleUrl = true;
        public bool hasNoParamsInUrl = true;

        /// <summary>
        /// 是否是URL分隔符
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool isSeparatorChar(char ch)
        {
            bool result = false;
            char[] sc = new char[] {'-','/' };
            foreach (char c in sc)
            {
                if (c == ch)
                {
                    result = true;
                }
            }
            return result;
        }
        public UrlParser(string url, System.Collections.Generic.List<ControllerParameterData> parameters)
        {
            char[] chars = url.ToCharArray();
            int leftBraceCount = 0;
            int leftBracePosition = 0;
            int rightBraceCount = 0;
            int rightBracePosition = 0;
            //用于组装URL
            this.formatUrl = string.Empty;
            //用于解析字符串
            System.Collections.Generic.List<UrlSegment> segmentList = new System.Collections.Generic.List<UrlSegment>();
            UrlSegment segment = null;
            int firstLeftBracePosition = 0;
            isSimpleUrl = true;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                if (chars[i] == '{')
                {
                    if (chars[i + 1] == '{')
                    {
                        //chars[i]='\\';
                        i++;
                    }
                    else
                    {
                        if (firstLeftBracePosition == 0)
                        {
                            firstLeftBracePosition = i;
                        }
                        leftBraceCount++;
                        leftBracePosition = i;
                        if (leftBracePosition > 1)
                        {
                            if (!isSeparatorChar(chars[leftBracePosition-1]))
                            {
                                isSimpleUrl = false;
                            }
                        }
                        i++;
                    }
                }
                else if (chars[i] == '}')
                {
                    if (chars[i + 1] == '}')
                    {
                        if (leftBraceCount == rightBraceCount)
                        {
                            //chars[i] = '\\';
                            i++;
                        }
                        else
                        {
                            rightBraceCount++;
                            rightBracePosition = i;
                            if (rightBracePosition + 2 < chars.Length)
                            {
                                if (!isSeparatorChar(chars[rightBracePosition]))
                                {
                                    isSimpleUrl = false;
                                }
                            }
                            if (leftBraceCount == rightBraceCount)
                            {
                                segment = new UrlSegment(url, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                                segmentList.Add(segment);
                            }
                        }
                    }
                    else
                    {
                        rightBraceCount++;
                        rightBracePosition = i;

                        if (leftBraceCount == rightBraceCount)
                        {
                            segment = new UrlSegment(url, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                            segmentList.Add(segment);
                        }
                    }
                }
                if (i == chars.Length - 2 && chars[i] != '}' && chars[i + 1] == '}')
                {
                    rightBraceCount++;
                    rightBracePosition = i + 1;

                    if (leftBraceCount == rightBraceCount)
                    {
                        segment = new UrlSegment(url, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                        segmentList.Add(segment);
                    }
                }
            }
            //生成查找路由的字符串
            if (segmentList.Count > 0)
            {
                hasNoParamsInUrl = false;
                actionUrl = url.Substring(0, firstLeftBracePosition).Replace("{{", "{").Replace("}}", "}");
            }
            else
            {
                hasNoParamsInUrl = true;
                actionUrl = url;
            }
            this.formatUrl = url;
            int relitivePosition = 0;
            string format = "";
            for (int i = 0; i < segmentList.Count; i++)
            {
                this.formatUrl = this.formatUrl.Remove(segmentList[i].start + relitivePosition, segmentList[i].length);
                if (string.IsNullOrEmpty(segmentList[i].format))
                {
                    format = i.ToString();
                }
                else
                {
                    format = i.ToString() + ":" + segmentList[i].format;
                }
                this.formatUrl = this.formatUrl.Insert(segmentList[i].start + relitivePosition, format);
                relitivePosition += format.Length - segmentList[i].length;
                if (i == segmentList.Count - 1)
                {
                    string ext = this.formatUrl.Substring(segmentList[i].start + relitivePosition + segmentList[i].length);
                    int pos = ext.LastIndexOf('.');
                    if (pos > -1)
                    {
                        this.formatUrl = this.formatUrl.Insert(segmentList[i].start + relitivePosition + segmentList[i].length + pos, (actionUrl.Length - 1).ToString("00"));
                    }
                    else
                    {
                        this.formatUrl = this.formatUrl + (actionUrl.Length - 1).ToString("00");
                    }
                }
            }
            
            //生成生成URL的代码
            this.formatParameters = "";
            int index = 1;
            for (int i = 0; i < parameters.Count; i++)
            {
                parameters[i].isUrlParameter = false;
                for (int j = 0; j < segmentList.Count; j++)
                {
                    if(segmentList[j].name == parameters[i].name)
                    {
                        parameters[i].isUrlParameter = true;
                        parameters[i].urlParameterIndex = index;
                        index++;
                        this.formatParameters += "," + segmentList[j].name;
                        continue;
                    }
                }
                if (parameters[i].isUrlParameter)
                {
                    this.formatParameterTypeAndNames += parameters[i].type + (parameters[i].isArray ? "[] " : " ") + parameters[i].name + ",";
                    this.formatParameterNames += parameters[i].name + ",";
                }
                if (i == parameters.Count - 1)
                {
                    this.formatParameterTypeAndNames = this.formatParameterTypeAndNames.TrimEnd(',');
                    this.formatParameterNames = this.formatParameterNames.TrimEnd(',');
                }
            }
            relitivePosition = 0;
            //生成解析URL的代码
            parameterRegex = url;
            for (int i = 0; i < segmentList.Count; i++)
            {
                parameterRegex = parameterRegex.Remove(segmentList[i].start - 1 + relitivePosition, segmentList[i].length + 2);
                parameterRegex = parameterRegex.Insert(segmentList[i].start - 1 + relitivePosition, string.Format("({0})", segmentList[i].regex));
                relitivePosition += segmentList[i].regex.Length - segmentList[i].length;
                if (i == segmentList.Count - 1)
                {
                    string ext = parameterRegex.Substring(segmentList[i].start + relitivePosition + segmentList[i].length);
                    int pos = ext.LastIndexOf('.');
                    if (pos > -1)
                    {
                        parameterRegex = parameterRegex.Insert(segmentList[i].start + relitivePosition + segmentList[i].length + pos, (actionUrl.Length - 1).ToString("00"));
                    }
                    else
                    {
                        parameterRegex = parameterRegex + (actionUrl.Length - 1).ToString("00");
                    }
                }
            }
            parameterRegex = parameterRegex.Replace("{{", "{").Replace("}}", "}");
            
        }
    }
}
