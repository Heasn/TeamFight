// ****************************************
// FileName:AccountController.cs
// Description:账户相关功能API接口
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Web.Http;
using TeamFight.Core.Cache;
using TeamFight.Core.Character;
using TeamFight.Core.Database;
using TeamFight.Host.Models;

namespace TeamFight.Host.Controllers
{
    public class AccountController : ApiController
    {
        /// <summary>
        /// 玩家登陆
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns>玩家相关数据</returns>
        public PlayerPropertiesModel Login([FromBody]int playerId)
        {
           
            if (OnlinePlayersCache.Instance.IsPlayerOnline(playerId))
                return null;

            var player = DatabaseHelper.LoadPlayer(playerId);

            if (OnlinePlayersCache.Instance.AddPlayer(player))
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
    }
}