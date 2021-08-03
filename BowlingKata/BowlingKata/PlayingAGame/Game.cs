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

        private bool IsStrike(int frameIndex)
        {
            return rolls[frameIndex] == 10;
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