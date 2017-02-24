using System;
using System.Collections.Generic;

namespace NFinal.Common.CloudStorage
{
    //获取云存储操作对象
    public class StorageFactory
    {
        private StorageInfo data;
        public void GetStoreInfo(string name, string password, string ak, string sk, string bucket)
        {
            data = new StorageInfo(name, password, ak, sk, bucket);
        }
        public static StorageInterface GetStorage(StorageType csType, StorageInfo sData)
        {
            if (csType == StorageType.QiNiu)
            {
                return new QiNiuStorage();
            }
            else
            {
                return null;
            }
        }
    }
}