using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Common.CloudStorage
{
    public class QiNiuStorage:StorageBase,StorageInterface
    {
        public static string VERSION = "6.1.8";
        public static string USER_AGENT = getUa();
        private static string getUa()
        {
            return "QiniuCsharp/" + VERSION + " (" + Environment.OSVersion.Version.ToString() + "; )";
        }
        #region 七牛服务器地址
        /// <summary>
        /// 七牛资源管理服务器地址
        /// </summary>
        public static string RS_HOST = "http://rs.Qbox.me";
        /// <summary>
        /// 七牛资源上传服务器地址.
        /// </summary>
        public static string UP_HOST = "http://up.qiniu.com";
        /// <summary>
        /// 七牛资源列表服务器地址.
        /// </summary>
        public static string RSF_HOST = "http://rsf.Qbox.me";

        public static string PREFETCH_HOST = "http://iovip.qbox.me";

        public static string API_HOST = "http://api.qiniu.com";
        #endregion
        public bool StorageInit(StorageInfo info)
        {
            return true;
        }
        //用户信息验证
        public bool Authentication(StorageInfo info)
        {
            return true;
        }
        //写权限
        public bool PutACL()
        {
            return true;
        }
        //获取权限
        public bool GetACL()
        {
            return true;
        }
        //创建bucket
        public bool CreateBucket() 
        {
            return true;
        }
        //获取bucket
        public bool GetBuckets()
        {
            return true;
        }
        //删除bucket
        public bool DeleteBucket()
        {
            return true;
        }
        //上传文件
        public string PutObject(string fileName, string localFileName)
        {
            return string.Empty;
        }
        //获取文件
        public bool GetObject(string fileName)
        {
            return true;
        }
        //删除文件
        public bool DeleteObject(string fileName)
        {
            return true;
        }
    }
}