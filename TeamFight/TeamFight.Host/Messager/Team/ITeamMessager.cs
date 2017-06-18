using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFight.Core.Character;

namespace TeamFight.Host.Messager.Team
{
    interface ITeamMessager : IMessager
    {
        string SendTeamInvitation(Player player);

        string SendFightInvitation(Player player);
    }
}
