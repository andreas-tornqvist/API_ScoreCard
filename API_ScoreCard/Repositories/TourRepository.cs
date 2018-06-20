using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;
using NHibernate;
using Infrastructure.Domain;
using NHibernate.Criterion;
using Infrastructure.ExtensionMethods;

namespace API_ScoreCard.Repositories
{
    public class TourRepository : RepositoryBase, ITourRepository
    {
        public TourRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Guid? CreateTour(TourPostDto dto)
        {
            var isTourExist = Session.QueryOver<TourModel>()
                .Where(Restrictions.InsensitiveLike(Projections.Property<TourModel>(t => t.Name), dto.Name))
                .Future().Any();
            PlayerModel player = null;
            if (dto.CreatorId != null)
            {
                player = Session.Get<PlayerModel>(dto.CreatorId);
                if (player == null) return null;
            }
            if (isTourExist) return null;
            var course = Session.Get<CourseModel>(dto.CourseId);
            if (course == null) return null;

            var tour = dto.ToEntity(player, course);
            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(tour);
                transaction.Commit();
            }
            return tour.Id;
        }
    }
}