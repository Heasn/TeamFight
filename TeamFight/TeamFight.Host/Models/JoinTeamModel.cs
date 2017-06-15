using System;

namespace TeamFight.Host.Models
{
    public class JoinTeamModel
    {
        public Guid TeamId { get; set; }
        public int InvitedCharacterId { get; set; }
    }
}