using System;

namespace BowlingKata.PlayingAGame
{
    public class Game
    {
        private string PlayerName { get; set; } = "(default Playername)";

        private readonly int[] rolls = new int[21];
        private int currentRoll;
        private int currentFrameIndex;

        public bool IsFinished { get; private set; }
        public Action<GameFinishedData> GameFinished { get; set; }
        private bool gameFinishedWasAlreadyCalled;

        public Action<RollData> RollHappened { get; set; }

        private Game()
        {
        }

        public static Game NewGameWithAnonymousPlayer()
        {
            return NewGameWithPlayer("(default anonymous playername)");
        }

        public static Game NewGameWithPlayer(string playerName)
        {
            var newGameWithPlayer = new Game {PlayerName = playerName};
            return newGameWithPlayer;
        }

        public void Roll(int pins)
        {
            rolls[currentRoll++] = pins;

            if (IsStrike(currentFrameIndex))
            {
                currentFrameIndex++;
            }

            TryRaiseRollHappenedEvent(pins);
            TryRaiseGameFinishedEvent();
        }

        public int ScoreForFrame(int frameNumber)
        {
            var score = ScoreForFrame(frameNumber, out var rollIndexNeededForCalculableResult);

            return rollIndexNeededForCalculableResult >= currentRoll && currentRoll != 0 ? -1 : score;
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

        private bool Frame10HasValidScore()
        {
            return ScoreForFrame(10) > -1;
        }

        private void TryRaiseRollHappenedEvent(int pins)
        {
            RollHappened?.Invoke(new RollData(this.PlayerName, pins));
        }

        private void TryRaiseGameFinishedEvent()
        {
            if (MinimumRollsHappened())
            {
                // TODO: FIXME: ASAP!  This call is important. 
                IsFinished = Frame10HasValidScore();

                int scoreForFrame = ScoreForFrame(10);

                if (IsFinished)
                {
                    RaiseGameFinishedEvent(scoreForFrame);
                }
            }
        }

        private bool MinimumRollsHappened()
        {
            return currentRoll >= 12;
        }

        private void RaiseGameFinishedEvent(int scoreForFrame)
        {
            if (gameFinishedWasAlreadyCalled == false)
            {
                GameFinished?.Invoke(new GameFinishedData
                {
                    TotalScore = scoreForFrame,
                    PlayerName = PlayerName,
                });
                gameFinishedWasAlreadyCalled = true;
            }
        }
    }

    public class RollData
    {
        public int Pins { get; }

        public string PlayerName { get; private set; }

        public RollData(string playerName, int pins)
        {
            this.PlayerName = playerName;
            this.Pins = pins;
        }
    }
}