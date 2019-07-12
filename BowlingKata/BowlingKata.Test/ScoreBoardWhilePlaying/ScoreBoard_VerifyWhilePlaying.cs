using BowlingKata.ScoreBoardWhilePlaying;
using NUnit.Framework;

namespace BowlingKata.Test.ScoreBoardWhilePlaying
{
    public class ScoreBoard_VerifyWhilePlaying
    {
        [Test]
        public void JustStarted_ScoreIsZero()
        {
            Scoreboard scoreboard = new Scoreboard();

            Assert.AreEqual(0, scoreboard["Peter"].CurrentScore());
        }


        [Test]
        public void Scenario_Simulate2Players_AssertInTheMiddle()
        {
            Scoreboard scoreboard = new Scoreboard();

            Assert.AreEqual(0, scoreboard["Peter"].CurrentScore());
            Assert.AreEqual(0, scoreboard["Sepp"].CurrentScore());

            scoreboard["Peter"].Roll(1);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);


            Assert.AreEqual(41, scoreboard["Peter"].CurrentScore());
            Assert.AreEqual(4, scoreboard["Sepp"].CurrentScore());
        }
    }
}