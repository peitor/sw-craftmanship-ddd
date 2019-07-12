using System;
using BowlingKata.PlayingAGame;
using Commons;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_CurrentScore
    {
        [Test]
        public void InitialScoreShouldBeZero()
        {
            var game = new Game();
            Assert.AreEqual(0, game.CurrentScore());
        }

        [Test]
        public void RunningGame_ReturnsMinus1()
        {
            var game = new Game();
            game.Roll(3);
            game.Roll(5);

            game.Roll(5);

            Assert.AreEqual(13, game.CurrentScore());
        }

        [Test]
        public void StrikesOnly_ShouldReturn300()
        {
            var game = new Game();
            12.Times(() => game.Roll(10));
            Assert.AreEqual(300, game.CurrentScore());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = new Game();
            10.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });

            Assert.AreEqual(90, game.CurrentScore());
        }

        [Test]
        public void FivesOnlyShouldReturn150()
        {
            var game = new Game();

            21.Times(() => game.Roll(5));

            Assert.AreEqual(150, game.CurrentScore());
        }

        [Test]
        public void OneNormalRoll()
        {
            var g = new Game();

            RollAndAssert(g, 3, 3);
        }

        [Test]
        public void TwoNormalRolls()
        {
            var g = new Game();

            RollAndAssert(g, 3, 3);
            RollAndAssert(g, 4, 7);
        }

        [Test]
        public void TODO_ToVerify()
        {
            var g = new Game();

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            Assert.Inconclusive("Shouldn't this be 10 since the frame is not completed?");

            RollAndAssert(g, 0, 12);
        }

        [Test]
        public void OneStrike_2Empty()
        {
            var g = new Game();

            RollAndAssert(g, 10, 10);

            RollAndAssert(g, 1, 12);
            RollAndAssert(g, 1, 14);

            RollAndAssert(g, 0, 14);

        }


        [Test]
        public void SimulateFullGame_2Strikes_LotsOr0()
        {
            var g = new Game();

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

            var game = new Game();
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

            Assert.AreEqual(49, game.CurrentScore());
        }

        [Test]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            var game = new Game();

            Assert.Throws<IndexOutOfRangeException>(() => 22.Times(() => game.Roll(5)));
        }

        private static void RollAndAssert(Game g, int pins, int expectedCurrentScore)
        {
            g.Roll(pins);
            Assert.AreEqual(expectedCurrentScore, g.CurrentScore());
        }
    }
}
