// ****************************************
// FileName:GameTeam.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamFight.Core.Character.Team
{
    using Cache;

    /// <summary>
    /// 组队
    /// </summary>
    public class GameTeam
    {
        /// <summary>
        /// 队伍成员容量
        /// </summary>
        public const uint TeamMemberCapcity = 2;

        /// <summary>
        /// 队伍Id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 队伍队长
        /// </summary>
        public Player Captain { get; private set; }

        /// <summary>
        /// 队伍成员
        /// </summary>
        public List<Player> Members { get; private set; }

        /// <summary>
        /// 队伍建立时间
        /// </summary>
        public DateTime BuildTime { get; private set; }

        /// <summary>
        /// 队伍投票箱
        /// </summary>
        public VoteCollector VoteCollector { get; private set; }

        /// <summary>
        /// 创建一个新的组队
        /// </summary>
        /// <param name="captain">队长玩家</param>
        public GameTeam(Player captain)
        {
            Id = Guid.NewGuid();
            Captain = captain;
            Members = new List<Player> {captain};
            VoteCollector = new VoteCollector(TeamMemberCapcity);
            BuildTime = DateTime.Now;
            TeamsCache.Instance.AddTeam(this);
        }

        /// <summary>
        /// 将新成员加入队伍
        /// </summary>
        /// <param name="other">新成员玩家</param>
        /// <returns>是否加入成功</returns>
        public bool AddMember(Player other)
        {
            if (!ContainsMember(other))
            {
                Members.Add(other);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 确认队伍中是否存在某个玩家
        /// </summary>
        /// <param name="member">成员玩家</param>
        /// <returns></returns>
        public bool ContainsMember(Player member)
        {
            return Members.Exists(x => x.Id == member.Id);
        }

        /// <summary>
        /// 移除队伍中的某个玩家
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool RemoveMember(Player member)
        {
            //队长解散
            if (Captain.Id == member.Id)
            {
                foreach (var teamMember in Members.Where(x => !ReferenceEquals(x, member)))
                {
                    teamMember.QuitTeam();
                }
                return true;
            }

            if (ContainsMember(member))
            {
                Members.RemoveAll(x => x.Id == member.Id);
                return true;
            }

            return false;
        }
    }
}