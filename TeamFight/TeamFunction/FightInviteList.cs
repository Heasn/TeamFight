using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TeamFunction
{
    public sealed class FightInviteList
    {
        public enum FightConfirmResult
        {
            WaitToConfirm,
            Accept,
            Refuse
        }

        private static readonly FightInviteList SingletonInstance = new FightInviteList();

        private static readonly Dictionary<GameTeam, FightConfirmResult> Invites =
            new Dictionary<GameTeam, FightConfirmResult>();

        private readonly ReaderWriterLock _readwritelock = new ReaderWriterLock();

        private FightInviteList()
        {
        }

        public static FightInviteList Instance
        {
            get { return SingletonInstance; }
        }

        /// <summary>
        ///     发出战斗请求
        /// </summary>
        /// <param name="team">所在队伍</param>
        /// <returns>发出请求是否成功</returns>
        public bool AddFightInvitation(GameTeam team)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return false;

            bool result;

            try
            {
                if (Invites.ContainsKey(team))
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
                        Invites.Add(team, FightConfirmResult.WaitToConfirm);
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
        ///     根据被邀请角色查找队伍
        /// </summary>
        /// <param name="invitedCharacter">被邀请角色</param>
        /// <returns>是否存在</returns>
        public bool ExistFightInvitation(Character invitedCharacter)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
            {
                return false;
            }

            bool result;

            try
            {
                result = Invites.Any(x => ReferenceEquals(x.Key.Member, invitedCharacter));
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return result;
        }

        /// <summary>
        ///     确认邀请结果
        /// </summary>
        /// <param name="team">队伍</param>
        /// <param name="confirmResult">确认结果</param>
        /// <returns>是否确认完成</returns>
        public bool ConfrimFightInvitation(GameTeam team, bool confirmResult)
        {
            var exist = ExistFightInvitation(team.Member);

            var result = false;

            if (exist)
            {
                _readwritelock.AcquireWriterLock(500);

                if (!_readwritelock.IsWriterLockHeld)
                    return false;

                try
                {
                    Invites[team] = confirmResult ? FightConfirmResult.Accept : FightConfirmResult.Refuse;
                    result = true;
                }
                finally
                {
                    _readwritelock.ReleaseWriterLock();
                }
            }

            return result;
        }

        /// <summary>
        ///     获取确认结果
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns>接收（true）拒绝（false）</returns>
        public FightConfirmResult GetFightInvitationConfirmResult(GameTeam team)
        {
            var exist = ExistFightInvitation(team.Member);

            var result = FightConfirmResult.WaitToConfirm;

            if (exist)
            {
                _readwritelock.AcquireReaderLock(500);

                if (!_readwritelock.IsWriterLockHeld)
                    return FightConfirmResult.WaitToConfirm;

                try
                {
                    result = Invites[team];
                }
                finally
                {
                    _readwritelock.ReleaseReaderLock();
                }
            }

            return result;
        }

        /// <summary>
        ///     移除邀请
        /// </summary>
        /// <param name="team">队伍</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveInvitation(GameTeam team)
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
                    result = Invites.Remove(team);
                    _readwritelock.DowngradeFromWriterLock(ref lockCookie);
                }
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return result;
        }
    }
}