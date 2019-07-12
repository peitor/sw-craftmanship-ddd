﻿using System;
using BowlingKata.PlayingAGame;
using Commons;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_TotalScore
    {
        [Test]
        public void InitialScoreShouldBeZero()
        {
            var game = new Game();
            Assert.AreEqual(0, game.TotalScore());
        }

        [Test]
        public void StrikesOnlyShouldReturn300()
        {
            var game = new Game();
            12.Times(() => game.Roll(10));
            Assert.AreEqual(300, game.TotalScore());
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

            Assert.AreEqual(90, game.TotalScore());
        }

        [Test]
        public void FivesOnlyShouldReturn150()
        {
            var game = new Game();

            21.Times(() => game.Roll(5));

            Assert.AreEqual(150, game.TotalScore());
        }

        [Test]
        public void RollingMoreThen21TimesShouldThrowException()
        {
            var game = new Game();

            Assert.Throws<IndexOutOfRangeException>(() => 22.Times(() => game.Roll(5)));
        }
    }
}