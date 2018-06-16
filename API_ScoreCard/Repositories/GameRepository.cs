using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;
using NHibernate;
using Infrastructure.Domain;
using Common.Enumerators;
using Infrastructure.ExtensionMethods;

namespace API_ScoreCard.Repositories
{
    public class GameRepository : RepositoryBase, IGameRepository
    {
        public GameRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Guid CreateGame(GamePostDto dto)
        {
            var creator = Session.Get<PlayerModel>(dto.CreatorId);
            if (creator == null) return Guid.Empty;

            var course = Session.Get<CourseModel>(dto.CourseId);
            if (course == null) return Guid.Empty;

            var gatheringCard = new CardModel { IsGatheringCard = true };
            var game = dto.ToEntity(creator, course, gatheringCard);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(game);
                transaction.Commit();
            }
            return game.Id;
        }

        public IEnumerable<GameModel> GetGamesByState(GameState state)
        {
            var games = Session.Query<GameModel>().Where(s => s.State == state);
            return games;
        }

        public bool JoinGame(JoinGameDto dto)
        {
            var player = Session.Get<PlayerModel>(dto.PlayerId);
            if (player == null) return false;

            var game = Session.Get<GameModel>(dto.GameId);
            if (game == null) return false;

            var gatheringCard = game.Cards.First(c => c.IsGatheringCard);
            if (gatheringCard.Players == null) gatheringCard.Players = new List<PlayerModel>();
            gatheringCard.Players.Add(player);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(gatheringCard);
                transaction.Commit();
            }
            return true;

        }

        public string LeaveGame(Guid gameId, Guid playerId)
        {
            
        }

        public bool UpdateState(GamePutDto dto)
        {
            var game = Session.Get<GameModel>(dto.Id);
            if (game == null)
                return false;

            using (var transaction = Session.BeginTransaction())
            {
                game.State = (GameState)dto.State;
                Session.Update(game);
                transaction.Commit();
            }
            return true;
        }
    }
}