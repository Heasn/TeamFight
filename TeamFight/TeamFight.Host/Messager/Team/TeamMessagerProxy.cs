using TeamFight.Core.Character;

namespace TeamFight.Host.Messager.Team
{
    class TeamMessagerProxy : ITeamMessager
    {
        private TeamMessager tm = new TeamMessager();

        public string SendFightInvitation(Player player)
        {
            return tm.SendTeamInvitation(player);
        }

        public string SendTeamInvitation(Player player)
        {
            return tm.SendFightInvitation(player);
        }
    }
}
