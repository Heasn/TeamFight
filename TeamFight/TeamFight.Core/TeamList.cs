using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TeamFight.Core
{
    public class TeamList
    {
        private static readonly TeamList SingletonInstance = new TeamList();
        private static readonly List<GameTeam> Teams = new List<GameTeam>();
        private readonly ReaderWriterLock _readwritelock = new ReaderWriterLock();

        private TeamList()
        {
        }

        public static TeamList Instance
        {
            get { return SingletonInstance; }
        } 

        /// <summary>
        ///     添加队伍
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns>添加是否成功</returns>
        public bool AddTeam(GameTeam team)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return false;

            bool result;

            try
            {
                if (Teams.Exists(x => ReferenceEquals(x, team)))
                {
                    result = false;
                }
                else
                {
                    var lockCookie = _readwritelock.UpgradeToWriterLock(500);
                    if (!_readwritelock.IsWriterLockHeld)
                    {
                        result = false;
                    }
                    else
                    {
                        Teams.Add(team);
                        _readwritelock.DowngradeFromWriterLock(ref lockCookie);
                        result = true;
                    }
                }
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return result;
        }

        /// <summary>
        ///     根据队伍Id查找组队
        /// </summary>
        /// <param name="teamId">队伍Id</param>
        /// <returns>查找到时返回该队伍，未查找到时返回null</returns>
        public GameTeam FindTeam(Guid teamId)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return null;

            GameTeam team;

            try
            {
                team = Teams.SingleOrDefault(x => x.Id == teamId);
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return team;
        }

        /// <summary>
        ///     移除队伍
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveTeam(GameTeam team)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return false;

            bool result;

            try
            {
                var lockCookie = _readwritelock.UpgradeToWriterLock(500);

                if (!_readwritelock.IsWriterLockHeld)
                {
                    result = false;
                }
                else
                {
                    result = Teams.Remove(team);
                    _readwritelock.DowngradeFromWriterLock(ref lockCookie);
                }
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return result;
        }

        /// <summary>
        ///     获取随机推荐的队伍
        /// </summary>
        /// <param name="length">推荐数量</param>
        /// <returns>成功返回队伍列表，失败返回null</returns>
        public List<GameTeam> GetRecommendTeams(int length)
        {
            _readwritelock.AcquireReaderLock(500);

            if (_readwritelock.IsReaderLockHeld)
                return null;

            List<GameTeam> teams;

            try
            {
                teams = Teams.AsParallel().OrderBy(x => Guid.NewGuid()).Take(length).ToList();
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return teams;
        }
    }
}