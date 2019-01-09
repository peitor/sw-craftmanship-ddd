
namespace BowlingKata
{
    public interface IAttempt
    {
        int TotalPins { get; }

        int BonusBalls { get; }

        int NumberOfBalls { get; }

        bool IsComplete { get; }
    }
}