// ****************************************
// FileName:OnlinePlayersCache.cs
// Description:
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Collections.Generic;

namespace TeamFight.Core.Cache
{
    using Character;

    /// <summary>
    /// 在线玩家缓存
    /// </summary>
    public class OnlinePlayersCache
    {
        private static readonly OnlinePlayersCache SingletonInstance = new OnlinePlayersCache();
        private static readonly Dictionary<int, Player> PlayersCache = new Dictionary<int, Player>();

        private OnlinePlayersCache()
        {
        }

        public static OnlinePlayersCache Instance
        {
            get { return SingletonInstance; }
        }

        /// <summary>
        /// 添加玩家
        /// </summary>
        /// <param name="player">要添加的玩家</param>
        /// <returns></returns>
        public bool AddPlayer(Player player)
        {
            if (PlayersCache.ContainsKey(player.Id))
            {
                return false;
            }

            PlayersCache.Add(player.Id, player);
            return true;
        }

        /// <summary>
        /// 移除玩家
        /// </summary>
        /// <param name="player">被移除的玩家</param>
        /// <returns></returns>
        public bool RemovePlayer(Player player)
        {
            return PlayersCache.Remove(player.Id);
        }

        /// <summary>
        /// 查询某玩家是否在线   
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public bool IsPlayerOnline(int playerId)
        {
            return PlayersCache.ContainsKey(playerId);
        }

        /// <summary>
        /// 查找玩家
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <returns></returns>
        public Player FindPlayer(int playerId)
        {
            Player player;
            return PlayersCache.TryGetValue(playerId, out player) ? player : null;
        }
    }
}