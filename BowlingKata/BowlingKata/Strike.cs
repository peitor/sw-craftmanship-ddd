using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Strike : IAttempt
    {
        public IEnumerable<int> Rolls { get; }

        public Strike()
        {
            Rolls = new List<int>(1) { 10 };
        }

        public int GetBaseScore()
        {
            return 10;
        }

        public int NumberOfRolls => Rolls.Count();

        public bool IsComplete => true;
    }
}