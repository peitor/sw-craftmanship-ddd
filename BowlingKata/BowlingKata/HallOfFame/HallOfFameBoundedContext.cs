using BowlingKata.PlayingAGame;

namespace BowlingKata.HallOfFame
{
    public class HallOfFameBoundedContext
    {
        public readonly HallOfFame HallOfFame = new HallOfFame();
        
        // TODO: Every new Game() needs to hook up the game.GameFinished to this method.
        //   Use IOC for that.
        public void GameFinishedHappened(GameFinishedData gameFinishedData)
        {
            new HallOfFameRepository().StoreGame(
                gameFinishedData.TotalScore,
                gameFinishedData.PlayerName);
        }
    }
}
