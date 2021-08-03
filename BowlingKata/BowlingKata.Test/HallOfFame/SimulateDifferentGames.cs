using System;
using BowlingKata.PlayingAGame;
using BowlingKata.ScoreBoardWhilePlaying;
using Commons;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata.Test.HallOfFame
{
    public class SimulateDifferentGames
    {
        [Test]
        public void OnePerfectGameFinished_SeeScoreAndBowlersName()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            var hallOfFame = new BowlingKata.HallOfFame.HallOfFame();
            var scoreBoard = new Scoreboard();
            var game = Game.NewGameWithPlayer("Peter Any-Bowler-Name");
            // Events between context
            game.RollHappened += scoreBoard.RollHappened;
            scoreBoard.GameFinished +=  hallOfFame.GameFinishedHappened;

            
            12.Times(() => game.Roll(10));

            hallOfFame[0].PlayerName.Should().Be("Peter Any-Bowler-Name");
        }


        [Test]
        public void ThreePerfectGamesFinished_SeeThem()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var hallOfFame = new BowlingKata.HallOfFame.HallOfFame();
            var scoreboard = new Scoreboard();
            scoreboard.GameFinished +=  hallOfFame.GameFinishedHappened;

            
            var game = Game.NewGameWithPlayer("1");
            game.RollHappened += scoreboard.RollHappened;
            12.Times(() => game.Roll(10));
            
            var game1 = Game.NewGameWithPlayer("2");
            game1.RollHappened += scoreboard.RollHappened;
            12.Times(() => game1.Roll(10));
            
            var game2 = Game.NewGameWithPlayer("3");
            game2.RollHappened += scoreboard.RollHappened;
            12.Times(() => game2.Roll(10));

            hallOfFame.Length.Should().Be(3);
            hallOfFame[0].Score.Should().Be(300);
            hallOfFame[1].Score.Should().Be(300);
            hallOfFame[2].Score.Should().Be(300);
        }


        [Test]
        public void FullHallOfFame_NewLowScoreGameFinishes_NoImpact()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var hallOfFame = new BowlingKata.HallOfFame.HallOfFame();
            var scoreboard = new Scoreboard();
            scoreboard.GameFinished += hallOfFame.GameFinishedHappened;

            var game = Game.NewGameWithPlayer("1");
            game.RollHappened += scoreboard.RollHappened;
            12.Times(() => game.Roll(10));
            var game1 = Game.NewGameWithPlayer("2");
            game1.RollHappened += scoreboard.RollHappened;
            12.Times(() => game1.Roll(10));
            
            var game2 = Game.NewGameWithPlayer("3");
            game2.RollHappened += scoreboard.RollHappened;
            12.Times(() => game2.Roll(10));

            hallOfFame.Length.Should().Be(3);
            Action<GameFinishedData> gameFinished = hallOfFame.GameFinishedHappened;
            var game3 = Game.NewGameWithPlayer("bad player :)");
            var scoreBoard = new Scoreboard();
            scoreBoard.GameFinished += gameFinished;
            20.Times(() => game3.Roll(1));

            hallOfFame.Length.Should().Be(3);
        }

        [Test]
        public void ThreePerfectGamesFinishedOneBad_ContainsOnlyTop3()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var hallOfFame = new BowlingKata.HallOfFame.HallOfFame();
            var scoreBoard = new Scoreboard();
            scoreBoard.GameFinished += hallOfFame.GameFinishedHappened;

            
            var game4 = Game.NewGameWithPlayer("bad 1 :)");
            game4.RollHappened += scoreBoard.RollHappened;
            20.Times(() => game4.Roll(1));
            
            var game5 = Game.NewGameWithPlayer("bad 2 :)");
            game5.RollHappened += scoreBoard.RollHappened;
            20.Times(() => game5.Roll(1));
            
            var game6 = Game.NewGameWithPlayer("bad 3 :)");
            game6.RollHappened += scoreBoard.RollHappened;
            20.Times(() => game6.Roll(1));
            
            var game7 = Game.NewGameWithPlayer("bad 4 :)");
            game7.RollHappened += scoreBoard.RollHappened;
            20.Times(() => game7.Roll(1));

            
            // These top 3 should show up in the hall of fame
            var game = Game.NewGameWithPlayer("1");
            game.RollHappened += scoreBoard.RollHappened;
            12.Times(() => game.Roll(10));
            
            var game1 = Game.NewGameWithPlayer("2");
            game1.RollHappened += scoreBoard.RollHappened;
            12.Times(() => game1.Roll(10));
            
            var game2 = Game.NewGameWithPlayer("3");
            game2.RollHappened += scoreBoard.RollHappened;
            12.Times(() => game2.Roll(10));
            var game3 = Game.NewGameWithPlayer("4");
            game3.RollHappened += scoreBoard.RollHappened;
            12.Times(() => game3.Roll(10));

            hallOfFame[0].Score.Should().Be(300);
            hallOfFame[1].Score.Should().Be(300);
            hallOfFame[2].Score.Should().Be(300);
            hallOfFame[0].PlayerName.Should().Be("1");
            hallOfFame[1].PlayerName.Should().Be("2");
            hallOfFame[2].PlayerName.Should().Be("3");
            hallOfFame.Length.Should().Be(3);
        }

        [Test]
        public void TwoPlayers_OneHallOfFame()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var hallOfFameBoundedContext = new BowlingKata.HallOfFame.HallOfFame();
            var scoreBoard = new Scoreboard();
            var game = Game.NewGameWithPlayer("Peter Any-Bowler-Name");
            var game2 = Game.NewGameWithPlayer("Sepp");
            game.RollHappened += scoreBoard.RollHappened;
            game2.RollHappened += scoreBoard.RollHappened;
            
            scoreBoard.GameFinished += hallOfFameBoundedContext.GameFinishedHappened;
           

            // Play
            12.Times(() => game.Roll(10));
            21.Times(() => game2.Roll(5));

            
            hallOfFameBoundedContext[0].PlayerName.Should().Be("Peter Any-Bowler-Name");
            hallOfFameBoundedContext[1].PlayerName.Should().Be("Sepp");
        }
    }
}