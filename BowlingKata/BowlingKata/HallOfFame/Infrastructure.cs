using System.Linq;
using Commons;

namespace BowlingKata.HallOfFame
{
    public class HallOfFame
    {
        public int Length => new HallOfFameRepository().GetTopGames(3).Count();

        public BowlingGame this[int position] => new HallOfFameRepository().GetAllGames()[position];
    }

    public class HallOfFameRepository
    {
        public void StoreGame(int score)
        {
            Database.Add("HallOfFameGames", new BowlingGame(score));
        }

        public BowlingGame[] GetAllGames()
        {
            return GetAllGamesOrdered()
                .ToArray();
        }

        public BowlingGame[] GetTopGames(int take)
        {
            return GetAllGamesOrdered()
                .Take(take).ToArray();
        }

        private static IOrderedEnumerable<BowlingGame> GetAllGamesOrdered()
        {
            return Enumerable.OrderByDescending<BowlingGame, int>(Database.GetAll<BowlingGame>(tablename: "HallOfFameGames"), c => c.Score);
        }
    }


    public class BowlingGame : DatabaseMetadata
    {
        private readonly int _score;

        public BowlingGame(int score)
        {
            _score = score;
        }

        public int Score
        {
            get
            {
                return _score;
            }
        }
    }
}
