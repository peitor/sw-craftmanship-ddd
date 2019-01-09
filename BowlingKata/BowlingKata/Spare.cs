using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Spare : IAttempt
    {
        private readonly int first;
        private readonly int second;

        public Spare(int first, int second)
        {
            if (first + second != 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.first = first;
            this.second = second;
        }

        public int TotalPins => first + second;

        public int BonusBalls => 1;

        public int NumberOfBalls => 2;

        public bool IsComplete => true;
    }
}