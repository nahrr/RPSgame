# Rock paper scissor (RPS)

## Description

Backend with HTTP-API for RPS game

## Usage
### Available APIs

**GET api/games/gameid**
Get game status response, will respond
```postman
	{
    	"successful": true,
    	"status": 1
	}
```
  **Explantion of status codes**
```js
  Game created = 1	
  Player one move pending = 2
  Player two move pending = 3
  Tie = 4
  Player one won = 5
  Player two won = 6
```

**POST /api/games/gameName**
  
  Create a new game, game name needs to be unique

  https://localhost:44302/api/games/LetsPlay

**POST api/games/gameName/playerName/gameId**
  
  Join the game, only two players allowed and player name needs to be unique, first player to join the game will be player one.
  
   https://localhost:44302/LetsPlay/Johan/6c7dc306-3e99-4ad7-a8f8-01bbf6441f09

**POST api/games/gameName/playerName/playerMove**

  Play game, to make your play hand choose between:

```
  Paper = 1
	Scissors = 2
	Rock = 3
```
  https://localhost:44302/LetsPlay/Johan/1

## Example game

Use Postman, free download https://www.postman.com/

###STEP 1:### 
Let's create a game, I am choosing ExampleGame as the game name.

Use post method:
https://localhost:44302/api/games/ExampleGame 

body response in Postman
```postman
{
    "gameId": "6b142927-361c-4b3c-8c0b-ee9529673d96"
}
```

Copy the game Id and send to your friend

###STEP 2:### 

Time for both players to join the game

Join the game with post method, use game name, your player name and the game id

Examples: 

https://localhost:44302/api/games/ExampleGame/Mike/6b142927-361c-4b3c-8c0b-ee9529673d96 

https://localhost:44302/api/games/ExampleGame/Jen/6b142927-361c-4b3c-8c0b-ee9529673d96 

body response in Postman
```postman
{
  "joinedGame": true
}
###STEP 3:###

Time for both players (Mike and Jen) to make their move.
Paper = 1, Scissors = 2, Rock = 3

Use Post method

Examples:

Mike chose paper and Jen chose Scissors
https://localhost:44302/LetsPlay/Mike/1
https://localhost:44302/LetsPlay/Jen/2

body response in Postman
```postman
{
    "playedGame": true
}

###STEP 4:###

Time to check who won

Use the game id

Use GET method

Examples:

https://localhost:44302/api/games/6b142927-361c-4b3c-8c0b-ee9529673d96

body response in Postman
```postman
{
    "successful": true,
    "status": 6
}

Status 6 means player two won, in this case Jen won



## Language/Frameworks
.NET Core 5.0