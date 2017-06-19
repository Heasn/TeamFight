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
    /// <summary>
    /// 推荐队伍数据模型
    /// </summary>
    public class RecommendTeamModel
    {
        /// <summary>
        /// 队伍Id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 队长姓名
        /// </summary>
        public string CaptainName { get; set; }

        /// <summary>
        /// 队长等级
        /// </summary>
        public uint CaptainLevel { get; set; }

        /// <summary>
        /// 队长FaceId
        /// </summary>
        public int CaptainFaceId { get; set; }
    }
}