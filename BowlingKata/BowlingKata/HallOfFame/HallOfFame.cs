using System.Linq;

namespace BowlingKata.HallOfFame
{
    public class HallOfFame
    {
        public int Length => new HallOfFameRepository().GetTopGames(3).Count();

        public BowlingGame this[int position] => new HallOfFameRepository().GetAllGames()[position];
    }
}