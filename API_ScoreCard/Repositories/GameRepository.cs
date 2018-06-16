using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;
using NHibernate;
using Infrastructure.Domain;
using Common.Enumerators;
using Infrastructure.ExtensionMethods;
using NHibernate.Criterion;

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

        public GameModel GetGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameModel> GetGamesByState(GameState state)
        {
            var games = Session.Query<GameModel>().Where(s => s.State == state);
            return games;
        }

        public IEnumerable<GameModel> GetPlayersGames(Guid playerId)
        {
            var isPlayerExist = Session.Get<PlayerModel>(playerId);
            if (isPlayerExist == null) return null;
            var cards = Session.QueryOver<PlayerModel>()
                .Where(p => p.Id == playerId)
                .Future().FirstOrDefault().Cards;
            var games = cards.Select(c => c.Game).Distinct();
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
            var game = Session.Get<GameModel>(gameId);
            if (game == null) return null;

            if (game.State != GameState.Pending)
                return "Det går inte att lämna spelet när det har startats. Spelar måste lämna DNF.";

            if (!game.Cards.First().Players.Any(p => p.Id == playerId))
                return "Spelaren är inte registrerad till spelet.";

            var card = Session.Get<CardModel>(game.Cards.First().Id);
            using (var transaction = Session.BeginTransaction())
            {
                card.Players.Remove(card.Players.First(p => p.Id == playerId));
                Session.Update(card);
                transaction.Commit();
            }
            return null;
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