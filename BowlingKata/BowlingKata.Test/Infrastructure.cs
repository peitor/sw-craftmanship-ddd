using System.Collections.Generic;
using System.Linq;

namespace BowlingKata.Test
{
    public class World
    {
        public HallOfFame hallOfFame = new HallOfFame();
        
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
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

    public class HallOfFame
    {
        public int Length => Database.GetAllGames().Count();
    }}
