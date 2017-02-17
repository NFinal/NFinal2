using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/*******************************************
 * 压缩jsp,html中的代码，去掉所有空白符、换行符
 * @author  bearrui(ak-47)
 * @version 0.1
 * @date     2010-5-13
 *******************************************/
namespace NFinal.Compile.Template
{
    /// <summary>
    /// Html代码压缩类
    /// </summary>
    public class HtmlCompressor
    {
        //<!--注释-->
        private static String tempSingleCommentBlock = "%%%HTMLCOMPRESS~SINGLECOMMENT&&&";  // //占位符
        private static Regex commentPattern = new Regex("<!--\\s*[^\\[].*?-->",RegexOptions.Singleline);
        //<pre></pre>
        private static String tempPreBlock = "%%%HTMLCOMPRESS~PRE&&&";
        private static Regex prePattern = new Regex("<pre[^>]*?>.*?</pre>",  RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //<textarea></texarea>
        private static String tempTextAreaBlock = "%%%HTMLCOMPRESS~TEXTAREA&&&";
        private static Regex taPattern = new Regex("<textarea[^>]*?>.*?</textarea>",  RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //Asp.Net代码段<%%>
        private static String tempAspBlock = "%%%HTMLCOMPRESS~JSP&&&";
        private static Regex aspPattern = new Regex("<%([^-@][\\w\\W]*?)%>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        // <script></script>
        private static String tempScriptBlock = "%%%HTMLCOMPRESS~SCRIPT&&&";
        private static Regex scriptPattern = new Regex("(?:<script\\s*>|<script type=['\"]text/javascript['\"]\\s*>)(.*?)</script>",  RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
        //<style></style>
        private static String tempStyleBlock = "%%%HTMLCOMPRESS~STYLE&&&";
        private static Regex stylePattern = new Regex("<style[^>()]*?>(.+)</style>",  RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
        //<div>   </div>空行
        private static Regex itsPattern = new Regex(">\\s+?<");
        // 单行注释
        private static Regex signleCommentPattern = new Regex("//.*");
        // 字符串匹配
        private static Regex stringPattern = new Regex("(\"[^\"\\n]*?\"|'[^'\\n]*?')");
        // trim去空格和换行符
        private static Regex trimPattern = new Regex("\\n\\s*",  RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static Regex trimPattern2 = new Regex("\\s*\\r",  RegexOptions.IgnoreCase | RegexOptions.Multiline);
        /*多行注释*/
        private static Regex multiCommentPattern = new Regex("/\\*.*?\\*/",  RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static String tempMulitCommentBlock1 = "%%%HTMLCOMPRESS~MULITCOMMENT1&&&";  // /*占位符
        private static String tempMulitCommentBlock2 = "%%%HTMLCOMPRESS~MULITCOMMENT2&&&";  // */占位符


        public static String compress(String fileName, System.Text.Encoding encoding)
        {
            StreamReader sr = new StreamReader(fileName, encoding);
            string html= sr.ReadToEnd();
            sr.Close();
            return compress(html);
        }
        public static bool hasAsp(String html)
        {
            Match jspMatcher = aspPattern.Match(html);
            return jspMatcher.Success;
        }
        public static String compress(String html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            System.Collections.Generic.List<String> preBlocks = new System.Collections.Generic.List<String>();
            System.Collections.Generic.List<String> textareaBlocks = new System.Collections.Generic.List<String>();
            System.Collections.Generic.List<String> scriptBlocks = new System.Collections.Generic.List<String>();
            System.Collections.Generic.List<String> styleBlocks = new System.Collections.Generic.List<String>();
            System.Collections.Generic.List<String> jspBlocks = new System.Collections.Generic.List<String>();

            String result = html;

            //preserve inline .net code
            Match aspMatcher = aspPattern.Match(result);

            while (aspMatcher.Success)
            {
                jspBlocks.Add(aspMatcher.Value);
                aspMatcher = aspMatcher.NextMatch();
            }
            result = aspPattern.Replace(result, tempAspBlock);

            //preserve PRE tags
            Match preMatcher = prePattern.Match(result);
            while (preMatcher.Success)
            {
                preBlocks.Add(preMatcher.Value);
                preMatcher = preMatcher.NextMatch();
            }
            result = prePattern.Replace(result, tempPreBlock);

            //preserve TEXTAREA tags
            Match taMatcher = taPattern.Match(result);
            while (taMatcher.Success)
            {
                textareaBlocks.Add(taMatcher.Value);
                taMatcher = taMatcher.NextMatch();
            }
            result = taPattern.Replace(result, tempTextAreaBlock);

            //preserve SCRIPT tags
            Match scriptMatcher = scriptPattern.Match(result);
            while (scriptMatcher.Success)
            {
                scriptBlocks.Add(scriptMatcher.Value);
                scriptMatcher = scriptMatcher.NextMatch();
            }
            result = scriptPattern.Replace(result, tempScriptBlock);

            // don't process inline css 
            Match styleMatcher = stylePattern.Match(result);
            while (styleMatcher.Success)
            {
                styleBlocks.Add(styleMatcher.Value);
                styleMatcher = styleMatcher.NextMatch();
            }
            result = stylePattern.Replace(result, tempStyleBlock);

            //process pure html
            result = processHtml(result);

            //process preserved blocks
            result = processPreBlocks(result, preBlocks);
            result = processTextareaBlocks(result, textareaBlocks);
            result = processScriptBlocks(result, scriptBlocks);
            result = processStyleBlocks(result, styleBlocks);
            result = processAspBlocks(result, jspBlocks);

            preBlocks = textareaBlocks = scriptBlocks = styleBlocks = jspBlocks = null;

            return result.Trim();
        }

        private static String processHtml(String html)
        {
            String result = html;

            //remove comments
            //		if(removeComments) {
            result = commentPattern.Replace(result, "");
            //		}

            //remove inter-tag spaces
            //		if(removeIntertagSpaces) {
            result = itsPattern.Replace(result, "><");
            //		}

            //remove multi whitespace characters
            //		if(removeMultiSpaces) {
            result = new Regex("\\s{2,}").Replace(result, " ");
            //      }
            return result;
        }

        private static String processAspBlocks(String html, System.Collections.Generic.List<String> blocks)
        {
            String result = html;
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = compressAsp(blocks[i]);
            }
            //put preserved blocks back
            Regex regex = new Regex(tempAspBlock);
            for (int i = 0; i < blocks.Count; i++)
            {
                result = regex.Replace(result, blocks[i], 1);
            }

            return result;
        }
        private static String processPreBlocks(String html, System.Collections.Generic.List<String> blocks)
        {
            String result = html;

            //put preserved blocks back

            Regex regex = new Regex(tempPreBlock);
            for (int i = 0; i < blocks.Count; i++)
            {
                result = regex.Replace(result, blocks[i], 1);
            }

            return result;
        }

        private static String processTextareaBlocks(String html, System.Collections.Generic.List<String> blocks)
        {
            String result = html;
            //put preserved blocks back
            Regex regex = new Regex(tempTextAreaBlock);
            for (int i = 0; i < blocks.Count; i++)
            {
                result = regex.Replace(result, blocks[i], 1);
            }

            return result;
        }

        private static String processScriptBlocks(String html, System.Collections.Generic.List<String> blocks)
        {
            String result = html;

            //		if(compressJavaScript) {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = compressJavaScript(blocks[i]);
            }
            //		}

            //put preserved blocks back
            Regex regex = new Regex(tempScriptBlock);
            for (int i = 0; i < blocks.Count; i++)
            {
                result=regex.Replace(result, blocks[i], 1);
            }

            return result;
        }

        private static String processStyleBlocks(String html, System.Collections.Generic.List<String> blocks)
        {
            String result = html;

            //		if(compressCss) {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i] = compressCssStyles(blocks[i]);
            }
            //		}

            //put preserved blocks back
            Regex regex = new Regex(tempStyleBlock);
            for (int i = 0; i < blocks.Count; i++)
            {
                result = regex.Replace(result, blocks[i], 1);
            }

            return result;
        }

        private static String compressAsp(String source)
        {
            //check if block is not empty
            Match aspMatcher = aspPattern.Match(source);
            if (aspMatcher.Success)
            {
                String result = compressAspJs(aspMatcher.Groups[1].Value);
                return (new StringBuilder(source.Substring(0, aspMatcher.Groups[1].Index)).Append(result).Append(source.Substring(aspMatcher.Groups[1].Index + aspMatcher.Groups[1].Length))).ToString();
            }
            else
            {
                return source;
            }
        }
        private static String compressJavaScript(String source)
        {
            //check if block is not empty
            Match scriptMatcher = scriptPattern.Match(source);
            if (scriptMatcher.Success)
            {
                String result = compressAspJs(scriptMatcher.Groups[1].Value);
                return (new StringBuilder(source.Substring(0, scriptMatcher.Groups[1].Index)).Append(result).Append(source.Substring(scriptMatcher.Groups[1].Index + scriptMatcher.Groups[1].Length))).ToString();
            }
            else
            {
                return source;
            }
        }

        private static String compressCssStyles(String source)
        {
            //check if block is not empty
            Match styleMatcher = stylePattern.Match(source);
            if (styleMatcher.Success)
            {
                // 去掉注释，换行
                String result = multiCommentPattern.Replace(styleMatcher.Groups[1].Value, "");
                result = trimPattern.Replace(result, "");
                result = trimPattern2.Replace(result, "");
                return (new StringBuilder(source.Substring(0, styleMatcher.Groups[1].Index)).Append(result).Append(source.Substring(styleMatcher.Groups[1].Index + styleMatcher.Groups[1].Length))).ToString();
            }
            else
            {
                return source;
            }
        }

        private static String compressAspJs(String source)
        {
            String result = source;
            // 因注释符合有可能出现在字符串中，所以要先把字符串中的特殊符好去掉
            Match stringMatcher = stringPattern.Match(result);
            while (stringMatcher.Success)
            {
                String tmpStr = stringMatcher.Value;

                if (tmpStr.IndexOf("//") != -1 || tmpStr.IndexOf("/*") != -1 || tmpStr.IndexOf("*/") != -1)
                {
                    String blockStr = tmpStr.Replace("//", tempSingleCommentBlock);
                    blockStr = new Regex("/\\*").Replace(blockStr, tempMulitCommentBlock1);
                    blockStr = new Regex("\\*/").Replace(blockStr, tempMulitCommentBlock2);
                    result = result.Replace(tmpStr, blockStr);
                }
                stringMatcher = stringMatcher.NextMatch();
            }
            // 去掉注释
            result = signleCommentPattern.Replace(result, "");
            result = multiCommentPattern.Replace(result, "");
            //修正注释中有jsp解析错误的bug.
            result = trimPattern2.Replace(result, "");
            result = trimPattern.Replace(result, " ");
            // 恢复替换掉的字符串
            result = result.Replace(tempSingleCommentBlock, "//").Replace(tempMulitCommentBlock1, "/*")
                    .Replace(tempMulitCommentBlock2, "*/");

            return result;
        }
    }
}