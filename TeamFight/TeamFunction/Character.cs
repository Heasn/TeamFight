using System.Collections.Generic;

namespace TeamFunction
{
    public sealed class Character
    {
        public Character(int id, uint level, uint fatigue, string name)
        {
            Id = id;
            Level = level;
            Fatigue = fatigue;
            Name = name;
            LoadFriends();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public uint Fatigue { get; private set; }
        public GameTeam GameTeam { get; private set; }
        public List<int> Friends { get; private set; }
        public uint Level { get; private set; }

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
            Friends = DbContext.QueryFriendId(Id);
        }
    }
}