using System;
using System.Threading;
using System.Threading.Tasks;
namespace TeamFunction
{
    public sealed class GameTeam
    {
        public Guid Id { get; }

        public Character Captain { get;  set; }

        public Character Member { get; set; }

        public static GameTeam Create(Character captain)
        {
            var team = new GameTeam(captain);
            return team;
        }

        //创建
        private GameTeam(Character captain)
        {
            Id = Guid.NewGuid();
            Captain = captain;
            TeamList.Instance.AddTeam(this);
        }

        //邀请
        public bool InviteMember(Character captain, Character other)
        {
            if (ReferenceEquals(Captain, captain))
            {
                InviteList.Instance.AddInvitation(this, other);
                return true;
            }

            return false;
        }

        //加入队伍
        public bool AddMember(Character captain, Character other)
        {
            if (ReferenceEquals(Captain,captain) &&  Member == null)
            {
                Member = other;
                return true;
            }

            return false;
        }

        //踢出队伍
        public bool RemoveMember(Character captain, Character member)
        {
            if (ReferenceEquals(Captain,captain)&& ReferenceEquals(Member, member))
            {
                Member = null;
                return true;
            }

            return false;
        }

        //解散队伍
        public bool Dismiss(Character captain)
        {
            if (!ReferenceEquals(Captain, captain))
                return false;

            Captain.QuitTeam();
            Member.QuitTeam();

            return TeamList.Instance.RemoveTeam(this);
        }

    }
}