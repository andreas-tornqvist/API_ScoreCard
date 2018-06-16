using API_ScoreCard.Repositories;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_ScoreCard.Controllers
{
    [RoutePrefix("scores")]
    public class ScoreController : ApiController
    {
        private readonly IScoreRepository _scoreRepository;
        public ScoreController(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        [Route("report"), HttpPost]
        public IHttpActionResult ReportScore(ScorePostDto dto)
        {
            var response = _scoreRepository.ReportScore(dto);

            if (response == null)
                return Ok();
            return BadRequest(response);
        }

        [Route("approve/{playerId:Guid}"), HttpPost]
        public IHttpActionResult ApproveScore([FromUri] Guid playerId, [FromUri] Guid scoreId)
        {
            var response = _scoreRepository.ApproveScore(playerId, scoreId);
            if (response == null)
                return Ok();
            return BadRequest(response);
        }

        [Route("approve/{playerId:Guid}"), HttpDelete]
        public IHttpActionResult RemoveApproval([FromUri] Guid playerId, [FromUri] Guid scoreId)
        {
            var response = _scoreRepository.RemoveApproval(playerId, scoreId);
            if (response == null)
                return Ok();
            return BadRequest(response);
        }
    }
}