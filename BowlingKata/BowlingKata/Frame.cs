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

        public int StrikeBonus()
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }

        public bool IsSpare()
        {
            return rolls[frameIndex] + rolls[frameIndex + 1] == 10;
        }

        public int SpareBonus()
        {
            return rolls[frameIndex + 2];
        }

        public int SumOfBallsInFrame()
        {
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }
    }
}