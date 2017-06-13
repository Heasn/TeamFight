using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFight.Tests
{
    [TestClass]
    public class CharacterTestClass
    {
        [TestMethod]
        public void LoadFriendsTest()
        {
            TeamFunction.Character chr = new TeamFunction.Character(1);
            Assert.AreNotEqual(chr.Friends, null);
            Assert.AreNotEqual(chr.Friends.Count, 0);
        }
    }
}
