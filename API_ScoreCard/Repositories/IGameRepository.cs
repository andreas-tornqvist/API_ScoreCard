using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;
using Common.Enumerators;
using Infrastructure.Domain;

namespace API_ScoreCard.Repositories
{
    public interface IGameRepository : IRepositoryBase
    {
        IEnumerable<GameModel> GetGamesByState(GameState state);
        bool UpdateState(GamePutDto dto);
        Guid CreateGame(GamePostDto dto);
        bool JoinGame(JoinGameDto dto);
        string LeaveGame(Guid gameId, Guid playerId);
    }
}