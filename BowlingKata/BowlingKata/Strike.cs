using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Strike : IAttempt
    {
        public Strike()
        {
        }

        public int TotalPins => 10;

        public int BonusBalls => 2;

        public int NumberOfBalls => 1;

        public bool IsComplete => true;
    }
}