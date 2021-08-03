using System;
using BowlingKata.PlayingAGame;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_CurrentScore_Infrastructure
    {
        private RollEventData receivedEventDataRollEventEvent;

        [Test]
        public void RaisesRollEvent()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var gameFinishedHandler = Substitute.For<Action<RollEventData>>();
            game.RollHappened += gameFinishedHandler;

            game.Roll(10);

            gameFinishedHandler.Received(1).Invoke(Arg.Any<RollEventData>());
        }


        [Test]
        public void RaisesRollEvent_WithData()
        {
            var playerName = "anyString Player";
            var game = Game.NewGameWithPlayer(playerName);
            game.RollHappened += gameFinishedHandler;

            game.Roll(10);

            receivedEventDataRollEventEvent.Pins.Should().Be(10);
            
            receivedEventDataRollEventEvent.PlayerName.Should().Be(playerName);
        }

        private void gameFinishedHandler(RollEventData receivedEventData)
        {
            this.receivedEventDataRollEventEvent = receivedEventData;
        }
    }
}