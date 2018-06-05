using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace API_ScoreCard.Repositories
{
    public class GroupRepository : RepositoryBase, IGroupRepository
    {
        public GroupRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}