using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile.DB
{
    public class DBCommand
    {
        /// <summary>
        /// 执行SQL并返回ID
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Insert(string sql)
        {
            return 0;
        }
        public int Update(string sql)
        {
            return 0;
        }
        public int Delete(string sql)
        {
            return 0;
        }
        /// <summary>
        /// 执行SQL并返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            return 0;
        }
    }
}