using System.Collections.Generic;
using System.Linq;

namespace BowlingKata.Test
{
    public class HallOfFameHook
    {
        public HallOfFame hallOfFame = new HallOfFame();
        
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            HallOfFameRepository.StoreGame(gameFinishedData.TotalScore);
        }
    }

    public class HallOfFame
    {
        public int Length => HallOfFameRepository.GetAllGames().Count();
    }

    public static class HallOfFameRepository
    {
        static readonly List<BowlingGame> games = new List<BowlingGame>();
        
        public static void StoreGame(int score)
        {
            games.Add(new BowlingGame(score));
        }

        public static BowlingGame[] GetAllGames()
        {
            return games.ToArray();
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
}
