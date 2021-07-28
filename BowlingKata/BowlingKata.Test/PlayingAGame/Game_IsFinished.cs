using System;
using BowlingKata.PlayingAGame;
using Commons;
using NSubstitute;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_IsFinished
    {
        [Test]
        public void TwoRolls_NotFinished()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(1);

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void EightRolls_NotFinished()
        {
            var game = new Game();
            8.Times(() => game.Roll(10));

            Assert.That(game.IsFinished == false);
        }

        [Test]
        public void TwelveStrikes_GameFinished()
        {
            var game = new Game();
            12.Times(() => game.Roll(10));
            Assert.That(game.IsFinished);
        }

        [Test]
        public void TwelveStrikes_GameFinished_EventsRaised()
        {
            var game = new Game();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            12.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void ThirteenStrikes_GameFinished_EventsRaised()
        {
            var game = new Game();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            13.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = new Game();
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
            var game = new Game();
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            Assert.That(game.IsFinished == false);

            gameFinishedHandler.DidNotReceive().Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void TwelveStrikes_GameFinished_ReturnPlayerNameInEvent()
        {
            var game = Game.NewGameWithPlayer("Peter");
            var gameFinishedHandler = GivenEventFinishedHandler(game);

            12.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Is<GameFinishedData>(data => data.PlayerName == "Peter"));
        }

        [Test]
        public void ASSUMPTION__ComplicatedGame_NotFinished()
        {
            var game = new Game();
            //TODO: Sophisticated games could break the logic in the "return" statement?? <-- Assumption
            13.Times(() => game.Roll(1));

            game.Roll(1);
            game.Roll(1);
            // TODO: play around here with this....
            Assert.That(game.IsFinished == false);
        }

        private static Action<GameFinishedData> GivenEventFinishedHandler(Game game)
        {
            var gameFinishedHandler = Substitute.For<Action<GameFinishedData>>();
            game.GameFinished += gameFinishedHandler;
            return gameFinishedHandler;
        }
    }
}