using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class JoinTeamModel
    {
        public Guid TeamId { get; set; }

        public int InvitedCharacterId { get; set; }
    }
}