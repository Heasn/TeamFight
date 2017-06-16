// ****************************************
// FileName:IDatabase.cs
// Description:数据库接口
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System;
using System.Data.Common;

namespace TeamFight.Tools.Database
{
    public interface IDatabase: IDisposable
    {
        ///<summary>
        ///打开数据库
        ///</summary>
        ///<param name="connString">连接字符串</param>
        void Open(string connString);

        ///<summary>
        ///关闭数据库
        ///</summary>
        void Close();

        ///<summary>
        ///数据连接
        ///</summary>
        DbConnection Connection { get; }

        ///<summary>
        ///获取数据库类型
        ///</summary>
        ///<returns>数据库类型</returns>
        DatabaseType GetDatabaseType();

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>影响行数</returns>
        int ExecuteNonQuery(string sql);

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="cmd">数据命令</param>
        ///<returns>影响行数</returns>
        int ExecuteNonQuery(DbCommand cmd);

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>第一行第一列值</returns>
        object ExecuteScalar(string sql);

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="cmd">数据命令</param>
        ///<returns>第一行第一列值</returns>
        object ExecuteScalar(DbCommand cmd);

        ///<summary>
        ///返回DataReader对象
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>DataReader对象</returns>
        DbDataReader ExecuteReader(string sql);

        ///<summary>
        ///返回DataReader对象
        ///</summary>
        ///<param name="cmd">查询命令</param>
        ///<returns>DataReader对象</returns>
        DbDataReader ExecuteReader(DbCommand cmd);
  
    }
}