using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using MySqlHelper = TeamFight.Tools.MySqlHelper;

namespace TeamFight.Core
{
    public class DbContext
    {
        public static Character LoadCharacter(int charId)
        {
            var reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text,
                "SELECT * FROM Characters WHERE Id=@charId", new MySqlParameter("@charId", charId));

            if (reader.Read())
            {
                return new Character(reader.GetInt32("Id"), reader.GetUInt32("Level"), reader.GetUInt32("Fatigue"),
                    reader.GetString("Name"));
            }

            return null;
        }

        public static List<int> QueryFriendId(int charId)
        {
            var reader = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text,
                "SELECT FriendCharacterId FROM Friends WHERE CharacterId = @charId ",
                new MySqlParameter("@charId", charId));

            var friendIdList = new List<int>();

            while (reader.Read())
            {
                friendIdList.Add(reader.GetInt32("FriendCharacterId"));
            }

            return friendIdList;
        }
    }
}