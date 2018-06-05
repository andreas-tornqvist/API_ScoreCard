using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ScoreCard.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        public ISession Session => _sessionFactory.GetCurrentSession();
        private readonly ISessionFactory _sessionFactory;
        public RepositoryBase(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
    }
}