// ****************************************
// FileName:AccountController.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System.Web.Http;

namespace TeamFight.Host.Controllers
{
    using Core.Cache;
    using Core.Character;
    using Core.Data;
    using Models;

    /// <summary>
    /// 账户类控制器
    /// </summary>
    public class AccountController : ApiController
    {
        /// <summary>
        /// 玩家登陆
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns>玩家相关数据</returns>
        public PlayerPropertiesModel Login([FromBody]int playerId)
        {
            //查找玩家是否在线
            if (OnlinePlayersCache.Instance.IsPlayerOnline(playerId))
            {
                return null;
            }

            var factory = (PlayersFactory)DataFactory.Create(DataFactory.FactoryType.Player);
            var player = factory.FindPlayer(playerId);

            if (player != null && OnlinePlayersCache.Instance.AddPlayer(player))
            {
                return new PlayerPropertiesModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    Gender = player.Gender != PlayerGender.Female,
                    Level = player.Level,
                    PhysicalStrength = player.PhysicalStrength,
                    Endurance = player.Endurance
                };
            }

            return null;
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public PlayerFriendModel[] PullFriends([FromBody] int playerId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(playerId);

            if (player == null)
            {
                return null;
            }

            player.UpdateFriends();

            return player.Friends.ToArray();
        }
    }
}