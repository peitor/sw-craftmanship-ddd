namespace BowlingKata
{
    public class Frame
    {
        private readonly int frameIndex;
        private readonly int[] rolls;

        public Frame(int frameIndex, int[] rolls)
        {
            this.frameIndex = frameIndex;
            this.rolls = rolls;
        }

        public bool IsStrike()
        {
            return rolls[frameIndex] == 10;
        }

        private int StrikeBonus()
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }

        public bool IsSpare()
        {
            return rolls[frameIndex] + rolls[frameIndex + 1] == 10;
        }

        private int SpareBonus()
        {
            return rolls[frameIndex + 2];
        }

        private int SumOfBallsInFrame()
        {
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }

        public int GetScore()
        {
            int frameScore;
            if (IsStrike())
            {
                frameScore = 10 + StrikeBonus();
            }
            else if (IsSpare())
            {
                frameScore = 10 + SpareBonus();
            }
            else
            {
                frameScore = SumOfBallsInFrame();
            }
            return frameScore;
        }
    }
}