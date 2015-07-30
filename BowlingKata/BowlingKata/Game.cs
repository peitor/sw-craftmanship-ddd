namespace BowlingKata
{
    using System.Collections.Generic;

    public class Game
    {
        private readonly int[] rolls = new int[21];
        private int currentRoll;

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;
        }

        public int Score()
        {
            int score = 0;
            int frameIndex = 0;
            var frames = new List<Frame>();
            for (int frame = 0; frame < 10; frame++)
            {
                var currentFrame = new Frame(frameIndex, rolls);
                frames.Add(currentFrame);

                if (currentFrame.IsStrike())
                {
                    frameIndex++;
                }
                else if (currentFrame.IsSpare())
                {
                    frameIndex += 2;
                }
                else
                {
                    frameIndex += 2;
                }
            }

            foreach (var currentFrame in frames)
            {
                if (currentFrame.IsStrike())
                {
                    score += 10 + currentFrame.StrikeBonus();
                }
                else if (currentFrame.IsSpare())
                {
                    score += 10 + currentFrame.SpareBonus();
                }
                else
                {
                    score += currentFrame.SumOfBallsInFrame();
                }

            }
            return score;
        }
    }
}