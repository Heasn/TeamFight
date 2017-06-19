// ****************************************
// FileName:RecommendTeamModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System;

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