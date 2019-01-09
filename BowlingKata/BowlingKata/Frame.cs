using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class Frame
    {
        protected int FrameNumber { get; }

        protected IAttempt Attempt { get; }

        public Frame(int frameNumber, IAttempt attempt)
        {
            if (frameNumber < 1 || frameNumber > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            FrameNumber = frameNumber;
            Attempt = attempt;
        }

        public bool HasBonus => Attempt.IsComplete;

        public virtual int NumberOfBalls => Attempt.NumberOfBalls;

        public int BaseScore => Attempt.TotalPins;

        public int ComputeScore(List<int> rolls, int rollIndex)
        {
            return BaseScore + ComputeBonusScore(rolls, rollIndex);
        }

        public int ComputeBonusScore(List<int> rolls, int rollIndex)
        {
            var result = 0;
            var startRollIndex = rollIndex + Attempt.NumberOfBalls;
            for (var i = startRollIndex; i < startRollIndex + Attempt.BonusBalls && i < rolls.Count; i++)
            {
                result += rolls[i];
            }

            return result;
        }
    }
}