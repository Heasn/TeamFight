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
            TeamList.Teams.Add(Id);
        }
        //邀请
        public void InviteMember(Character other)
        {
            InviteList.Invites.Add(Tuple.Create(Id, other));
        }

        //加入队伍
        public bool AddMember(Character other)
        {
            if (Member == null)
            {
                Member = other;
                return true;
            }
            return false;
        }

        //踢出队伍
        public bool RemoveMember(Character member)
        {
            if (ReferenceEquals(Member, member))
            {
                Member = null;
                return true;
            }
            return false;
        }

        //解散队伍
        public bool Dismiss()
        {
            Captain.QuitTeam();
            Member.QuitTeam();
            Guid removeId;
            return TeamList.Teams.TryTake(out removeId);
        }

    }
}