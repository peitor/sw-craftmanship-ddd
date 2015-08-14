using System;
using NUnit.Framework;

namespace BowlingKata.Test
{
    public class GameTest
    {
        private Game game;

        [SetUp]
        public void BeforeTest()
        {
            game = new Game();
        }

        [Test]
        public void InitialScoreShouldBeZero()
        {
            Assert.AreEqual(0, game.Score());
        }

        [Test]
        public void StrikesOnlyShouldReturn300()
        {
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);

            Assert.AreEqual(300, game.Score());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            game.Roll(9);
            game.Roll(0);

            Assert.AreEqual(90, game.Score());
        }

        [Test]
        public void FivesOnlyShouldReturn150()
        {
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            
            Assert.AreEqual(150, game.Score());
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
        }

        [Test]
        public void ScoreForFrame_OneRoll()
        {
            game.Roll(1);

            int score = game.ScoreForFrame(1);

            Assert.AreEqual(1, score);
        }

        [Test]
        public void ScoreForFrame_TwoRolls()
        {
            game.Roll(1);
            game.Roll(4);

            int score = game.ScoreForFrame(1);

            Assert.AreEqual(5, score);
        }
        
        [Test]
        public void ScoreForFrame_SpareInCurrentFrame_ScoreIsUnknown()
        {
            game.Roll(7);
            game.Roll(3);

            int score = game.ScoreForFrame(1);

            Assert.AreEqual(-1, score);
        }
    }
}