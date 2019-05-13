using System;
using Commons;
using NUnit.Framework;

namespace BowlingKata.Test
{
    public class TotalScoreAfterEndOfGame
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
            Assert.AreEqual(0, game.TotalScore());
        }

        [Test]
        public void StrikesOnlyShouldReturn300()
        {
            12.Times(() => game.Roll(10));
            Assert.AreEqual(300, game.TotalScore());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
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
            21.Times(() => game.Roll(5));

            Assert.AreEqual(150, game.TotalScore());
        }

        [Test]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            Assert.Throws<IndexOutOfRangeException>(() => 22.Times(() => game.Roll(5)));
        }
    }

    public class ScoreBoardWhilePlaying
    {
        private Game game;

        [SetUp]
        public void BeforeTest()
        {
            game = new Game();
        }


        [Test]
        public void ScoreForFrame_OneRoll()
        {
            var game = new Game();
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

        [Test]
        public void ScoreForFrame_TwoStrikes_FirstRollInThirdFrame_ScoreIsUnknown()
        {
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
            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            int scoreForFrame1 = game.ScoreForFrame(1);
            int scoreForFrame2 = game.ScoreForFrame(2);

            Assert.AreEqual(11, scoreForFrame1);
            Assert.AreEqual(12, scoreForFrame2);
        }
    }

    public class GameFinishesDetection
    {
        private Game game;

        [SetUp]
        public void BeforeTest()
        {
            game = new Game();
        }

        [Test]
        public void TwoRolls_NotFinished()
        {
            game.Roll(1);
            game.Roll(1);

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void EightRolls_NotFinished()
        {
            8.Times(() => game.Roll(10));

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void TwelveStrikes_GameFinished()
        {
            12.Times(() => game.Roll(10));
            Assert.That(game.IsFinished);
        }

        [Test]
        public void TwelveStrikes_GameFinished_EventsRaised()
        {
            ListenToEvents();
            12.Times(() => game.Roll(10));
            Assert.That(gameFinishedEventCalledNumberOfTimes == 1);
        }

        [Test]
        public void ThirteenStrikes_GameFinished_EventsRaised()
        {
            ListenToEvents();
            13.Times(() => game.Roll(10));
            Assert.AreEqual(1, gameFinishedEventCalledNumberOfTimes);
        }


        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
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
            ListenToEvents();
            Assert.That(game.IsFinished == false);
            Assert.That(gameFinishedEventCalledNumberOfTimes == 0);
        }

        [Test]
        public void ASSUMPTION__ComplicatedGame_NotFinished()
        {
            //TODO: Sophisticated games could break the logic in the "return" statement?? <-- Assumption
            13.Times(() => game.Roll(1));

            game.Roll(1);
            game.Roll(1);
            // TODO: play around here with this....
            Assert.That(game.IsFinished == false);
        }

        private void ListenToEvents()
        {
            // call how often the event was called
            gameFinishedEventCalledNumberOfTimes = 0;
            game.GameFinished += GameFinishedEventCalled;
        }

        int gameFinishedEventCalledNumberOfTimes;
        private void GameFinishedEventCalled(int obj)
        {
            gameFinishedEventCalledNumberOfTimes++;
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

        //  TODO LIST:
        //  3 top games finished, 1 new top game finishes -> assert on result
        // 
        //  Simulate games and verify if we really see the final topscore --> Assumption event gets raised too early.
        //  DISCUSSION: 
        //  What does "game finishes" mean?
        //  write 1 integration test, is that enough?

    }
}
