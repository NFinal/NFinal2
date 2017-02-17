using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Core.Action
{
    public static class HttpCache
    {
        /// <summary>  
        /// 本地时间转成GMT时间  
        /// </summary>  
        public static string ToGmtString(DateTime dt)
        {
            return dt.ToUniversalTime().ToString("r");
        }
        /// <summary>  
        /// 本地时间转成GMT格式的时间  
        /// </summary>  
        public static string ToGmtFormat(DateTime dt)
        {
            return dt.ToString("r") + dt.ToString("zzz").Replace(":", "");
        }
        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime Gmt2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }
        private static void Callback(IAsyncResult result)
        {
            //结束异步写入
            System.IO.FileStream stream = (System.IO.FileStream)result.AsyncState;
            stream.EndWrite(result);
            stream.Dispose();
        }
        //设置浏览器缓存
        public static void SetBrowerCache(NFinal.Owin.HtmlWriter writer, string requestPath, int optimizing, int seconds)
        {
            //浏览器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Brower_Cached) != 0)
            {
                writer.WriteHeader("Cache-Control", "public");
                writer.WriteHeader("Date", ToGmtString(DateTime.Now));
                writer.WriteHeader("Date", ToGmtString(DateTime.Now));
                //NotModify
                if ((optimizing & (int)NFinal.Optimizing.Cache_Browse_NotModify) != 0)
                {
                    string filename = NFinal.Utility.MapPath(requestPath);
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                        string lastModified = ToGmtString(fileInfo.LastWriteTime);
                        writer.WriteHeader("Last-Modified", lastModified);
                    }
                    else
                    {
                        writer.WriteHeader("Last-Modified", ToGmtString(DateTime.Now));
                    }
                }
                //Expires
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Browser_Expires) != 0)
                {
                    writer.WriteHeader("Expires", ToGmtString(DateTime.Now.AddSeconds(seconds)));
                }
                //NoExpires
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Browser_NoExpires) != 0)
                {
                }
            }
            else
            {
                //不进行缓存
                writer.WriteHeader("Cache-Control", "private");
            }
        }
        
        //设置服务器缓存
        public static void SetServerCacheAndOutPut(NFinal.Owin.HtmlWriter writer,ICache cache, string requestPath, string query, byte[] buffer, int optimizing, int seconds)
        {
            //服务器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0)
            {
                //FileDependency
                if ((optimizing & (int)NFinal.Optimizing.Cache_Server_FileDependency) != 0)
                {
                    string fileName = null;
                    if (requestPath.EndsWith("/"))
                    {
                        fileName = NFinal.Utility.MapPath(requestPath + "Index.html");
                    }
                    else
                    {
                        fileName = NFinal.Utility.MapPath(requestPath);
                    }
                    if ((optimizing & (int)NFinal.Optimizing.CompressZip) != 0)
                    {
                        fileName = fileName + ".zip";
                    }
                    else if ((optimizing & (int)NFinal.Optimizing.CompressDeflate) != 0)
                    {
                        fileName = fileName + ".deflate";
                    }
                    //将HTML写入静态文件
                    if (!System.IO.File.Exists(fileName))
                    {
                        string dir = System.IO.Path.GetDirectoryName(fileName);
                        if (!System.IO.Directory.Exists(dir))
                        {
                            System.IO.Directory.CreateDirectory(dir);
                        }
                        System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite, 1024, true);
                        file.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Callback), file);
                    }
                    if (seconds > 0)
                    {
                        //将HTML插入新的缓存
                        cache.Set(requestPath + query, buffer);
                    }
                }
                //AbsoluteExpiration
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Server_AbsoluteExpiration) != 0)
                {
                    cache.Set(requestPath +query ,buffer);
                }
                //SlidingExpiration
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Server_SlidingExpiration) != 0)
                {
                    cache.Set(requestPath + query, buffer);
                }
            }
            //直接输出
            writer.Write(buffer, 0, buffer.Length);
            writer.Close();
        }
        //写入缓存
        public static bool WriteCache(NFinal.Owin.HtmlWriter writer,ICache cache, string requestPath, int optimizing)
        {
            //服务器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0)
            {
                byte[] data = cache.Get(requestPath);
                //如果缓存存在则直接输出.
                if (data != null)
                {
                    if ((optimizing & (int)NFinal.Optimizing.Comresssed) != 0)
                    {
                        if ((optimizing & (int)NFinal.Optimizing.CompressZip) != 0)
                        {
                            writer.WriteHeader("Content-encoding", "gzip");
                        }
                        else if ((optimizing & (int)NFinal.Optimizing.CompressDeflate) != 0)
                        {
                            writer.WriteHeader("Content-encoding", "deflate");
                        }
                    }
                    byte[] buffer = data;
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Close();
                    return true;
                }
            }
            return false;
        }
    }
}
