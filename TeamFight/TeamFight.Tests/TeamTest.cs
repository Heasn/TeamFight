using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TeamFight.Tests
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void CreateTest()
        {
            TeamFunction.Character chr = TeamFunction.DbContext.LoadCharacter(1);

            Assert.AreNotEqual(chr.CreateTeam(), false);
            Assert.AreNotEqual(chr.GameTeam, null);
        }

        [TestMethod]
        public void JoinTest()
        {
            TeamFunction.Character captain = TeamFunction.DbContext.LoadCharacter(1);
            TeamFunction.Character member = TeamFunction.DbContext.LoadCharacter(2);

            Assert.AreNotEqual(captain.CreateTeam(), false);
            Assert.AreNotEqual(captain.GameTeam, null);

            captain.GameTeam.AddMember(captain, member);

            Assert.AreNotEqual(captain.GameTeam.Member, null);
        }
    }
}
