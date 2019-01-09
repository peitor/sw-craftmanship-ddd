using System.Collections.Generic;

namespace BowlingKata
{
    public abstract class PlayerState
    {
        protected int CurrentFrameNumber { get; }
        public List<Frame> Frames { get; }

        protected PlayerState(int currentFrameNumber, List<Frame> frames)
        {
            CurrentFrameNumber = currentFrameNumber;
            Frames = frames;
        }

        public abstract PlayerState Roll(int pins);

        public bool IsLastFrame()
        {
            return CurrentFrameNumber == 10;
        }

        protected List<Frame> ExtendFrames(Frame frame)
        {
            var result = new List<Frame>(Frames.Count + 1);
            result.AddRange(Frames);
            result.Add(frame);
            return result;
        }

        public virtual bool HasMoreBalls()
        {
            return true;
        }
    }
}