using System;

namespace BowlingKata
{
    public class StrikeWithBonusFrame : Frame
    {
        private readonly int first;
        private readonly int second;

        public StrikeWithBonusFrame(int first, int second) : base(10, new Strike())
        {
            if (first < 0 || first > 10 || second < 0 || second > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.first = first;
            this.second = second;
        }

        public override int NumberOfBalls => base.NumberOfBalls + 2;
    }
}