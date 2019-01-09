using System;
using System.Collections;
using System.Collections.Generic;

namespace BowlingKata
{
    public class Game
    {
        private List<int> rolls;
        private PlayerState state;

        public Game()
        {
            rolls = new List<int>(21);
            state = Start();
        }

        public void Roll(int pins)
        {
            state = state.Roll(pins);
            rolls.Add(pins);
        }

        public int Score()
        {
            return new ScoreService(state.Frames, rolls).ComputeScore();
        }

        public PlayerState Start()
        {
            return PlayerStateFactory.NewInitialPlayerState();
        }
    }

    public static class PlayerStateFactory
    {
        public static PlayerState NewInitialPlayerState()
        {
            return new FirstBallPlayerState(1, new List<Frame>());
        }
    }
}