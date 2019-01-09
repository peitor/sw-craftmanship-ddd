namespace BowlingKata
{
    public static class FrameFactory
    {
        public static Frame NewStrike(int number)
        {
            return new Frame(number, new Strike());
        }

        public static Frame NewFrame(int number, int first, int second)
        {
            return first + second == 10 ? new Frame(number, new Spare(first, second)) : new Frame(number, new IncompleteAttempt(first, second));
        }

        public static SpareWithBonusFrame NewSpareWithBonus(int first, int second, int bonus)
        {
            return new SpareWithBonusFrame(new Spare(first, second), bonus);
        }

        public static StrikeWithBonusFrame NewStrikeWithBonus(int first, int second)
        {
            return new StrikeWithBonusFrame(first, second);
        }
    }
}