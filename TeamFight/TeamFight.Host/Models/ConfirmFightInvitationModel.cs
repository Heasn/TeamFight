// ****************************************
// FileName:ConfirmFightInvitationModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-15
// Revision History:
// ****************************************

using System;

namespace TeamFight.Host.Models
{
    public class ConfirmFightInvitationModel
    {
        public Guid TeamId { get; set; }
        public bool ConfirmResult { get; set; }
    }
}