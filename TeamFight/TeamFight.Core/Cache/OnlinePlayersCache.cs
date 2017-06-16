using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFight.Core.Character;

namespace TeamFight.Core.Cache
{
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

        public bool AddPlayer(Player player)
        {
            if (PlayersCache.ContainsKey(player.Id))
                return false;

            PlayersCache.Add(player.Id, player);
            return true;
        }

        public bool RemovePlayer(Player player)
        {
            return PlayersCache.Remove(player.Id);
        }

        public bool IsPlayerOnline(int charId)
        {
            return PlayersCache.ContainsKey(charId);
        }

        public Player FindPlayer(int charId)
        {
            Player player;
            return PlayersCache.TryGetValue(charId, out player) ? player : null;
        }
    }
}
