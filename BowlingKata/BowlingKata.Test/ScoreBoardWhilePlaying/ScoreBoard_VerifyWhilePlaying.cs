using BowlingKata.ScoreBoardWhilePlaying;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata.Test.ScoreBoardWhilePlaying
{
    public class ScoreBoard_VerifyWhilePlaying
    {
        [Test]
        public void JustStarted_ScoreIsZero()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Peter"].CurrentScore().Should().Be(0);
        }


        [Test]
        public void Scenario_Simulate2Players_AssertInTheMiddle()
        {
            Scoreboard scoreboard = new Scoreboard();

            scoreboard["Peter"].CurrentScore().Should().Be(0);
            scoreboard["Sepp"].CurrentScore().Should().Be(0);

            scoreboard["Peter"].Roll(1);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);

            scoreboard["Peter"].Roll(10);
            scoreboard["Sepp"].Roll(1);


            scoreboard["Peter"].CurrentScore().Should().Be(41);
            scoreboard["Sepp"].CurrentScore().Should().Be(4);
        }
        
    }
}