using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Spare : IAttempt
    {
        public IEnumerable<int> Rolls { get; }

        public Spare(int first)
        {
            if (first < 0 || first >= 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            Rolls = new List<int>(2)
            {
                first,
                10 - first
            };
        }

        public int GetBaseScore()
        {
            return 10;
        }

        public int NumberOfRolls => Rolls.Count();

        public bool IsComplete => true;
    }
}