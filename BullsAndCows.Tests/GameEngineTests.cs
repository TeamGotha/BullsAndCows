using System;
using System.IO;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void CalculateBullsAndCowsShouldWorkProperly()
        {
            var engine = GameEngine.Instance;
            var bulsAndCows = engine.CalculateBullsAndCows("1234", "3216");
            CollectionAssert.AreEqual(bulsAndCows, new int[]{1,2});
        }

    }
}
