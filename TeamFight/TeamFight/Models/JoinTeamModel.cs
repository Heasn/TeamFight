using System;

namespace TeamFight.Models
{
    public class JoinTeamModel
    {
        public Guid TeamId { get; set; }
        public int InvitedCharacterId { get; set; }
    }
}