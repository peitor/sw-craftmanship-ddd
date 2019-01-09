using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class FirstBallPlayerState : PlayerState
    {
        protected internal FirstBallPlayerState(int currentFrameNumber, List<Frame> frames) : base(currentFrameNumber, frames)
        {
        }

        public override PlayerState Roll(int pins)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (pins != 10)
            {
                return new SecondBallPlayerState(CurrentFrameNumber, Frames, pins);
            }

            if (IsLastFrame())
            {
                return new TwoBonusBallsPlayerState(Frames);
            }

            return new FirstBallPlayerState(CurrentFrameNumber + 1, ExtendFrames(FrameFactory.NewStrike(CurrentFrameNumber)));

        }
    }
}