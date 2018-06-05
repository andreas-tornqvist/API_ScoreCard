using API_ScoreCard.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace API_ScoreCard.Bootstrapper.Tasks
{
    public static class RegisterRepositories
    {
        public static void Register(IUnityContainer container)
        {
            var sessionFactory = container.Resolve<ISessionFactory>();
            var playerRepository = new PlayerRepository(sessionFactory);
            var gameRepository = new GameRepository(sessionFactory);
            var courseRepository = new CourseRepository(sessionFactory);
            var statisticsRepository = new StatisticsRepository(sessionFactory);

            container.RegisterType<IPlayerRepository, PlayerRepository>();
            container.RegisterType<IGameRepository, GameRepository>();
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IStatisticsRepository, StatisticsRepository>();

            container.RegisterInstance(playerRepository);
            container.RegisterInstance(gameRepository);
            container.RegisterInstance(courseRepository);
            container.RegisterInstance(statisticsRepository);
        }
    }
}