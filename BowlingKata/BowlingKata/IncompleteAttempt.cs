using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class IncompleteAttempt : IAttempt
    {
        public IEnumerable<int> Rolls { get; }

        public IncompleteAttempt(int first, int second)
        {
            if (first < 0 || second < 0 || first + second >= 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            Rolls = new List<int>(2)
            {
                first,
                second
            };
        }

        public int GetBaseScore()
        {
            return Rolls.Sum();
        }

        public int NumberOfRolls => Rolls.Count();

        public bool IsComplete => false;
    }
}