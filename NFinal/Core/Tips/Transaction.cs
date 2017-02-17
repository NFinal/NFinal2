//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Transation.cs
//        Description :事务处理类
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
    /// SQL事务魔法类
    /// </summary>
    public class Transaction:NFinal.Tips.DBBase,System.Data.IDbTransaction
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        { 
            
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        { 
        
        }
        /// <summary>
        /// 释放事务对象
        /// </summary>
        public void Dispose()
        {
        }
        /// <summary>
        /// 事务异常
        /// </summary>
        public DBException dbExcetion;

        /// <summary>
        /// 事务对应的数据库连接对象
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 指定事务的锁定行为
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
    /// <summary>
    /// 数据库异常类
    /// </summary>
    public class DBException : System.Exception
    { 
    
    }
}