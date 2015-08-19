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
        
        [Test]
        public void ScoreForFrame_SpareInPreviousFrame_ScoreIsKnown()
        {
            game.Roll(7);
            game.Roll(3);

            game.Roll(2);
            game.Roll(6);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(12, scoreForFrame1);
            Assert.AreEqual(20, scoreForFrame2);
        }

        [Test]
        public void ScoreForFrame_Strike_ScoreIsUnknown()
        {
            game.Roll(10);

            int scoreForFrame1 = game.ScoreForFrame(1);

            Assert.AreEqual(-1, scoreForFrame1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_FirstRoll_ScoreIsUnknown()
        {
            game.Roll(10);
            game.Roll(2);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(-1, scoreForFrame1);
            Assert.AreEqual(-1, scoreForFrame2);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_SecondRoll_ScoreIsKnown()
        {
            game.Roll(10);
            
            game.Roll(2);
            game.Roll(6);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(18, scoreForFrame1);
            Assert.AreEqual(26, scoreForFrame2);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_ScoreIsUnknown()
        {
            game.Roll(10);
            game.Roll(10);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(-1, scoreForFrame1);
            Assert.AreEqual(-1, scoreForFrame2);
        }
    }
}