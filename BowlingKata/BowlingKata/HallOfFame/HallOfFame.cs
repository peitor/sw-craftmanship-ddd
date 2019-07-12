using System.Linq;

namespace BowlingKata.HallOfFame
{
    public class HallOfFame
    {
        /// <summary>
        /// How many top games are in the hall of fame?
        /// There is a Maximum.
        /// </summary>
        public int Length => new HallOfFameRepository().GetTopGames(3).Count();

        /// <summary>
        /// Return the details of that game in the hall of fame.
        /// TODO: Better GetHallOfFame returns in order the top games. Avoid the this.
        /// Smell: Tell don't ask.
        /// </summary>
        public BowlingGame this[int position] => new HallOfFameRepository().GetAllGames()[position];
    }
}