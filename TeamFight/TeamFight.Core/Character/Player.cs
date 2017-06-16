// ****************************************
// FileName:Player.cs
// Description:玩家类
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Collections.Generic;
using TeamFight.Core.Cache;
using TeamFight.Core.Character.Team;
using TeamFight.Core.Database;

namespace TeamFight.Core.Character
{
    public sealed class Player
    {
        #region 玩家基础属性

        /// <summary>
        /// 玩家Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 玩家性别
        /// </summary>
        public PlayerGender Gender { get; private set; }

        /// <summary>
        /// 玩家等级
        /// </summary>
        public uint Level { get; private set; }

        /// <summary>
        /// 玩家体力
        /// </summary>
        public uint PhysicalStrength { get; private set; }

        /// <summary>
        /// 玩家耐力
        /// </summary>
        public uint Endurance { get; private set; }

        #endregion

        /// <summary>
        /// 队伍
        /// </summary>
        public GameTeam GameTeam { get; private set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<PlayerFriend> Friends { get; private set; }

        public Player(int id, string name, PlayerGender gender, uint level, uint physicalStrength, uint endurance)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Level = level;
            PhysicalStrength = physicalStrength;
            Endurance = endurance;
            UpdateFriends();
        }

        /// <summary>
        /// 创建队伍
        /// </summary>
        /// <returns></returns>
        public bool CreateTeam()
        {
            if (GameTeam == null)
            {
                GameTeam = new GameTeam(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 加入队伍
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns></returns>
        public bool JoinTeam(GameTeam team)
        {
            GameTeam = team;
            GameTeam.AddMember(this);
            InvitationCache.Instance.RemoveInvitation(this, InvitationCache.InvitationType.Team);
            return true;
        }

        /// <summary>
        /// 退出队伍
        /// </summary>
        /// <returns></returns>
        public bool QuitTeam()
        {
            GameTeam.RemoveMember(this);
            GameTeam = null;
            return true;
        }

        /// <summary>
        /// 添加邀请
        /// </summary>
        /// <param name="playerId">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        /// <returns></returns>
        public bool AddInvitation(int playerId, InvitationCache.InvitationType type)
        {
            var player = OnlinePlayersCache.Instance.FindPlayer(playerId);

            if (player == null)
                return false;

            InvitationCache.Instance.AddInvitation(player, type, GameTeam);
            return true;
        }

        /// <summary>
        /// 更新好友
        /// </summary>
        public void UpdateFriends()
        {
            var friendList = DatabaseHelper.GetFriendIdList(Id);
            friendList.ForEach(x => x.IsOnline = OnlinePlayersCache.Instance.IsPlayerOnline(x.FriendId));
            Friends = friendList;
        }
    }
}