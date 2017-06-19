// ****************************************
// FileName:JoinTeamModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System;

namespace TeamFight.Models
{
    public class JoinTeamModel
    {
        public Guid TeamId { get; set; }
        public int InvitedPlayerId { get; set; }
    }
}