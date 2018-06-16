using API_ScoreCard.Authorization;
using API_ScoreCard.Repositories;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_ScoreCard.Controllers
{
    [RoutePrefix("players")]
    public class PlayerController : ApiController
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [AuthorizeUser]
        [Route("{id:Guid}"), HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var player = _playerRepository.GetPlayer(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [Route(""), HttpPost]
        public IHttpActionResult Post(PlayerPostDto dto)
        {
            var result = _playerRepository.CreatePlayer(dto);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [AuthorizeUser]
        [Route("{id:Guid}"), HttpPut]
        public IHttpActionResult Put([FromUri] Guid id, PlayerPatchDto dto)
        {
            var result = _playerRepository.UpdatePlayer(dto, id);
            if (result) return Ok();
            return NotFound();
        }

        [AuthorizeAdmin]
        [Route("{id:Guid}"), HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var result = _playerRepository.DeletePlayer(id);
            if (result) return Ok();
            return NotFound();
        }
    }
}