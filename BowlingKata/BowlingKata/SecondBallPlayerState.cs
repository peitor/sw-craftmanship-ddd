using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class SecondBallPlayerState : PlayerState
    {
        public int FirstRoll { get; }

        protected internal SecondBallPlayerState(int currentFrameNumber, List<Frame> frames, int firstRoll) : base(currentFrameNumber, frames)
        {
            if (firstRoll >= 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            FirstRoll = firstRoll;
        }

        public override PlayerState Roll(int pins)
        {
            if (pins < 0 || pins > 10 - FirstRoll)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (!IsLastFrame())
            {
                return new FirstBallPlayerState(CurrentFrameNumber + 1,
                    ExtendFrames(FrameFactory.NewFrame(CurrentFrameNumber, FirstRoll, pins)));
            }

            if (FirstRoll + pins == 10)
            {
                return new OneBonusBallPlayerState(Frames, FirstRoll, pins);
            }

            return new GameOverPlayerState(
                ExtendFrames(FrameFactory.NewFrame(CurrentFrameNumber, FirstRoll, pins)));
        }
    }
}