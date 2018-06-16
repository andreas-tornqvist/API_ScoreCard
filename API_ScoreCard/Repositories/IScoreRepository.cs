using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;

namespace API_ScoreCard.Repositories
{
    public interface IScoreRepository : IRepositoryBase
    {
        string ReportScore(ScorePostDto dto);
        string ApproveScore(Guid playerId, Guid scoreId);
        string RemoveApproval(Guid playerId, Guid scoreId);
    }
}