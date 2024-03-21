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
                .AddJsonFile("appsettings.Docker.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _dbContext = new MyDbContext(_options);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
