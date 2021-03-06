﻿// ****************************************
// FileName:InvitationCache.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamFight.Core.Cache
{
    using Character;
    using Character.Team;

    /// <summary>
    /// 邀请缓存
    /// </summary>
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

        /// <summary>
        /// InvitationCache的唯一实例
        /// </summary>
        private static readonly InvitationCache SingletonInstance = new InvitationCache();

        /// <summary>
        /// 邀请列表
        /// </summary>
        private static readonly List<Tuple<Player, InvitationType, GameTeam>> Invitations =
            new List<Tuple<Player, InvitationType, GameTeam>>();

        private InvitationCache()
        {
        }

        /// <summary>
        /// 提供对InvitationCache的唯一实例访问
        /// </summary>
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
        public bool AddInvitation(Player player, InvitationType type, GameTeam team)
        {
            if (IsInvitationExist(player, type))
            {
                return false;
            }

            Invitations.Add(Tuple.Create(player, type, team));

            return true;
        }

        /// <summary>
        /// 移除邀请
        /// </summary>
        /// <param name="player">被邀请玩家</param>
        /// <param name="type">邀请类型</param>
        public void RemoveInvitation(Player player, InvitationType type)
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
            {
                return null;
            }

            var tuple = Invitations.Single(x => (x.Item1.Id == player.Id) && (x.Item2 == type));

            return tuple.Item3;
        }
    }
}