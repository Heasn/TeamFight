// ****************************************
// FileName:VoteCollector.cs
// Description:队伍投票箱
// Tables:Nothing
// Author:陈柏宇
// Create Date:2017-06-16
// Revision History:
// ****************************************

using System.Collections.Generic;
using System.Linq;

namespace TeamFight.Core.Character.Team
{
    public class VoteCollector
    {
        private uint _voteSize;
        private static List<bool> _collector;

        /// <summary>
        /// 功能投票箱
        /// </summary>
        /// <param name="voteSize">投票人数</param>
        public VoteCollector(uint voteSize)
        {
            _voteSize = voteSize;
            _collector = new List<bool>();
        }

        public void ChagneVoteSize(uint newSize)
        {
            //当前没有进行中的投票才能更改投票人数
            if (!_collector.Any())
                _voteSize = newSize;
        }

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="result">投票选项</param>
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