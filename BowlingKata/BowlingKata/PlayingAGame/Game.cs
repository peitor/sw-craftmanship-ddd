using System;

namespace BowlingKata.PlayingAGame
{
    public class Game
    {
        public const string DefaultAnonymousPlayername = "(default anonymous playername)";
        private string PlayerName { get; set; } = "(default Playername)";

        private readonly int[] rolls = new int[21];
        private int currentRoll;
        private int currentFrameIndex;
     
        public Action<RollEventData> RollHappened { get; set; }

        private Game()
        {
        }

        public static Game NewGameWithAnonymousPlayer()
        {
            return NewGameWithPlayer(DefaultAnonymousPlayername);
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
           
        }

        public int ScoreForFrame(int frameNumber)
        {
            var score = ScoreForFrame(frameNumber, out var rollIndexNeededForCalculableResult);

            return rollIndexNeededForCalculableResult >= currentRoll && currentRoll != 0 ? -1 : score;
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

        private void TryRaiseRollHappenedEvent(int pins)
        {
            RollHappened?.Invoke(new RollEventData(this.PlayerName, pins));
        }

       
    
    }

    public class RollEventData
    {
        public int Pins { get; }

        public string PlayerName { get; private set; }

        public RollEventData(string playerName, int pins)
        {
            this.PlayerName = playerName;
            this.Pins = pins;
        }
    }
}