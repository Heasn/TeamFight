// ****************************************
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
    /// <summary>
    /// 确认战斗邀请结果数据模型
    /// </summary>
    public class ConfirmFightInvitationModel
    {
        //队伍Id
        public Guid TeamId { get; set; }

        //确认结果
        public bool ConfirmResult { get; set; }
    }
}