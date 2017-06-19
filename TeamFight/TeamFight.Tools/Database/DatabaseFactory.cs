// ****************************************
// FileName:DatabaseFactory.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System;

namespace TeamFight.Tools.Database
{
    public static class DatabaseFactory
    {
        /// <summary>
        /// 根据数据库类型创建相应的数据库实例
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public static IDatabase Create(DatabaseType dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseType.MySql:
                    return new MySqlDatabase(connectionString);
                default:
                    throw new Exception("Unknown Database Type!");
            }
        }
    }
}
