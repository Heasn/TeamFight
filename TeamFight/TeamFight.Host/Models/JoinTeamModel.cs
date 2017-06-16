// ****************************************
// FileName:JoinTeamModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-15
// Revision History:
// ****************************************

using System;

namespace TeamFight.Host.Models
{
    public class JoinTeamModel
    {
        public Guid TeamId { get; set; }
        public int InvitedCharacterId { get; set; }
    }
}