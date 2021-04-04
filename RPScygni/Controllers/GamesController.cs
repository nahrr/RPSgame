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

        /// <summary>
        /// Create game
        /// POST api/games/gameName
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>ActionResult - Game Id or error message</returns>
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

        /// <summary>
        /// Join game
        /// POST api/games/gameName/playerName/gameId
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="playerName"></param>
        /// <param name="gameId"></param>
        /// <returns>IActionResult - Joined game or error message</returns>
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

        /// <summary>
        /// Play game
        /// POST api/games/gameName/playerName/playerMove
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="playerName"></param>
        /// <param name="playerMove"></param>
        /// <returns>IActionResult - Played game or error message</returns>
        [HttpPost]
        [Route("{gameName}/{playerName}/{playerMove:int}")]
        public IActionResult PostPlayGame(string gameName, string playerName, Move playerMove)
        {
            var playGame = new Request
            {
                GameName = gameName,
                PlayerName = playerName,
                PlayerMove = playerMove
            };

            var playGameResponse = this.service.PlayGame(playGame);
            if (playGameResponse.Successful == false)
            {
                return this.BadRequest(playGameResponse.Error);
            }

            return this.Created("api/games/guid/playerId/nextMove", playGameResponse.PlayedGame);
        }


        /// <summary>
        /// Check game status
        /// Game created = 1
        /// Player one move pending = 2
        /// Player two move pending = 3
        /// Tie = 4
        /// Player one won = 5
        /// Player two won = 6
        /// GET api/games/gameid
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>ActionResult - Game status or error message</returns>
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
