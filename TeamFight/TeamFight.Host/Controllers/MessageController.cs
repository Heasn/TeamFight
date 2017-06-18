// ****************************************
// FileName:MessageController.cs
// Description:消息相关功能API接口
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Web.Http;

namespace TeamFight.Host.Controllers
{
    using Newtonsoft.Json;
    using TeamFight.Core.Cache;
    using TeamFight.Host.Messager.Team;

    public class MessageController : ApiController
    {
        /// <summary>
        /// 轮询获取消息
        /// </summary>
        /// <param name="charId"></param>
        /// <returns></returns>
        public string GetMessage([FromBody] int charId)
        {
            var messager = new TeamMessagerProxy();

            var player = OnlinePlayersCache.Instance.FindPlayer(charId);

            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Team))
            {
                return messager.SendTeamInvitation(player);
            }

            if (InvitationCache.Instance.IsInvitationExist(player, InvitationCache.InvitationType.Team))
            {
                return messager.SendFightInvitation(player);
            }

            return JsonConvert.SerializeObject("null");
        }

    }
}
