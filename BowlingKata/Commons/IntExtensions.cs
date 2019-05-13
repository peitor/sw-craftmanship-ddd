using System;

namespace Commons
{
    public static class IntExtensions
    {
        // https://stackoverflow.com/a/177561/35693
        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}
