using System;
using System.Collections.Generic;

namespace NFinal.Common.CloudStorage
{
    /// <summary>
    /// 云存储类型
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// 七牛
        /// </summary>
        QiNiu = 1,
        /// <summary>
        /// 阿里
        /// </summary>
        Ali = 2,
        /// <summary>
        /// 又拍
        /// </summary>
        YouPai = 3,
        /// <summary>
        /// 百度
        /// </summary>
        BaiDu = 4
    }
}