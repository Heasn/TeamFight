// ****************************************
// FileName:MessageController.cs
// Description:消息相关功能API接口
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

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
