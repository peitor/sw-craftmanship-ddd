using System.Linq;

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
            return Database.GetAll<BowlingGame>(tablename: "HallOfFameGames");
        }

        public BowlingGame[] GetTopGames(int take)
        {
            return Database.GetAll<BowlingGame>(tablename: "HallOfFameGames")
                .OrderBy(c => c.Score).Take(take).ToArray();
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
