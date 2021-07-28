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
        readonly Dictionary<string, Game> playersWithGames = new Dictionary<string, Game>();

        public Game this[string name]
        {
            get
            {
                if (!playersWithGames.ContainsKey(name))
                {
                    playersWithGames[name] = Game.NewGameWithAnonymousPlayer();
                }

                return playersWithGames[name];
            }
        }
    }

    // TODO: Scoreboard should not use the Game, but have its own. Separate Bounded Contexts!
    public class RunningGame
    {
    //    public string PlayerName { get; set; } = "(default Playername)";

        public int CurrentScore { get; set; }

    }
}