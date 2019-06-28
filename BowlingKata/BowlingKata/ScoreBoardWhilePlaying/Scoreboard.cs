using System.Collections.Generic;
using BowlingKata.PlayingAGame;

namespace BowlingKata.ScoreBoardWhilePlaying
{
    public class Scoreboard
    {
        Dictionary<string, Game> playersWithGames = new Dictionary<string, Game>();

        public Game game
        {
            get
            {
                return this["default"];

            }
        }

        public Game this[string name]
        {
            get
            {
                if (!playersWithGames.ContainsKey(name))
                {
                    playersWithGames[name] = new Game();
                }

                return playersWithGames[name];
            }
        }
    }

    // TODO: Scoreboard should not use the Game, but have its own. Separate Bounded Contexts!
    //public class Game
    //{
    //    public string PlayerName { get; set; } = "(default Playername)";

    //    public int CurrentScore { get; set; }

    //}
}