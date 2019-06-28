using System.Linq;
using Commons;

namespace BowlingKata.HallOfFame
{
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
            return Database.GetAll<BowlingGame>(tablename: "HallOfFameGames").OrderByDescending(c => c.Score);
        }
    }
}