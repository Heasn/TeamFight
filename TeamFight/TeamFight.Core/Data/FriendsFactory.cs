// ****************************************
// FileName:FriendsFactory.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System.Collections.Generic;
using System.Data;

namespace TeamFight.Core.Data
{
    using MySql.Data.MySqlClient;
    using Models;
    using Tools.Database;

    /// <summary>
    /// 好友数据工厂
    /// </summary>
    public class FriendsFactory : IFactory
    {
        /// <summary>
        /// 加载玩家好友数据
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public List<PlayerFriendModel> GetFriendIdList(int playerId)
        {
            using (var reader = MySqlDatabaseHelper.ExecuteReader(CommandType.Text,
                "SELECT * FROM Players WHERE Id IN (SELECT FriendCharacterId FROM friends WHERE CharacterId = @charId)",
                new MySqlParameter("@charId", playerId)))
            {
                var friendIdList = new List<PlayerFriendModel>();

                while (reader.Read())
                {
                    friendIdList.Add(new PlayerFriendModel
                    {
                        FriendId = reader.GetInt32("Id"),
                        FriendName = reader.GetString("Name"),
                        FriendGender = reader.GetBoolean("Gender"),
                        FriendCe = reader.GetUInt32("CE")
                    });
                }

                return friendIdList;
            }
        }
    }
}
