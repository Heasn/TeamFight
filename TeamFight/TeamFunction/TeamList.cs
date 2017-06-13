using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace TeamFunction
{
    public class TeamList
    {
        public static ConcurrentBag<Guid> Teams { get; } = new ConcurrentBag<Guid>();

        private static readonly object lockobject = new object();

        public static IEnumerable<Guid> GetRecommendTeam()
        {
            if (Monitor.TryEnter(lockobject, 1000))
            {
                var ac = new List<Guid>(Teams);
                var result = ac.OrderBy(x => Guid.NewGuid()).Take(5);
                Monitor.Exit(lockobject);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}