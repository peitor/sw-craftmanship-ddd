using System;
using System.Linq;
using Commons;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace BowlingKata.Test
{
    public class TotalScoreAfterEndOfGame : TestCommons
    {
        [Test]
        public void InitialScoreShouldBeZero()
        {
            var game = GivenNewGame();
            Assert.AreEqual(0, game.TotalScore());
        }

        [Test]
        public void StrikesOnlyShouldReturn300()
        {
            var game = GivenNewGame();
            12.Times(() => game.Roll(10));
            Assert.AreEqual(300, game.TotalScore());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = GivenNewGame();
            10.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });

            Assert.AreEqual(90, game.TotalScore());
        }

        [Test]
        public void FivesOnlyShouldReturn150()
        {
            var game = GivenNewGame();

            21.Times(() => game.Roll(5));

            Assert.AreEqual(150, game.TotalScore());
        }

        [Test]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            var game = GivenNewGame();

            Assert.Throws<IndexOutOfRangeException>(() => 22.Times(() => game.Roll(5)));
        }
    }

    public class ScoreBoardWhilePlaying : TestCommons
    {
        [Test]
        public void ScoreForFrame_OneRoll()
        {
            var game = GivenNewGame();
            game.Roll(1);

            int score = game.ScoreForFrame(1);

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

    public class GameFinishesDetection : TestCommons
    {
        [Test]
        public void TwoRolls_NotFinished()
        {
            var game = GivenNewGame();
            game.Roll(1);
            game.Roll(1);

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void EightRolls_NotFinished()
        {
            var game = GivenNewGame();
            8.Times(() => game.Roll(10));

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void TwelveStrikes_GameFinished()
        {
            var game = GivenNewGame();
            12.Times(() => game.Roll(10));
            Assert.That(game.IsFinished);
        }

        [Test]
        public void TwelveStrikes_GameFinished_EventsRaised()
        {
            var game = GivenNewGame();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            12.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());

        }

        [Test]
        public void ThirteenStrikes_GameFinished_EventsRaised()
        {
            var game = GivenNewGame();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            13.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = GivenNewGame();
            5.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });
            Assert.That(game.IsFinished == false);

            5.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });

            Assert.That(game.IsFinished);
        }

        [Test]
        public void NoRolls_NotFinished()
        {
            var game = GivenNewGame();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            Assert.That(game.IsFinished == false);

            gameFinishedHandler.DidNotReceive().Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void TwelveStrikes_GameFinished_ReturnPlayerNameInEvent()
        {
            var game = new Game { PlayerName = "Peter" };
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            12.Times(() => game.Roll(10));
             
            gameFinishedHandler.Received(1).Invoke(Arg.Is<GameFinishedData>(data => data.PlayerName == "Peter"));
        }

        [Test]
        public void ASSUMPTION__ComplicatedGame_NotFinished()
        {
            var game = GivenNewGame();
            //TODO: Sophisticated games could break the logic in the "return" statement?? <-- Assumption
            13.Times(() => game.Roll(1));

            game.Roll(1);
            game.Roll(1);
            // TODO: play around here with this....
            Assert.That(game.IsFinished == false);
        }
    }

    public class VisualizeHallOfFame
    {
        [Test]
        public void NoGame_NoHallOfFame()
        {
            var world = new World();
            Assert.AreEqual(0, world.hallOfFame.Length);
        }

        [Test]
        public void OneGameFinished_SeeIt()
        {
            var world = new World();
            world.gameSimulator.FinishGame();
            Assert.AreEqual(1, world.hallOfFame.Length);
        }

    }
}
