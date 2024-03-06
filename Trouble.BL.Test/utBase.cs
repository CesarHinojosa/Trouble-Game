using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Trouble.Bl.Test
{
    [TestClass]
    public abstract class utBase
    {
        protected TroubleEntities tc;
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;

        protected DbContextOptions<TroubleEntities> options;

        public utBase() 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            options = new DbContextOptionsBuilder<TroubleEntities>()
                .UseSqlServer(_configuration.GetConnectionString("TroubleConnection"))
                .UseLazyLoadingProxies()
                .Options;

            tc = new TroubleEntities(options);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            transaction = tc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            tc = null;
        }

    }
}
