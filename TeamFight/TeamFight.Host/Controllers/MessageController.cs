// ****************************************
// FileName:MessageController.cs
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

    /// <summary>
    /// 消息类控制器
    /// </summary>
    public class MessageController : ApiController
    {
        /// <summary>
        /// 轮询获取消息
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public IHttpActionResult PullMessage([FromBody] int playerId)
        {
            
            var player = OnlinePlayersCache.Instance.FindPlayer(playerId);

            //查找是否有组队邀请
            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Team))
            {
                return Json(new
                {
                    mtype = "team",
                    id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Team).Id
                });
            }

            //查找是否有战斗邀请
            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Fight))
            {
                return Json(new
                {
                    mtype = "fight",
                    id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Fight).Id
                });
            }

            return Json("null");
        }

    }
}
