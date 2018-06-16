using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Common.Dtos;
using Infrastructure.Domain;
using Infrastructure.ExtensionMethods;

namespace API_ScoreCard.Repositories
{
    public class ScoreRepository : RepositoryBase, IScoreRepository
    {
        public ScoreRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public string ApproveScore(Guid playerId, Guid scoreId)
        {
            var score = Session.Get<ScoreModel>(scoreId);
            if (score == null) return "Rapporten finns inte";

            var player = Session.Get<PlayerModel>(playerId);
            if (player == null) return "Spelaren finns inte";

            if (score.ApprovedBy == null) score.ApprovedBy = new List<PlayerModel>();
            score.ApprovedBy.Add(player);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(score);
                transaction.Commit();
            }
            return null;
        }

        public string RemoveApproval(Guid playerId, Guid scoreId)
        {
            var score = Session.Get<ScoreModel>(scoreId);
            if (score == null) return "Rapporten finns inte";

            if (score.ApprovedBy == null)
                return "Spelaren har inte godkännt resultatet";

            var player = score.ApprovedBy.FirstOrDefault(p => p.Id == playerId);
            if (player == null) return "Spelaren finns inte";


            score.ApprovedBy.Remove(player);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(score);
                transaction.Commit();
            }
            return null;
        }

        public string ReportScore(ScorePostDto dto)
        {
            var card = Session.Get<CardModel>(dto.CardId);
            if (card == null) return "Kortet finns inte";
            var player = Session.Get<PlayerModel>(dto.PlayerId);
            if (player == null) return "Spelaren finns inte";
            ScoreModel score;
            if (player.Scores.Any(s => s.Card.Id == card.Id && s.IsDNF))
                score = new ScoreModel
                {
                    Player = player,
                    Card = card,
                    IsDNF = true
                };
            else
                score = dto.ToEntity(player, card);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(score);
                transaction.Commit();
            }
            return null;
        }
    }
}