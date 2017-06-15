using System;

namespace TeamFight.Host.Models
{
    public class ConfirmFightInvitationModel
    {
        public Guid TeamId { get; set; }
        public bool ConfirmResult { get; set; }
    }
}