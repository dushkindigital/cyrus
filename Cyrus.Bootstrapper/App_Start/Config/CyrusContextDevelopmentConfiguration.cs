using Cyrus.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyrus.Data.Impl;

namespace Cyrus.Bootstrapper
{
    public class CyrusContextDevelopmentConfiguration : DbConfiguration
    {
        public CyrusContextDevelopmentConfiguration()
        {
            SetDatabaseInitializer<CyrusDbContext>(new CyrusDbInitializer());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("MSSQLLocalDB"));
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