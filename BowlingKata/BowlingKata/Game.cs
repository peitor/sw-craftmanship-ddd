using System.Collections.Generic;

namespace BowlingKata
{
    public class Game
    {
        public List<Frame> Frames { get; }

        public Game()
        {
            Frames = new List<Frame>();
        }
    }
}