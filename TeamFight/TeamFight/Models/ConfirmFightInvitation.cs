using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class ConfirmFightInvitation
    {
        public Guid TeamId { get; set; }

        public bool ConfirmResult { get; set; }
    }
}