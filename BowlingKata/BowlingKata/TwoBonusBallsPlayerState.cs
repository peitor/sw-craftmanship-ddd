using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class TwoBonusBallsPlayerState : PlayerState
    {
        private int? firstBonusRoll;

        public TwoBonusBallsPlayerState(List<Frame> frames) : base(10, frames)
        {
            firstBonusRoll = null;
        }

        public override PlayerState Roll(int pins)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (firstBonusRoll != null)
            {
                return new GameOverPlayerState(ExtendFrames(FrameFactory.NewStrikeWithBonus(firstBonusRoll.Value, pins)));
            }

            firstBonusRoll = pins;
            return this;
        }
    }
}