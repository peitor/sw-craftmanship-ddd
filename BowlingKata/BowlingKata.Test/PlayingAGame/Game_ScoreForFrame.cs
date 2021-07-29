using BowlingKata.PlayingAGame;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata.Test.ScoreBoardWhilePlaying
{
    public class Game_ScoreForFrame
    {
        [Test]
        public void ScoreForFrame_OneRoll()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(1);

            game.ScoreForFrame(1).Should().Be(1);
        }

        [Test]
        public void ScoreForFrame_TwoRolls()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(1);
            game.Roll(4);

            game.ScoreForFrame(1).Should().Be(5);
        }

        [Test]
        public void ScoreForFrame_SpareInCurrentFrame_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(7);
            game.Roll(3);

            game.ScoreForFrame(1).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_SpareInPreviousFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(7);
            game.Roll(3);

            game.Roll(2);
            game.Roll(6);

            game.ScoreForFrame(1).Should().Be(12);
            game.ScoreForFrame(2).Should().Be(20);
        }

        [Test]
        public void ScoreForFrame_Strike_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);

            game.ScoreForFrame(1).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_FirstRoll_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);
            game.Roll(2);

            game.ScoreForFrame(1).Should().Be(-1);
            game.ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_SecondRoll_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);

            game.Roll(2);
            game.Roll(6);

            game.ScoreForFrame(1).Should().Be(18);
            game.ScoreForFrame(2).Should().Be(26);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);
            game.Roll(10);

            game.ScoreForFrame(1).Should().Be(-1);
            game.ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_FirstRollInThirdFrame_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);

            game.ScoreForFrame(1).Should().Be(22);
            game.ScoreForFrame(2).Should().Be(-1);
            game.ScoreForFrame(3).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_SecondRollInThirdFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);
            game.Roll(7);

            game.ScoreForFrame(1).Should().Be(22);
            game.ScoreForFrame(2).Should().Be(41);
            game.ScoreForFrame(3).Should().Be(50);
        }

        [Test]
        public void ScoreForFrame_SpareAfterAStrike_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            game.ScoreForFrame(1).Should().Be(20);
            game.ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeSpareRoll_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            game.ScoreForFrame(1).Should().Be(20);
            game.ScoreForFrame(2).Should().Be(31);
            game.ScoreForFrame(3).Should().Be(32);
        }

        [Test]
        public void ScoreForFrame_Spare_FirstRollInSecondFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            game.ScoreForFrame(1).Should().Be(11);
            game.ScoreForFrame(2).Should().Be(12);
        }
    }
}