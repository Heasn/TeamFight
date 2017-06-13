using System;
using System.Collections.Generic;

namespace TeamFunction
{
    public sealed class Character
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public uint Fatigue { get; private set; }

        public GameTeam GameTeam { get; private set; }

        public List<int> Friends { get; private set; }

        public Character(int id)
        {
            Id = id;
            LoadFriends();
        }

        public bool CreateTeam()
        {
            if (GameTeam == null)
            {
                GameTeam = GameTeam.Create(this);
                return true;
            }
            return false;
        }

        public bool QuitTeam()
        {
            GameTeam = null;
            return true;
        }


        private void LoadFriends()
        {
            Friends = Tools.DbContext.QueryFriendId(Id);
        }
    }
}