using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace API_ScoreCard.Repositories
{
    public class GameRepository : RepositoryBase, IGameRepository
    {
        public GameRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}