using System;

namespace BowlingKata
{
    public class SpareWithBonusFrame : Frame
    {
        private readonly int pins;

        public SpareWithBonusFrame(Spare spare, int pins) : base(10, spare)
        {
            if (pins < 0 || pins > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.pins = pins;
        }

        public override int NumberOfBalls => base.NumberOfBalls + 1;
    }
}