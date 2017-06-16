using System.Collections.Generic;
using System.Linq;

namespace TeamFight.Core.Character.Team
{
    public class VoteCollector
    {
        private static int _voteSize;
        private static List<bool> _collector;

        /// <summary>
        /// 功能投票箱
        /// </summary>
        /// <param name="voteSize">投票人数</param>
        public VoteCollector(int voteSize)
        {
            _voteSize = voteSize;
            _collector = new List<bool>();
        }

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="result"></param>
        public void Vote(bool result)
        {
            _collector.Add(result);
        }

        /// <summary>
        /// 获取投票结果
        /// </summary>
        /// <returns>NULL表示投票还未完成，True表示通过，False表示不通过</returns>
        public bool? GetFinalResult()
        {
            if (_collector.Count == _voteSize)
            {
                _collector.Clear();
                return _collector.All(x => x);
            }

            return null;
        }
    }
}
