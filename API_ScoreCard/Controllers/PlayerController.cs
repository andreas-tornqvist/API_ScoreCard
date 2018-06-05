using API_ScoreCard.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_ScoreCard.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
    }
}