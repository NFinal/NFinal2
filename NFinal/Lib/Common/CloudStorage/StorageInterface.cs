using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Common.CloudStorage
{
    //去存储基本操作
    public interface StorageInterface
    {
        bool StorageInit(StorageInfo info);
        //用户信息验证
        bool Authentication(StorageInfo info);
        //写权限
        bool PutACL();
        //获取权限
        bool GetACL();
        //创建bucket
        bool CreateBucket();
        //获取bucket
        bool GetBuckets();
        //删除bucket
        bool DeleteBucket();
        //上传文件
        string PutObject(string fileName, string localFileName);
        //获取文件
        bool GetObject(string fileName);
        //删除文件
        bool DeleteObject(string fileName);
    }
}