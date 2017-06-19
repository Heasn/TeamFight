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
   /// <summary>
   /// 加入队伍数据模型
   /// </summary>
    public class JoinTeamModel
    {
        /// <summary>
        /// 队伍Id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 被邀请玩家Id
        /// </summary>
        public int InvitedPlayerId { get; set; }
    }
}