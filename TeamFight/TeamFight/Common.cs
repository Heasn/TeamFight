using System.Collections.Concurrent;
using TeamFunction;

namespace TeamFight
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