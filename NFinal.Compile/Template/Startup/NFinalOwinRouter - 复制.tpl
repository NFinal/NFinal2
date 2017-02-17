﻿﻿//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Router.cs
//        Description :路由类
//
//        created by Lucas at  2015-10-15`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ${project}
{
#if (!AspNET && !MicrosoftOwin)
    public static class NFinalOwinRouter
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
        public static void SetBrowerCache(NFinal.Action action,string requestPath, int optimizing,int minutes)
        {
            //浏览器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Brower_Cached) != 0)
            {
                action.SetResponseHeader("Cache-Control","public");
                action.SetResponseHeader("Date",ToGmtString(DateTime.Now));
                action.SetResponseHeader("Date",ToGmtString(DateTime.Now));
                //NotModify
                if ((optimizing & (int)NFinal.Optimizing.Cache_Browse_NotModify) != 0)
                {
                    string filename = NFinal.Utility.MapPath(requestPath);
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                        string lastModified = ToGmtString(fileInfo.LastWriteTime);
                        action.SetResponseHeader("Last-Modified", lastModified);
                    }
                    else
                    {
                        action.SetResponseHeader("Last-Modified", ToGmtString(DateTime.Now));
                    }
                }
                //Expires
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Browser_Expires) != 0)
                {
                    action.SetResponseHeader("Expires", ToGmtString(DateTime.Now.AddMinutes(minutes)));
                }
                //NoExpires
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Browser_NoExpires) != 0)
                {
                }
            }
            else
            {
                //不进行缓存
                action.SetResponseHeader("Cache-Control","private");
            }
        }
        //设置服务器缓存
        public static void SetServerCache(string requestPath, string query, byte[] buffer, int optimizing, int seconds)
        {
            //服务器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0)
            {
                //FileDependency
                if ((optimizing & (int)NFinal.Optimizing.Cache_Server_FileDependency) != 0)
                {
                    string fileName=null;
					if(requestPath.EndsWith("/"))
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
						System.Runtime.Caching.CacheItemPolicy itemPolicy = new System.Runtime.Caching.CacheItemPolicy();
						itemPolicy.ChangeMonitors.Add(new System.Runtime.Caching.HostFileChangeMonitor(new string []{fileName }));
						System.Runtime.Caching.MemoryCache.Default.Set(new System.Runtime.Caching.CacheItem(requestPath+query,buffer),itemPolicy);
					}
                }
                //AbsoluteExpiration
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Server_AbsoluteExpiration) != 0)
                {
                    System.Runtime.Caching.CacheItemPolicy itemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                    itemPolicy.AbsoluteExpiration=DateTimeOffset.Now.AddSeconds(seconds);
                    System.Runtime.Caching.MemoryCache.Default.Set(new System.Runtime.Caching.CacheItem(requestPath + query, buffer), itemPolicy);
                }
                //SlidingExpiration
                else if ((optimizing & (int)NFinal.Optimizing.Cache_Server_SlidingExpiration) != 0)
                {
                    System.Runtime.Caching.CacheItemPolicy itemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                    itemPolicy.SlidingExpiration=new TimeSpan(0, 0, seconds);
                    System.Runtime.Caching.MemoryCache.Default.Set(new System.Runtime.Caching.CacheItem(requestPath + query,buffer),itemPolicy);
                }
            }
        }
        //写入缓存
        public static bool WriteCache(NFinal.Action action,string requestPath,int optimizing)
        {
            //服务器缓存
            if ((optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0)
            {
                object cache = System.Runtime.Caching.MemoryCache.Default.Get(requestPath);
                //如果缓存存在则直接输出.
                if (cache != null)
                {
                    if ((optimizing & (int)NFinal.Optimizing.Comresssed) != 0)
                    {
                        if ((optimizing & (int)NFinal.Optimizing.CompressZip) != 0)
                        {
                            action.SetResponseHeader("Content-encoding", "gzip");
                        }
                        else if ((optimizing & (int)NFinal.Optimizing.CompressDeflate) != 0)
                        {
                            action.SetResponseHeader("Content-encoding", "deflate");
                        }
                    }
                    byte[] buffer= (byte[])cache;
                    action.Write(buffer, 0, buffer.Length);
                    action.Close();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 计算actionUrl
        /// </summary>
        /// <param name="requestedPath"></param>
        /// <returns></returns>
        private static string GetActionUrl(string requestedPath)
        {
            int position = requestedPath.LastIndexOf('.');
            string actionUrl = "";
            if (position > 0)
            {
				int pointer = 0;
                bool hasGet = int.TryParse(requestedPath.Substring(position - 2, 2), out pointer);
                if (hasGet)
                {
                    actionUrl = requestedPath.Substring(0, pointer + 1);
                }
                else
                {
                    actionUrl = requestedPath;
                }
            }
            return actionUrl;
        }
        public static void Run(IDictionary<string,object> enviroment,int actionId)
        {
			NFinal.Owin.Request request = new NFinal.Owin.Request(enviroment);
			NFinal.NameValueCollection get = request.get;
            //获取actionUrl,用于获取参数
			switch (actionId)
            {
				${set(i=1)}
				${foreach(fileData in controllerFileDataList)}
				${set(config=fileData.config)}
				${set(fileDataClassDataList=fileData.ClassDataList)}
                ${foreach(classData in fileDataClassDataList)}
				${set(classDataMethodDataList=classData.MethodDataList)}
				${foreach(method in classDataMethodDataList)}
					${if(method.isPublic)}
					case @i:
					{
						${if(method.urlParser.hasNoParamsInUrl)}
						}else{
							Regex regParameters = new Regex("${method.urlParser.parameterRegex}");
							Match matParameters = regParameters.Match(request.requestPath);
							${set(methodparameterDataList=method.parameterDataList)}
							${set(j=1)}
							${foreach(parameterData in methodparameterDataList)}
							${if(parameterData.isUrlParameter)}
							request.get.Add("${parameterData.name}", matParameters.Groups[${j}].Value);
							${set(j=j+1)}
							}
							}
						}
						${method.verifyCode}
						${if((method.optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0)}
						NFinal.CompressMode compressMode = NFinal.CompressMode.None;
                        if ((optimizing & (int)NFinal.Optimizing.CompressZip) != 0)
                        {
                            compressMode = NFinal.CompressMode.GZip;
                        }
                        if ((optimizing & (int)NFinal.Optimizing.CompressDeflate) != 0)
                        {
                            compressMode = NFinal.CompressMode.Deflate;
                        }
						${fileData.appName}.Web.${config.defaultStyle}${classData.relativeDotName}.${method.name}Action control= new ${fileData.appName}.Web.${config.defaultStyle}${classData.relativeDotName}.${method.name}Action(enviroment,request,compressMode);
						control.SetResponseHeader("Content-Type", "${method.contentType}");
						${set(methodparameterDataList=method.parameterDataList)}
						$foreach(parameterData in methodparameterDataList)
							${parameterData.getParamterCode}
						$end
						control._methodType = request.methodType;
						control._subdomain = request.subdomain;
						control._url=request.requestPath;
						control._get = request.get;
						control._files = request.files;
						control._app="/${fileData.appName}";
						control.Before();
						control.${method.name}(${method.parameterNames});
						control.After();
						control.Close();
						}else{
						//优化选项
                        int optimizing = ${method.optimizing};
                        int minutes = ${method.minutes};
                        string url = request.requestPath;
						string query=request.queryString;
                        bool compress = (optimizing & (int)NFinal.Optimizing.Comresssed)!=0;
                        //设置浏览器缓存
                        SetBrowerCache(writer,url, optimizing, seconds);
                        //是否启用了服务器缓存
                        bool serverCahce = (optimizing & (int)NFinal.Optimizing.Cache_Server_Cached) != 0;
                        bool hasCache = false;
                        //服务器缓存
                        if (serverCahce)
                        {
							//如果是文件缓存，不加query
							if ((optimizing & (int)NFinal.Optimizing.Cache_Server_FileDependency) != 0)
							{
								hasCache=WriteCache(writer,url,optimizing);
							}
                            else
							{
								hasCache=WriteCache(writer,url+query,optimizing);
							}
                        }
                        //如果没有缓存
                        if (!hasCache)
                        {
                            System.IO.MemoryStream htmlStream = new System.IO.MemoryStream();
                                    
                            ${fileData.appName}.Web.${config.defaultStyle}${classData.relativeDotName}.${method.name}Action control = null;
                            System.IO.Stream stream = null;
                            System.IO.StreamWriter sw = null;
                            //如果有压缩或缓存
                            if (compress || serverCahce)
                            {
                                if ((optimizing & (int)NFinal.Optimizing.Comresssed) != 0)
                                {
                                    if ((optimizing & (int)NFinal.Optimizing.CompressZip) != 0)
                                    {
                                        stream = new System.IO.Compression.GZipStream(htmlStream, System.IO.Compression.CompressionMode.Compress, true);
                                        writer.WriteHeader("Content-Encoding","gzip");
                                    }
                                    else if ((optimizing & (int)NFinal.Optimizing.CompressDeflate) != 0)
                                    {
                                        stream = new System.IO.Compression.DeflateStream(htmlStream, System.IO.Compression.CompressionMode.Compress, true);
                                        writer.WriteHeader("Content-Encoding", "deflate");
                                    }
                                    sw = new System.IO.StreamWriter(stream);
                                }
                                else
                                {
                                    sw = new System.IO.StreamWriter(htmlStream);
                                }
                                control = new ${fileData.appName}.Web.${config.defaultStyle}${classData.relativeDotName}.${method.name}Action(sw);
                            }
                            else
                            {
                                control = new ${fileData.appName}.Web.${config.defaultStyle}${classData.relativeDotName}.${method.name}Action(writer,request);
                            }
							${set(methodparameterDataList=method.parameterDataList)}
							$foreach(parameterData in methodparameterDataList)
								${parameterData.getParamterCode}
							$end
                            control._subdomain = request.subdomain;
                            control._url = request.requestPath;
                            control._get = request.get;
                            control._app="/${fileData.appName}";
                            control.Before();
                            control.${method.name}(${method.parameterNames});
                            control.After();
                            control.Close();
                            byte[] buffer = htmlStream.ToArray();
							htmlStream.Dispose();
                            //如果有压缩或缓存
                            if (compress || serverCahce)
                            {
                                SetServerCacheAndOutPut(writer, url, query, buffer, optimizing, seconds);
                            }	
						}
						}
					}break;
					i++;
					}
				}
				}
				}
                default: break;
            }
        }
    }
#endif
}