using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamFunction;

namespace TeamFight.Tests
{
    [TestClass]
    public class CharacterTest
    {
        [TestMethod]
        public void LoadCharacterTest()
        {
            var chr = DbContext.LoadCharacter(1);

            Console.WriteLine("玩家名字：" + chr.Name);
            Console.WriteLine("玩家等级：" + chr.Level);
            Console.WriteLine("玩家疲劳度：" + chr.Fatigue);


            Assert.AreNotEqual(chr.Friends, null);
            Assert.AreNotEqual(chr.Friends.Count, 0);
        }
    }
}