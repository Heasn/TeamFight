// ****************************************
// FileName:TeamController.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TeamFight.Host.Controllers
{
    using Core.Cache;
    using Models;

    /// <summary>
    /// 组队类控制器
    /// </summary>
    public class TeamController : ApiController
    {
        /// <summary>
        ///     玩家创建组队
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns>创建的组队的Id</returns>
        public Guid Create([FromBody]int playerId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(playerId);

            if (player == null)
            {
                return Guid.Empty;
            }

            return player.CreateTeam() ? player.GameTeam.Id : Guid.Empty;
        }

        /// <summary>
        ///     获取推荐组队列表
        /// </summary>
        /// <returns>成功返回组队列表，失败返回null</returns>
        [HttpPost]
        public List<RecommendTeamModel> PullRecommendTeams()
        {
            var teams = TeamsCache.Instance.GetRecommendTeams(10);

            if (teams == null)
            {
                return null;
            }

            return teams.Select(x => new RecommendTeamModel
            {
                TeamId = x.Id,
                CaptainName = x.Captain.Name,
                CaptainLevel = x.Captain.Level,
                CaptainFaceId = 13456
            }).ToList();
        }

        /// <summary>
        ///     邀请玩家
        /// </summary>
        /// <param name="model">接收到的邀请玩家的数据模型，包含发起人Id（CharacterId）与被邀请人Id（InvitedCharacterId）</param>
        /// <returns>邀请成功返回true，邀请失败返回false</returns>
        public bool InviteCharacter([FromBody] InviteCharacterModel model)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(model.PlayerId);

            if (player == null)
            {
                return false;
            }

            return ReferenceEquals(player, player.GameTeam.Captain) && player.AddInvitation(model.InvitedPlayerId, InvitationCache.InvitationType.Team);
        }

        /// <summary>
        ///     加入队伍
        /// </summary>
        /// <param name="model">加入组队的数据模型</param>
        /// <returns>是否加入成功</returns>
        public bool JoinTeam([FromBody]JoinTeamModel model)
        {
            var team = TeamsCache.Instance.FindTeam(model.TeamId);

            if (team == null)
            {
                return false;
            }

            var player = OnlinePlayersCache.Instance.FindPlayer(model.InvitedPlayerId);

            if (player == null)
            {
                return false;
            }

            return player.JoinTeam(team);
        }

        /// <summary>
        ///     玩家退出组队
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>退出是否成功，true表示成功，false表示失败或队伍不存在</returns>
        public bool QuitTeam([FromBody]int charId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(charId);

            if (player == null)
            {
                return false;
            }

            return player.QuitTeam();
        }

        /// <summary>
        ///     由队长发起，发起战斗请求
        /// </summary>
        /// <param name="charId">人物Id</param>
        /// <returns>发出战斗请求是否成功</returns>
        public bool SendFightInvitation([FromBody]int charId)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(charId);

            if (player == null)
            {
                return false;
            }

            foreach (var member in player.GameTeam.Members)
            {
                if (member.Id != player.Id)
                {
                    player.AddInvitation(member.Id, InvitationCache.InvitationType.Fight);
                }
            }

            return true;
        }

        /// <summary>
        ///     由队员发起，队员确认是否接收战斗请求
        /// </summary>
        /// <param name="model">确认战斗请求结果数据模型</param>
        /// <returns>确认结果是否成功</returns>
        public bool VoteFightInvitation([FromBody]ConfirmFightInvitationModel model)
        {
            var team = TeamsCache.Instance.FindTeam(model.TeamId);
            if (team != null)
            {
                team.VoteCollector.Vote(model.ConfirmResult);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     获取队伍所有成员的投票结果
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns>确认结果</returns>
        public bool? PullFightInvitationResult([FromBody]Guid teamId)
        {
            var team = TeamsCache.Instance.FindTeam(teamId);
            if (team != null)
            {
                return team.VoteCollector.GetFinalResult();
            }

            return false;
        }

    }
}
