using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamFight
{
    public sealed class Common
    {
        private static readonly ConcurrentDictionary<int, TeamFunction.Character> _online =
            new ConcurrentDictionary<int, TeamFunction.Character>();

        public static ConcurrentDictionary<int, TeamFunction.Character> OnlinePlayers
        {
            get { return _online; }
        }
            
    }
}