using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFight.Core.Character;
using TeamFight.Core.Character.Team;

namespace TeamFight.Core.Cache
{
    public class InvitationCache
    {
        /// <summary>
        /// 邀请类型
        /// </summary>
        public enum InvitationType
        {
            Team,
            Fight,
        }

        private static readonly InvitationCache SingletonInstance = new InvitationCache();

        private static readonly List<Tuple<Player, InvitationType, GameTeam>> Invitations =
            new List<Tuple<Player, InvitationType, GameTeam>>();

        private InvitationCache()
        {

        }

        public static InvitationCache Instance
        {
            get { return SingletonInstance; }
        }

        /// <summary>
        /// 添加邀请
        /// </summary>
        /// <param name="player">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        /// <param name="team">邀请队伍</param>
        /// <returns></returns>
        public bool AddInvitation(Player player,InvitationType type,GameTeam team)
        {
            if (IsInvitationExist(player,type))
                return false;

            Invitations.Add(Tuple.Create(player, type, team));
            return true;
        }

        /// <summary>
        /// 移除邀请
        /// </summary>
        /// <param name="player">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        public void RemoveInvitation(Player player,InvitationType type)
        {
            Invitations.RemoveAll(x => (x.Item1.Id == player.Id) && (x.Item2 == type));
        }

        /// <summary>
        /// 是否存在某个玩家的某个邀请
        /// </summary>
        /// <param name="player">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        /// <returns></returns>
        public bool IsInvitationExist(Player player, InvitationType type)
        {
            return Invitations.Exists(x => (x.Item1.Id == player.Id) && (x.Item2 == type));
        }

        /// <summary>
        /// 查找与邀请关联的队伍
        /// </summary>
        /// <param name="player">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        /// <returns></returns>
        public GameTeam FindTeam(Player player, InvitationType type)
        {
            if (!IsInvitationExist(player, type))
                return null;

            var tuple = Invitations.Single(x => (x.Item1.Id == player.Id) && (x.Item2 == type));

            return tuple.Item3;
        }
    }
}
