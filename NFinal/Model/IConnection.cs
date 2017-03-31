using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NFinal.Model
{
    public interface IConnection
    {
        DBType dbType { get; set; }
        IDbConnection Con { get; }
        //int Insert<TModel>() where TModel : class;
        //int InsertKeyInt<TModel>() where TModel : class;
        //long InsertKeyLong<TModel>() where TModel : class;
        //bool Update<TModel>() where TModel : class;
        //bool Delete<TModel>(string strWhere) where TModel : class;
        void CloseConnection();
        IDbConnection GetDbConnection();
    }
}
