using System.Collections.Generic;

namespace BowlingKata
{
    public static class PlayerStateFactory
    {
        public static PlayerState NewInitialPlayerState()
        {
            return new FirstBallPlayerState(1, new List<Frame>());
        }
    }
}