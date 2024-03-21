using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTest.Data.Context;

namespace TechTest.Api.IntegrationTests.Config;


internal class AppFactory<Program> : WebApplicationFactory<Program> where Program : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    protected async override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            //builder.UseEnvironment("Testing");
            // Remove the existing DbContext registration

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();
        });
    }
}