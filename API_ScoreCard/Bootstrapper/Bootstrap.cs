using API_ScoreCard.Bootstrapper.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API_ScoreCard.Bootstrapper
{
    public static class Bootstrap
    {
        public static void Start()
        {
            var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            ConfigureNHibernate.Configure(container);

        }
    }
}