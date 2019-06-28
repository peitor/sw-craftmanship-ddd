using BowlingKata.HallOfFame;

namespace BowlingKata.Test
{
    public class HallOfFameHook
    {
        public HallOfFame.HallOfFame hallOfFame = new HallOfFame.HallOfFame();
        
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            new HallOfFameRepository().StoreGame(gameFinishedData.TotalScore);
        }
    }
}
