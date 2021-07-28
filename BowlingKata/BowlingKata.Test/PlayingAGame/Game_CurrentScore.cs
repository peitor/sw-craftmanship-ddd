using System;
using BowlingKata.PlayingAGame;
using Commons;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_CurrentScore
    {
        [Test]
        public void InitialScoreShouldBeZero()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.CurrentScore().Should().Be(0);
        }

        [Test]
        public void RunningGame_ReturnsMinus1()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(3);
            game.Roll(5);

            game.Roll(5);

            game.CurrentScore().Should().Be(13);
        }

        [Test]
        public void StrikesOnly_ShouldReturn300()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            12.Times(() => game.Roll(10));

            game.CurrentScore().Should().Be(300);
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = Game.NewGameWithAnonymousPlayer();
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
            var game = Game.NewGameWithAnonymousPlayer();

            21.Times(() => game.Roll(5));

            game.CurrentScore().Should().Be(150);
        }

        [Test]
        public void OneNormalRoll()
        {
            var g = Game.NewGameWithAnonymousPlayer();

            RollAndAssert(g, 3, 3);
        }

        [Test]
        public void TwoNormalRolls()
        {
            var g = Game.NewGameWithAnonymousPlayer();

            RollAndAssert(g, 3, 3);
            RollAndAssert(g, 4, 7);
        }

        [Test]
        public void TODO_ToVerify()
        {
            // TODO
            var g = Game.NewGameWithAnonymousPlayer();

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            Assert.Inconclusive("Shouldn't this be 10 since the frame is not completed?");

            RollAndAssert(g, 0, 12);
        }

        [Test]
        public void OneStrike_2Empty()
        {
            var g = Game.NewGameWithAnonymousPlayer();

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            RollAndAssert(g, 1, 14);

            RollAndAssert(g, 0, 14);
        }


        [Test]
        public void SimulateFullGame_2Strikes_LotsOr0()
        {
            var g = Game.NewGameWithAnonymousPlayer();

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

            var game = Game.NewGameWithAnonymousPlayer();
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
            var game = Game.NewGameWithAnonymousPlayer();

            Action act = () => 22.Times(() => game.Roll(5));

            act.Should().Throw<IndexOutOfRangeException>();
        }

        private static void RollAndAssert(Game g, int pins, int expectedCurrentScore)
        {
            g.Roll(pins);
            g.CurrentScore().Should().Be(expectedCurrentScore);
        }
    }
}