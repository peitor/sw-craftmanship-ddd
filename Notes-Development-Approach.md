# My approach in thinking and developing a new feature called "Hall Of Fame" as a separate bounded context.

## New Feature
Introduced new feature: How do we know when a game is finished?
* bool property?
* raise event? callback gets called?

## Decision
Use a property. #KISS

## Question
Write an adapter "GameListener" for the Game? Add a property on the Game?
## Decision
Use property on the Game. #KISS

## Question
Write additional test for the feature or add assert to existing Game Tests?0

## Decision
Write new test to get a feeling for the property.
  
  
_After 2 new tests I found it too cumbersome..._
  
   
## Decision
Add additional Assert to existing tests.

```
        [Test]
        public void InitialScoreShouldBeZero()
        {
            Assert.AreEqual(0, game.Score());
            Assert.That(game.IsFinished == false);
        }
```

## Finding		
Bug here? Test "InitialScoreShouldBeZero?" in the Game.IsFinished of ScoreForFrame(10)
Not really... It's the API and my interpretation.

## Revert 
Revert decision: Add additional Assert to existing tests.  

  
Is there a bug in this return statement?  

```
  return rollIndexNeededForCalculableResult >= currentRoll && currentRoll != 0 ? -1 : score;
```


## Starting now with Hall Of Fame.

  
-> Shared Kernel Approach.

   1. Detect via event when game has finished. Store that in DB.  
   2. Hall Of Fame reads from that DB.  

## Decision
First all in memory. #KISS

## Decision
Next step: refactor to a "database" --> global state between bounded context (still being shared kernel).

  
# What is ugly at the moment?


  * The raising of the event is in the Game.  
  * The raise event is probably buggy and broken.
  * The world hooks up events... Is that responsibility of the world?

    
# Continuation on 2019-05-13
  
## Fix raising event code in Game -> Separation of Concerns (SoC).

  And there was a bug in there.
  
## Don't use a field in tests!   

Example "gameFinishedEventCalledNumberOfTimes" --> Parallel runs? Avoid Threading issues!

        [Test]
        public void NoRolls_NotFinished()
        {
            ListenToEvents();
            Assert.That(game.IsFinished == false);
            Assert.That(gameFinishedEventCalledNumberOfTimes == 0);
        }

        private void ListenToEvents()
        {
            // call how often the event was called
            gameFinishedEventCalledNumberOfTimes = 0;
            game.GameFinished += GameFinishedCounter;
        }
		
        int gameFinishedEventCalledNumberOfTimes;
        private void GameFinishedCounter(GameFinishedData obj)
        {
            gameFinishedEventCalledNumberOfTimes++;
        }

## Decision
Every test gets its own instance of "new Game()".





## Decision 
Replace FakeItEasy with Nsubstitute. Nicer Asserts on Action methods.

# TODO LIST 
  * 3 top games finished, 1 new top game finishes -> assert on result
  * Simulate games and verify if we really see the final topscore --> Assumption event gets raised too early.
  
  * DISCUSSION: 
  *    What does "game finishes" mean?
  *    write 1 integration test, is that enough?
  
  
What is ugly at the moment? 

  * The raising of the event is still in the Game.  
  * The static Database behaves like a database. Parallel test runs fail. Sounds familiar? :)

  
---  

  

# Self Reflection 2019-06-28 (2 months later since last work)

It took me a while to understand how the code works :/
I started to read by refactoring :)



## Issue found: Code Smell
Found 1 issue in Game and annotated with TODO. 
  
       // TODO: FIXME: ASAP!  This call is important. 
       IsFinished = Frame10HasValidScore();

## Fixed global state issue, that caused to fail parallel test runs.

Issue: Shared state causes tests to sometime work and sometimes not!

Parallel change to introduce a "Database" and a "Config.ConnectionString". 

The "Config.Connectionstring" allows tests (and users) to specify which database to use and have their own database. 




# TODO

  * Scoreboard should not use the Game, but have its own. Separate Bounded Contexts!
    Start with ScoreBoard_VerifyWhilePlaying.  
  * The raising of the event is still in the Game.  
  * TODOs in code.
  * Scoreboard simulation in tests.
     Player "Peter" rolls 10, 4, 7, 1 --> Sees state of Game
  * Hall Of Fame 
     should verify Playername, Score, ...
	 
  
  