using System;
using FakeItEasy;

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
            var gameFinishedHandler = A.Fake<Action<GameFinishedData>>();
            game.GameFinished += gameFinishedHandler;
            return gameFinishedHandler;
        }
    }
}