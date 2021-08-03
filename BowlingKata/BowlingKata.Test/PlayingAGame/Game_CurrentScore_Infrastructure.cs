using System;
using BowlingKata.PlayingAGame;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_CurrentScore_Infrastructure
    {
        private RollData receivedDataRollEvent;

        [Test]
        public void RaisesRollEvent()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var gameFinishedHandler = Substitute.For<Action<RollData>>();
            game.RollHappened += gameFinishedHandler;

            game.Roll(10);

            gameFinishedHandler.Received(1).Invoke(Arg.Any<RollData>());
        }


        [Test]
        public void RaisesRollEvent_WithData()
        {
            var playerName = "anyString Player";
            var game = Game.NewGameWithPlayer(playerName);
            game.RollHappened += gameFinishedHandler;

            game.Roll(10);

            receivedDataRollEvent.Pins.Should().Be(10);
            
            receivedDataRollEvent.PlayerName.Should().Be(playerName);
        }

        private void gameFinishedHandler(RollData receivedData)
        {
            this.receivedDataRollEvent = receivedData;
        }
    }
}