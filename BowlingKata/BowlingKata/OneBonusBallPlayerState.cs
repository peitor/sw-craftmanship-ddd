using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class OneBonusBallPlayerState : PlayerState
    {
        private readonly int first;
        private readonly int second;

        protected internal OneBonusBallPlayerState(List<Frame> frames, int first, int second) : base(10, frames)
        {
            if (first + second != 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.first = first;
            this.second = second;
        }

        public override PlayerState Roll(int pins)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new GameOverPlayerState(ExtendFrames(FrameFactory.NewSpareWithBonus(first, second, pins)));
        }
    }
}