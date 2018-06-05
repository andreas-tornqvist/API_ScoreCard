using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace API_ScoreCard.Repositories
{
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public CourseRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}