using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Engine;

namespace Infrastructure.Data
{
    public class CurrentSessionContext : ICurrentSessionContext
    {
        private readonly ISessionFactoryImplementor _sessionFactoryImplementor;
        public CurrentSessionContext(ISessionFactoryImplementor sessionFactoryImplementor)
        {
            _sessionFactoryImplementor = sessionFactoryImplementor;
        }
        public ISession CurrentSession()
        {
            return _sessionFactoryImplementor.OpenSession();
        }
    }
}
