using System;
using BowlingKata.PlayingAGame;
using NSubstitute;

namespace BowlingKata.Test
{
    public class TestCommons
    {
        public static Game GivenNewGame()
        {
            var game = new Game();
            return game;
        }

        public static Action<GameFinishedData> GivenEventFinishedHandler(Game game)
        {
            var gameFinishedHandler = Substitute.For<Action<GameFinishedData>>();
            game.GameFinished += gameFinishedHandler;
            return gameFinishedHandler;
        }
    }
}