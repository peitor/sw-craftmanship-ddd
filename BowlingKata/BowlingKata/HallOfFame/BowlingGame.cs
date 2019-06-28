using Commons;

namespace BowlingKata.HallOfFame
{
    public class BowlingGame : DatabaseMetadata
    {
        public BowlingGame(int score)
        {
            Score = score;
        }

        public int Score { get; }
    }
}
