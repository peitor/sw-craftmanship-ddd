using System;

namespace BowlingKata
{
    public class Game
    {
        private readonly int[] rolls = new int[21];
        private int currentRoll;

        public bool IsFinished { get; private set; } = false;

        private bool gameFinishedWasAlreadyCalled = false;
        public Action<int> GameFinished { get; set; }

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;

            if (MinimumRollsHappened())
            {
                IsFinished = Frame10HasValidScore();

                RaiseGameFinishedEvent(ScoreForFrame(10));
            }
        }

        public int TotalScore()
        {
            return ScoreForFrame(10);
        }

        public int ScoreForFrame(int frameNumber)
        {
            if (frameNumber > 10 || frameNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            var score = 0;
            var currentRollIndex = 0;
            var rollIndexNeededForCalculableResult = 0;

            for (var currentFrame = 1; currentFrame <= frameNumber; currentFrame++)
            {
                if (IsStrike(currentRollIndex))
                {
                    score += 10 + StrikeBonus(currentRollIndex);
                    rollIndexNeededForCalculableResult = currentRollIndex + 2;
                    currentRollIndex++;
                }
                else if (IsSpare(currentRollIndex))
                {
                    score += 10 + SpareBonus(currentRollIndex);
                    rollIndexNeededForCalculableResult = currentRollIndex + 2;
                    currentRollIndex += 2;
                }
                else
                {
                    score += SumOfBallsInFrame(currentRollIndex);
                    rollIndexNeededForCalculableResult = Math.Max(currentRollIndex, rollIndexNeededForCalculableResult);
                    currentRollIndex += 2;
                }
            }

            return rollIndexNeededForCalculableResult >= currentRoll && currentRoll != 0 ? -1 : score;
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

        private bool Frame10HasValidScore()
        {
            return ScoreForFrame(10) > -1;
        }

        private bool MinimumRollsHappened()
        {
            return currentRoll >= 12;
        }

        private void RaiseGameFinishedEvent(int scoreForFrame)
        {
            if (IsFinished
                && gameFinishedWasAlreadyCalled == false
            )
            {
                GameFinished?.Invoke(scoreForFrame);
                gameFinishedWasAlreadyCalled = true;
            }
        }
    }
}