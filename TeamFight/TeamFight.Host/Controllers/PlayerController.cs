using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TeamFight.Core.Cache;
using TeamFight.Core.Character;

namespace TeamFight.Host.Controllers
{
    public class PlayerController:ApiController
    {
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public List<PlayerFriend> GetFriends([FromBody] int playerId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(playerId);

            if (player == null)
                return null;

            player.UpdateFriends();

            return player.Friends;
        }
    }
}
