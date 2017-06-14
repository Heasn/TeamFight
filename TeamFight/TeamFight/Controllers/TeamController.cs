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
        public List<int> PullFirendList([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.Friends;
            }
            return null;
        }

        /// <summary>
        /// 获取推荐组队列表
        /// </summary>
        /// <returns>成功返回组队列表，失败返回null</returns>
        public List<Models.RecommendTeamModel> PullRecommendTeams()
        {
            var teams = TeamFunction.TeamList.Instance.GetRecommendTeams(10);

            return teams?.Select(x => new Models.RecommendTeamModel
            {
                TeamId = x.Id,
                CaptainName = x.Captain.Name,
                CaptainLevel = x.Captain.Level,
                CaptainFaceId = 13456,
            }).ToList();

        }

        /// <summary>
        /// 邀请玩家
        /// </summary>
        /// <param name="model">接收到的邀请玩家的数据模型，包含发起人Id（CharacterId）与被邀请人Id（InvitedCharacterId）</param>
        /// <returns>邀请成功返回true，邀请失败返回false</returns>
        public bool InviteCharacter([FromBody] Models.InviteCharacterModel model)
        {
            TeamFunction.Character captain;
            TeamFunction.Character member;

            if (!Common.OnlinePlayers.TryGetValue(model.CharacterId, out captain) ||
                !Common.OnlinePlayers.TryGetValue(model.InvitedCharacterId, out member))
                return false;

            return captain.GameTeam.InviteMember(captain, member);
        }

        /// <summary>
        /// 查询是否有组队邀请
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>组队Id</returns>
        public Guid QueryInvitation([FromBody] int charId)
        {
            TeamFunction.Character member;

            if (Common.OnlinePlayers.TryGetValue(charId, out member))
            {
                var team = TeamFunction.InviteList.Instance.FindInvitation(member);
                return team?.Id ?? Guid.Empty;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 加入队伍
        /// </summary>
        /// <param name="model">加入组队的数据模型</param>
        /// <returns>是否加入成功</returns>
        public bool JoinTeam([FromBody] Models.JoinTeamModel model)
        {
            TeamFunction.Character member;

            if (Common.OnlinePlayers.TryGetValue(model.InvitedCharacterId, out member))
            {
                var team = TeamFunction.TeamList.Instance.FindTeam(model.TeamId);
                if (team != null)
                {
                    return team.AddMember(team.Captain, member);
                }
            }

            return false;
        }

        /// <summary>
        /// 玩家退出组队
        /// </summary>
        /// <param name="charId">玩家Id</param>
        /// <returns>退出是否成功，true表示成功，false表示失败或队伍不存在</returns>
        public bool QuitTeam([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return character.QuitTeam();
            }

            return false;
        }

        /// <summary>
        /// 队长解散队伍
        /// </summary>
        /// <param name="model">解散队伍的数据模型</param>
        /// <returns>是否解散成功</returns>
        public bool DismissTeam([FromBody] Models.DissmissTeamModel model)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(model.CaptainCharacterId, out character))
            {
                var team = TeamFunction.TeamList.Instance.FindTeam(model.TeamId);
                if (team != null)
                {
                    return team.Dismiss(character);
                }
            }

            return false;
        }

        /// <summary>
        /// 由队长发起，发起战斗请求
        /// </summary>
        /// <param name="charId">人物Id</param>
        /// <returns>发出战斗请求是否成功</returns>
        public bool SendFightInvitation([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return TeamFunction.FightInviteList.Instance.AddFightInvitation(character.GameTeam);
            }

            return false;
        }

        /// <summary>
        /// 由队员发起，查询是否有战斗请求
        /// </summary>
        /// <param name="charId">人物Id</param>
        /// <returns>查询战斗请求是否成功</returns>
        public bool QueryFightInvitation([FromBody] int charId)
        {
            TeamFunction.Character character;

            if (Common.OnlinePlayers.TryGetValue(charId, out character))
            {
                return TeamFunction.FightInviteList.Instance.ExistFightInvitation(character);
            }

            return false;
        }

        /// <summary>
        /// 由队员发起，队员确认是否接收战斗请求
        /// </summary>
        /// <param name="model">确认战斗请求结果数据模型</param>
        /// <returns>确认结果是否成功</returns>
        public bool ConfirmFightInvitation([FromBody] Models.ConfirmFightInvitation model )
        {
            var team= TeamFunction.TeamList.Instance.FindTeam(model.TeamId);
            if (team != null)
            {
                return TeamFunction.FightInviteList.Instance.ConfrimFightInvitation(team, model.ConfirmResult);
            }

            return false;
        }

        /// <summary>
        /// 由队长发起，获取队员确认结果
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns>确认结果</returns>
        public int PullFightInvitationResult([FromBody] Guid teamId )
        {
            var team = TeamFunction.TeamList.Instance.FindTeam(teamId);
            if (team != null)
            {
                return (int)TeamFunction.FightInviteList.Instance.GetFightInvitationConfirmResult(team);
            }

            return (int) TeamFunction.FightInviteList.FightConfirmResult.WaitToConfirm;
        }
    }
}
