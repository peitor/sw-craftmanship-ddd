﻿using System;
using BowlingKata.HallOfFame;
using BowlingKata.PlayingAGame;
using Commons;
using NUnit.Framework;

namespace BowlingKata.Test.HallOfFame
{
    public class SimulateDifferentGames
    {
        [Test]
        public void OneGameFinished_SeeIt()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var world = new HallOfFameBoundedContext();
            
            var hallOfFameLength = world.HallOfFame.Length;
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);

            Assert.True((bool) (world.HallOfFame.Length > hallOfFameLength));
        }

        [Test]
        public void GivenFullHallOfFame_NewLowScoreGameFinishes_NoImpact()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var world = new HallOfFameBoundedContext();
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);

            Assert.AreEqual(3, world.HallOfFame.Length);
        }
        
        [Test]
        public void HallOfFame_ContainsOnlyTop3()
        {
            Config.ConnectionString = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var world = new HallOfFameBoundedContext();
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);
            HookUpAndSimulateBadGame(world.GameFinishedHappened);

            // These top 3 should show up in the hall of fame
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);
            HookUpAndSimulatePerfectGame(world.GameFinishedHappened);

            Assert.AreEqual(300, world.HallOfFame[0].Score);
            Assert.AreEqual(300, world.HallOfFame[1].Score);
            Assert.AreEqual(300, world.HallOfFame[2].Score);
        }

        private void HookUpAndSimulateBadGame(Action<GameFinishedData> gameFinished)
        {
            var game = new Game();
            game.GameFinished += gameFinished;
            20.Times(() => game.Roll(1));
        }


        private static void HookUpAndSimulatePerfectGame(Action<GameFinishedData> gameFinished)
        {
            var game = new Game();
            game.GameFinished += gameFinished;
            12.Times(() => game.Roll(10));
        }
    }
}