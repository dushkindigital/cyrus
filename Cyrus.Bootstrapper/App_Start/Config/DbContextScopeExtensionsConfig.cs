using Cyrus.Data;
using Cyrus.Data.Extensions;

namespace Cyrus.Bootstrapper.Config
{
    public static class DbContextScopeExtensionConfig
    {
        public static void Setup()
        {
            DbContextScopeExtensions.GetDbContextFromCollection = (collection, type) =>
            {
                if (type == typeof(ICyrusDbContext))
                    return collection.Get<CyrusDbContext>();
                return null;
            };

            DbContextScopeExtensions.GetDbContextFromLocator = (locator, type) =>
            {
                if (type == typeof(ICyrusDbContext))
                    return locator.Get<CyrusDbContext>();
                return null;

            };
        }
    }
}
