using System.Collections.Generic;
using System.Web.Http;
using TeamFight.Core.Cache;
using TeamFight.Core.Character;
using TeamFight.Core.Database;

namespace TeamFight.Host.Controllers
{
    public class AccountController : ApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public Models.PlayerPropertiesModel Login([FromBody]int playerId)
        {
           
            if (OnlinePlayersCache.Instance.IsPlayerOnline(playerId))
                return null;

            var player = DatabaseHelper.LoadPlayer(playerId);

            if (OnlinePlayersCache.Instance.AddPlayer(player))
            {
                return new Models.PlayerPropertiesModel
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