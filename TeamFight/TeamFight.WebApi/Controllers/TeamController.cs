using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamFight.Core;
using TeamFight.Core.Character;

namespace TeamFight.WebApi.Controllers
{
    public class TeamController : ApiController
    {
        /// <summary>
        ///     玩家创建组队
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>创建的组队的Id</returns>
        [HttpPost]
        private Guid Create(int charId)
        {
            GamePlayer character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.CreateTeam() ? character.GameTeam.Id : Guid.Empty;
            }
            return Guid.Empty;
        }

        /// <summary>
        ///     获取玩家好友列表
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>玩家好友Id</returns>
        private List<int> PullFriendList(int charId)
        {
            GamePlayer character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.Friends;
            }

            return null;
        }

        ///// <summary>
        /////     获取推荐组队列表
        ///// </summary>
        ///// <returns>成功返回组队列表，失败返回null</returns>
        //private List<RecommendTeamModel> PullRecommendTeams()
        //{
        //    var teams = TeamList.Instance.GetRecommendTeams(10);

        //    if (teams == null)
        //        return null;

        //    return teams.Select(x => new RecommendTeamModel
        //    {
        //        TeamId = x.Id,
        //        CaptainName = x.Captain.Name,
        //        CaptainLevel = x.Captain.Level,
        //        CaptainFaceId = 13456
        //    }).ToList();
        //}

        ///// <summary>
        /////     邀请玩家
        ///// </summary>
        ///// <param name="model">接收到的邀请玩家的数据模型，包含发起人Id（CharacterId）与被邀请人Id（InvitedCharacterId）</param>
        ///// <returns>邀请成功返回true，邀请失败返回false</returns>
        //private bool InviteCharacter(InviteCharacterModel model)
        //{
        //    GamePlayer captain;
        //    GamePlayer member;

        //    if (!Common.OnlinePlayers.TryGetValue(model.CharacterId, out captain) ||
        //        !Common.OnlinePlayers.TryGetValue(model.InvitedCharacterId, out member) ||
        //        captain.GameTeam == null)
        //        return false;

        //    return captain.GameTeam.InviteMember(captain, member);
        //}

        ///// <summary>
        /////     查询是否有组队邀请
        ///// </summary>
        ///// <param name="charId">玩家Id</param>
        ///// <returns>组队Id</returns>
        //private Guid QueryInvitation(int charId)
        //{
        //    GamePlayer member;

        //    if (Common.OnlinePlayers.TryGetValue(charId, out member))
        //    {
        //        var team = InviteList.Instance.FindInvitation(member);
        //        return team != null ? team.Id : Guid.Empty;
        //    }

        //    return Guid.Empty;
        //}

        ///// <summary>
        /////     加入队伍
        ///// </summary>
        ///// <param name="model">加入组队的数据模型</param>
        ///// <returns>是否加入成功</returns>
        //private bool JoinTeam(JoinTeamModel model)
        //{
        //    GamePlayer member;

        //    if (Common.OnlinePlayers.TryGetValue(model.InvitedCharacterId, out member))
        //    {
        //        var team = TeamList.Instance.FindTeam(model.TeamId);
        //        if (team != null)
        //        {
        //            return team.AddMember(team.Captain, member);
        //        }
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     玩家退出组队
        ///// </summary>
        ///// <param name="charId">玩家Id</param>
        ///// <returns>退出是否成功，true表示成功，false表示失败或队伍不存在</returns>
        //private bool QuitTeam(int charId)
        //{
        //    GamePlayer character;

        //    if (Common.OnlinePlayers.TryGetValue(charId, out character))
        //    {
        //        return character.QuitTeam();
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     队长解散队伍
        ///// </summary>
        ///// <param name="model">解散队伍的数据模型</param>
        ///// <returns>是否解散成功</returns>
        //private bool DismissTeam(DissmissTeamModel model)
        //{
        //    GamePlayer character;

        //    if (Common.OnlinePlayers.TryGetValue(model.CaptainCharacterId, out character))
        //    {
        //        var team = TeamList.Instance.FindTeam(model.TeamId);
        //        if (team != null)
        //        {
        //            return team.Dismiss(character);
        //        }
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     由队长发起，发起战斗请求
        ///// </summary>
        ///// <param name="charId">人物Id</param>
        ///// <returns>发出战斗请求是否成功</returns>
        //private bool SendFightInvitation(int charId)
        //{
        //    GamePlayer character;

        //    if (Common.OnlinePlayers.TryGetValue(charId, out character))
        //    {
        //        return FightInviteList.Instance.AddFightInvitation(character.GameTeam);
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     由队员发起，查询是否有战斗请求
        ///// </summary>
        ///// <param name="charId">人物Id</param>
        ///// <returns>查询战斗请求是否成功</returns>
        //private bool QueryFightInvitation(int charId)
        //{
        //    GamePlayer character;

        //    if (Common.OnlinePlayers.TryGetValue(charId, out character))
        //    {
        //        return FightInviteList.Instance.ExistFightInvitation(character);
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     由队员发起，队员确认是否接收战斗请求
        ///// </summary>
        ///// <param name="model">确认战斗请求结果数据模型</param>
        ///// <returns>确认结果是否成功</returns>
        //private bool ConfirmFightInvitation(ConfirmFightInvitationModel model)
        //{
        //    var team = TeamList.Instance.FindTeam(model.TeamId);
        //    if (team != null)
        //    {
        //        return FightInviteList.Instance.ConfrimFightInvitation(team, model.ConfirmResult);
        //    }

        //    return false;
        //}

        ///// <summary>
        /////     由队长发起，获取队员确认结果
        ///// </summary>
        ///// <param name="teamId">队伍Id</param>
        ///// <returns>确认结果</returns>
        //private FightInviteList.FightConfirmResult PullFightInvitationResult(Guid teamId)
        //{
        //    var team = TeamList.Instance.FindTeam(teamId);
        //    if (team != null)
        //    {
        //        return FightInviteList.Instance.GetFightInvitationConfirmResult(team);
        //    }

        //    return FightInviteList.FightConfirmResult.WaitToConfirm;
        //}
    }
}
