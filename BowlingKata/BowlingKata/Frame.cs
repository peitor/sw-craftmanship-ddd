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

        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }
        }

        public int[] Rolls
        {
            get
            {
                return rolls;
            }
        }

        public bool IsStrike()
        {
            return Rolls[FrameIndex] == 10;
        }

        public int StrikeBonus()
        {
            return Rolls[FrameIndex + 1] + Rolls[FrameIndex + 2];
        }

        public bool IsSpare()
        {
            return Rolls[FrameIndex] + Rolls[FrameIndex + 1] == 10;
        }

        public int SpareBonus()
        {
            return Rolls[FrameIndex + 2];
        }

        public int SumOfBallsInFrame()
        {
            return Rolls[FrameIndex] + Rolls[FrameIndex + 1];
        }
    }
}