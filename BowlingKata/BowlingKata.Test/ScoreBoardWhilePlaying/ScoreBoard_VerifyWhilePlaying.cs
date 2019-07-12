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
        public void OneStrike()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Franz"].Roll(10);

            Assert.AreEqual(10, scoreboard["Franz"].CurrentScore());
        }

        [Test]
        public void OneNormalRoll()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Franz"].Roll(3);

            Assert.AreEqual(3, scoreboard["Franz"].CurrentScore());
        }

        [Test]
        public void TwoNormalRolls()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Franz"].Roll(3);
            scoreboard["Franz"].Roll(4);

            Assert.AreEqual(7, scoreboard["Franz"].CurrentScore());
        }


        [Test]
        public void OneStrike_2Empty()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Franz"].Roll(10);
            scoreboard["Franz"].Roll(0);
            scoreboard["Franz"].Roll(0);

            Assert.AreEqual(10, scoreboard["Franz"].CurrentScore());
        }


        [Test]
        public void TotalGAMEEEE()
        {
            Scoreboard scoreboard = new Scoreboard();

            var g = scoreboard["Franz"];
            g.Roll(10);
            Assert.AreEqual(10, g.CurrentScore());

            g.Roll(0);
            Assert.AreEqual(10, g.CurrentScore());
            g.Roll(0);
            Assert.AreEqual(10, g.CurrentScore());

            g.Roll(2);
            Assert.AreEqual(12, g.CurrentScore());
            g.Roll(3);
            Assert.AreEqual(15, g.CurrentScore());

            g.Roll(10);
            Assert.AreEqual(25, g.CurrentScore());

            g.Roll(5);
            Assert.AreEqual(35, g.CurrentScore());
            g.Roll(5);
            Assert.AreEqual(45, g.CurrentScore());

            g.Roll(2);
            Assert.AreEqual(49, g.CurrentScore());

            g.Roll(0);
            Assert.AreEqual(49, g.CurrentScore());
            g.Roll(0);
            Assert.AreEqual(49, g.CurrentScore());
            g.Roll(0);
            Assert.AreEqual(49, g.CurrentScore());
            g.Roll(0);
            Assert.AreEqual(49, g.CurrentScore());
            g.Roll(0);
            Assert.AreEqual(49, g.CurrentScore());

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