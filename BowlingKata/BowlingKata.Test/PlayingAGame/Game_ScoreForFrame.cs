using BowlingKata.PlayingAGame;
using BowlingKata.ScoreBoardWhilePlaying;
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
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(1);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(1);
        }

        [Test]
        public void ScoreForFrame_TwoRolls()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(1);
            game.Roll(4);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(5);
        }

        [Test]
        public void ScoreForFrame_SpareInCurrentFrame_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(7);
            game.Roll(3);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_SpareInPreviousFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(7);
            game.Roll(3);

            game.Roll(2);
            game.Roll(6);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(12);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(20);
        }

        [Test]
        public void ScoreForFrame_Strike_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_FirstRoll_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);
            game.Roll(2);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(-1);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeInPreviousFrame_SecondRoll_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);

            game.Roll(2);
            game.Roll(6);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(18);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(26);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);
            game.Roll(10);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(-1);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_FirstRollInThirdFrame_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(22);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(-1);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(3).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_TwoStrikes_SecondRollInThirdFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);
            game.Roll(10);
            game.Roll(2);
            game.Roll(7);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(22);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(41);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(3).Should().Be(50);
        }

        [Test]
        public void ScoreForFrame_SpareAfterAStrike_ScoreIsUnknown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(20);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(-1);
        }

        [Test]
        public void ScoreForFrame_StrikeSpareRoll_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(10);

            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(20);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(31);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(3).Should().Be(32);
        }

        [Test]
        public void ScoreForFrame_Spare_FirstRollInSecondFrame_ScoreIsKnown()
        {
            var game = Game.NewGameWithAnonymousPlayer();
            var scoreboard = new Scoreboard();
            game.RollHappened += scoreboard.RollHappened;
            
            game.Roll(3);
            game.Roll(7);

            game.Roll(1);

            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(1).Should().Be(11);
            scoreboard[Game.DefaultAnonymousPlayername].ScoreForFrame(2).Should().Be(12);
        }
    }
}