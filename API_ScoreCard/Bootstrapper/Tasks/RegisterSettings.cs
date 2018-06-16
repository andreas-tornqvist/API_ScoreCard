using Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace API_ScoreCard.Bootstrapper.Tasks
{
    public static class RegisterSettings
    {
        public static void Register(IUnityContainer container)
        {
            EnvironmentEnum environment;
            var environmentString = System.Configuration.ConfigurationManager.AppSettings["environment"];
            if (environmentString.ToLower() == "local")
                environment = EnvironmentEnum.Local;
            else
                environment = EnvironmentEnum.Production;
            var settings = new Settings(environment);
            container.RegisterInstance(settings);
        }
    }
}