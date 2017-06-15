using System;

namespace TeamFight.Host.Models
{
    public class RecommendTeamModel
    {
        public Guid TeamId { get; set; }
        public string CaptainName { get; set; }
        public uint CaptainLevel { get; set; }
        public int CaptainFaceId { get; set; }
    }
}