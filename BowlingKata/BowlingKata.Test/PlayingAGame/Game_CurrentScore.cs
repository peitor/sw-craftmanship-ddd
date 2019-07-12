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
    }
}
