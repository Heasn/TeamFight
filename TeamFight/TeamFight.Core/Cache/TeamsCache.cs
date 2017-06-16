using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFight.Core.Character;
using TeamFight.Core.Character.Team;

namespace TeamFight.Core.Cache
{
    public class TeamsCache
    {
        private static readonly TeamsCache SingletonInstance = new TeamsCache();
        private static readonly List<GameTeam> Teams = new List<GameTeam>();

        private TeamsCache()
        {

        }

        public static TeamsCache Instance
        {
            get { return SingletonInstance; }
        }

        /// <summary>
        /// 添加队伍
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns></returns>
        public bool AddTeam(GameTeam team)
        {
            if (IsExistTeam(team.Id))
                return false;

            Teams.Add(team);
            return true;
        }

        /// <summary>
        /// 移除队伍
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns></returns>
        public bool RemoveTeam(GameTeam team)
        {
            return Teams.Remove(team);
        }

        /// <summary>
        /// 是否存在某个队伍
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns></returns>
        public bool IsExistTeam(Guid teamId)
        {
            return Teams.Exists(x => x.Id == teamId);
        }

        /// <summary>
        /// 查找队伍
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns></returns>
        public GameTeam FindTeam(Guid teamId)
        {
            return Teams.SingleOrDefault(x => x.Id == teamId);
        }

        /// <summary>
        /// 获取推荐队伍
        /// </summary>
        /// <param name="length">队伍数量</param>
        /// <returns></returns>
        public List<GameTeam> GetRecommendTeams(int length)
        {
            return Teams.AsParallel().Where(x => x.Members == null).OrderBy(x => Guid.NewGuid()).Take(length).ToList();
        }
    }
}
