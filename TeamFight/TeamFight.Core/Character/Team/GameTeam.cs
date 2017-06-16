using System;
using System.Collections.Generic;
using TeamFight.Core.Cache;

namespace TeamFight.Core.Character.Team
{
    public sealed class GameTeam
    {

        public const int TeamMemberCapcity = 2;

        public Guid Id { get; private set; }

        public Player Captain { get;private set; }

        public List<Player> Members { get;private set; }

        public DateTime BuildTime { get; private set; }

        public VoteCollector VoteCollector { get;private set; }

        //创建
        public GameTeam(Player captain)
        {
            Id = Guid.NewGuid();
            Captain = captain;
            Members = new List<Player> {captain};
            TeamsCache.Instance.AddTeam(this);
            BuildTime = DateTime.Now;
            VoteCollector = new VoteCollector(TeamMemberCapcity);
        }

        //加入队伍
        public bool AddMember(Player captain, Player other)
        {
            if (ReferenceEquals(Captain, captain) && !ContainsMember(other))
            {
                Members.Add(other);
                return true;
            }

            return false;
        }

        public bool ContainsMember(Player member)
        {
            return Members.Exists(x => x.Id == member.Id);
        }

        //移除队员
        public bool RemoveMember(Player captain, Player member)
        {
            //队长解散
            if (captain.Id == member.Id)
            {
                Members.ForEach(x => x.QuitTeam());
            }

            if (ReferenceEquals(Captain, captain) && ContainsMember(member))
            {
                Members.RemoveAll(x => x.Id == member.Id);
                return true;
            }

            return false;
        }
    }
}