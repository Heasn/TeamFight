﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight.Models
{
    public class RecommendTeamModel
    {
        public Guid TeamId { get; set; }

        public string CaptainName { get; set; }

        public uint CaptainLevel { get; set; }

        public int CaptainFaceId { get; set; }
    }
}