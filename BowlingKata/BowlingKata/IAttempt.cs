using System.Collections.Generic;

namespace BowlingKata
{
    public interface IAttempt
    {
        IEnumerable<int> Rolls { get; }

        int GetBaseScore();

        int NumberOfRolls { get; }

        bool IsComplete { get; }
    }
}