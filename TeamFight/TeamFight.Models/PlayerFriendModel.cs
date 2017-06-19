// ****************************************
// FileName:PlayerFriendModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

namespace TeamFight.Models
{
    /// <summary>
    /// 玩家好友数据模型
    /// </summary>
    public class PlayerFriendModel
    {
        /// <summary>
        /// 好友角色Id
        /// </summary>
        public int FriendId { get;  set; }

        /// <summary>
        /// 好友姓名
        /// </summary>
        public string FriendName { get; set; }

        /// <summary>
        /// 好友战力
        /// </summary>
        public uint FriendCe { get;  set; }

        /// <summary>
        /// 好友性别
        /// </summary>
        public bool FriendGender { get;  set; }

        /// <summary>
        /// 好友是否在线
        /// </summary>
        public bool IsOnline { get; set; }

    }
}