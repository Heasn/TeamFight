// ****************************************
// FileName:DataFactory.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-19
// Revision History:
// ****************************************

namespace TeamFight.Core.Data
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 数据工厂类型
        /// </summary>
        public enum FactoryType
        {
            Player,
            Friend
        }

        /// <summary>
        /// 创建数据工厂
        /// </summary>
        /// <param name="type">数据工厂类型</param>
        /// <returns></returns>
        public static IFactory Create(FactoryType type)
        {
            switch (type)
            {
                case FactoryType.Player:
                    return new PlayersFactory();
                case FactoryType.Friend:
                    return new FriendsFactory();
                default:
                    return null;
            }
        }
    }
}
