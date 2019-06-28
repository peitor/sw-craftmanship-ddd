﻿using System;
using BowlingKata.PlayingAGame;
using Commons;
using NSubstitute;
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
}
