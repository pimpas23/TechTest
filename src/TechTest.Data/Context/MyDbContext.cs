using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechTest.Business.Models;

namespace TechTest.Data.Context
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<CallFilters>();
        }


        public DbSet<CallDetailRecord> CallDetailRecords { get; set; }
    }
}
