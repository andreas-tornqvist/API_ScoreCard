using ChamberOfSecrets;
using Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ScoreCard
{
    public class Settings
    {
        public EnvironmentEnum Environment;
        public bool RebuildDatabase;
        public string ConnectionString { get; set; }
        public Settings(EnvironmentEnum environment)
        {
            Environment = environment;
            if (Environment == EnvironmentEnum.Local)
            {
                RebuildDatabase = false;
                ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ScoreCard;Integrated Security=SSPI;";
            }
            else if (Environment == EnvironmentEnum.Production)
            {
                RebuildDatabase = false;
                var secretContainer = new SecretContainer("scoreCard_db");
                var connectionStringCredentials = $"User Id={secretContainer.UserName};Password={ secretContainer.Password};";
                ConnectionString = @"Data Source=SQL6003.site4now.net;Initial Catalog=DB_A2AF2E_scorecard;" + connectionStringCredentials;

            }
        }
    }
}