using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BullsAndCows.Tests
{
    using Engine;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScoreboardTests
    {
        [TestMethod]
        public void AddScoreShouldAddNumber()
        {
            var scoreboard = new Scoreboard();
            scoreboard.AddScore("pesho", 11);
            var result = scoreboard.Results;
            var expected = new Dictionary<string, int> {{"pesho", 11}};
            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InvalidScoreShouldThrowException()
        {
            var scoreboard = new Scoreboard();
            scoreboard.AddScore("pesho", -12);
        }

        [TestMethod]
        public void SortingScoresShouldWorkProperly ()
        {
            var result = new Dictionary<string, int>();
            result.Add("eeeeee", 1);
            result.Add("sdfs", 56);
            

            var scboard = new Scoreboard();
            scboard.AddScore("eeeeee", 1);
            scboard.AddScore("sdfs", 3);
            scboard.AddScore("sdfs", 4);
            scboard.AddScore("sdfs", 56);
            scboard.AddScore("sdfs", 2);
            scboard.AddScore("sdfs", 5);
            
            CollectionAssert.AreEqual(scboard.Results, result);
        
        }
    }
}
