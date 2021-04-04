using RPScygni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RPScygni.Controllers
{
    public class RPSGames : IRPSgames
    {
        private readonly List<Game> games;
        public RPSGames()
        {
            games = new List<Game>();
        }

        public Response CreateGame(Request request)
        {
            foreach (Game activeGame in this.games)
            {
                if (activeGame.Name == request.GameName)
                {
                    return new Response
                    {
                        Error = new Errors
                        {
                            ErrorCode = HttpStatusCode.BadRequest,
                            ErrorDesc = "The game name already exists"
                        }
                    };
                }
            }

            var game = new Game
            {
                GameId = Guid.NewGuid(),
                Name = request.GameName,
                GameStatus = GameStatus.Created
            };
            games.Add(game);

            return new Response
            {
                Successful = true,
                GameId = new GameIdResponse
                {
                    GameId = game.GameId
                }
            };
        }
        public Response JoinGame(Request request)
        {
            if (request == null || request.Player == null)
            {
                return new Response
                {
                    Successful = false
                };
            }

            var game = games.FirstOrDefault(item => item.Name == request.GameName && item.GameId == request.GameId);

            if (game == null)
            {
                return new Response
                {
                    Successful = false,
                    Error = new Errors
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        ErrorDesc = "The game name or id was not found"
                    }
                };
            }
            //check if game is full or finished
            if (game.IsFinished || game.IsFull)
            {
                return new Response
                {
                    Successful = false,
                    Error = new Errors
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        ErrorDesc = "Game is full or finished, use GET game to check status"
                    }
                };
            }
            //Join the game
            if (game.PlayerOne == null)
            {
                game.PlayerOne = request.Player;
            }
            else if (game.PlayerTwo == null)
            {
                game.PlayerTwo = request.Player;
                //Game is now full
                game.IsFull = true;
            }
            else
            {
                return new Response
                {
                    Successful = false,
                    Error = new Errors
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        ErrorDesc = "The game is waiting for players"
                    }
                };
            }

            return new Response
            {
                Successful = true,
                JoinedGame = new JoinGameResponse
                {
                    JoinedGame = true
                }
            };

        }
        public Response PlayGame(Request request)
        {
            //Validate request
            var game = games.FirstOrDefault(item => item.Name == request.GameName);
            if (game == null)
            {
                return new Response
                {
                    Successful = false
                };
            }

            //Check if game is finished
            if (game.IsFinished)
            {
                return new Response
                {
                    Successful = false
                };
            }

            //Check if player is allowed
            if (game.PlayerOne.Name != request.PlayerName && game.PlayerTwo.Name != request.PlayerName)
            {
                return new Response
                {
                    Successful = false
                };
            }

            // Play game
            if (game.PlayerOne.Name == request.PlayerName)
            {
                switch (game.GameStatus)
                {
                    case GameStatus.PlayerOneMovePending:
                        game.PlayerOne.Move = request.NextMove;
                        game.IsFinished = true;
                        break;
                    case GameStatus.Created:
                        game.PlayerOne.Move = request.NextMove;
                        game.GameStatus = GameStatus.PlayerTwoMovePending;
                        break;
                    default:
                        return new Response
                        {
                            Successful = false
                        };
                }

            }
            //Check player two (pending)
            else if (game.PlayerTwo.Name == request.PlayerName)
            {
                switch (game.GameStatus)
                {
                    case GameStatus.PlayerTwoMovePending:
                        game.PlayerTwo.Move = request.NextMove;
                        game.IsFinished = true;
                        break;
                    case GameStatus.Created:
                        game.PlayerTwo.Move = request.NextMove;
                        game.GameStatus = GameStatus.PlayerOneMovePending;
                        break;
                    default:
                        return new Response
                        {
                            Successful = false
                        };
                }
            }

            return new Response
            {
                Successful = true,
                PlayedGame = new PlayGameResponse
                {
                    PlayedGame = true
                }
            };
        }

        public Response CheckGameStatus(Request request)
        {
            var game = this.games.FirstOrDefault(item => item.GameId == request.GameId);
            if (game == null)
            {
                return new Response
                {
                    Successful = false,
                    Error = new Errors
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        ErrorDesc = "The was not found"
                    }
                };
            }

            GameStatus status;
            if (game.IsFinished)
            {
                status = DetermineWinner(game.PlayerOne.Move, game.PlayerTwo.Move);
            }
            else
            {
                status = game.GameStatus;
            }

            return new Response
            {

                Successful = true,
                Status = status
            };
        }

        private static GameStatus DetermineWinner(Move playerOne, Move playerTwo)
        {

            if (playerOne == Move.Rock && playerTwo == Move.Paper)
            {

                return GameStatus.PlayerTwoWon;
            }

            else if (playerTwo == Move.Rock && playerOne == Move.Paper)
            {
                return GameStatus.PlayerOneWon;
            }

            else if (playerOne == Move.Scissors && playerTwo == Move.Paper)
            {
                return GameStatus.PlayerOneWon;
            }

            else if (playerTwo == Move.Scissors && playerOne == Move.Paper)
            {
                return GameStatus.PlayerTwoWon;
            }

            else if (playerOne == Move.Paper && playerTwo == Move.Scissors)
            {
                return GameStatus.PlayerTwoWon;
            }

            else if (playerTwo == Move.Paper && playerOne == Move.Scissors)
            {
                return GameStatus.PlayerOneWon;
            }

            else
                return GameStatus.Tie;
        }
    }
}

