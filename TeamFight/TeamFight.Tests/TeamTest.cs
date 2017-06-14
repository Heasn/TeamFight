using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamFunction;

namespace TeamFight.Tests
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var chr = DbContext.LoadCharacter(1);

            Assert.AreNotEqual(chr.CreateTeam(), false);
            Assert.AreNotEqual(chr.GameTeam, null);
        }

        [TestMethod]
        public void JoinTest()
        {
            var captain = DbContext.LoadCharacter(1);
            var member = DbContext.LoadCharacter(2);

            Assert.AreNotEqual(captain.CreateTeam(), false);
            Assert.AreNotEqual(captain.GameTeam, null);

            captain.GameTeam.AddMember(captain, member);

            Assert.AreNotEqual(captain.GameTeam.Member, null);
        }
    }
}