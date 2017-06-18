using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFight.Core.Cache;
using TeamFight.Core.Character;

namespace TeamFight.Host.Messager.Team
{
    class TeamMessager:ITeamMessager
    {
        public string SendFightInvitation(Player player)
        {
            return JsonConvert.SerializeObject(new
            {
                mtype = "team",
                id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Team).Id
            });

        }

        public string SendTeamInvitation(Player player)
        {
            return JsonConvert.SerializeObject(new
            {
                mtype = "fight",
                id = InvitationCache.Instance.FindTeam(player, InvitationCache.InvitationType.Team).Id
            });
        }
    }
}
