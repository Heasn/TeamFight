using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFight.Tools.Database
{
    public sealed class DatabaseFactory
    {
        private DatabaseFactory()
        {
            
        }

        public static IDatabase Create(DatabaseType dbType, string connString)
        {
            switch (dbType)
            {
                case DatabaseType.MySql:
                    return new MySqlDatabase(connString);
                default:
                    throw new Exception("Unknown Database Type!");
            }
        }
    }
}
