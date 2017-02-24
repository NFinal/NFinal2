using System;
using System.Collections.Generic;

namespace NFinal.Common.CloudStorage
{
    /// <summary>
    /// 云存储帐号信息
    /// </summary>
    public class StorageInfo
    {
        public string accessKey;
        public string secretKey;
        public string name;
        public string password;
        public string bucket;
        public StorageInfo(string name, string password, string accessKey, string secretKey, string bucket)
        {
            this.name = name;
            this.password = password;
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            this.bucket = bucket;
        }
    }
}