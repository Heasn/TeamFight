﻿// ****************************************
// FileName:ConfirmFightInvitationModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

using System;

namespace TeamFight.Models
{
    public class ConfirmFightInvitationModel
    {
        public Guid TeamId { get; set; }
        public bool ConfirmResult { get; set; }
    }
}