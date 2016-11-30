using Cyrus.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Cyrus.Data.Impl;
using System.Configuration;
using System;

namespace Cyrus.Bootstrapper
{
    public class CyrusContextDevelopmentConfiguration : DbConfiguration
    {
        private static readonly object Lock = new object();
        private static bool _databaseInitialized;

        public CyrusContextDevelopmentConfiguration()
        {

            //Database.Log = logger.Log;
            
            _databaseInitialized = 
                !Convert.ToBoolean(ConfigurationManager.AppSettings["AutoBuildDatabase"]);

            if (_databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    // Set the database intializer which is run once during application start
                    // This seeds the database with admin user credentials and admin role
                    SetDatabaseInitializer<CyrusDbContext>(new CyrusDbInitializer());
                    SetDefaultConnectionFactory(new LocalDbConnectionFactory("MSSQLLocalDB"));
                    
                    _databaseInitialized = true;
                }
            }
        }
    }

    public class CyrusDbInitializer : DropCreateDatabaseAlways<CyrusDbContext>
    {
        protected override void Seed(CyrusDbContext context)
        {
            CyrusDbSeed.Seed(context);
            base.Seed(context);
        }
    }
}