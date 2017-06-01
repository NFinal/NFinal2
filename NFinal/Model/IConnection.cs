//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IConnection.cs
//        Description :数据库连接接口，用于限制用户必须实现GetDbConnection方法，以及存储数据库类型。
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
using System.Data;

namespace NFinal.Model
{
    /// <summary>
    /// 数据库连接接口，用于规范用户必须实现GetDbConnection方法，以及存储数据库类型。
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DBType dbType { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        IDbConnection Con { get; }
        //int Insert<TModel>() where TModel : class;
        //int InsertKeyInt<TModel>() where TModel : class;
        //long InsertKeyLong<TModel>() where TModel : class;
        //bool Update<TModel>() where TModel : class;
        //bool Delete<TModel>(string strWhere) where TModel : class;
        /// <summary>
        /// 关闭连接
        /// </summary>
        void CloseConnection();
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();
    }
}
