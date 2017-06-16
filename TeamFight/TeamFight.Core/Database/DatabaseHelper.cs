using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TeamFight.Core.Character;
using TeamFight.Tools.Database;

namespace TeamFight.Core.Database
{
    public class DatabaseHelper
    {
        /// <summary>
        /// 加载玩家数据
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public static Player LoadPlayer(int playerId)
        {
            using (var reader = MySqlDatabaseHelper.ExecuteReader(CommandType.Text,
                "SELECT * FROM Players WHERE Id=@charId", new MySqlParameter("@charId", playerId)))
            {
                if (reader.Read())
                {
                    return new Player(
                        reader.GetInt32("Id"),
                        reader.GetString("Name"),
                        (PlayerGender)reader.GetInt32("Gender"),
                        reader.GetUInt32("Level"),
                        reader.GetUInt32("PhysicalStrength"),
                        reader.GetUInt32("Endurance")
                        );
                }

                return null;
            }
        }

        /// <summary>
        /// 加载玩家好友数据
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public static List<PlayerFriend> GetFriendIdList(int playerId)
        {
            using (var reader = MySqlDatabaseHelper.ExecuteReader(CommandType.Text,
                "SELECT * FROM Players WHERE Id IN (SELECT FriendCharacterId FROM friends WHERE CharacterId = @charId)",
                new MySqlParameter("@charId", playerId)))
            {
                var friendIdList = new List<PlayerFriend>();

                while (reader.Read())
                {
                    friendIdList.Add(new PlayerFriend(reader.GetInt32("Id"), reader.GetString("Name"),
                        reader.GetUInt32("CE"), reader.GetBoolean("Gender")));
                }

                return friendIdList;
            }
        }
    }
}