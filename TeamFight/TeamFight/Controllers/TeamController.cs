using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeamFight.Controllers
{
    /// <summary>
    /// 组队相关接口
    /// </summary>
    public class TeamController : ApiController
    {
        /// <summary>
        /// 玩家创建组队
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>创建的组队的Id</returns>
        public Guid Create([FromBody]int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                var team = TeamFunction.GameTeam.Create(character);
                return team.Id;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// 获取玩家好友列表
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>玩家好友Id</returns>
        public List<int> PullFirends([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.Friends;
            }
            return null;
        }


        /// <summary>
        /// 玩家退出组队
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>退出是否成功，true表示成功，false表示失败或队伍不存在</returns>
        public bool Quit([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.QuitTeam();
            }
            return false;
        }

    }
}
