// ****************************************
// FileName:InviteCharacterModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

namespace TeamFight.Models
{
    /// <summary>
    /// 邀请玩家数据模型
    /// </summary>
    public class InviteCharacterModel
    {
        /// <summary>
        /// 玩家Id
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 被邀请玩家Id
        /// </summary>
        public int InvitedPlayerId { get; set; }
    }
}