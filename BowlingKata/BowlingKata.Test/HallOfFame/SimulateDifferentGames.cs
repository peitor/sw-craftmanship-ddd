﻿using System;
using BowlingKata.HallOfFame;
using BowlingKata.PlayingAGame;
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
            var world = new HallOfFameBoundedContext();

            HookUpAndSimulatePerfectGame(world.GameFinishedHappened, "Peter Any-Bowler-Name");

            world.HallOfFame[0].PlayerName.Should().Be("Peter Any-Bowler-Name");
        }


        [Test]
        public void ThreePerfectGamesFinished_SeeThem()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var world = new HallOfFameBoundedContext();
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);

            world.HallOfFame[0].Score.Should().Be(300);
            world.HallOfFame[1].Score.Should().Be(300);
            world.HallOfFame[2].Score.Should().Be(300);
            world.HallOfFame.Length.Should().Be(3);
        }


        [Test]
        public void FullHallOfFame_NewLowScoreGameFinishes_NoImpact()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var world = new HallOfFameBoundedContext();
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);

            world.HallOfFame.Length.Should().Be(3);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);

            world.HallOfFame.Length.Should().Be(3);
        }

        [Test]
        public void ThreePerfectGamesFinishedOneBad_ContainsOnlyTop3()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var world = new HallOfFameBoundedContext();
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);

            // These top 3 should show up in the hall of fame
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened, "1st");
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened, "2nd");
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened, "3rd");
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened, "4th");

            world.HallOfFame[0].Score.Should().Be(300);
            world.HallOfFame[1].Score.Should().Be(300);
            world.HallOfFame[2].Score.Should().Be(300);
            world.HallOfFame[0].PlayerName.Should().Be("1st");
            world.HallOfFame[1].PlayerName.Should().Be("2nd");
            world.HallOfFame[2].PlayerName.Should().Be("3rd");
            world.HallOfFame.Length.Should().Be(3);
        }

        [Test]
        public void TwoPlayers_OneHallOfFame()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var hallOfFameBoundedContext = new HallOfFameBoundedContext();
            var game = Game.NewGameWithPlayer("Peter Any-Bowler-Name");
            var game2 = Game.NewGameWithPlayer("Sepp");
            
            game.GameFinished += hallOfFameBoundedContext.GameFinishedHappened;
            game2.GameFinished += hallOfFameBoundedContext.GameFinishedHappened;
           

            // Play
            12.Times(() => game.Roll(10));
            21.Times(() => game2.Roll(5));

            
            hallOfFameBoundedContext.HallOfFame[0].PlayerName.Should().Be("Peter Any-Bowler-Name");
            hallOfFameBoundedContext.HallOfFame[1].PlayerName.Should().Be("Sepp");
        }
        
        private void HookUpAndSimulateBadGame(Action<GameFinishedData> gameFinished)
        {
            var game = Game.NewGameWithPlayer("bad player :)");
            game.GameFinished += gameFinished;
            20.Times(() => game.Roll(1));
        }


        private static void HookUpAndSimulatePerfectGame(Action<GameFinishedData> gameFinished, string playerName = "(none set)")
        {
            var game = Game.NewGameWithPlayer(playerName);
            game.GameFinished += gameFinished;
            12.Times(() => game.Roll(10));
        }
    }
}