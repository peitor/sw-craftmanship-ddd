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
            return ScoreForFrame(10);
        }

        public int ScoreForFrame(int frameNumber)
        {
            int score = 0;
            int frameIndex = 0;
            int frameIndexNeeded = 0;
            for (int frame = 0; frame < frameNumber; frame++)
            {
                if (IsStrike(frameIndex))
                {
                    frameIndexNeeded = frameIndex + 2; // need to know the next two rolls for the strike bonus
                    score += 10 + StrikeBonus(frameIndex);
                    frameIndex++;
                }
                else if (IsSpare(frameIndex))
                {
                    frameIndexNeeded = frameIndex + 2; // +2 because frameIndex currently points to the _first_ roll in the spare and we need the roll after the spare for the bonus
                    score += 10 + SpareBonus(frameIndex);
                    frameIndex += 2;
                }
                else
                {
                    frameIndexNeeded = frameIndex;
                    score += SumOfBallsInFrame(frameIndex);
                    frameIndex += 2;
                }
            }
            bool scoreIsUnknown = currentRoll > 0 && frameIndexNeeded >= currentRoll;
            return scoreIsUnknown ? -1: score;
        }

        private bool IsStrike(int frameIndex)
        {
            return rolls[frameIndex] == 10;
        }

        private bool IsSpare(int frameIndex)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1] == 10;
        }

        private int StrikeBonus(int frameIndex)
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }

        private int SpareBonus(int frameIndex)
        {
            return rolls[frameIndex + 2];
        }

        private int SumOfBallsInFrame(int frameIndex)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }
    }
}