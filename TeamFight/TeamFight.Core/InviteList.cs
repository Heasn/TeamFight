using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TeamFight.Core
{
    public sealed class InviteList
    {
        private static readonly InviteList SingletonInstance = new InviteList();
        private static readonly List<Tuple<GameTeam, Character>> Invites = new List<Tuple<GameTeam, Character>>();
        private readonly ReaderWriterLock _readwritelock = new ReaderWriterLock();

        private InviteList()
        {
        }

        public static InviteList Instance
        {
            get { SingletonInstance; }
        }

        /// <summary>
        ///     添加邀请
        /// </summary>
        /// <param name="team">邀请所在队伍</param>
        /// <param name="invitedCharacter">被邀请的角色</param>
        /// <returns>邀请是否成功</returns>
        public bool AddInvitation(GameTeam team, Character invitedCharacter)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return false;

            bool result;

            try
            {
                if (Invites.Exists(x => ReferenceEquals(x.Item1, team) && ReferenceEquals(x.Item2, invitedCharacter)))
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
                        Invites.Add(Tuple.Create(team, invitedCharacter));
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
        /// <returns>队伍</returns>
        public GameTeam FindInvitation(Character invitedCharacter)
        {
            _readwritelock.AcquireReaderLock(500);

            if (!_readwritelock.IsReaderLockHeld)
                return null;

            Tuple<GameTeam, Character> tuple;

            try
            {
                tuple = Invites.SingleOrDefault(x => ReferenceEquals(x.Item2, invitedCharacter));
            }
            finally
            {
                _readwritelock.ReleaseReaderLock();
            }

            return tuple == null ? null : tuple.Item1;
        }

        /// <summary>
        ///     移除邀请
        /// </summary>
        /// <param name="team">队伍</param>
        /// <param name="invitedCharacter">被邀请角色</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveInvitation(GameTeam team, Character invitedCharacter)
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
                    Invites.RemoveAll(x => ReferenceEquals(x.Item1, team));
                    Invites.RemoveAll(x => ReferenceEquals(x.Item2, invitedCharacter));

                    _readwritelock.DowngradeFromWriterLock(ref lockCookie);
                    result = true;
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