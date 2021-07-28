# TODO
## Smells
* Complicated bool
    ```  
    return rollIndexNeededForCalculableResult >= currentRoll && currentRoll != 0 ? -1 : score;
    ```

* The raising of the event is in the Game.
* The raise event is probably buggy and broken.
* The world hooks up events... Is that responsibility of the world?


# TODO LIST
* 3 top games finished, 1 new top game finishes -> assert on result
* Simulate games and verify if we really see the final topscore --> Assumption event gets raised too early.

* DISCUSSION:
*    What does "game finishes" mean?
*    write 1 integration test, is that enough?


What is ugly at the moment?

* The raising of the event is still in the Game.
* The static Database behaves like a database. Parallel test runs fail. Sounds familiar? :)




## Starting now with Hall Of Fame. 
-> Shared Kernel Approach.

   1. Detect via event when game has finished. Store that in DB.  
   2. Hall Of Fame reads from that DB.  

## Decision
First all in memory. #KISS
## Decision
Next step: refactor to a "database" --> global state between bounded context (still being shared kernel).

  

## Issue found: Code Smell
Found 1 issue in Game and annotated with TODO. 
  
       // TODO: FIXME: ASAP!  This call is important. 
       IsFinished = Frame10HasValidScore();

## Fixed global state issue, that caused to fail parallel test runs.

Issue: Shared state causes tests to sometime work and sometimes not!

Parallel change to introduce a "Database" and a "Config.ConnectionString". 

The "Config.Connectionstring" allows tests (and users) to specify which database to use and have their own database. 

## Separated all classes into own files and folders
Now we have 3 folders for the 3 bounded contexts: 
  1. PlayingAGame
  2. ScoreBoardWhilePlaying 
  3. HallOfFame



## Started on running ScoreBoard logic
Major refactoring needed :(


# TODO

The current context map looks like this:
![Current Context Map - 2 Bounded Context](images/2019-07-12-bounded-context-not-separated.png)


  * Scoreboard should not use the Game, but have its own. Separate Bounded Contexts!  
    Start with ScoreBoard_VerifyWhilePlaying.  
  * The raising of the event is still in the Game.  
  * TODOs in code.
  * Scoreboard simulation in tests.  
    Player "Peter" rolls 10, 4, 7, 1 --> Sees state of Game
  * Hall Of Fame 
    should verify Playername, Score, ...
  * The world that hooks up HallOfFame and Game is weird somehow.
  * Bug: ScoreForFrame is the sum over all frames until the one. Not just score for that frame.
  * Invalid rolls should throw: Roll(11) or Roll(5), Roll(5). 
  * The ScoreForFrame is ugly as hell.  
  * The codebase is in bad shape. 
  * Game class has too many responsibilities. Fix that next.

## Experiments
* Use ApprovalTests to nail the current state and refactor code.
