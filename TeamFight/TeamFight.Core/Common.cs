using System.Collections.Concurrent;

namespace TeamFight.Core
{
    public sealed class Common
    {
        private static readonly ConcurrentDictionary<int, Character> _online =
            new ConcurrentDictionary<int, Character>();

        public static ConcurrentDictionary<int, Character> OnlinePlayers
        {
            get { return _online; }
        }
    }
}