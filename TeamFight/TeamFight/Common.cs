using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight
{
    public sealed class Common
    {
        public static ConcurrentDictionary<int, TeamFunction.Character> OnlinePlayers { get; } =
            new ConcurrentDictionary<int, TeamFunction.Character>();
    }
}