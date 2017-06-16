// ****************************************
// FileName:RecommendTeamModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-15
// Revision History:
// ****************************************

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