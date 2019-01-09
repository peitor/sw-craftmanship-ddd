using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class ScoreService
    {
        private readonly List<Frame> frames;
        private readonly List<int> rolls;

        public ScoreService(List<Frame> frames, List<int> rolls)
        {
            this.frames = frames;
            this.rolls = rolls;
        }

        public int ComputeScore()
        {
            if (frames.Count == 0)
            {
                return 0;
            }

            var result = 0;
            var rollIndex = 0;
            foreach (var frame in frames)
            {
                result += frame.ComputeScore(rolls, rollIndex);
                rollIndex += frame.NumberOfBalls;
            }

            for (var i = rollIndex; i < rolls.Count; i++)
            {
                result += rolls[i];
            }

            return result;
        }
    }
}