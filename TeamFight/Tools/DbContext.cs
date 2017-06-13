using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tools
{
    public class DbContext
    {
        public static List<int> QueryFriendId(int charId)
        {
            var reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text,
                "SELECT FriendCharacterId FROM Friends WHERE CharacterId = @charId ", new MySqlParameter("@charId", charId));

            var friendIdList = new List<int>();

            while (reader.Read())
            {
                friendIdList.Add(reader.GetInt32("FriendCharacterId"));
            }

            return friendIdList;
        }
    }
}
