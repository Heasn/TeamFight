// ****************************************
// FileName:PlayersFactory.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System.Data;

namespace TeamFight.Core.Data
{
    using MySql.Data.MySqlClient;
    using Character;
    using Tools.Database;

    /// <summary>
    /// 玩家数据工厂
    /// </summary>
    public class PlayersFactory : IFactory
    {
        /// <summary>
        /// 查找玩家
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public Player FindPlayer(int playerId)
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
                        reader.GetUInt32("Endurance"),
                        reader.GetUInt32("CE")
                        );
                }

                return null;
            }
        }
    }
}
