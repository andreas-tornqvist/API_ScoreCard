using ChamberOfSecrets;
using Infrastructure.Data;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
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
            var secretContainer = new SecretContainer("scoreCard_db");
            var connectionString = @"Data Source=SQL6003.site4now.net;Initial Catalog=DB_A2AF2E_scorecard;";
            var connectionStringCredentials = $"User Id={secretContainer.UserName};Password={ secretContainer.Password};";
            var configuration = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString + connectionStringCredentials;
                db.Dialect<MsSql2008Dialect>();
            });
            var mapper = new ModelMapper();
            Type[] myTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            mapper.AddMappings(myTypes);
            var mapping = new NHibernateMapper().Map();
            configuration
                .AddMapping(mapping);

            var sessionFactory = configuration.BuildSessionFactory();

            container.RegisterInstance(configuration);
            container.RegisterInstance(sessionFactory);

            var buildDatabase = true;
            if (buildDatabase)
            {
                var schema = new SchemaExport(configuration);
                schema.Create(false, true);
            }
        }
    }
}