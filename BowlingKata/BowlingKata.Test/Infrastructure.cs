﻿using System.Collections.Generic;
using System.Linq;
using Commons;

namespace BowlingKata.Test
{
    public class World
    {
        public GameSimulator gameSimulator = new GameSimulator();
        public HallOfFame hallOfFame = new HallOfFame();
        
        public World()
        {
            // hook up delegate
            gameSimulator.game.GameFinished += GameFinishedHappened;
        }

        private void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            Database.StoreGame(gameFinishedData.TotalScore);
        }
    }

    public static class Database
    {
        static readonly List<BowlingGame> database = new List<BowlingGame>();
        
        public static void StoreGame(int score)
        {
            database.Add(new BowlingGame(score));
        }

        public static BowlingGame[] GetAllGames()
        {
            return database.ToArray();
        }
    }

    public class BowlingGame
    {
        private readonly int _score;

        public BowlingGame(int score)
        {
            _score = score;
        }
    }

    public class GameSimulator
    {
        public readonly Game game = new Game();

        public void FinishGame()
        {

            12.Times(() => game.Roll(10));
        }
    }

    public class HallOfFame
    {
        public int Length => Database.GetAllGames().Count();
    }}