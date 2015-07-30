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
    }
}