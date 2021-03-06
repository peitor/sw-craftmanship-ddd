# A DDD Approach to Uncle Bobs Bowling Kata
We used the BowlingKata from Uncle Bob which is OO only and extended the code with 2 new features in a Domain Driven Design approach.
   
Here you see the approach from Uncle Bob  
http://butunclebob.com/ArticleS.UncleBob.TheBowlingGameKata   
  
  
Someone asked:   
  _"Why is the design so different from the implementation?"_
  
Uncle Bob said:  
  _"Excellent question! It's because we could not see how simple the solution really was when we drew that design!"_ 
 
Uncle Bob had 1 bounded context if you will:

## Bowler plays a game and rolls the ball.
![Bowler rolls ball](images/01-play-game-2019-05-29%2009_35_43-DDD%20workshop.png)


# We added 2 new features 
This extension adds to Uncle Bobs approach a 
## Running Scoreboard  
![Scoreboard](images/02-scoreboard-2019-05-29%2009_36_00-DDD%20workshop.png)

## Hall Of Fame
![Hall Of Fame](images/03-hall-of-fame-2019-05-29%2009_36_20-DDD%20workshop.png)

## Context Map
The final context map looks like this:
![Overall Context Map](images/04-context-map-2019-05-29%2009_35_21-DDD%20workshop.png)


# Progress
My thinking and progress can be monitored via the commit messages and here [Notes-Development-Approach.md](Notes-Development-Approach.md).

## Learn the rules
Here is a simulator to learn the rules http://www.bowlinggenius.com/ .
