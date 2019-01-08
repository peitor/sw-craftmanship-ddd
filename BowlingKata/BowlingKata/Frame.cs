namespace BowlingKata
{
    public class Frame
    {
        public int FrameNumber { get; }

        public IAttempt Attempt { get; }

        public Frame(int frameNumber, IAttempt attempt)
        {
            FrameNumber = frameNumber;
            Attempt = attempt;
        }
    }
}