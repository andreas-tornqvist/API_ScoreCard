using API_ScoreCard.Repositories;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_ScoreCard.Controllers
{
    public class TourController : ApiController
    {
        private readonly ITourRepository _tourRepository;
        public TourController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }
        [Route(""), HttpPost]
        public IHttpActionResult Post(TourPostDto dto)
        {
            var response = _tourRepository.CreateTour(dto);
            if (response == null) return Created(Request.RequestUri, response);
            return BadRequest();
        }
    }
}