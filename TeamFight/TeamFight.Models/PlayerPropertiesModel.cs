// ****************************************
// FileName:PlayerPropertiesModel.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************
namespace TeamFight.Models
{
    public class PlayerPropertiesModel
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家性别（false：女性，true：男性）
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 玩家等级
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// 玩家体力
        /// </summary>
        public uint PhysicalStrength { get; set; }

        /// <summary>
        /// 玩家耐力
        /// </summary>
        public uint Endurance { get; set; }

        /// <summary>
        /// 玩家战力
        /// </summary>
        public uint CombatEffectiveness { get; set; }
    }
}