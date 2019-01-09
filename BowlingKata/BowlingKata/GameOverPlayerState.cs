using System;
using System.Collections.Generic;

namespace BowlingKata
{
    public class GameOverPlayerState : PlayerState
    {
        protected internal GameOverPlayerState(List<Frame> frames) : base(-1, frames)
        {
            if (frames.Count != 10)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override PlayerState Roll(int pins)
        {
            throw new InvalidOperationException("Game is already over");
        }

        public override bool HasMoreBalls()
        {
            return false;
        }
    }
}