using BowlingKata.HallOfFame;
using BowlingKata.PlayingAGame;

namespace BowlingKata.Test
{
    public class HallOfFameHook
    {
        public readonly BowlingKata.HallOfFame.HallOfFame HallOfFame = new BowlingKata.HallOfFame.HallOfFame();
        
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            new HallOfFameRepository().StoreGame(gameFinishedData.TotalScore);
        }
    }
}
