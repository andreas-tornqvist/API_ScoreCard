using API_ScoreCard.Repositories;
using Common.Dtos;
using Common.Enumerators;
using Infrastructure.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_ScoreCard.Controllers
{
    [RoutePrefix("games")]
    public class GameController : ApiController
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Route("{state:int}"), HttpGet]
        public IHttpActionResult GetGames(int state)
        {
            var response = _gameRepository.GetGamesByState((GameState)state);
            return Ok(response);
        }

        [Route("{playerId:Guid}/player"), HttpGet]
        public IHttpActionResult GetPlayersGames(Guid playerId)
        {
            var response = _gameRepository.GetPlayersGames(playerId);
            if (response == null)
                return BadRequest();
            return Ok(response.Select(g => g.ToDto()));
        }

        [Route("{gameId:Guid}"), HttpGet]
        public IHttpActionResult GetGame(Guid gameId)
        {
            var response = _gameRepository.GetGame(gameId);
            if (response == null)
                return NotFound();
            return Ok(response.ToDto());
        }

        [Route(""), HttpPost]
        public IHttpActionResult CreateGame(GamePostDto dto)
        {
            var response = _gameRepository.CreateGame(dto);
            if (response == Guid.Empty)
                return BadRequest();
            return Ok(response);
        }

        [Route("join"), HttpPost]
        public IHttpActionResult AddPlayerToCard(JoinGameDto dto)
        {
            var response = _gameRepository.JoinGame(dto);
            if (response)
                return Ok();
            return BadRequest();
        }

        [Route("leave/{gameId:Guid}"), HttpDelete]
        public IHttpActionResult LeaveCard([FromUri] Guid gameId, [FromUri] Guid playerId)
        {
            var response = _gameRepository.LeaveGame(gameId, playerId);
            if (response == null)
                return Ok();
            return BadRequest(response);
        }

        [Route("state"), HttpPut]
        public IHttpActionResult UpdateGameState(GamePutDto dto)
        {
            var response = _gameRepository.UpdateState(dto);
            if (response)
                return Ok();
            return NotFound();
        }
    }
}