// ****************************************
// FileName:MySqlDatabase.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace TeamFight.Tools.Database
{
    public sealed class MySqlDatabase : IDatabase
    {

        private MySqlConnection mDbConnection;

        ///<summary>
        ///构造函数
        ///</summary>
        public MySqlDatabase() { }

        ///<summary>
        ///构造函数
        ///</summary>
        ///<param name="connString">连接字符串</param>
        public MySqlDatabase(string connString)
        {
            Open(connString);
        }

        ///<summary>
        ///打开数据库
        ///</summary>
        ///<param name="connString">连接字符串</param>
        public void Open(string connString)
        {
            mDbConnection = new MySqlConnection(connString);
            mDbConnection.Open();
        }

        ///<summary>
        ///关闭数据库
        ///</summary>
        public void Close()
        {
            mDbConnection.Close();
        }

        ///<summary>
        ///数据库连接
        ///</summary>
        public DbConnection Connection
        {
            get
            {
                return mDbConnection;
            }
        }

        ///<summary>
        ///获取数据库类型
        ///</summary>
        ///<returns>数据库类型</returns>
        public DatabaseType GetDatabaseType()
        {
            return DatabaseType.MySql;
        }

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>影响行数</returns>
        public int ExecuteNonQuery(string sql)
        {
            var cmdDb = mDbConnection.CreateCommand();
            cmdDb.CommandText = sql;
            int n = cmdDb.ExecuteNonQuery();
            return n;
        }

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="cmd">数据命令</param>
        ///<returns>影响行数</returns>
        public int ExecuteNonQuery(DbCommand cmd)
        {
            var cmdDb = (MySqlCommand)cmd;
            int n = cmdDb.ExecuteNonQuery();
            return n;
        }

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>第一行第一列值</returns>
        public object ExecuteScalar(string sql)
        {
            var cmdDb = mDbConnection.CreateCommand();
            cmdDb.CommandText = sql;
            object obj = cmdDb.ExecuteScalar();
            return obj;
        }

        ///<summary>
        ///执行SQL语句
        ///</summary>
        ///<param name="cmd">数据命令</param>
        ///<returns>第一行第一列值</returns>
        public object ExecuteScalar(DbCommand cmd)
        {
            var cmdDb = (MySqlCommand)cmd;
            object obj = cmdDb.ExecuteScalar();
            return obj;
        }

        ///<summary>
        ///返回DataReader对象
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<returns>DataReader对象</returns>
        public DbDataReader ExecuteReader(string sql)
        {
            var cmdDb = mDbConnection.CreateCommand();
            cmdDb.CommandText = sql;
            return cmdDb.ExecuteReader(CommandBehavior.CloseConnection);
        }

        ///<summary>
        ///返回DataReader对象
        ///</summary>
        ///<param name="cmd">查询命令</param>
        ///<returns>DataReader对象</returns>
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            var cmdDb = (MySqlCommand)cmd;
            return cmdDb.ExecuteReader();
        }

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
    }
}
