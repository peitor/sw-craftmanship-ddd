namespace BowlingKata
{
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
            for (int frame = 0; frame < 10; frame++)
            {
                var currentFrame = new Frame(frameIndex, rolls);
                if (IsStrike(currentFrame))
                {
                    score += 10 + StrikeBonus(currentFrame);
                    frameIndex++;
                }
                else if (IsSpare(currentFrame))
                {
                    score += 10 + SpareBonus(currentFrame);
                    frameIndex += 2;
                }
                else
                {
                    score += SumOfBallsInFrame(currentFrame);
                    frameIndex += 2;
                }
            }
            return score;
        }

        private bool IsStrike(Frame frame)
        {
            return frame.Rolls[frame.FrameIndex] == 10;
        }

        private bool IsSpare(Frame frame)
        {
            return frame.Rolls[frame.FrameIndex] + frame.Rolls[frame.FrameIndex + 1] == 10;
        }

        private int StrikeBonus(Frame frame)
        {
            return frame.Rolls[frame.FrameIndex + 1] + frame.Rolls[frame.FrameIndex + 2];
        }

        private int SpareBonus(Frame frame)
        {
            return frame.Rolls[frame.FrameIndex + 2];
        }

        private int SumOfBallsInFrame(Frame frame)
        {
            return frame.Rolls[frame.FrameIndex] + frame.Rolls[frame.FrameIndex + 1];
        }
    }
}