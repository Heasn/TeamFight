// ****************************************
// FileName:Player.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Collections.Generic;

namespace TeamFight.Core.Character
{
    using Cache;
    using Team;
    using Data;
    using Models;

    /// <summary>
    /// 玩家类
    /// </summary>
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

        /// <summary>
        /// 玩家战力
        /// </summary>
        public uint CombatEffectiveness { get; set; }

        #endregion

        /// <summary>
        /// 队伍
        /// </summary>
        public GameTeam GameTeam { get; private set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<PlayerFriendModel> Friends { get; private set; }

        /// <summary>
        /// 构造玩家
        /// </summary>
        /// <param name="id">玩家Id</param>
        /// <param name="name">玩家姓名</param>
        /// <param name="gender">玩家性别</param>
        /// <param name="level">玩家等级</param>
        /// <param name="physicalStrength">玩家体力</param>
        /// <param name="endurance">玩家耐力</param>
        /// <param name="combatEffectiveness">玩家战力</param>
        public Player(int id, string name, PlayerGender gender, uint level, uint physicalStrength, uint endurance,uint combatEffectiveness)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Level = level;
            PhysicalStrength = physicalStrength;
            Endurance = endurance;
            CombatEffectiveness = combatEffectiveness;
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
            var factory = (FriendsFactory)DataFactory.Create(DataFactory.FactoryType.Friend);
            var friendList = factory.GetFriendIdList(Id);
            friendList.ForEach(x => x.IsOnline = OnlinePlayersCache.Instance.IsPlayerOnline(x.FriendId));
            Friends = friendList;
        }

        /// <summary>
        /// 增加耐力
        /// </summary>
        /// <param name="value"></param>
        public void AddEndurance(uint value)
        {
            Endurance += value;
        }

        /// <summary>
        /// 减少耐力
        /// </summary>
        /// <param name="value"></param>
        public void SubEndurance(uint value)
        {
            Endurance -= value;
        }

        /// <summary>
        /// 增加体力
        /// </summary>
        /// <param name="value"></param>
        public void AddPhysicalStrength(uint value)
        {
            PhysicalStrength += value;
        }

        /// <summary>
        /// 减少体力
        /// </summary>
        /// <param name="value"></param>
        public void SubPhysicalStrength(uint value)
        {
            PhysicalStrength -= value;
        }
    }
}