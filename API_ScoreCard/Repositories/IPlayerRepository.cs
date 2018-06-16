using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ScoreCard.Repositories
{
    public interface IPlayerRepository : IRepositoryBase
    {
        PlayerResponseDto GetPlayer(Guid id);
        Guid CreatePlayer(PlayerPostDto dto);
        bool UpdatePlayer(PlayerPatchDto dto, Guid id);
        bool DeletePlayer(Guid id);
    }
}