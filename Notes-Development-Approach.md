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
  * The world 
