using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechTest.Data.Context;

namespace TechTest.Api.IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {
        protected readonly MyDbContext _dbContext;
        private readonly DbContextOptions<MyDbContext> _options;

        public IntegrationTestBase()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Use SQLite in-memory database for testing
            _options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Initialize the DbContext with SQLite options
            _dbContext = new MyDbContext(_options);

            // Ensure the database is created and migrated
            //_dbContext.Database.Migrate();

            // Seed test data if necessary
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            //if (File.Exists("techtest.db"))
            //{
            //    File.Delete("techtest.db");
            //}
        }
    }
}
