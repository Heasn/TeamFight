using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TeamFight.Core.Cache;

namespace TeamFight.Host.Controllers
{
    public class MessageController : ApiController
    {
        /// <summary>
        /// 轮询获取消息
        /// </summary>
        /// <param name="charId"></param>
        /// <returns></returns>
        public dynamic PullOrder([FromBody] int charId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(charId);

            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Team))
            {
                return Json(new
                {
                    mtype = "team",
                    id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Team).Id
                });
            }

            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Fight))
            {
                return Json(new
                {
                    mtype = "fight",
                    id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Team).Id
                });
            }

            return null;
        }
    }
}
