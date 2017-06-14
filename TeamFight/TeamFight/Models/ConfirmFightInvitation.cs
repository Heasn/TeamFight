using System;

namespace TeamFight.Models
{
    public class ConfirmFightInvitation
    {
        public Guid TeamId { get; set; }
        public bool ConfirmResult { get; set; }
    }
}