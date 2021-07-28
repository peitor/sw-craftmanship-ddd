using Commons;

namespace BowlingKata.HallOfFame
{
    public class BowlingGame : DatabaseMetadata
    {
        public string PlayerName { get; }

        public int Score { get; }

        public BowlingGame(int score, string playerName)
        {
            PlayerName = playerName;
            Score = score;
        }
    }
}
