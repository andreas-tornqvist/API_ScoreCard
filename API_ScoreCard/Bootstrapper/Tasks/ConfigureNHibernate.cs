using ChamberOfSecrets;
using Common.Enumerators;
using Infrastructure.Data;
using Infrastructure.EventListeners;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Unity;

namespace API_ScoreCard.Bootstrapper.Tasks
{
    public static class ConfigureNHibernate
    {
        public static void Configure(IUnityContainer container)
        {
            var settings = container.Resolve<Settings>();
            var configuration = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = settings.ConnectionString;
                db.Dialect<MsSql2008Dialect>();
            });
            configuration.CurrentSessionContext<CurrentSessionContext>();
            var mapper = new ModelMapper();
            Type[] myTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            mapper.AddMappings(myTypes);
            var mapping = new NHibernateMapper().Map();
            configuration
                .AddMapping(mapping);
            configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new CoordinatesInsertEventListener() };
            configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new CoordinatesUpdateEventListener() };

            var sessionFactory = configuration.BuildSessionFactory();

            container.RegisterInstance(configuration);
            container.RegisterInstance(sessionFactory);

            if (settings.RebuildDatabase)
            {
                var schema = new SchemaExport(configuration);
                schema.Create(false, true);
            }
        }
    }
}