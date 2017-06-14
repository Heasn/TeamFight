using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class DissmissTeamModel
    {
        public Guid TeamId { get; set; }

        public int CaptainCharacterId { get; set; }
    }
}