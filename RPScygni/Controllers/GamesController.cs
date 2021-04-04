using Microsoft.AspNetCore.Mvc;
using RPScygni.Models;
using System;


namespace RPScygni.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IRPSgames service;

        public GamesController(IRPSgames service)
        {
            this.service = service;
        }

        // Create game
        // Post api/games/gameName
        [HttpPost]
        [Route("{gameName}")]
        public ActionResult PostGameName(string gameName)
        {
            var createGameRequest = new Request
            {
                GameName = gameName
            };

            var createGameResponse = this.service.CreateGame(createGameRequest);
            if (createGameResponse.Successful == false)
            {
                return this.BadRequest(createGameResponse.Error);
            }

            return this.Created("api/games/gameName", createGameResponse.GameId);
        }

        // Join Game
        // Post api/games/gameName/playerName/gameId
        [HttpPost]
        [Route("{gameName}/{playerName}/{gameId}")]
        public IActionResult PostJoinGame(string gameName, string playerName, Guid gameId)
        {
            var joinGame = new Request
            {
                GameName = gameName,
                GameId = gameId,
                Player = new Player
                {
                    Id = Guid.NewGuid(),
                    Name = playerName,
                    Move = Move.Empty
                }
            };
            
            var joinGameResponse = this.service.JoinGame(joinGame);
            if (joinGameResponse.Successful == false)
            {
                return this.BadRequest(joinGameResponse.Error);
            }

            return this.Created("api/games/gameName/gameId", joinGameResponse.JoinedGame);
        }
        // Play Game
        // Post api/games/guid/playerId/nextMove
        [HttpPost]
        [Route("{gameName}/{playerName}/{nextMove:int}")]
        public IActionResult PostPlayGame(string gameName, string playerName, Move nextMove)
        {
            var playGame = new Request
            {
                GameName = gameName,
                PlayerName = playerName,
                NextMove = nextMove
            };

            var playGameResponse = this.service.PlayGame(playGame);
            if (playGameResponse.Successful == false)
            {
                return NotFound();
            }
            return this.Created("api/games/guid/playerId/nextMove", playGameResponse.PlayedGame);
        }

        //Get Game Status
        //GET api/games/gameid
        [HttpGet("{gameId}")]
        public ActionResult<Game> GetAvailableGame(Guid gameId)
        {
            var gameStatusRequest = new Request
            {
                GameId = gameId
            };

            var gameStatus = this.service.CheckGameStatus(gameStatusRequest);
            if (gameStatus.Successful == false)
            {
                return this.BadRequest(gameStatus);
            }
           
            return this.Ok(gameStatus);
        }
    }
}
