using System;
using BowlingKata.PlayingAGame;
using BowlingKata.ScoreBoardWhilePlaying;
using Commons;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BowlingKata.Test.PlayingAGame
{
    public class Game_IsFinished
    {
        [Test]
        public void TwoRolls_NotFinished()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;

            game.Roll(1);
            game.Roll(1);

            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(false);
        }

        [Test]
        public void EightRolls_NotFinished()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;


            8.Times(() => game.Roll(10));

            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(false);
        }

        [Test]
        public void TwelveStrikes_GameFinished()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            
            12.Times(() => game.Roll(10));

            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(true);
        }

        [Test]
        public void TwelveStrikes_GameFinished_EventsRaised()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            
            var gameFinishedHandler = GivenEventFinishedHandler(scoreBoard[Game.DefaultAnonymousPlayername]);

            12.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void ThirteenStrikes_GameFinished_EventsRaised()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            
            var gameFinishedHandler = GivenEventFinishedHandler(scoreBoard[Game.DefaultAnonymousPlayername]);

            13.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void PairsOfNineAndMissShouldReturn90()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            
            5.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });
            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(false);

            5.Times(() =>
            {
                game.Roll(9);
                game.Roll(0);
            });

            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(true);
        }

        [Test]
        public void NoRolls_NotFinished()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            
            var gameFinishedHandler = GivenEventFinishedHandler(scoreBoard[Game.DefaultAnonymousPlayername]);

            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(false);

            gameFinishedHandler.DidNotReceive().Invoke(Arg.Any<GameFinishedData>());
        }

        [Test]
        public void TwelveStrikes_GameFinished_ReturnPlayerNameInEvent()
        {
            var game = Game.NewGameWithPlayer("Peter");
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            var gameFinishedHandler = GivenEventFinishedHandler(scoreBoard[Game.DefaultAnonymousPlayername]);
            scoreBoard.GameFinished += gameFinishedHandler;

            12.Times(() => game.Roll(10));

            gameFinishedHandler.Received(1).Invoke(Arg.Is<GameFinishedData>(data => data.PlayerName == "Peter"));
        }

        [Test]
        public void ASSUMPTION__ComplicatedGame_NotFinished()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreBoard = new Scoreboard();
            game.RollHappened += scoreBoard.RollHappened;
            
            //TODO: Sophisticated games could break the logic in the "return" statement?? <-- Assumption
            13.Times(() => game.Roll(1));

            game.Roll(1);
            game.Roll(1);
            // TODO: play around here with this....
            scoreBoard[Game.DefaultAnonymousPlayername].IsFinished.Should().Be(false);
        }

        private static Action<GameFinishedData> GivenEventFinishedHandler(ScoreWhilePlayingGame scoreWhilePlayingGame)
        {
            var gameFinishedHandler = Substitute.For<Action<GameFinishedData>>();
            
            scoreWhilePlayingGame.GameFinished += gameFinishedHandler;
            return gameFinishedHandler;
        }
    }
}