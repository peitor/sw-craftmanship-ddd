namespace BowlingKata
{
    public class Game
    {
        private readonly int[] rolls = new int[21];
        private int currentRoll;

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;
        }

        public int Score()
        {
            int score = 0;
            int frameIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(frameIndex, rolls))
                {
                    score += 10 + StrikeBonus(frameIndex, rolls);
                    frameIndex++;
                }
                else if (IsSpare(frameIndex, rolls))
                {
                    score += 10 + SpareBonus(frameIndex, rolls);
                    frameIndex += 2;
                }
                else
                {
                    score += SumOfBallsInFrame(frameIndex, rolls);
                    frameIndex += 2;
                }
            }
            return score;
        }

        private bool IsStrike(int frameIndex, int[] rolls)
        {
            return rolls[frameIndex] == 10;
        }

        private bool IsSpare(int frameIndex, int[] rolls)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1] == 10;
        }

        private int StrikeBonus(int frameIndex, int[] rolls)
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }

        private int SpareBonus(int frameIndex, int[] rolls)
        {
            return rolls[frameIndex + 2];
        }

        private int SumOfBallsInFrame(int frameIndex, int[] rolls)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }
    }
}