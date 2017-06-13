using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFunction
{
    public class InviteList
    {
        public static ConcurrentBag<Tuple<Guid, Character>> Invites { get; } =
            new ConcurrentBag<Tuple<Guid, Character>>();
    }
}
