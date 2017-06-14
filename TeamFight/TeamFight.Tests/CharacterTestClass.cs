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
        public void LoadCharacterTest()
        {
            TeamFunction.Character chr = TeamFunction.DbContext.LoadCharacter(1);

            Console.WriteLine("玩家名字：" + chr.Name);
            Console.WriteLine("玩家等级：" + chr.Level);
            Console.WriteLine("玩家疲劳度：" + chr.Fatigue);
            

            Assert.AreNotEqual(chr.Friends, null);
            Assert.AreNotEqual(chr.Friends.Count, 0);
        }


    }
}
