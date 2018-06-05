using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace API_ScoreCard.Repositories
{
    public class StatisticsRepository : RepositoryBase, IStatisticsRepository
    {
        public StatisticsRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}