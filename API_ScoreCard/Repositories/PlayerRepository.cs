using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace API_ScoreCard.Repositories
{
    public class PlayerRepository : RepositoryBase, IPlayerRepository
    {
        public PlayerRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}