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



## Language/Frameworks
.NET Core 5.0