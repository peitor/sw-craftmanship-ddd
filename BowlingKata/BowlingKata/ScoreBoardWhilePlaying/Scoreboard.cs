using System;
using System.Collections.Generic;
using BowlingKata.PlayingAGame;

namespace BowlingKata.ScoreBoardWhilePlaying
{
    /// <summary>
    /// Bowlers see score while bowling.
    /// This is not the overall bowling hall running scoreboard.
    /// </summary>
    public class Scoreboard
    {
        private readonly Dictionary<string, ScoreWhilePlayingGame> playersWithGames = new Dictionary<string, ScoreWhilePlayingGame>();

        public ScoreWhilePlayingGame this[string name]
        {
            get
            {
                if (!playersWithGames.ContainsKey(name))
                {
                    playersWithGames[name] = new ScoreWhilePlayingGame(); // Game.NewGameWithAnonymousPlayer();
                }

                return playersWithGames[name];
            }
        }

        public void RollHappened(RollEventData rollEventData)
        {
            this[rollEventData.PlayerName].Roll(rollEventData.Pins);
        }
    }

    public class ScoreWhilePlayingGame
    {
        private readonly int[] rolls = new int[21];
        private int currentRoll;
        private int currentFrameIndex;

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;
            if (IsStrike(currentFrameIndex))
            {
                currentFrameIndex++;
            }
        }

        public int CurrentScore()
        {
            var score = ScoreForFrame(10, out _);

            return score;
        }

        private int ScoreForFrame(int frameNumber, out int rollIndexNeededForCalculableResult)
        {
            if (frameNumber > 10 || frameNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var score = 0;
            var currentRollIndex = 0;
            rollIndexNeededForCalculableResult = 0;

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

            return score;
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