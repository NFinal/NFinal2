//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Connection.cs
//        Description :数据库对象类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace NFinal.Tips
{
    /// <summary>
    /// 数据库连接对象对应的提示类
    /// </summary>
    public class Connection : NFinal.Tips.DBBase, System.Data.IDbConnection
    {
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns></returns>
        public static NFinal.Tips.Connection OpenConnection()
        {
            return new NFinal.Tips.Connection();
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="ConnectionName">连接字符串名称</param>
        /// <returns></returns>
        public static NFinal.Tips.Connection OpenConnection(string ConnectionName)
        {
            return new NFinal.Tips.Connection();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public NFinal.Tips.Transaction BeginTransaction()
        {
            return new NFinal.Tips.Transaction();
        }
        /// <summary>
        /// 获取事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        public NFinal.Tips.Transaction GetTransaction(NFinal.Tips.Transaction transaction)
        {
            return transaction;
        }
        /// <summary>
        /// 获取数据库连接类
        /// </summary>
        /// <param name="connection">数据库连接接口对象</param>
        /// <returns>数据库连接对象</returns>
        public static NFinal.Tips.Connection GetConnection(System.Data.IDbConnection connection)
        {
            return new NFinal.Tips.Connection();
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 数据库连接超时时间
        /// </summary>
        public int ConnectionTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 数据库连接状态
        /// </summary>
        public ConnectionState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction IDbConnection.BeginTransaction()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 切换当前数据库
        /// </summary>
        /// <param name="databaseName"></param>
        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 创建command对象
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 关闭数据库连接对象
        /// </summary>
        public void Close()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 打开数据库连接对象
        /// </summary>
        public void Open()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 释放数据库连接对象
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}