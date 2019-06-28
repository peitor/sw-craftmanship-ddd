using BowlingKata.ScoreBoardWhilePlaying;
using NUnit.Framework;

namespace BowlingKata.Test.ScoreBoardWhilePlaying
{
    public class VerifyScoreBoardWhilePlaying : TestCommons
    {
        [Test]
        public void ScoreForFrame_OneRoll()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard.game.Roll(1);

            int score = scoreboard.game.ScoreForFrame(1);

            Assert.AreEqual(1, score);
        }

        [Test]
        public void ScoreForFrame_TwoRolls()
        {
            var game = GivenNewGame();
            game.Roll(1);
            game.Roll(4);

            int score = game.ScoreForFrame(1);

            Assert.AreEqual(5, score);
        }

        [Test]
        public void ScoreForFrame_SpareInCurrentFrame_ScoreIsUnknown()
        {
            var game = GivenNewGame();
            game.Roll(7);
            game.Roll(3);

            int score = game.ScoreForFrame(1);

            Assert.AreEqual(-1, score);
        }

        [Test]
        public void ScoreForFrame_SpareInPreviousFrame_ScoreIsKnown()
        {
            var game = GivenNewGame();
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
            var game = GivenNewGame();
            game.Roll(10);

            int scoreForFrame1 = game.ScoreForFrame(1);

            Assert.AreEqual(-1, scoreForFrame1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_FirstRoll_ScoreIsUnknown()
        {
            var game = GivenNewGame();
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
            var game = GivenNewGame();
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
            var game = GivenNewGame();
            game.Roll(10);
            game.Roll(10);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(-1, scoreForFrame1);
            Assert.AreEqual(-1, scoreForFrame2);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_FirstRollInThirdFrame_ScoreIsUnknown()
        {
            var game = GivenNewGame();
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);
            int scoreForFrame3 = game.ScoreForFrame(3);

            Assert.AreEqual(22, scoreForFrame1);
            Assert.AreEqual(-1, scoreForFrame2);
            Assert.AreEqual(-1, scoreForFrame3);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_SecondRollInThirdFrame_ScoreIsKnown()
        {
            var game = GivenNewGame();
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);
            game.Roll(7);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);
            int scoreForFrame3 = game.ScoreForFrame(3);

            Assert.AreEqual(22, scoreForFrame1);
            Assert.AreEqual(41, scoreForFrame2);
            Assert.AreEqual(50, scoreForFrame3);
        }

        [Test]
        public void ScoreForFrame_SpareAfterAStrike_ScoreIsUnknown()
        {
            var game = GivenNewGame();
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(20, scoreForFrame1);
            Assert.AreEqual(-1, scoreForFrame2);
        }

        [Test]
        public void ScoreForFrame_StrikeSpareRoll_ScoreIsKnown()
        {
            var game = GivenNewGame();
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);
            int scoreForFrame3 = game.ScoreForFrame(3);

            Assert.AreEqual(20, scoreForFrame1);
            Assert.AreEqual(31, scoreForFrame2);
            Assert.AreEqual(32, scoreForFrame3);
        }

        [Test]
        public void ScoreForFrame_Spare_FirstRollInSecondFrame_ScoreIsKnown()
        {
            var game = GivenNewGame();
            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(11, scoreForFrame1);
            Assert.AreEqual(12, scoreForFrame2);
        }
    }
}