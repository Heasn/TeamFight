using System.Data;
using MySql.Data.MySqlClient;

namespace TeamFight.Tools.Database
{
    public sealed class MySqlDatabaseHelper
    {
        //数据库连接字符串
        private const string ConnectionString =
            "Database='TeamFightDb';Data Source='localhost';User Id='root';Password='cby159753';charset='utf8';pooling=true";

        /// <summary>
        ///     给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandStr">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandStr, params MySqlParameter[] commandParameters)
        {
            using (var db = DatabaseFactory.Create(DatabaseType.MySql, ConnectionString))
            {
                var command = new MySqlCommand(commandStr, (MySqlConnection) db.Connection) {CommandType = commandType };
                command.Parameters.AddRange(commandParameters);
                return db.ExecuteNonQuery(command);
            }
        }


        /// <summary>
        ///     用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        ///     举例:
        ///     MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new
        ///     MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandStr">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(CommandType commandType, string commandStr,
            params MySqlParameter[] commandParameters)
        {
            var db = DatabaseFactory.Create(DatabaseType.MySql, ConnectionString);

            var command = new MySqlCommand(commandStr, (MySqlConnection) db.Connection) {CommandType = commandType};
            command.Parameters.AddRange(commandParameters);
            return (MySqlDataReader) db.ExecuteReader(command);
        }

        /// <summary>
        ///     用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///     例如:
        ///     Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid",
        ///     24));
        /// </remarks>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandStr">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(CommandType commandType, string commandStr,
            params MySqlParameter[] commandParameters)
        {
            using (var db = DatabaseFactory.Create(DatabaseType.MySql, ConnectionString))
            {
                var command = new MySqlCommand(commandStr, (MySqlConnection) db.Connection) {CommandType = commandType};
                command.Parameters.AddRange(commandParameters);
                return db.ExecuteScalar(command);
            }
        }
    }
}