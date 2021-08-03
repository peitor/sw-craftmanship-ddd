using System;
using BowlingKata.ScoreBoardWhilePlaying;
using Commons;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata.Test.ScoreBoardWhilePlaying
{
    public class ScoreBoard_VerifyWhilePlaying
    {
        [Test]
        public void InitialScoreShouldBeZero()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["any"].CurrentScore().Should().Be(0);
        }
        
        
        [Test]
        public void JustStarted_ScoreIsZero()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Peter"].CurrentScore().Should().Be(0);
        }


        [Test]
        public void Scenario_Simulate2Players_AssertInTheMiddle()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Peter"].CurrentScore().Should().Be(0);
            scoreboard["Sepp"].CurrentScore().Should().Be(0);

            scoreboard["Peter"].Roll(1);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);


            scoreboard["Peter"].CurrentScore().Should().Be(41);
            scoreboard["Sepp"].CurrentScore().Should().Be(4);
        }
        
        
         [Test]
        public void RunningGame_ReturnsMinus1()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];
            game.Roll(3);
            game.Roll(5);

            game.Roll(5);

            game.CurrentScore().Should().Be(13);
        }

        [Test]
        public void StrikesOnly_ShouldReturn300()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];
            12.Times(() => game.Roll(10));

            game.CurrentScore().Should().Be(300);
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];
            10.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });

            game.CurrentScore().Should().Be(90);
        }

        [Test]
        public void FivesOnlyShouldReturn150()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];

            21.Times(() => game.Roll(5));

            game.CurrentScore().Should().Be(150);
        }

        [Test]
        public void OneNormalRoll()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];

            RollAndAssert(game, 3, 3);
        }

        [Test]
        public void TwoNormalRolls()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];

            RollAndAssert(game, 3, 3);
            RollAndAssert(game, 4, 7);
        }

        [Test]
        public void TODO_ToVerify()
        {
            // TODO
            Scoreboard scoreboard = new Scoreboard();
            var g = scoreboard["1stGame"];

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            Assert.Inconclusive("Shouldn't this be 10 since the frame is not completed?");

            RollAndAssert(g, 0, 12);
        }

        [Test]
        public void OneStrike_2Empty()
        {
            Scoreboard scoreboard = new Scoreboard();
            var g = scoreboard["1stGame"];

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            RollAndAssert(g, 1, 14);

            RollAndAssert(g, 0, 14);
        }


        [Test]
        public void SimulateFullGame_2Strikes_LotsOr0()
        {
            Scoreboard scoreboard = new Scoreboard();
            var g = scoreboard["1stGame"];

            RollAndAssert(g, 10, 10);
            RollAndAssert(g, 0, 10);
            RollAndAssert(g, 0, 10);

            RollAndAssert(g, 2, 12);
            RollAndAssert(g, 3, 15);

            RollAndAssert(g, 10, 25);

            RollAndAssert(g, 5, 35);
            RollAndAssert(g, 5, 45);

            RollAndAssert(g, 2, 49);
            RollAndAssert(g, 0, 49);

            RollAndAssert(g, 0, 49);
            RollAndAssert(g, 0, 49);

            RollAndAssert(g, 0, 49);
            RollAndAssert(g, 0, 49);
        }

        [Test]
        public void SimulateGame_WithStrikes_And_LotsOfEmpty()
        {
            // Simulation validated by http://www.sportcalculators.com/bowling-score-calculator

            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];
            game.Roll(10);

            game.Roll(0);
            game.Roll(0);

            game.Roll(2);
            game.Roll(3);

            game.Roll(10);

            game.Roll(5);
            game.Roll(5);

            game.Roll(2);
            game.Roll(0);

            game.Roll(0);
            game.Roll(0);

            game.Roll(0);
            game.Roll(0);

            game.Roll(0);
            game.Roll(0);

            game.Roll(0);
            game.Roll(0);

            game.CurrentScore().Should().Be(49);
        }


        [Test]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            Scoreboard scoreboard = new Scoreboard();
            var game = scoreboard["1stGame"];

            Action act = () => 22.Times(() => game.Roll(5));

            act.Should().Throw<IndexOutOfRangeException>();
        }

        private static void RollAndAssert(ScoreWhilePlayingGame g, int pins, int expectedCurrentScore)
        {
            g.Roll(pins);
            g.CurrentScore().Should().Be(expectedCurrentScore);
        }
    }
}