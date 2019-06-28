using System;
using Commons;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
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
}
