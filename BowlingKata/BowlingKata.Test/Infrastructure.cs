using System.Collections;
using System.Linq;
using Castle.Core.Resource;

namespace BowlingKata.Test
{
    public class HallOfFameHook
    {
        public HallOfFame hallOfFame = new HallOfFame();
        
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            new HallOfFameRepository().StoreGame(gameFinishedData.TotalScore);
        }
    }

    public class HallOfFame
    {
        public int Length => new HallOfFameRepository().GetAllGames().Count();
    }

    public class HallOfFameRepository
    {
        public void StoreGame(int score)
        {
            Database.Add("HallOfFameGames", new BowlingGame(score));
        }

        public BowlingGame[] GetAllGames()
        {
            return Database.GetAll<BowlingGame>(tablename: "HallOfFameGames");
        }
    }


    public class BowlingGame : DatabaseMetadata
    {
        private readonly int _score;

        public BowlingGame(int score)
        {
            _score = score;
        }
    }
}
